import { Footer } from "../../Components/Layout/Footer";
import { Header } from "../../Components/Layout/Header";
import { LoginForm } from "../../features/auth/login/ui/LoginForm";

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
