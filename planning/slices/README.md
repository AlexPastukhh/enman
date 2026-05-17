# Slice Planning Index

Status: current slice-planning navigation index / L2 review and AgreementProposalExchange canonical numbering synchronized

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

Current-state answers must inspect GitHub/current branch. Uploaded archives are handoff inputs, not current implementation evidence.

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

Existing business-specific wrappers in `shared/api` are transitional compatibility. New drafts must not copy that shape.

## 3. Drafting Rules

```text
- Draft by existing examples, not by improvisation.
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
- Full server/client drafts keep Questions/Decisions and Behavior Coverage near the beginning and Implementation Checklist near the end.
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

Current remaining review client sidecar gaps:

```text
L2-REVIEW-APPROVE-001.client — Approve Request Review client action
L2-REVIEW-REJECT-001.client — Reject Request Review client action/form
```

RejectReview feedback direction:

```text
Feedback/body is optional unless current server/OpenAPI intentionally changes it.
Empty feedback must not block submit by default.
```

## 5. AgreementProposalExchange Canonical Slice Family

Canonical server/backend/API slice set:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Canonical client sidecars:

```text
L2-AGR-EXCH-START-001.client — Start Agreement Exchange With Initial Employee Proposal
L2-AGR-EXCH-LIST-001.client — Agreement Exchange List Pages
L2-AGR-EXCH-DETAILS-001.client — Agreement Exchange Details Pages
L2-AGR-EXCH-SEND-PROPOSAL-001.client — Send Agreement Proposal Version
L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal
L2-AGR-EXCH-FINAL-REFUSE-001.client — Employee Final Refuse Agreement Exchange
```

Decision summary:

```text
Start initial exchange — separate slice because it creates the exchange and version 1.
Client send / Employee send counter-proposal — one slice with two actor branches.
List read — separate slice.
Details read — separate slice.
Accept — separate Client-only command slice.
Final refusal — separate Employee-only command slice.
```

Do not use the older numbering:

```text
003 Read Agreement Exchange
004 Accept Active Agreement Proposal
005 Final Refuse Agreement Exchange
```

Use the canonical list/details split above.

`SL-DOC-*` remains the future document/reference family for AgreementDocumentRef/document metadata/storage follow-up work.

## 6. StartReview Entry Point Rule

StartReview client has one command sidecar and two entry points:

```text
dashboard/list row
details action area
```

Dashboard/details read pages host feature actions; they do not own command mutations.

## 7. Agreement Exchange Guardrails

```text
- AgreementProposalExchange stores ClientAccountId.
- Client actions are protected by client.Id == exchange.ClientAccountId.
- Do not add ResponsibleEmployeeId as authorization guard first pass.
- Any active Employee can service the exchange first pass.
- Proposal authors are tracked per proposal version with Sender and SenderId.
- Counter-proposal replacement is SupersededByCounterProposal, not ordinary Rejected.
- Do not add per-command status enums.
- AgreementExchangeStatus is persisted domain state, not command execution result.
- Start exchange happens from Employee request details before exchange exists.
- After exchange exists, proposal negotiation happens from Agreement Exchange details.
```
