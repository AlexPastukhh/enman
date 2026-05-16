import { groupAccountApplicantParties } from "../model/groupAccountApplicantParties";
import type { ApplicantPartySummary } from "../model/applicantPartyTypes";
import { ApplicantPartiesEmptyState } from "./ApplicantPartiesEmptyState";
import { applicantPartiesListConst } from "./applicantPartiesListConst";
import { ApplicantPartySummaryCard } from "./ApplicantPartySummaryCard";
import "./applicantPartiesList.css";

type ApplicantPartiesListProps = {
  applicantParties: ApplicantPartySummary[];
};

export const ApplicantPartiesList = ({
  applicantParties,
}: ApplicantPartiesListProps) => {
  const { currentDefaults, otherSaved } =
    groupAccountApplicantParties(applicantParties);

  if (applicantParties.length === 0) {
    return <ApplicantPartiesEmptyState />;
  }

  return (
    <section
      className="applicantPartiesList"
      aria-labelledby="applicant-parties-heading"
    >
      <h2 id="applicant-parties-heading">{applicantPartiesListConst.title}</h2>

      {currentDefaults.length > 0 && (
        <section
          className="applicantPartiesList__section"
          aria-labelledby="current-default-applicant-parties-heading"
        >
          <h3 id="current-default-applicant-parties-heading">
            {applicantPartiesListConst.currentDefaultsTitle}
          </h3>
          <div className="applicantPartiesList__cards">
            {currentDefaults.map((applicantParty, index) => (
              <ApplicantPartySummaryCard
                key={applicantParty.applicantPartyId ?? `current-${index}`}
                applicantParty={applicantParty}
                highlighted
              />
            ))}
          </div>
        </section>
      )}

      <section
        className="applicantPartiesList__section"
        aria-labelledby="other-saved-applicant-parties-heading"
      >
        <h3 id="other-saved-applicant-parties-heading">
          {applicantPartiesListConst.otherSavedTitle}
        </h3>
        {otherSaved.length === 0 ? (
          <p>{applicantPartiesListConst.emptyDescription}</p>
        ) : (
          <div className="applicantPartiesList__cards">
            {otherSaved.map((applicantParty, index) => (
              <ApplicantPartySummaryCard
                key={applicantParty.applicantPartyId ?? `saved-${index}`}
                applicantParty={applicantParty}
              />
            ))}
          </div>
        )}
      </section>
    </section>
  );
};
