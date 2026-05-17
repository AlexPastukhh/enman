import type { ApplicantPartySummary } from "../../../../entities/applicant-party/model/applicantPartyTypes";
import { FormButton } from "../../../../shared/ui/form/FormButton";
import { FormTitle } from "../../../../shared/ui/form/FormTitle";
import { useCreateConnectionRequestForm } from "../model/useCreateConnectionRequestForm";
import { ApplicantContextSection } from "./ApplicantContextSection";
import { createConnectionRequestConst } from "./createConnectionRequestConst";
import { FormErrorMessage } from "./FormErrorMessage";
import { ObjectAddressFields } from "./ObjectAddressFields";
import { RequestDetailsFields } from "./RequestDetailsFields";
import "./createConnectionRequest.css";

type CreateConnectionRequestFormProps = {
  applicantParties: ApplicantPartySummary[];
  onSuccess?: () => void;
};

export const CreateConnectionRequestForm = ({
  applicantParties,
  onSuccess,
}: CreateConnectionRequestFormProps) => {
  const {
    values,
    errors,
    isSubmitting,
    setFieldValue,
    setApplicantContextType,
    handleSubmit,
  } = useCreateConnectionRequestForm({ applicantParties, onSuccess });

  return (
    <form
      className="form createConnectionRequestForm"
      aria-labelledby="create-connection-request-form-heading"
      onSubmit={handleSubmit}
    >
      <FormTitle id="create-connection-request-form-heading">
        {createConnectionRequestConst.formTitle}
      </FormTitle>

      {errors.root && (
        <FormErrorMessage id="create-request-root-error" message={errors.root} />
      )}

      <ApplicantContextSection
        applicantParties={applicantParties}
        values={values}
        errors={errors}
        onContextTypeChange={setApplicantContextType}
        onFieldChange={setFieldValue}
      />

      <RequestDetailsFields
        values={values}
        errors={errors}
        onChange={setFieldValue}
      />

      <ObjectAddressFields
        values={values}
        errors={errors}
        onChange={setFieldValue}
      />

      <div className="formGroup formButtonGroup">
        <FormButton type="submit" disabled={isSubmitting}>
          {isSubmitting
            ? createConnectionRequestConst.submittingButtonText
            : createConnectionRequestConst.submitButtonText}
        </FormButton>
      </div>
    </form>
  );
};
