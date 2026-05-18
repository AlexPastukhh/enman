import { Link } from "react-router-dom";
import { LoginForm } from "../../features/auth/login/ui/LoginForm";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";

export const LoginPage = () => {
  return (
    <>
      <Header />
      <main className="content">
        <section className="authPage" aria-labelledby="login-page-heading">
          <div className="authPage__intro">
            <h1 id="login-page-heading">Вход в личный кабинет</h1>
            <p>
              Войдите, чтобы продолжить работу с заявками, проверкой и договорными
              обменами. Нет аккаунта?{" "}
              <Link to={clientRoutes.register}>Зарегистрируйтесь</Link>.
            </p>
          </div>
          <LoginForm />
        </section>
      </main>
      <Footer />
    </>
  );
};
