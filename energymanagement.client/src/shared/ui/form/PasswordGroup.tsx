import { useState } from "react";
import { EyeIcon, EyeIconSlash } from "../../../assets/icons/svgr_barrel";
import type { DebouncedFormRegister } from "../../form/formTypes";
import type { FieldError, FieldValues } from "react-hook-form";
import { FormFieldBase } from "./FormFieldBase";
import { formConst } from "./formConst";

export const Password = ({
  registerFormFn: register,
  fieldName,
  labelText,
  placeHolder,
  passwordInputId,
  passwordErrorId,
  error,
  autoComplete = "current-password",
}: {
  registerFormFn: DebouncedFormRegister<FieldValues>;
  fieldName: string;
  labelText: string;
  placeHolder: string;
  passwordInputId: string;
  passwordErrorId: string;
  error?: FieldError;
  autoComplete?: string;
}) => {
  const [showPassword, setShowPassword] = useState(false);
  return (
    <FormFieldBase
      labelText={labelText}
      inputId={passwordInputId}
      inputErrorId={passwordErrorId}
      error={error}
    >
      <div className="passwordWrapper">
        <input
          className="formControl"
          type={showPassword ? "text" : "password"}
          id={passwordInputId}
          placeholder={placeHolder}
          autoComplete={autoComplete}
          aria-errormessage={error ? passwordErrorId : undefined}
          aria-invalid={error ? "true" : "false"}
          {...register(fieldName)}
        />
        <button
          className="iconsButton"
          type="button"
          onClick={() => setShowPassword(!showPassword)}
          aria-label={
            showPassword
              ? formConst.ariaLabelHidePwd
              : formConst.ariaLabelShowPwd
          }
          aria-pressed={showPassword ? "true" : "false"}
          aria-controls={passwordInputId}
        >
          {showPassword ? (
            <EyeIcon aria-hidden="true" className="icon" />
          ) : (
            <EyeIconSlash aria-hidden="true" className="icon eyeClosedIcon" />
          )}
        </button>
      </div>
    </FormFieldBase>
  );
};
