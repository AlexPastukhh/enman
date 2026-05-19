import type { AgreementExchangeDetails } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { agreementExchangeDetailsConst } from "./agreementExchangeDetailsConst";
import { formatAgreementExchangeStatus } from "./formatAgreementExchangeDetails";

type AgreementExchangeStatusPanelProps = {
  details: AgreementExchangeDetails;
};

export const AgreementExchangeStatusPanel = ({
  details,
}: AgreementExchangeStatusPanelProps) => (
  <section
    className="agreementExchangeDetails__panel"
    aria-labelledby="agreement-exchange-status-heading"
  >
    <h3 id="agreement-exchange-status-heading">
      {agreementExchangeDetailsConst.exchangeTitlePrefix} #{details.exchangeId}
    </h3>
    <dl>
      <div>
        <dt>{agreementExchangeDetailsConst.statusLabel}</dt>
        <dd>{formatAgreementExchangeStatus(details.exchangeStatus)}</dd>
      </div>
      <div>
        <dt>{agreementExchangeDetailsConst.actorSideLabel}</dt>
        <dd>{details.currentActorSide === "Employee" ? "Сотрудник" : "Клиент"}</dd>
      </div>
      <div>
        <dt>{agreementExchangeDetailsConst.createdAtLabel}</dt>
        <dd>{details.createdAt}</dd>
      </div>
      {details.lastActivityAt && (
        <div>
          <dt>{agreementExchangeDetailsConst.lastActivityAtLabel}</dt>
          <dd>{details.lastActivityAt}</dd>
        </div>
      )}
    </dl>
  </section>
);
