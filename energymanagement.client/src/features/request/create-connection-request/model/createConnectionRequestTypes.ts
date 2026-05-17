export type ApplicantContextType = "Existing" | "New";

export type CreateConnectionRequestFormValues = {
  applicantContextType: ApplicantContextType;
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
  email: string;
  phoneNumber: string;
};

export type CreateConnectionRequestFormErrors = Partial<
  Record<keyof CreateConnectionRequestFormValues | "root", string>
>;

export const createConnectionRequestFieldNames = {
  applicantContextType: "applicantContextType",
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
  email: "email",
  phoneNumber: "phoneNumber",
} as const satisfies Record<string, keyof CreateConnectionRequestFormValues>;

export const createConnectionRequestInitialValues = (
  existingApplicantPartyId = "",
): CreateConnectionRequestFormValues => ({
  applicantContextType: existingApplicantPartyId ? "Existing" : "New",
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
  email: "",
  phoneNumber: "",
});
