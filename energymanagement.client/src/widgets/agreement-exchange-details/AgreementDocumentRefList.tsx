import type { AgreementDocumentRef } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { agreementExchangeDetailsConst } from "./agreementExchangeDetailsConst";
import { buildAgreementProposalDocumentDownloadPath } from "./buildAgreementProposalDocumentDownloadPath";
import { formatDocumentRef } from "./formatAgreementExchangeDetails";

type AgreementDocumentRefListProps = {
  exchangeId: number;
  proposalId: number;
  document?: AgreementDocumentRef | null;
};

export const AgreementDocumentRefList = ({
  exchangeId,
  proposalId,
  document,
}: AgreementDocumentRefListProps) => {
  if (!document) {
    return null;
  }

  const downloadPath = buildAgreementProposalDocumentDownloadPath(
    exchangeId,
    proposalId,
  );
  const fileName = document.originalFileName ?? undefined;

  return (
    <div className="agreementExchangeDetails__document">
      <h4>{agreementExchangeDetailsConst.documentTitle}</h4>
      <p>{formatDocumentRef(document)}</p>
      <a
        className="agreementExchangeDetails__documentDownloadLink"
        href={downloadPath}
        download={fileName}
        aria-label={`${agreementExchangeDetailsConst.downloadDocumentLinkText}: ${
          fileName ?? agreementExchangeDetailsConst.unknownDocumentFileName
        }`}
      >
        {agreementExchangeDetailsConst.downloadDocumentLinkText}
      </a>
    </div>
  );
};
