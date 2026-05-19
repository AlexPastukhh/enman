import { Link } from "react-router-dom";
import type { MyRequestDetails } from "../../../../entities/request/model/requestTypes";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import {
  formatMyRequestDate,
  formatMyRequestStatus,
  formatMyRequestType,
} from "./formatMyRequestDetails";
import { myRequestDetailsConst } from "./myRequestDetailsConst";
import { MyRequestReviewResult } from "./MyRequestReviewResult";
import { MyRequestSubmittedData } from "./MyRequestSubmittedData";

type MyRequestDetailsViewProps = {
  request: MyRequestDetails;
};

export const MyRequestDetailsView = ({ request }: MyRequestDetailsViewProps) => {
  const title = `${myRequestDetailsConst.requestTitlePrefix} #${
    request.requestId ?? myRequestDetailsConst.unknownValue
  }`;

  return (
    <article className="myRequestDetailsView" aria-labelledby="my-request-details-title">
      <header className="myRequestDetailsView__header">
        <h2 id="my-request-details-title">{title}</h2>
        <Link to={clientRoutes.requests}>{myRequestDetailsConst.backToRequestsText}</Link>
      </header>

      <section
        className="myRequestDetailsSection"
        aria-labelledby="my-request-metadata-heading"
      >
        <h3 id="my-request-metadata-heading">{myRequestDetailsConst.metadataTitle}</h3>
        <dl className="myRequestDetailsDefinitionList">
          <div className="myRequestDetailsDefinitionList__row">
            <dt>{myRequestDetailsConst.statusLabel}</dt>
            <dd>{formatMyRequestStatus(request.status)}</dd>
          </div>
          <div className="myRequestDetailsDefinitionList__row">
            <dt>{myRequestDetailsConst.requestTypeLabel}</dt>
            <dd>{formatMyRequestType(request.requestType)}</dd>
          </div>
          <div className="myRequestDetailsDefinitionList__row">
            <dt>{myRequestDetailsConst.createdAtLabel}</dt>
            <dd>{formatMyRequestDate(request.createdAt)}</dd>
          </div>
        </dl>
      </section>

      <MyRequestSubmittedData request={request} />
      <MyRequestReviewResult request={request} />
    </article>
  );
};
