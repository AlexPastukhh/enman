import {
  loginClientAccount as postLoginClientAccount,
  type LoginRequestDto,
} from "../../../../shared/api/authApi";
import { loginFieldNames, type LoginFormValues } from "../model/loginSchema";

export const loginClientAccount = (
  values: LoginFormValues,
): ReturnType<typeof postLoginClientAccount> => {
  const request: LoginRequestDto = {
    email: values[loginFieldNames.email],
    password: values[loginFieldNames.password],
  };

  return postLoginClientAccount(request);
};
