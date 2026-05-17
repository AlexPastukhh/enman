import type { ApplicantPartySummary } from "../../../../entities/applicant-party/model/applicantPartyTypes";
import { valueOrUnknown } from "../../../../entities/applicant-party/ui/formatApplicantParty";
import { createConnectionRequestConst } from "./createConnectionRequestConst";
import { FormErrorMessage } from "./FormErrorMessage";

type ExistingApplicantSelectorProps = {
  applicantParties: ApplicantPartySummary[];
  selectedApplicantPartyId: string;
  error?: string;
  onChange: (applicantPartyId: string) => void;
};

const getApplicantPartyOptionLabel = (applicantParty: ApplicantPartySummary) => {
  const marker = applicantParty.isCurrentDefault
    ? ` (${createConnectionRequestConst.currentDefaultMarker})`
    : "";
  return `${valueOrUnknown(applicantParty.displayName)} — ${valueOrUnknown(
    applicantParty.applicantPartyType,
  )}${marker}`;
};

export const ExistingApplicantSelector = ({
  applicantParties,
  selectedApplicantPartyId,
  error,
  onChange,
}: ExistingApplicantSelectorProps) => {
  const selectId = "create-request-existing-applicant-party-id";
  const errorId = `${selectId}-errors`;

  if (applicantParties.length === 0) {
    return <p>{createConnectionRequestConst.noSavedApplicantPartiesText}</p>;
  }

  return (
    <div className="formGroup createConnectionRequestForm__field">
      <label htmlFor={selectId}>
        {createConnectionRequestConst.savedApplicantSelectLabel}
      </label>
      <select
        className="formControl"
        id={selectId}
        value={selectedApplicantPartyId}
        aria-invalid={error ? "true" : "false"}
        aria-errormessage={error ? errorId : undefined}
        onChange={(event) => onChange(event.target.value)}
      >
        <option value="">
          {createConnectionRequestConst.savedApplicantSelectPlaceholder}
        </option>
        {applicantParties.map((applicantParty) => (
          <option
            key={applicantParty.applicantPartyId}
            value={String(applicantParty.applicantPartyId ?? "")}
          >
            {getApplicantPartyOptionLabel(applicantParty)}
          </option>
        ))}
      </select>
      <FormErrorMessage id={errorId} message={error} />
    </div>
  );
};
