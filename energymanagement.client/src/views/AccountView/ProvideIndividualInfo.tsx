import { FormButton } from "../../shared/ui/form/FormButton";
import { FormField } from "../../shared/ui/form/FormField";
import { FormGroup } from "../../shared/ui/form/FormGroup";
import { FormTitle } from "../../shared/ui/form/FormTitle";
import { Password } from "../../shared/ui/form/PasswordGroup";
import { useLogin } from "../../hooks/useLogin";
import { accountConst } from "./accountConst";
import { loginConst } from "../LoginView/loginConst";

export const ProvideIndividualInfo = () => {
  const {
    errors,
    register,
    loginFieldNames,
    handleSubmit,
    isSubmitting,
    isValid,
  } = useLogin();
  const emailLabe = loginConst.emailLabel;
  const emailPlaceholder = loginConst.emailPlaceholder;
  return (
    <form className="form" onSubmit={handleSubmit}>
      <FormTitle>{accountConst.provideInfoTitle}</FormTitle>
      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={loginFieldNames.email}
          error={errors[loginFieldNames.email]}
          labelText={emailLabe}
          placeHolder={emailPlaceholder}
          inputId={loginConst.emailInputId}
          errorId={loginConst.emailErrorsId}
        />
      </FormGroup>

      <FormGroup>
        <Password
          registerFormFn={register}
          fieldName={loginFieldNames.password}
          labelText="Password"
          placeHolder="Enter your password"
          error={errors[loginFieldNames.password]}
          passwordInputId={loginConst.passwordInputId}
          passwordErrorId={loginConst.passwordErrorsId}
        />
      </FormGroup>

      <FormButton type="submit" disabled={isSubmitting || !isValid}>
        {isSubmitting ? "Logging in..." : "Login"}
      </FormButton>
    </form>
  );
};
