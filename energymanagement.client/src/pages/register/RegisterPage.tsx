import { NavLink } from "react-router-dom";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { clientRoutes } from "../../shared/config/clientRoutes";

const RegisterPage = () => {
  return (
    <main className="content register-page">
        <section className="authPage" aria-labelledby="register-page-heading">
          <div className="authPage__intro">
            <p className="pageEyebrow">Клиентский аккаунт</p>
            <h1 className="pageTitle" id="register-page-heading">
              Зарегистрируйтесь, чтобы создать заявку
            </h1>
            <p className="pageDescription">
              После регистрации можно добавить данные заявителя, создать заявку
              на подключение и отслеживать договорный обмен.
            </p>
            <div className="homePage__actions">
              <NavLink className="button-hollow" to={clientRoutes.login}>
                Уже есть аккаунт
              </NavLink>
              <NavLink className="button-hollow" to={clientRoutes.home}>
                На главную
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
