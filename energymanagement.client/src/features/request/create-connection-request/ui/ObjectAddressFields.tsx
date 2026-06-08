import type {
  CreateConnectionRequestFormErrors,
  CreateConnectionRequestFormValues,
} from "../model/createConnectionRequestTypes";
import { createConnectionRequestConst } from "./createConnectionRequestConst";
import { TextInputField } from "./TextInputField";

type ObjectAddressFieldsProps = {
  values: CreateConnectionRequestFormValues;
  errors: CreateConnectionRequestFormErrors;
  onChange: (fieldName: keyof CreateConnectionRequestFormValues, value: string) => void;
};

export const ObjectAddressFields = ({
  values,
  errors,
  onChange,
}: ObjectAddressFieldsProps) => (
  <section
    className="createConnectionRequestForm__section"
    aria-labelledby="object-address-fields-heading"
  >
    <h3 id="object-address-fields-heading">
      {"\u0410\u0434\u0440\u0435\u0441 \u043e\u0431\u044a\u0435\u043a\u0442\u0430"}
    </h3>
    <TextInputField
      fieldName="postalCode"
      label={createConnectionRequestConst.postalCodeLabel}
      value={values.postalCode}
      error={errors.postalCode}
      onChange={onChange}
    />
    <TextInputField
      fieldName="region"
      label={createConnectionRequestConst.regionLabel}
      value={values.region}
      error={errors.region}
      onChange={onChange}
    />
    <TextInputField
      fieldName="city"
      label={createConnectionRequestConst.cityLabel}
      value={values.city}
      error={errors.city}
      onChange={onChange}
    />
    <TextInputField
      fieldName="street"
      label={createConnectionRequestConst.streetLabel}
      value={values.street}
      error={errors.street}
      onChange={onChange}
    />
    <TextInputField
      fieldName="house"
      label={createConnectionRequestConst.houseLabel}
      value={values.house}
      error={errors.house}
      onChange={onChange}
    />
    <TextInputField
      fieldName="building"
      label={createConnectionRequestConst.buildingLabel}
      value={values.building}
      error={errors.building}
      onChange={onChange}
    />
    <TextInputField
      fieldName="apartment"
      label={createConnectionRequestConst.apartmentLabel}
      value={values.apartment}
      error={errors.apartment}
      onChange={onChange}
    />
  </section>
);
