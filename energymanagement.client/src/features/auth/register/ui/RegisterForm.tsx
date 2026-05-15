import { useEffect } from "react";
import { NavLink } from "react-router-dom";
import { FormButton } from "../../../../Components/Form/FormButton";
import { FormField } from "../../../../Components/Form/FormField";
import { FormGroup } from "../../../../Components/Form/FormGroup";
import { FormTitle } from "../../../../Components/Form/FormTitle";
import { Password } from "../../../../Components/Form/PasswordGroup";
import "../../../../styles/Register.css";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { useRegisterForm } from "../model/useRegisterForm";
import { registerConst } from "./registerConst";

type RegisterFormProps = {
  setRootError: React.Dispatch<React.SetStateAction<string>>;
};

export const RegisterForm = ({ setRootError }: RegisterFormProps) => {
  const { register, handleSubmit, errors, isSubmitting, fieldNames } =
    useRegisterForm();

  useEffect(() => {
    setRootError(errors.root?.message ?? "");
  }, [errors.root?.message, setRootError]);

  return (
    <form onSubmit={handleSubmit} className="form registerForm">
      <FormTitle>{registerConst.registerTitle}</FormTitle>

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.email}
          inputId={registerConst.emailInputId}
          errorId={registerConst.emailErrorsId}
          labelText={registerConst.emailLabel}
          placeHolder={registerConst.emailPlaceholder}
          error={errors[fieldNames.email]}
        />
      </FormGroup>

      <FormGroup>
        <Password
          registerFormFn={register}
          fieldName={fieldNames.password}
          error={errors[fieldNames.password]}
          labelText={registerConst.passwordLabel}
          passwordInputId={registerConst.passwordInputId}
          passwordErrorId={registerConst.passwordErrorsId}
          placeHolder={registerConst.passwordPlaceholder}
        />
      </FormGroup>

      <FormGroup>
        <Password
          registerFormFn={register}
          fieldName={fieldNames.passwordConfirmation}
          error={errors[fieldNames.passwordConfirmation]}
          labelText={registerConst.passwordConfirmLabel}
          passwordInputId={registerConst.passwordConfirmInputId}
          passwordErrorId={registerConst.passwordConfirmErrorsId}
          placeHolder={registerConst.passwordConfirmPlaceholder}
        />
      </FormGroup>

      <FormGroup addClassName="closeGroup">
        <p className="formInfoText">{registerConst.formInfoText}</p>
      </FormGroup>

      <FormGroup addClassName="formButtonGroup">
        <FormButton
          type="submit"
          aria-label={registerConst.submitButtonAriaLabel}
        >
          {isSubmitting
            ? registerConst.buttonSubmittingText
            : registerConst.submitButtonText}
        </FormButton>
      </FormGroup>

      <FormGroup addClassName="redirectGroup">
        <p>
          {registerConst.loginLinkText.getFirstPartTrim()}{" "}
          <NavLink to={clientRoutes.login}>
            {registerConst.loginLinkText.linkText}
          </NavLink>{" "}
          {registerConst.loginLinkText.getSecondPartTrim()}
        </p>
      </FormGroup>
    </form>
  );
};
