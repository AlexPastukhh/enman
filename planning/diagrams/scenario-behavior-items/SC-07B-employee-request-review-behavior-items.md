# SC-07B — Employee Request Review Behavior Items

Status: migrated v1  
Source scenario: `SC-07B`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-07B-...
planning/diagrams/scenario-data/SC-07B-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| REQ-CMD-APPROVE-001 | CMD | Approve request | SC-07B | Migrated from pre-domain baseline 5.6. |
| REQ-CMD-REJECT-001 | CMD | Reject request | SC-07B | Migrated from pre-domain baseline 5.7. |
| REQ-LC-002 | LC | InReview request can be approved | SC-07B | Migrated from pre-domain baseline 6.1. |
| REQ-LC-003 | LC | InReview request can be rejected | SC-07B | Migrated from pre-domain baseline 6.1; feedback optional in current domain direction. |
| REQ-LC-004 | LC | Approved request cannot be reviewed again | SC-07A / SC-07B | Owning scenario set to SC-07B because review commands live there; related SC-07A for details/action availability. |
| REQ-LC-005 | LC | Rejected request cannot be approved in core | SC-07B | Migrated from pre-domain baseline 6.1. |
| REQ-LC-006 | LC | Rejected request cannot be rejected again in core | SC-07B | Migrated from pre-domain baseline 6.1. |
| REQ-IBS-001 | IBS | Approved request has review decision | SC-07B | Migrated from pre-domain baseline 7.1. |
| REQ-IBS-002 | IBS | Rejected request feedback policy | SC-07B / SC-05 | Migrated from pre-domain baseline 7.1; current decision: optional feedback in domain. |
| AGR-UCQ-002 | UCQ | Approval enables but does not create proposal | SC-07B / SC-13D | Owning scenario SC-07B; related SC-13D. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### REQ-CMD-APPROVE-001 — Approve request

Source: `SC-07B`

Required behavior / guarantee:

```text
Successful employee approval records a positive review result and makes the request Approved.
```

Failure / no-write / preservation guarantee:

```text
Failed approval does not change request status and does not record a new review decision.
```

Migration note:

```text
Migrated from pre-domain baseline 5.6.
```

#### REQ-CMD-REJECT-001 — Reject request

Source: `SC-07B`

Required behavior / guarantee:

```text
Successful employee rejection records a negative review result and makes the request Rejected.
```

Failure / no-write / preservation guarantee:

```text
Failed rejection does not change request status and does not record a new review decision or accepted rejection feedback.
```

Migration note:

```text
Migrated from pre-domain baseline 5.7.
```

### 4.LC — Scenario State / Condition Matrices

#### REQ-LC-002 — InReview request can be approved

Source: `SC-07B`

Required behavior / guarantee:

```text
Approval changes status to Approved and records review decision.
```

Failure / no-write / preservation guarantee:

```text
If approval fails, status and decision remain unchanged.
```

Migration note:

```text
Migrated from pre-domain baseline 6.1.
```

#### REQ-LC-003 — InReview request can be rejected

Source: `SC-07B`

Required behavior / guarantee:

```text
Rejection changes status to Rejected and records rejection decision/feedback if provided.
```

Failure / no-write / preservation guarantee:

```text
If rejection fails, status and decision/feedback remain unchanged.
```

Migration note:

```text
Migrated from pre-domain baseline 6.1; feedback optional in current domain direction.
```

#### REQ-LC-004 — Approved request cannot be reviewed again

Source: `SC-07A / SC-07B`

Required behavior / guarantee:

```text
Approved request is final for review in core.
```

Failure / no-write / preservation guarantee:

```text
Status unchanged; no new decision.
```

Migration note:

```text
Owning scenario set to SC-07B because review commands live there; related SC-07A for details/action availability.
```

#### REQ-LC-005 — Rejected request cannot be approved in core

Source: `SC-07B`

Required behavior / guarantee:

```text
Rejected request cannot be approved in core.
```

Failure / no-write / preservation guarantee:

```text
Status unchanged; no new decision.
```

Migration note:

```text
Migrated from pre-domain baseline 6.1.
```

#### REQ-LC-006 — Rejected request cannot be rejected again in core

Source: `SC-07B`

Required behavior / guarantee:

```text
Rejected request cannot be rejected again in core.
```

Failure / no-write / preservation guarantee:

```text
Status unchanged; no new decision.
```

Migration note:

```text
Migrated from pre-domain baseline 6.1.
```

### 4.IBS — Impossible Business State Candidates

#### REQ-IBS-001 — Approved request has review decision

Source: `SC-07B`

Required behavior / guarantee:

```text
Approved request without recorded review decision must not exist.
```

Failure / no-write / preservation guarantee:

```text
Failed approval must not write status or decision.
```

Migration note:

```text
Migrated from pre-domain baseline 7.1.
```

#### REQ-IBS-002 — Rejected request feedback policy

Source: `SC-07B / SC-05`

Required behavior / guarantee:

```text
Rejected request may have rejection feedback; current domain direction treats feedback as optional, with empty-feedback warning in Client/UI.
```

Failure / no-write / preservation guarantee:

```text
Failed rejection must not write status/feedback.
```

Migration note:

```text
Migrated from pre-domain baseline 7.1; current decision: optional feedback in domain.
```

### 4.UCQ — Use-Case Coordination Items

#### AGR-UCQ-002 — Approval enables but does not create proposal

Source: `SC-07B / SC-13D`

Required behavior / guarantee:

```text
Request approval enables agreement proposal creation but must not automatically create proposal.
```

Failure / no-write / preservation guarantee:

```text
Approval failure writes neither Approved status nor proposal.
```

Migration note:

```text
Owning scenario SC-07B; related SC-13D.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| AGR-UCQ-001 | SC-13D | UCQ | Proposal creation requires Approved request | Source mentions `SC-13D / SC-07B` |

## 6. Scenario Questions Raised

| Question ID | Affected item(s) | Question | Status |
|---|---|---|---|
| Q-SC-07B-001 | REQ-IBS-002 / REQ-CMD-REJECT-001 | Is rejection explanation required or optional? | open |

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
