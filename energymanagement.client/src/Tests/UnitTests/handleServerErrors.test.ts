import { describe, expect, it, vi } from "vitest";
import { errorCodes } from "../../shared/constants/generatedConstants";
import { generalConstants, ServerErrorFieldNames } from "../../globConstants";
import { handleErrorResponse } from "../../Utils/handleErrorResponse";
import { getServerErrorsIfAny, isProblemDetails } from "../../Utils/problemDetailsFactory";
import { parseServerErrorsForSetting } from "../../Utils/setErrorObjectUtils";

describe("server validation error handling", () => {
  it("accepts ProblemDetails without instance", () => {
    const problemDetails = {
      type: "https://problems-registry.smartbear.com/validation-error",
      title: "Validation Error",
      status: generalConstants.ValidationErrorStatusCode,
      detail: "Validation Error",
    };

    expect(isProblemDetails(problemDetails)).toBe(true);
  });

  it("extracts server validation errors from ProblemDetails extensions", () => {
    const serverError = {
      [ServerErrorFieldNames.FieldNameField]: "email",
      [ServerErrorFieldNames.ErrorCodeField]: errorCodes.Email.IsRequired,
    };
    const problemDetails = {
      type: "https://problems-registry.smartbear.com/validation-error",
      title: "Validation Error",
      status: generalConstants.ValidationErrorStatusCode,
      detail: "Validation Error",
      [generalConstants.ErrorsCollectionName]: [serverError],
    };

    expect(getServerErrorsIfAny(problemDetails)).toEqual([serverError]);
  });

  it("groups multiple server validation errors for one form field", () => {
    const parsed = parseServerErrorsForSetting([
      {
        [ServerErrorFieldNames.FieldNameField]: "password",
        [ServerErrorFieldNames.ErrorCodeField]: errorCodes.Password.IsRequired,
      },
      {
        [ServerErrorFieldNames.FieldNameField]: "password",
        [ServerErrorFieldNames.ErrorCodeField]: errorCodes.Password.LacksSpecialChars,
      },
    ]);

    expect(parsed).toHaveLength(1);
    expect(parsed[0].fieldName).toBe("password");
    expect(parsed[0].errorOption.type).toBe("server");
    expect(parsed[0].errorOption.types).toEqual({
      [errorCodes.Password.IsRequired]: "Password is required.",
      [errorCodes.Password.LacksSpecialChars]: "Password must contain special characters.",
    });
  });

  it("sets field errors for validation response and does not also set root internal error", () => {
    const setError = vi.fn();
    const problemDetails = {
      type: "https://problems-registry.smartbear.com/validation-error",
      title: "Validation Error",
      status: generalConstants.ValidationErrorStatusCode,
      detail: "Validation Error",
      [generalConstants.ErrorsCollectionName]: [
        {
          [ServerErrorFieldNames.FieldNameField]: "email",
          [ServerErrorFieldNames.ErrorCodeField]: errorCodes.Email.IsInvalid,
        },
      ],
    };

    handleErrorResponse(problemDetails, setError);

    expect(setError).toHaveBeenCalledTimes(1);
    expect(setError).toHaveBeenCalledWith("email", {
      type: "server",
      message: "Email format is invalid.",
      types: {
        [errorCodes.Email.IsInvalid]: "Email format is invalid.",
      },
    });
  });
});
