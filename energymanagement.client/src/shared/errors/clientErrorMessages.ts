import { errorCodes } from "../constants/generatedConstants";

export const fallbackErrorMessage = "Something went wrong";

export const errorToMessageMap: Record<string, string> = {
  [errorCodes.Email.IsInvalid]:
    "Email format is invalid. Use name@example.com.",
  [errorCodes.Email.IsRegisteredAlready]: "Email is already registered.",
  [errorCodes.Email.IsRequired]: "Email is required.",
  [errorCodes.Password.IsTooShort]:
    "Password is too short. Use at least 12 characters.",
  [errorCodes.Password.IsRequired]: "Password is required.",
  [errorCodes.Password.IsTooLong]:
    "Password is too long. Use no more than 50 characters.",
  [errorCodes.Password.LacksSpecialChars]:
    "Password must contain a special character, for example !, @, #, $, %, ^, &, *, (, or ).",
  [errorCodes.PasswordConfirmation.DoesNotMatch]:
    "Password confirmation does not match.",
  [errorCodes.PasswordConfirmation.IsRequired]:
    "Password confirmation is required.",
  [errorCodes.Phone.IsRequired]: "Phone number is required.",
  [errorCodes.Phone.IsInvalid]:
    "Phone number format is invalid. Use digits with an optional leading +.",
  "account.firstName.is.required": "First name is required.",
  "account.firstName.is.too.large": "First name is too long.",
  "account.middleName.is.required": "Middle name is required.",
  "account.middleName.is.too.large": "Middle name is too long.",
  "account.lastName.is.required": "Last name is required.",
  "account.lastName.is.too.large": "Last name is too long.",
};

export const getMessageFromErrorCode = (code: string): string =>
  errorToMessageMap[code] ?? "An unknown error occurred.";
