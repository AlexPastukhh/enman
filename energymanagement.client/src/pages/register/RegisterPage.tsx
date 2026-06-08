import { NavLink } from "react-router-dom";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { clientRoutes } from "../../shared/config/clientRoutes";

const registerPageText = {
  eyebrow: "\u041a\u043b\u0438\u0435\u043d\u0442\u0441\u043a\u0438\u0439 \u0430\u043a\u043a\u0430\u0443\u043d\u0442",
  title: "\u0421\u043e\u0437\u0434\u0430\u0439\u0442\u0435 \u0430\u043a\u043a\u0430\u0443\u043d\u0442 \u0434\u043b\u044f \u043f\u043e\u0434\u0430\u0447\u0438 \u0437\u0430\u044f\u0432\u043a\u0438",
  description:
    "\u041f\u043e\u0441\u043b\u0435 \u0432\u0445\u043e\u0434\u0430 \u0432\u044b \u0441\u043c\u043e\u0436\u0435\u0442\u0435 \u0443\u043a\u0430\u0437\u0430\u0442\u044c \u0434\u0430\u043d\u043d\u044b\u0435 \u0437\u0430\u044f\u0432\u0438\u0442\u0435\u043b\u044f \u0438 \u043e\u0444\u043e\u0440\u043c\u0438\u0442\u044c \u0437\u0430\u044f\u0432\u043a\u0443 \u043d\u0430 \u043f\u043e\u0434\u043a\u043b\u044e\u0447\u0435\u043d\u0438\u0435.",
  hasAccount: "\u0423\u0436\u0435 \u0435\u0441\u0442\u044c \u0430\u043a\u043a\u0430\u0443\u043d\u0442",
  home: "\u041d\u0430 \u0433\u043b\u0430\u0432\u043d\u0443\u044e",
} as const;

const RegisterPage = () => {
  return (
    <main className="content register-page">
      <section className="authPage" aria-labelledby="register-page-heading">
        <div className="authPage__intro">
          <p className="pageEyebrow">{registerPageText.eyebrow}</p>
          <h1 className="pageTitle" id="register-page-heading">
            {registerPageText.title}
          </h1>
          <p className="pageDescription">{registerPageText.description}</p>
          <div className="homePage__actions">
            <NavLink className="button-hollow" to={clientRoutes.login}>
              {registerPageText.hasAccount}
            </NavLink>
            <NavLink className="button-hollow" to={clientRoutes.home}>
              {registerPageText.home}
            </NavLink>
          </div>
        </div>
        <div className="authPage__card">
          <RegisterForm />
        </div>
      </section>
    </main>
  );
};

export default RegisterPage;
