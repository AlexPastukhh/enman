import { Link } from "react-router-dom";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";

const RegisterPage = () => {
  return (
    <>
      <Header />
      <main className="content register-page">
        <section className="authPage" aria-labelledby="register-page-heading">
          <div className="authPage__intro">
            <h1 id="register-page-heading">Регистрация клиента</h1>
            <p>
              Создайте клиентский аккаунт для подачи заявки и отслеживания
              договорных обменов. Уже есть аккаунт?{" "}
              <Link to={clientRoutes.login}>Войдите</Link>.
            </p>
          </div>
          <RegisterForm />
        </section>
      </main>
      <Footer />
    </>
  );
};

export default RegisterPage;
