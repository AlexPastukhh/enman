import { fetchJson } from "../../../../shared/api/fetchJson";
import type { StartAgreementExchangeInput } from "./startAgreementExchangeApiTypes";

const startAgreementExchangePath = (requestId: number | string) =>
  `/api/employee/requests/${encodeURIComponent(
    String(requestId),
  )}/agreement-exchange/start`;

export const startAgreementExchange = ({
  requestId,
  proposal,
}: StartAgreementExchangeInput): Promise<void> =>
  fetchJson<void>(startAgreementExchangePath(requestId), {
    method: "POST",
    body: JSON.stringify(proposal),
  });
