import { fetchJson } from "../../../shared/api/fetchJson";
import type { L1MyRequestDetails } from "./requestApiTypes";

export const getMyRequestDetails = (
  requestId: number,
): Promise<L1MyRequestDetails> =>
  fetchJson<L1MyRequestDetails>(
    `/api/requests/${encodeURIComponent(String(requestId))}`,
    {
      method: "GET",
    },
  );
