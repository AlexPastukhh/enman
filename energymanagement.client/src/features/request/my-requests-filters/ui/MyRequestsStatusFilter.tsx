import type { ChangeEvent } from "react";
import {
  myRequestStatuses,
  type MyRequestStatus,
} from "../../../../entities/request/model/myRequestsFilters";
import { myRequestsFiltersConst } from "./myRequestsFiltersConst";

type MyRequestsStatusFilterProps = {
  status?: MyRequestStatus;
  onChange: (status: MyRequestStatus | undefined) => void;
};

export const MyRequestsStatusFilter = ({
  status,
  onChange,
}: MyRequestsStatusFilterProps) => {
  const handleStatusChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const value = event.target.value;
    onChange(value ? (value as MyRequestStatus) : undefined);
  };

  return (
    <div className="myRequestsFilters__field">
      <label htmlFor="my-requests-status-filter">
        {myRequestsFiltersConst.statusLabel}
      </label>
      <select
        id="my-requests-status-filter"
        value={status ?? ""}
        onChange={handleStatusChange}
      >
        <option value="">{myRequestsFiltersConst.allStatusesLabel}</option>
        {myRequestStatuses.map((requestStatus) => (
          <option key={requestStatus} value={requestStatus}>
            {myRequestsFiltersConst.statusOptions[requestStatus]}
          </option>
        ))}
      </select>
    </div>
  );
};
