import {
  listMyRequests as listMyRequestsApi,
  type L1ListMyRequestsResponse,
} from "../../../shared/api/l1RequestApi";
import type { MyRequestsFilters } from "../model/myRequestsFilters";

export const listMyRequests = (
  filters: MyRequestsFilters = {},
): Promise<L1ListMyRequestsResponse> =>
  listMyRequestsApi({
    status: filters.status,
  });
