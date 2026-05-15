# SC-10 — Applicant Data Behavior Items

Status: current scenario/UI behavior items draft  
Source type: scenario-derived  
Source scenario: `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md`  
Source DATA: `planning/diagrams/scenario-data/SC-10-applicant-data.md`  
Source UI spec: `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md`

## 1. Purpose

These behavior items make SC-10 Applicant Data behavior traceable into domain, slice, client and testing planning.

They are source behavior items, not implementation tasks.

## 2. Scenario Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-10-BI-001` | Client can provide applicant data for the account-level applicant profile. | SC-10 text spec | current source behavior |
| `SC-10-BI-002` | Applicant types are alternative data shapes for one account-level applicant profile, not simultaneously active applicant contexts. | SC-10 text spec / questions register | accepted direction |
| `SC-10-BI-003` | Accepted applicant data becomes the account's current active ApplicantParty. | SC-10 text spec | accepted direction |
| `SC-10-BI-004` | Invalid applicant data is not saved. | SC-10 text spec / validation addendum | current source behavior |
| `SC-10-BI-005` | Saved current active applicant data is reusable by request creation. | SC-10 text spec / SC-04 relationship | accepted direction |
| `SC-10-BI-006` | Future applicant replacement should make newly accepted applicant data current and previous current applicant data non-current. | SC-10 text spec | future slice behavior |

## 3. UI Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-10-UI-001` | Authenticated client can reach an Account page / applicant section that owns applicant data presentation. | SC-10 UI spec | accepted direction |
| `SC-10-UI-002` | If current applicant data is missing, the UI shows an editable applicant data form. | SC-10 UI spec | accepted direction |
| `SC-10-UI-003` | For the current narrow individual applicant flow, the UI collects full name, email and phone number. | SC-10 UI spec / current backend DTO | current implementation alignment |
| `SC-10-UI-004` | After successful applicant data save, the UI shows applicant data as filled/read-only. | SC-10 UI spec | accepted direction |
| `SC-10-UI-005` | After successful applicant data save, the UI shows a self-dismissing success notification. | SC-10 UI spec | accepted direction |
| `SC-10-UI-006` | After successful applicant data save, the UI shows an Edit action. | SC-10 UI spec | accepted direction; edit flow future |
| `SC-10-UI-007` | Applicant data create UI does not introduce a create-request entry point. | SC-10 UI spec | accepted direction |
| `SC-10-UI-008` | Account page should eventually load current applicant data from a current-applicant read model for stable refresh behavior. | SC-10 UI spec | future read slice |
| `SC-10-UI-009` | Future Account page may show applicant verification state when available. | SC-10 UI spec | future review |

## 4. Non-Behavior Notes

The following are implementation or planning details, not behavior items:

```text
- React component names;
- exact route file layout;
- TanStack Query keys;
- feature folder names;
- whether a mutation stores returned applicantPartyId;
- whether a session query is invalidated.
```

Those belong to `.client.md` sidecars or implementation notes.

## 5. Downstream Use

These behavior items should be consumed by:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
future `SL-APPL-001-create-individual-applicant-party.client.md`
future current-applicant read slice
future applicant replacement/edit slice
future request creation client sidecar
```

Temporary `Source BI TBD` labels in client drafts should be replaced with these IDs when the related client sidecar is finalized.
