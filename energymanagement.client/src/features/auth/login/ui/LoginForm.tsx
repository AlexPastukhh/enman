import { useEffect } from "react";
import { FormButton } from "../../../../Components/Form/FormButton";
import { FormField } from "../../../../Components/Form/FormField";
import { FormGroup } from "../../../../Components/Form/FormGroup";
import { FormTitle } from "../../../../Components/Form/FormTitle";
import { Password } from "../../../../Components/Form/PasswordGroup";
import { useLoginForm } from "../model/useLoginForm";
import { loginConst } from "./loginConst";

type LoginFormProps = {
  setRootError: React.Dispatch<React.SetStateAction<string>>;
};

export const LoginForm = ({ setRootError }: LoginFormProps) => {
  const {
    errors,
    register,
    loginFieldNames,
    handleSubmit,
    isSubmitting,
    isValid,
  } = useLoginForm();

  useEffect(() => {
    setRootError(errors.root?.message ?? "");
  }, [errors.root?.message, setRootError]);

  return (
    <form className="form" onSubmit={handleSubmit}>
      <FormTitle>{loginConst.loginTitle}</FormTitle>
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
