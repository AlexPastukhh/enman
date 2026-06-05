import type { CreateConnectionRequestRequest } from "../../../../entities/request/api/requestApiTypes";
import { createConnectionRequestConst } from "../ui/createConnectionRequestConst";
import type {
  CreateConnectionRequestFormErrors,
  CreateConnectionRequestFormValues,
} from "./createConnectionRequestTypes";

const required = (label: string) => `${label}${createConnectionRequestConst.requiredSuffix}`;

const applicantRequisiteLabels = {
  organizationName: "Название организации",
  inn: "ИНН",
  kpp: "КПП",
  ogrn: "ОГРН",
  ogrnip: "ОГРНИП",
} as const;

const trimmed = (value: string) => value.trim();

const optionalTrimmed = (value: string) => {
  const result = trimmed(value);
  return result ? result : null;
};

const requireField = (
  errors: CreateConnectionRequestFormErrors,
  values: CreateConnectionRequestFormValues,
  fieldName: keyof CreateConnectionRequestFormValues,
  label: string,
) => {
  if (!trimmed(values[fieldName])) {
    errors[fieldName] = required(label);
  }
};

const buildFullName = (values: CreateConnectionRequestFormValues) => ({
  firstName: trimmed(values.firstName),
  middleName: trimmed(values.middleName),
  lastName: trimmed(values.lastName),
});

export const validateCreateConnectionRequestValues = (
  values: CreateConnectionRequestFormValues,
): CreateConnectionRequestFormErrors => {
  const errors: CreateConnectionRequestFormErrors = {};

  requireField(errors, values, "details", createConnectionRequestConst.requestDetailsLabel);
  requireField(errors, values, "postalCode", createConnectionRequestConst.postalCodeLabel);
  requireField(errors, values, "region", createConnectionRequestConst.regionLabel);
  requireField(errors, values, "city", createConnectionRequestConst.cityLabel);
  requireField(errors, values, "street", createConnectionRequestConst.streetLabel);
  requireField(errors, values, "house", createConnectionRequestConst.houseLabel);

  if (values.applicantContextType === "Existing") {
    if (!trimmed(values.existingApplicantPartyId)) {
      errors.existingApplicantPartyId =
        createConnectionRequestConst.existingApplicantRequired;
    }
  }

  if (values.applicantContextType === "New") {
    if (
      values.applicantPartyType === "Individual" ||
      values.applicantPartyType === "IndividualEntrepreneur"
    ) {
      requireField(errors, values, "firstName", createConnectionRequestConst.firstNameLabel);
      requireField(errors, values, "middleName", createConnectionRequestConst.middleNameLabel);
      requireField(errors, values, "lastName", createConnectionRequestConst.lastNameLabel);
    }

    if (values.applicantPartyType === "IndividualEntrepreneur") {
      requireField(errors, values, "inn", applicantRequisiteLabels.inn);
      requireField(errors, values, "ogrnip", applicantRequisiteLabels.ogrnip);
    }

    if (values.applicantPartyType === "LegalEntity") {
      requireField(
        errors,
        values,
        "organizationName",
        applicantRequisiteLabels.organizationName,
      );
      requireField(errors, values, "inn", applicantRequisiteLabels.inn);
      requireField(errors, values, "kpp", applicantRequisiteLabels.kpp);
      requireField(errors, values, "ogrn", applicantRequisiteLabels.ogrn);
    }

    requireField(errors, values, "email", createConnectionRequestConst.emailLabel);
    requireField(errors, values, "phoneNumber", createConnectionRequestConst.phoneNumberLabel);

    if (trimmed(values.email) && !/^(.+)@(.+)$/.test(trimmed(values.email))) {
      errors.email = createConnectionRequestConst.invalidEmailMessage;
    }

    if (
      trimmed(values.phoneNumber) &&
      !/^(\+7|8|7)\d{10}$/.test(trimmed(values.phoneNumber))
    ) {
      errors.phoneNumber = createConnectionRequestConst.invalidPhoneMessage;
    }
  }

  return errors;
};

export const buildCreateConnectionRequestDto = (
  values: CreateConnectionRequestFormValues,
): CreateConnectionRequestRequest => {
  const baseRequest = {
    details: trimmed(values.details),
    address: {
      postalCode: trimmed(values.postalCode),
      region: trimmed(values.region),
      city: trimmed(values.city),
      street: trimmed(values.street),
      house: trimmed(values.house),
      building: optionalTrimmed(values.building),
      apartment: optionalTrimmed(values.apartment),
    },
  } satisfies Pick<CreateConnectionRequestRequest, "details" | "address">;

  if (values.applicantContextType === "Existing") {
    return {
      ...baseRequest,
      applicantContextType: "Existing",
      existingApplicantPartyId: Number(values.existingApplicantPartyId),
    };
  }

  return {
    ...baseRequest,
    applicantContextType: "New",
    existingApplicantPartyId: null,
    newApplicantParty: {
      applicantPartyType: values.applicantPartyType,
      fullName:
        values.applicantPartyType === "LegalEntity"
          ? undefined
          : buildFullName(values),
      organizationName:
        values.applicantPartyType === "LegalEntity"
          ? trimmed(values.organizationName)
          : null,
      inn:
        values.applicantPartyType === "Individual"
          ? null
          : trimmed(values.inn),
      kpp:
        values.applicantPartyType === "LegalEntity"
          ? trimmed(values.kpp)
          : null,
      ogrn:
        values.applicantPartyType === "LegalEntity"
          ? trimmed(values.ogrn)
          : null,
      ogrnip:
        values.applicantPartyType === "IndividualEntrepreneur"
          ? trimmed(values.ogrnip)
          : null,
      email: trimmed(values.email),
      phoneNumber: trimmed(values.phoneNumber),
    },
  };
};
