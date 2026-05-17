import { fetchFormData } from "../../../../shared/api/fetchFormData";
import type {
  AgreementProposalDocumentRef,
  UploadAgreementProposalDocumentInput,
} from "./uploadAgreementProposalDocumentApiTypes";

export const uploadAgreementProposalDocument = ({
  document,
}: UploadAgreementProposalDocumentInput): Promise<AgreementProposalDocumentRef> => {
  const formData = new FormData();
  formData.append("document", document);

  return fetchFormData<AgreementProposalDocumentRef>(
    "/api/agreement-proposal-documents",
    formData,
  );
};
