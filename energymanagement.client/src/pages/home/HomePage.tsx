import { Link } from "react-router-dom";
import { useSession } from "../../entities/session/model/useSession";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";
import "./homePage.css";

type HomeAction = {
  title: string;
  description: string;
  href: string;
  variant?: "primary" | "secondary";
};

const guestActions: HomeAction[] = [
  {
    title: "Войти в личный кабинет",
    description: "Продолжить работу с заявками и договорными обменами.",
    href: clientRoutes.login,
    variant: "primary",
  },
  {
    title: "Зарегистрироваться",
    description: "Создать аккаунт клиента для подачи заявки на подключение.",
    href: clientRoutes.register,
  },
];

const clientActions: HomeAction[] = [
  {
    title: "Создать заявку",
    description: "Подать новую заявку на технологическое присоединение.",
    href: clientRoutes.createRequest,
    variant: "primary",
  },
  {
    title: "Мои заявки",
    description: "Посмотреть созданные заявки и их текущий статус.",
    href: clientRoutes.requests,
  },
  {
    title: "Мои договоры",
    description: "Открыть договорные обмены и актуальные предложения.",
    href: clientRoutes.agreementExchanges,
  },
];

const employeeActions: HomeAction[] = [
  {
    title: "Заявки на проверку",
    description: "Открыть рабочий список заявок Employee.",
    href: clientRoutes.employeeRequests,
    variant: "primary",
  },
  {
    title: "Договорные обмены",
    description: "Перейти к dashboard договорных обменов.",
    href: clientRoutes.employeeAgreementExchanges,
  },
  {
    title: "Личный кабинет",
    description: "Проверить текущую учетную запись и сессию.",
    href: clientRoutes.account,
  },
];

const fallbackActions: HomeAction[] = [
  {
    title: "Личный кабинет",
    description: "Открыть данные текущей учетной записи.",
    href: clientRoutes.account,
    variant: "primary",
  },
];

const getActionsForRole = (role?: string | null): HomeAction[] => {
  if (role === "Client") {
    return clientActions;
  }

  if (role === "Employee") {
    return employeeActions;
  }

  return fallbackActions;
};

const HomeActionCard = ({ action }: { action: HomeAction }) => (
  <article className="homePage__actionCard">
    <h3>{action.title}</h3>
    <p>{action.description}</p>
    <Link
      className={
        action.variant === "primary"
          ? "button-primary link-base-clear homePage__actionLink"
          : "button-hollow link-base-clear homePage__actionLink"
      }
      to={action.href}
    >
      Открыть
    </Link>
  </article>
);

const HomePage = () => {
  const session = useSession();
  const actions = session ? getActionsForRole(session.role) : guestActions;

  return (
    <>
      <Header />
      <main className="content">
        <section className="homePage" aria-labelledby="home-page-heading">
          <div className="homePage__hero">
            <p className="homePage__eyebrow">Energy Management System</p>
            <h1 id="home-page-heading">Заринская сетевая компания</h1>
            <p className="homePage__lead">
              Единая точка входа для заявок, проверки и договорного обмена.
            </p>
          </div>

          <section className="homePage__quickStart" aria-labelledby="home-actions-heading">
            <div className="homePage__sectionHeader">
              <h2 id="home-actions-heading">
                {session ? "Продолжить работу" : "Начать работу"}
              </h2>
              {session && (
                <p>
                  Вы вошли как <strong>{session.role}</strong>: {session.email}
                </p>
              )}
              {!session && (
                <p>
                  Войдите или зарегистрируйтесь, чтобы перейти к рабочему flow.
                </p>
              )}
            </div>

            <div className="homePage__actions">
              {actions.map((action) => (
                <HomeActionCard action={action} key={action.href} />
              ))}
            </div>
          </section>

          <section className="homePage__flow" aria-labelledby="home-flow-heading">
            <h2 id="home-flow-heading">Основной flow приложения</h2>
            <ol className="homePage__flowSteps">
              <li>
                <strong>Клиент</strong> создает заявку и отслеживает ее статус.
              </li>
              <li>
                <strong>Employee</strong> проверяет заявку и запускает договорный обмен.
              </li>
              <li>
                <strong>Стороны</strong> обмениваются предложениями до принятия или финального отказа.
              </li>
            </ol>
          </section>
        </section>
      </main>
      <Footer />
    </>
  );
};

export default HomePage;
