# SC-10 — Applicant Data Behavior Items

Status: current scenario/UI behavior items draft / applicant template per type policy synchronized  
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
| `SC-10-BI-001` | Client can provide applicant data for a selected applicant type. | SC-10 text spec | current source behavior |
| `SC-10-BI-002` | Account can store multiple saved ApplicantParty profiles over time. | SC-10 text spec | target direction |
| `SC-10-BI-003` | At most one saved ApplicantParty per applicant type can be current/default for future prefill. | SC-10 text spec / DATA | target direction |
| `SC-10-BI-004` | Accepted new applicant data creates a new ApplicantParty profile. | SC-10 text spec | target direction |
| `SC-10-BI-005` | New ApplicantParty starts as NotVerified. | SC-10 text spec | target direction |
| `SC-10-BI-006` | Adding new ApplicantParty does not overwrite, delete or deactivate existing ApplicantParties. | SC-10 text spec | target direction |
| `SC-10-BI-007` | Existing requests keep the applicant context used at submission time. | SC-10 text spec / request history policy | target direction / future implementation policy |
| `SC-10-BI-008` | Invalid applicant data is not saved. | SC-10 text spec / validation addendum | current source behavior |
| `SC-10-BI-009` | Applicant verification happens in request/review context, not standalone applicant data entry. | SC-10 text spec | accepted direction |
| `SC-10-BI-010` | Future My Applicant Parties can list/manage saved ApplicantParty profiles. | SC-10B relationship | future scenario |

Superseded behavior items:

| Previous item | Status | Reason |
|---|---|---|
| `SC-10-BI-002` previous meaning: applicant types are alternative data shapes for one account-level applicant profile | superseded | Target direction now supports multiple saved ApplicantParty profiles with current/default per type. |
| `SC-10-BI-003` previous meaning: accepted applicant data becomes account's single current active ApplicantParty | superseded | New applicant data creates a saved profile and may be offered as current/default for its type. |
| `SC-10-BI-006` previous meaning: replacement makes previous current non-current | superseded | Adding new ApplicantParty does not delete or overwrite existing profiles; current/default is per-type template only. |

## 3. UI Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-10-UI-001` | Authenticated client can reach an Account page / applicant section that owns applicant data presentation. | SC-10 UI spec | accepted direction |
| `SC-10-UI-002` | UI can show current/default ApplicantParty template for a supported applicant type. | SC-10 UI spec | target direction |
| `SC-10-UI-003` | If current/default template for a type is missing, UI can show an empty applicant form/add action for that type. | SC-10 UI spec | target direction |
| `SC-10-UI-004` | For current narrow individual applicant flow, UI collects full name, email and phone number. | SC-10 UI spec / current backend DTO | current implementation alignment |
| `SC-10-UI-005` | After successful applicant data save, UI shows saved applicant data. | SC-10 UI spec | accepted direction |
| `SC-10-UI-006` | After successful applicant data save, UI shows NotVerified status when status is available. | SC-10 UI spec | target direction |
| `SC-10-UI-007` | UI offers to make newly saved ApplicantParty current/default for its applicant type. | SC-10 UI spec | target direction |
| `SC-10-UI-008` | Applicant data create UI does not introduce a create-request entry point. | SC-10 UI spec | accepted direction |
| `SC-10-UI-009` | Future request creation may show all saved ApplicantParties in a dropdown, with current/default as initial prefill/default selection. | SC-10 UI / SC-04 UI | future request client direction |
| `SC-10-UI-010` | Future My Applicant Parties can show details/add/edit/delete/archive/set-current-default actions. | SC-10B UI spec | future scenario |

## 4. Non-Behavior Notes

The following are implementation or planning details, not behavior items:

```text
- React component names;
- exact route file layout;
- TanStack Query keys;
- feature folder names;
- exact set-current/default mutation name;
- whether request details store applicant snapshot or immutable version;
- exact server endpoint names.
```

Those belong to `.client.md` sidecars, API docs, slice files or implementation notes.

## 5. Downstream Use

These behavior items should be consumed by:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
future `SL-APPL-001-create-individual-applicant-party.client.md`
future current/default applicant template read slice
future My Applicant Parties slices
future applicant edit/delete/archive slice
future request creation client sidecar
```

Temporary `Source BI TBD` labels in client drafts should be replaced with these IDs when the related client sidecar is finalized.
