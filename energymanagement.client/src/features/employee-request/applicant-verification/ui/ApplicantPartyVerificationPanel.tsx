import type { EmployeeRequestApplicantVerification } from "../../../../entities/employee-request/model/employeeRequestTypes";
import { ApplicantVerificationStatusBadge } from "../../../../entities/employee-request/ui/ApplicantVerificationStatusBadge";
import { RunApplicantPartyVerificationButton } from "./RunApplicantPartyVerificationButton";
import { applicantPartyVerificationConst } from "./applicantPartyVerificationConst";
import "./applicantPartyVerification.css";

type ApplicantPartyVerificationPanelProps = {
  requestId: number;
  verification: EmployeeRequestApplicantVerification;
};

export const ApplicantPartyVerificationPanel = ({
  requestId,
  verification,
}: ApplicantPartyVerificationPanelProps) => {
  const canRun = verification.canRun === true;

  return (
    <section
      className="applicantVerificationPanel"
      aria-labelledby="applicant-verification-panel-title"
    >
      <div className="applicantVerificationPanel__header">
        <h2 id="applicant-verification-panel-title">
          {applicantPartyVerificationConst.panelTitle}
        </h2>
        <ApplicantVerificationStatusBadge verification={verification} />
      </div>
      <p className="applicantVerificationPanel__description">
        {applicantPartyVerificationConst.panelDescription}
      </p>
      {verification.message && (
        <p className="applicantVerificationPanel__message">
          {verification.message}
        </p>
      )}
      {canRun && (
        <RunApplicantPartyVerificationButton
          requestId={requestId}
          surface="details"
        />
      )}
    </section>
  );
};
