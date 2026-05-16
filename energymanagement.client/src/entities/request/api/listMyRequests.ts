import {
  listMyRequests as listMyRequestsApi,
  type L1ListMyRequestsResponse,
} from "../../../shared/api/l1RequestApi";

export const listMyRequests = (): Promise<L1ListMyRequestsResponse> =>
  listMyRequestsApi();
