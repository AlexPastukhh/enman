# SC-06 — Employee Request Dashboard Behavior Items

Status: migrated v1  
Source scenario: `SC-06`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-06-...
planning/diagrams/scenario-data/SC-06-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| EMP-READ-001 | READ | Employee request dashboard visibility | SC-06 / SC-07A | Owning scenario SC-06; related SC-07A. |

## 4. Grouped Behavior Items

### 4.READ — Read / Access Items

#### EMP-READ-001 — Employee request dashboard visibility

Source: `SC-06 / SC-07A`

Required behavior / guarantee:

```text
Employee dashboard/details show employee-accessible request data and review actions only where allowed.
```

Failure / no-write / preservation guarantee:

```text
Unauthorized employee context cannot access protected employee data/actions.
```

Migration note:

```text
Owning scenario SC-06; related SC-07A.
```

## 5. Cross-Scenario / Related Items

No related cross-scenario items identified in this migration pass.

## 6. Scenario Questions Raised

No scenario questions raised in this migration pass.

## 7. Downstream Use

This file should be checked when creating or updating:

```text
domain drafts
slice boundary drafts
parent vertical slice files
.client.md sidecars
unit/integration/client/E2E test plans
```
