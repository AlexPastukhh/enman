# SC-14 — Client Data Verification Behavior Items

Status: migrated v1  
Doc version: v0.1.0  
Source scenario: `SC-14`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-14-...
planning/diagrams/scenario-data/SC-14-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| VER-CMD-START-001 | CMD | Start request-context verification | SC-14 | Migrated from pre-domain baseline 5.13; deferred/future. |
| VER-UCQ-001 | UCQ | Verification is request-context-only | SC-10 / SC-14 | Migrated from pre-domain baseline 9.3; deferred/future. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### VER-CMD-START-001 — Start request-context verification

Source: `SC-14`

Required behavior / guarantee:

```text
Verification can be started only in request context and only by employee/future review action.
```

Failure / no-write / preservation guarantee:

```text
Verification cannot start without request context. Standalone ApplicantParty save/edit creates no verification state.
```

Migration note:

```text
Migrated from pre-domain baseline 5.13; deferred/future.
```

### 4.UCQ — Use-Case Coordination Items

#### VER-UCQ-001 — Verification is request-context-only

Source: `SC-10 / SC-14`

Required behavior / guarantee:

```text
Verification is request-context-only and is not triggered by standalone ApplicantParty save/edit.
```

Failure / no-write / preservation guarantee:

```text
Standalone applicant data save/edit creates no verification state.
```

Migration note:

```text
Migrated from pre-domain baseline 9.3; deferred/future.
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
