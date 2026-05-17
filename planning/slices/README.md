# Slice Planning Index

Status: current / near-final L2 Employee Review and Agreement Exchange slice navigation synchronized

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

For the current L2 state also read:

```text
planning/l2-current-planning-status.md
planning/slices/l2/README.md
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
- Read-only UI belongs in entities/widgets/pages.
- Command/user-action UI belongs in features.
- Read endpoint wrappers belong in entities/*/api.
- Command endpoint wrappers belong in features/*/api.
- shared/api is only transport/generated infrastructure.
- Generated OpenAPI types remain shared generated artifacts, but entity/feature API files may import them directly and define local business aliases.
- One draft covers one slice; extension slices are named but not implemented.
- Questions / Decisions and Behavior Coverage should appear early enough to guide implementation.
- Full server/client drafts must include Implementation Checklist near the end.
```

## 4. Current L2 Review Drafts

Server/API slices:

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
StartReview:
  two UI entry points: dashboard/list row + details action area;
  one feature sidecar.

ApproveReview:
  details-only first pass;
  no AgreementProposalExchange creation.

RejectReview:
  details-only first pass;
  feedback optional;
  no AgreementProposalExchange creation.
```

## 5. AgreementProposalExchange Slice Family

Canonical server/backend/API slices:

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md
planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md
planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md
```

Canonical client sidecars:

```text
planning/slices/l2/L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md
planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md
planning/slices/l2/L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md
planning/slices/l2/L2-AGR-EXCH-SEND-PROPOSAL-001-send-agreement-proposal-version.client.md
planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
planning/slices/l2/L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md
```

Decision summary:

```text
Start initial exchange — separate slice because it creates the exchange and version 1.
Client send / Employee send counter-proposal — one slice with two actor branches.
List read — separate read slice.
Details read — separate read slice.
Client accept — separate Client-only command slice.
Employee final refusal — separate Employee-only command slice.
```

Do not split client and employee counter-proposal sends unless UI, permissions, document handling or validation diverge materially.

`SL-DOC-*` remains the future document/reference family for AgreementDocumentRef/document metadata/storage follow-up work.

## 6. Agreement Exchange Guardrails

```text
AgreementProposalExchange and Request are separate aggregates.
ApproveReview does not create AgreementProposalExchange.
Initial exchange creation requires initial Employee proposal document.
AgreementProposalExchange stores ClientAccountId from approved request owner.
Client actions are protected by client.Id == exchange.ClientAccountId.
Do not add ResponsibleEmployeeId as first-pass authorization guard.
Any active Employee can service agreement exchanges first pass.
Proposal author is stored per proposal version through Sender and SenderId.
Counter-proposal replacement is SupersededByCounterProposal, not ordinary Rejected.
AgreementDocumentRef is document metadata reference, not bytes/storage adapter.
Do not use per-command status enums.
```

## 7. StartReview Entry Point Rule

StartReview client has one command sidecar and two entry points:

```text
dashboard/list row
details action area
```

Dashboard/details read pages host feature actions; they do not own command mutations.

## 8. Current L2 Client Sidecar Reminder

For read sidecars:

```text
Do:
  entities/<entity>/api/<readWrapper>.ts
  entities/<entity>/api/<entity>ApiTypes.ts

Do not:
  shared/api/<businessEntity>Api.ts
```

For command sidecars:

```text
Do:
  features/<business-area>/<action>/api/<commandWrapper>.ts
  features/<business-area>/<action>/model/<mutation>.ts
  features/<business-area>/<action>/ui/<ActionFormOrButton>.tsx

Do not:
  entities/*/api for command wrappers
  shared/api business wrappers
```

Agreement exchange reads/commands follow the same ownership rule: reads in entities, commands in features.

## 9. Current-State Rule

When asked what exists or is implemented now, inspect GitHub/current branch.

Do not answer current implementation status from uploaded archives or slice drafts alone.
