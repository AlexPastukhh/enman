export type ApplicantContextType = "Existing" | "New";
export type ApplicantPartyType = "Individual" | "IndividualEntrepreneur" | "LegalEntity";

export type CreateConnectionRequestFormValues = {
  applicantContextType: ApplicantContextType;
  applicantPartyType: ApplicantPartyType;
  existingApplicantPartyId: string;
  details: string;
  postalCode: string;
  region: string;
  city: string;
  street: string;
  house: string;
  building: string;
  apartment: string;
  firstName: string;
  middleName: string;
  lastName: string;
  organizationName: string;
  inn: string;
  kpp: string;
  ogrn: string;
  ogrnip: string;
  email: string;
  phoneNumber: string;
};

export type CreateConnectionRequestFormErrors = Partial<
  Record<keyof CreateConnectionRequestFormValues | "root", string>
>;

export const createConnectionRequestFieldNames = {
  applicantContextType: "applicantContextType",
  applicantPartyType: "applicantPartyType",
  existingApplicantPartyId: "existingApplicantPartyId",
  details: "details",
  postalCode: "postalCode",
  region: "region",
  city: "city",
  street: "street",
  house: "house",
  building: "building",
  apartment: "apartment",
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
} as const satisfies Record<string, keyof CreateConnectionRequestFormValues>;

export const createConnectionRequestInitialValues = (
  existingApplicantPartyId = "",
): CreateConnectionRequestFormValues => ({
  applicantContextType: existingApplicantPartyId ? "Existing" : "New",
  applicantPartyType: "Individual",
  existingApplicantPartyId,
  details: "",
  postalCode: "",
  region: "",
  city: "",
  street: "",
  house: "",
  building: "",
  apartment: "",
  firstName: "",
  middleName: "",
  lastName: "",
  organizationName: "",
  inn: "",
  kpp: "",
  ogrn: "",
  ogrnip: "",
  email: "",
  phoneNumber: "",
});
