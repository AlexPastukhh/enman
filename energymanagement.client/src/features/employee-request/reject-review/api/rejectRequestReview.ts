import { fetchJson } from "../../../../shared/api/fetchJson";

export type RejectRequestReviewInput = {
  requestId: number;
  feedback?: string | null;
};

const rejectRequestReviewPath = (requestId: number | string) =>
  `/api/employee/requests/${encodeURIComponent(String(requestId))}/review/reject`;

const toRejectReviewBody = (feedback?: string | null) =>
  JSON.stringify({ feedback: feedback ?? "" });

export const rejectRequestReview = ({
  requestId,
  feedback,
}: RejectRequestReviewInput): Promise<void> =>
  fetchJson<void>(rejectRequestReviewPath(requestId), {
    method: "POST",
    body: toRejectReviewBody(feedback),
  });
