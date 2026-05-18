import type { EmployeeRequestApplicantVerification } from "../model/employeeRequestTypes";
import { applicantVerificationStatusConst } from "./applicantVerificationStatusConst";

type ApplicantVerificationStatusBadgeProps = {
  verification: EmployeeRequestApplicantVerification;
};

const getStatusLabel = (verification: EmployeeRequestApplicantVerification) => {
  if (verification.required === false || verification.status === "NotRequired") {
    return applicantVerificationStatusConst.notRequiredLabel;
  }

  if (verification.status === "Verified") {
    return applicantVerificationStatusConst.verifiedLabel;
  }

  return applicantVerificationStatusConst.unverifiedLabel;
};

const getStatusKind = (verification: EmployeeRequestApplicantVerification) => {
  if (verification.required === false || verification.status === "NotRequired") {
    return "not-required";
  }

  if (verification.status === "Verified") {
    return "verified";
  }

  return "unverified";
};

export const ApplicantVerificationStatusBadge = ({
  verification,
}: ApplicantVerificationStatusBadgeProps) => (
  <span
    className="applicantVerificationStatusBadge"
    data-status={getStatusKind(verification)}
  >
    {getStatusLabel(verification)}
  </span>
);
