import { fetchJson } from "../../../../shared/api/fetchJson";
import type { SendAgreementProposalRequest } from "./sendAgreementProposalApiTypes";

export type SendAgreementProposalInput = {
  exchangeId: number;
  requestId: number;
  proposal: SendAgreementProposalRequest;
};

const sendAgreementProposalPath = (requestId: number | string) =>
  `/api/requests/${encodeURIComponent(
    String(requestId),
  )}/agreement-exchange/proposals`;

export const sendAgreementProposal = ({
  requestId,
  proposal,
}: SendAgreementProposalInput): Promise<void> =>
  fetchJson<void>(sendAgreementProposalPath(requestId), {
    method: "POST",
    body: JSON.stringify(proposal),
  });
