export type AgreementProposalDocumentRef = {
  storageKey: string;
  originalFileName: string;
  contentType: string;
  sizeBytes: number;
};

export type UploadAgreementProposalDocumentInput = {
  document: File;
};
