import { createConnectionRequestFieldNames } from "./createConnectionRequestTypes";

export const createConnectionRequestServerFieldMap: Record<string, string> = {
  ApplicantContextType: createConnectionRequestFieldNames.applicantContextType,
  applicantContextType: createConnectionRequestFieldNames.applicantContextType,
  ExistingApplicantPartyId:
    createConnectionRequestFieldNames.existingApplicantPartyId,
  existingApplicantPartyId:
    createConnectionRequestFieldNames.existingApplicantPartyId,
  NewApplicantParty: createConnectionRequestFieldNames.firstName,
  newApplicantParty: createConnectionRequestFieldNames.firstName,
  "NewApplicantParty.ApplicantPartyType":
    createConnectionRequestFieldNames.applicantPartyType,
  "newApplicantParty.applicantPartyType":
    createConnectionRequestFieldNames.applicantPartyType,
  "NewApplicantParty.FullName.FirstName":
    createConnectionRequestFieldNames.firstName,
  "newApplicantParty.fullName.firstName":
    createConnectionRequestFieldNames.firstName,
  "NewApplicantParty.FullName.MiddleName":
    createConnectionRequestFieldNames.middleName,
  "newApplicantParty.fullName.middleName":
    createConnectionRequestFieldNames.middleName,
  "NewApplicantParty.FullName.LastName":
    createConnectionRequestFieldNames.lastName,
  "newApplicantParty.fullName.lastName":
    createConnectionRequestFieldNames.lastName,
  "NewApplicantParty.Email": createConnectionRequestFieldNames.email,
  "newApplicantParty.email": createConnectionRequestFieldNames.email,
  "NewApplicantParty.PhoneNumber": createConnectionRequestFieldNames.phoneNumber,
  "newApplicantParty.phoneNumber": createConnectionRequestFieldNames.phoneNumber,
  "NewApplicantParty.OrganizationName":
    createConnectionRequestFieldNames.organizationName,
  "newApplicantParty.organizationName":
    createConnectionRequestFieldNames.organizationName,
  "NewApplicantParty.Inn": createConnectionRequestFieldNames.inn,
  "newApplicantParty.inn": createConnectionRequestFieldNames.inn,
  "NewApplicantParty.Kpp": createConnectionRequestFieldNames.kpp,
  "newApplicantParty.kpp": createConnectionRequestFieldNames.kpp,
  "NewApplicantParty.Ogrn": createConnectionRequestFieldNames.ogrn,
  "newApplicantParty.ogrn": createConnectionRequestFieldNames.ogrn,
  "NewApplicantParty.Ogrnip": createConnectionRequestFieldNames.ogrnip,
  "newApplicantParty.ogrnip": createConnectionRequestFieldNames.ogrnip,
  Details: createConnectionRequestFieldNames.details,
  details: createConnectionRequestFieldNames.details,
  Address: createConnectionRequestFieldNames.postalCode,
  address: createConnectionRequestFieldNames.postalCode,
  "Address.PostalCode": createConnectionRequestFieldNames.postalCode,
  "address.postalCode": createConnectionRequestFieldNames.postalCode,
  "Address.Region": createConnectionRequestFieldNames.region,
  "address.region": createConnectionRequestFieldNames.region,
  "Address.City": createConnectionRequestFieldNames.city,
  "address.city": createConnectionRequestFieldNames.city,
  "Address.Street": createConnectionRequestFieldNames.street,
  "address.street": createConnectionRequestFieldNames.street,
  "Address.House": createConnectionRequestFieldNames.house,
  "address.house": createConnectionRequestFieldNames.house,
  "Address.Building": createConnectionRequestFieldNames.building,
  "address.building": createConnectionRequestFieldNames.building,
  "Address.Apartment": createConnectionRequestFieldNames.apartment,
  "address.apartment": createConnectionRequestFieldNames.apartment,
};
