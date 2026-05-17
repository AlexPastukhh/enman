# L2 Slice Planning Index

Status: current / Employee request read + dashboard/details client + start-review drafts synchronized  
Scope: L2 Employee, Request Review, AgreementProposalExchange and document-reference slice navigation

## 1. Source Rule

Scenario sources are the source of truth for Scenario Flow and Behavior Coverage.

Domain draft is domain-design input for aggregates, naming, invariants and target code sketches.

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/tables/domain-drafts/domain-draft-02.md
```

Do not use the domain draft as a replacement for scenario files.

## 2. Current L2 Drafted Backend Slices

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md
```

## 3. Current L2 Drafted Client Sidecars

```text
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
```

## 4. Recommended Next Drafts

```text
SL-EMP-REQ-003.client — Start Request Review client action
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review
SL-AGR-001 — Employee Starts AgreementProposalExchange / Sends First Proposal
SL-AGR-002 — Client Agreement Proposal Details + Response
SL-AGR-003 — Employee Agreement Proposal Details + Send New Version
SL-AGR-004 — Final Refusal
SL-DOC-001 — AgreementDocumentRef / Proposal Document Reference
```

## 5. Current Guardrails

```text
- Use Employee, not Worker.
- Review is owned by Request; no Review repository.
- Request public review API: StartReview / ApproveReview / RejectReview.
- No EmployeeRef in L2 target; domain methods receive Employee object where capability matters.
- Owned records store scalar ids such as StartedByEmployeeId.
- AgreementProposalExchange is separate aggregate from Request.
- Final refusal is exchange state plus request MarkAgreementExchangeFailed orchestration.
- AgreementDocumentRef is metadata reference, not bytes/storage adapter.
```

## 6. Client Sidecar Placement Guardrails

```text
- Read/detail pages map to pages + entities.
- Command actions map to pages + features + entities.
- Employee Details read sidecar must not execute StartReview/Approve/Reject.
- StartReviewResponseDto is not a details DTO.
- Details read contract must come from SL-EMP-REQ-002 server read slice and generated OpenAPI.
```
