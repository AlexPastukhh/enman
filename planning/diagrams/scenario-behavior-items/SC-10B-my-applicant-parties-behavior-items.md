# SC-10B — My Applicant Parties Behavior Items

Status: future scenario/UI behavior items draft  
Source type: scenario-derived + UI-scenario-derived  
Source scenario: `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md`  
Source DATA: `planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md`  
Source UI spec: `planning/diagrams/scenario-ui-specs/SC-10B-my-applicant-parties-ui.md`

## 1. Purpose

These behavior items preserve future My Applicant Parties management behavior for later slice/client planning.

They are source behavior items, not implementation tasks.

## 2. Scenario Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-10B-BI-001` | Client can view saved ApplicantParty profiles for the account. | SC-10B text spec | future behavior |
| `SC-10B-BI-002` | Client can view details of a saved ApplicantParty. | SC-10B text spec | future behavior |
| `SC-10B-BI-003` | Client can add ApplicantParty of supported applicant type. | SC-10B text spec | future behavior |
| `SC-10B-BI-004` | Client can set one ApplicantParty as current/default template for its applicant type. | SC-10B text spec | future behavior |
| `SC-10B-BI-005` | Setting current/default does not delete or overwrite other ApplicantParties. | SC-10B text spec | future behavior |
| `SC-10B-BI-006` | Delete/archive behavior is safety-checked and warning-driven. | SC-10B text spec | future behavior |
| `SC-10B-BI-007` | ApplicantParty used by requests or approved requests may require archive/hide instead of hard delete. | SC-10B text spec | future review |

## 3. UI Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-10B-UI-001` | My Applicant Parties management area is reachable. | SC-10B UI spec | future requirement |
| `SC-10B-UI-002` | UI shows saved ApplicantParty list. | SC-10B UI spec | future requirement |
| `SC-10B-UI-003` | UI shows verification status and current/default marker. | SC-10B UI spec | future requirement |
| `SC-10B-UI-004` | UI provides add ApplicantParty action. | SC-10B UI spec | future requirement |
| `SC-10B-UI-005` | UI provides view details action. | SC-10B UI spec | future requirement |
| `SC-10B-UI-006` | UI provides set-current/default action. | SC-10B UI spec | future requirement |
| `SC-10B-UI-007` | UI warns before dangerous delete/archive. | SC-10B UI spec | future requirement |

## 4. Downstream Use

These behavior items should be consumed by future My Applicant Parties backend/client slices.

Do not use them to claim current implementation exists.
