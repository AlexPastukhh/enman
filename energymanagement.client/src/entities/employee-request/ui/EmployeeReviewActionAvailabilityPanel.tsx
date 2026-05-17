import type { ReactNode } from "react";
import { getEmployeeReviewActionAvailability } from "../model/reviewActionAvailability";
import type { EmployeeRequestDetails } from "../model/employeeRequestTypes";
import { employeeRequestDetailsConst } from "./employeeRequestDetailsConst";

type EmployeeReviewActionAvailabilityPanelProps = {
  details: EmployeeRequestDetails;
  renderReviewActions?: (details: EmployeeRequestDetails) => ReactNode;
};

export const EmployeeReviewActionAvailabilityPanel = ({
  details,
  renderReviewActions,
}: EmployeeReviewActionAvailabilityPanelProps) => {
  const availability = getEmployeeReviewActionAvailability(details);

  return (
    <section
      className="employeeRequestDetailsPanel"
      aria-labelledby="employee-review-action-availability-heading"
    >
      <h2 id="employee-review-action-availability-heading">
        {employeeRequestDetailsConst.actionAvailabilityTitle}
      </h2>
      <ul className="employeeReviewActionAvailability">
        <li>
          {availability.canStartReview
            ? employeeRequestDetailsConst.startReviewAvailable
            : employeeRequestDetailsConst.startReviewBlocked}
        </li>
        <li>
          {availability.canApproveReview
            ? employeeRequestDetailsConst.approveAvailable
            : employeeRequestDetailsConst.approveBlocked}
        </li>
        <li>
          {availability.canRejectReview
            ? employeeRequestDetailsConst.rejectAvailable
            : employeeRequestDetailsConst.rejectBlocked}
        </li>
      </ul>
      {availability.reason && (
        <p className="employeeRequestDetailsPanel__hint">{availability.reason}</p>
      )}
      {renderReviewActions && (
        <div className="employeeRequestDetailsActions">
          {renderReviewActions(details)}
        </div>
      )}
    </section>
  );
};
