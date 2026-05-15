import constantsJson from "../../../../Shared/constants.json" with { type: "json" };
import errorCodesJson from "../../../../Shared/errorcodes.json" with { type: "json" };

export const constants = constantsJson;
export const errorCodes = errorCodesJson;

export const authFieldNames = {
  register: {
    email: constants.AuthConstants.RegisterIndividualClient.Email.FieldName,
    password: constants.AuthConstants.RegisterIndividualClient.Password.FieldName,
    passwordConfirmation:
      constants.AuthConstants.RegisterIndividualClient.PasswordConfirmation
        .FieldName,
  },
  login: {
    email: constants.AuthConstants.Login.Email.FieldName,
    password: constants.AuthConstants.Login.Password.FieldName,
  },
} as const;

export const applicantPartyFieldNames = {
  createIndividual: {
    firstName:
      constants.AuthConstants.ProvideIndividualClientsData.FirstName.FieldName,
    middleName:
      constants.AuthConstants.ProvideIndividualClientsData.MiddleName.FieldName,
    lastName:
      constants.AuthConstants.ProvideIndividualClientsData.LastName.FieldName,
    email: constants.AuthConstants.RegisterIndividualClient.Email.FieldName,
    phoneNumber:
      constants.AuthConstants.ProvideIndividualClientsData.PhoneNumber.FieldName,
  },
} as const;

export const serverValidationFieldNames = {
  fieldName: errorCodes.ServerValidationError.FieldNameField,
  errorCode: errorCodes.ServerValidationError.ErrorCodeField,
} as const;

export const generalConstants = {
  validationErrorStatusCode: constants.GeneralConstants.ValidationErrorStatusCode,
  errorsCollectionName: constants.GeneralConstants.ErrorsCollectionName,
  rootErrorName: "root",
} as const;
