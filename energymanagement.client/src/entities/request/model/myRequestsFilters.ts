export const myRequestStatuses = ["InReview", "Approved", "Rejected"] as const;

export type MyRequestStatus = (typeof myRequestStatuses)[number];

export type MyRequestsFilters = {
  status?: MyRequestStatus;
};

export const isMyRequestStatus = (value: string): value is MyRequestStatus =>
  myRequestStatuses.includes(value as MyRequestStatus);

export const hasActiveMyRequestsFilters = (filters: MyRequestsFilters) =>
  Boolean(filters.status);
