import { Link, useParams } from "react-router-dom";
import { getEmployeeReviewActionAvailability } from "../../../../entities/employee-request/model/reviewActionAvailability";
import { useEmployeeRequestDetailsQuery } from "../../../../entities/employee-request/model/useEmployeeRequestDetailsQuery";
import { EmployeeRequestDetailsEmptyState } from "../../../../entities/employee-request/ui/EmployeeRequestDetailsEmptyState";
import { EmployeeRequestDetailsView } from "../../../../entities/employee-request/ui/EmployeeRequestDetailsView";
import { employeeRequestDetailsConst } from "../../../../entities/employee-request/ui/employeeRequestDetailsConst";
import { useSession } from "../../../../entities/session/model/useSession";
import { getStartAgreementExchangeAvailability } from "../../../../features/agreement-exchange/start-exchange/model/startAgreementExchangeAvailability";
import { StartAgreementExchangeForm } from "../../../../features/agreement-exchange/start-exchange/ui/StartAgreementExchangeForm";
import { ApproveReviewButton } from "../../../../features/employee-request/approve-review/ui/ApproveReviewButton";
import { RejectReviewForm } from "../../../../features/employee-request/reject-review/ui/RejectReviewForm";
import { StartReviewButton } from "../../../../features/employee-request/start-review/ui/StartReviewButton";
import { ApiError } from "../../../../shared/api/fetchJson";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { Footer } from "../../../../shared/ui/layout/Footer";
import { Header } from "../../../../shared/ui/layout/Header";
import "./employeeRequestDetailsPage.css";

const isEmployeeSession = (role?: string | null) => role === "Employee";

const parseRequestId = (value: string | undefined) => {
  if (!value) {
    return null;
  }

  const requestId = Number(value);
  return Number.isInteger(requestId) && requestId > 0 ? requestId : null;
};

const EmployeeRequestDetailsPage = () => {
  const session = useSession();
  const params = useParams();
  const requestId = parseRequestId(params.requestId);
  const isEmployee = isEmployeeSession(session?.role);

  const detailsQuery = useEmployeeRequestDetailsQuery({
    requestId: requestId ?? 0,
    enabled: Boolean(session) && isEmployee && requestId !== null,
  });

  const isNotFound =
    requestId === null ||
    (detailsQuery.error instanceof ApiError && detailsQuery.error.status === 404);

  const isAccessError =
    detailsQuery.error instanceof ApiError &&
    (detailsQuery.error.status === 401 || detailsQuery.error.status === 403);

  return (
    <>
      <Header />
      <main className="content">
        <section
          className="employeeRequestDetailsPage"
          aria-labelledby="employee-request-details-page-heading"
        >
          <Link to={clientRoutes.employeeRequests}>
            {employeeRequestDetailsConst.backToDashboardText}
          </Link>

          <h1 id="employee-request-details-page-heading">
            {employeeRequestDetailsConst.pageTitle}
          </h1>

          {!session && (
            <div className="employeeRequestDetailsPage__state">
              <h2>{employeeRequestDetailsConst.signInRequiredTitle}</h2>
              <p>{employeeRequestDetailsConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>
                {employeeRequestDetailsConst.signInLinkText}
              </Link>
            </div>
          )}

          {session && !isEmployee && (
            <div className="employeeRequestDetailsPage__state" role="alert">
              <h2>{employeeRequestDetailsConst.accessDeniedTitle}</h2>
              <p>{employeeRequestDetailsConst.accessDeniedDescription}</p>
            </div>
          )}

          {session && isEmployee && isNotFound && (
            <EmployeeRequestDetailsEmptyState />
          )}

          {session && isEmployee && !isNotFound && detailsQuery.isPending && (
            <p className="employeeRequestDetailsPage__state">
              {employeeRequestDetailsConst.loadingText}
            </p>
          )}

          {session && isEmployee && !isNotFound && isAccessError && (
            <div className="employeeRequestDetailsPage__state" role="alert">
              <h2>{employeeRequestDetailsConst.accessDeniedTitle}</h2>
              <p>{employeeRequestDetailsConst.accessDeniedDescription}</p>
            </div>
          )}

          {session &&
            isEmployee &&
            !isNotFound &&
            !isAccessError &&
            detailsQuery.isError && (
              <p className="employeeRequestDetailsPage__state" role="alert">
                {employeeRequestDetailsConst.errorText}
              </p>
            )}

          {session && isEmployee && !isNotFound && detailsQuery.data && (
            <EmployeeRequestDetailsView
              details={detailsQuery.data}
              renderReviewActions={(details) => {
                const availability = getEmployeeReviewActionAvailability(details);
                const startExchangeAvailability =
                  getStartAgreementExchangeAvailability(details);

                return (
                  <>
                    <StartReviewButton
                      requestId={details.requestId}
                      disabled={!availability.canStartReview}
                      unavailableReason={availability.reason}
                      surface="details"
                    />
                    <ApproveReviewButton
                      requestId={details.requestId}
                      disabled={!availability.canApproveReview}
                      unavailableReason={availability.reason}
                    />
                    <RejectReviewForm
                      requestId={details.requestId}
                      disabled={!availability.canRejectReview}
                      unavailableReason={availability.reason}
                      showEmptyFeedbackWarning
                    />
                    <StartAgreementExchangeForm
                      requestId={details.requestId}
                      disabled={!startExchangeAvailability.canStartAgreementExchange}
                      unavailableReason={startExchangeAvailability.reason}
                    />
                  </>
                );
              }}
            />
          )}
        </section>
      </main>
      <Footer />
    </>
  );
};

export default EmployeeRequestDetailsPage;
