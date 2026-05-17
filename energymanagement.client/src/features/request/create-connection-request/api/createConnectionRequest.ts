import {
  createConnectionRequest as postCreateConnectionRequest,
  type L1CreateConnectionRequestRequest,
} from "../../../../shared/api/l1RequestApi";

export const createConnectionRequest = (
  request: L1CreateConnectionRequestRequest,
): Promise<void> => postCreateConnectionRequest(request);
