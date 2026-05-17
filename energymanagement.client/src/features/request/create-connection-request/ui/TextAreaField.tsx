import type { CreateConnectionRequestFormValues } from "../model/createConnectionRequestTypes";
import { FormErrorMessage } from "./FormErrorMessage";

type TextAreaFieldProps = {
  fieldName: keyof CreateConnectionRequestFormValues;
  label: string;
  value: string;
  error?: string;
  onChange: (fieldName: keyof CreateConnectionRequestFormValues, value: string) => void;
  placeholder?: string;
};

export const TextAreaField = ({
  fieldName,
  label,
  value,
  error,
  onChange,
  placeholder,
}: TextAreaFieldProps) => {
  const inputId = `create-request-${fieldName}`;
  const errorId = `${inputId}-errors`;

  return (
    <div className="formGroup createConnectionRequestForm__field">
      <label htmlFor={inputId}>{label}</label>
      <textarea
        className="formControl createConnectionRequestForm__textarea"
        id={inputId}
        value={value}
        placeholder={placeholder}
        aria-invalid={error ? "true" : "false"}
        aria-errormessage={error ? errorId : undefined}
        onChange={(event) => onChange(fieldName, event.target.value)}
      />
      <FormErrorMessage id={errorId} message={error} />
    </div>
  );
};
