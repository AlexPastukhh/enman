# SC-05 — My Requests / Own Request Details Behavior Items

Status: migrated v1  
Source scenario: `SC-05`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-05-...
planning/diagrams/scenario-data/SC-05-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| REQ-READ-001 | READ | Client sees only own requests | SC-05 / SC-15 | Migrated from pre-domain baseline 10.1. |

## 4. Grouped Behavior Items

### 4.READ — Read / Access Items

#### REQ-READ-001 — Client sees only own requests

Source: `SC-05 / SC-15`

Required behavior / guarantee:

```text
Client can see only own requests and own request details.
```

Failure / no-write / preservation guarantee:

```text
Wrong owner/context cannot read request details/list entry.
```

Migration note:

```text
Migrated from pre-domain baseline 10.1.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| REQ-IBS-002 | SC-07B | IBS | Rejected request feedback policy | Source mentions `SC-07B / SC-05` |

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
