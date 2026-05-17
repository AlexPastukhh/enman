export type StartAgreementExchangeDocumentRef = {
  storageKey: string;
  originalFileName: string;
  contentType: string;
  sizeBytes: number;
};

export type StartAgreementExchangeInitialProposal = {
  document: StartAgreementExchangeDocumentRef;
  comment?: string | null;
};

export type StartAgreementExchangeRequest = {
  requestId: number;
  initialProposal: StartAgreementExchangeInitialProposal;
};

export type StartAgreementExchangeResponse = {
  exchangeId?: number | null;
};
