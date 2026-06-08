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

type NormalizedValidationError = {
  fieldName: string;
  errorCode: string;
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

const normalizeValidationError = (
  value: unknown,
): NormalizedValidationError | null => {
  if (isServerValidationError(value)) {
    return {
      fieldName: value[serverValidationFieldNames.fieldName],
      errorCode: value[serverValidationFieldNames.errorCode],
    };
  }

  return null;
};

export const getServerValidationErrors = (
  problemDetails: ProblemDetails,
): NormalizedValidationError[] => {
  const errors = problemDetails[generalConstants.errorsCollectionName];
  if (!Array.isArray(errors)) {
    return [];
  }

  return errors
    .map(normalizeValidationError)
    .filter((error): error is NormalizedValidationError => error !== null);
};

const getMessageFromValidationError = (
  error: NormalizedValidationError,
): string => {
  if (error.fieldName === "document.contentType") {
    return "\u0417\u0430\u0433\u0440\u0443\u0437\u0438\u0442\u0435 PDF-\u0444\u0430\u0439\u043b.";
  }

  if (error.fieldName === "document.sizeBytes") {
    return "\u0424\u0430\u0439\u043b \u0434\u043e\u043b\u0436\u0435\u043d \u0431\u044b\u0442\u044c \u043d\u0435 \u043f\u0443\u0441\u0442\u044b\u043c \u0438 \u043d\u0435 \u0431\u043e\u043b\u044c\u0448\u0435 10 \u041c\u0411.";
  }

  if (error.fieldName === "document") {
    return "\u0412\u044b\u0431\u0435\u0440\u0438\u0442\u0435 PDF-\u0444\u0430\u0439\u043b.";
  }

  return getMessageFromErrorCode(error.errorCode);
};

export const problemDetailsToErrorMessage = (
  problemDetails: ProblemDetails | null,
): string => {
  if (problemDetails === null) {
    return fallbackErrorMessage;
  }

  const serverErrors = getServerValidationErrors(problemDetails);
  if (serverErrors.length > 0) {
    return serverErrors.map(getMessageFromValidationError).join("\n");
  }

  if (problemDetails.detail && problemDetails.detail !== "Validation Error") {
    return problemDetails.detail;
  }

  if (problemDetails.title && problemDetails.title !== "Validation Error") {
    return problemDetails.title;
  }

  return fallbackErrorMessage;
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
    const serverFieldName = error.fieldName;
    const fieldName = fieldNameMap[serverFieldName] ?? serverFieldName;
    const existing = groups.get(fieldName) ?? [];
    existing.push(error);
    groups.set(fieldName, existing);
    return groups;
  }, new Map<string, NormalizedValidationError[]>());

  return [...groupedErrors.entries()].map(([fieldName, errors]) => {
    const types: Record<string, string> = {};
    errors.forEach((error) => {
      types[error.errorCode] = getMessageFromValidationError(error);
    });

    return {
      fieldName,
      message: getMessageFromValidationError(errors[0]),
      types,
    };
  });
};
