import { ServerErrorFieldNames } from "../globConstants";

export type ServerValidationError = {
  [ServerErrorFieldNames.FieldNameField]: string;
  [ServerErrorFieldNames.ErrorCodeField]: string;
};

export const isServerValidationError = (
  error: unknown
): error is ServerValidationError => {
  return (
    typeof error === "object" &&
    error !== null &&
    typeof ServerErrorFieldNames.FieldNameField === "string" &&
    typeof ServerErrorFieldNames.ErrorCodeField === "string" &&
    ServerErrorFieldNames.FieldNameField in error &&
    ServerErrorFieldNames.ErrorCodeField in error
  );
};

export const isServerValidationErrorArray = (
  errors: unknown
): errors is ServerValidationError[] => {
  return (
    typeof errors === "object" &&
    errors !== null &&
    Array.isArray(errors) &&
    errors.every(isServerValidationError)
  );
};


export const getFieldToServerErrorsMap = (errors: ServerValidationError[]) => {
  return errors.reduce((acc, curr) => {
    if (acc.get(curr[ServerErrorFieldNames.FieldNameField]) === undefined) {
      acc.set(curr[ServerErrorFieldNames.FieldNameField], []);
    }
    acc.get(curr[ServerErrorFieldNames.FieldNameField])!.push(curr);
    return acc;
  }, new Map<string, ServerValidationError[]>());
};


