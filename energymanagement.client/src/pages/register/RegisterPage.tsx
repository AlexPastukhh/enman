import { useState } from "react";
import { ErrorMessage } from "../../Components/Layout/ErrorMessage";
import { Footer } from "../../Components/Layout/Footer";
import { Header } from "../../Components/Layout/Header";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";

const RegisterPage = () => {
  const [rootError, setRootError] = useState<string>("");

  return (
    <>
      <Header />
      <main className="content register-page">
        <RegisterForm setRootError={setRootError} />
      </main>
      {rootError && <ErrorMessage message={rootError} />}
      <Footer />
    </>
  );
};

export default RegisterPage;

