import { errorCodes } from "../constants/generatedConstants";

export const fallbackErrorMessage = "Не удалось выполнить действие. Попробуйте ещё раз.";

export const errorToMessageMap: Record<string, string> = {
  [errorCodes.Email.IsInvalid]:
    "Неверный формат email. Используйте формат name@example.com.",
  [errorCodes.Email.IsRegisteredAlready]: "Этот email уже зарегистрирован.",
  [errorCodes.Email.IsRequired]: "Укажите email.",
  [errorCodes.Password.IsTooShort]:
    "Пароль должен содержать не менее 12 символов.",
  [errorCodes.Password.IsRequired]: "Укажите пароль.",
  [errorCodes.Password.IsTooLong]:
    "Пароль должен содержать не более 50 символов.",
  [errorCodes.Password.LacksSpecialChars]:
    "Пароль должен содержать специальный символ, например !, @, #, $, %, ^, &, *.",
  [errorCodes.PasswordConfirmation.DoesNotMatch]:
    "Пароли не совпадают.",
  [errorCodes.PasswordConfirmation.IsRequired]:
    "Подтвердите пароль.",
  [errorCodes.Phone.IsRequired]: "Укажите телефон.",
  [errorCodes.Phone.IsInvalid]:
    "Неверный формат телефона. Используйте цифры и при необходимости знак + в начале.",
  "account.firstName.is.required": "Укажите имя.",
  "account.firstName.is.too.large": "Имя слишком длинное.",
  "account.middleName.is.required": "Укажите отчество.",
  "account.middleName.is.too.large": "Отчество слишком длинное.",
  "account.lastName.is.required": "Укажите фамилию.",
  "account.lastName.is.too.large": "Фамилия слишком длинная.",
};

export const getMessageFromErrorCode = (code: string): string =>
  errorToMessageMap[code] ?? "Не удалось выполнить действие. Попробуйте ещё раз.";
