import constantsJson from "../../../../Shared/constants.json" with { type: "json" };
import errorCodesJson from "../../../../Shared/errorcodes.json" with { type: "json" };

export const constants = constantsJson;
export const errorCodes = errorCodesJson;

export const authFieldNames = {
  register: {
    email: constants.AuthConstants.RegisterClientAccount.Email.FieldName,
    password: constants.AuthConstants.RegisterClientAccount.Password.FieldName,
    passwordConfirmation: "passwordConfirmation",
  },
  login: {
    email: constants.AuthConstants.Login.Email.FieldName,
    password: constants.AuthConstants.Login.Password.FieldName,
  },
} as const;

export const applicantPartyFieldNames = {
  createIndividual: {
    firstName:
      constants.ApplicantPartyConstants.CreateIndividualApplicantParty.FirstName
        .FieldName,
    middleName:
      constants.ApplicantPartyConstants.CreateIndividualApplicantParty.MiddleName
        .FieldName,
    lastName:
      constants.ApplicantPartyConstants.CreateIndividualApplicantParty.LastName
        .FieldName,
    email:
      constants.ApplicantPartyConstants.CreateIndividualApplicantParty.Email
        .FieldName,
    phoneNumber:
      constants.ApplicantPartyConstants.CreateIndividualApplicantParty
        .PhoneNumber.FieldName,
  },
} as const;

export const serverValidationFieldNames = {
  fieldName: errorCodes.ServerValidationError.FieldNameField,
  errorCode: errorCodes.ServerValidationError.ErrorCodeField,
} as const;

export const generalConstants = {
  validationErrorStatusCode: constants.ProblemDetails.ValidationErrorStatusCode,
  errorsCollectionName: constants.ProblemDetails.ErrorsCollectionName,
  rootErrorName: "root",
} as const;
