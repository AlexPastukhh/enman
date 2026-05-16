import type { MyRequestSummary } from "../../../../entities/request/model/requestTypes";
import { MyRequestsEmptyState } from "./MyRequestsEmptyState";
import { MyRequestSummaryCard } from "./MyRequestSummaryCard";

type MyRequestsListProps = {
  requests: MyRequestSummary[];
};

export const MyRequestsList = ({ requests }: MyRequestsListProps) => {
  if (requests.length === 0) {
    return <MyRequestsEmptyState />;
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
