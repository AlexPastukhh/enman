import type { MyRequestSummary } from "../../../../entities/request/model/requestTypes";
import {
  MyRequestsEmptyState,
  type MyRequestsEmptyStateVariant,
} from "./MyRequestsEmptyState";
import { MyRequestSummaryCard } from "./MyRequestSummaryCard";

type MyRequestsListProps = {
  requests: MyRequestSummary[];
  emptyStateVariant?: MyRequestsEmptyStateVariant;
  onResetFilters?: () => void;
};

export const MyRequestsList = ({
  requests,
  emptyStateVariant = "default",
  onResetFilters,
}: MyRequestsListProps) => {
  if (requests.length === 0) {
    return (
      <MyRequestsEmptyState
        variant={emptyStateVariant}
        onResetFilters={onResetFilters}
      />
    );
  }

  return (
    <div className="myRequestsList" aria-label="Список моих заявок">
      {requests.map((request, index) => (
        <MyRequestSummaryCard
          key={request.requestId ?? `my-request-${index}`}
          request={request}
        />
      ))}
    </div>
  );
};
