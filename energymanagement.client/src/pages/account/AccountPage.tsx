import { useSession } from "../../entities/session/model/useSession";
import { CreateIndividualApplicantPartyForm } from "../../features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm";
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
        {session && (
          <section aria-labelledby="account-page-heading">
            <h1 id="account-page-heading">Account</h1>
            <p>{session.email}</p>
            <CreateIndividualApplicantPartyForm />
          </section>
        )}
      </main>
      <Footer />
    </>
  );
};

export default AccountPage;
