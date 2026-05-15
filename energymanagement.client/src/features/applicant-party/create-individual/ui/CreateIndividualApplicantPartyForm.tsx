import { FormButton } from "../../../../shared/ui/form/FormButton";
import { FormField } from "../../../../shared/ui/form/FormField";
import { FormGroup } from "../../../../shared/ui/form/FormGroup";
import { FormTitle } from "../../../../shared/ui/form/FormTitle";
import { useCreateIndividualApplicantPartyForm } from "../model/useCreateIndividualApplicantPartyForm";
import { ApplicantPartyReadOnlyView } from "./ApplicantPartyReadOnlyView";
import { createIndividualApplicantPartyConst } from "./createIndividualApplicantPartyConst";
import "./createIndividualApplicantParty.css";

export const CreateIndividualApplicantPartyForm = () => {
  const {
    register,
    handleSubmit,
    errors,
    isSubmitting,
    fieldNames,
    savedApplicantParty,
    isSuccessNotificationVisible,
    startEditingSavedApplicantParty,
  } = useCreateIndividualApplicantPartyForm();

  if (savedApplicantParty) {
    return (
      <section
        className="applicantPartySection"
        aria-labelledby="applicant-party-heading"
      >
        <FormTitle id="applicant-party-heading">
          {createIndividualApplicantPartyConst.readOnlyTitle}
        </FormTitle>
        {isSuccessNotificationVisible && (
          <p className="formSuccessNotification" role="status">
            {createIndividualApplicantPartyConst.successMessage}
          </p>
        )}
        <ApplicantPartyReadOnlyView
          values={savedApplicantParty}
          onEdit={startEditingSavedApplicantParty}
        />
      </section>
    );
  }

  return (
    <form
      className="form applicantPartyForm"
      onSubmit={handleSubmit}
      aria-labelledby="applicant-party-heading"
    >
      <FormTitle id="applicant-party-heading">
        {createIndividualApplicantPartyConst.formTitle}
      </FormTitle>

      {errors.root?.message && (
        <p className="formRootError" role="alert">
          {errors.root.message}
        </p>
      )}

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.firstName}
          inputId={createIndividualApplicantPartyConst.firstNameInputId}
          errorId={createIndividualApplicantPartyConst.firstNameErrorsId}
          labelText={createIndividualApplicantPartyConst.firstNameLabel}
          placeHolder={createIndividualApplicantPartyConst.firstNamePlaceholder}
          error={errors[fieldNames.firstName]}
        />
      </FormGroup>

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.middleName}
          inputId={createIndividualApplicantPartyConst.middleNameInputId}
          errorId={createIndividualApplicantPartyConst.middleNameErrorsId}
          labelText={createIndividualApplicantPartyConst.middleNameLabel}
          placeHolder={createIndividualApplicantPartyConst.middleNamePlaceholder}
          error={errors[fieldNames.middleName]}
        />
      </FormGroup>

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.lastName}
          inputId={createIndividualApplicantPartyConst.lastNameInputId}
          errorId={createIndividualApplicantPartyConst.lastNameErrorsId}
          labelText={createIndividualApplicantPartyConst.lastNameLabel}
          placeHolder={createIndividualApplicantPartyConst.lastNamePlaceholder}
          error={errors[fieldNames.lastName]}
        />
      </FormGroup>

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.email}
          inputId={createIndividualApplicantPartyConst.emailInputId}
          errorId={createIndividualApplicantPartyConst.emailErrorsId}
          labelText={createIndividualApplicantPartyConst.emailLabel}
          placeHolder={createIndividualApplicantPartyConst.emailPlaceholder}
          error={errors[fieldNames.email]}
        />
      </FormGroup>

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.phoneNumber}
          inputId={createIndividualApplicantPartyConst.phoneNumberInputId}
          errorId={createIndividualApplicantPartyConst.phoneNumberErrorsId}
          labelText={createIndividualApplicantPartyConst.phoneNumberLabel}
          placeHolder={createIndividualApplicantPartyConst.phoneNumberPlaceholder}
          error={errors[fieldNames.phoneNumber]}
        />
      </FormGroup>

      <FormGroup addClassName="formButtonGroup">
        <FormButton type="submit" disabled={isSubmitting}>
          {isSubmitting
            ? createIndividualApplicantPartyConst.submittingButtonText
            : createIndividualApplicantPartyConst.submitButtonText}
        </FormButton>
      </FormGroup>
    </form>
  );
};
