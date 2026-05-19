import { Link, useParams } from "react-router-dom";
import { useMyRequestDetailsQuery } from "../../../entities/request/model/useMyRequestDetailsQuery";
import { useSession } from "../../../entities/session/model/useSession";
import { MyRequestDetailsNotFound } from "../../../features/request/my-request-details/ui/MyRequestDetailsNotFound";
import { MyRequestDetailsView } from "../../../features/request/my-request-details/ui/MyRequestDetailsView";
import { myRequestDetailsConst } from "../../../features/request/my-request-details/ui/myRequestDetailsConst";
import { ApiError } from "../../../shared/api/fetchJson";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import "./myRequestDetailsPage.css";

const parseRequestId = (value: string | undefined) => {
  if (!value) {
    return null;
  }

  const requestId = Number(value);
  return Number.isInteger(requestId) && requestId > 0 ? requestId : null;
};

const MyRequestDetailsPage = () => {
  const session = useSession();
  const params = useParams();
  const requestId = parseRequestId(params.requestId);
  const requestDetailsQuery = useMyRequestDetailsQuery({
    requestId: requestId ?? 0,
    enabled: Boolean(session) && requestId !== null,
  });

  const isNotFound =
    requestId === null ||
    (requestDetailsQuery.error instanceof ApiError &&
      requestDetailsQuery.error.status === 404);

  return (
    <main className="content">
        <section
          className="myRequestDetailsPage clientPage"
          aria-labelledby="my-request-details-page-heading"
        >
          <h1 id="my-request-details-page-heading">
            {myRequestDetailsConst.pageTitle}
          </h1>

          {!session && (
            <div className="myRequestDetailsState">
              <h2>{myRequestDetailsConst.signInRequiredTitle}</h2>
              <p>{myRequestDetailsConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>{myRequestDetailsConst.signInLinkText}</Link>
            </div>
          )}

          {session && isNotFound && <MyRequestDetailsNotFound />}

          {session && !isNotFound && requestDetailsQuery.isPending && (
            <p className="myRequestDetailsState">
              {myRequestDetailsConst.loadingText}
            </p>
          )}

          {session && !isNotFound && requestDetailsQuery.isError && (
            <p className="myRequestDetailsState" role="alert">
              {myRequestDetailsConst.errorText}
            </p>
          )}

          {session && !isNotFound && requestDetailsQuery.data && (
            <MyRequestDetailsView request={requestDetailsQuery.data} />
          )}
        </section>
    </main>
  );
};

export default MyRequestDetailsPage;
