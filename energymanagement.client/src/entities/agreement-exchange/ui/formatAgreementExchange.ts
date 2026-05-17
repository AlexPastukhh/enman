import { agreementExchangeListConst } from "./agreementExchangeListConst";

export const formatAgreementExchangeDate = (value?: string | null) => {
  if (!value) {
    return agreementExchangeListConst.unknownValue;
  }

  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return agreementExchangeListConst.unknownValue;
  }

  return new Intl.DateTimeFormat("ru-RU", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
};

export const valueOrUnknown = (value?: string | number | null) => {
  if (typeof value === "number") {
    return Number.isFinite(value) ? String(value) : agreementExchangeListConst.unknownValue;
  }

  return value?.trim() ? value : agreementExchangeListConst.unknownValue;
};
