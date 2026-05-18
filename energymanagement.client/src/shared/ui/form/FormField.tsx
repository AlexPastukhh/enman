import type { FieldError, FieldValues } from "react-hook-form";
import type { DebouncedFormRegister } from "../../form/formTypes";
import { FormFieldBase } from "./FormFieldBase";

export const FormField = ({
  registerFormFn,
  fieldName,
  inputId,
  errorId,
  error,
  labelText,
  placeHolder,
  type = "text",
  autoComplete,
}: {
  registerFormFn: DebouncedFormRegister<FieldValues>;
  fieldName: string;
  inputId: string;
  errorId: string;
  error?: FieldError;
  labelText: string;
  placeHolder: string;
  type?: React.HTMLInputTypeAttribute;
  autoComplete?: string;
}) => {
  return (
    <FormFieldBase
      labelText={labelText}
      inputId={inputId}
      inputErrorId={errorId}
      error={error}
    >
      <input
        className="formControl"
        type={type}
        id={inputId}
        placeholder={placeHolder}
        autoComplete={autoComplete}
        aria-errormessage={error ? errorId : undefined}
        aria-invalid={error ? "true" : "false"}
        {...registerFormFn(fieldName)}
      />
    </FormFieldBase>
  );
};
