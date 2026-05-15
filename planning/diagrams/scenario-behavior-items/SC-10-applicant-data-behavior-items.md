# SC-10 — Applicant Data Behavior Items

Status: migrated v1  
Source scenario: `SC-10`  
Source baselines: `pre-domain-variants-input.md`, `scenario-behavior-baseline-account-activation-addendum.md` where applicable

## 1. Purpose

This file contains behavior items owned by this scenario.

It is used by domain drafts, slice drafts, parent slice files and `.client.md` sidecars.

Behavior items do not invent new behavior. If an item reveals missing scenario/DATA/validation detail, use the scenario question loop.

## 2. Source Set

```text
planning/diagrams/scenario-text-specs/SC-10-...
planning/diagrams/scenario-data/SC-10-...
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md, when applicable
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md, when applicable
```

## 3. Coverage Items Registry

| ID | Category | Short name | Source | Migration note |
|---|---|---|---|---|
| APPL-CMD-SAVE-001 | CMD | Save applicant data | SC-10 | Migrated from pre-domain baseline 5.4. |
| APPL-VI-001 | VI | Applicant data by applicant type | SC-10-DATA | Migrated from pre-domain baseline 8.2. |

## 4. Grouped Behavior Items

### 4.CMD — Command Behavior Cards

#### APPL-CMD-SAVE-001 — Save applicant data

Source: `SC-10`

Required behavior / guarantee:

```text
Accepted applicant data is saved as reusable applicant data owned by client context.
```

Failure / no-write / preservation guarantee:

```text
Invalid applicant data is not saved. Standalone applicant data save/edit must not start verification.
```

Migration note:

```text
Migrated from pre-domain baseline 5.4.
```

### 4.VI — Value Integrity Items

#### APPL-VI-001 — Applicant data by applicant type

Source: `SC-10-DATA`

Required behavior / guarantee:

```text
Applicant data must match required data shape for selected applicant type.
```

Failure / no-write / preservation guarantee:

```text
Invalid applicant data is not saved/accepted.
```

Migration note:

```text
Migrated from pre-domain baseline 8.2.
```

## 5. Cross-Scenario / Related Items

| Item ID | Owning scenario | Category | Short name | Why related |
|---|---|---|---|---|
| REQ-UCQ-001 | SC-04 | UCQ | Request uses saved ApplicantParty without mutating it | Source mentions `SC-04 / SC-10` |
| VER-UCQ-001 | SC-14 | UCQ | Verification is request-context-only | Source mentions `SC-10 / SC-14` |

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
