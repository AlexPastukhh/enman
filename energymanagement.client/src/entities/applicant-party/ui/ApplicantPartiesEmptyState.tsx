import { applicantPartiesListConst } from "./applicantPartiesListConst";

export const ApplicantPartiesEmptyState = () => (
  <section
    className="applicantPartiesEmpty"
    aria-labelledby="applicant-parties-empty-heading"
  >
    <h2 id="applicant-parties-empty-heading">
      {applicantPartiesListConst.emptyTitle}
    </h2>
    <p>{applicantPartiesListConst.emptyDescription}</p>
  </section>
);
