import { useState } from "react";
import { ErrorMessage } from "../../Components/Layout/ErrorMessage";
import { Footer } from "../../Components/Layout/Footer";
import { Header } from "../../Components/Layout/Header";
import { LoginForm } from "../../features/auth/login/ui/LoginForm";

export const LoginPage = () => {
  const [rootError, setRootError] = useState<string>("");

  return (
    <>
      <Header />
      <main className="content">
        <LoginForm setRootError={setRootError} />
      </main>
      {rootError && <ErrorMessage message={rootError} />}
      <Footer />
    </>
  );
};

