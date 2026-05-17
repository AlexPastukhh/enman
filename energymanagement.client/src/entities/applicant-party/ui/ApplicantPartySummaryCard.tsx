import type { ReactNode } from "react";
import type { ApplicantPartySummary } from "../model/applicantPartyTypes";
import { applicantPartiesListConst } from "./applicantPartiesListConst";
import { formatApplicantPartyDate, valueOrUnknown } from "./formatApplicantParty";

type ApplicantPartySummaryCardProps = {
  applicantParty: ApplicantPartySummary;
  highlighted?: boolean;
  action?: ReactNode;
};

const getCardTitle = (applicantParty: ApplicantPartySummary) => {
  const id = applicantParty.applicantPartyId;
  const displayName = applicantParty.displayName?.trim();

  if (id && displayName) {
    return `${applicantPartiesListConst.applicantPartyTitlePrefix} #${id}: ${displayName}`;
  }

  if (id) {
    return `${applicantPartiesListConst.applicantPartyTitlePrefix} #${id}`;
  }

  return displayName ?? applicantPartiesListConst.applicantPartyTitlePrefix;
};

export const ApplicantPartySummaryCard = ({
  applicantParty,
  highlighted = false,
  action = null,
}: ApplicantPartySummaryCardProps) => {
  const title = getCardTitle(applicantParty);
  const titleId = `applicant-party-${applicantParty.applicantPartyId ?? "unknown"}-title`;

  return (
    <article
      className={
        highlighted
          ? "applicantPartyCard applicantPartyCard--currentDefault"
          : "applicantPartyCard"
      }
      aria-labelledby={titleId}
    >
      <header className="applicantPartyCard__header">
        <h3 id={titleId}>{title}</h3>
        {highlighted && (
          <span className="applicantPartyCard__badge">
            {applicantPartiesListConst.currentDefaultBadge}
          </span>
        )}
      </header>

      <dl className="applicantPartyCard__summary">
        <div className="applicantPartyCard__row">
          <dt>{applicantPartiesListConst.applicantPartyTypeLabel}</dt>
          <dd>{valueOrUnknown(applicantParty.applicantPartyType)}</dd>
        </div>
        <div className="applicantPartyCard__row">
          <dt>{applicantPartiesListConst.displayNameLabel}</dt>
          <dd>{valueOrUnknown(applicantParty.displayName)}</dd>
        </div>
        {applicantParty.fullName && (
          <>
            <div className="applicantPartyCard__row">
              <dt>{applicantPartiesListConst.firstNameLabel}</dt>
              <dd>{valueOrUnknown(applicantParty.fullName.firstName)}</dd>
            </div>
            <div className="applicantPartyCard__row">
              <dt>{applicantPartiesListConst.middleNameLabel}</dt>
              <dd>{valueOrUnknown(applicantParty.fullName.middleName)}</dd>
            </div>
            <div className="applicantPartyCard__row">
              <dt>{applicantPartiesListConst.lastNameLabel}</dt>
              <dd>{valueOrUnknown(applicantParty.fullName.lastName)}</dd>
            </div>
          </>
        )}
        <div className="applicantPartyCard__row">
          <dt>{applicantPartiesListConst.emailLabel}</dt>
          <dd>{valueOrUnknown(applicantParty.email)}</dd>
        </div>
        <div className="applicantPartyCard__row">
          <dt>{applicantPartiesListConst.phoneNumberLabel}</dt>
          <dd>{valueOrUnknown(applicantParty.phoneNumber)}</dd>
        </div>
        <div className="applicantPartyCard__row">
          <dt>{applicantPartiesListConst.verificationStatusLabel}</dt>
          <dd>{valueOrUnknown(applicantParty.verificationStatus)}</dd>
        </div>
        <div className="applicantPartyCard__row">
          <dt>{applicantPartiesListConst.createdAtLabel}</dt>
          <dd>{formatApplicantPartyDate(applicantParty.createdAt)}</dd>
        </div>
      </dl>
      {action && <div className="applicantPartyCard__actions">{action}</div>}
    </article>
  );
};
