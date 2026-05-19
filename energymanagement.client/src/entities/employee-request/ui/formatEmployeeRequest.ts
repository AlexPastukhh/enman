import { employeeRequestDashboardConst } from "./employeeRequestDashboardConst";

export const valueOrUnknown = (value?: string | null) =>
  value?.trim() ? value : employeeRequestDashboardConst.unknownValue;

export const formatEmployeeRequestStatus = (status?: string | null) =>
  status
    ? employeeRequestDashboardConst.statusLabels[
        status as keyof typeof employeeRequestDashboardConst.statusLabels
      ] ?? status
    : employeeRequestDashboardConst.unknownValue;

export const formatEmployeeRequestType = (requestType?: string | null) =>
  requestType
    ? employeeRequestDashboardConst.requestTypeLabels[
        requestType as keyof typeof employeeRequestDashboardConst.requestTypeLabels
      ] ?? requestType
    : employeeRequestDashboardConst.unknownValue;

export const formatEmployeeRequestDate = (value?: string | null) => {
  if (!value) {
    return employeeRequestDashboardConst.unknownValue;
  }

  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return employeeRequestDashboardConst.unknownValue;
  }

  return new Intl.DateTimeFormat("ru-RU", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
};
