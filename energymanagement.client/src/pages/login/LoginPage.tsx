import { LoginForm } from "../../features/auth/login/ui/LoginForm";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";

export const LoginPage = () => {
  return (
    <>
      <Header />
      <main className="content">
        <LoginForm />
      </main>
      <Footer />
    </>
  );
};
