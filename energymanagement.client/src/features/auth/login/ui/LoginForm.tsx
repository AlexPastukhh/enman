import { FormButton } from "../../../../shared/ui/form/FormButton";
import { FormField } from "../../../../shared/ui/form/FormField";
import { FormGroup } from "../../../../shared/ui/form/FormGroup";
import { FormTitle } from "../../../../shared/ui/form/FormTitle";
import { Password } from "../../../../shared/ui/form/PasswordGroup";
import { useLoginForm } from "../model/useLoginForm";
import { loginConst } from "./loginConst";

export const LoginForm = () => {
  const {
    errors,
    register,
    loginFieldNames,
    handleSubmit,
    isSubmitting,
    isValid,
  } = useLoginForm();

  return (
    <form className="form" onSubmit={handleSubmit}>
      <FormTitle>{loginConst.loginTitle}</FormTitle>
      {errors.root?.message && (
        <p className="formRootError" role="alert">
          {errors.root.message}
        </p>
      )}
      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={loginFieldNames.email}
          error={errors[loginFieldNames.email]}
          labelText={loginConst.emailLabel}
          placeHolder={loginConst.emailPlaceholder}
          inputId={loginConst.emailInputId}
          errorId={loginConst.emailErrorsId}
        />
      </FormGroup>

      <FormGroup>
        <Password
          registerFormFn={register}
          fieldName={loginFieldNames.password}
          labelText={loginConst.passwordLabel}
          placeHolder={loginConst.passwordPlaceholder}
          error={errors[loginFieldNames.password]}
          passwordInputId={loginConst.passwordInputId}
          passwordErrorId={loginConst.passwordErrorsId}
        />
      </FormGroup>
      <FormGroup addClassName="closeGroup">
        <FormButton type="submit" disabled={isSubmitting || !isValid}>
          {isSubmitting ? "Logging in..." : loginConst.submitButtonText}
        </FormButton>
      </FormGroup>
    </form>
  );
};
