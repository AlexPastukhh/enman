# SC-13A — My Agreements Behavior Items

Status: migrated v1  
Doc version: v0.1.0  
Source scenario: `SC-13A`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-13A-...
planning/diagrams/scenario-data/SC-13A-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| AGR-READ-001 | READ | Client sees only own agreements/proposals | SC-13A / SC-13B / SC-15 | Owning scenario SC-13A; related SC-13B and SC-15. |

## 4. Grouped Behavior Items

### 4.READ — Read / Access Items

#### AGR-READ-001 — Client sees only own agreements/proposals

Source: `SC-13A / SC-13B / SC-15`

Required behavior / guarantee:

```text
Client can see only own agreement proposals/details.
```

Failure / no-write / preservation guarantee:

```text
Wrong owner/context cannot read agreement proposal/details.
```

Migration note:

```text
Owning scenario SC-13A; related SC-13B and SC-15.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| AGR-IBS-001 | SC-13D | IBS | Proposal has sender | Source mentions `SC-13A..SC-13D` |

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
