import { Link } from "react-router-dom";
import type { MyRequestSummary } from "../../../../entities/request/model/requestTypes";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { myRequestsConst } from "./myRequestsConst";

type MyRequestSummaryCardProps = {
  request: MyRequestSummary;
};

const formatDate = (value?: string) => {
  if (!value) {
    return myRequestsConst.unknownValue;
  }

  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return myRequestsConst.unknownValue;
  }

  return new Intl.DateTimeFormat("ru-RU", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
};

const formatAddress = (request: MyRequestSummary) => {
  const address = request.objectAddress;
  if (!address) {
    return myRequestsConst.unknownValue;
  }

  const parts = [
    address.postalCode,
    address.region,
    address.city,
    address.street,
    address.house,
    address.building,
    address.apartment,
  ].filter((part): part is string => Boolean(part?.trim()));

  return parts.length > 0 ? parts.join(", ") : myRequestsConst.unknownValue;
};

const valueOrUnknown = (value?: string | null) =>
  value?.trim() ? value : myRequestsConst.unknownValue;

export const MyRequestSummaryCard = ({ request }: MyRequestSummaryCardProps) => {
  const requestId = request.requestId;
  const titleId = `my-request-${requestId ?? "unknown"}-title`;
  const title = `${myRequestsConst.requestTitlePrefix} #${
    requestId ?? myRequestsConst.unknownValue
  }`;

  return (
    <article className="myRequestCard" aria-labelledby={titleId}>
      <h2 id={titleId} className="myRequestCard__title">
        {title}
      </h2>
      <dl className="myRequestCard__summary">
        <div className="myRequestCard__row">
          <dt>{myRequestsConst.statusLabel}</dt>
          <dd>{valueOrUnknown(request.status)}</dd>
        </div>
        <div className="myRequestCard__row">
          <dt>{myRequestsConst.requestTypeLabel}</dt>
          <dd>{valueOrUnknown(request.requestType)}</dd>
        </div>
        <div className="myRequestCard__row">
          <dt>{myRequestsConst.createdAtLabel}</dt>
          <dd>{formatDate(request.createdAt)}</dd>
        </div>
        <div className="myRequestCard__row">
          <dt>{myRequestsConst.summaryLabel}</dt>
          <dd>{valueOrUnknown(request.summary)}</dd>
        </div>
        <div className="myRequestCard__row">
          <dt>{myRequestsConst.objectAddressLabel}</dt>
          <dd>{formatAddress(request)}</dd>
        </div>
      </dl>
      {requestId && (
        <Link
          className="myRequestCard__detailsLink"
          to={clientRoutes.requestDetails(requestId)}
          aria-label={`${myRequestsConst.detailsLinkText} ${title}`}
        >
          {myRequestsConst.detailsLinkText}
        </Link>
      )}
    </article>
  );
};
