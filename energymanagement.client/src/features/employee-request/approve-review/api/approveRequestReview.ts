import { fetchJson } from "../../../../shared/api/fetchJson";

const approveRequestReviewPath = (requestId: number | string) =>
  `/api/employee/requests/${encodeURIComponent(String(requestId))}/review/approve`;

export const approveRequestReview = (requestId: number): Promise<void> =>
  fetchJson<void>(approveRequestReviewPath(requestId), {
    method: "POST",
  });
