import { fetchJson } from "../../../../shared/api/fetchJson";

export type RejectRequestReviewInput = {
  requestId: number;
  feedback: string;
};

const rejectRequestReviewPath = (requestId: number | string) =>
  `/api/employee/requests/${encodeURIComponent(String(requestId))}/review/reject`;

export const rejectRequestReview = ({
  requestId,
  feedback,
}: RejectRequestReviewInput): Promise<void> =>
  fetchJson<void>(rejectRequestReviewPath(requestId), {
    method: "POST",
    body: JSON.stringify({ feedback }),
  });
