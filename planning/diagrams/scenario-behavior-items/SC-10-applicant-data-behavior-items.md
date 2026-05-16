# SC-10 — Applicant Data Behavior Items

Status: current scenario/UI behavior items draft / synchronized with ApplicantParty template-per-type model  
Source type: scenario-derived + UI-scenario-derived  
Source scenario: `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md`  
Source DATA: `planning/diagrams/scenario-data/SC-10-applicant-data.md`  
Source UI spec: `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md`

## 1. Purpose

These behavior items make SC-10 Applicant Data behavior traceable into domain, slice, client and testing planning.

They are source behavior items, not implementation tasks.

## 2. Scenario Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-10-BI-001` | Client can add applicant data and create a saved ApplicantParty linked to the account. | SC-10 text spec | current/target source behavior |
| `SC-10-BI-002` | A client account may store many ApplicantParties over time. | SC-10 text spec | accepted direction |
| `SC-10-BI-003` | Adding a new ApplicantParty does not delete, overwrite, deactivate or replace existing ApplicantParties. | SC-10 text spec | accepted direction |
| `SC-10-BI-004` | New ApplicantParty starts as NotVerified. | SC-10 text spec | accepted direction |
| `SC-10-BI-005` | One current/default ApplicantParty template may exist per applicant type. | SC-10 text spec | accepted direction |
| `SC-10-BI-006` | First ApplicantParty of a type may initialize the current/default template for that type. | SC-10 text spec | accepted direction |
| `SC-10-BI-007` | Creating an additional ApplicantParty of the same type does not silently change the existing current/default template. | SC-10 text spec | accepted direction |
| `SC-10-BI-008` | Changing current/default when one already exists is an explicit future behavior. | SC-10 text spec | future slice |
| `SC-10-BI-009` | Applicant contact email may differ from account email. | SC-10 DATA / accepted direction | accepted direction |
| `SC-10-BI-010` | Invalid applicant data is not saved. | SC-10 text spec / validation addendum | current source behavior |

## 3. UI Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-10-UI-001` | Account page can show Applicant Parties section. | SC-10 UI spec | requirement |
| `SC-10-UI-002` | Account page can show current/default templates grouped by applicant type. | SC-10 UI spec | target direction |
| `SC-10-UI-003` | Current/default template is visually distinguished. | SC-10 UI spec | target direction |
| `SC-10-UI-004` | Account page can show all saved ApplicantParties, including non-default ones. | SC-10 UI spec | target direction |
| `SC-10-UI-005` | Client can add new individual ApplicantParty through visible applicant data form. | SC-10 UI spec | current/narrow implementation alignment |
| `SC-10-UI-006` | Created ApplicantParty appears in saved ApplicantParties after update/refetch. | SC-10 UI spec | target direction |
| `SC-10-UI-007` | If no default exists for the type, created ApplicantParty may appear as initial default. | SC-10 UI spec | target direction |
| `SC-10-UI-008` | If default already exists for the type, creating another ApplicantParty does not silently switch the visible default. | SC-10 UI spec | accepted direction |
| `SC-10-UI-009` | UI refetches ApplicantParties after create to reconcile server-truth saved list/default state. | SC-10 UI spec | client convention direction |
| `SC-10-UI-010` | ApplicantParty details can be displayed inline; separate details page is not required by current direction. | SC-10 UI spec | accepted direction |

## 4. Non-Behavior Notes

The following are implementation or planning details, not source behavior items:

```text
- exact React component names;
- exact query keys;
- whether React Query optimistic update is used;
- exact API response field names;
- whether creation logic lives in a service;
- exact persistence/repository method names.
```

Those belong to slice files, `.client.md` sidecars, implementation notes or API docs.

## 5. Superseded Behavior Wording

Superseded source wording:

```text
- one current active ApplicantParty per account;
- applicant replacement makes previous current ApplicantParty non-current;
- creating ApplicantParty is replacement by default.
```

Use instead:

```text
many saved ApplicantParties + one current/default template per applicant type.
```

## 6. Downstream Use

These behavior items should be consumed by:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
future request creation client sidecar
planning/slices/slice-scenario-flow-behavior-register.md
```
