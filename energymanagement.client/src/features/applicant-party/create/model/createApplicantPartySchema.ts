import z from "zod";
import { getMessageFromErrorCode } from "../../../../shared/errors/clientErrorMessages";
import { errorCodes } from "../../../../shared/constants/generatedConstants";

export const applicantPartyTypeValues = [
  "Individual",
  "IndividualEntrepreneur",
  "LegalEntity",
] as const;

export type ApplicantPartyTypeValue = (typeof applicantPartyTypeValues)[number];

export const createApplicantPartyFieldNames = {
  applicantPartyType: "applicantPartyType",
  firstName: "firstName",
  middleName: "middleName",
  lastName: "lastName",
  organizationName: "organizationName",
  inn: "inn",
  kpp: "kpp",
  ogrn: "ogrn",
  ogrnip: "ogrnip",
  email: "email",
  phoneNumber: "phoneNumber",
} as const;

const requiredMessage = (fieldLabel: string) => `Укажите ${fieldLabel}.`;
const tooLongMessage = (fieldLabel: string) => `${fieldLabel} слишком длинное.`;

const requiredText = (fieldLabel: string, maxLength: number) =>
  z
    .string()
    .trim()
    .min(1, requiredMessage(fieldLabel))
    .max(maxLength, tooLongMessage(fieldLabel));

const optionalText = z.string().trim().optional();

const innSchema = z
  .string()
  .trim()
  .regex(/^(\d{10}|\d{12})$/, "ИНН должен содержать 10 или 12 цифр.");

const kppSchema = z
  .string()
  .trim()
  .regex(/^\d{9}$/, "КПП должен содержать 9 цифр.");

const ogrnSchema = z
  .string()
  .trim()
  .regex(/^\d{13}$/, "ОГРН должен содержать 13 цифр.");

const ogrnipSchema = z
  .string()
  .trim()
  .regex(/^\d{15}$/, "ОГРНИП должен содержать 15 цифр.");

export const createApplicantPartySchema = z
  .object({
    [createApplicantPartyFieldNames.applicantPartyType]: z.enum(
      applicantPartyTypeValues,
    ),
    [createApplicantPartyFieldNames.firstName]: optionalText,
    [createApplicantPartyFieldNames.middleName]: optionalText,
    [createApplicantPartyFieldNames.lastName]: optionalText,
    [createApplicantPartyFieldNames.organizationName]: optionalText,
    [createApplicantPartyFieldNames.inn]: optionalText,
    [createApplicantPartyFieldNames.kpp]: optionalText,
    [createApplicantPartyFieldNames.ogrn]: optionalText,
    [createApplicantPartyFieldNames.ogrnip]: optionalText,
    [createApplicantPartyFieldNames.email]: z
      .string()
      .trim()
      .min(1, getMessageFromErrorCode(errorCodes.Email.IsRequired))
      .max(100, getMessageFromErrorCode(errorCodes.Email.IsInvalid))
      .regex(
        /^(.+)@(.+)$/,
        getMessageFromErrorCode(errorCodes.Email.IsInvalid),
      ),
    [createApplicantPartyFieldNames.phoneNumber]: z
      .string()
      .trim()
      .min(1, getMessageFromErrorCode(errorCodes.Phone.IsRequired))
      .regex(
        /^(\+7|8|7)\d{10}$/,
        getMessageFromErrorCode(errorCodes.Phone.IsInvalid),
      ),
  })
  .superRefine((values, context) => {
    const applicantPartyType =
      values[createApplicantPartyFieldNames.applicantPartyType];

    const validateField = (
      fieldName: keyof typeof createApplicantPartyFieldNames,
      schema: z.ZodType,
    ) => {
      const formFieldName = createApplicantPartyFieldNames[fieldName];
      const result = schema.safeParse(values[formFieldName]);
      if (!result.success) {
        result.error.issues.forEach((issue) => {
          context.addIssue({
            ...issue,
            path: [formFieldName],
          });
        });
      }
    };

    if (
      applicantPartyType === "Individual" ||
      applicantPartyType === "IndividualEntrepreneur"
    ) {
      validateField("firstName", requiredText("имя", 50));
      validateField("middleName", requiredText("отчество", 50));
      validateField("lastName", requiredText("фамилию", 50));
    }

    if (applicantPartyType === "IndividualEntrepreneur") {
      validateField("inn", innSchema);
      validateField("ogrnip", ogrnipSchema);
    }

    if (applicantPartyType === "LegalEntity") {
      validateField("organizationName", requiredText("наименование", 250));
      validateField("inn", innSchema);
      validateField("kpp", kppSchema);
      validateField("ogrn", ogrnSchema);
    }
  });

export type CreateApplicantPartyFormValues = z.infer<
  typeof createApplicantPartySchema
>;

export const createApplicantPartyServerFieldMap: Record<string, string> = {
  applicantPartyType: createApplicantPartyFieldNames.applicantPartyType,
  ApplicantPartyType: createApplicantPartyFieldNames.applicantPartyType,
  fullName: createApplicantPartyFieldNames.firstName,
  FullName: createApplicantPartyFieldNames.firstName,
  "fullName.firstName": createApplicantPartyFieldNames.firstName,
  "FullName.FirstName": createApplicantPartyFieldNames.firstName,
  firstName: createApplicantPartyFieldNames.firstName,
  FirstName: createApplicantPartyFieldNames.firstName,
  "fullName.middleName": createApplicantPartyFieldNames.middleName,
  "FullName.MiddleName": createApplicantPartyFieldNames.middleName,
  middleName: createApplicantPartyFieldNames.middleName,
  MiddleName: createApplicantPartyFieldNames.middleName,
  "fullName.lastName": createApplicantPartyFieldNames.lastName,
  "FullName.LastName": createApplicantPartyFieldNames.lastName,
  lastName: createApplicantPartyFieldNames.lastName,
  LastName: createApplicantPartyFieldNames.lastName,
  organizationName: createApplicantPartyFieldNames.organizationName,
  OrganizationName: createApplicantPartyFieldNames.organizationName,
  inn: createApplicantPartyFieldNames.inn,
  Inn: createApplicantPartyFieldNames.inn,
  kpp: createApplicantPartyFieldNames.kpp,
  Kpp: createApplicantPartyFieldNames.kpp,
  ogrn: createApplicantPartyFieldNames.ogrn,
  Ogrn: createApplicantPartyFieldNames.ogrn,
  ogrnip: createApplicantPartyFieldNames.ogrnip,
  Ogrnip: createApplicantPartyFieldNames.ogrnip,
  email: createApplicantPartyFieldNames.email,
  Email: createApplicantPartyFieldNames.email,
  phoneNumber: createApplicantPartyFieldNames.phoneNumber,
  PhoneNumber: createApplicantPartyFieldNames.phoneNumber,
};
