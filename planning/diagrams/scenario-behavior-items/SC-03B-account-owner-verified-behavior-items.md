# SC-03B — Account Owner Verified / Password Reset Choice Behavior Items

Status: migrated v1  
Source scenario: `SC-03B`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-03B-...
planning/diagrams/scenario-data/SC-03B-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| ACC-CMD-RESET-001 | CMD | Set new password | SC-03B | Migrated from pre-domain baseline 5.3. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### ACC-CMD-RESET-001 — Set new password

Source: `SC-03B`

Required behavior / guarantee:

```text
Verified account owner can set a new password when recovery context is valid, unless user chooses to log in with existing password instead.
```

Failure / no-write / preservation guarantee:

```text
Invalid, expired or used recovery context does not update password.
```

Migration note:

```text
Migrated from pre-domain baseline 5.3.
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
