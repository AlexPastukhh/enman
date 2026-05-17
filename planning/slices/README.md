# Slice Planning Index

Status: current slice-planning navigation index / client API placement, L2 review command drafts and AgreementProposalExchange slice boundaries synchronized

## 1. Core Rule

Scenario Flow and Behavior Items come from:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Client drafters must use:

```text
planning/slices/client-slice-short-draft-rules-and-example.md
planning/client/client-api-placement-decision.md
planning/client/client-layering-for-read-and-command-slices.md
```

## 2. Current Client API Placement Rule

```text
Read endpoint wrappers:
  entities/*/api

Command/mutation endpoint wrappers:
  features/*/api

Shared API:
  fetchJson
  ProblemDetails / ApiError
  CSRF/antiforgery helpers
  generated OpenAPI types
  generic transport helpers
```

Existing business-specific wrappers in `shared/api` are transitional compatibility. New drafts should not copy that shape.

## 3. Drafting Rules

```text
- Draft by examples, not by improvisation.
- Scenario Flow is user/system behavior from scenario sources.
- Implementation Flow is code/layer responsibility.
- Behavior items are not implementation details.
- Read-only UI belongs in entities.
- Command/user-action UI belongs in features.
- Read endpoint wrappers belong in entities/*/api.
- Command endpoint wrappers belong in features/*/api.
- shared/api is only transport/generated infrastructure.
- Generated OpenAPI types remain shared generated artifacts, but entity/feature API files may import them directly and define local business aliases.
- One draft covers one slice; extension slices are named but not implemented.
```

## 4. Current L2 Review Drafts

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
SL-EMP-REQ-003 — Start Request Review
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review

L2-EMP-DASH-001.client — Employee Request Dashboard
L2-EMP-DETAILS-001.client — Employee Request Details
L2-REVIEW-START-001.client — Start Request Review client action
```

Current remaining review draft gaps:

```text
L2-REVIEW-APPROVE-001.client — Approve Request Review client action
L2-REVIEW-REJECT-001.client — Reject Request Review client action/form
```

## 5. AgreementProposalExchange Slice Family

Fixed planned slice boundaries:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Read Agreement Exchange
SL-AGR-EXCH-004 — Accept Active Agreement Proposal
SL-AGR-EXCH-005 — Final Refuse Agreement Exchange
```

Decision summary:

```text
Start initial exchange — separate slice because it creates the exchange and version 1.
Client send / Employee send counter-proposal — one slice with two actor branches.
Read — separate slice.
Accept — separate slice.
Final refusal — separate slice.
```

Do not split client and employee counter-proposal sends unless UI, permissions, document handling or validation diverge materially.

`SL-DOC-*` remains the future document/reference family for AgreementDocumentRef/document metadata/storage follow-up work.

## 6. StartReview Entry Point Rule

StartReview client has one command sidecar and two entry points:

```text
dashboard/list row
details action area
```

Dashboard/details read pages host feature actions; they do not own command mutations.

## 7. Current L2 Client Sidecar Reminder

For `L2-EMP-DETAILS-001.client`:

```text
Do:
  entities/employee-request/api/getEmployeeRequestDetails.ts
  entities/employee-request/api/employeeRequestApiTypes.ts

Do not:
  shared/api/employeeRequestApi.ts
```

Future review commands use `features/employee-request/<action>/api`.
Agreement exchange reads/commands follow the same ownership rule: reads in entities, commands in features.
