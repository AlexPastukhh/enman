import type { L1CreateConnectionRequestRequest } from "../../../../entities/request/api/requestApiTypes";
import { fetchJson } from "../../../../shared/api/fetchJson";

export const createConnectionRequest = (
  request: L1CreateConnectionRequestRequest,
): Promise<void> =>
  fetchJson<void>("/api/l1/requests", {
    method: "POST",
    body: JSON.stringify(request),
  });
