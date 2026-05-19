import type { EmployeeRequestDetails } from "../model/employeeRequestTypes";
import { employeeRequestDetailsConst } from "./employeeRequestDetailsConst";
import {
  formatEmployeeApplicantType,
  valueOrUnknown,
} from "./formatEmployeeRequestDetails";

type EmployeeApplicantReviewDataProps = {
  details: EmployeeRequestDetails;
};

export const EmployeeApplicantReviewData = ({
  details,
}: EmployeeApplicantReviewDataProps) => {
  const applicant = details.applicant;

  return (
    <section
      className="employeeRequestDetailsPanel"
      aria-labelledby="employee-applicant-data-heading"
    >
      <h2 id="employee-applicant-data-heading">
        {employeeRequestDetailsConst.applicantTitle}
      </h2>
      <dl className="employeeRequestDetailsDefinitionList">
        <div>
          <dt>{employeeRequestDetailsConst.applicantNameLabel}</dt>
          <dd>{valueOrUnknown(applicant?.displayName)}</dd>
        </div>
        <div>
          <dt>{employeeRequestDetailsConst.applicantTypeLabel}</dt>
          <dd>{formatEmployeeApplicantType(applicant?.applicantPartyType)}</dd>
        </div>
        <div>
          <dt>{employeeRequestDetailsConst.applicantEmailLabel}</dt>
          <dd>{valueOrUnknown(applicant?.email)}</dd>
        </div>
        <div>
          <dt>{employeeRequestDetailsConst.applicantPhoneLabel}</dt>
          <dd>{valueOrUnknown(applicant?.phoneNumber)}</dd>
        </div>
      </dl>
    </section>
  );
};
