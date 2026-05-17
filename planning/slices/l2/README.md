# L2 Slice Planning Index

Status: current / Employee request read + dashboard/details/start-review client + start-review server drafts synchronized  
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
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
```

## 4. Recommended Next Drafts

```text
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review
L2-REVIEW-APPROVE-001.client — Approve Request Review
L2-REVIEW-REJECT-001.client — Reject Request Review
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
- L2-REVIEW-START-001.client owns StartReview button/action/mutation only.
- StartReviewResponseDto is not a details DTO.
- Details read contract must come from SL-EMP-REQ-002 server read slice and generated OpenAPI.
- StartReview client implementation waits for SL-EMP-REQ-003 endpoint + generated OpenAPI.
```


## 7. Implementation Verification Notes

```text
planning/slices/l2/SL-EMP-REQ-001-implementation-verification-and-packaging-note.md
```

Current note:

```text
SL-EMP-REQ-001 implementation verification logs indicate client tests, server build, OpenAPI generation, API type generation and check:api passed after generated artifacts were staged.

If final implementation archive changes API shape, include:
- Shared/openapi.json
- energymanagement.client/src/shared/api/generated/openapi-types.ts

Run server/integration tests separately if they are part of the final gate.
```
