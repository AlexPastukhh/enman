# Slice Scenario Flow / Behavior Source Register

Status: active source register / client short-draft rules and ApplicantParty read sidecar synchronized

## 1. Rule

Scenario Flow and Behavior Items come from scenario source artifacts:

```text
[SCENARIO] planning/diagrams/scenario-text-specs/
[DATA] planning/diagrams/scenario-data/
[UI-SCENARIO] planning/diagrams/scenario-ui-specs/
[BEHAVIOR] planning/diagrams/scenario-behavior-items/
[CONCERN] planning/slices/cross-cutting/
```

Scenario Flow is the part of the scenario that belongs to the current slice.

It is not the whole scenario unless the slice owns the whole scenario.

A scenario may be implemented by multiple slices and extension slices.

Do not put implementation details into Scenario Flow.

Do not invent behavior items locally inside a slice when source artifacts exist.

## 2. Source Map

| Slice / sidecar | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | add ApplicantParty and first-of-type default/current initialization | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | add ApplicantParty action/form on Applicant Parties page | current/target |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | flat account-level ApplicantParty list read | implementation-ready |
| `SL-APPL-002-account-applicant-parties-read.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | current/default top area, other saved cards, empty read state | draft/implementation handoff |
| `SL-APPL-002-account-applicant-parties-read.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | read behavior coverage for saved/current/default cards | draft/implementation handoff |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | future same-page explicit default/current action | planned |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | My Requests list | current |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | own request details | current |
| `L1-MY-REQUESTS-READ-LIST.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | My Requests read list UI | current |
| `L1-MY-REQUESTS-LIST-FILTERS.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | My Requests status filter UI | current |
| `L1-MY-REQUEST-DETAILS.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | My Request details UI | current |
| `CC-API-001-openapi-contract-artifacts-and-type-generation.md` | `[CONCERN]` | `planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md` | OpenAPI/generated artifact workflow | current |
| `CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | `[CONCERN]` | `planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | server request validation boundary | current |

## 3. Update Rule

Update this register when scenarios, behavior items, slices or sidecars are added/renamed.

When a slice introduces local behavior wording that affects future work, create/update the scenario behavior item source first or mark `Source BI TBD` explicitly.

When a slice changes API contract direction but not scenario behavior, sync API docs, questions register and implementation notes instead of inventing behavior items.
