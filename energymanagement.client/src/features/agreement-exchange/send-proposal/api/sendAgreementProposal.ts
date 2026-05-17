import { fetchJson } from "../../../../shared/api/fetchJson";
import type { SendAgreementProposalRequest } from "./sendAgreementProposalApiTypes";

export type SendAgreementProposalInput = {
  exchangeId: number;
  proposal: SendAgreementProposalRequest;
};

const sendAgreementProposalPath = (exchangeId: number | string) =>
  `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}/proposals`;

export const sendAgreementProposal = ({
  exchangeId,
  proposal,
}: SendAgreementProposalInput): Promise<void> =>
  fetchJson<void>(sendAgreementProposalPath(exchangeId), {
    method: "POST",
    body: JSON.stringify(proposal),
  });
