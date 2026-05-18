import { Link, useNavigate } from "react-router-dom";
import { useAccountApplicantPartiesQuery } from "../../../entities/applicant-party/model/useAccountApplicantPartiesQuery";
import { useSession } from "../../../entities/session/model/useSession";
import { CreateConnectionRequestForm } from "../../../features/request/create-connection-request/ui/CreateConnectionRequestForm";
import { createConnectionRequestConst } from "../../../features/request/create-connection-request/ui/createConnectionRequestConst";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import "./createConnectionRequestPage.css";

const CreateConnectionRequestPage = () => {
  const session = useSession();
  const navigate = useNavigate();
  const accountApplicantPartiesQuery = useAccountApplicantPartiesQuery({
    enabled: Boolean(session),
  });
  const applicantParties =
    accountApplicantPartiesQuery.data?.applicantParties ?? [];

  const handleSuccess = () => {
    void navigate(clientRoutes.requests);
  };

  return (
    <main className="content">
        <section
          className="createConnectionRequestPage"
          aria-labelledby="create-connection-request-page-heading"
        >
          <h1 id="create-connection-request-page-heading">
            {createConnectionRequestConst.pageTitle}
          </h1>

          {!session && (
            <div className="createConnectionRequestPage__state">
              <h2>{createConnectionRequestConst.signInRequiredTitle}</h2>
              <p>{createConnectionRequestConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>
                {createConnectionRequestConst.signInLinkText}
              </Link>
            </div>
          )}

          {session && accountApplicantPartiesQuery.isPending && (
            <p className="createConnectionRequestPage__state">
              {createConnectionRequestConst.loadingApplicantPartiesText}
            </p>
          )}

          {session && accountApplicantPartiesQuery.isError && (
            <p className="createConnectionRequestPage__state" role="alert">
              {createConnectionRequestConst.applicantPartiesErrorText}
            </p>
          )}

          {session && accountApplicantPartiesQuery.data && (
            <CreateConnectionRequestForm
              applicantParties={applicantParties}
              onSuccess={handleSuccess}
            />
          )}
        </section>
    </main>
  );
};

export default CreateConnectionRequestPage;
