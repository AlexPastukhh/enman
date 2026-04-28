import type { UseFormSetError } from "react-hook-form";
import {
  getServerErrorsIfAny,
  isProblemDetails,
} from "./problemDetailsFactory";
import {
  getISEObject,
  getSetErrObjectsFromCol,
} from "./ServerValidationErrUtils";
import { generalConstants } from "../globConstants";

export const handleErrorResponse = <T extends Record<string, unknown>>(
  response: Response,
  setErrorFunc: UseFormSetError<T>
) => {
  if (
    isProblemDetails(response) &&
    response.status == generalConstants.ValidationErrorStatusCode
  ) {
    const serverErrors = getServerErrorsIfAny(response);
    if (serverErrors) {
      const serverErrObjs = getSetErrObjectsFromCol(serverErrors);
      serverErrObjs.forEach((errObj) => {
        setErrorFunc(errObj.fieldName, errObj.errorOption);
      });
      return;
    }
  }
  const internalErrObj = getISEObject();
  setErrorFunc(internalErrObj.fieldName, internalErrObj.errorOption);
};
