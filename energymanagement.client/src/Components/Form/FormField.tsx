import type { FieldError } from "react-hook-form";
import type { FormRegister } from "../../hooks/useFormRegisterDebounce";
import { FormFieldBase } from "./FormFieldBase";

export const FormField = ({
  registerFormFn,
  inputId,
  errorId,
  error,
  labelText,
  placeHolder,
}: {
  registerFormFn: FormRegister;
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
        {...registerFormFn}
        />
      </FormFieldBase>
    </>
  );
};
