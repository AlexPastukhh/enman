export const buildAgreementProposalDocumentDownloadPath = (
  exchangeId: number,
  proposalId: number,
): string =>
  `/api/agreement-exchanges/${encodeURIComponent(
    String(exchangeId),
  )}/proposals/${encodeURIComponent(String(proposalId))}/document/download`;
