import { useAccountApplicantPartiesQuery } from "../../entities/applicant-party/model/useAccountApplicantPartiesQuery";
import { ApplicantPartiesList } from "../../entities/applicant-party/ui/ApplicantPartiesList";
import { useSession } from "../../entities/session/model/useSession";
import { CreateIndividualApplicantPartyForm } from "../../features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";

const AccountPage = () => {
  const session = useSession();
  const accountApplicantPartiesQuery = useAccountApplicantPartiesQuery({
    enabled: Boolean(session),
  });

  const handleApplicantPartyCreated = () => {
    void accountApplicantPartiesQuery.refetch();
  };

  const applicantParties =
    accountApplicantPartiesQuery.data?.applicantParties ?? [];

  return (
    <>
      <Header />
      <main className="content">
        {!session && <RegisterForm />}
        {session && (
          <section aria-labelledby="account-page-heading">
            <h1 id="account-page-heading">Account</h1>
            <p>{session.email}</p>

            {accountApplicantPartiesQuery.isPending && (
              <p>Loading applicant parties...</p>
            )}
            {accountApplicantPartiesQuery.isError && (
              <p role="alert">Applicant parties could not be loaded.</p>
            )}
            {accountApplicantPartiesQuery.data && (
              <ApplicantPartiesList applicantParties={applicantParties} />
            )}

            <CreateIndividualApplicantPartyForm
              onSuccess={handleApplicantPartyCreated}
            />
          </section>
        )}
      </main>
      <Footer />
    </>
  );
};

export default AccountPage;
