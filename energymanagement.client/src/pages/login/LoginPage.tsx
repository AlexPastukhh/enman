import { NavLink } from "react-router-dom";
import { LoginForm } from "../../features/auth/login/ui/LoginForm";
import { clientRoutes } from "../../shared/config/clientRoutes";

const loginPageText = {
  eyebrow: "\u0412\u0445\u043e\u0434",
  title: "\u0412\u043e\u0439\u0434\u0438\u0442\u0435 \u0432 \u043b\u0438\u0447\u043d\u044b\u0439 \u043a\u0430\u0431\u0438\u043d\u0435\u0442",
  description:
    "\u0417\u0434\u0435\u0441\u044c \u043c\u043e\u0436\u043d\u043e \u043f\u043e\u0434\u0430\u0442\u044c \u0437\u0430\u044f\u0432\u043a\u0443, \u043f\u043e\u0441\u043c\u043e\u0442\u0440\u0435\u0442\u044c \u0435\u0451 \u0441\u0442\u0430\u0442\u0443\u0441 \u0438 \u043f\u0435\u0440\u0435\u0439\u0442\u0438 \u043a \u0434\u043e\u0433\u043e\u0432\u043e\u0440\u0443.",
  createAccount: "\u0421\u043e\u0437\u0434\u0430\u0442\u044c \u0430\u043a\u043a\u0430\u0443\u043d\u0442",
  home: "\u041d\u0430 \u0433\u043b\u0430\u0432\u043d\u0443\u044e",
} as const;

export const LoginPage = () => {
  return (
    <main className="content">
      <section className="authPage" aria-labelledby="login-page-heading">
        <div className="authPage__intro">
          <p className="pageEyebrow">{loginPageText.eyebrow}</p>
          <h1 className="pageTitle" id="login-page-heading">
            {loginPageText.title}
          </h1>
          <p className="pageDescription">{loginPageText.description}</p>
          <div className="homePage__actions">
            <NavLink className="button-hollow" to={clientRoutes.register}>
              {loginPageText.createAccount}
            </NavLink>
            <NavLink className="button-hollow" to={clientRoutes.home}>
              {loginPageText.home}
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
