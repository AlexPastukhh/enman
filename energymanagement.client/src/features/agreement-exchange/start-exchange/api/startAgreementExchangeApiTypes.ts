export type StartAgreementExchangeDocumentRef = {
  storageKey: string;
  originalFileName: string;
  contentType: string;
  sizeBytes: number;
};

export type StartAgreementExchangeRequest = {
  document: StartAgreementExchangeDocumentRef;
  comment?: string | null;
};

export type StartAgreementExchangeInput = {
  requestId: number;
  proposal: StartAgreementExchangeRequest;
};
