import z from "zod";
import { authFieldNames, errorCodes } from "../../../../shared/constants/generatedConstants";
import { getMessageFromErrorCode } from "../../../../shared/errors/clientErrorMessages";

export const registerFieldNames = authFieldNames.register;

const emailRegex = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;

export const registerSchema = z
  .object({
    [registerFieldNames.email]: z
      .string()
      .regex(emailRegex, getMessageFromErrorCode(errorCodes.Email.IsInvalid)),
    [registerFieldNames.password]: z
      .string()
      .min(12, getMessageFromErrorCode(errorCodes.Password.IsTooShort))
      .max(50, getMessageFromErrorCode(errorCodes.Password.IsTooLong))
      .regex(
        /[!@#$%^&*()]/,
        getMessageFromErrorCode(errorCodes.Password.LacksSpecialChars),
      ),
    [registerFieldNames.passwordConfirmation]: z.string(),
  })
  .refine(
    (data) =>
      data[registerFieldNames.password] ===
      data[registerFieldNames.passwordConfirmation],
    {
      message: getMessageFromErrorCode(
        errorCodes.PasswordConfirmation.DoesNotMatch,
      ),
      path: [registerFieldNames.passwordConfirmation],
    },
  );

export type RegisterFormValues = z.infer<typeof registerSchema>;

export const registerServerFieldMap: Record<string, string> = {
  Email: registerFieldNames.email,
  email: registerFieldNames.email,
  Password: registerFieldNames.password,
  password: registerFieldNames.password,
  PasswordConfirmation: registerFieldNames.passwordConfirmation,
  passwordConfirmation: registerFieldNames.passwordConfirmation,
};
