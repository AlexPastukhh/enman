import { NavLink } from "react-router-dom";
import type { ApplicantPartySummary } from "../../entities/applicant-party/model/applicantPartyTypes";
import { useAccountApplicantPartiesQuery } from "../../entities/applicant-party/model/useAccountApplicantPartiesQuery";
import { ApplicantPartiesList } from "../../entities/applicant-party/ui/ApplicantPartiesList";
import { useSession } from "../../entities/session/model/useSession";
import { CreateIndividualApplicantPartyForm } from "../../features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm";
import { useMakeApplicantPartyCurrentDefaultMutation } from "../../features/applicant-party/make-current-default/model/useMakeApplicantPartyCurrentDefaultMutation";
import { MakeCurrentDefaultButton } from "../../features/applicant-party/make-current-default/ui/MakeCurrentDefaultButton";
import { makeCurrentDefaultButtonConst } from "../../features/applicant-party/make-current-default/ui/makeCurrentDefaultButtonConst";
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
          <section className="accountPage__signedOut pageCard" aria-labelledby="account-signed-out-heading">
            <p className="pageEyebrow">Личный кабинет</p>
            <h1 className="pageTitle" id="account-signed-out-heading">
              Войдите, чтобы управлять данными заявителя
            </h1>
            <p className="pageDescription">
              После входа здесь будут доступны сохранённые заявители,
              текущий/default заявитель и форма добавления данных.
            </p>
            <div className="accountPage__actions">
              <NavLink className="button-primary" to={clientRoutes.login}>
                Войти
              </NavLink>
              <NavLink className="button-hollow" to={clientRoutes.register}>
                Зарегистрироваться
              </NavLink>
            </div>
          </section>
        )}

        {session && (
          <section className="accountPage__panel pageCard" aria-labelledby="account-page-heading">
            <p className="pageEyebrow">Личный кабинет</p>
            <h1 id="account-page-heading" className="pageTitle">
              Данные аккаунта
            </h1>
            <p className="pageDescription">{session.email}</p>

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
