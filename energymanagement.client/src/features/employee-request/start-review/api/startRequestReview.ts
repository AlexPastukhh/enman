import { fetchJson } from "../../../../shared/api/fetchJson";

const startRequestReviewPath = (requestId: number | string) =>
  `/api/employee/requests/${encodeURIComponent(String(requestId))}/review/start`;

export const startRequestReview = (requestId: number): Promise<void> =>
  fetchJson<void>(startRequestReviewPath(requestId), {
    method: "POST",
  });
