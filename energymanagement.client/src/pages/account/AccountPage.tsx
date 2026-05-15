import { useState } from "react";
import { ErrorMessage } from "../../Components/Layout/ErrorMessage";
import { Footer } from "../../Components/Layout/Footer";
import { Header } from "../../Components/Layout/Header";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { useSession } from "../../entities/session/model/useSession";

const AccountPage = () => {
  const session = useSession();
  const [rootError, setRootError] = useState<string>("");

  return (
    <>
      <Header />
      <main className="content">
        {!session && <RegisterForm setRootError={setRootError} />}
        {session && <p>{session.email}</p>}
      </main>
      {rootError && <ErrorMessage message={rootError} />}
      <Footer />
    </>
  );
};

export default AccountPage;

