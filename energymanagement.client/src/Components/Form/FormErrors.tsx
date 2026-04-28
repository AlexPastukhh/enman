import type { HTMLAttributes } from "react";
import type { FieldError } from "react-hook-form";
import { formConst } from "./formConst";

type props = HTMLAttributes<HTMLParagraphElement> & {
  error?: FieldError;
  id: string;
};


export const FormErrors: React.FC<props> = ({ error: errors, id, ...rest }) => {
  const errorClass = errors ? "errorsVisible" : "errorsHidden";
  const errMsg = errors
    ? errors.types
      ? formConst.getMultipleErrorsStr(errors.types)
      : errors.message
    : undefined;
  return (
    <p id={id} className={`formErrors ${errorClass}`} {...rest}>
      {errMsg}
    </p>
  );
};
