import z from "zod";
import {
  errorCodes,
  getMessageFromErrorCode,
  LoginConstants,
  RegisterIndividualClientConstants,
} from "../globConstants";
import type { FieldValues } from "react-hook-form";

export type TDto = FieldValues;
export type TOfPath = Record<string, unknown>;

const registerFieldNames: RegisterIndividualClientConstants["FieldNames"] =
  RegisterIndividualClientConstants.FieldNames;

const registerScheme = z
  .object({
    [registerFieldNames.email]: z
      .string()
      .regex(
        /^(.+)@(.+)$/,
        getMessageFromErrorCode(errorCodes.Email.IsInvalid)
      ),
    [registerFieldNames.password]: z
      .string()
      .min(12, getMessageFromErrorCode(errorCodes.Password.IsTooShort))
      .max(50, getMessageFromErrorCode(errorCodes.Password.IsTooLong))
      .regex(
        /[!@#$%^&*()]/,
        getMessageFromErrorCode(errorCodes.Password.LacksSpecialChars)
      ),
    [registerFieldNames.passwordConfirmation]: z.string(),
  })
  .refine(
    (data) =>
      data[registerFieldNames.password] ==
      data[registerFieldNames.passwordConfirmation],
    {
      message: getMessageFromErrorCode(
        errorCodes.PasswordConfirmation.DoesNotMatch
      ),
      path: [registerFieldNames.passwordConfirmation],
    }
  );
export type RegisterDto = z.infer<typeof registerScheme>;

const loginFieldNames: LoginConstants["FieldNames"] = LoginConstants.FieldNames;

const loginScheme = z.object({
  [loginFieldNames.email]: z
    .string()
    .regex(/^(.+)@(.+)$/, getMessageFromErrorCode(errorCodes.Email.IsInvalid)),
  [loginFieldNames.password]: z
    .string()
    .min(12,getMessageFromErrorCode( errorCodes.Password.IsTooShort))
    .max(50, getMessageFromErrorCode(errorCodes.Password.IsTooLong)),
});
export type LoginDto = z.infer<typeof loginScheme>;

// const provideIndInfoScheme = z.object({

//     }
// )

export const FormSchemas = {
  Register: {
    scheme: registerScheme,
    fieldNames: registerFieldNames,
  },
  Login: {
    scheme: loginScheme,
    fieldNames: loginFieldNames,
  },
};
