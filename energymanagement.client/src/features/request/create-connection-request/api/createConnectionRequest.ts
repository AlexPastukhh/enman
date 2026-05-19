import type { CreateConnectionRequestRequest } from "../../../../entities/request/api/requestApiTypes";
import { fetchJson } from "../../../../shared/api/fetchJson";

export const createConnectionRequest = (
  request: CreateConnectionRequestRequest,
): Promise<void> =>
  fetchJson<void>("/api/requests", {
    method: "POST",
    body: JSON.stringify(request),
  });
