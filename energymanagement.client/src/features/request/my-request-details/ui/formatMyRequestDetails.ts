import type { components } from "../../../../shared/api/generated/openapi-types";
import { myRequestDetailsConst } from "./myRequestDetailsConst";

export const formatMyRequestDate = (value?: string) => {
  if (!value) {
    return myRequestDetailsConst.unknownValue;
  }

  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return myRequestDetailsConst.unknownValue;
  }

  return new Intl.DateTimeFormat("ru-RU", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
};

export const valueOrUnknown = (value?: string | null) =>
  value?.trim() ? value : myRequestDetailsConst.unknownValue;

export const formatMyRequestAddress = (
  address?: components["schemas"]["AddressDto"],
) => {
  if (!address) {
    return myRequestDetailsConst.unknownValue;
  }

  const parts = [
    address.postalCode,
    address.region,
    address.city,
    address.street,
    address.house,
    address.building,
    address.apartment,
  ].filter((part): part is string => Boolean(part?.trim()));

  return parts.length > 0 ? parts.join(", ") : myRequestDetailsConst.unknownValue;
};
