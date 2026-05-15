import { Footer } from "../../Components/Layout/Footer";
import { Header } from "../../Components/Layout/Header";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";

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
