import { Link } from "react-router-dom";
import type { AgreementExchangeListItem } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { agreementExchangeListConst } from "./agreementExchangeListConst";
import {
  formatAgreementExchangeStatus,
  formatDateTime,
  formatProposalSummary,
  getAgreementExchangeTitle,
} from "./formatAgreementExchangeList";

type AgreementExchangeRowProps = {
  exchange: AgreementExchangeListItem;
  detailsHref: string;
};

export const AgreementExchangeRow = ({
  exchange,
  detailsHref,
}: AgreementExchangeRowProps) => (
  <article className="agreementExchangeRow" aria-labelledby={`agreement-exchange-${exchange.exchangeId}-title`}>
    <div className="agreementExchangeRow__header">
      <div>
        <p className="agreementExchangeRow__eyebrow">
          {agreementExchangeListConst.exchangeLabel} #{exchange.exchangeId}
        </p>
        <h3 id={`agreement-exchange-${exchange.exchangeId}-title`}>
          {getAgreementExchangeTitle(exchange)}
        </h3>
      </div>
      <span className="agreementExchangeRow__status">
        {formatAgreementExchangeStatus(exchange.exchangeStatus)}
      </span>
    </div>

    <dl className="agreementExchangeRow__meta">
      <div>
        <dt>{agreementExchangeListConst.requestLabel}</dt>
        <dd>#{exchange.requestId}</dd>
      </div>
      <div>
        <dt>{agreementExchangeListConst.activeProposalLabel}</dt>
        <dd>{formatProposalSummary(exchange)}</dd>
      </div>
      <div>
        <dt>{agreementExchangeListConst.createdAtLabel}</dt>
        <dd>{formatDateTime(exchange.createdAt)}</dd>
      </div>
      <div>
        <dt>{agreementExchangeListConst.lastActivityAtLabel}</dt>
        <dd>{formatDateTime(exchange.lastActivityAt)}</dd>
      </div>
    </dl>

    <p className="agreementExchangeRow__address">
      {exchange.objectAddress?.trim() || agreementExchangeListConst.objectAddressFallback}
    </p>

    <Link className="agreementExchangeRow__detailsLink" to={detailsHref}>
      {agreementExchangeListConst.openDetailsText}
    </Link>
  </article>
);
