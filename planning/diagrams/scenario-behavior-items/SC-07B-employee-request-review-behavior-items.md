# SC-07B — Employee Request Review Behavior Items

Status: current / RejectReview optional feedback synchronized  
Source scenario: `SC-07B`

## 1. Purpose

This file contains behavior items owned by Employee Request Review.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
planning/diagrams/scenario-data/SC-07B-...
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md
planning/tables/domain-drafts/domain-draft-02.md as domain-design input only
```

The old global `scenario-server-domain-validation-addendum.md`, if present in deprecated folders, is not an active source of truth.

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Current note |
|---|---|---|---|---|
| REQ-CMD-APPROVE-001 | CMD | Approve request | SC-07B | Successful approval records positive review result and makes request Approved. |
| REQ-CMD-REJECT-001 | CMD | Reject request | SC-07B | Successful rejection records negative review result; feedback is optional current direction. |
| REQ-LC-002 | LC | InReview request can be approved | SC-07B | Approval changes status to Approved and records review decision. |
| REQ-LC-003 | LC | InReview request can be rejected | SC-07B | Rejection changes status to Rejected and records feedback if provided. |
| REQ-LC-004 | LC | Approved request cannot be reviewed again | SC-07A / SC-07B | Owning scenario SC-07B; related SC-07A for details/action availability. |
| REQ-LC-005 | LC | Rejected request cannot be approved in core | SC-07B | Rejected request cannot be approved in core. |
| REQ-LC-006 | LC | Rejected request cannot be rejected again in core | SC-07B | Rejected request cannot be rejected again in core. |
| REQ-IBS-001 | IBS | Approved request has review decision | SC-07B | Approved request without recorded review decision must not exist. |
| REQ-IBS-002 | IBS | Rejected request feedback policy | SC-07B / SC-05 | Current decision: optional feedback in domain/API direction unless server/OpenAPI intentionally changes it. |
| AGR-UCQ-002 | UCQ | Approval enables but does not create proposal | SC-07B / SC-13D | ApproveReview does not create AgreementProposalExchange. |

## 4. Grouped Behavior Items

### REQ-CMD-APPROVE-001 — Approve request

Required behavior / guarantee:

```text
Successful Employee approval records a positive review result and makes the request Approved.
```

Failure / no-write / preservation guarantee:

```text
Failed approval does not change request status and does not record a new review decision.
```

### REQ-CMD-REJECT-001 — Reject request

Required behavior / guarantee:

```text
Successful Employee rejection records a negative review result and makes the request Rejected.
Feedback may be recorded if provided.
```

Failure / no-write / preservation guarantee:

```text
Failed rejection does not change request status and does not record a new review decision or accepted rejection feedback.
```

### REQ-LC-003 — InReview request can be rejected

Required behavior / guarantee:

```text
Rejection changes status to Rejected and records rejection decision/feedback if provided.
```

Failure / no-write / preservation guarantee:

```text
If rejection fails, status and decision/feedback remain unchanged.
```

### REQ-IBS-002 — Rejected request feedback policy

Required behavior / guarantee:

```text
Rejected request may have rejection feedback; current domain/API direction treats feedback as optional.
```

Failure / no-write / preservation guarantee:

```text
Failed rejection must not write status/feedback.
```

### AGR-UCQ-002 — Approval enables but does not create proposal

Required behavior / guarantee:

```text
Request approval enables agreement proposal creation but must not automatically create proposal or AgreementProposalExchange.
```

Failure / no-write / preservation guarantee:

```text
Approval failure writes neither Approved status nor proposal/exchange.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| AGR-UCQ-001 | SC-13D | UCQ | Proposal creation requires Approved request | Source mentions `SC-13D / SC-07B` |

## 6. Scenario Questions Raised

| Question ID | Affected item(s) | Question | Status |
|---|---|---|---|
| Q-SC-07B-001 | REQ-IBS-002 / REQ-CMD-REJECT-001 | Is rejection explanation required or optional? | accepted: optional current direction |

See `planning/diagrams/scenario-questions-register.md` for full details.

## 7. Downstream Use

This file should be checked when creating or updating:

```text
domain drafts
slice boundary drafts
parent vertical slice files
.client.md sidecars
unit/integration/client/E2E test plans
```
