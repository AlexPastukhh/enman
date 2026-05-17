import type {
  AgreementExchangeListItem,
  AgreementExchangeViewerRole,
} from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { AgreementExchangeListEmptyState } from "./AgreementExchangeListEmptyState";
import { AgreementExchangeRow } from "./AgreementExchangeRow";
import { agreementExchangeListConst } from "./agreementExchangeListConst";
import "./agreementExchangeList.css";

type AgreementExchangeListProps = {
  exchanges: AgreementExchangeListItem[];
  viewerRole: AgreementExchangeViewerRole;
  emptyStateTitle?: string;
  emptyStateDescription?: string;
  getDetailsHref: (exchange: AgreementExchangeListItem) => string;
};

export const AgreementExchangeList = ({
  exchanges,
  viewerRole,
  emptyStateTitle = agreementExchangeListConst.defaultEmptyTitle,
  emptyStateDescription = agreementExchangeListConst.defaultEmptyDescription,
  getDetailsHref,
}: AgreementExchangeListProps) => {
  if (exchanges.length === 0) {
    return (
      <AgreementExchangeListEmptyState
        title={emptyStateTitle}
        description={emptyStateDescription}
      />
    );
  }

  return (
    <div
      className="agreementExchangeList"
      aria-label={agreementExchangeListConst.listLabel}
      data-viewer-role={viewerRole}
    >
      {exchanges.map((exchange) => (
        <AgreementExchangeRow
          key={exchange.exchangeId}
          exchange={exchange}
          detailsHref={getDetailsHref(exchange)}
        />
      ))}
    </div>
  );
};
