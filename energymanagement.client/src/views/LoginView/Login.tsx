import { FormButton } from "../../shared/ui/form/FormButton";
import { FormField } from "../../shared/ui/form/FormField";
import { FormGroup } from "../../shared/ui/form/FormGroup";
import { FormTitle } from "../../shared/ui/form/FormTitle";
import { Password } from "../../shared/ui/form/PasswordGroup";
import { useLogin } from "../../hooks/useLogin";
import { loginConst } from "./loginConst";

type loginProps = {setRootError:React.Dispatch<React.SetStateAction<string>>}
export const Login: React.FC<loginProps> = ({ setRootError }) => {
  const {
    errors,
    register,
    loginFieldNames,
    handleSubmit,
    isSubmitting,
    isValid,
  } = useLogin();
  const emailLabel = loginConst.emailLabel;
  const emailPlaceholder = loginConst.emailPlaceholder;
  if(errors.root?.message){
    setRootError(errors.root.message)
  }
  return (
    <form className="form" onSubmit={handleSubmit}>
      <FormTitle>{loginConst.loginTitle}</FormTitle>
      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={loginFieldNames.email}
          error={errors[loginFieldNames.email]}
          labelText={emailLabel}
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
      <FormGroup addClassName="closeGroup" >
        <FormButton type="submit" disabled={isSubmitting || !isValid}>
          {isSubmitting ? "Logging in..." : "Login"}
        </FormButton>
      </FormGroup>
    </form>
  );
};
