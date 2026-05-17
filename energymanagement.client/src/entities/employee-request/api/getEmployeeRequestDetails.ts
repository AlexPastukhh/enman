import { fetchJson } from "../../../shared/api/fetchJson";
import type { EmployeeRequestDetailsDto } from "./employeeRequestApiTypes";

export const getEmployeeRequestDetails = (
  requestId: number,
): Promise<EmployeeRequestDetailsDto> =>
  fetchJson<EmployeeRequestDetailsDto>(
    `/api/employee/requests/${encodeURIComponent(String(requestId))}`,
    {
      method: "GET",
    },
  );
