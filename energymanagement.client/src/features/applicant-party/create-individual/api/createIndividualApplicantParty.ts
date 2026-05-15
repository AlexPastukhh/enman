import {
  createIndividualApplicantParty as postCreateIndividualApplicantParty,
  type L1CreateIndividualApplicantPartyRequest,
} from "../../../../shared/api/l1ApplicantPartyApi";
import {
  createIndividualApplicantPartyFieldNames,
  type CreateIndividualApplicantPartyFormValues,
} from "../model/createIndividualApplicantPartySchema";

export const createIndividualApplicantParty = (
  values: CreateIndividualApplicantPartyFormValues,
): Promise<void> => {
  const request: L1CreateIndividualApplicantPartyRequest = {
    fullName: {
      firstName: values[createIndividualApplicantPartyFieldNames.firstName],
      middleName: values[createIndividualApplicantPartyFieldNames.middleName],
      lastName: values[createIndividualApplicantPartyFieldNames.lastName],
    },
    email: values[createIndividualApplicantPartyFieldNames.email],
    phoneNumber: values[createIndividualApplicantPartyFieldNames.phoneNumber],
  };

  return postCreateIndividualApplicantParty(request).then(() => undefined);
};
