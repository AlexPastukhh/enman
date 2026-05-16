import { applicantPartiesListConst } from "./applicantPartiesListConst";

export const valueOrUnknown = (value?: string | null) =>
  value?.trim() ? value : applicantPartiesListConst.unknownValue;

export const formatApplicantPartyDate = (value?: string | null) => {
  if (!value) {
    return applicantPartiesListConst.unknownValue;
  }

  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return applicantPartiesListConst.unknownValue;
  }

  return new Intl.DateTimeFormat("ru-RU", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
};
