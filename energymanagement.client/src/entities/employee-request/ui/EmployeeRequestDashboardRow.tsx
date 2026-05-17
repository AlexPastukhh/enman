import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import type { EmployeeRequestDashboardItem } from "../model/employeeRequestTypes";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import { EmployeeReviewStateBadge } from "./EmployeeReviewStateBadge";
import { employeeRequestDashboardConst } from "./employeeRequestDashboardConst";
import {
  formatEmployeeRequestDate,
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
        <EmployeeReviewStateBadge reviewState={request.reviewState} />
      </header>

      <dl className="employeeRequestDashboardRow__summary">
        <div className="employeeRequestDashboardRow__field">
          <dt>{employeeRequestDashboardConst.statusLabel}</dt>
          <dd>{valueOrUnknown(request.status)}</dd>
        </div>
        <div className="employeeRequestDashboardRow__field">
          <dt>{employeeRequestDashboardConst.requestTypeLabel}</dt>
          <dd>{valueOrUnknown(request.requestType)}</dd>
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
