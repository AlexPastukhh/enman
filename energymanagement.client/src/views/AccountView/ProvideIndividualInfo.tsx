import { FormButton } from "../../Components/Form/FormButton";
import { FormField } from "../../Components/Form/FormField";
import { FormGroup } from "../../Components/Form/FormGroup";
import { FormTitle } from "../../Components/Form/FormTitle";
import { Password } from "../../Components/Form/PasswordGroup";
import { useLogin } from "../../hooks/useLogin";
import { accountConst } from "./accountConst";
import { loginConst } from "./loginConst";

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
