import { employeeRequestDetailsConst } from "./employeeRequestDetailsConst";
import "./employeeRequestDetails.css";

export const EmployeeRequestDetailsEmptyState = () => (
  <div className="employeeRequestDetailsState">
    <h2>{employeeRequestDetailsConst.notFoundTitle}</h2>
    <p>{employeeRequestDetailsConst.notFoundDescription}</p>
  </div>
);
