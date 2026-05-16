import {
  isMyRequestStatus,
  type MyRequestsFilters,
} from "../../../../entities/request/model/myRequestsFilters";

export type ParsedMyRequestsFilters = {
  filters: MyRequestsFilters;
  invalidFilterReason?: string;
};

export const parseMyRequestsUrlFilters = (
  searchParams: URLSearchParams,
): ParsedMyRequestsFilters => {
  const status = searchParams.get("status");

  if (!status) {
    return { filters: {} };
  }

  if (!isMyRequestStatus(status)) {
    return {
      filters: {},
      invalidFilterReason: "Некорректный статус заявки.",
    };
  }

  return {
    filters: { status },
  };
};

export const serializeMyRequestsUrlFilters = (
  filters: MyRequestsFilters,
): URLSearchParams => {
  const next = new URLSearchParams();

  if (filters.status) {
    next.set("status", filters.status);
  }

  return next;
};
