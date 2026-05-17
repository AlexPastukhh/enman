export type SendAgreementProposalDocumentRef = {
  storageKey: string;
  originalFileName: string;
  contentType: string;
  sizeBytes: number;
};

export type SendAgreementProposalRequest = {
  document: SendAgreementProposalDocumentRef;
  comment?: string | null;
};
