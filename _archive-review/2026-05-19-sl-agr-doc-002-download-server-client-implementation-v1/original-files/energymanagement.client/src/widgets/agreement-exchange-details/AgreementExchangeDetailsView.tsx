import type { ReactNode } from "react";
import type { AgreementExchangeDetails } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { AgreementActiveProposalPanel } from "./AgreementActiveProposalPanel";
import { AgreementExchangeRequestSummary } from "./AgreementExchangeRequestSummary";
import { AgreementExchangeStatusPanel } from "./AgreementExchangeStatusPanel";
import { AgreementProposalHistory } from "./AgreementProposalHistory";
import { agreementExchangeDetailsConst } from "./agreementExchangeDetailsConst";
import "./agreementExchangeDetails.css";

type ViewerRole = "Client" | "Employee";

type AgreementExchangeDetailsViewProps = {
  details: AgreementExchangeDetails;
  viewerRole: ViewerRole;
  renderActions?: (details: AgreementExchangeDetails) => ReactNode;
};

export const AgreementExchangeDetailsView = ({
  details,
  renderActions,
}: AgreementExchangeDetailsViewProps) => {
  const actions = renderActions?.(details);

  return (
    <div className="agreementExchangeDetails">
      <AgreementExchangeStatusPanel details={details} />
      <AgreementExchangeRequestSummary request={details.request} />
      <AgreementActiveProposalPanel proposal={details.activeProposal} />
      <AgreementProposalHistory proposals={details.proposals ?? []} />
      {actions && (
        <section
          className="agreementExchangeDetails__panel"
          aria-labelledby="agreement-exchange-actions-heading"
        >
          <h3 id="agreement-exchange-actions-heading">
            {agreementExchangeDetailsConst.actionsRegionTitle}
          </h3>
          {actions}
        </section>
      )}
    </div>
  );
};
