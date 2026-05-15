type ErrorMessageProps = {
  message: string;
};

export const ErrorMessage = ({ message }: ErrorMessageProps) => (
  <div className="errorMessageWrapper" role="alert">
    <p className="errorMessage">{message}</p>
  </div>
);
