import { Link } from "react-router-dom";
import { useAgreementExchangeListQuery } from "../../../../entities/agreement-exchange/model/useAgreementExchangeListQuery";
import { useSession } from "../../../../entities/session/model/useSession";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { Footer } from "../../../../shared/ui/layout/Footer";
import { Header } from "../../../../shared/ui/layout/Header";
import { AgreementExchangeList } from "../../../../widgets/agreement-exchange-list/AgreementExchangeList";
import { employeeAgreementExchangesDashboardPageConst } from "./employeeAgreementExchangesDashboardPageConst";
import "./employeeAgreementExchangesDashboardPage.css";

const isEmployeeSession = (role?: string | null) => role === "Employee";

const EmployeeAgreementExchangesDashboardPage = () => {
  const session = useSession();
  const isEmployee = isEmployeeSession(session?.role);
  const agreementsQuery = useAgreementExchangeListQuery({
    enabled: Boolean(session) && isEmployee,
  });

  return (
    <>
      <Header />
      <main className="content">
        <section
          className="employeeAgreementExchangesDashboardPage"
          aria-labelledby="employee-agreement-exchanges-heading"
        >
          <h1 id="employee-agreement-exchanges-heading">
            {employeeAgreementExchangesDashboardPageConst.pageTitle}
          </h1>
          <p>{employeeAgreementExchangesDashboardPageConst.pageDescription}</p>

          {!session && (
            <div className="employeeAgreementExchangesDashboardPage__state">
              <h2>{employeeAgreementExchangesDashboardPageConst.signInRequiredTitle}</h2>
              <p>{employeeAgreementExchangesDashboardPageConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>
                {employeeAgreementExchangesDashboardPageConst.signInLinkText}
              </Link>
            </div>
          )}

          {session && !isEmployee && (
            <div className="employeeAgreementExchangesDashboardPage__state" role="alert">
              <h2>{employeeAgreementExchangesDashboardPageConst.accessDeniedTitle}</h2>
              <p>{employeeAgreementExchangesDashboardPageConst.accessDeniedDescription}</p>
            </div>
          )}

          {session && isEmployee && agreementsQuery.isPending && (
            <p className="employeeAgreementExchangesDashboardPage__state">
              {employeeAgreementExchangesDashboardPageConst.loadingText}
            </p>
          )}

          {session && isEmployee && agreementsQuery.isError && (
            <p className="employeeAgreementExchangesDashboardPage__state" role="alert">
              {employeeAgreementExchangesDashboardPageConst.errorText}
            </p>
          )}

          {session && isEmployee && agreementsQuery.data && (
            <AgreementExchangeList
              exchanges={agreementsQuery.data}
              viewerRole="Employee"
              emptyStateTitle={employeeAgreementExchangesDashboardPageConst.emptyTitle}
              emptyStateDescription={employeeAgreementExchangesDashboardPageConst.emptyDescription}
              getDetailsHref={(exchange) =>
                clientRoutes.employeeAgreementExchangeDetails(exchange.exchangeId)
              }
            />
          )}
        </section>
      </main>
      <Footer />
    </>
  );
};

export default EmployeeAgreementExchangesDashboardPage;
