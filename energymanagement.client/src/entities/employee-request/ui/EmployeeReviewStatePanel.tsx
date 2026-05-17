import type { EmployeeRequestDetails } from "../model/employeeRequestTypes";
import { EmployeeReviewStateBadge } from "./EmployeeReviewStateBadge";
import { employeeRequestDetailsConst } from "./employeeRequestDetailsConst";

type EmployeeReviewStatePanelProps = {
  details: EmployeeRequestDetails;
};

export const EmployeeReviewStatePanel = ({
  details,
}: EmployeeReviewStatePanelProps) => (
  <section
    className="employeeRequestDetailsPanel"
    aria-labelledby="employee-review-state-heading"
  >
    <h2 id="employee-review-state-heading">
      {employeeRequestDetailsConst.reviewStateTitle}
    </h2>
    <EmployeeReviewStateBadge reviewState={details.reviewState} />
  </section>
);
