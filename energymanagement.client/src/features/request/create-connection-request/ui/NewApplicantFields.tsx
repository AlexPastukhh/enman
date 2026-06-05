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

const applicantTypeLabels = {
  Individual: "Физическое лицо",
  IndividualEntrepreneur: "ИП",
  LegalEntity: "Юридическое лицо",
} as const;

const requisiteLabels = {
  applicantPartyType: "Тип заявителя",
  organizationName: "Название организации",
  inn: "ИНН",
  kpp: "КПП",
  ogrn: "ОГРН",
  ogrnip: "ОГРНИП",
} as const;

export const NewApplicantFields = ({
  values,
  errors,
  onChange,
}: NewApplicantFieldsProps) => {
  const requiresFullName =
    values.applicantPartyType === "Individual" ||
    values.applicantPartyType === "IndividualEntrepreneur";
  const isIndividualEntrepreneur =
    values.applicantPartyType === "IndividualEntrepreneur";
  const isLegalEntity = values.applicantPartyType === "LegalEntity";

  return (
    <section
      className="createConnectionRequestForm__section"
      aria-labelledby="new-applicant-fields-heading"
    >
      <h3 id="new-applicant-fields-heading">
        {createConnectionRequestConst.newApplicantFieldsTitle}
      </h3>
      <div className="formGroup createConnectionRequestForm__field">
        <label htmlFor="create-request-applicantPartyType">
          {requisiteLabels.applicantPartyType}
        </label>
        <select
          className="formControl"
          id="create-request-applicantPartyType"
          value={values.applicantPartyType}
          onChange={(event) => onChange("applicantPartyType", event.target.value)}
        >
          <option value="Individual">{applicantTypeLabels.Individual}</option>
          <option value="IndividualEntrepreneur">
            {applicantTypeLabels.IndividualEntrepreneur}
          </option>
          <option value="LegalEntity">{applicantTypeLabels.LegalEntity}</option>
        </select>
      </div>
      {requiresFullName ? (
        <>
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
        </>
      ) : null}
      {isLegalEntity ? (
        <TextInputField
          fieldName="organizationName"
          label={requisiteLabels.organizationName}
          value={values.organizationName}
          error={errors.organizationName}
          onChange={onChange}
        />
      ) : null}
      {isIndividualEntrepreneur || isLegalEntity ? (
        <TextInputField
          fieldName="inn"
          label={requisiteLabels.inn}
          value={values.inn}
          error={errors.inn}
          onChange={onChange}
        />
      ) : null}
      {isIndividualEntrepreneur ? (
        <TextInputField
          fieldName="ogrnip"
          label={requisiteLabels.ogrnip}
          value={values.ogrnip}
          error={errors.ogrnip}
          onChange={onChange}
        />
      ) : null}
      {isLegalEntity ? (
        <>
          <TextInputField
            fieldName="kpp"
            label={requisiteLabels.kpp}
            value={values.kpp}
            error={errors.kpp}
            onChange={onChange}
          />
          <TextInputField
            fieldName="ogrn"
            label={requisiteLabels.ogrn}
            value={values.ogrn}
            error={errors.ogrn}
            onChange={onChange}
          />
        </>
      ) : null}
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
};
