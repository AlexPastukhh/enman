import type { ErrorOption, Path } from "react-hook-form";
import { generalConstants, getMessageFromErrorCode, ServerErrorFieldNames } from "../globConstants";
import { getFieldToServerErrorsMap, type ServerValidationError } from "./ServerValidationErrUtils";


export const getSetErrObjectsFromMap = <T = unknown>(
  fieldToErrors: Map<string, ServerValidationError[]>
):SetErrObject<T>[] => {
  const setErrObjects = [] as SetErrObject<T>[];
  fieldToErrors.forEach((group) => {
    const fieldName = group[0][ServerErrorFieldNames.FieldNameField];
    const typesRecord: Record<string, string> = {};
    group.forEach((err) => {
      typesRecord[err[ServerErrorFieldNames.ErrorCodeField]] = getMessageFromErrorCode(
        err[ServerErrorFieldNames.ErrorCodeField]
      );
    });
    setErrObjects.push({
      fieldName: fieldName as Path<T>,
      errorOption: {
        type: "server",
        message: getMessageFromErrorCode(
          group[0][ServerErrorFieldNames.ErrorCodeField]
        ),
        types: typesRecord,
      },
    });
  });
  return setErrObjects;
};
export const parseServerErrorsForSetting = <T = unknown>(
  errors: ServerValidationError[]
): SetErrObject<T>[] => {
  const fieldToErrors: Map<string, ServerValidationError[]> =
    getFieldToServerErrorsMap(errors);
  const setErrObjects = getSetErrObjectsFromMap<T>(fieldToErrors);

  return setErrObjects;
};

export type InternalErrorObj = {
  fieldName: "root";
  errorOption: ErrorOption;
};

export const parseInternalServerErrorsForSetting = (): InternalErrorObj => {
  return {
    fieldName: "root",
    errorOption: {
      type: "server",
      message: getMessageFromErrorCode(generalConstants.InternalServerErrorMsg),
    },
  };
};

export type SetErrObject<T> = {
  fieldName: Path<T>;
  errorOption: ErrorOption;
};
export const parseServerErrorForSetting = <T = unknown>(
  error: ServerValidationError
): SetErrObject<T> => {
  return {
    fieldName: error[ServerErrorFieldNames.FieldNameField] as Path<T>,
    errorOption: {
      type: "server",
      message: getMessageFromErrorCode(error[ServerErrorFieldNames.ErrorCodeField]),
    },
  };
};