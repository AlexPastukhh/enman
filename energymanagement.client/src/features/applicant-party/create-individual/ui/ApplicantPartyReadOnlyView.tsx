import type { CreateIndividualApplicantPartyFormValues } from "../model/createIndividualApplicantPartySchema";
import { createIndividualApplicantPartyConst } from "./createIndividualApplicantPartyConst";
import { createIndividualApplicantPartyFieldNames } from "../model/createIndividualApplicantPartySchema";

type ApplicantPartyReadOnlyViewProps = {
  values: CreateIndividualApplicantPartyFormValues;
  onEdit: () => void;
};

export const ApplicantPartyReadOnlyView = ({
  values,
  onEdit,
}: ApplicantPartyReadOnlyViewProps) => {
  const fieldNames = createIndividualApplicantPartyFieldNames;

  return (
    <div className="applicantPartyReadOnlyView">
      <dl className="applicantPartySummary">
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.firstNameLabel}</dt>
          <dd>{values[fieldNames.firstName]}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.middleNameLabel}</dt>
          <dd>{values[fieldNames.middleName]}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.lastNameLabel}</dt>
          <dd>{values[fieldNames.lastName]}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.emailLabel}</dt>
          <dd>{values[fieldNames.email]}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.phoneNumberLabel}</dt>
          <dd>{values[fieldNames.phoneNumber]}</dd>
        </div>
      </dl>

      <button
        className="formButton applicantPartyEditButton"
        type="button"
        onClick={onEdit}
      >
        {createIndividualApplicantPartyConst.editButtonText}
      </button>
    </div>
  );
};
