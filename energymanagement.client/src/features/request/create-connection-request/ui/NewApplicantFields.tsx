import type {
  CreateConnectionRequestFormErrors,
  CreateConnectionRequestFormValues,
} from "../model/createConnectionRequestTypes";
import { createConnectionRequestConst } from "./createConnectionRequestConst";
import { TextInputField } from "./TextInputField";

type NewApplicantFieldsProps = {
  values: CreateConnectionRequestFormValues;
  errors: CreateConnectionRequestFormErrors;
  onChange: (fieldName: keyof CreateConnectionRequestFormValues, value: string) => void;
};

export const NewApplicantFields = ({
  values,
  errors,
  onChange,
}: NewApplicantFieldsProps) => (
  <section
    className="createConnectionRequestForm__section"
    aria-labelledby="new-applicant-fields-heading"
  >
    <h3 id="new-applicant-fields-heading">
      {createConnectionRequestConst.newApplicantFieldsTitle}
    </h3>
    <TextInputField
      fieldName="firstName"
      label={createConnectionRequestConst.firstNameLabel}
      value={values.firstName}
      error={errors.firstName}
      onChange={onChange}
    />
    <TextInputField
      fieldName="middleName"
      label={createConnectionRequestConst.middleNameLabel}
      value={values.middleName}
      error={errors.middleName}
      onChange={onChange}
    />
    <TextInputField
      fieldName="lastName"
      label={createConnectionRequestConst.lastNameLabel}
      value={values.lastName}
      error={errors.lastName}
      onChange={onChange}
    />
    <TextInputField
      fieldName="email"
      label={createConnectionRequestConst.emailLabel}
      value={values.email}
      error={errors.email}
      onChange={onChange}
    />
    <TextInputField
      fieldName="phoneNumber"
      label={createConnectionRequestConst.phoneNumberLabel}
      value={values.phoneNumber}
      error={errors.phoneNumber}
      onChange={onChange}
    />
  </section>
);
