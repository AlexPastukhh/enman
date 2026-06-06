import { describe, expect, it } from "vitest";
import { problemDetailsToFormErrors } from "./problemDetails";

describe("problemDetailsToFormErrors", () => {
  it("maps server validation errors to form fields", () => {
    const formErrors = problemDetailsToFormErrors(
      {
        title: "Validation Error",
        status: 422,
        errors: [
          {
            FieldName: "Email",
            ErrorCode: "account.email.value.is.invalid",
          },
        ],
      },
      {
        Email: "email",
      },
    );

    expect(formErrors).toEqual([
      {
        fieldName: "email",
        message:
          "\u041d\u0435\u0432\u0435\u0440\u043d\u044b\u0439 \u0444\u043e\u0440\u043c\u0430\u0442 email. \u0418\u0441\u043f\u043e\u043b\u044c\u0437\u0443\u0439\u0442\u0435 \u0444\u043e\u0440\u043c\u0430\u0442 name@example.com.",
        types: {
          "account.email.value.is.invalid":
            "\u041d\u0435\u0432\u0435\u0440\u043d\u044b\u0439 \u0444\u043e\u0440\u043c\u0430\u0442 email. \u0418\u0441\u043f\u043e\u043b\u044c\u0437\u0443\u0439\u0442\u0435 \u0444\u043e\u0440\u043c\u0430\u0442 name@example.com.",
        },
      },
    ]);
  });

  it("maps domain errors after backend converts them to server validation errors", () => {
    const formErrors = problemDetailsToFormErrors(
      {
        title: "Validation Error",
        status: 422,
        errors: [
          {
            FieldName: "Password",
            ErrorCode: "account.password.is.wrong",
          },
        ],
      },
      {
        Password: "password",
      },
    );

    expect(formErrors).toEqual([
      {
        fieldName: "password",
        message:
          "\u041d\u0435\u0432\u0435\u0440\u043d\u044b\u0439 \u043f\u0430\u0440\u043e\u043b\u044c.",
        types: {
          "account.password.is.wrong":
            "\u041d\u0435\u0432\u0435\u0440\u043d\u044b\u0439 \u043f\u0430\u0440\u043e\u043b\u044c.",
        },
      },
    ]);
  });
});
