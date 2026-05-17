import type { ApplicantPartySummary } from "../../../../entities/applicant-party/model/applicantPartyTypes";
import type {
  ApplicantContextType,
  CreateConnectionRequestFormErrors,
  CreateConnectionRequestFormValues,
} from "../model/createConnectionRequestTypes";
import { ExistingApplicantSelector } from "./ExistingApplicantSelector";
import { createConnectionRequestConst } from "./createConnectionRequestConst";
import { NewApplicantFields } from "./NewApplicantFields";

type ApplicantContextSectionProps = {
  applicantParties: ApplicantPartySummary[];
  values: CreateConnectionRequestFormValues;
  errors: CreateConnectionRequestFormErrors;
  onContextTypeChange: (applicantContextType: ApplicantContextType) => void;
  onFieldChange: (fieldName: keyof CreateConnectionRequestFormValues, value: string) => void;
};

export const ApplicantContextSection = ({
  applicantParties,
  values,
  errors,
  onContextTypeChange,
  onFieldChange,
}: ApplicantContextSectionProps) => {
  const hasSavedApplicantParties = applicantParties.length > 0;

  return (
    <fieldset className="createConnectionRequestForm__section">
      <legend>{createConnectionRequestConst.applicantContextLegend}</legend>

      <label className="createConnectionRequestForm__radio">
        <input
          type="radio"
          name="applicantContextType"
          value="Existing"
          checked={values.applicantContextType === "Existing"}
          disabled={!hasSavedApplicantParties}
          onChange={() => onContextTypeChange("Existing")}
        />
        {createConnectionRequestConst.existingApplicantOptionLabel}
      </label>

      <label className="createConnectionRequestForm__radio">
        <input
          type="radio"
          name="applicantContextType"
          value="New"
          checked={values.applicantContextType === "New"}
          onChange={() => onContextTypeChange("New")}
        />
        {createConnectionRequestConst.newApplicantOptionLabel}
      </label>

      {values.applicantContextType === "Existing" && (
        <ExistingApplicantSelector
          applicantParties={applicantParties}
          selectedApplicantPartyId={values.existingApplicantPartyId}
          error={errors.existingApplicantPartyId}
          onChange={(applicantPartyId) =>
            onFieldChange("existingApplicantPartyId", applicantPartyId)
          }
        />
      )}

      {values.applicantContextType === "New" && (
        <NewApplicantFields
          values={values}
          errors={errors}
          onChange={onFieldChange}
        />
      )}
    </fieldset>
  );
};
