import type { EmployeeRequestDetails } from "../model/employeeRequestTypes";
import { employeeRequestDetailsConst } from "./employeeRequestDetailsConst";
import {
  formatEmployeeRequestDetailsStatus,
  formatEmployeeRequestDetailsType,
  formatEmployeeRequestDetailsDate,
  valueOrUnknown,
} from "./formatEmployeeRequestDetails";

type EmployeeRequestStatusPanelProps = {
  details: EmployeeRequestDetails;
};

export const EmployeeRequestStatusPanel = ({
  details,
}: EmployeeRequestStatusPanelProps) => (
  <section
    className="employeeRequestDetailsPanel"
    aria-labelledby="employee-request-data-heading"
  >
    <h2 id="employee-request-data-heading">
      {employeeRequestDetailsConst.requestDataTitle}
    </h2>
    <dl className="employeeRequestDetailsDefinitionList">
      <div>
        <dt>{employeeRequestDetailsConst.statusLabel}</dt>
        <dd>{formatEmployeeRequestDetailsStatus(details.status)}</dd>
      </div>
      <div>
        <dt>{employeeRequestDetailsConst.requestTypeLabel}</dt>
        <dd>{formatEmployeeRequestDetailsType(details.requestType)}</dd>
      </div>
      <div>
        <dt>{employeeRequestDetailsConst.createdAtLabel}</dt>
        <dd>{formatEmployeeRequestDetailsDate(details.createdAt)}</dd>
      </div>
      <div>
        <dt>{employeeRequestDetailsConst.objectAddressLabel}</dt>
        <dd>{valueOrUnknown(details.objectAddress)}</dd>
      </div>
      <div>
        <dt>{employeeRequestDetailsConst.detailsLabel}</dt>
        <dd>{valueOrUnknown(details.details)}</dd>
      </div>
    </dl>
  </section>
);
