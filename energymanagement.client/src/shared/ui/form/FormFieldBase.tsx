import type { FieldError } from "react-hook-form";
import { FormErrors } from "./FormErrors";
import { formConst } from "./formConst";
import "./form.css";

export const FormFieldBase = ({
  labelText,
  inputId,
  inputErrorId,
  error,
  children,
}: {
  labelText: string;
  inputId: string;
  inputErrorId: string;
  error?: FieldError;
  children: React.ReactNode;
}) => {
  return (
    <>
      <label className="" htmlFor={inputId}>
        {labelText}
      </label>
      {children}
      <FormErrors
        aria-label={formConst.getAriaLabelForError(inputId)}
        role="alert"
        id={inputErrorId}
        error={error}
      />
    </>
  );
};
