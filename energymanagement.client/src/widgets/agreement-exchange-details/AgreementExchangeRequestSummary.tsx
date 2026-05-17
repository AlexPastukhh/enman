import type { AgreementExchangeRequestSummary as AgreementExchangeRequestSummaryModel } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { agreementExchangeDetailsConst } from "./agreementExchangeDetailsConst";

type AgreementExchangeRequestSummaryProps = {
  request: AgreementExchangeRequestSummaryModel;
};

export const AgreementExchangeRequestSummary = ({
  request,
}: AgreementExchangeRequestSummaryProps) => (
  <section
    className="agreementExchangeDetails__panel"
    aria-labelledby="agreement-exchange-request-summary-heading"
  >
    <h3 id="agreement-exchange-request-summary-heading">
      {agreementExchangeDetailsConst.requestSummaryTitle}
    </h3>
    <dl>
      <div>
        <dt>Request</dt>
        <dd>{request.requestDisplayName ?? `Request #${request.requestId}`}</dd>
      </div>
      <div>
        <dt>Request status</dt>
        <dd>{request.requestStatus}</dd>
      </div>
      {request.objectAddress && (
        <div>
          <dt>Object address</dt>
          <dd>{request.objectAddress}</dd>
        </div>
      )}
    </dl>
  </section>
);
