# L2 Slice Planning Index

Status: current / near-final Employee Review and AgreementProposalExchange planning synchronized  
Scope: L2 Employee, Request Review, AgreementProposalExchange, AgreementDocumentRef and client sidecar navigation

## 1. Source Rule

Scenario sources are the source of truth for Scenario Flow and Behavior Coverage.

Domain drafts are domain-design input for aggregates, naming, invariants and target code sketches.

Current implementation state must be checked from GitHub/current branch, not from uploaded archives.

## 2. Account / Employee Identity Rule

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

## 3. Client API Placement Rule

For L2 client sidecars:

```text
Employee request reads:
  entities/employee-request/api

Employee review commands:
  features/employee-request/<action>/api

Agreement exchange reads:
  entities/agreement-exchange/api

Agreement exchange commands:
  features/agreement-exchange/<action>/api

shared/api:
  fetchJson, ProblemDetails/ApiError, CSRF helpers,
  generated OpenAPI types and generic transport helpers only
```

Do not add new business-specific wrappers such as:

```text
shared/api/employeeRequestApi.ts
shared/api/agreementExchangeApi.ts
```

Existing business-specific `shared/api/*Api.ts` files are transitional compatibility only and should not be copied into new L2 client work.

## 4. Employee Request Page Placement Rule

Employee request-area pages live under:

```text
pages/employee/requests/dashboard
pages/employee/requests/details
```

Do not place dashboard under:

```text
pages/employee/dashboard
```

## 5. Current L2 Review Slices

Server/API:

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md
planning/slices/SL-EMP-REQ-004-approve-request-review.md
planning/slices/SL-EMP-REQ-005-reject-request-review.md
```

Client sidecars:

```text
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
planning/slices/l2/L2-REVIEW-APPROVE-001-approve-request-review.client.md
planning/slices/l2/L2-REVIEW-REJECT-001-reject-request-review.client.md
```

Review command chain:

```text
SL-EMP-REQ-003 — Start Request Review
  POST /api/employee/requests/{requestId}/review/start
  success: 204 No Content
  client read state comes from list/details refetch
  two UI entry points: dashboard/list row and details action area

SL-EMP-REQ-004 — Approve Request Review
  POST /api/employee/requests/{requestId}/review/approve
  success: 204 No Content
  details-only first pass
  does not create AgreementProposalExchange

SL-EMP-REQ-005 — Reject Request Review
  POST /api/employee/requests/{requestId}/review/reject
  feedback/body optional unless implementation explicitly changes it
  success: 204 No Content
  details-only first pass
  no agreement proposal flow starts
```

## 6. AgreementProposalExchange Slice Family

Canonical server/backend/API slices:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Canonical files:

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md
planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md
planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md
```

Client sidecars:

```text
planning/slices/l2/L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md
planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md
planning/slices/l2/L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md
planning/slices/l2/L2-AGR-EXCH-SEND-PROPOSAL-001-send-agreement-proposal-version.client.md
planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
planning/slices/l2/L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md
```

Decision:

```text
Start initial exchange — separate slice.
Client send / Employee send counter-proposal — one slice with two actor branches.
List read — separate slice.
Details read — separate slice.
Client accept — separate slice.
Employee final refusal — separate slice.
```

Do not split client and employee counter-proposal sends unless UI, permissions, document handling or validation diverge materially.

## 7. Agreement Exchange Command/Read Summary

```text
SL-AGR-EXCH-001:
  Employee-only start from approved request/request details.
  Creates AgreementProposalExchange and proposal version 1.
  Stores ClientAccountId from approved request owner.

SL-AGR-EXCH-002:
  Shared Client/Employee counter-proposal command.
  Route direction: POST /api/requests/{requestId}/agreement-exchange/proposals.
  Previous active proposal becomes SupersededByCounterProposal.

SL-AGR-EXCH-003:
  Shared Client/Employee list read.
  Route direction: GET /api/agreement-exchanges.
  List summary only; no full proposal history.

SL-AGR-EXCH-004:
  Shared Client/Employee details read.
  Route direction: GET /api/agreement-exchanges/{exchangeId}.
  Details owns active proposal and proposal version history.

SL-AGR-EXCH-005:
  Client-only accept active Employee proposal.
  No body, 204 success, no new proposal version.

SL-AGR-EXCH-006:
  Employee-only final refusal.
  Optional/nullable reason body.
  Orchestrates exchange.FinalRefuseProposal(...) and request.MarkAgreementExchangeFailed(...).
```

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
- AgreementProposalExchange stores ClientAccountId.
- Do not add ResponsibleEmployeeId as first-pass authorization guard.
- Counter-proposal versioning is one lifecycle pattern with client and employee actor branches.
- AgreementDocumentRef is metadata reference, not file bytes/storage adapter.
- Do not add per-command status enums.
```
