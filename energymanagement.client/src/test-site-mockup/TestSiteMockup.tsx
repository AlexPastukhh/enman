import { useMemo, useState, type ChangeEvent } from "react";
import {
  accountStates,
  agreementSenders,
  agreementStatuses,
  applicantTypes,
  demoClientId,
  demoRoles,
  initialAgreements,
  initialApplicantData,
  initialRequests,
  requestStatuses,
  uiStates,
  type AccountState,
  type AgreementProposal,
  type AgreementSender,
  type AgreementStatus,
  type ApplicantData,
  type ApplicantType,
  type DemoRole,
  type RequestRecord,
  type RequestStatus,
  type UiState,
} from "./mockData";
import { mockApi, type MockApiResult } from "./mockApi";
import "./mockupStyles.css";

type PageId =
  | "guest-registration"
  | "guest-login"
  | "guest-password-recovery"
  | "guest-reset-choice"
  | "guest-anonymous-request"
  | "client-create-request"
  | "client-applicant-data"
  | "client-my-requests"
  | "client-request-details"
  | "client-request-documents"
  | "client-my-agreements"
  | "client-agreement-details"
  | "employee-dashboard"
  | "employee-request-details"
  | "employee-request-review"
  | "employee-agreements"
  | "employee-agreement-create"
  | "system-not-authenticated"
  | "system-access-denied"
  | "system-activation-required"
  | "system-not-found";

type PageLink = {
  id: PageId;
  label: string;
  role: DemoRole | "System";
};

type ApiMessage = {
  kind: MockApiResult<unknown>["kind"] | "info" | "loading";
  message: string;
};

const pageLinks: PageLink[] = [
  { id: "guest-registration", label: "Registration", role: "Guest" },
  { id: "guest-login", label: "Login", role: "Guest" },
  { id: "guest-password-recovery", label: "Password Recovery", role: "Guest" },
  { id: "guest-reset-choice", label: "Owner Verified / Reset Choice", role: "Guest" },
  { id: "guest-anonymous-request", label: "Anonymous Request future", role: "Guest" },
  { id: "client-create-request", label: "Create Request", role: "Client" },
  { id: "client-applicant-data", label: "Applicant Data", role: "Client" },
  { id: "client-my-requests", label: "My Requests", role: "Client" },
  { id: "client-request-details", label: "Own Request Details", role: "Client" },
  { id: "client-request-documents", label: "Request Documents", role: "Client" },
  { id: "client-my-agreements", label: "My Agreements", role: "Client" },
  { id: "client-agreement-details", label: "Agreement Details / Response", role: "Client" },
  { id: "employee-dashboard", label: "Request Dashboard", role: "Employee" },
  { id: "employee-request-details", label: "Request Details", role: "Employee" },
  { id: "employee-request-review", label: "Request Review", role: "Employee" },
  { id: "employee-agreements", label: "Employee Agreements", role: "Employee" },
  { id: "employee-agreement-create", label: "Create / Send Proposal", role: "Employee" },
  { id: "system-not-authenticated", label: "Not authenticated", role: "System" },
  { id: "system-access-denied", label: "Access denied", role: "System" },
  { id: "system-activation-required", label: "Activation required", role: "System" },
  { id: "system-not-found", label: "Not found", role: "System" },
];

const roleDefaultPage: Record<DemoRole, PageId> = {
  Guest: "guest-registration",
  Client: "client-my-requests",
  Employee: "employee-dashboard",
};


const statusDescriptions: Record<RequestStatus, string> = {
  InReview: "Under review by employee. Review action is available only for this status.",
  Approved: "Approved request. Agreement proposal is not auto-created; employee sends it separately.",
  Rejected: "Rejected request. Feedback is visible and the client may start a new request path.",
};

const agreementDescriptions: Record<AgreementStatus, string> = {
  AwaitingClientConfirmation: "Employee-sent proposal awaits client confirmation or one client own version.",
  SentByClient: "Client own version has been sent. Core mockup blocks a second own version.",
  Accepted: "Accepted proposal. This mockup does not call it Signed or legally finalized.",
  Rejected: "Rejected/inactive proposal. Response actions are unavailable.",
};

const makeNewRequest = (count: number, applicant: ApplicantData): RequestRecord => ({
  id: `REQ-DEMO-${count}`,
  ownerId: demoClientId,
  number: `CR-2026-DEMO-${count}`,
  status: "InReview",
  objectAddress: "Demo address from Create Request form",
  serviceSummary: "Demo connection request created from isolated mockup",
  createdAt: "2026-05-14",
  applicantSnapshot: `${applicant.summary} · request-local copy`,
  documents: [
    {
      id: `DOC-DEMO-${count}`,
      title: "Draft attachment reference",
      state: "accepted",
      fileName: "selected-demo-document.pdf",
      note: "Created as accepted reference only. No file upload persistence is implemented.",
    },
  ],
});

const getPageTitle = (pageId: PageId) => pageLinks.find((page) => page.id === pageId)?.label ?? pageId;

const isProtectedPage = (pageId: PageId) => {
  const role = pageLinks.find((page) => page.id === pageId)?.role;
  return role === "Client" || role === "Employee";
};

const isRolePage = (pageId: PageId, role: DemoRole) => pageLinks.find((page) => page.id === pageId)?.role === role;

const pageRole = (pageId: PageId) => pageLinks.find((page) => page.id === pageId)?.role;

const statusClass = (status: RequestStatus | AgreementStatus) => `test-ui__badge test-ui__badge--${status.toLowerCase()}`;

const mapResultToMessage = <T,>(result: MockApiResult<T>): ApiMessage => ({
  kind: result.kind,
  message: result.message,
});

const PageCard = ({ title, children, note }: { title: string; children: React.ReactNode; note?: string }) => (
  <section className="test-ui__card">
    <div className="test-ui__card-header">
      <h2>{title}</h2>
      {note ? <span className="test-ui__note">{note}</span> : null}
    </div>
    {children}
  </section>
);

