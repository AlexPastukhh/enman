import { Link, useSearchParams } from "react-router-dom";
import {
  hasActiveMyRequestsFilters,
  type MyRequestsFilters as MyRequestsFiltersState,
} from "../../../entities/request/model/myRequestsFilters";
import { useMyRequestsQuery } from "../../../entities/request/model/useMyRequestsQuery";
import { useSession } from "../../../entities/session/model/useSession";
import { MyRequestsFilters } from "../../../features/request/my-requests-filters/ui/MyRequestsFilters";
import { MyRequestsList } from "../../../features/request/my-requests-list/ui/MyRequestsList";
import { myRequestsConst } from "../../../features/request/my-requests-list/ui/myRequestsConst";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import {
  parseMyRequestsUrlFilters,
  serializeMyRequestsUrlFilters,
} from "./model/myRequestsUrlFilters";
import "./myRequestsPage.css";

const MyRequestsPage = () => {
  const session = useSession();
  const [searchParams, setSearchParams] = useSearchParams();
  const parsedFilters = parseMyRequestsUrlFilters(searchParams);
  const filters = parsedFilters.filters;
  const hasInvalidFilters = Boolean(parsedFilters.invalidFilterReason);
  const hasActiveFilters = hasActiveMyRequestsFilters(filters);

  const myRequestsQuery = useMyRequestsQuery({
    filters,
    enabled: Boolean(session) && !hasInvalidFilters,
  });

  const handleFiltersChange = (nextFilters: MyRequestsFiltersState) => {
    setSearchParams(serializeMyRequestsUrlFilters(nextFilters));
  };

  const handleResetFilters = () => {
    setSearchParams(new URLSearchParams());
  };

  return (
    <main className="content">
        <section className="myRequestsPage" aria-labelledby="my-requests-heading">
          <h1 id="my-requests-heading">{myRequestsConst.pageTitle}</h1>

          {session && (
            <Link to={clientRoutes.createRequest}>
              {myRequestsConst.createRequestLinkText}
            </Link>
          )}

          {!session && (
            <div className="myRequestsPage__state">
              <h2>{myRequestsConst.signInRequiredTitle}</h2>
              <p>{myRequestsConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>{myRequestsConst.signInLinkText}</Link>
            </div>
          )}

          {session && (
            <MyRequestsFilters
              filters={filters}
              onChange={handleFiltersChange}
              onReset={handleResetFilters}
            />
          )}

          {session && hasInvalidFilters && (
            <div className="myRequestsPage__state" role="alert">
              <p>{parsedFilters.invalidFilterReason}</p>
              <button type="button" onClick={handleResetFilters}>
                {myRequestsConst.invalidFiltersResetText}
              </button>
            </div>
          )}

          {session && !hasInvalidFilters && myRequestsQuery.isPending && (
            <p className="myRequestsPage__state">{myRequestsConst.loadingText}</p>
          )}

          {session && !hasInvalidFilters && myRequestsQuery.isError && (
            <p className="myRequestsPage__state" role="alert">
              {myRequestsConst.errorText}
            </p>
          )}

          {session && !hasInvalidFilters && myRequestsQuery.data && (
            <MyRequestsList
              requests={myRequestsQuery.data}
              emptyStateVariant={hasActiveFilters ? "filtered" : "default"}
              onResetFilters={handleResetFilters}
            />
          )}
        </section>
    </main>
  );
};

export default MyRequestsPage;
