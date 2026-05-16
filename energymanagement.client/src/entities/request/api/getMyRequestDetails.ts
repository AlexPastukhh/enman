import {
  getMyRequestDetails as getMyRequestDetailsApi,
  type L1MyRequestDetails,
} from "../../../shared/api/l1RequestApi";

export const getMyRequestDetails = (
  requestId: number,
): Promise<L1MyRequestDetails> => getMyRequestDetailsApi(requestId);
