import { Link, useParams } from "react-router-dom";
import { useAgreementExchangeDetailsQuery } from "../../../../entities/agreement-exchange/model/useAgreementExchangeDetailsQuery";
import { useSession } from "../../../../entities/session/model/useSession";
import { getSendAgreementProposalAvailability } from "../../../../features/agreement-exchange/send-proposal/model/sendAgreementProposalAvailability";
import { SendAgreementProposalForm } from "../../../../features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm";
import { ApiError } from "../../../../shared/api/fetchJson";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { Footer } from "../../../../shared/ui/layout/Footer";
import { Header } from "../../../../shared/ui/layout/Header";
import { AgreementExchangeDetailsView } from "../../../../widgets/agreement-exchange-details/AgreementExchangeDetailsView";
import { employeeAgreementExchangeDetailsPageConst } from "./employeeAgreementExchangeDetailsPageConst";
import "./employeeAgreementExchangeDetailsPage.css";

const isEmployeeSession = (role?: string | null) => role === "Employee";

const parseExchangeId = (value: string | undefined): number | null => {
  if (!value) {
    return null;
  }

  const exchangeId = Number(value);
  return Number.isInteger(exchangeId) && exchangeId > 0 ? exchangeId : null;
};

const isNotFoundOrAccessError = (error: unknown) =>
  error instanceof ApiError && (error.status === 403 || error.status === 404);

const EmployeeAgreementExchangeDetailsPage = () => {
  const { exchangeId: exchangeIdParam } = useParams();
  const exchangeId = parseExchangeId(exchangeIdParam);
  const session = useSession();
  const isEmployee = isEmployeeSession(session?.role);
  const detailsQuery = useAgreementExchangeDetailsQuery({
    exchangeId: exchangeId ?? 0,
    enabled: Boolean(session) && isEmployee && exchangeId !== null,
  });

  return (
    <>
      <Header />
      <main className="content">
        <section
          className="employeeAgreementExchangeDetailsPage"
          aria-labelledby="employee-agreement-exchange-details-heading"
        >
          <Link
            className="employeeAgreementExchangeDetailsPage__backLink"
            to={clientRoutes.employeeAgreementExchanges}
          >
            {employeeAgreementExchangeDetailsPageConst.backToListText}
          </Link>
          <h1 id="employee-agreement-exchange-details-heading">
            {employeeAgreementExchangeDetailsPageConst.pageTitle}
          </h1>
          <p>{employeeAgreementExchangeDetailsPageConst.pageDescription}</p>

          {!session && (
            <div className="employeeAgreementExchangeDetailsPage__state">
              <h2>{employeeAgreementExchangeDetailsPageConst.signInRequiredTitle}</h2>
              <p>{employeeAgreementExchangeDetailsPageConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>
                {employeeAgreementExchangeDetailsPageConst.signInLinkText}
              </Link>
            </div>
          )}

          {session && !isEmployee && (
            <div className="employeeAgreementExchangeDetailsPage__state" role="alert">
              <h2>{employeeAgreementExchangeDetailsPageConst.accessDeniedTitle}</h2>
              <p>{employeeAgreementExchangeDetailsPageConst.accessDeniedDescription}</p>
            </div>
          )}

          {session && isEmployee && exchangeId === null && (
            <div className="employeeAgreementExchangeDetailsPage__state" role="alert">
              <h2>{employeeAgreementExchangeDetailsPageConst.invalidExchangeTitle}</h2>
              <p>{employeeAgreementExchangeDetailsPageConst.invalidExchangeDescription}</p>
            </div>
          )}

          {session && isEmployee && exchangeId !== null && detailsQuery.isPending && (
            <p className="employeeAgreementExchangeDetailsPage__state">
              {employeeAgreementExchangeDetailsPageConst.loadingText}
            </p>
          )}

          {session && isEmployee && exchangeId !== null && detailsQuery.isError && (
            <p className="employeeAgreementExchangeDetailsPage__state" role="alert">
              {isNotFoundOrAccessError(detailsQuery.error)
                ? employeeAgreementExchangeDetailsPageConst.notFoundText
                : employeeAgreementExchangeDetailsPageConst.errorText}
            </p>
          )}

          {session && isEmployee && detailsQuery.data && (
            <AgreementExchangeDetailsView
              details={detailsQuery.data}
              viewerRole="Employee"
              renderActions={(details) => {
                const availability = getSendAgreementProposalAvailability(
                  details,
                  "Employee",
                );

                return (
                  <SendAgreementProposalForm
                    exchangeId={details.exchangeId}
                    viewerRole="Employee"
                    disabled={!availability.canSendProposal}
                    unavailableReason={availability.reason}
                  />
                );
              }}
            />
          )}
        </section>
      </main>
      <Footer />
    </>
  );
};

export default EmployeeAgreementExchangeDetailsPage;
