import { fetchJson } from "../../../../shared/api/fetchJson";
import type { RunApplicantPartyVerificationResponse } from "./applicantVerificationApiTypes";

const runApplicantPartyVerificationPath = (requestId: number | string) =>
  `/api/employee/requests/${encodeURIComponent(String(requestId))}/applicant-party/verification/run`;

export const runApplicantPartyVerification = (
  requestId: number,
): Promise<RunApplicantPartyVerificationResponse> =>
  fetchJson<RunApplicantPartyVerificationResponse>(
    runApplicantPartyVerificationPath(requestId),
    { method: "POST" },
  );
