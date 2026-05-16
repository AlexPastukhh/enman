import { useState } from "react";
import { useCurrentIndividualApplicantPartyQuery } from "../../entities/applicant-party/model/useCurrentIndividualApplicantPartyQuery";
import { useSession } from "../../entities/session/model/useSession";
import { CreateIndividualApplicantPartyForm } from "../../features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm";
import { ApplicantPartyReadOnlyView } from "../../features/applicant-party/create-individual/ui/ApplicantPartyReadOnlyView";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";

const AccountPage = () => {
  const session = useSession();
  const [showCreateSuccessNotification, setShowCreateSuccessNotification] =
    useState(false);
  const currentApplicantQuery = useCurrentIndividualApplicantPartyQuery(
    Boolean(session),
  );

  return (
    <>
      <Header />
      <main className="content">
        {!session && <RegisterForm />}
        {session && (
          <section aria-labelledby="account-page-heading">
            <h1 id="account-page-heading">Account</h1>
            <p>{session.email}</p>
            {currentApplicantQuery.isPending && <p>Loading applicant data...</p>}
            {currentApplicantQuery.isError && (
              <p role="alert">Applicant data could not be loaded.</p>
            )}
            {currentApplicantQuery.data?.exists &&
              currentApplicantQuery.data.applicantParty && (
                <ApplicantPartyReadOnlyView
                  applicantParty={currentApplicantQuery.data.applicantParty}
                  showSuccessNotification={showCreateSuccessNotification}
                />
              )}
            {currentApplicantQuery.data?.exists === false && (
              <CreateIndividualApplicantPartyForm
                onSuccess={() => setShowCreateSuccessNotification(true)}
              />
            )}
          </section>
        )}
      </main>
      <Footer />
    </>
  );
};

export default AccountPage;
