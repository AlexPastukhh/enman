# L2 Slice Planning Index

Status: current / Employee request read/review drafts, StartReview entry points, review command drafts and AgreementProposalExchange slice boundaries synchronized  
Scope: L2 Employee, Request Review, AgreementProposalExchange and document-reference slice navigation

## 1. Source Rule

Scenario sources are the source of truth for Scenario Flow and Behavior Coverage.

Domain draft is domain-design input for aggregates, naming, invariants and target code sketches.

## 1A. Account / Employee Identity Rule

Accepted L2 target:

```text
Account
  -> ClientAccount
  -> Employee
```

Persistence direction:

```text
L1Accounts TPH with AccountType / Role discriminator.
```

Identity rule:

```text
ClaimTypes.NameIdentifier = Account.Id.
For Employee sessions, Account.Id == Employee.Id.
```

Do not model target `Employee` as a separate profile entity linked by `AccountId`.
If current implementation has `Employee.AccountId`, treat it as compatibility/drift until a scoped persistence/domain slice resolves it.

## 2. Client API Placement Rule

For L2 client sidecars:

```text
Employee request reads:
  entities/employee-request/api

Employee review commands:
  features/employee-request/<action>/api

Agreement exchange reads:
  entities/agreement-exchange/api, unless a more specific entity name is chosen in the full draft

Agreement exchange commands:
  features/agreement-exchange/<action>/api

shared/api:
  fetchJson, ProblemDetails/ApiError, CSRF helpers,
  generated OpenAPI types and generic transport helpers only
```

Examples:

```text
entities/employee-request/api/listEmployeeDashboardRequests.ts
entities/employee-request/api/getEmployeeRequestDetails.ts
entities/employee-request/api/employeeRequestApiTypes.ts

features/employee-request/start-review/api/startRequestReview.ts
features/employee-request/approve-review/api/approveRequestReview.ts
features/employee-request/reject-review/api/rejectRequestReview.ts
```

Do not add new business-specific wrappers such as:

```text
shared/api/employeeRequestApi.ts
shared/api/agreementExchangeApi.ts
```

Existing business-specific `shared/api/*Api.ts` files are transitional compatibility only and should not be copied into new L2 client work.

## 3. Employee Request Page Placement Rule

Employee request-area pages live under:

```text
pages/employee/requests/dashboard
pages/employee/requests/details
```

Do not place dashboard under:

```text
pages/employee/dashboard
```

## 4. Current L2 Drafted Slices

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md
planning/slices/SL-EMP-REQ-004-approve-request-review.md
planning/slices/SL-EMP-REQ-005-reject-request-review.md

planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
```

Planned client sidecars not yet full-drafted here:

```text
L2-REVIEW-APPROVE-001.client — Approve Request Review client action
L2-REVIEW-REJECT-001.client — Reject Request Review client action/form
```

## 5. AgreementProposalExchange Slice Family

The previous loose `SL-AGR-*` placeholder is now fixed as five planned slice boundaries:

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md
planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md
```

Decision:

```text
Start initial exchange — separate slice.
Client send / Employee send counter-proposal — one slice with two actor branches.
Read — separate slice.
Accept — separate slice.
Final refusal — separate slice.
```

Do not split client and employee counter-proposal sends yet unless UI, permissions, document handling or validation diverge materially.

## 6. Review Command Chain

```text
SL-EMP-REQ-003 — Start Request Review
  POST /api/employee/requests/{requestId}/review/start
  success: 204 No Content
  client read state comes from list/details refetch

SL-EMP-REQ-004 — Approve Request Review
  POST /api/employee/requests/{requestId}/review/approve
  success: 204 No Content
  does not create AgreementProposalExchange

SL-EMP-REQ-005 — Reject Request Review
  POST /api/employee/requests/{requestId}/review/reject
  body: rejection feedback
  success: 204 No Content
  no agreement proposal flow starts
```

## 7. StartReview Client Entry Points

`L2-REVIEW-START-001.client` is one client command sidecar with two host placements:

```text
1. Employee request dashboard/list row.
2. Employee request details action area.
```

This does not create two StartReview slices. Dashboard and details host the same feature-owned command action.

## 8. Current Guardrails

```text
- Use Employee, not Worker.
- Employee inherits from Account in L2 target; Employee.Id is Account.Id.
- Employee endpoints resolve current Employee from ClaimTypes.NameIdentifier as Account.Id.
- Review is owned by Request; no Review repository.
- Request public review API: StartReview / ApproveReview / RejectReview.
- No EmployeeRef in L2 target.
- Temporary employee visibility policy: all active Employees can see all review-relevant requests.
- Employee visibility is backend authorization/read filtering, not UI visibility.
- Command success responses use 204 No Content when read state should be refreshed from read endpoints.
- AgreementProposalExchange and Request are separate aggregates.
- Initial agreement exchange creation requires initial Employee proposal document.
- Counter-proposal versioning is one lifecycle pattern with client and employee actor branches.
```

<!-- L2-AGR-EXCH-COMMAND-SLICES-SYNC -->
## Agreement Exchange Command Slice Sync

Canonical agreement exchange command/read slice set:

`	ext
SL-AGR-EXCH-001 вЂ” Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 вЂ” Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 вЂ” Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 вЂ” Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 вЂ” Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 вЂ” Final Refuse Agreement Exchange
`

Client sidecars added for command/read continuation:

`	ext
L2-AGR-EXCH-LIST-001.client
L2-AGR-EXCH-DETAILS-001.client
L2-AGR-EXCH-SEND-PROPOSAL-001.client
L2-AGR-EXCH-ACCEPT-001.client
`

Rules:
- current project state questions require GitHub/current branch inspection, not archives;
- server/client full drafts must keep Implementation Checklist near the end;
- server/backend/API slice drafts live in planning/slices/;
- client sidecar drafts live in planning/slices/l2/.


<!-- AGR-EXCH-FINAL-REFUSE-SYNC -->
## Agreement exchange final refusal sync

Canonical final refusal drafts:

```text
planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md
planning/slices/l2/L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md
```

`SL-AGR-EXCH-006` is Employee-only backend/API command:

```text
POST /api/agreement-exchanges/{exchangeId}/final-refuse
success: 204 No Content
body: nullable FinalRefuseAgreementExchangeDto? with optional reason
```

The command orchestrates two aggregates through domain methods:

```text
exchange.FinalRefuseProposal(...)
request.MarkAgreementExchangeFailed(...)
```

Client sidecar is Employee details-only first pass. Do not add Client final refusal.
<!-- /AGR-EXCH-FINAL-REFUSE-SYNC -->

