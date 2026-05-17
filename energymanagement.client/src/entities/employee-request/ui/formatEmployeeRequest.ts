import { employeeRequestDashboardConst } from "./employeeRequestDashboardConst";

export const valueOrUnknown = (value?: string | null) =>
  value?.trim() ? value : employeeRequestDashboardConst.unknownValue;

export const formatEmployeeRequestDate = (value?: string | null) => {
  if (!value) {
    return employeeRequestDashboardConst.unknownValue;
  }

  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return employeeRequestDashboardConst.unknownValue;
  }

  return new Intl.DateTimeFormat("en", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
};
