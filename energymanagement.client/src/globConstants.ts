// src/constants/constants.ts
import constantsJson from '../../Shared/constants.json' with { type: 'json' };
import errorCodesJson from '../../Shared/errorcodes.json' with { type: 'json' };

// Type-safe constants from the shared JSON
export const constants = constantsJson;
export const errorCodes = errorCodesJson;

// Extract field names for easy access
export const RegisterIndividualClientConstants={
    FieldNames : {
        email: constants.AuthConstants.RegisterIndividualClient.Email.FieldName,
        password: constants.AuthConstants.RegisterIndividualClient.Password.FieldName,
        passwordConfirmation: constants.AuthConstants.RegisterIndividualClient.PasswordConfirmation.FieldName,
      } as const
} as const;

export const LoginConstants={
    FieldNames : {
        email: constants.AuthConstants.Login.Email.FieldName,
        password: constants.AuthConstants.Login.Password.FieldName
      } as const
} as const;

export const errorToMessageMap: Record<string, string> = {

  [errorCodesJson.Email.IsInvalid]: "Email format is invalid.",
  [errorCodesJson.Email.IsRegisteredAlready]: "Email is already registered.",
  [errorCodesJson.Email.IsRequired]: "Email is required.",
  [errorCodesJson.Password.IsTooShort]: "Password is too short.",
  [errorCodesJson.Password.IsRequired]: "Password is required.",
  [errorCodesJson.Password.IsTooLong]: "Password is too long.",
  [errorCodesJson.Password.LacksSpecialChars]: "Password must contain special characters.",
  [errorCodesJson.PasswordConfirmation.DoesNotMatch]: "Password confirmation does not match.",
  [errorCodesJson.PasswordConfirmation.IsRequired]: "Password confirmation is required."
}as const;

export const generalConstants ={
  ValidationErrorStatusCode: constants.GeneralConstants.ValidationErrorStatusCode,
  ErrorsCollectionName:constants.GeneralConstants.ErrorsCollectionName,
  ExceptionExtensionName:constants.GeneralConstants.ExceptionExtensionName,
  rootErrorName:"root",
  InternalServerErrorMsg:"Something went wrong"
} as const;

export const ServerErrorFieldNames={
  FieldNameField: errorCodesJson.ServerValidationError.FieldNameField,
  ErrorCodeField: errorCodesJson.ServerValidationError.ErrorCodeField
}as const;

export const Keys={
  SessionQueryKey: "session"
}as const;

export class ClientRoute {
  private value:string;
  constructor(value:string){
    this.value = value;
  }
  get Path():string{
    return this.value;
  }
}

export class ServerRoutes{
  static  RegisterIndividual:ClientRoute  = new ClientRoute(constants.Routes.RegisterIndividualPath);
  static Login: ClientRoute = new ClientRoute(constants.Routes.LoginPath);
  static ProvideIndividualData: ClientRoute = new ClientRoute(constants.Routes.ProvideIndividualClientsDataPath);
  static GetUser: ClientRoute = new ClientRoute(constants.Routes.GetUserPath);
}




export class  ClientRoutes{
  static Home: ClientRoute = new ClientRoute("/"); 
  static Register: ClientRoute = new ClientRoute("/register");
  static Login: ClientRoute = new ClientRoute("/login");
  static Account: ClientRoute = new ClientRoute("/account");
}

export const getMessageFromErrorCode = (code: string): string => {
    
    return errorToMessageMap[code] || "An unknown error occurred.";
}
// Type for the constants
export type ConstantsConfig = typeof constantsJson;
export type RegisterIndividualClientConstants = typeof RegisterIndividualClientConstants;
export type LoginConstants = typeof LoginConstants;