const Field = ({ label, children }: { label: string; children: React.ReactNode }) => (
  <label className="test-ui__field">
    <span>{label}</span>
    {children}
  </label>
);

const EmptyState = ({ title, actionLabel, onAction }: { title: string; actionLabel?: string; onAction?: () => void }) => (
  <div className="test-ui__state test-ui__state--empty">
    <strong>{title}</strong>
    <p>No records are rendered for this demo state.</p>
    {actionLabel && onAction ? (
      <button className="button-hollow" type="button" onClick={onAction}>
        {actionLabel}
      </button>
    ) : null}
  </div>
);

const SecurityPage = ({ title, reason, onSafeBack }: { title: string; reason: string; onSafeBack: () => void }) => (
  <PageCard title={title} note="System/security page">
    <div className="test-ui__state test-ui__state--security">
      <strong>{reason}</strong>
      <p>Forbidden data is not displayed. Use safe navigation to return to an allowed demo area.</p>
      <button className="button-primary" type="button" onClick={onSafeBack}>
        Go to safe demo page
      </button>
    </div>
  </PageCard>
);

const StateOverride = ({ uiState, onSafeBack }: { uiState: UiState; onSafeBack: () => void }) => {
  if (uiState === "loading") {
    return (
      <PageCard title="Loading state" note="Mock API delay state">
        <div className="test-ui__skeleton" />
        <div className="test-ui__skeleton test-ui__skeleton--short" />
        <p>Loading is simulated locally. No backend request is sent.</p>
      </PageCard>
    );
  }

  if (uiState === "empty") {
    return <EmptyState title="Empty state selected from demo controls" onAction={onSafeBack} actionLabel="Return to normal page" />;
  }

  if (uiState === "accessDenied") {
    return <SecurityPage title="Access denied" reason="The selected mock UI state blocks this resource." onSafeBack={onSafeBack} />;
  }

  if (uiState === "notFound") {
    return <SecurityPage title="Not found" reason="The selected mock UI state hides the requested resource." onSafeBack={onSafeBack} />;
  }

  return null;
};

const MessageBanner = ({ apiMessage }: { apiMessage: ApiMessage | null }) => {
  if (!apiMessage) {
    return null;
  }

  return <div className={`test-ui__message test-ui__message--${apiMessage.kind}`}>{apiMessage.message}</div>;
};

