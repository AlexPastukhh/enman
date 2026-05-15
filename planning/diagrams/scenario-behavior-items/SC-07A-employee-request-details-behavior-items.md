# SC-07A — Employee Request Details Behavior Items

Status: migrated v1  
Source scenario: `SC-07A`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-07A-...
planning/diagrams/scenario-data/SC-07A-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

No migrated items currently own this scenario. This does not mean the scenario has no behavior; it means the existing compiled baseline did not contain a directly owned item in this migration pass.

## 4. Grouped Behavior Items

No grouped items in this migration pass.

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| REQ-LC-004 | SC-07B | LC | Approved request cannot be reviewed again | Source mentions `SC-07A / SC-07B` |
| EMP-READ-001 | SC-06 | READ | Employee request dashboard visibility | Source mentions `SC-06 / SC-07A` |

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
