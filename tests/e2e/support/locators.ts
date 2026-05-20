import type { Locator, Page } from "@playwright/test";

export const L = {
  headings: {
    createRequest: /Создание заявки на подключение|Заявка на подключение|Create connection request/i,
    myRequests: /Мои заявки|My Requests/i,
    requestDetails: /Детали заявки|Request Details/i,
    employeeDashboard: /Заявки на рассмотрение|Request Dashboard/i,
    employeeRequestDetails: /Рассмотрение заявки|Employee Request Details/i,
    employeeAgreementExchanges: /Согласование договоров|Employee Agreement Exchanges/i,
    clientAgreementExchanges: /Мои договоры|Client Agreement Exchanges/i,
  },
  status: {
    inReview: /На рассмотрении|InReview/i,
    approved: /Одобрена|Approved/i,
    rejected: /Отклонена|Rejected/i,
  },
  applicantParties: {
    noSaved: /Сохранённых заявителей пока нет|Сохраненных заявителей пока нет|No saved Applicant Parties yet\.?/i,
    currentDefaultRegion: /Текущий заявитель|Current\/default templates|Текущие шаблоны/i,
    otherSavedRegion: /Другие сохранённые заявители|Другие сохраненные заявители|Other saved Applicant Parties/i,
    currentDefaultBadge: /Текущий|Current\/default|Current default/i,
    unverifiedSummary: /Данные не проверены|Unverified/i,
    savedApplicantLabel: /Сохранённый заявитель|Сохраненный заявитель|Saved Applicant Party/i,
  },
  agreementExchange: {
    initialDocumentLabel: /Первичный документ предложения|Initial proposal document/i,
    proposalDocumentLabel: /Документ предложения|Proposal document/i,
    initialCommentLabel: /Комментарий к первичному предложению|Initial proposal comment/i,
    commentLabel: /Комментарий|Comment/i,
    openDetailsLink: /Открыть детали согласования|Open exchange details/i,
    downloadDocumentLink: /Скачать документ|Download document/i,
  },
  buttons: {
    createRequest: /Создать заявку|Create request/i,
    startReview: /Начать рассмотрение|Start review/i,
    approveReview: /Одобрить заявку|Approve review/i,
    startAgreementExchange: /Начать согласование договора|Start agreement exchange/i,
    sendProposalVersion: /Отправить версию предложения|Send proposal version/i,
  },
};

export function headingSelector(regex: RegExp, level = 2) {
  return { name: regex, level } as const;
}

export function statusTextLocator(page: Page, regex: RegExp) {
  return page.getByText(regex, { exact: true });
}

export default L;
export function escapeRegExp(value: string) {
  return value.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
}

export function exactTextIgnoreCase(value: string) {
  return new RegExp(`^${escapeRegExp(value)}$`, "i");
}

export function cardWithText(page: Page, text: string | RegExp) {
  return page.locator("article").filter({ hasText: text });
}

export function statusInCard(card: Locator, regex: RegExp) {
  return card.getByText(regex);
}
