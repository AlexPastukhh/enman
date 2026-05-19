import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import type { EmployeeRequestDashboardItem } from "../model/employeeRequestTypes";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import { ApplicantVerificationStatusBadge } from "./ApplicantVerificationStatusBadge";
import { EmployeeReviewStateBadge } from "./EmployeeReviewStateBadge";
import { employeeRequestDashboardConst } from "./employeeRequestDashboardConst";
import {
  formatEmployeeRequestDate,
  formatEmployeeRequestStatus,
  formatEmployeeRequestType,
  valueOrUnknown,
} from "./formatEmployeeRequest";

type EmployeeRequestDashboardRowProps = {
  request: EmployeeRequestDashboardItem;
  renderActions?: (request: EmployeeRequestDashboardItem) => ReactNode;
};

export const EmployeeRequestDashboardRow = ({
  request,
  renderActions,
}: EmployeeRequestDashboardRowProps) => {
  const requestId = request.requestId;
  const title = `${employeeRequestDashboardConst.requestTitlePrefix} #${requestId}`;
  const titleId = `employee-request-${requestId}-title`;

  return (
    <article className="employeeRequestDashboardRow" aria-labelledby={titleId}>
      <header className="employeeRequestDashboardRow__header">
        <h2 id={titleId}>{title}</h2>
        <div className="employeeRequestDashboardRow__badges">
          {request.applicantVerification && (
            <ApplicantVerificationStatusBadge
              verification={request.applicantVerification}
            />
          )}
          <EmployeeReviewStateBadge reviewState={request.reviewState} />
        </div>
      </header>

      <dl className="employeeRequestDashboardRow__summary">
        <div className="employeeRequestDashboardRow__field">
          <dt>{employeeRequestDashboardConst.statusLabel}</dt>
          <dd>{formatEmployeeRequestStatus(request.status)}</dd>
        </div>
        <div className="employeeRequestDashboardRow__field">
          <dt>{employeeRequestDashboardConst.requestTypeLabel}</dt>
          <dd>{formatEmployeeRequestType(request.requestType)}</dd>
        </div>
        <div className="employeeRequestDashboardRow__field">
          <dt>{employeeRequestDashboardConst.createdAtLabel}</dt>
          <dd>{formatEmployeeRequestDate(request.createdAt)}</dd>
        </div>
        <div className="employeeRequestDashboardRow__field">
          <dt>{employeeRequestDashboardConst.applicantLabel}</dt>
          <dd>{valueOrUnknown(request.applicantDisplayName)}</dd>
        </div>
        <div className="employeeRequestDashboardRow__field">
          <dt>{employeeRequestDashboardConst.objectAddressLabel}</dt>
          <dd>{valueOrUnknown(request.objectAddress)}</dd>
        </div>
      </dl>

      <div className="employeeRequestDashboardRow__actions">
        <Link
          className="employeeRequestDashboardRow__detailsLink"
          to={clientRoutes.employeeRequestDetails(requestId)}
          aria-label={`${employeeRequestDashboardConst.detailsLinkText} ${title}`}
        >
          {employeeRequestDashboardConst.detailsLinkText}
        </Link>
        {renderActions?.(request)}
      </div>
    </article>
  );
};
