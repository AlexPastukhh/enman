# SC-03A — Password Recovery Request Behavior Items

Status: migrated v1  
Doc version: v0.1.0  
Source scenario: `SC-03A`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-03A-...
planning/diagrams/scenario-data/SC-03A-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| ACC-CMD-RECOVERY-001 | CMD | Request password recovery | SC-03A | Migrated from pre-domain baseline 5.2. |
| AUTH-INT-001 | INT | Password recovery email side effect | SC-03A | Migrated from pre-domain baseline 10.2. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### ACC-CMD-RECOVERY-001 — Request password recovery

Source: `SC-03A`

Required behavior / guarantee:

```text
Password recovery request accepts an email-like value and behaves according to account recovery rules without revealing account existence.
```

Failure / no-write / preservation guarantee:

```text
If email is not registered, no recovery context is created and account existence is not revealed.
```

Migration note:

```text
Migrated from pre-domain baseline 5.2.
```

### 4.INT — Integration / Infrastructure Items

#### AUTH-INT-001 — Password recovery email side effect

Source: `SC-03A`

Required behavior / guarantee:

```text
Password recovery email is sent only as side effect of acceptable recovery flow; account existence is not revealed.
```

Failure / no-write / preservation guarantee:

```text
Unacceptable recovery flow does not reveal existence or create accepted context.
```

Migration note:

```text
Migrated from pre-domain baseline 10.2.
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
