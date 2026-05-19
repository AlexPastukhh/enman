import type { IndividualApplicantParty } from "../../../../entities/applicant-party/model/applicantPartyTypes";
import { createIndividualApplicantPartyConst } from "./createIndividualApplicantPartyConst";

type ApplicantPartyReadOnlyViewProps = {
  applicantParty: IndividualApplicantParty;
  showSuccessNotification?: boolean;
};

const formatVerificationStatus = (status?: string | null) =>
  status
    ? createIndividualApplicantPartyConst.verificationStatusLabels[
        status as keyof typeof createIndividualApplicantPartyConst.verificationStatusLabels
      ] ?? status
    : "—";

export const ApplicantPartyReadOnlyView = ({
  applicantParty,
  showSuccessNotification = false,
}: ApplicantPartyReadOnlyViewProps) => {
  return (
    <section
      className="applicantPartySection"
      aria-labelledby="applicant-party-heading"
    >
      <h2 id="applicant-party-heading">
        {createIndividualApplicantPartyConst.readOnlyTitle}
      </h2>
      {showSuccessNotification && (
        <p className="formSuccessNotification" role="status">
          {createIndividualApplicantPartyConst.successMessage}
        </p>
      )}
      <dl className="applicantPartySummary">
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.firstNameLabel}</dt>
          <dd>{applicantParty.fullName?.firstName}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.middleNameLabel}</dt>
          <dd>{applicantParty.fullName?.middleName}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.lastNameLabel}</dt>
          <dd>{applicantParty.fullName?.lastName}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.emailLabel}</dt>
          <dd>{applicantParty.email}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.phoneNumberLabel}</dt>
          <dd>{applicantParty.phoneNumber}</dd>
        </div>
        <div className="applicantPartySummary__row">
          <dt>{createIndividualApplicantPartyConst.verificationStatusLabel}</dt>
          <dd>{formatVerificationStatus(applicantParty.verificationStatus)}</dd>
        </div>
      </dl>
    </section>
  );
};
