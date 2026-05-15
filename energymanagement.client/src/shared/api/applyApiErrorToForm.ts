import type { FieldValues, Path, UseFormSetError } from "react-hook-form";
import { ApiError } from "./fetchJson";
import { problemDetailsToFormErrors } from "./problemDetails";
import { fallbackErrorMessage } from "../errors/clientErrorMessages";

export const applyApiErrorToForm = <TFormValues extends FieldValues>(
  error: unknown,
  setError: UseFormSetError<TFormValues>,
  fieldNameMap: Record<string, string>,
) => {
  if (error instanceof ApiError && error.problemDetails) {
    problemDetailsToFormErrors(error.problemDetails, fieldNameMap).forEach(
      (formError) => {
        setError(formError.fieldName as Path<TFormValues>, {
          type: "server",
          message: formError.message,
          types: formError.types,
        });
      },
    );
    return;
  }

  setError("root" as Path<TFormValues>, {
    type: "server",
    message: fallbackErrorMessage,
  });
};

