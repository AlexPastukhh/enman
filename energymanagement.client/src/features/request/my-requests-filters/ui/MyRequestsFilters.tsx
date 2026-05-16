import type { MyRequestsFilters as MyRequestsFiltersState } from "../../../../entities/request/model/myRequestsFilters";
import { MyRequestsStatusFilter } from "./MyRequestsStatusFilter";
import { myRequestsFiltersConst } from "./myRequestsFiltersConst";
import "./myRequestsFilters.css";

type MyRequestsFiltersProps = {
  filters: MyRequestsFiltersState;
  onChange: (filters: MyRequestsFiltersState) => void;
  onReset: () => void;
};

export const MyRequestsFilters = ({
  filters,
  onChange,
  onReset,
}: MyRequestsFiltersProps) => (
  <section className="myRequestsFilters" aria-labelledby="my-requests-filters-heading">
    <h2 id="my-requests-filters-heading">{myRequestsFiltersConst.title}</h2>
    <div className="myRequestsFilters__controls">
      <MyRequestsStatusFilter
        status={filters.status}
        onChange={(status) =>
          onChange({
            ...filters,
            status,
          })
        }
      />
      <button type="button" onClick={onReset}>
        {myRequestsFiltersConst.resetButtonText}
      </button>
    </div>
  </section>
);
