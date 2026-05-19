import { Link, useParams } from "react-router-dom";
import { useAgreementExchangeDetailsQuery } from "../../../entities/agreement-exchange/model/useAgreementExchangeDetailsQuery";
import { useSession } from "../../../entities/session/model/useSession";
import { getAcceptAgreementProposalAvailability } from "../../../features/agreement-exchange/accept-proposal/model/acceptAgreementProposalAvailability";
import { AcceptAgreementProposalButton } from "../../../features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton";
import { getSendAgreementProposalAvailability } from "../../../features/agreement-exchange/send-proposal/model/sendAgreementProposalAvailability";
import { SendAgreementProposalForm } from "../../../features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm";
import { ApiError } from "../../../shared/api/fetchJson";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import { AgreementExchangeDetailsView } from "../../../widgets/agreement-exchange-details/AgreementExchangeDetailsView";
import { clientAgreementExchangeDetailsPageConst } from "./clientAgreementExchangeDetailsPageConst";
import "./clientAgreementExchangeDetailsPage.css";

const isClientSession = (role?: string | null) => role === "Client";

const parseExchangeId = (value: string | undefined): number | null => {
  if (!value) {
    return null;
  }

  const exchangeId = Number(value);
  return Number.isInteger(exchangeId) && exchangeId > 0 ? exchangeId : null;
};

const isNotFoundOrAccessError = (error: unknown) =>
  error instanceof ApiError && (error.status === 403 || error.status === 404);

const ClientAgreementExchangeDetailsPage = () => {
  const { exchangeId: exchangeIdParam } = useParams();
  const exchangeId = parseExchangeId(exchangeIdParam);
  const session = useSession();
  const isClient = isClientSession(session?.role);
  const detailsQuery = useAgreementExchangeDetailsQuery({
    exchangeId: exchangeId ?? 0,
    enabled: Boolean(session) && isClient && exchangeId !== null,
  });

  return (
    <main className="content">
      <section
        className="clientAgreementExchangeDetailsPage clientPage"
        aria-labelledby="client-agreement-exchange-details-heading"
      >
        <Link
          className="clientAgreementExchangeDetailsPage__backLink"
          to={clientRoutes.agreementExchanges}
        >
          {clientAgreementExchangeDetailsPageConst.backToListText}
        </Link>
        <h1 id="client-agreement-exchange-details-heading">
          {clientAgreementExchangeDetailsPageConst.pageTitle}
        </h1>
        <p className="clientPage__description">{clientAgreementExchangeDetailsPageConst.pageDescription}</p>

        {!session && (
          <div className="clientAgreementExchangeDetailsPage__state">
            <h2>{clientAgreementExchangeDetailsPageConst.signInRequiredTitle}</h2>
            <p>{clientAgreementExchangeDetailsPageConst.signInRequiredDescription}</p>
            <Link to={clientRoutes.login}>
              {clientAgreementExchangeDetailsPageConst.signInLinkText}
            </Link>
          </div>
        )}

        {session && !isClient && (
          <div className="clientAgreementExchangeDetailsPage__state" role="alert">
            <h2>{clientAgreementExchangeDetailsPageConst.accessDeniedTitle}</h2>
            <p>{clientAgreementExchangeDetailsPageConst.accessDeniedDescription}</p>
          </div>
        )}

        {session && isClient && exchangeId === null && (
          <div className="clientAgreementExchangeDetailsPage__state" role="alert">
            <h2>{clientAgreementExchangeDetailsPageConst.invalidExchangeTitle}</h2>
            <p>{clientAgreementExchangeDetailsPageConst.invalidExchangeDescription}</p>
          </div>
        )}

        {session && isClient && exchangeId !== null && detailsQuery.isPending && (
          <p className="clientAgreementExchangeDetailsPage__state">
            {clientAgreementExchangeDetailsPageConst.loadingText}
          </p>
        )}

        {session && isClient && exchangeId !== null && detailsQuery.isError && (
          <p className="clientAgreementExchangeDetailsPage__state" role="alert">
            {isNotFoundOrAccessError(detailsQuery.error)
              ? clientAgreementExchangeDetailsPageConst.notFoundText
              : clientAgreementExchangeDetailsPageConst.errorText}
          </p>
        )}

        {session && isClient && detailsQuery.data && (
          <AgreementExchangeDetailsView
            details={detailsQuery.data}
            viewerRole="Client"
            renderActions={(details) => {
              const sendAvailability = getSendAgreementProposalAvailability(
                details,
                "Client",
              );
              const acceptAvailability = getAcceptAgreementProposalAvailability(details);

              return (
                <>
                  <AcceptAgreementProposalButton
                    exchangeId={details.exchangeId}
                    disabled={!acceptAvailability.canAcceptProposal}
                    unavailableReason={acceptAvailability.reason}
                  />
                  <SendAgreementProposalForm
                    exchangeId={details.exchangeId}
                    requestId={details.requestId}
                    viewerRole="Client"
                    disabled={!sendAvailability.canSendProposal}
                    unavailableReason={sendAvailability.reason}
                  />
                </>
              );
            }}
          />
        )}
      </section>
    </main>
  );
};

export default ClientAgreementExchangeDetailsPage;
