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
    <h3 id="request-details-fields-heading">
      {"\u0418\u043d\u0444\u043e\u0440\u043c\u0430\u0446\u0438\u044f \u043e \u0437\u0430\u044f\u0432\u043a\u0435"}
    </h3>
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
