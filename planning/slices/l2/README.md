# L2 Slice Drafting Index

Status: current / Employee request read drafts added  
Scope: planned L2 Employee, Review, AgreementProposalExchange and AgreementDocumentRef slices

## 1. Source Rule

Scenario text/DATA/UI/behavior files are source of truth for Scenario Flow and Behavior Coverage.

Domain draft is domain-design input, not a replacement for scenario sources:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

## 2. Current Full Drafts

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
```

## 3. Recommended Draft / Implementation Order

```text
1. SL-EMP-REQ-001 — Employee Request List Read
2. SL-EMP-REQ-002 — Employee Request Details Read
3. SL-EMP-REQ-003 — Start Request Review
4. SL-EMP-REQ-004 — Approve Request Review
5. SL-EMP-REQ-005 — Reject Request Review
6. SL-AGR-001 — Employee Starts AgreementProposalExchange / Sends First Proposal
7. SL-AGR-002 — Client Agreement Proposal Details + Response
8. SL-AGR-003 — Employee Agreement Proposal Details + Send New Version
9. SL-AGR-004 — Final Refusal
10. SL-DOC-001 — AgreementDocumentRef / Proposal Document Reference
```

## 4. Current Read-Slice Test Rule

For Employee request read slices:

```text
Primary verification: API/read integration tests.
Do not add unit tests by default.
Unit tests are allowed only for reusable helper logic with non-trivial branching.
```

This applies to:

```text
SL-EMP-REQ-001
SL-EMP-REQ-002
```
