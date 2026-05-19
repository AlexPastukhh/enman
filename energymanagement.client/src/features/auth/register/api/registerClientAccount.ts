import {
  registerClientAccount as postRegisterClientAccount,
  type L1RegisterRequest,
} from "../../../../shared/api/authApi";
import type { RegisterFormValues } from "../model/registerSchema";
import { registerFieldNames } from "../model/registerSchema";

export const registerClientAccount = (
  values: RegisterFormValues,
): Promise<void> => {
  const request: L1RegisterRequest = {
    email: values[registerFieldNames.email],
    password: values[registerFieldNames.password],
  };

  return postRegisterClientAccount(request).then(() => undefined);
};
