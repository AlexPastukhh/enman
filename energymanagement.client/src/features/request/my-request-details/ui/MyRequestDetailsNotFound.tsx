import { Link } from "react-router-dom";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { myRequestDetailsConst } from "./myRequestDetailsConst";

export const MyRequestDetailsNotFound = () => (
  <section className="myRequestDetailsState" aria-labelledby="my-request-not-found-heading">
    <h2 id="my-request-not-found-heading">{myRequestDetailsConst.notFoundTitle}</h2>
    <p>{myRequestDetailsConst.notFoundDescription}</p>
    <Link to={clientRoutes.requests}>{myRequestDetailsConst.backToRequestsText}</Link>
  </section>
);
