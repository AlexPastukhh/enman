import type { AgreementExchangeRequestSummary as AgreementExchangeRequestSummaryModel } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { agreementExchangeDetailsConst } from "./agreementExchangeDetailsConst";
import { formatRequestStatus } from "./formatAgreementExchangeDetails";

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
        <dt>{agreementExchangeDetailsConst.requestLabel}</dt>
        <dd>{request.requestDisplayName ?? `Заявка #${request.requestId}`}</dd>
      </div>
      <div>
        <dt>{agreementExchangeDetailsConst.requestStatusLabel}</dt>
        <dd>{formatRequestStatus(request.requestStatus)}</dd>
      </div>
      {request.objectAddress && (
        <div>
          <dt>{agreementExchangeDetailsConst.objectAddressLabel}</dt>
          <dd>{request.objectAddress}</dd>
        </div>
      )}
    </dl>
  </section>
);
