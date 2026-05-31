# SC-13B — Agreement Proposal Details / Response Behavior Items

Status: migrated v1  
Doc version: v0.1.0  
Source scenario: `SC-13B`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-13B-...
planning/diagrams/scenario-data/SC-13B-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| AGR-CMD-CLIENT-ACCEPT-001 | CMD | Client accepts proposal | SC-13B | Migrated from pre-domain baseline 5.10. |
| AGR-CMD-CLIENT-SEND-001 | CMD | Client sends own proposal version | SC-13B | Migrated from pre-domain baseline 5.11. |
| AGR-LC-002 | LC | Awaiting proposal can be accepted | SC-13B | Migrated from pre-domain baseline 6.2. |
| AGR-LC-003 | LC | Awaiting proposal can receive client version | SC-13B | Migrated from pre-domain baseline 6.2. |
| AGR-LC-004 | LC | Accepted proposal has no response actions in core | SC-13B | Migrated from pre-domain baseline 6.2. |
| AGR-LC-005 | LC | Rejected proposal has no response actions in core | SC-13B | Migrated from pre-domain baseline 6.2. |
| AGR-LC-006 | LC | Client own version can be sent only once in core | SC-13B | Migrated from pre-domain baseline 6.3. |
| AGR-LC-006B | LC | Duplicate client own proposal is rejected | SC-13B | Detail row existed in state matrix but not registry; migrated as explicit sub-item. |
| AGR-IBS-003 | IBS | Client does not start exchange | SC-13B / SC-13D | Owning scenario SC-13B because it constrains client response flow. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### AGR-CMD-CLIENT-ACCEPT-001 — Client accepts proposal

Source: `SC-13B`

Required behavior / guarantee:

```text
Client can accept an employee-sent proposal that is awaiting client confirmation and belongs to the client context.
```

Failure / no-write / preservation guarantee:

```text
Wrong status, wrong ownership/context or inactive proposal does not become Accepted.
```

Migration note:

```text
Migrated from pre-domain baseline 5.10.
```

#### AGR-CMD-CLIENT-SEND-001 — Client sends own proposal version

Source: `SC-13B`

Required behavior / guarantee:

```text
Client can send one own agreement version in response to an employee-sent proposal awaiting confirmation.
```

Failure / no-write / preservation guarantee:

```text
No client proposal is created if proposal is not awaiting client confirmation, not employee-sent, not in client context, input is invalid, or client already sent own version in core.
```

Migration note:

```text
Migrated from pre-domain baseline 5.11.
```

### 4.LC — Scenario State / Condition Matrices

#### AGR-LC-002 — Awaiting proposal can be accepted

Source: `SC-13B`

Required behavior / guarantee:

```text
Client can accept active employee proposal awaiting client confirmation.
```

Failure / no-write / preservation guarantee:

```text
Wrong status/context remains unchanged.
```

Migration note:

```text
Migrated from pre-domain baseline 6.2.
```

#### AGR-LC-003 — Awaiting proposal can receive client version

Source: `SC-13B`

Required behavior / guarantee:

```text
Client can send own version in response to active employee proposal awaiting confirmation.
```

Failure / no-write / preservation guarantee:

```text
Wrong status/context/input creates no client version.
```

Migration note:

```text
Migrated from pre-domain baseline 6.2.
```

#### AGR-LC-004 — Accepted proposal has no response actions in core

Source: `SC-13B`

Required behavior / guarantee:

```text
Accepted proposal has no response actions in core.
```

Failure / no-write / preservation guarantee:

```text
Status unchanged; no new proposal.
```

Migration note:

```text
Migrated from pre-domain baseline 6.2.
```

#### AGR-LC-005 — Rejected proposal has no response actions in core

Source: `SC-13B`

Required behavior / guarantee:

```text
Rejected/inactive proposal has no response actions in core.
```

Failure / no-write / preservation guarantee:

```text
Status unchanged; no new proposal.
```

Migration note:

```text
Migrated from pre-domain baseline 6.2.
```

#### AGR-LC-006 — Client own version can be sent only once in core

Source: `SC-13B`

Required behavior / guarantee:

```text
Client can send one own version in core.
```

Failure / no-write / preservation guarantee:

```text
Invalid/duplicate response creates no own version.
```

Migration note:

```text
Migrated from pre-domain baseline 6.3.
```

#### AGR-LC-006B — Duplicate client own proposal is rejected

Source: `SC-13B`

Required behavior / guarantee:

```text
Client cannot send second own version in core.
```

Failure / no-write / preservation guarantee:

```text
No new proposal created; existing proposals unchanged.
```

Migration note:

```text
Detail row existed in state matrix but not registry; migrated as explicit sub-item.
```

### 4.IBS — Impossible Business State Candidates

#### AGR-IBS-003 — Client does not start exchange

Source: `SC-13B / SC-13D`

Required behavior / guarantee:

```text
Client-started agreement exchange without employee proposal must not exist.
```

Failure / no-write / preservation guarantee:

```text
Client cannot create first proposal.
```

Migration note:

```text
Owning scenario SC-13B because it constrains client response flow.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| AGR-IBS-002 | SC-13D | IBS | Proposal has document/file | Source mentions `SC-13B / SC-13D` |
| AGR-VI-001 | SC-13D | VI | Agreement document reference | Source mentions `SC-13B / SC-13D-DATA` |
| AGR-VI-002 | SC-13D | VI | Proposal text details/comment | Source mentions `SC-13B / SC-13D-DATA` |
| AGR-READ-001 | SC-13A | READ | Client sees only own agreements/proposals | Source mentions `SC-13A / SC-13B / SC-15` |
| FILE-INT-001 | SC-11 | INT | File/blob storage is infrastructure | Source mentions `SC-11 / SC-13B / SC-13D` |

## 6. Scenario Questions Raised

| Question ID | Affected item(s) | Question | Status |
|---|---|---|---|
| Q-SC-13D-001 | AGR-LC-007 / AGR-CMD-EMP-NEW-001 | Should replaced proposal version be named Superseded/Replaced instead of Rejected? | open |
| Q-SC-13D-002 | AGR-VI-002 | Are proposal text details/comment required or optional? | open |

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
