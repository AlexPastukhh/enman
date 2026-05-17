import constantsJson from "../../../../Shared/constants.json" with { type: "json" };
import errorCodesJson from "../../../../Shared/errorcodes.json" with { type: "json" };

export const constants = constantsJson;
export const errorCodes = errorCodesJson;

export const authFieldNames = {
  register: {
    email: constants.L1AuthConstants.RegisterClientAccount.Email.FieldName,
    password: constants.L1AuthConstants.RegisterClientAccount.Password.FieldName,
    passwordConfirmation: "passwordConfirmation",
  },
  login: {
    email: constants.L1AuthConstants.Login.Email.FieldName,
    password: constants.L1AuthConstants.Login.Password.FieldName,
  },
} as const;

export const applicantPartyFieldNames = {
  createIndividual: {
    firstName:
      constants.L1ApplicantPartyConstants.CreateIndividualApplicantParty.FirstName
        .FieldName,
    middleName:
      constants.L1ApplicantPartyConstants.CreateIndividualApplicantParty.MiddleName
        .FieldName,
    lastName:
      constants.L1ApplicantPartyConstants.CreateIndividualApplicantParty.LastName
        .FieldName,
    email:
      constants.L1ApplicantPartyConstants.CreateIndividualApplicantParty.Email
        .FieldName,
    phoneNumber:
      constants.L1ApplicantPartyConstants.CreateIndividualApplicantParty
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
