import { Footer } from "../../Components/Layout/Footer";
import { Header } from "../../Components/Layout/Header";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { useSession } from "../../entities/session/model/useSession";

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
