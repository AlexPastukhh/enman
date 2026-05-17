import type {
  CreateConnectionRequestFormErrors,
  CreateConnectionRequestFormValues,
} from "../model/createConnectionRequestTypes";
import { createConnectionRequestConst } from "./createConnectionRequestConst";
import { TextAreaField } from "./TextAreaField";

type RequestDetailsFieldsProps = {
  values: CreateConnectionRequestFormValues;
  errors: CreateConnectionRequestFormErrors;
  onChange: (fieldName: keyof CreateConnectionRequestFormValues, value: string) => void;
};

export const RequestDetailsFields = ({
  values,
  errors,
  onChange,
}: RequestDetailsFieldsProps) => (
  <section
    className="createConnectionRequestForm__section"
    aria-labelledby="request-details-fields-heading"
  >
    <h3 id="request-details-fields-heading">Request information</h3>
    <TextAreaField
      fieldName="details"
      label={createConnectionRequestConst.requestDetailsLabel}
      value={values.details}
      error={errors.details}
      onChange={onChange}
      placeholder={createConnectionRequestConst.requestDetailsPlaceholder}
    />
  </section>
);
