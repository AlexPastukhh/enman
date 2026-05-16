# SC-04 — Request Creation Behavior Items

Status: current scenario/UI behavior items draft  
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
| `SC-04-BI-003` | Request creation uses the account-level current active ApplicantParty at submit time. | SC-04 text spec | accepted direction |
| `SC-04-BI-004` | Client does not select or spoof ApplicantPartyId during request creation. | SC-04 text spec | accepted direction |
| `SC-04-BI-005` | If current applicant data is missing, the user provides applicant data before request submission can complete. | SC-04 text / SC-10 relationship | current source behavior |
| `SC-04-BI-006` | If current applicant data is wrong or cleared, accepted replacement applicant data becomes current active before request submission. | SC-04 text / SC-10 relationship | current source behavior / future replacement slice |
| `SC-04-BI-007` | Accepted request is created in InReview state. | SC-04 text spec | current source behavior |
| `SC-04-BI-008` | Accepted request appears in My Requests and employee review queue. | SC-04 text spec | current source behavior / downstream read slices |
| `SC-04-BI-009` | Invalid request or applicant data prevents accepted request creation. | SC-04 text / validation addendum | current source behavior |

## 3. UI Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-04-UI-001` | Request creation page is available to the signed-in client. | SC-04 UI spec | requirement |
| `SC-04-UI-002` | UI shows request fields for request details and object address. | SC-04 UI/DATA | requirement |
| `SC-04-UI-003` | UI shows applicant data fields/section in the request creation journey. | SC-04 UI spec | requirement |
| `SC-04-UI-004` | Existing current applicant data pre-fills applicant fields. | SC-04 UI spec | requirement |
| `SC-04-UI-005` | Client can keep prefilled applicant data. | SC-04 UI spec | requirement |
| `SC-04-UI-006` | Client can clear prefilled applicant data and enter new applicant data. | SC-04 UI spec | requirement |
| `SC-04-UI-007` | When current applicant data is missing, UI guides the user to provide applicant data before submit. | SC-04 UI spec | requirement |
| `SC-04-UI-008` | UI shows validation/error feedback for invalid request/applicant visible data. | SC-04 UI spec | requirement |
| `SC-04-UI-009` | Successful request submit shows success outcome and moves to the next read context. | SC-04 UI spec / CL-COMMAND-001 | requirement |
| `SC-04-UI-010` | UI does not expose ApplicantPartyId selection/submission. | SC-04 UI spec | accepted direction |

## 4. Non-Behavior Notes

The following are implementation or planning details, not behavior items:

```text
- React component names;
- exact route file layout;
- TanStack Query keys;
- whether applicant replacement is implemented by a nested form or navigation;
- exact DTO mapper names;
- exact server endpoint names.
```

Those belong to `.client.md` sidecars, API docs or implementation notes.

## 5. Downstream Use

These behavior items should be consumed by:

```text
planning/slices/SL-REQ-001-create-connection-request.md
future planning/slices/SL-REQ-001-create-connection-request.client.md
future applicant replacement/edit slice
future My Requests read/list/detail slices
planning/slices/slice-scenario-flow-behavior-register.md
```

Behavior Coverage in slice files should reference these IDs instead of inventing new Source BI labels.
