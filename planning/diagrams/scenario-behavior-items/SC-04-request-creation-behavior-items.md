# SC-04 — Request Creation Behavior Items

Status: current scenario/UI behavior items draft / per-type ApplicantParty template direction synchronized  
Source type: scenario-derived + UI-scenario-derived  
Source scenario: `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md`  
Source DATA: `planning/diagrams/scenario-data/SC-04-request-creation-data.md`  
Source UI spec: `planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md`

## 1. Purpose

These behavior items make SC-04 Request Creation behavior traceable into slice drafts, client sidecars and tests.

They are source behavior items, not implementation tasks.

## 2. Scenario Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-04-BI-001` | Signed-in client can start request creation. | SC-04 text spec | current source behavior |
| `SC-04-BI-002` | Client provides request data such as request details and object address. | SC-04 text / DATA | current source behavior |
| `SC-04-BI-003` | Request creation uses one ApplicantParty context selected or created for the request. | SC-04 text spec | target direction |
| `SC-04-BI-004` | Client does not spoof ApplicantParty outside allowed account-owned selection/creation. | SC-04 text spec | accepted direction |
| `SC-04-BI-005` | If current/default template for selected type exists, applicant fields are prefilled. | SC-04 text / UI | target direction |
| `SC-04-BI-006` | If no current/default template exists, applicant fields are empty and client enters new applicant data. | SC-04 text / UI | target direction |
| `SC-04-BI-007` | Clearing prefilled applicant fields and missing prefill both lead to the same new ApplicantParty creation path. | SC-04 text / UI | target direction |
| `SC-04-BI-008` | Accepted new applicant data creates a new ApplicantParty and uses it for the request. | SC-04 text / SC-10 relationship | target direction |
| `SC-04-BI-009` | Creating a new ApplicantParty does not overwrite, delete or deactivate existing ApplicantParties. | SC-04 text / SC-10 relationship | target direction |
| `SC-04-BI-010` | UI offers to make newly created ApplicantParty current/default template for its applicant type. | SC-04 text / UI | target direction |
| `SC-04-BI-011` | Accepted request is created in InReview state. | SC-04 text spec | current source behavior |
| `SC-04-BI-012` | Accepted request appears in My Requests and employee review queue. | SC-04 text spec | current source behavior / downstream read slices |
| `SC-04-BI-013` | Invalid request or applicant data prevents accepted request creation. | SC-04 text / validation addendum | current source behavior |

## 3. UI Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-04-UI-001` | Request creation page is available to the signed-in client. | SC-04 UI spec | requirement |
| `SC-04-UI-002` | UI shows request fields for request details and object address. | SC-04 UI/DATA | requirement |
| `SC-04-UI-003` | UI shows applicant data fields/section in the request creation journey. | SC-04 UI spec | requirement |
| `SC-04-UI-004` | Existing current/default template pre-fills applicant fields. | SC-04 UI spec | requirement |
| `SC-04-UI-005` | Client can keep prefilled applicant data. | SC-04 UI spec | requirement |
| `SC-04-UI-006` | Client can clear prefilled applicant data and enter new applicant data. | SC-04 UI spec | requirement |
| `SC-04-UI-007` | When current/default applicant template is missing, applicant fields are empty and no clear action is required. | SC-04 UI spec | requirement |
| `SC-04-UI-008` | Accepted new applicant data creates a new ApplicantParty and uses it for this request. | SC-04 UI spec | requirement |
| `SC-04-UI-009` | UI offers to make newly created ApplicantParty current/default template for its applicant type. | SC-04 UI spec | requirement |
| `SC-04-UI-010` | UI shows validation/error feedback for invalid request/applicant visible data. | SC-04 UI spec | requirement |
| `SC-04-UI-011` | Successful request submit shows success outcome and moves to the next read context. | SC-04 UI spec / CL-COMMAND-001 | requirement |
| `SC-04-UI-012` | UI does not expose arbitrary ApplicantPartyId spoofing. | SC-04 UI spec | accepted direction |
| `SC-04-UI-013` | Future UI may show all saved ApplicantParties in a dropdown/list, with current/default template as initial selection. | SC-04 UI spec | future direction |

## 4. Non-Behavior Notes

The following are implementation or planning details, not behavior items:

```text
- React component names;
- exact route file layout;
- TanStack Query keys;
- whether applicant selection is a dropdown or a separate modal;
- exact DTO mapper names;
- exact server endpoint names;
- exact set-current/default command name.
```

Those belong to `.client.md` sidecars, API docs or implementation notes.

## 5. Downstream Use

These behavior items should be consumed by:

```text
planning/slices/SL-REQ-001-create-connection-request.md
future planning/slices/SL-REQ-001-create-connection-request.client.md
future applicant template/current-default slices
future My Applicant Parties slices
future My Requests read/list/detail slices
planning/slices/slice-scenario-flow-behavior-register.md
```

Behavior Coverage in slice files should reference these IDs instead of inventing new Source BI labels.
