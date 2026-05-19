import { employeeRequestDetailsConst } from "./employeeRequestDetailsConst";

export const valueOrUnknown = (value: string | null | undefined) =>
  value?.trim() ? value : employeeRequestDetailsConst.unknownValue;

export const formatEmployeeRequestDetailsStatus = (
  status: string | null | undefined,
) =>
  status
    ? employeeRequestDetailsConst.statusLabels[
        status as keyof typeof employeeRequestDetailsConst.statusLabels
      ] ?? status
    : employeeRequestDetailsConst.unknownValue;

export const formatEmployeeRequestDetailsType = (
  requestType: string | null | undefined,
) =>
  requestType
    ? employeeRequestDetailsConst.requestTypeLabels[
        requestType as keyof typeof employeeRequestDetailsConst.requestTypeLabels
      ] ?? requestType
    : employeeRequestDetailsConst.unknownValue;

export const formatEmployeeApplicantType = (
  applicantType: string | null | undefined,
) =>
  applicantType
    ? employeeRequestDetailsConst.applicantTypeLabels[
        applicantType as keyof typeof employeeRequestDetailsConst.applicantTypeLabels
      ] ?? applicantType
    : employeeRequestDetailsConst.unknownValue;

export const formatEmployeeRequestDetailsDate = (
  value: string | null | undefined,
) => {
  if (!value) {
    return employeeRequestDetailsConst.unknownValue;
  }

  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return value;
  }

  return new Intl.DateTimeFormat("ru-RU", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
};
