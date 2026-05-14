export const demoRoles = ["Guest", "Client", "Employee"] as const;
export type DemoRole = (typeof demoRoles)[number];

export const accountStates = ["Active", "Not activated"] as const;
export type AccountState = (typeof accountStates)[number];

export const requestStatuses = ["InReview", "Approved", "Rejected"] as const;
export type RequestStatus = (typeof requestStatuses)[number];

export const agreementStatuses = [
  "AwaitingClientConfirmation",
  "SentByClient",
  "Accepted",
  "Rejected",
] as const;
export type AgreementStatus = (typeof agreementStatuses)[number];

export const agreementSenders = ["employee", "client"] as const;
export type AgreementSender = (typeof agreementSenders)[number];

export const uiStates = [
  "normal",
  "loading",
  "empty",
  "validationError",
  "serverRejection",
  "accessDenied",
  "notFound",
] as const;
export type UiState = (typeof uiStates)[number];

export const applicantTypes = [
  "physical person",
  "individual entrepreneur",
  "legal entity",
] as const;
export type ApplicantType = (typeof applicantTypes)[number];

export type ApplicantData = {
  type: ApplicantType;
  summary: string;
  fields: Record<string, string>;
  savedAt: string;
};

export type RequestDocument = {
  id: string;
  title: string;
  state: "accepted" | "missing" | "rejected";
  fileName?: string;
  note: string;
};

export type RequestRecord = {
  id: string;
  ownerId: string;
  number: string;
  status: RequestStatus;
  objectAddress: string;
  serviceSummary: string;
  createdAt: string;
  applicantSnapshot: string;
  reviewFeedback?: string;
  documents: RequestDocument[];
};

export type AgreementProposal = {
  id: string;
  relatedRequestId: string;
  status: AgreementStatus;
  sender: AgreementSender;
  version: string;
  documentName: string;
  comment: string;
  createdAt: string;
  clientOwnVersionSent: boolean;
};

export const demoClientId = "client-demo-001";
export const otherClientId = "client-other-999";

export const initialApplicantData: ApplicantData = {
  type: "physical person",
  summary: "Иванов Иван Иванович · physical person · saved applicant data",
  fields: {
    fullName: "Иванов Иван Иванович",
    email: "client@example.test",
    phone: "+7 900 100-20-30",
    passport: "4512 345678",
  },
  savedAt: "2026-05-10",
};

export const initialRequests: RequestRecord[] = [
  {
    id: "REQ-001",
    ownerId: demoClientId,
    number: "CR-2026-001",
    status: "InReview",
    objectAddress: "г. Москва, ул. Энергетиков, д. 14",
    serviceSummary: "Технологическое присоединение частного дома, 15 кВт",
    createdAt: "2026-05-12",
    applicantSnapshot: "Иванов Иван Иванович · physical person",
    documents: [
      {
        id: "DOC-001",
        title: "Заявление",
        state: "accepted",
        fileName: "request-form.pdf",
        note: "Accepted attachment reference only; no real upload persistence.",
      },
      {
        id: "DOC-002",
        title: "Документ на объект",
        state: "missing",
        note: "Demo missing document state.",
      },
    ],
  },
  {
    id: "REQ-002",
    ownerId: demoClientId,
    number: "CR-2026-002",
    status: "Approved",
    objectAddress: "г. Тверь, промзона Северная, участок 8",
    serviceSummary: "Электроснабжение производственного объекта, 150 кВт",
    createdAt: "2026-05-08",
    applicantSnapshot: "ООО «Северная линия» · legal entity",
    documents: [
      {
        id: "DOC-003",
        title: "Пакет документов",
        state: "accepted",
        fileName: "legal-entity-package.zip",
        note: "Accepted document reference for mock walkthrough.",
      },
    ],
  },
  {
    id: "REQ-003",
    ownerId: demoClientId,
    number: "CR-2026-003",
    status: "Rejected",
    objectAddress: "г. Коломна, ул. Сетевая, д. 3",
    serviceSummary: "Увеличение мощности до 30 кВт",
    createdAt: "2026-05-02",
    applicantSnapshot: "Петров Пётр Петрович · individual entrepreneur",
    reviewFeedback: "Не приложен документ, подтверждающий право использования объекта.",
    documents: [
      {
        id: "DOC-004",
        title: "Правоустанавливающий документ",
        state: "rejected",
        fileName: "ownership-scan.jpg",
        note: "Rejected file state for UI planning.",
      },
    ],
  },
  {
    id: "REQ-OTHER",
    ownerId: otherClientId,
    number: "CR-2026-999",
    status: "Approved",
    objectAddress: "Forbidden client address placeholder",
    serviceSummary: "Forbidden request must not disclose details to current client.",
    createdAt: "2026-04-20",
    applicantSnapshot: "Other client",
    documents: [],
  },
];

export const initialAgreements: AgreementProposal[] = [
  {
    id: "AGR-001",
    relatedRequestId: "REQ-002",
    status: "AwaitingClientConfirmation",
    sender: "employee",
    version: "Employee proposal v1",
    documentName: "agreement-proposal-v1.pdf",
    comment: "Please review the proposed connection terms.",
    createdAt: "2026-05-13",
    clientOwnVersionSent: false,
  },
  {
    id: "AGR-002",
    relatedRequestId: "REQ-002",
    status: "SentByClient",
    sender: "client",
    version: "Client comments v1",
    documentName: "client-version.docx",
    comment: "Client requests a corrected payment schedule.",
    createdAt: "2026-05-14",
    clientOwnVersionSent: true,
  },
  {
    id: "AGR-003",
    relatedRequestId: "REQ-002",
    status: "Accepted",
    sender: "employee",
    version: "Accepted proposal demo",
    documentName: "accepted-proposal.pdf",
    comment: "Shown as Accepted proposal only; not a Signed agreement.",
    createdAt: "2026-05-09",
    clientOwnVersionSent: false,
  },
];
