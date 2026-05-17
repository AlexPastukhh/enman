# L2 Slice Planning Index

Status: current / Employee request read, dashboard client and StartReview drafts synchronized  
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

## 2. Current L2 Drafted Backend / API Slices

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md
```

## 3. Current L2 Drafted Client Sidecars

```text
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
```

## 4. Recommended Next Drafts

```text
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review
L2-EMP-DETAILS-001.client — Employee Request Details
L2-REVIEW-START.client — Start Review Action
L2-REVIEW-APPROVE.client — Approve Review Action
L2-REVIEW-REJECT.client — Reject Review Action
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

## 6. Testing Guardrail

For Employee read slices:

```text
Primary verification: API/read integration tests.
No unit tests by default.
Unit tests only for reusable helper logic with meaningful branching.
```

For Employee command slices:

```text
Primary verification: API integration tests with DB state assertions.
Do not use repository mocks or handler call-order as primary proof.
```
