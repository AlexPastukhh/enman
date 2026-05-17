import { agreementExchangeListConst } from "./agreementExchangeListConst";

export type AgreementExchangeListEmptyStateVariant = "default" | "filtered";

type AgreementExchangeListEmptyStateProps = {
  variant?: AgreementExchangeListEmptyStateVariant;
  onResetFilters?: () => void;
};

export const AgreementExchangeListEmptyState = ({
  variant = "default",
  onResetFilters,
}: AgreementExchangeListEmptyStateProps) => {
  const isFiltered = variant === "filtered";

  return (
    <div className="agreementExchangeList__empty" role="status">
      <h2>
        {isFiltered
          ? agreementExchangeListConst.filteredEmptyTitle
          : agreementExchangeListConst.emptyTitle}
      </h2>
      <p>
        {isFiltered
          ? agreementExchangeListConst.filteredEmptyDescription
          : agreementExchangeListConst.emptyDescription}
      </p>
      {isFiltered && onResetFilters && (
        <button type="button" onClick={onResetFilters}>
          {agreementExchangeListConst.filteredEmptyResetText}
        </button>
      )}
    </div>
  );
};
