# SC-02 — Login Behavior Items

Status: migrated v1  
Source scenario: `SC-02`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-02-...
planning/diagrams/scenario-data/SC-02-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| ACC-CMD-LOGIN-001 | CMD | Login with activation requirement | SC-02 / SC-15 | Migrated from account activation addendum 4.1. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### ACC-CMD-LOGIN-001 — Login with activation requirement

Source: `SC-02 / SC-15`

Required behavior / guarantee:

```text
Valid credentials and activated account allow protected app access.
```

Failure / no-write / preservation guarantee:

```text
Invalid credentials do not issue session/protected access. Non-active account must not gain protected functionality.
```

Migration note:

```text
Migrated from account activation addendum 4.1.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| ACC-LC-002 | SC-15 | LC | Non-active account cannot use protected functionality | Source mentions `SC-02 / SC-15` |
| ACC-UCQ-001 | SC-15 | UCQ | Protected use cases require active account without polluting aggregates | Source mentions `SC-02 / SC-15` |
| ACC-SEC-001 | SC-15 | SEC | Activated account required for protected functionality | Source mentions `SC-02 / SC-15` |
| ACC-SQ-001 | SC-15 | SQ | Non-active account login behavior | Source mentions `SC-02 / SC-15` |
| ACC-SQ-002 | SC-15 | SQ | Claim-based activation policy refresh | Source mentions `SC-02 / SC-15` |

## 6. Scenario Questions Raised

| Question ID | Affected item(s) | Question | Status |
|---|---|---|---|
| Q-SC-02-001 | ACC-SQ-001 | If user created account but did not activate it and then tries to log in, what should happen? | open |
| Q-SC-15-001 | ACC-SQ-002 | If future AccountActivated policy uses account_activated claim, how is stale claim refreshed after activation state changes? | open |

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
