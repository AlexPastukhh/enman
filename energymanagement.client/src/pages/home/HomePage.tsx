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
            <p className="pageEyebrow">ООО «ЗСК»</p>
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
                  Заявки на рассмотрение
                </NavButtonPrimary>
                <NavButtonHollow to={clientRoutes.employeeAgreementExchanges}>
                  Согласование договоров
                </NavButtonHollow>
              </div>
            )}
          </div>

          {/* Process promo removed per UI visibility rules */}
        </section>

        <section className="homePage__cards" aria-label="Разделы приложения">
          {clientSession && (
            <article className="homePage__card pageCard">
              <h2>Клиент</h2>
              <p>Создание заявок, просмотр своих заявок и согласований договоров.</p>
              <NavButtonHollow to={clientRoutes.agreementExchanges}>
                Мои договоры
              </NavButtonHollow>
            </article>
          )}

          {employeeSession && (
            <>
              <article className="homePage__card pageCard">
                <h2>Сотрудник</h2>
                <p>Рабочая панель заявок, детали проверки и согласование договоров.</p>
                <NavButtonHollow to={clientRoutes.employeeRequests}>
                  Перейти к заявкам
                </NavButtonHollow>
              </article>

              <article className="homePage__card pageCard">
                <h2>Согласование договора</h2>
                <p>История предложений, активное предложение и доступные действия.</p>
                <NavButtonHollow to={clientRoutes.employeeAgreementExchanges}>
                  Перейти к согласованию договоров
                </NavButtonHollow>
              </article>
            </>
          )}
        </section>
    </main>
  );
};

export default HomePage;
