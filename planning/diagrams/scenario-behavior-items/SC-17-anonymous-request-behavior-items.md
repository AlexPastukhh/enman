# SC-17 — Anonymous Request Behavior Items

Status: migrated v1  
Source scenario: `SC-17`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-17-...
planning/diagrams/scenario-data/SC-17-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| ANON-CMD-SUBMIT-001 | CMD | Submit anonymous request/contact | SC-17 | Migrated from pre-domain baseline 5.14; deferred/question. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### ANON-CMD-SUBMIT-001 — Submit anonymous request/contact

Source: `SC-17`

Required behavior / guarantee:

```text
Anonymous submission is recorded only if required request/contact data and follow-up contact data are accepted.
```

Failure / no-write / preservation guarantee:

```text
Invalid contact/request data is not recorded.
```

Migration note:

```text
Migrated from pre-domain baseline 5.14; deferred/question.
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
