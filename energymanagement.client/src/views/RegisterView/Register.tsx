import { useRegister } from "../../hooks/useRegister";
import "../../features/auth/register/ui/registerForm.css";

import { Password } from "../../shared/ui/form/PasswordGroup";
import { FormButton } from "../../shared/ui/form/FormButton";
import { FormGroup } from "../../shared/ui/form/FormGroup";
import { FormTitle } from "../../shared/ui/form/FormTitle";
import { FormField } from "../../shared/ui/form/FormField";
import { registerConst } from "./registerConst";
import { NavLink } from "react-router-dom";
import { ClientRoutes } from "../../globConstants";

type registerProps = {setRootError:React.Dispatch<React.SetStateAction<string>>}

export const Register: React.FC<registerProps> = ({ setRootError }) => {

  const { register, handleSubmit, errors, isSubmitting, fieldNames } =
    useRegister();
    if(errors.root?.message){
      setRootError(errors.root.message)
    }
    
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
        <FormButton type="submit" aria-label={registerConst.submitButtonAriaLabel}>
          {isSubmitting ? registerConst.buttonSubmittingText : registerConst.submitButtonText}
        </FormButton>
      </FormGroup>

      <FormGroup addClassName="redirectGroup">
        <p>
          {registerConst.loginLinkText.getFirstPartTrim()}{" "}
          <NavLink  to={ClientRoutes.Login.Path}>
            {registerConst.loginLinkText.linkText}
          </NavLink>
          {" "}{registerConst.loginLinkText.getSecondPartTrim()}
        </p>
      </FormGroup>
      
      
    </form>

  );
};
