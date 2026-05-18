import { NavLink } from "react-router-dom";
import { FormButton } from "../../../../shared/ui/form/FormButton";
import { FormField } from "../../../../shared/ui/form/FormField";
import { FormGroup } from "../../../../shared/ui/form/FormGroup";
import { FormTitle } from "../../../../shared/ui/form/FormTitle";
import { Password } from "../../../../shared/ui/form/PasswordGroup";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { useRegisterForm } from "../model/useRegisterForm";
import "./registerForm.css";
import { registerConst } from "./registerConst";

export const RegisterForm = () => {
  const { register, handleSubmit, errors, isSubmitting, fieldNames } =
    useRegisterForm();

  return (
    <form onSubmit={handleSubmit} className="form registerForm">
      <FormTitle>{registerConst.registerTitle}</FormTitle>
      {errors.root?.message && (
        <p className="formRootError" role="alert">
          {errors.root.message}
        </p>
      )}

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.email}
          inputId={registerConst.emailInputId}
          errorId={registerConst.emailErrorsId}
          labelText={registerConst.emailLabel}
          placeHolder={registerConst.emailPlaceholder}
          type="email"
          autoComplete="email"
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
          autoComplete="new-password"
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
          autoComplete="new-password"
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
