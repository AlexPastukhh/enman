import { errorCodes } from "../constants/generatedConstants";

export const fallbackErrorMessage = "Something went wrong";

export const errorToMessageMap: Record<string, string> = {
  [errorCodes.Email.IsInvalid]: "Email format is invalid.",
  [errorCodes.Email.IsRegisteredAlready]: "Email is already registered.",
  [errorCodes.Email.IsRequired]: "Email is required.",
  [errorCodes.Password.IsTooShort]: "Password is too short.",
  [errorCodes.Password.IsRequired]: "Password is required.",
  [errorCodes.Password.IsTooLong]: "Password is too long.",
  [errorCodes.Password.LacksSpecialChars]:
    "Password must contain special characters.",
  [errorCodes.PasswordConfirmation.DoesNotMatch]:
    "Password confirmation does not match.",
  [errorCodes.PasswordConfirmation.IsRequired]:
    "Password confirmation is required.",
};

export const getMessageFromErrorCode = (code: string): string =>
  errorToMessageMap[code] ?? "An unknown error occurred.";

