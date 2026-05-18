import z from "zod";
import { authFieldNames, errorCodes } from "../../../../shared/constants/generatedConstants";
import { getMessageFromErrorCode } from "../../../../shared/errors/clientErrorMessages";

export const loginFieldNames = authFieldNames.login;

const emailRegex = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;

export const loginSchema = z.object({
  [loginFieldNames.email]: z
    .string()
    .regex(emailRegex, getMessageFromErrorCode(errorCodes.Email.IsInvalid)),
  [loginFieldNames.password]: z
    .string()
    .min(12, getMessageFromErrorCode(errorCodes.Password.IsTooShort))
    .max(50, getMessageFromErrorCode(errorCodes.Password.IsTooLong)),
});

export type LoginFormValues = z.infer<typeof loginSchema>;

export const loginServerFieldMap: Record<string, string> = {
  Email: loginFieldNames.email,
  email: loginFieldNames.email,
  Password: loginFieldNames.password,
  password: loginFieldNames.password,
};
