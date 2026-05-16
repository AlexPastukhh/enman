import type { MyRequestDetails } from "../../../../entities/request/model/requestTypes";
import {
  formatMyRequestDate,
  valueOrUnknown,
} from "./formatMyRequestDetails";
import { myRequestDetailsConst } from "./myRequestDetailsConst";

type MyRequestReviewResultProps = {
  request: MyRequestDetails;
};

export const MyRequestReviewResult = ({ request }: MyRequestReviewResultProps) => {
  const reviewResult = request.reviewResult;

  return (
    <section
      className="myRequestDetailsSection"
      aria-labelledby="my-request-review-result-heading"
    >
      <h2 id="my-request-review-result-heading">
        {myRequestDetailsConst.reviewResultTitle}
      </h2>
      {!reviewResult && <p>{myRequestDetailsConst.noReviewResultText}</p>}
      {reviewResult && (
        <dl className="myRequestDetailsDefinitionList">
          <div className="myRequestDetailsDefinitionList__row">
            <dt>{myRequestDetailsConst.decisionLabel}</dt>
            <dd>{valueOrUnknown(reviewResult.decision)}</dd>
          </div>
          <div className="myRequestDetailsDefinitionList__row">
            <dt>{myRequestDetailsConst.decidedAtLabel}</dt>
            <dd>{formatMyRequestDate(reviewResult.decidedAt)}</dd>
          </div>
          {reviewResult.rejection && (
            <div className="myRequestDetailsDefinitionList__row">
              <dt>{myRequestDetailsConst.rejectionReasonLabel}</dt>
              <dd>{valueOrUnknown(reviewResult.rejection.reason)}</dd>
            </div>
          )}
        </dl>
      )}
    </section>
  );
};
