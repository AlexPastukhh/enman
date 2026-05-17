import { Link } from "react-router-dom";
import type { AgreementExchangeListItem } from "../model/agreementExchangeTypes";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import { agreementExchangeListConst } from "./agreementExchangeListConst";
import { formatAgreementExchangeDate, valueOrUnknown } from "./formatAgreementExchange";

type AgreementExchangeListRowProps = {
  exchange: AgreementExchangeListItem;
};

export const AgreementExchangeListRow = ({ exchange }: AgreementExchangeListRowProps) => {
  const titleId = `agreement-exchange-${exchange.exchangeId}-title`;

  return (
    <article className="agreementExchangeCard" aria-labelledby={titleId}>
      <h2 id={titleId} className="agreementExchangeCard__title">
        {agreementExchangeListConst.exchangeTitlePrefix} #{exchange.exchangeId}
      </h2>

      <dl className="agreementExchangeCard__summary">
        <div className="agreementExchangeCard__row">
          <dt>{agreementExchangeListConst.requestLabel}</dt>
          <dd>#{exchange.requestId}</dd>
        </div>
        <div className="agreementExchangeCard__row">
          <dt>{agreementExchangeListConst.statusLabel}</dt>
          <dd>{valueOrUnknown(exchange.exchangeStatus)}</dd>
        </div>
        <div className="agreementExchangeCard__row">
          <dt>{agreementExchangeListConst.activeVersionLabel}</dt>
          <dd>{valueOrUnknown(exchange.activeProposalVersion)}</dd>
        </div>
        <div className="agreementExchangeCard__row">
          <dt>{agreementExchangeListConst.activeSenderLabel}</dt>
          <dd>
            {valueOrUnknown(exchange.activeProposalSender)} #{exchange.activeProposalSenderId}
          </dd>
        </div>
        <div className="agreementExchangeCard__row">
          <dt>{agreementExchangeListConst.requestDisplayNameLabel}</dt>
          <dd>{valueOrUnknown(exchange.requestDisplayName)}</dd>
        </div>
        <div className="agreementExchangeCard__row">
          <dt>{agreementExchangeListConst.objectAddressLabel}</dt>
          <dd>{valueOrUnknown(exchange.objectAddress)}</dd>
        </div>
        <div className="agreementExchangeCard__row">
          <dt>{agreementExchangeListConst.createdAtLabel}</dt>
          <dd>{formatAgreementExchangeDate(exchange.createdAt)}</dd>
        </div>
        <div className="agreementExchangeCard__row">
          <dt>{agreementExchangeListConst.lastActivityAtLabel}</dt>
          <dd>{formatAgreementExchangeDate(exchange.lastActivityAt)}</dd>
        </div>
      </dl>

      <Link
        className="agreementExchangeCard__detailsLink"
        to={clientRoutes.agreementExchangeDetails(exchange.requestId)}
      >
        {agreementExchangeListConst.detailsLinkText}
      </Link>
    </article>
  );
};
