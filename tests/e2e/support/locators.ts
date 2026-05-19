import type { Page } from "@playwright/test";

export const L = {
  headings: {
    createRequest: /Создать заявку|Создание заявки|Create connection request/i,
    employeeDashboard: /Заявки|Employee Request Dashboard|Request Dashboard/i,
    employeeRequestDetails: /Детали заявки|Employee Request Details/i,
  },
  status: {
    inReview: /На рассмотрении|InReview/i,
  },
  applicantParties: {
    noSaved: /Сохранённых заявителей пока нет|Сохраненных заявителей пока нет|No saved Applicant Parties yet\.?/i,
    currentDefaultRegion: /Текущий|Current\/default templates|Текущие шаблоны/i,
    otherSavedRegion: /Other saved Applicant Parties|Сохранённые заявители|Другие сохранённые заявители/i,
    currentDefaultBadge: /Текущий|Current\/default|Current default/i,
  },
  agreementExchange: {
    // labels seen in UI: "Первичный документ предложения", "Документ предложения", or English fallbacks
    initialDocumentLabel: /Первичный документ предложения|Документ предложения|Initial proposal document/i,
    proposalDocumentLabel: /Документ предложения|Proposal document|Proposal document/i,
    commentLabel: /Комментарий|Comment|Initial proposal comment/i,
  },
  buttons: {
    createRequest: /Создать заявку|Create request/i,
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
