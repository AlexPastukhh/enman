import type { L1CreateConnectionRequestRequest } from "../../../../shared/api/l1RequestApi";
import { createConnectionRequestConst } from "../ui/createConnectionRequestConst";
import type {
  CreateConnectionRequestFormErrors,
  CreateConnectionRequestFormValues,
} from "./createConnectionRequestTypes";

const required = (label: string) => `${label}${createConnectionRequestConst.requiredSuffix}`;

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
    requireField(errors, values, "firstName", createConnectionRequestConst.firstNameLabel);
    requireField(errors, values, "middleName", createConnectionRequestConst.middleNameLabel);
    requireField(errors, values, "lastName", createConnectionRequestConst.lastNameLabel);
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
): L1CreateConnectionRequestRequest => {
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
  } satisfies Pick<L1CreateConnectionRequestRequest, "details" | "address">;

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
      fullName: {
        firstName: trimmed(values.firstName),
        middleName: trimmed(values.middleName),
        lastName: trimmed(values.lastName),
      },
      email: trimmed(values.email),
      phoneNumber: trimmed(values.phoneNumber),
    },
  };
};
