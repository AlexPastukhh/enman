type FormErrorMessageProps = {
  id: string;
  message?: string;
};

export const FormErrorMessage = ({ id, message }: FormErrorMessageProps) => (
  <p id={id} className={message ? "formErrors errorsVisible" : "formErrors errorsHidden"} role="alert">
    {message}
  </p>
);
