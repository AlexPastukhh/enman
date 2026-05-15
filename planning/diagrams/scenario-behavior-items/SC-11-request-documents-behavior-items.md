# SC-11 — Request Documents Behavior Items

Status: migrated v1  
Source scenario: `SC-11`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-11-...
planning/diagrams/scenario-data/SC-11-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| DOC-CMD-ATTACH-001 | CMD | Attach request document | SC-11 | Migrated from pre-domain baseline 5.8. |
| FILE-INT-001 | INT | File/blob storage is infrastructure | SC-11 / SC-13B / SC-13D | Owning scenario SC-11; related agreements. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### DOC-CMD-ATTACH-001 — Attach request document

Source: `SC-11`

Required behavior / guarantee:

```text
Accepted document/file reference becomes attached to request context.
```

Failure / no-write / preservation guarantee:

```text
Invalid/rejected document is not attached. Physical file/blob storage failure does not create accepted domain attachment state.
```

Migration note:

```text
Migrated from pre-domain baseline 5.8.
```

### 4.INT — Integration / Infrastructure Items

#### FILE-INT-001 — File/blob storage is infrastructure

Source: `SC-11 / SC-13B / SC-13D`

Required behavior / guarantee:

```text
Physical file/blob content is not the same as accepted domain document/reference state.
```

Failure / no-write / preservation guarantee:

```text
Storage failure does not create accepted domain attachment/reference state.
```

Migration note:

```text
Owning scenario SC-11; related agreements.
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
