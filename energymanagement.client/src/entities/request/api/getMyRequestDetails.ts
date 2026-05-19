import { fetchJson } from "../../../shared/api/fetchJson";
import type { MyRequestDetailsDto } from "./requestApiTypes";

export const getMyRequestDetails = (
  requestId: number,
): Promise<MyRequestDetailsDto> =>
  fetchJson<MyRequestDetailsDto>(
    `/api/requests/${encodeURIComponent(String(requestId))}`,
    {
      method: "GET",
    },
  );
