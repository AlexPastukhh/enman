# SC-04 — Request Creation Behavior Items

Status: current scenario/UI behavior items draft / synchronized with ApplicantParty template-per-type model  
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
| `SC-04-BI-003` | Request creation uses one accepted applicant context. | SC-04 text spec | accepted direction |
| `SC-04-BI-004` | If current/default ApplicantParty exists for selected type, request applicant fields can be prefilled from it. | SC-04 text / UI | accepted direction |
| `SC-04-BI-005` | If current/default ApplicantParty is missing, applicant fields start empty and the user enters new applicant data. | SC-04 text / UI | accepted direction |
| `SC-04-BI-006` | Client can clear prefilled applicant data and enter new applicant data. | SC-04 text / UI | accepted direction |
| `SC-04-BI-007` | Missing default and cleared prefill both lead to the same new-applicant-data path. | SC-04 text / UI | accepted direction |
| `SC-04-BI-008` | Accepted new applicant data creates a new ApplicantParty and uses it for this request. | SC-04 text / SC-10 relationship | target direction |
| `SC-04-BI-009` | Creating new ApplicantParty does not replace or overwrite existing ApplicantParties. | SC-04 text / SC-10 | accepted direction |
| `SC-04-BI-010` | Request creation must not let the client spoof ApplicantParty ownership. | SC-04 text spec | accepted direction |
| `SC-04-BI-011` | Accepted request is created in InReview state. | SC-04 text spec | current source behavior |
| `SC-04-BI-012` | Accepted request appears in My Requests and employee review queue. | SC-04 text spec | downstream read behavior |
| `SC-04-BI-013` | Invalid request or applicant data prevents accepted request creation. | SC-04 text / validation addendum | current source behavior |

## 3. UI Behavior Items

| ID | Behavior | Source | Status |
|---|---|---|---|
| `SC-04-UI-001` | Request creation page is available to the signed-in client. | SC-04 UI spec | requirement |
| `SC-04-UI-002` | UI shows request fields for request details and object address. | SC-04 UI/DATA | requirement |
| `SC-04-UI-003` | UI shows applicant data fields/section in the request creation journey. | SC-04 UI spec | requirement |
| `SC-04-UI-004` | Existing current/default ApplicantParty pre-fills applicant fields. | SC-04 UI spec | requirement |
| `SC-04-UI-005` | Missing current/default shows empty applicant fields. | SC-04 UI spec | requirement |
| `SC-04-UI-006` | Client can keep prefilled applicant data. | SC-04 UI spec | requirement |
| `SC-04-UI-007` | Client can clear prefilled applicant data and enter new applicant data. | SC-04 UI spec | requirement |
| `SC-04-UI-008` | Cleared prefill and missing default use the same new-applicant-data path. | SC-04 UI spec | requirement |
| `SC-04-UI-009` | New applicant data creates ApplicantParty and uses it for request. | SC-04 UI spec | target direction |
| `SC-04-UI-010` | UI may offer to make new ApplicantParty current/default for future requests when a default already exists. | SC-04 UI spec | future review |
| `SC-04-UI-011` | Future UI may allow choosing from all saved ApplicantParties via dropdown/list. | SC-04 UI spec | future review |
| `SC-04-UI-012` | UI shows validation/error feedback for invalid request/applicant visible data. | SC-04 UI spec | requirement |
| `SC-04-UI-013` | Successful request submit shows success outcome and moves to next read context. | SC-04 UI spec / CL-COMMAND-001 | requirement |
| `SC-04-UI-014` | UI does not expose cross-account ApplicantParty ownership selection/submission. | SC-04 UI spec | accepted direction |

## 4. Non-Behavior Notes

The following are implementation or planning details, not behavior items:

```text
- React component names;
- exact route file layout;
- TanStack Query keys;
- exact applicantContextType DTO shape;
- exact server endpoint names;
- exact shared application service class names.
```

Those belong to `.client.md` sidecars, API docs, slice implementation notes or implementation tasks.

## 5. Downstream Use

These behavior items should be consumed by:

```text
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REQ-004-create-request-with-applicant-context.md
future planning/slices/SL-REQ-001-create-connection-request.client.md
planning/slices/slice-scenario-flow-behavior-register.md
```

Behavior Coverage in slice files should reference these IDs instead of inventing new Source BI labels.
