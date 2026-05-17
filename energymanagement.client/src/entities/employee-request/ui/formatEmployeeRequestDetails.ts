import { employeeRequestDetailsConst } from "./employeeRequestDetailsConst";

export const valueOrUnknown = (value: string | null | undefined) =>
  value?.trim() ? value : employeeRequestDetailsConst.unknownValue;

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

  return date.toLocaleString();
};
