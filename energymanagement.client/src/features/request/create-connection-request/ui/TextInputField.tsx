import type { CreateConnectionRequestFormValues } from "../model/createConnectionRequestTypes";
import { FormErrorMessage } from "./FormErrorMessage";

type TextInputFieldProps = {
  fieldName: keyof CreateConnectionRequestFormValues;
  label: string;
  value: string;
  error?: string;
  onChange: (fieldName: keyof CreateConnectionRequestFormValues, value: string) => void;
  placeholder?: string;
};

export const TextInputField = ({
  fieldName,
  label,
  value,
  error,
  onChange,
  placeholder,
}: TextInputFieldProps) => {
  const inputId = `create-request-${fieldName}`;
  const errorId = `${inputId}-errors`;

  return (
    <div className="formGroup createConnectionRequestForm__field">
      <label htmlFor={inputId}>{label}</label>
      <input
        className="formControl"
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
