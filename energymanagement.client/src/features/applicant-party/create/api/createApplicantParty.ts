import type {
  CreateIndividualApplicantPartyRequest,
  CreateIndividualApplicantPartyResponse,
  CreateIndividualEntrepreneurApplicantPartyRequest,
  CreateLegalEntityApplicantPartyRequest,
} from "../../../../entities/applicant-party/api/applicantPartyApiTypes";
import { fetchJson } from "../../../../shared/api/fetchJson";
import {
  createApplicantPartyFieldNames,
  type CreateApplicantPartyFormValues,
} from "../model/createApplicantPartySchema";

const endpointByApplicantPartyType = {
  Individual: "/api/applicant-parties/individual",
  IndividualEntrepreneur: "/api/applicant-parties/individual-entrepreneur",
  LegalEntity: "/api/applicant-parties/legal-entity",
} as const;

export const createApplicantParty = (
  values: CreateApplicantPartyFormValues,
): Promise<void> => {
  const applicantPartyType =
    values[createApplicantPartyFieldNames.applicantPartyType];

  if (applicantPartyType === "LegalEntity") {
    const request: CreateLegalEntityApplicantPartyRequest = {
      organizationName: values[createApplicantPartyFieldNames.organizationName],
      inn: values[createApplicantPartyFieldNames.inn],
      kpp: values[createApplicantPartyFieldNames.kpp],
      ogrn: values[createApplicantPartyFieldNames.ogrn],
      email: values[createApplicantPartyFieldNames.email],
      phoneNumber: values[createApplicantPartyFieldNames.phoneNumber],
    };

    return fetchJson<CreateIndividualApplicantPartyResponse>(
      endpointByApplicantPartyType.LegalEntity,
      {
        method: "POST",
        body: JSON.stringify(request),
      },
    ).then(() => undefined);
  }

  const fullName = {
    firstName: values[createApplicantPartyFieldNames.firstName],
    middleName: values[createApplicantPartyFieldNames.middleName],
    lastName: values[createApplicantPartyFieldNames.lastName],
  };

  if (applicantPartyType === "IndividualEntrepreneur") {
    const request: CreateIndividualEntrepreneurApplicantPartyRequest = {
      fullName,
      inn: values[createApplicantPartyFieldNames.inn],
      ogrnip: values[createApplicantPartyFieldNames.ogrnip],
      email: values[createApplicantPartyFieldNames.email],
      phoneNumber: values[createApplicantPartyFieldNames.phoneNumber],
    };

    return fetchJson<CreateIndividualApplicantPartyResponse>(
      endpointByApplicantPartyType.IndividualEntrepreneur,
      {
        method: "POST",
        body: JSON.stringify(request),
      },
    ).then(() => undefined);
  }

  const request: CreateIndividualApplicantPartyRequest = {
    fullName,
    email: values[createApplicantPartyFieldNames.email],
    phoneNumber: values[createApplicantPartyFieldNames.phoneNumber],
  };

  return fetchJson<CreateIndividualApplicantPartyResponse>(
    endpointByApplicantPartyType.Individual,
    {
      method: "POST",
      body: JSON.stringify(request),
    },
  ).then(() => undefined);
};
