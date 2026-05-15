import {
  generalConstants,
  serverValidationFieldNames,
} from "../constants/generatedConstants";
import {
  fallbackErrorMessage,
  getMessageFromErrorCode,
} from "../errors/clientErrorMessages";

export type ProblemDetails = {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  instance?: string;
  [key: string]: unknown;
};

export type ServerValidationError = {
  [serverValidationFieldNames.fieldName]: string;
  [serverValidationFieldNames.errorCode]: string;
};

export type FormErrorTarget = {
  fieldName: string;
  message: string;
  types?: Record<string, string>;
};

export const isProblemDetails = (value: unknown): value is ProblemDetails =>
  typeof value === "object" &&
  value !== null &&
  "status" in value &&
  "title" in value;

const isServerValidationError = (
  value: unknown,
): value is ServerValidationError =>
  typeof value === "object" &&
  value !== null &&
  serverValidationFieldNames.fieldName in value &&
  serverValidationFieldNames.errorCode in value;

export const getServerValidationErrors = (
  problemDetails: ProblemDetails,
): ServerValidationError[] => {
  const errors = problemDetails[generalConstants.errorsCollectionName];
  if (!Array.isArray(errors)) {
    return [];
  }

  return errors.filter(isServerValidationError);
};

export const problemDetailsToFormErrors = (
  problemDetails: ProblemDetails,
  fieldNameMap: Record<string, string>,
): FormErrorTarget[] => {
  if (problemDetails.status !== generalConstants.validationErrorStatusCode) {
    return [
      {
        fieldName: generalConstants.rootErrorName,
        message: problemDetails.detail ?? fallbackErrorMessage,
      },
    ];
  }

  const serverErrors = getServerValidationErrors(problemDetails);
  if (serverErrors.length === 0) {
    return [
      {
        fieldName: generalConstants.rootErrorName,
        message: problemDetails.detail ?? fallbackErrorMessage,
      },
    ];
  }

  const groupedErrors = serverErrors.reduce((groups, error) => {
    const serverFieldName = error[serverValidationFieldNames.fieldName];
    const fieldName = fieldNameMap[serverFieldName] ?? serverFieldName;
    const existing = groups.get(fieldName) ?? [];
    existing.push(error);
    groups.set(fieldName, existing);
    return groups;
  }, new Map<string, ServerValidationError[]>());

  return [...groupedErrors.entries()].map(([fieldName, errors]) => {
    const types: Record<string, string> = {};
    errors.forEach((error) => {
      const code = error[serverValidationFieldNames.errorCode];
      types[code] = getMessageFromErrorCode(code);
    });

    return {
      fieldName,
      message: getMessageFromErrorCode(
        errors[0][serverValidationFieldNames.errorCode],
      ),
      types,
    };
  });
};

