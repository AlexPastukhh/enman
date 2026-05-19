import {
  registerClientAccount as postRegisterClientAccount,
  type RegisterClientAccountRequest,
} from "../../../../shared/api/authApi";
import type { RegisterFormValues } from "../model/registerSchema";
import { registerFieldNames } from "../model/registerSchema";

export const registerClientAccount = (
  values: RegisterFormValues,
): Promise<void> => {
  const request: RegisterClientAccountRequest = {
    email: values[registerFieldNames.email],
    password: values[registerFieldNames.password],
  };

  return postRegisterClientAccount(request).then(() => undefined);
};
