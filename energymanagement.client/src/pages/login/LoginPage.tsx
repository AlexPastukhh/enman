import { NavLink } from "react-router-dom";
import { LoginForm } from "../../features/auth/login/ui/LoginForm";
import { clientRoutes } from "../../shared/config/clientRoutes";

export const LoginPage = () => {
  return (
    <main className="content">
        <section className="authPage" aria-labelledby="login-page-heading">
          <div className="authPage__intro">
            <p className="pageEyebrow">Вход</p>
            <h1 className="pageTitle" id="login-page-heading">
              Продолжите работу с заявками и договорами
            </h1>
            <p className="pageDescription">
              Войдите как клиент для работы со своими заявками или используйте
              Employee-вход, если работаете с dashboard сотрудника.
            </p>
            <div className="homePage__actions">
              <NavLink className="button-hollow" to={clientRoutes.register}>
                Создать аккаунт клиента
              </NavLink>
              <NavLink className="button-hollow" to={clientRoutes.home}>
                На главную
              </NavLink>
            </div>
          </div>
          <div className="authPage__card">
            <LoginForm />
          </div>
        </section>
    </main>
  );
};
