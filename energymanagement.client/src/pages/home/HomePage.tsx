import { useSession } from "../../entities/session/model/useSession";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { NavButtonHollow } from "../../shared/ui/layout/NavButtonHollow";
import { NavButtonPrimary } from "../../shared/ui/layout/NavButtonPrimary";

const isEmployee = (role?: string | null) => role === "Employee";
const isClient = (role?: string | null) => role === "Client";

const HomePage = () => {
  const session = useSession();
  const employeeSession = isEmployee(session?.role);
  const clientSession = isClient(session?.role);

  return (
    <main className="content homePage">
        <section className="homePage__hero pageCard" aria-labelledby="home-page-heading">
          <div>
            <p className="pageEyebrow">Energy Management</p>
            <h1 className="pageTitle" id="home-page-heading">
              Заявки, проверки и договоры в одном рабочем потоке
            </h1>
            <p className="pageDescription">
              Клиент создаёт заявку и отслеживает договоры, сотрудник ведёт
              проверку, предложения и финальные решения без лишних переходов.
            </p>

            {!session && (
              <div className="homePage__actions">
                <NavButtonPrimary to={clientRoutes.login}>Войти</NavButtonPrimary>
                <NavButtonHollow to={clientRoutes.register}>
                  Зарегистрироваться
                </NavButtonHollow>
              </div>
            )}

            {clientSession && (
              <div className="homePage__actions">
                <NavButtonPrimary to={clientRoutes.createRequest}>
                  Создать заявку
                </NavButtonPrimary>
                <NavButtonHollow to={clientRoutes.requests}>
                  Мои заявки
                </NavButtonHollow>
                <NavButtonHollow to={clientRoutes.agreementExchanges}>
                  Мои договоры
                </NavButtonHollow>
              </div>
            )}

            {employeeSession && (
              <div className="homePage__actions">
                <NavButtonPrimary to={clientRoutes.employeeRequests}>
                  Заявки Employee
                </NavButtonPrimary>
                <NavButtonHollow to={clientRoutes.employeeAgreementExchanges}>
                  Договорные обмены
                </NavButtonHollow>
              </div>
            )}
          </div>

          <aside className="homePage__summary" aria-label="Основные сценарии">
            <span className="statusPill">Flow</span>
            <ul>
              <li>Клиент создаёт заявку и видит статус.</li>
              <li>Employee проверяет заявку и запускает обмен.</li>
              <li>Стороны работают с версиями предложений.</li>
            </ul>
          </aside>
        </section>

        <section className="homePage__cards" aria-label="Разделы приложения">
          <article className="homePage__card pageCard">
            <h2>Клиент</h2>
            <p>Создание заявок, просмотр своих заявок и договорных обменов.</p>
            <NavButtonHollow to={clientRoutes.agreementExchanges}>
              Мои договоры
            </NavButtonHollow>
          </article>
          <article className="homePage__card pageCard">
            <h2>Employee</h2>
            <p>Рабочий dashboard заявок, детали проверки и договорные обмены.</p>
            <NavButtonHollow to={clientRoutes.employeeRequests}>
              Рабочие заявки
            </NavButtonHollow>
          </article>
          <article className="homePage__card pageCard">
            <h2>Договорный обмен</h2>
            <p>История предложений, активное предложение и доступные действия.</p>
            <NavButtonHollow to={clientRoutes.employeeAgreementExchanges}>
              Открыть exchanges
            </NavButtonHollow>
          </article>
        </section>
    </main>
  );
};

export default HomePage;
