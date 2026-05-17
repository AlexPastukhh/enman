import type { AgreementExchangeListItem } from "../model/agreementExchangeTypes";
import {
  AgreementExchangeListEmptyState,
  type AgreementExchangeListEmptyStateVariant,
} from "./AgreementExchangeListEmptyState";
import { AgreementExchangeListRow } from "./AgreementExchangeListRow";

type AgreementExchangeListProps = {
  exchanges: AgreementExchangeListItem[];
  emptyStateVariant?: AgreementExchangeListEmptyStateVariant;
  onResetFilters?: () => void;
};

export const AgreementExchangeList = ({
  exchanges,
  emptyStateVariant = "default",
  onResetFilters,
}: AgreementExchangeListProps) => {
  if (exchanges.length === 0) {
    return (
      <AgreementExchangeListEmptyState
        variant={emptyStateVariant}
        onResetFilters={onResetFilters}
      />
    );
  }

  return (
    <div className="agreementExchangeList" aria-label="Список договорных обменов">
      {exchanges.map((exchange) => (
        <AgreementExchangeListRow key={exchange.exchangeId} exchange={exchange} />
      ))}
    </div>
  );
};
