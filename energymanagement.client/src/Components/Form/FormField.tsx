import type { FieldError, FieldValues } from "react-hook-form";
import type { DebouncedFormRegister } from "../../shared/form/formTypes";
import { FormFieldBase } from "./FormFieldBase";

export const FormField = ({
  registerFormFn,
  fieldName,
  inputId,
  errorId,
  error,
  labelText,
  placeHolder,
}: {
  registerFormFn: DebouncedFormRegister<FieldValues>;
  fieldName: string;
  inputId: string;
  errorId: string;
  error?: FieldError;
  labelText: string;
  placeHolder: string;
}) => {
  return (
    <>
      <FormFieldBase
        labelText={labelText}
        inputId={inputId}
        inputErrorId={errorId}
        error={error}
      >
        <input
        className="formControl"
        type="text"
        id={inputId}
        placeholder={placeHolder}
        aria-errormessage={error ? errorId : undefined}
        aria-invalid={error ? "true" : "false"}
        {...registerFormFn(fieldName)}
        />
      </FormFieldBase>
    </>
  );
};
