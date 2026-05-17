import { Link, useSearchParams } from "react-router-dom";
import {
  agreementExchangeStatuses,
  hasActiveAgreementExchangeFilters,
  type AgreementExchangeListFilters,
} from "../../entities/agreement-exchange/model/agreementExchangeFilters";
import { useAgreementExchangeListQuery } from "../../entities/agreement-exchange/model/useAgreementExchangeListQuery";
import { AgreementExchangeList } from "../../entities/agreement-exchange/ui/AgreementExchangeList";
import { agreementExchangeListConst } from "../../entities/agreement-exchange/ui/agreementExchangeListConst";
import "../../entities/agreement-exchange/ui/agreementExchangeList.css";
import type { AgreementExchangeStatus } from "../../entities/agreement-exchange/api/agreementExchangeApiTypes";
import { useSession } from "../../entities/session/model/useSession";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { Footer } from "../../shared/ui/layout/Footer";
import { Header } from "../../shared/ui/layout/Header";
import "./agreementExchangesPage.css";

const parseFilters = (searchParams: URLSearchParams) => {
  const status = searchParams.get("status");

  if (!status) {
    return { filters: {} as AgreementExchangeListFilters, invalidFilterReason: null };
  }

  if (!agreementExchangeStatuses.includes(status as AgreementExchangeStatus)) {
    return {
      filters: {} as AgreementExchangeListFilters,
      invalidFilterReason: "Выбран неизвестный статус договорного обмена.",
    };
  }

  return {
    filters: { status: status as AgreementExchangeStatus },
    invalidFilterReason: null,
  };
};

const AgreementExchangesPage = () => {
  const session = useSession();
  const [searchParams, setSearchParams] = useSearchParams();
  const parsedFilters = parseFilters(searchParams);
  const filters = parsedFilters.filters;
  const hasInvalidFilters = Boolean(parsedFilters.invalidFilterReason);
  const hasActiveFilters = hasActiveAgreementExchangeFilters(filters);

  const exchangesQuery = useAgreementExchangeListQuery({
    filters,
    enabled: Boolean(session) && !hasInvalidFilters,
  });

  const handleStatusChange = (status: string) => {
    const next = new URLSearchParams(searchParams);
    if (status) {
      next.set("status", status);
    } else {
      next.delete("status");
    }
    setSearchParams(next);
  };

  const handleResetFilters = () => {
    setSearchParams(new URLSearchParams());
  };

  return (
    <>
      <Header />
      <main className="content">
        <section className="agreementExchangesPage" aria-labelledby="agreement-exchanges-heading">
          <h1 id="agreement-exchanges-heading">{agreementExchangeListConst.pageTitle}</h1>
          <p>{agreementExchangeListConst.pageDescription}</p>

          {!session && (
            <div className="agreementExchangesPage__state">
              <h2>{agreementExchangeListConst.signInRequiredTitle}</h2>
              <p>{agreementExchangeListConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>{agreementExchangeListConst.signInLinkText}</Link>
            </div>
          )}

          {session && (
            <div className="agreementExchangesPage__filters">
              <label htmlFor="agreement-exchange-status-filter">
                {agreementExchangeListConst.statusFilterLabel}
              </label>
              <select
                id="agreement-exchange-status-filter"
                value={filters.status ?? ""}
                onChange={(event) => handleStatusChange(event.target.value)}
              >
                <option value="">{agreementExchangeListConst.allStatusesOption}</option>
                {agreementExchangeStatuses.map((status) => (
                  <option key={status} value={status}>
                    {status}
                  </option>
                ))}
              </select>
              {hasActiveFilters && (
                <button type="button" onClick={handleResetFilters}>
                  {agreementExchangeListConst.resetFiltersText}
                </button>
              )}
            </div>
          )}

          {session && hasInvalidFilters && (
            <div className="agreementExchangesPage__state" role="alert">
              <p>{parsedFilters.invalidFilterReason}</p>
              <button type="button" onClick={handleResetFilters}>
                {agreementExchangeListConst.resetFiltersText}
              </button>
            </div>
          )}

          {session && !hasInvalidFilters && exchangesQuery.isPending && (
            <p className="agreementExchangesPage__state">
              {agreementExchangeListConst.loadingText}
            </p>
          )}

          {session && !hasInvalidFilters && exchangesQuery.isError && (
            <p className="agreementExchangesPage__state" role="alert">
              {agreementExchangeListConst.errorText}
            </p>
          )}

          {session && !hasInvalidFilters && exchangesQuery.data && (
            <AgreementExchangeList
              exchanges={exchangesQuery.data}
              emptyStateVariant={hasActiveFilters ? "filtered" : "default"}
              onResetFilters={handleResetFilters}
            />
          )}
        </section>
      </main>
      <Footer />
    </>
  );
};

export default AgreementExchangesPage;
