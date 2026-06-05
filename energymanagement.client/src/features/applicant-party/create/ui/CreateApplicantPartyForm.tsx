import { FormButton } from "../../../../shared/ui/form/FormButton";
import { FormField } from "../../../../shared/ui/form/FormField";
import { FormFieldBase } from "../../../../shared/ui/form/FormFieldBase";
import { FormGroup } from "../../../../shared/ui/form/FormGroup";
import { FormTitle } from "../../../../shared/ui/form/FormTitle";
import { useCreateApplicantPartyForm } from "../model/useCreateApplicantPartyForm";
import { createApplicantPartyConst } from "./createApplicantPartyConst";
import "./createApplicantParty.css";

type CreateApplicantPartyFormProps = {
  onSuccess?: () => void;
};

export const CreateApplicantPartyForm = ({
  onSuccess,
}: CreateApplicantPartyFormProps) => {
  const {
    register,
    registerRaw,
    handleSubmit,
    errors,
    isSubmitting,
    applicantPartyType,
    fieldNames,
    isSuccessNotificationVisible,
  } = useCreateApplicantPartyForm({ onSuccess });

  const showFullName =
    applicantPartyType === "Individual" ||
    applicantPartyType === "IndividualEntrepreneur";
  const showIndividualEntrepreneurFields =
    applicantPartyType === "IndividualEntrepreneur";
  const showLegalEntityFields = applicantPartyType === "LegalEntity";

  return (
    <form
      className="form applicantPartyForm"
      onSubmit={handleSubmit}
      aria-labelledby="applicant-party-heading"
    >
      <FormTitle id="applicant-party-heading">
        {createApplicantPartyConst.formTitle}
      </FormTitle>

      {isSuccessNotificationVisible && (
        <p className="formSuccessNotification" role="status">
          {createApplicantPartyConst.successMessage}
        </p>
      )}

      {errors.root?.message && (
        <p className="formRootError" role="alert">
          {errors.root.message}
        </p>
      )}

      <FormGroup>
        <FormFieldBase
          labelText={createApplicantPartyConst.applicantPartyTypeLabel}
          inputId={createApplicantPartyConst.applicantPartyTypeInputId}
          inputErrorId={createApplicantPartyConst.applicantPartyTypeErrorsId}
          error={errors[fieldNames.applicantPartyType]}
        >
          <select
            className="formControl applicantPartyForm__typeControl"
            id={createApplicantPartyConst.applicantPartyTypeInputId}
            aria-errormessage={
              errors[fieldNames.applicantPartyType]
                ? createApplicantPartyConst.applicantPartyTypeErrorsId
                : undefined
            }
            aria-invalid={
              errors[fieldNames.applicantPartyType] ? "true" : "false"
            }
            {...registerRaw(fieldNames.applicantPartyType)}
          >
            <option value="Individual">
              {createApplicantPartyConst.applicantPartyTypes.Individual}
            </option>
            <option value="IndividualEntrepreneur">
              {
                createApplicantPartyConst.applicantPartyTypes
                  .IndividualEntrepreneur
              }
            </option>
            <option value="LegalEntity">
              {createApplicantPartyConst.applicantPartyTypes.LegalEntity}
            </option>
          </select>
        </FormFieldBase>
      </FormGroup>

      {showFullName && (
        <>
          <FormGroup>
            <FormField
              registerFormFn={register}
              fieldName={fieldNames.firstName}
              inputId={createApplicantPartyConst.firstNameInputId}
              errorId={createApplicantPartyConst.firstNameErrorsId}
              labelText={createApplicantPartyConst.firstNameLabel}
              placeHolder={createApplicantPartyConst.firstNamePlaceholder}
              error={errors[fieldNames.firstName]}
            />
          </FormGroup>

          <FormGroup>
            <FormField
              registerFormFn={register}
              fieldName={fieldNames.middleName}
              inputId={createApplicantPartyConst.middleNameInputId}
              errorId={createApplicantPartyConst.middleNameErrorsId}
              labelText={createApplicantPartyConst.middleNameLabel}
              placeHolder={createApplicantPartyConst.middleNamePlaceholder}
              error={errors[fieldNames.middleName]}
            />
          </FormGroup>

          <FormGroup>
            <FormField
              registerFormFn={register}
              fieldName={fieldNames.lastName}
              inputId={createApplicantPartyConst.lastNameInputId}
              errorId={createApplicantPartyConst.lastNameErrorsId}
              labelText={createApplicantPartyConst.lastNameLabel}
              placeHolder={createApplicantPartyConst.lastNamePlaceholder}
              error={errors[fieldNames.lastName]}
            />
          </FormGroup>
        </>
      )}

      {showLegalEntityFields && (
        <FormGroup>
          <FormField
            registerFormFn={register}
            fieldName={fieldNames.organizationName}
            inputId={createApplicantPartyConst.organizationNameInputId}
            errorId={createApplicantPartyConst.organizationNameErrorsId}
            labelText={createApplicantPartyConst.organizationNameLabel}
            placeHolder={createApplicantPartyConst.organizationNamePlaceholder}
            error={errors[fieldNames.organizationName]}
          />
        </FormGroup>
      )}

      {(showIndividualEntrepreneurFields || showLegalEntityFields) && (
        <FormGroup>
          <FormField
            registerFormFn={register}
            fieldName={fieldNames.inn}
            inputId={createApplicantPartyConst.innInputId}
            errorId={createApplicantPartyConst.innErrorsId}
            labelText={createApplicantPartyConst.innLabel}
            placeHolder={createApplicantPartyConst.innPlaceholder}
            error={errors[fieldNames.inn]}
          />
        </FormGroup>
      )}

      {showIndividualEntrepreneurFields && (
        <FormGroup>
          <FormField
            registerFormFn={register}
            fieldName={fieldNames.ogrnip}
            inputId={createApplicantPartyConst.ogrnipInputId}
            errorId={createApplicantPartyConst.ogrnipErrorsId}
            labelText={createApplicantPartyConst.ogrnipLabel}
            placeHolder={createApplicantPartyConst.ogrnipPlaceholder}
            error={errors[fieldNames.ogrnip]}
          />
        </FormGroup>
      )}

      {showLegalEntityFields && (
        <>
          <FormGroup>
            <FormField
              registerFormFn={register}
              fieldName={fieldNames.kpp}
              inputId={createApplicantPartyConst.kppInputId}
              errorId={createApplicantPartyConst.kppErrorsId}
              labelText={createApplicantPartyConst.kppLabel}
              placeHolder={createApplicantPartyConst.kppPlaceholder}
              error={errors[fieldNames.kpp]}
            />
          </FormGroup>

          <FormGroup>
            <FormField
              registerFormFn={register}
              fieldName={fieldNames.ogrn}
              inputId={createApplicantPartyConst.ogrnInputId}
              errorId={createApplicantPartyConst.ogrnErrorsId}
              labelText={createApplicantPartyConst.ogrnLabel}
              placeHolder={createApplicantPartyConst.ogrnPlaceholder}
              error={errors[fieldNames.ogrn]}
            />
          </FormGroup>
        </>
      )}

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.email}
          inputId={createApplicantPartyConst.emailInputId}
          errorId={createApplicantPartyConst.emailErrorsId}
          labelText={createApplicantPartyConst.emailLabel}
          placeHolder={createApplicantPartyConst.emailPlaceholder}
          error={errors[fieldNames.email]}
        />
      </FormGroup>

      <FormGroup>
        <FormField
          registerFormFn={register}
          fieldName={fieldNames.phoneNumber}
          inputId={createApplicantPartyConst.phoneNumberInputId}
          errorId={createApplicantPartyConst.phoneNumberErrorsId}
          labelText={createApplicantPartyConst.phoneNumberLabel}
          placeHolder={createApplicantPartyConst.phoneNumberPlaceholder}
          error={errors[fieldNames.phoneNumber]}
        />
      </FormGroup>

      <FormGroup addClassName="formButtonGroup">
        <FormButton type="submit" disabled={isSubmitting}>
          {isSubmitting
            ? createApplicantPartyConst.submittingButtonText
            : createApplicantPartyConst.submitButtonText}
        </FormButton>
      </FormGroup>
    </form>
  );
};
