import type { MyRequestStatus } from "../../../../entities/request/model/myRequestsFilters";

export const myRequestsFiltersConst = {
  title: "Фильтры",
  statusLabel: "Статус",
  allStatusesLabel: "Все статусы",
  resetButtonText: "Сбросить фильтры",
  statusOptions: {
    InReview: "На рассмотрении",
    Approved: "Одобрено",
    Rejected: "Отклонено",
  } satisfies Record<MyRequestStatus, string>,
} as const;
