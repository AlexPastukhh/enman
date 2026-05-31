# SC-01 — Guest Registration Behavior Items

Status: migrated v1  
Doc version: v0.1.0  
Source scenario: `SC-01`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-01-...
planning/diagrams/scenario-data/SC-01-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| ACC-CMD-REGISTER-001 | CMD | Register account | SC-01 | Migrated from pre-domain baseline 5.1. |
| ACC-LC-001 | LC | Registration creates Active account in core | SC-01 | Migrated from account activation addendum 5.1. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### ACC-CMD-REGISTER-001 — Register account

Source: `SC-01`

Required behavior / guarantee:

```text
Accepted registration creates an account / registered identity from email, password and password confirmation.
```

Failure / no-write / preservation guarantee:

```text
Invalid registration input does not create account.
```

Migration note:

```text
Migrated from pre-domain baseline 5.1.
```

### 4.LC — Scenario State / Condition Matrices

#### ACC-LC-001 — Registration creates Active account in core

Source: `SC-01`

Required behavior / guarantee:

```text
Current core registration creates activated account.
```

Failure / no-write / preservation guarantee:

```text
Invalid registration creates no account.
```

Migration note:

```text
Migrated from account activation addendum 5.1.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| ACC-LC-003 | SC-15 | LC | Future PendingActivation to Active flow | Source mentions `SC-01 / SC-15` |

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
