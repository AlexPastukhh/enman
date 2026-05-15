import {
  loginClientAccount as postLoginClientAccount,
  type L1LoginRequest,
} from "../../../../shared/api/l1AuthApi";
import { loginFieldNames, type LoginFormValues } from "../model/loginSchema";

export const loginClientAccount = (
  values: LoginFormValues,
): ReturnType<typeof postLoginClientAccount> => {
  const request: L1LoginRequest = {
    email: values[loginFieldNames.email],
    password: values[loginFieldNames.password],
  };

  return postLoginClientAccount(request);
};

