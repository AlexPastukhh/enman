import { useSession } from "../../entities/session/model/useSession";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";

const AccountPage = () => {
  const session = useSession();

  return (
    <>
      <Header />
      <main className="content">
        {!session && <RegisterForm />}
        {session && <p>{session.email}</p>}
      </main>
      <Footer />
    </>
  );
};

export default AccountPage;
