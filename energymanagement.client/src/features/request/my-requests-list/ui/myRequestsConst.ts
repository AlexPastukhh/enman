export const myRequestsConst = {
  pageTitle: "Мои заявки",
  createRequestLinkText: "Создать заявку",
  loadingText: "Загружаем заявки...",
  errorText: "Не удалось загрузить заявки.",
  invalidFiltersResetText: "Сбросить фильтры",
  signInRequiredTitle: "Войдите, чтобы увидеть свои заявки",
  signInRequiredDescription:
    "Список заявок доступен только авторизованному клиенту.",
  signInLinkText: "Войти",
  emptyTitle: "У вас пока нет заявок.",
  emptyDescription: "Созданные заявки появятся здесь.",
  filteredEmptyTitle: "Заявок с выбранным фильтром не найдено.",
  filteredEmptyDescription:
    "Измените или сбросьте фильтры, чтобы увидеть другие заявки.",
  filteredEmptyResetText: "Сбросить фильтры",
  requestTitlePrefix: "Заявка",
  requestTypeLabel: "Тип заявки",
  statusLabel: "Статус",
  createdAtLabel: "Дата создания",
  summaryLabel: "Описание",
  objectAddressLabel: "Адрес объекта",
  detailsLinkText: "Открыть детали",
  unknownValue: "—",
  statusLabels: {
    InReview: "На рассмотрении",
    Approved: "Одобрена",
    Rejected: "Отклонена",
    AgreementExchangeFailed: "Договорной обмен не завершён",
  },
  requestTypeLabels: {
    Connection: "Подключение",
  },
} as const;
