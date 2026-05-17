import { fetchJson } from "../../../../shared/api/fetchJson";

const acceptAgreementProposalPath = (exchangeId: number | string) =>
  `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}/accept`;

export const acceptAgreementProposal = (exchangeId: number): Promise<void> =>
  fetchJson<void>(acceptAgreementProposalPath(exchangeId), {
    method: "POST",
  });
