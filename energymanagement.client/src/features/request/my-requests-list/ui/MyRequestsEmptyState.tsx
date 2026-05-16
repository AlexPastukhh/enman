import { myRequestsConst } from "./myRequestsConst";

export type MyRequestsEmptyStateVariant = "default" | "filtered";

type MyRequestsEmptyStateProps = {
  variant?: MyRequestsEmptyStateVariant;
  onResetFilters?: () => void;
};

export const MyRequestsEmptyState = ({
  variant = "default",
  onResetFilters,
}: MyRequestsEmptyStateProps) => {
  const isFiltered = variant === "filtered";

  return (
    <section className="myRequestsEmpty" aria-labelledby="my-requests-empty-title">
      <h2 id="my-requests-empty-title">
        {isFiltered ? myRequestsConst.filteredEmptyTitle : myRequestsConst.emptyTitle}
      </h2>
      <p>
        {isFiltered
          ? myRequestsConst.filteredEmptyDescription
          : myRequestsConst.emptyDescription}
      </p>
      {isFiltered && onResetFilters && (
        <button type="button" onClick={onResetFilters}>
          {myRequestsConst.filteredEmptyResetText}
        </button>
      )}
    </section>
  );
};
