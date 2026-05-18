import type { ApplicantPartySummary } from "../../entities/applicant-party/model/applicantPartyTypes";
import { useAccountApplicantPartiesQuery } from "../../entities/applicant-party/model/useAccountApplicantPartiesQuery";
import { ApplicantPartiesList } from "../../entities/applicant-party/ui/ApplicantPartiesList";
import { useSession } from "../../entities/session/model/useSession";
import { useMakeApplicantPartyCurrentDefaultMutation } from "../../features/applicant-party/make-current-default/model/useMakeApplicantPartyCurrentDefaultMutation";
import { MakeCurrentDefaultButton } from "../../features/applicant-party/make-current-default/ui/MakeCurrentDefaultButton";
import { makeCurrentDefaultButtonConst } from "../../features/applicant-party/make-current-default/ui/makeCurrentDefaultButtonConst";
import { CreateIndividualApplicantPartyForm } from "../../features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm";
import { Link } from "react-router-dom";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";

const getApplicantPartyId = (applicantParty: ApplicantPartySummary) => {
  const id = applicantParty.applicantPartyId;
  return typeof id === "number" ? id : null;
};

const AccountPage = () => {
  const session = useSession();
  const accountApplicantPartiesQuery = useAccountApplicantPartiesQuery({
    enabled: Boolean(session),
  });
  const makeCurrentDefaultMutation =
    useMakeApplicantPartyCurrentDefaultMutation();

  const handleApplicantPartyCreated = () => {
    void accountApplicantPartiesQuery.refetch();
  };

  const applicantParties =
    accountApplicantPartiesQuery.data?.applicantParties ?? [];

  const renderMakeCurrentDefaultAction = (
    applicantParty: ApplicantPartySummary,
    options: { highlighted: boolean },
  ) => {
    const applicantPartyId = getApplicantPartyId(applicantParty);

    if (options.highlighted || applicantPartyId === null) {
      return null;
    }

    const isActiveMutation =
      makeCurrentDefaultMutation.variables === applicantPartyId;
    const errorMessage =
      isActiveMutation && makeCurrentDefaultMutation.isError
        ? makeCurrentDefaultMutation.error instanceof Error
          ? makeCurrentDefaultMutation.error.message
          : makeCurrentDefaultButtonConst.errorMessage
        : null;

    return (
      <MakeCurrentDefaultButton
        isPending={isActiveMutation && makeCurrentDefaultMutation.isPending}
        errorMessage={errorMessage}
        onClick={() => makeCurrentDefaultMutation.mutate(applicantPartyId)}
      />
    );
  };

  return (
    <>
      <Header />
      <main className="content">
        {!session && (
          <section className="signedOutPanel" aria-labelledby="account-signed-out-heading">
            <h1 id="account-signed-out-heading">Личный кабинет</h1>
            <p>
              Войдите в существующий аккаунт или зарегистрируйтесь, чтобы работать
              с заявками, applicant parties и договорными обменами.
            </p>
            <div className="signedOutPanel__actions">
              <Link className="button-primary link-base-clear" to={clientRoutes.login}>
                Войти
              </Link>
              <Link className="button-hollow link-base-clear" to={clientRoutes.register}>
                Зарегистрироваться
              </Link>
            </div>
          </section>
        )}
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
              <ApplicantPartiesList
                applicantParties={applicantParties}
                renderApplicantPartyActions={renderMakeCurrentDefaultAction}
              />
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
