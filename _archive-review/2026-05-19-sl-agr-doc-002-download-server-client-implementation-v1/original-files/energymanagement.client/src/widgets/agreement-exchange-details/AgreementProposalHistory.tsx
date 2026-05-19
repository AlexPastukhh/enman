import type { AgreementProposalDetails } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { AgreementDocumentRefList } from "./AgreementDocumentRefList";
import { agreementExchangeDetailsConst } from "./agreementExchangeDetailsConst";
import {
  formatProposalSender,
  formatProposalVersion,
} from "./formatAgreementExchangeDetails";

type AgreementProposalHistoryProps = {
  proposals: AgreementProposalDetails[];
};

export const AgreementProposalHistory = ({
  proposals,
}: AgreementProposalHistoryProps) => (
  <section
    className="agreementExchangeDetails__panel"
    aria-labelledby="agreement-proposal-history-heading"
  >
    <h3 id="agreement-proposal-history-heading">
      {agreementExchangeDetailsConst.proposalHistoryTitle}
    </h3>
    {proposals.length === 0 && (
      <p>{agreementExchangeDetailsConst.noProposalHistoryText}</p>
    )}
    {proposals.length > 0 && (
      <ol className="agreementExchangeDetails__history">
        {proposals.map((proposal) => (
          <li key={proposal.proposalId}>
            <article className="agreementExchangeDetails__proposal">
              <h4>{formatProposalVersion(proposal.version, proposal.sender)}</h4>
              <dl>
                <div>
                  <dt>Sender</dt>
                  <dd>{formatProposalSender(proposal.sender, proposal.senderId)}</dd>
                </div>
                <div>
                  <dt>State</dt>
                  <dd>{proposal.state}</dd>
                </div>
                <div>
                  <dt>Created</dt>
                  <dd>{proposal.createdAt}</dd>
                </div>
              </dl>
              {proposal.comment && <p>{proposal.comment}</p>}
              <AgreementDocumentRefList document={proposal.document} />
            </article>
          </li>
        ))}
      </ol>
    )}
  </section>
);
