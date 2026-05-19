import type { AgreementProposalDetails } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { AgreementDocumentRefList } from "./AgreementDocumentRefList";
import { agreementExchangeDetailsConst } from "./agreementExchangeDetailsConst";
import {
  formatProposalSender,
  formatProposalState,
  formatProposalVersion,
} from "./formatAgreementExchangeDetails";

type AgreementActiveProposalPanelProps = {
  exchangeId: number;
  proposal?: AgreementProposalDetails | null;
};

export const AgreementActiveProposalPanel = ({
  exchangeId,
  proposal,
}: AgreementActiveProposalPanelProps) => (
  <section
    className="agreementExchangeDetails__panel"
    aria-labelledby="agreement-active-proposal-heading"
  >
    <h3 id="agreement-active-proposal-heading">
      {agreementExchangeDetailsConst.activeProposalTitle}
    </h3>
    {!proposal && <p>{agreementExchangeDetailsConst.noActiveProposalText}</p>}
    {proposal && (
      <article className="agreementExchangeDetails__proposal">
        <h4>{formatProposalVersion(proposal.version, proposal.sender)}</h4>
        <dl>
          <div>
            <dt>{agreementExchangeDetailsConst.senderLabel}</dt>
            <dd>{formatProposalSender(proposal.sender, proposal.senderId)}</dd>
          </div>
          <div>
            <dt>{agreementExchangeDetailsConst.stateLabel}</dt>
            <dd>{formatProposalState(proposal.state)}</dd>
          </div>
          <div>
            <dt>{agreementExchangeDetailsConst.createdAtLabel}</dt>
            <dd>{proposal.createdAt}</dd>
          </div>
        </dl>
        {proposal.comment && <p>{proposal.comment}</p>}
        <AgreementDocumentRefList
          exchangeId={exchangeId}
          proposalId={proposal.proposalId}
          document={proposal.document}
        />
      </article>
    )}
  </section>
);
