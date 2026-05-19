import {
  type CreateIndividualApplicantPartyRequest,
  type CreateIndividualApplicantPartyResponse,
} from "../../../../entities/applicant-party/api/applicantPartyApiTypes";
import { fetchJson } from "../../../../shared/api/fetchJson";
import {
  createIndividualApplicantPartyFieldNames,
  type CreateIndividualApplicantPartyFormValues,
} from "../model/createIndividualApplicantPartySchema";

const createIndividualApplicantPartyPath =
  "/api/applicant-parties/individual";

export const createIndividualApplicantParty = (
  values: CreateIndividualApplicantPartyFormValues,
): Promise<void> => {
  const request: CreateIndividualApplicantPartyRequest = {
    fullName: {
      firstName: values[createIndividualApplicantPartyFieldNames.firstName],
      middleName: values[createIndividualApplicantPartyFieldNames.middleName],
      lastName: values[createIndividualApplicantPartyFieldNames.lastName],
    },
    email: values[createIndividualApplicantPartyFieldNames.email],
    phoneNumber: values[createIndividualApplicantPartyFieldNames.phoneNumber],
  };

  return fetchJson<CreateIndividualApplicantPartyResponse>(
    createIndividualApplicantPartyPath,
    {
      method: "POST",
      body: JSON.stringify(request),
    },
  ).then(() => undefined);
};
