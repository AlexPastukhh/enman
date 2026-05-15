# SC-04 — Client Request Creation Behavior Items

Status: migrated v1  
Source scenario: `SC-04`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-04-...
planning/diagrams/scenario-data/SC-04-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| REQ-CMD-CREATE-001 | CMD | Create request | SC-04 | Migrated from pre-domain baseline 5.5; wording aligned with current InReview direction. |
| REQ-LC-001 | LC | Request creation creates InReview | SC-04 | Migrated from pre-domain baseline 6.1. |
| REQ-IBS-003 | IBS | Request has object address | SC-04 | Migrated from pre-domain baseline 7.1. |
| REQ-VI-001 | VI | Object address value integrity | SC-04-DATA | Migrated from pre-domain baseline 8.1. |
| REQ-UCQ-001 | UCQ | Request uses saved ApplicantParty without mutating it | SC-04 / SC-10 | Migrated from baseline; naming aligned with current ApplicantParty term. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### REQ-CMD-CREATE-001 — Create request

Source: `SC-04`

Required behavior / guarantee:

```text
A valid request creation creates a persisted request with status InReview, object address, request details and applicant party reference / request-local applicant data as defined by current scenario direction.
```

Failure / no-write / preservation guarantee:

```text
If request data is invalid, no request is created. Saved ApplicantParty data is not mutated by failed or edited request-local data.
```

Migration note:

```text
Migrated from pre-domain baseline 5.5; wording aligned with current InReview direction.
```

### 4.LC — Scenario State / Condition Matrices

#### REQ-LC-001 — Request creation creates InReview

Source: `SC-04`

Required behavior / guarantee:

```text
Successful request creation creates request with status InReview.
```

Failure / no-write / preservation guarantee:

```text
Invalid request creates no request.
```

Migration note:

```text
Migrated from pre-domain baseline 6.1.
```

### 4.IBS — Impossible Business State Candidates

#### REQ-IBS-003 — Request has object address

Source: `SC-04`

Required behavior / guarantee:

```text
Request without object address must not exist.
```

Failure / no-write / preservation guarantee:

```text
Invalid address prevents request creation.
```

Migration note:

```text
Migrated from pre-domain baseline 7.1.
```

### 4.VI — Value Integrity Items

#### REQ-VI-001 — Object address value integrity

Source: `SC-04-DATA`

Required behavior / guarantee:

```text
Request must not be accepted with missing/structurally invalid object address.
```

Failure / no-write / preservation guarantee:

```text
Invalid object address prevents request creation.
```

Migration note:

```text
Migrated from pre-domain baseline 8.1.
```

### 4.UCQ — Use-Case Coordination Items

#### REQ-UCQ-001 — Request uses saved ApplicantParty without mutating it

Source: `SC-04 / SC-10`

Required behavior / guarantee:

```text
Request creation may use saved ApplicantParty as source/reference, but request-local edits must not implicitly mutate saved ApplicantParty.
```

Failure / no-write / preservation guarantee:

```text
Failed/edited request-local data leaves saved ApplicantParty unchanged.
```

Migration note:

```text
Migrated from baseline; naming aligned with current ApplicantParty term.
```

## 5. Cross-Scenario / Related Items

No related cross-scenario items identified in this migration pass.

## 6. Scenario Questions Raised

| Question ID | Affected item(s) | Question | Status |
|---|---|---|---|
| Q-SC-04-001 | REQ-UCQ-001 | Which ApplicantParty data is copied/prefilled into request creation form, and which fields are request-local only? | open |

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
