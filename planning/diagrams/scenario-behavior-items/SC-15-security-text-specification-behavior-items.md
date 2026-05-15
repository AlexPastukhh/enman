# SC-15 — Security Text Specification Behavior Items

Status: migrated v1  
Source scenario: `SC-15`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-15-...
planning/diagrams/scenario-data/SC-15-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| ACC-LC-002 | LC | Non-active account cannot use protected functionality | SC-02 / SC-15 | Owning scenario SC-15; related SC-02. |
| ACC-LC-003 | LC | Future PendingActivation to Active flow | SC-01 / SC-15 | Deferred/future. |
| ACC-UCQ-001 | UCQ | Protected use cases require active account without polluting aggregates | SC-02 / SC-15 | Migrated from account activation addendum 6.1. |
| ACC-SEC-001 | SEC | Activated account required for protected functionality | SC-02 / SC-15 | Migrated from account activation addendum 7.1. SEC category exists in activation addendum. |
| ACC-SQ-001 | SQ | Non-active account login behavior | SC-02 / SC-15 | Migrated from activation addendum 8.1. |
| ACC-SQ-002 | SQ | Claim-based activation policy refresh | SC-02 / SC-15 | Migrated from activation addendum 8.1. |
| ACC-SQ-003 | SQ | Activation scope for client vs employee accounts | SC-15 | Migrated from activation addendum 8.1. |

## 4. Grouped Behavior Items

### 4.LC — Scenario State / Condition Matrices

#### ACC-LC-002 — Non-active account cannot use protected functionality

Source: `SC-02 / SC-15`

Required behavior / guarantee:

```text
Non-active account cannot use protected client/employee functionality.
```

Failure / no-write / preservation guarantee:

```text
Protected command/read does not execute; business state unchanged.
```

Migration note:

```text
Owning scenario SC-15; related SC-02.
```

#### ACC-LC-003 — Future PendingActivation to Active flow

Source: `SC-01 / SC-15`

Required behavior / guarantee:

```text
Future activation flow may activate account.
```

Failure / no-write / preservation guarantee:

```text
Failed activation keeps account non-active.
```

Migration note:

```text
Deferred/future.
```

### 4.UCQ — Use-Case Coordination Items

#### ACC-UCQ-001 — Protected use cases require active account without polluting aggregates

Source: `SC-02 / SC-15`

Required behavior / guarantee:

```text
Protected business use cases require activated account, but business aggregates should not duplicate account activation logic internally.
```

Failure / no-write / preservation guarantee:

```text
If account is not activated, protected use case does not execute and business state remains unchanged.
```

Migration note:

```text
Migrated from account activation addendum 6.1.
```

### 4.SEC — Security / Policy Items

#### ACC-SEC-001 — Activated account required for protected functionality

Source: `SC-02 / SC-15`

Required behavior / guarantee:

```text
Activated account is required for protected client/employee functionality.
```

Failure / no-write / preservation guarantee:

```text
Non-active attempts are rejected before business state changes.
```

Migration note:

```text
Migrated from account activation addendum 7.1. SEC category exists in activation addendum.
```

### 4.SQ — Scenario Questions / Clarifications

#### ACC-SQ-001 — Non-active account login behavior

Source: `SC-02 / SC-15`

Required behavior / guarantee:

```text
If user created account but did not activate it and then tries to log in, behavior must be clarified.
```

Failure / no-write / preservation guarantee:

```text
Options: reject login; limited session; allow session but block protected actions.
```

Migration note:

```text
Migrated from activation addendum 8.1.
```

#### ACC-SQ-002 — Claim-based activation policy refresh

Source: `SC-02 / SC-15`

Required behavior / guarantee:

```text
If future AccountActivated policy uses account_activated claim, stale claim refresh behavior must be clarified.
```

Failure / no-write / preservation guarantee:

```text
Future implementation question.
```

Migration note:

```text
Migrated from activation addendum 8.1.
```

#### ACC-SQ-003 — Activation scope for client vs employee accounts

Source: `SC-15`

Required behavior / guarantee:

```text
Clarify whether activation applies identically to client and employee accounts or only client accounts in core.
```

Failure / no-write / preservation guarantee:

```text
Avoid silent assumption in protected employee flows.
```

Migration note:

```text
Migrated from activation addendum 8.1.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| REQ-READ-001 | SC-05 | READ | Client sees only own requests | Source mentions `SC-05 / SC-15` |
| AGR-READ-001 | SC-13A | READ | Client sees only own agreements/proposals | Source mentions `SC-13A / SC-13B / SC-15` |
| ACC-CMD-LOGIN-001 | SC-02 | CMD | Login with activation requirement | Source mentions `SC-02 / SC-15` |

## 6. Scenario Questions Raised

| Question ID | Affected item(s) | Question | Status |
|---|---|---|---|
| Q-SC-02-001 | ACC-SQ-001 | If user created account but did not activate it and then tries to log in, what should happen? | open |
| Q-SC-15-001 | ACC-SQ-002 | If future AccountActivated policy uses account_activated claim, how is stale claim refreshed after activation state changes? | open |
| Q-SC-15-002 | ACC-SQ-003 | Does account activation apply identically to client and employee accounts? | open |

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