export default function TestSiteMockup() {
  const [currentRole, setCurrentRole] = useState<DemoRole>("Guest");
  const [accountState, setAccountState] = useState<AccountState>("Active");
  const [currentPage, setCurrentPage] = useState<PageId>("guest-registration");
  const [requestStatus, setRequestStatus] = useState<RequestStatus>("InReview");
  const [agreementStatus, setAgreementStatus] = useState<AgreementStatus>("AwaitingClientConfirmation");
  const [agreementSender, setAgreementSender] = useState<AgreementSender>("employee");
  const [uiState, setUiState] = useState<UiState>("normal");
  const [requests, setRequests] = useState<RequestRecord[]>(initialRequests);
  const [agreements, setAgreements] = useState<AgreementProposal[]>(initialAgreements);
  const [applicantData, setApplicantData] = useState<ApplicantData>(initialApplicantData);
  const [applicantType, setApplicantType] = useState<ApplicantType>(initialApplicantData.type);
  const [selectedRequestId, setSelectedRequestId] = useState("REQ-001");
  const [selectedAgreementId, setSelectedAgreementId] = useState("AGR-001");
  const [selectedFile, setSelectedFile] = useState("proposal-demo.pdf");
  const [acceptedAttachments, setAcceptedAttachments] = useState<string[]>(["request-form.pdf"]);
  const [requestFilter, setRequestFilter] = useState<RequestStatus | "All">("All");
  const [agreementStatusFilter, setAgreementStatusFilter] = useState<AgreementStatus | "All">("All");
  const [agreementSenderFilter, setAgreementSenderFilter] = useState<AgreementSender | "All">("All");
  const [reviewDecision, setReviewDecision] = useState<RequestStatus>("Approved");
  const [reviewFeedback, setReviewFeedback] = useState("Missing ownership document.");
  const [localAddress, setLocalAddress] = useState("г. Москва, ул. Энергетиков, д. 14");
  const [localComment, setLocalComment] = useState("Please review this demo version.");
  const [apiMessage, setApiMessage] = useState<ApiMessage | null>(null);
  const [isActionLoading, setIsActionLoading] = useState(false);

  const accountActivated = accountState === "Active";

  const rolePages = useMemo(
    () => pageLinks.filter((page) => page.role === currentRole || page.role === "System"),
    [currentRole],
  );

  const visibleRequests = useMemo(
    () => requests.filter((request) => request.ownerId === demoClientId),
    [requests],
  );

  const controlledRequests = useMemo(
    () =>
      visibleRequests.map((request, index) => (index === 0 ? { ...request, status: requestStatus } : request)),
    [requestStatus, visibleRequests],
  );

  const focusedRequest = controlledRequests.find((request) => request.id === selectedRequestId) ?? controlledRequests[0];
  const employeeFocusedRequest = requests.find((request) => request.id === selectedRequestId) ?? requests[0];
  const controlledAgreements = useMemo(
    () =>
      agreements.map((agreement, index) =>
        index === 0 ? { ...agreement, status: agreementStatus, sender: agreementSender } : agreement,
      ),
    [agreementSender, agreementStatus, agreements],
  );
  const focusedAgreement = controlledAgreements.find((agreement) => agreement.id === selectedAgreementId) ?? controlledAgreements[0];

  const changeRole = (role: DemoRole) => {
    setCurrentRole(role);
    setCurrentPage(roleDefaultPage[role]);
    setApiMessage({ kind: "info", message: `Demo role switched to ${role}.` });
  };

  const runAction = async <T,>(action: () => Promise<MockApiResult<T>>, onSuccess: (data: T) => void) => {
    setIsActionLoading(true);
    setApiMessage({ kind: "loading", message: "Mock API call is running locally..." });
    const result = await action();
    setIsActionLoading(false);
    setApiMessage(mapResultToMessage(result));
    if (result.kind === "success") {
      onSuccess(result.data);
    }
  };

  const showPage = (pageId: PageId) => {
    setCurrentPage(pageId);
    setApiMessage(null);
  };

  const saveApplicant = () => {
    const nextApplicant: ApplicantData = {
      type: applicantType,
      summary: `${applicantType} applicant · saved standalone demo data`,
      fields: {
        ...applicantData.fields,
        fullName: applicantData.fields.fullName ?? "Demo applicant",
        organization: applicantType === "legal entity" ? "ООО Demo Grid" : "",
        inn: applicantType !== "physical person" ? "7700000000" : "",
      },
      savedAt: "2026-05-14",
    };

    void runAction(() => mockApi.saveApplicantData(uiState, nextApplicant), (data) => {
      setApplicantData(data);
    });
  };

  const submitRequest = () => {
    const nextRequest = makeNewRequest(requests.length + 1, applicantData);
    nextRequest.objectAddress = localAddress || nextRequest.objectAddress;
    void runAction(() => mockApi.createRequest(uiState, nextRequest), (data) => {
      setRequests((items) => [data, ...items]);
      setSelectedRequestId(data.id);
      setRequestStatus("InReview");
      setCurrentPage("client-my-requests");
    });
  };

  const attachFileReference = () => {
    if (!selectedFile) {
      setApiMessage({ kind: "validationError", message: "Select a file name/reference before accepting attachment." });
      return;
    }
    setAcceptedAttachments((items) => (items.includes(selectedFile) ? items : [selectedFile, ...items]));
    setApiMessage({ kind: "success", message: "Selected file is accepted as a mock attachment reference only." });
  };

  const submitReview = () => {
    const target = employeeFocusedRequest;
    if (!target || target.status !== "InReview") {
      setApiMessage({ kind: "serverRejection", message: "Review is allowed only for InReview requests." });
      return;
    }

    const reviewedRequest: RequestRecord = {
      ...target,
      status: reviewDecision,
      reviewFeedback: reviewDecision === "Rejected" ? reviewFeedback : undefined,
    };

    void runAction(() => mockApi.submitReview(uiState, reviewedRequest), (data) => {
      setRequests((items) => items.map((request) => (request.id === data.id ? data : request)));
      setRequestStatus(data.status);
      setSelectedRequestId(data.id);
      setCurrentPage("employee-request-details");
    });
  };

  const createEmployeeAgreement = () => {
    const approvedRequest = requests.find((request) => request.status === "Approved") ?? focusedRequest;
    if (!approvedRequest || approvedRequest.status !== "Approved") {
      setApiMessage({ kind: "serverRejection", message: "Employee can send a proposal only for an Approved request." });
      return;
    }

    const nextAgreement: AgreementProposal = {
      id: `AGR-DEMO-${agreements.length + 1}`,
      relatedRequestId: approvedRequest.id,
      status: "AwaitingClientConfirmation",
      sender: "employee",
      version: "Employee proposal demo version",
      documentName: selectedFile || "agreement-proposal-demo.pdf",
      comment: localComment,
      createdAt: "2026-05-14",
      clientOwnVersionSent: false,
    };

    void runAction(() => mockApi.sendAgreementVersion(uiState, nextAgreement), (data) => {
      setAgreements((items) => [data, ...items.map((item) => (item.status === "SentByClient" ? { ...item, status: "Rejected" as AgreementStatus } : item))]);
      setSelectedAgreementId(data.id);
      setAgreementStatus(data.status);
      setAgreementSender(data.sender);
      setCurrentPage("employee-agreements");
    });
  };

  const acceptAgreement = () => {
    if (!focusedAgreement || focusedAgreement.status !== "AwaitingClientConfirmation" || focusedAgreement.sender !== "employee") {
      setApiMessage({ kind: "serverRejection", message: "Accept action is available only for active employee-sent proposal." });
      return;
    }

    const accepted: AgreementProposal = { ...focusedAgreement, status: "Accepted" };
    void runAction(() => mockApi.acceptAgreement(uiState, accepted), (data) => {
      setAgreements((items) => items.map((agreement) => (agreement.id === data.id ? data : agreement)));
      setAgreementStatus("Accepted");
    });
  };

  const sendClientVersion = () => {
    if (!focusedAgreement || focusedAgreement.status !== "AwaitingClientConfirmation" || focusedAgreement.sender !== "employee") {
      setApiMessage({ kind: "serverRejection", message: "Client can start own version only after employee-sent proposal." });
      return;
    }

    if (focusedAgreement.clientOwnVersionSent || agreements.some((agreement) => agreement.relatedRequestId === focusedAgreement.relatedRequestId && agreement.sender === "client")) {
      setApiMessage({ kind: "serverRejection", message: "Core mockup limit: client can send only one own version." });
      return;
    }

    const clientVersion: AgreementProposal = {
      id: `AGR-CLIENT-${agreements.length + 1}`,
      relatedRequestId: focusedAgreement.relatedRequestId,
      status: "SentByClient",
      sender: "client",
      version: "Client own version demo",
      documentName: selectedFile || "client-own-version.pdf",
      comment: localComment,
      createdAt: "2026-05-14",
      clientOwnVersionSent: true,
    };

    void runAction(() => mockApi.sendAgreementVersion(uiState, clientVersion), (data) => {
      setAgreements((items) => [data, ...items.map((agreement) => (agreement.id === focusedAgreement.id ? { ...agreement, clientOwnVersionSent: true } : agreement))]);
      setSelectedAgreementId(data.id);
      setAgreementStatus(data.status);
      setAgreementSender(data.sender);
    });
  };

  const updateApplicantField = (field: string, value: string) => {
    setApplicantData((data) => ({ ...data, fields: { ...data.fields, [field]: value } }));
  };

  const renderGuestPage = () => {
    if (currentPage === "guest-registration") {
      return (
        <PageCard title="Registration" note="SC-01 · creates Active demo account">
          <div className="test-ui__grid test-ui__grid--two">
            <Field label="Email">
              <input className="formControl" defaultValue="client@example.test" />
            </Field>
            <Field label="Password">
              <input className="formControl" defaultValue="Demo-password-1!" type="password" />
            </Field>
          </div>
          <p>Registration creates an Active account in the current core mock flow.</p>
          <button
            className="button-primary"
            type="button"
            disabled={isActionLoading}
            onClick={() => {
              setAccountState("Active");
              changeRole("Client");
              setCurrentPage("client-create-request");
              setApiMessage({ kind: "success", message: "Registration completed. Demo account is Active." });
            }}
          >
            Register and continue as Client
          </button>
        </PageCard>
      );
    }

    if (currentPage === "guest-login") {
      return (
        <PageCard title="Login" note="SC-02 · fake auth state only">
          <div className="test-ui__grid test-ui__grid--two">
            <Field label="Email">
              <input className="formControl" defaultValue="client@example.test" />
            </Field>
            <Field label="Password">
              <input className="formControl" defaultValue="Demo-password-1!" type="password" />
            </Field>
          </div>
          <p>Non-active login behavior is provisional/open; protected pages are blocked when account is Not activated.</p>
          <button
            className="button-primary"
            type="button"
            onClick={() => {
              changeRole("Client");
              setCurrentPage(accountActivated ? "client-my-requests" : "system-activation-required");
            }}
          >
            Login in demo
          </button>
        </PageCard>
      );
    }

    if (currentPage === "guest-password-recovery") {
      return (
        <PageCard title="Password Recovery Request" note="SC-03A">
          <Field label="Registered email">
            <input className="formControl" defaultValue="client@example.test" />
          </Field>
          <button className="button-primary" type="button" onClick={() => showPage("guest-reset-choice")}>
            Send recovery request
          </button>
        </PageCard>
      );
    }

    if (currentPage === "guest-reset-choice") {
      return (
        <PageCard title="Account Owner Verified / Reset Password Choice" note="SC-03B">
          <p>Account owner verification is shown as a demo checkpoint. No real reset token is generated.</p>
          <div className="test-ui__actions">
            <button className="button-primary" type="button">
              Choose new password
            </button>
            <button className="button-hollow" type="button" onClick={() => showPage("guest-login")}>
              Back to login
            </button>
          </div>
        </PageCard>
      );
    }

    return (
      <PageCard title="Anonymous Request" note="Future / extension">
        <p>This screen is represented only as a future extension because core behavior is not finalized in the UI planning files.</p>
        <Field label="Contact email">
          <input className="formControl" defaultValue="anonymous@example.test" />
        </Field>
        <Field label="Short request text">
          <textarea className="formControl" defaultValue="Future anonymous request text." />
        </Field>
      </PageCard>
    );
  };

  const renderClientPage = () => {
    if (currentPage === "client-create-request") {
      return (
        <PageCard title="Create Request" note="SC-04 · request-local applicant copy">
          <div className="test-ui__callout">
            Saved applicant prefill: <strong>{applicantData.summary}</strong>. Editing this request-local data does not mutate saved Applicant Data.
          </div>
          <div className="test-ui__grid test-ui__grid--two">
            <Field label="Request details">
              <textarea className="formControl" defaultValue="Connection request details for diploma UI scenario." />
            </Field>
            <Field label="Object address">
              <input className="formControl" value={localAddress} onChange={(event: ChangeEvent<HTMLInputElement>) => setLocalAddress(event.target.value)} />
            </Field>
            <Field label="Applicant copy name">
              <input
                className="formControl"
                value={applicantData.fields.fullName ?? ""}
                onChange={(event: ChangeEvent<HTMLInputElement>) => updateApplicantField("fullName", event.target.value)}
              />
            </Field>
            <Field label="Request-local phone">
              <input className="formControl" defaultValue={applicantData.fields.phone} />
            </Field>
          </div>
          {uiState === "validationError" ? <div className="test-ui__message test-ui__message--validationError">Address and applicant fields are shown as invalid in this demo state.</div> : null}
          <div className="test-ui__actions">
            <button className="button-primary" type="button" disabled={isActionLoading} onClick={submitRequest}>
              Submit request as InReview
            </button>
            <button className="button-hollow" type="button" onClick={() => showPage("client-applicant-data")}>
              Edit saved Applicant Data
            </button>
          </div>
        </PageCard>
      );
    }

    if (currentPage === "client-applicant-data") {
      return (
        <PageCard title="Applicant Data" note="SC-10 · standalone save">
          <div className="test-ui__callout">Standalone save does not start verification and does not create a request.</div>
          <Field label="Applicant type">
            <select className="formControl" value={applicantType} onChange={(event: ChangeEvent<HTMLSelectElement>) => setApplicantType(event.target.value as ApplicantType)}>
              {applicantTypes.map((type) => (
                <option key={type} value={type}>
                  {type}
                </option>
              ))}
            </select>
          </Field>
          <div className="test-ui__grid test-ui__grid--two">
            <Field label={applicantType === "legal entity" ? "Organization name" : "Full name"}>
              <input
                className="formControl"
                value={applicantType === "legal entity" ? applicantData.fields.organization ?? "ООО Demo Grid" : applicantData.fields.fullName ?? ""}
                onChange={(event: ChangeEvent<HTMLInputElement>) => updateApplicantField(applicantType === "legal entity" ? "organization" : "fullName", event.target.value)}
              />
            </Field>
            <Field label={applicantType === "physical person" ? "Passport" : "INN"}>
              <input
                className="formControl"
                value={applicantType === "physical person" ? applicantData.fields.passport ?? "" : applicantData.fields.inn ?? "7700000000"}
                onChange={(event: ChangeEvent<HTMLInputElement>) => updateApplicantField(applicantType === "physical person" ? "passport" : "inn", event.target.value)}
              />
            </Field>
          </div>
          <p className="test-ui__muted">Saved summary: {applicantData.summary}. Saved at: {applicantData.savedAt}.</p>
          <button className="button-primary" type="button" disabled={isActionLoading} onClick={saveApplicant}>
            Save Applicant Data only
          </button>
        </PageCard>
      );
    }

    if (currentPage === "client-my-requests") {
      const filtered = requestFilter === "All" ? controlledRequests : controlledRequests.filter((request) => request.status === requestFilter);
      return (
        <PageCard title="My Requests" note="SC-05">
          <div className="test-ui__toolbar">
            <Field label="Status filter">
              <select className="formControl" value={requestFilter} onChange={(event: ChangeEvent<HTMLSelectElement>) => setRequestFilter(event.target.value as RequestStatus | "All")}>
                <option value="All">All</option>
                {requestStatuses.map((status) => (
                  <option key={status} value={status}>
                    {status}
                  </option>
                ))}
              </select>
            </Field>
            <button className="button-primary" type="button" onClick={() => showPage("client-create-request")}>
              Create request
            </button>
          </div>
          {filtered.length === 0 ? (
            <EmptyState title="No own requests yet" actionLabel="Create request" onAction={() => showPage("client-create-request")} />
          ) : (
            <div className="test-ui__list">
              {filtered.map((request) => (
                <article className="test-ui__list-item" key={request.id}>
                  <div>
                    <strong>{request.number}</strong>
                    <p>{request.objectAddress}</p>
                    <p className="test-ui__muted">{request.serviceSummary}</p>
                  </div>
                  <span className={statusClass(request.status)}>{request.status}</span>
                  <button
                    className="button-hollow"
                    type="button"
                    onClick={() => {
                      setSelectedRequestId(request.id);
                      showPage("client-request-details");
                    }}
                  >
                    Open details
                  </button>
                </article>
              ))}
            </div>
          )}
        </PageCard>
      );
    }

    if (currentPage === "client-request-details") {
      if (!focusedRequest) {
        return <SecurityPage title="Not found" reason="No own request is selected." onSafeBack={() => showPage("client-my-requests")} />;
      }
      return (
        <PageCard title="Own Request Details" note="SC-05 · no access to other client's request">
          <div className="test-ui__summary-row">
            <span className={statusClass(focusedRequest.status)}>{focusedRequest.status}</span>
            <strong>{focusedRequest.number}</strong>
            <span>{focusedRequest.objectAddress}</span>
          </div>
          <p>{statusDescriptions[focusedRequest.status]}</p>
          <p className="test-ui__muted">Applicant snapshot: {focusedRequest.applicantSnapshot}</p>
          {focusedRequest.status === "Approved" ? (
            <div className="test-ui__callout">
              Approved request entry is shown together with a path to My Agreements. This follows Q:UI-001 preference E only for test walkthrough.
              <button className="button-hollow" type="button" onClick={() => showPage("client-my-agreements")}>
                Open My Agreements
              </button>
            </div>
          ) : null}
          {focusedRequest.status === "Rejected" ? (
            <div className="test-ui__message test-ui__message--serverRejection">
              Rejection feedback: {focusedRequest.reviewFeedback ?? "Demo rejection feedback."}
              <button className="button-hollow" type="button" onClick={() => showPage("client-create-request")}>
                Create new request
              </button>
            </div>
          ) : null}
          <div className="test-ui__actions">
            <button className="button-hollow" type="button" onClick={() => showPage("client-request-documents")}>
              View documents
            </button>
            <button className="button-hollow" type="button" onClick={() => showPage("system-access-denied")}>
              Try forbidden request
            </button>
          </div>
        </PageCard>
      );
    }

    if (currentPage === "client-request-documents") {
      return (
        <PageCard title="Request Documents" note="SC-11 · no real file upload">
          <div className="test-ui__grid test-ui__grid--two">
            <Field label="Selected file reference">
              <input className="formControl" value={selectedFile} onChange={(event: ChangeEvent<HTMLInputElement>) => setSelectedFile(event.target.value)} />
            </Field>
            <Field label="Use native picker for name only">
              <input
                className="formControl"
                type="file"
                onChange={(event: ChangeEvent<HTMLInputElement>) => setSelectedFile(event.currentTarget.files?.[0]?.name ?? selectedFile)}
              />
            </Field>
          </div>
          <button className="button-primary" type="button" onClick={attachFileReference}>
            Accept selected file reference
          </button>
          <div className="test-ui__list">
            {(focusedRequest?.documents ?? []).map((document) => (
              <article className="test-ui__list-item" key={document.id}>
                <strong>{document.title}</strong>
                <span className={`test-ui__badge test-ui__badge--${document.state}`}>{document.state}</span>
                <span>{document.fileName ?? "No file accepted"}</span>
                <p className="test-ui__muted">{document.note}</p>
              </article>
            ))}
            {acceptedAttachments.map((attachment) => (
              <article className="test-ui__list-item" key={attachment}>
                <strong>Accepted attachment reference</strong>
                <span className="test-ui__badge test-ui__badge--accepted">accepted</span>
                <span>{attachment}</span>
              </article>
            ))}
          </div>
        </PageCard>
      );
    }

    if (currentPage === "client-my-agreements") {
      const filtered = controlledAgreements.filter((agreement) => {
        const statusMatch = agreementStatusFilter === "All" || agreement.status === agreementStatusFilter;
        const senderMatch = agreementSenderFilter === "All" || agreement.sender === agreementSenderFilter;
        return statusMatch && senderMatch;
      });

      return (
        <PageCard title="My Agreements" note="SC-13A">
          <div className="test-ui__toolbar">
            <Field label="Status">
              <select className="formControl" value={agreementStatusFilter} onChange={(event: ChangeEvent<HTMLSelectElement>) => setAgreementStatusFilter(event.target.value as AgreementStatus | "All")}>
                <option value="All">All</option>
                {agreementStatuses.map((status) => (
                  <option key={status} value={status}>
                    {status}
                  </option>
                ))}
              </select>
            </Field>
            <Field label="Sender">
              <select className="formControl" value={agreementSenderFilter} onChange={(event: ChangeEvent<HTMLSelectElement>) => setAgreementSenderFilter(event.target.value as AgreementSender | "All")}>
                <option value="All">All</option>
                {agreementSenders.map((sender) => (
                  <option key={sender} value={sender}>
                    {sender}
                  </option>
                ))}
              </select>
            </Field>
          </div>
          {filtered.length === 0 ? (
            <EmptyState title="No agreement proposals yet" />
          ) : (
            <div className="test-ui__list">
              {filtered.map((agreement) => (
                <article className="test-ui__list-item" key={agreement.id}>
                  <div>
                    <strong>{agreement.version}</strong>
                    <p>Related Approved request: {agreement.relatedRequestId}</p>
                  </div>
                  <span className={statusClass(agreement.status)}>{agreement.status}</span>
                  <span className="test-ui__badge">{agreement.sender}</span>
                  <button
                    className="button-hollow"
                    type="button"
                    onClick={() => {
                      setSelectedAgreementId(agreement.id);
                      showPage("client-agreement-details");
                    }}
                  >
                    Open proposal
                  </button>
                </article>
              ))}
            </div>
          )}
        </PageCard>
      );
    }

    return renderAgreementDetails(false);
  };

  const renderAgreementDetails = (employeeMode: boolean) => {
    if (!focusedAgreement) {
      return <SecurityPage title="Agreement not found" reason="No proposal is selected." onSafeBack={() => showPage(employeeMode ? "employee-agreements" : "client-my-agreements")} />;
    }

    const clientCanRespond = focusedAgreement.status === "AwaitingClientConfirmation" && focusedAgreement.sender === "employee";
    return (
      <PageCard title={employeeMode ? "Employee Proposal Details" : "Agreement Proposal Details / Response"} note="SC-13B">
        <div className="test-ui__summary-row">
          <span className={statusClass(focusedAgreement.status)}>{focusedAgreement.status}</span>
          <span className="test-ui__badge">sender: {focusedAgreement.sender}</span>
          <strong>{focusedAgreement.version}</strong>
        </div>
        <p>{agreementDescriptions[focusedAgreement.status]}</p>
        <p>Related Approved request: {focusedAgreement.relatedRequestId}</p>
        <p>Proposal document/file reference: {focusedAgreement.documentName}</p>
        <p className="test-ui__muted">Comment: {focusedAgreement.comment}</p>
        <div className="test-ui__callout">
          Q:UI-003 is open: accepted proposal is shown as Accepted proposal only. Signed status is intentionally not introduced.
        </div>
        {!employeeMode ? (
          <div className="test-ui__grid test-ui__grid--two">
            <Field label="Own document reference">
              <input className="formControl" value={selectedFile} onChange={(event: ChangeEvent<HTMLInputElement>) => setSelectedFile(event.target.value)} />
            </Field>
            <Field label="Own text details/comment">
              <textarea className="formControl" value={localComment} onChange={(event: ChangeEvent<HTMLTextAreaElement>) => setLocalComment(event.target.value)} />
            </Field>
          </div>
        ) : null}
        <div className="test-ui__actions">
          {!employeeMode && clientCanRespond ? (
            <>
              <button className="button-primary" type="button" disabled={isActionLoading} onClick={acceptAgreement}>
                Accept proposal
              </button>
              <button className="button-hollow" type="button" disabled={isActionLoading} onClick={sendClientVersion}>
                Send one own version
              </button>
            </>
          ) : (
            <span className="test-ui__muted">Response actions are unavailable for this sender/status or inactive proposal.</span>
          )}
          {employeeMode && focusedAgreement.sender === "client" ? (
            <button className="button-primary" type="button" onClick={() => showPage("employee-agreement-create")}>
              Send new employee version
            </button>
          ) : null}
        </div>
      </PageCard>
    );
  };

  const renderEmployeePage = () => {
    if (currentPage === "employee-dashboard") {
      const filtered = requestFilter === "All" ? requests : requests.filter((request) => request.status === requestFilter);
      return (
        <PageCard title="Employee Request Dashboard" note="SC-06">
          <div className="test-ui__toolbar">
            <Field label="Request status filter">
              <select className="formControl" value={requestFilter} onChange={(event: ChangeEvent<HTMLSelectElement>) => setRequestFilter(event.target.value as RequestStatus | "All")}>
                <option value="All">All</option>
                {requestStatuses.map((status) => (
                  <option key={status} value={status}>
                    {status}
                  </option>
                ))}
              </select>
            </Field>
          </div>
          <div className="test-ui__list">
            {filtered.map((request) => (
              <article className="test-ui__list-item" key={request.id}>
                <div>
                  <strong>{request.number}</strong>
                  <p>{request.objectAddress}</p>
                  <p className="test-ui__muted">{request.applicantSnapshot}</p>
                </div>
                <span className={statusClass(request.status)}>{request.status}</span>
                <button
                  className="button-hollow"
                  type="button"
                  onClick={() => {
                    setSelectedRequestId(request.id);
                    showPage("employee-request-details");
                  }}
                >
                  Open details
                </button>
                {request.status === "InReview" ? (
                  <button
                    className="button-primary"
                    type="button"
                    onClick={() => {
                      setSelectedRequestId(request.id);
                      showPage("employee-request-review");
                    }}
                  >
                    Review
                  </button>
                ) : (
                  <span className="test-ui__muted">Review hidden/disabled for processed request (Q:UI-002 provisional).</span>
                )}
              </article>
            ))}
          </div>
        </PageCard>
      );
    }

    if (currentPage === "employee-request-details") {
      if (!employeeFocusedRequest) {
        return <SecurityPage title="Not found" reason="No employee request is selected." onSafeBack={() => showPage("employee-dashboard")} />;
      }
      return (
        <PageCard title="Employee Request Details" note="SC-07A · read-only for processed statuses">
          <div className="test-ui__summary-row">
            <span className={statusClass(employeeFocusedRequest.status)}>{employeeFocusedRequest.status}</span>
            <strong>{employeeFocusedRequest.number}</strong>
            <span>{employeeFocusedRequest.objectAddress}</span>
          </div>
          <p>{employeeFocusedRequest.serviceSummary}</p>
          <p className="test-ui__muted">Applicant data: {employeeFocusedRequest.applicantSnapshot}</p>
          <div className="test-ui__list">
            {employeeFocusedRequest.documents.map((document) => (
              <article className="test-ui__list-item" key={document.id}>
                <strong>{document.title}</strong>
                <span className={`test-ui__badge test-ui__badge--${document.state}`}>{document.state}</span>
                <span>{document.fileName ?? "Missing"}</span>
              </article>
            ))}
          </div>
          <div className="test-ui__actions">
            {employeeFocusedRequest.status === "InReview" ? (
              <button className="button-primary" type="button" onClick={() => showPage("employee-request-review")}>
                Review request
              </button>
            ) : (
              <span className="test-ui__muted">Review actions are read-only/unavailable for Approved or Rejected requests.</span>
            )}
            {employeeFocusedRequest.status === "Approved" ? (
              <button className="button-primary" type="button" onClick={() => showPage("employee-agreement-create")}>
                Create agreement proposal separately
              </button>
            ) : null}
          </div>
        </PageCard>
      );
    }

    if (currentPage === "employee-request-review") {
      return (
        <PageCard title="Employee Request Review" note="SC-07B · no proposal auto-creation">
          <p>Review is valid only when the selected request is InReview.</p>
          <div className="test-ui__summary-row">
            <strong>{employeeFocusedRequest?.number ?? "No request"}</strong>
            <span className={statusClass(employeeFocusedRequest?.status ?? "InReview")}>{employeeFocusedRequest?.status ?? "InReview"}</span>
          </div>
          <Field label="Review decision">
            <select className="formControl" value={reviewDecision} onChange={(event: ChangeEvent<HTMLSelectElement>) => setReviewDecision(event.target.value as RequestStatus)}>
              <option value="Approved">Approved</option>
              <option value="Rejected">Rejected</option>
            </select>
          </Field>
          {reviewDecision === "Rejected" ? (
            <Field label="Rejection feedback">
              <textarea className="formControl" value={reviewFeedback} onChange={(event: ChangeEvent<HTMLTextAreaElement>) => setReviewFeedback(event.target.value)} />
            </Field>
          ) : null}
          <div className="test-ui__callout">After approval, employee starts proposal creation from a separate action.</div>
          <button className="button-primary" type="button" disabled={isActionLoading} onClick={submitReview}>
            Submit review
          </button>
        </PageCard>
      );
    }

    if (currentPage === "employee-agreements") {
      return (
        <PageCard title="Employee Agreements" note="SC-13C">
          <div className="test-ui__list">
            {controlledAgreements.map((agreement) => (
              <article className="test-ui__list-item" key={agreement.id}>
                <div>
                  <strong>{agreement.version}</strong>
                  <p>Related request: {agreement.relatedRequestId}</p>
                </div>
                <span className={statusClass(agreement.status)}>{agreement.status}</span>
                <span className="test-ui__badge">{agreement.sender}</span>
                <button
                  className="button-hollow"
                  type="button"
                  onClick={() => {
                    setSelectedAgreementId(agreement.id);
                    showPage("client-agreement-details");
                  }}
                >
                  Open proposal details
                </button>
                {agreement.sender === "client" ? (
                  <button
                    className="button-primary"
                    type="button"
                    onClick={() => {
                      setSelectedAgreementId(agreement.id);
                      showPage("employee-agreement-create");
                    }}
                  >
                    Send new version
                  </button>
                ) : null}
              </article>
            ))}
          </div>
        </PageCard>
      );
    }

    if (currentPage === "employee-agreement-create") {
      const approvedRequests = requests.filter((request) => request.status === "Approved");
      return (
        <PageCard title="Employee Agreement Proposal Create / Send Version" note="SC-13D">
          <Field label="Selected Approved request">
            <select className="formControl" value={selectedRequestId} onChange={(event: ChangeEvent<HTMLSelectElement>) => setSelectedRequestId(event.target.value)}>
              {approvedRequests.map((request) => (
                <option key={request.id} value={request.id}>
                  {request.number} · {request.objectAddress}
                </option>
              ))}
            </select>
          </Field>
          <div className="test-ui__grid test-ui__grid--two">
            <Field label="Agreement document/file reference">
              <input className="formControl" value={selectedFile} onChange={(event: ChangeEvent<HTMLInputElement>) => setSelectedFile(event.target.value)} />
            </Field>
            <Field label="Text details/comment">
              <textarea className="formControl" value={localComment} onChange={(event: ChangeEvent<HTMLTextAreaElement>) => setLocalComment(event.target.value)} />
            </Field>
          </div>
          <div className="test-ui__callout">When employee sends a new version in response to a client version, the previous client version becomes Rejected in mock flow.</div>
          <button className="button-primary" type="button" disabled={isActionLoading} onClick={createEmployeeAgreement}>
            Send employee proposal/version
          </button>
        </PageCard>
      );
    }

    return renderAgreementDetails(true);
  };

  const renderSystemPage = () => {
    if (currentPage === "system-not-authenticated") {
      return <SecurityPage title="Not authenticated" reason="This page requires a logged-in actor in the demo shell." onSafeBack={() => changeRole("Guest")} />;
    }
    if (currentPage === "system-access-denied") {
      return <SecurityPage title="Access denied" reason="The current actor is not allowed to view this protected resource." onSafeBack={() => showPage(roleDefaultPage[currentRole])} />;
    }
    if (currentPage === "system-activation-required") {
      return (
        <PageCard title="Account activation required" note="Provisional/open behavior">
          <div className="test-ui__state test-ui__state--security">
            <strong>Protected pages are blocked because account state is Not activated.</strong>
            <p>Current core: registration creates Active account. Future non-active login behavior is not finalized.</p>
            <button className="button-primary" type="button" onClick={() => setAccountState("Active")}>
              Activate in demo controls
            </button>
          </div>
        </PageCard>
      );
    }
    return <SecurityPage title="Not found" reason="The requested page/resource is not available in this mockup." onSafeBack={() => showPage(roleDefaultPage[currentRole])} />;
  };

  const renderActivePage = () => {
    const forcedState = StateOverride({ uiState, onSafeBack: () => setUiState("normal") });
    if (forcedState) {
      return forcedState;
    }

    if (isProtectedPage(currentPage) && !accountActivated) {
      return renderSystemPageForActivation();
    }

    if (pageRole(currentPage) === "System") {
      return renderSystemPage();
    }

    if (!isRolePage(currentPage, currentRole)) {
      return <SecurityPage title="Not authenticated" reason="Selected page does not match the current demo role." onSafeBack={() => showPage(roleDefaultPage[currentRole])} />;
    }

    if (currentRole === "Guest") {
      return renderGuestPage();
    }

    if (currentRole === "Client") {
      return renderClientPage();
    }

    return renderEmployeePage();
  };

  const renderSystemPageForActivation = () => (
    <PageCard title="Account activation required" note="Protected page guard">
      <div className="test-ui__state test-ui__state--security">
        <strong>{getPageTitle(currentPage)} is blocked.</strong>
        <p>Mockup demonstrates that protected Client/Employee pages require an activated account.</p>
        <p>Non-active login behavior remains provisional/open and is not treated as a final business rule.</p>
        <button className="button-primary" type="button" onClick={() => setAccountState("Active")}>
          Switch account to Active
        </button>
      </div>
    </PageCard>
  );

  return (
    <main className="content test-ui">
        <section className="test-ui__hero">
          <div>
            <span className="test-ui__eyebrow">Low-fidelity isolated test-site mockup</span>
            <h1>Energy Management UI scenario walkthrough</h1>
            <p>
              This route is a standalone demo module for diploma UI scenarios. It uses mock data, fake auth state, and local mockApi only.
            </p>
          </div>
          <div className="test-ui__hero-card">
            <strong>Current page</strong>
            <span>{getPageTitle(currentPage)}</span>
            <span className="test-ui__badge">/test-ui</span>
          </div>
        </section>

        <section className="test-ui__shell">
          <aside className="test-ui__sidebar">
            <PageCard title="Demo shell">
              <Field label="Role">
                <div className="test-ui__segmented">
                  {demoRoles.map((role) => (
                    <button
                      key={role}
                      className={role === currentRole ? "test-ui__segment test-ui__segment--active" : "test-ui__segment"}
                      type="button"
                      onClick={() => changeRole(role)}
                    >
                      {role}
                    </button>
                  ))}
                </div>
              </Field>
              <Field label="Account state">
                <select className="formControl" value={accountState} onChange={(event: ChangeEvent<HTMLSelectElement>) => setAccountState(event.target.value as AccountState)}>
                  {accountStates.map((state) => (
                    <option key={state} value={state}>
                      {state}
                    </option>
                  ))}
                </select>
              </Field>
              <p className="test-ui__muted">Fake/demo authorization only. No real auth hooks or backend calls are used by this mockup.</p>
            </PageCard>

            <PageCard title="Demo state controls">
              <Field label="Request status">
                <select className="formControl" value={requestStatus} onChange={(event: ChangeEvent<HTMLSelectElement>) => setRequestStatus(event.target.value as RequestStatus)}>
                  {requestStatuses.map((status) => (
                    <option key={status} value={status}>
                      {status}
                    </option>
                  ))}
                </select>
              </Field>
              <Field label="Agreement proposal status">
                <select className="formControl" value={agreementStatus} onChange={(event: ChangeEvent<HTMLSelectElement>) => setAgreementStatus(event.target.value as AgreementStatus)}>
                  {agreementStatuses.map((status) => (
                    <option key={status} value={status}>
                      {status}
                    </option>
                  ))}
                </select>
              </Field>
              <Field label="Agreement sender">
                <select className="formControl" value={agreementSender} onChange={(event: ChangeEvent<HTMLSelectElement>) => setAgreementSender(event.target.value as AgreementSender)}>
                  {agreementSenders.map((sender) => (
                    <option key={sender} value={sender}>
                      {sender}
                    </option>
                  ))}
                </select>
              </Field>
              <Field label="UI state">
                <select className="formControl" value={uiState} onChange={(event: ChangeEvent<HTMLSelectElement>) => setUiState(event.target.value as UiState)}>
                  {uiStates.map((state) => (
                    <option key={state} value={state}>
                      {state}
                    </option>
                  ))}
                </select>
              </Field>
            </PageCard>

            <PageCard title="Navigation">
              <nav className="test-ui__nav" aria-label="Test UI pages">
                {rolePages.map((page) => (
                  <button
                    key={page.id}
                    className={currentPage === page.id ? "test-ui__nav-button test-ui__nav-button--active" : "test-ui__nav-button"}
                    type="button"
                    onClick={() => showPage(page.id)}
                  >
                    <span>{page.label}</span>
                    <small>{page.role}</small>
                  </button>
                ))}
              </nav>
            </PageCard>
          </aside>

          <section className="test-ui__workspace">
            <MessageBanner apiMessage={apiMessage} />
            <PageCard title="Demo state panel" note="Current local state snapshot">
              <div className="test-ui__state-grid">
                <span>Role: <strong>{currentRole}</strong></span>
                <span>Account: <strong>{accountState}</strong></span>
                <span>Request: <strong>{requestStatus}</strong></span>
                <span>Agreement: <strong>{agreementStatus}</strong></span>
                <span>Sender: <strong>{agreementSender}</strong></span>
                <span>UI: <strong>{uiState}</strong></span>
              </div>
            </PageCard>
            {renderActivePage()}
          </section>
        </section>
    </main>
  );
}
