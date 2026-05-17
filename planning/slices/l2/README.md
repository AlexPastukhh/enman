# L2 Slice Planning Index

Status: current / Employee request read/review drafts and client API placement synchronized  
Scope: L2 Employee, Request Review, AgreementProposalExchange and document-reference slice navigation

## 1. Source Rule

Scenario sources are the source of truth for Scenario Flow and Behavior Coverage.

Domain draft is domain-design input for aggregates, naming, invariants and target code sketches.

## 2. Client API Placement Rule

For L2 client sidecars:

```text
Employee request reads:
  entities/employee-request/api

Employee review commands:
  features/employee-request/<action>/api

shared/api:
  fetchJson, ProblemDetails/ApiError, CSRF helpers,
  generated OpenAPI types and generic transport helpers only
```

Examples:

```text
entities/employee-request/api/listEmployeeDashboardRequests.ts
entities/employee-request/api/getEmployeeRequestDetails.ts
features/employee-request/start-review/api/startRequestReview.ts
```

## 3. Current L2 Drafted Slices

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md

planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
```

## 4. Current Guardrails

```text
- Use Employee, not Worker.
- Review is owned by Request; no Review repository.
- Request public review API: StartReview / ApproveReview / RejectReview.
- No EmployeeRef in L2 target.
- Temporary employee visibility policy: all active Employees can see all review-relevant requests.
```
