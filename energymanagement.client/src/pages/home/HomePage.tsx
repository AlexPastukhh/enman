import { useSession } from "../../entities/session/model/useSession";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { NavButtonHollow } from "../../shared/ui/layout/NavButtonHollow";
import { NavButtonPrimary } from "../../shared/ui/layout/NavButtonPrimary";

const isEmployee = (role?: string | null) => role === "Employee";
const isClient = (role?: string | null) => role === "Client";

const homeText = {
  eyebrow: "\u041e\u041e\u041e \u00ab\u0417\u0421\u041a\u00bb",
  anonymousTitle:
    "\u041f\u043e\u0434\u0430\u0439\u0442\u0435 \u0437\u0430\u044f\u0432\u043a\u0443 \u043d\u0430 \u043f\u043e\u0434\u043a\u043b\u044e\u0447\u0435\u043d\u0438\u0435 \u043e\u043d\u043b\u0430\u0439\u043d",
  anonymousDescription:
    "\u0421\u043e\u0437\u0434\u0430\u0439\u0442\u0435 \u0430\u043a\u043a\u0430\u0443\u043d\u0442, \u0437\u0430\u043f\u043e\u043b\u043d\u0438\u0442\u0435 \u0434\u0430\u043d\u043d\u044b\u0435 \u0437\u0430\u044f\u0432\u0438\u0442\u0435\u043b\u044f \u0438 \u043e\u0444\u043e\u0440\u043c\u0438\u0442\u0435 \u0437\u0430\u044f\u0432\u043a\u0443 \u0431\u0435\u0437 \u0432\u0438\u0437\u0438\u0442\u0430 \u0432 \u043e\u0444\u0438\u0441.",
  clientTitle:
    "\u041f\u043e\u0434\u0430\u0439\u0442\u0435 \u0437\u0430\u044f\u0432\u043a\u0443 \u0438\u043b\u0438 \u043f\u0440\u043e\u0432\u0435\u0440\u044c\u0442\u0435 \u0441\u0442\u0430\u0442\u0443\u0441",
  clientDescription:
    "\u0412 \u043b\u0438\u0447\u043d\u043e\u043c \u043a\u0430\u0431\u0438\u043d\u0435\u0442\u0435 \u043c\u043e\u0436\u043d\u043e \u0441\u043e\u0437\u0434\u0430\u0442\u044c \u043d\u043e\u0432\u0443\u044e \u0437\u0430\u044f\u0432\u043a\u0443, \u043f\u043e\u0441\u043c\u043e\u0442\u0440\u0435\u0442\u044c \u0445\u043e\u0434 \u0440\u0430\u0441\u0441\u043c\u043e\u0442\u0440\u0435\u043d\u0438\u044f \u0438 \u043e\u0442\u043a\u0440\u044b\u0442\u044c \u0434\u043e\u0441\u0442\u0443\u043f\u043d\u044b\u0435 \u0434\u043e\u0433\u043e\u0432\u043e\u0440\u044b.",
  employeeTitle:
    "\u0420\u0430\u0431\u043e\u0447\u0435\u0435 \u043c\u0435\u0441\u0442\u043e \u0441\u043e\u0442\u0440\u0443\u0434\u043d\u0438\u043a\u0430",
  employeeDescription:
    "\u041f\u0440\u043e\u0432\u0435\u0440\u044f\u0439\u0442\u0435 \u0437\u0430\u044f\u0432\u043a\u0438, \u043f\u0440\u0438\u043d\u0438\u043c\u0430\u0439\u0442\u0435 \u0440\u0435\u0448\u0435\u043d\u0438\u044f \u0438 \u0432\u0435\u0434\u0438\u0442\u0435 \u0434\u043e\u0433\u043e\u0432\u043e\u0440\u044b \u043f\u043e \u043e\u0434\u043e\u0431\u0440\u0435\u043d\u043d\u044b\u043c \u0437\u0430\u044f\u0432\u043a\u0430\u043c.",
  signIn: "\u0412\u043e\u0439\u0442\u0438",
  register: "\u0417\u0430\u0440\u0435\u0433\u0438\u0441\u0442\u0440\u0438\u0440\u043e\u0432\u0430\u0442\u044c\u0441\u044f",
  createRequest:
    "\u0421\u043e\u0437\u0434\u0430\u0442\u044c \u0437\u0430\u044f\u0432\u043a\u0443",
  myRequests:
    "\u041c\u043e\u0438 \u0437\u0430\u044f\u0432\u043a\u0438",
  myAgreements:
    "\u041c\u043e\u0438 \u0434\u043e\u0433\u043e\u0432\u043e\u0440\u044b",
  employeeRequests:
    "\u0417\u0430\u044f\u0432\u043a\u0438 \u043d\u0430 \u0440\u0430\u0441\u0441\u043c\u043e\u0442\u0440\u0435\u043d\u0438\u0435",
  employeeAgreements:
    "\u0414\u043e\u0433\u043e\u0432\u043e\u0440\u044b",
} as const;

const HomePage = () => {
  const session = useSession();
  const employeeSession = isEmployee(session?.role);
  const clientSession = isClient(session?.role);

  const title = clientSession
    ? homeText.clientTitle
    : employeeSession
      ? homeText.employeeTitle
      : homeText.anonymousTitle;
  const description = clientSession
    ? homeText.clientDescription
    : employeeSession
      ? homeText.employeeDescription
      : homeText.anonymousDescription;

  return (
    <main className="content homePage">
      <section className="homePage__hero pageCard" aria-labelledby="home-page-heading">
        <div>
          <p className="pageEyebrow">{homeText.eyebrow}</p>
          <h1 className="pageTitle" id="home-page-heading">
            {title}
          </h1>
          <p className="pageDescription">{description}</p>

          {!session && (
            <div className="homePage__actions">
              <NavButtonPrimary to={clientRoutes.login}>
                {homeText.signIn}
              </NavButtonPrimary>
              <NavButtonHollow to={clientRoutes.register}>
                {homeText.register}
              </NavButtonHollow>
            </div>
          )}

          {clientSession && (
            <div className="homePage__actions">
              <NavButtonPrimary to={clientRoutes.createRequest}>
                {homeText.createRequest}
              </NavButtonPrimary>
              <NavButtonHollow to={clientRoutes.requests}>
                {homeText.myRequests}
              </NavButtonHollow>
              <NavButtonHollow to={clientRoutes.agreementExchanges}>
                {homeText.myAgreements}
              </NavButtonHollow>
            </div>
          )}

          {employeeSession && (
            <div className="homePage__actions">
              <NavButtonPrimary to={clientRoutes.employeeRequests}>
                {homeText.employeeRequests}
              </NavButtonPrimary>
              <NavButtonHollow to={clientRoutes.employeeAgreementExchanges}>
                {homeText.employeeAgreements}
              </NavButtonHollow>
            </div>
          )}
        </div>
      </section>

    </main>
  );
};

export default HomePage;
