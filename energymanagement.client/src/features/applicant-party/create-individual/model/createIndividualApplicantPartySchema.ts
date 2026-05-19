import z from "zod";
import {
  applicantPartyFieldNames,
  errorCodes,
} from "../../../../shared/constants/generatedConstants";
import { getMessageFromErrorCode } from "../../../../shared/errors/clientErrorMessages";

export const createIndividualApplicantPartyFieldNames =
  applicantPartyFieldNames.createIndividual;

const requiredMessage = (fieldLabel: string) => `Укажите ${fieldLabel}.`;
const tooLongMessage = (fieldLabel: string) => `${fieldLabel} слишком длинное.`;

export const createIndividualApplicantPartySchema = z.object({
  [createIndividualApplicantPartyFieldNames.firstName]: z
    .string()
    .trim()
    .min(1, requiredMessage("имя"))
    .max(50, tooLongMessage("Имя")),
  [createIndividualApplicantPartyFieldNames.middleName]: z
    .string()
    .trim()
    .min(1, requiredMessage("отчество"))
    .max(50, tooLongMessage("Отчество")),
  [createIndividualApplicantPartyFieldNames.lastName]: z
    .string()
    .trim()
    .min(1, requiredMessage("фамилию"))
    .max(50, "Фамилия слишком длинная."),
  [createIndividualApplicantPartyFieldNames.email]: z
    .string()
    .trim()
    .min(1, getMessageFromErrorCode(errorCodes.Email.IsRequired))
    .regex(/^(.+)@(.+)$/, getMessageFromErrorCode(errorCodes.Email.IsInvalid)),
  [createIndividualApplicantPartyFieldNames.phoneNumber]: z
    .string()
    .trim()
    .min(1, getMessageFromErrorCode(errorCodes.Phone.IsRequired))
    .regex(
      /^(\+7|8|7)\d{10}$/,
      getMessageFromErrorCode(errorCodes.Phone.IsInvalid),
    ),
});

export type CreateIndividualApplicantPartyFormValues = z.infer<
  typeof createIndividualApplicantPartySchema
>;

export const createIndividualApplicantPartyServerFieldMap: Record<
  string,
  string
> = {
  FirstName: createIndividualApplicantPartyFieldNames.firstName,
  firstName: createIndividualApplicantPartyFieldNames.firstName,
  "FullName.FirstName": createIndividualApplicantPartyFieldNames.firstName,
  "fullName.firstName": createIndividualApplicantPartyFieldNames.firstName,
  MiddleName: createIndividualApplicantPartyFieldNames.middleName,
  middleName: createIndividualApplicantPartyFieldNames.middleName,
  "FullName.MiddleName": createIndividualApplicantPartyFieldNames.middleName,
  "fullName.middleName": createIndividualApplicantPartyFieldNames.middleName,
  LastName: createIndividualApplicantPartyFieldNames.lastName,
  lastName: createIndividualApplicantPartyFieldNames.lastName,
  "FullName.LastName": createIndividualApplicantPartyFieldNames.lastName,
  "fullName.lastName": createIndividualApplicantPartyFieldNames.lastName,
  Email: createIndividualApplicantPartyFieldNames.email,
  email: createIndividualApplicantPartyFieldNames.email,
  PhoneNumber: createIndividualApplicantPartyFieldNames.phoneNumber,
  phoneNumber: createIndividualApplicantPartyFieldNames.phoneNumber,
};
