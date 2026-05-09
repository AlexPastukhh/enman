import type { UseFormSetError } from "react-hook-form";
import {
  getServerErrorsIfAny,
  isProblemDetails,
} from "./problemDetailsFactory";
import { generalConstants } from "../globConstants";
import { parseInternalServerErrorsForSetting, parseServerErrorsForSetting } from "./setErrorObjectUtils";

export const handleErrorResponse = <T extends Record<string, unknown>>(
  responseBody: unknown,
  setErrorFunc: UseFormSetError<T>
) => {
  if (
    isProblemDetails(responseBody) &&
    responseBody.status == generalConstants.ValidationErrorStatusCode
  ) {
    const serverErrors = getServerErrorsIfAny(responseBody);
    if (serverErrors) {

      const errorObjectsToSet = parseServerErrorsForSetting(serverErrors);
      errorObjectsToSet.forEach((errObj) => {
        setErrorFunc(errObj.fieldName, errObj.errorOption);
      });
      return;
    }
  }
  const internalErrObj = parseInternalServerErrorsForSetting();
  setErrorFunc(internalErrObj.fieldName, internalErrObj.errorOption);
};

