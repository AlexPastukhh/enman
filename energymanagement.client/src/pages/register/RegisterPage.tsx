import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";

const RegisterPage = () => {
  return (
    <>
      <Header />
      <main className="content register-page">
        <RegisterForm />
      </main>
      <Footer />
    </>
  );
};

export default RegisterPage;
