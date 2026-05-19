import type { AgreementDocumentRef } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { agreementExchangeDetailsConst } from "./agreementExchangeDetailsConst";
import { formatDocumentRef } from "./formatAgreementExchangeDetails";

type AgreementDocumentRefListProps = {
  document?: AgreementDocumentRef | null;
};

export const AgreementDocumentRefList = ({
  document,
}: AgreementDocumentRefListProps) => {
  if (!document) {
    return null;
  }

  return (
    <div className="agreementExchangeDetails__document">
      <h4>{agreementExchangeDetailsConst.documentTitle}</h4>
      <p>{formatDocumentRef(document)}</p>
    </div>
  );
};
