import { Link } from "react-router-dom";
import { useAgreementExchangeListQuery } from "../../../entities/agreement-exchange/model/useAgreementExchangeListQuery";
import { useSession } from "../../../entities/session/model/useSession";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import { AgreementExchangeList } from "../../../widgets/agreement-exchange-list/AgreementExchangeList";
import { clientAgreementExchangesPageConst } from "./clientAgreementExchangesPageConst";
import "./clientAgreementExchangesPage.css";

const isClientSession = (role?: string | null) => role === "Client";

const ClientAgreementExchangesPage = () => {
  const session = useSession();
  const isClient = isClientSession(session?.role);
  const agreementsQuery = useAgreementExchangeListQuery({
    enabled: Boolean(session) && isClient,
  });

  return (
    <main className="content">
      <section
        className="clientAgreementExchangesPage clientPage"
        aria-labelledby="client-agreement-exchanges-heading"
      >
        <h1 id="client-agreement-exchanges-heading">
          {clientAgreementExchangesPageConst.pageTitle}
        </h1>
        <p className="clientPage__description">{clientAgreementExchangesPageConst.pageDescription}</p>

        {!session && (
          <div className="clientAgreementExchangesPage__state">
            <h2>{clientAgreementExchangesPageConst.signInRequiredTitle}</h2>
            <p>{clientAgreementExchangesPageConst.signInRequiredDescription}</p>
            <Link to={clientRoutes.login}>
              {clientAgreementExchangesPageConst.signInLinkText}
            </Link>
          </div>
        )}

        {session && !isClient && (
          <div className="clientAgreementExchangesPage__state" role="alert">
            <h2>{clientAgreementExchangesPageConst.accessDeniedTitle}</h2>
            <p>{clientAgreementExchangesPageConst.accessDeniedDescription}</p>
          </div>
        )}

        {session && isClient && agreementsQuery.isPending && (
          <p className="clientAgreementExchangesPage__state">
            {clientAgreementExchangesPageConst.loadingText}
          </p>
        )}

        {session && isClient && agreementsQuery.isError && (
          <p className="clientAgreementExchangesPage__state" role="alert">
            {clientAgreementExchangesPageConst.errorText}
          </p>
        )}

        {session && isClient && agreementsQuery.data && (
          <AgreementExchangeList
            exchanges={agreementsQuery.data}
            viewerRole="Client"
            emptyStateTitle={clientAgreementExchangesPageConst.emptyTitle}
            emptyStateDescription={clientAgreementExchangesPageConst.emptyDescription}
            getDetailsHref={(exchange) =>
              clientRoutes.agreementExchangeDetails(exchange.exchangeId)
            }
          />
        )}
      </section>
    </main>
  );
};

export default ClientAgreementExchangesPage;
