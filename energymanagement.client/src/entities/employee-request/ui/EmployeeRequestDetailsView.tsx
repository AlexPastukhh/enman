import type { ReactNode } from "react";
import type { EmployeeRequestDetails } from "../model/employeeRequestTypes";
import { EmployeeApplicantReviewData } from "./EmployeeApplicantReviewData";
import { EmployeeRequestStatusPanel } from "./EmployeeRequestStatusPanel";
import { EmployeeReviewActionAvailabilityPanel } from "./EmployeeReviewActionAvailabilityPanel";
import { EmployeeReviewStatePanel } from "./EmployeeReviewStatePanel";
import { employeeRequestDetailsConst } from "./employeeRequestDetailsConst";
import "./employeeRequestDetails.css";

type EmployeeRequestDetailsViewProps = {
  details: EmployeeRequestDetails;
  renderApplicantVerification?: (details: EmployeeRequestDetails) => ReactNode;
  renderReviewActions?: (details: EmployeeRequestDetails) => ReactNode;
  renderAgreementExchangeActions?: (details: EmployeeRequestDetails) => ReactNode;
};

export const EmployeeRequestDetailsView = ({
  details,
  renderApplicantVerification,
  renderReviewActions,
  renderAgreementExchangeActions,
}: EmployeeRequestDetailsViewProps) => {
  const title = `${employeeRequestDetailsConst.requestTitlePrefix} #${details.requestId}`;

  return (
    <article className="employeeRequestDetailsView" aria-labelledby="employee-request-details-title">
      <header className="employeeRequestDetailsView__header">
        <h2 id="employee-request-details-title">{title}</h2>
      </header>
      <EmployeeRequestStatusPanel details={details} />
      <EmployeeApplicantReviewData details={details} />
      {renderApplicantVerification?.(details)}
      <EmployeeReviewStatePanel details={details} />
      <EmployeeReviewActionAvailabilityPanel
        details={details}
        renderReviewActions={renderReviewActions}
        renderAgreementExchangeActions={renderAgreementExchangeActions}
      />
    </article>
  );
};
