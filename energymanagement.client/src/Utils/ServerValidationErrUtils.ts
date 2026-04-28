import type { ErrorOption, Path } from "react-hook-form";
import {
  generalConstants,
  getMessageFromErrorCode,
  SVEFieldNames,
} from "../globConstants";

export type ServerValidationError = {
  [SVEFieldNames.FieldNameField]: string;
  [SVEFieldNames.ErrorCodeField]: string;
};

const isServerValidationError = (
  error: unknown
): error is ServerValidationError => {
  return (
    typeof error === "object" &&
    error !== null &&
    typeof SVEFieldNames.FieldNameField === "string" &&
    typeof SVEFieldNames.ErrorCodeField === "string" &&
    SVEFieldNames.FieldNameField in error &&
    SVEFieldNames.ErrorCodeField in error
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

export type SetErrObject<T> = {
  fieldName: Path<T>;
  errorOption: ErrorOption;
};
export const getSVEObject = <T = unknown>(
  error: ServerValidationError
): SetErrObject<T> => {
  return {
    fieldName: error[SVEFieldNames.FieldNameField] as Path<T>,
    errorOption: {
      type: "server",
      message: getMessageFromErrorCode(error[SVEFieldNames.ErrorCodeField]),
    },
  };
};
const getFieldToErrorsMap = (errors: ServerValidationError[]) => {
  return errors.reduce((acc, curr) => {
    if (acc.get(curr[SVEFieldNames.FieldNameField]) === undefined) {
      acc.set(curr[SVEFieldNames.FieldNameField], []);
    }
    acc.get(curr[SVEFieldNames.FieldNameField])!.push(curr);
    return acc;
  }, {} as Map<string, ServerValidationError[]>);
};

const getSetErrObjectsFromMap = <T = unknown>(
  fieldToErrors: Map<string, ServerValidationError[]>
) => {
  const setErrObjects = [] as SetErrObject<T>[];
  fieldToErrors.forEach((group) => {
    const fieldName = group[0][SVEFieldNames.FieldNameField];
    const typesRecord: Record<string, string> = {};
    group.forEach((err) => {
      typesRecord[err[SVEFieldNames.ErrorCodeField]] = getMessageFromErrorCode(
        err[SVEFieldNames.ErrorCodeField]
      );
    });
    setErrObjects.push({
      fieldName: fieldName as Path<T>,
      errorOption: {
        type: "server",
        message: getMessageFromErrorCode(
          group[0][SVEFieldNames.ErrorCodeField]
        ),
        types: typesRecord,
      },
    });
  });
  return setErrObjects;
};
export const getSetErrObjectsFromCol = <T = unknown>(
  errors: ServerValidationError[]
): SetErrObject<T>[] => {
  const fieldToErrors: Map<string, ServerValidationError[]> =
    getFieldToErrorsMap(errors);
  const setErrObjects = getSetErrObjectsFromMap<T>(fieldToErrors);

  return setErrObjects;
};

export type ISEObject = {
  fieldName: "root";
  errorOption: ErrorOption;
};

export const getISEObject = (): ISEObject => {
  return {
    fieldName: "root",
    errorOption: {
      type: "server",
      message: getMessageFromErrorCode(generalConstants.InternalServerErrorMsg),
    },
  };
};
