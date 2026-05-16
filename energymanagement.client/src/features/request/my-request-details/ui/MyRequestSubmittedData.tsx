import type { MyRequestDetails } from "../../../../entities/request/model/requestTypes";
import {
  formatMyRequestAddress,
  valueOrUnknown,
} from "./formatMyRequestDetails";
import { myRequestDetailsConst } from "./myRequestDetailsConst";

type MyRequestSubmittedDataProps = {
  request: MyRequestDetails;
};

export const MyRequestSubmittedData = ({ request }: MyRequestSubmittedDataProps) => (
  <section
    className="myRequestDetailsSection"
    aria-labelledby="my-request-submitted-data-heading"
  >
    <h2 id="my-request-submitted-data-heading">
      {myRequestDetailsConst.submittedDataTitle}
    </h2>
    <dl className="myRequestDetailsDefinitionList">
      <div className="myRequestDetailsDefinitionList__row">
        <dt>{myRequestDetailsConst.detailsLabel}</dt>
        <dd>{valueOrUnknown(request.submittedRequest?.details)}</dd>
      </div>
      <div className="myRequestDetailsDefinitionList__row">
        <dt>{myRequestDetailsConst.objectAddressLabel}</dt>
        <dd>{formatMyRequestAddress(request.submittedRequest?.objectAddress)}</dd>
      </div>
    </dl>
  </section>
);
