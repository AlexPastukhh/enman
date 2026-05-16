# Slice Scenario Flow / Behavior Source Register

Status: active source register / ApplicantParty flat-list read model, validation and My Requests filters synchronized

## 1. Rule

Scenario Flow and Behavior Items come from scenario source artifacts:

```text
[SCENARIO] planning/diagrams/scenario-text-specs/
[DATA] planning/diagrams/scenario-data/
[UI-SCENARIO] planning/diagrams/scenario-ui-specs/
[BEHAVIOR] planning/diagrams/scenario-behavior-items/
[CONCERN] planning/slices/cross-cutting/
```

Do not invent scenario flow or behavior items locally inside a slice when source artifacts exist.

## 2. Source Map

| Slice / sidecar | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | add ApplicantParty on one Applicant Parties page / section | current |
| `SL-APPL-001-create-individual-applicant-party.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | additive create/default rules/existing requests unchanged | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | Applicant Parties page UI add flow | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | account-owned flat ApplicantParty list read model | current target |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | same-page future management context | future addendum |
| `SL-APPL-002-account-applicant-parties-read.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | multiple saved parties/default marker/existing requests unchanged | current target |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | explicit same-page default/current selection | planned |
| `SL-APPL-004-applicant-party-creation-application-service.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | shared creation logic | planned |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | request target flow | current target |
| `SL-REQ-001-create-connection-request.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md` | coverage | current target |
| `SL-REQ-001-create-connection-request.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md` | future request UI | planned |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | My Requests list | current |
| `SL-REQ-002-my-requests-list.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-05-my-requests-data.md` | list summary/status/filter DATA | current |
| `SL-REQ-002-my-requests-list.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | list behavior coverage | current |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | own details | current |
| `SL-REQ-003-own-request-details.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-05-my-requests-data.md` | details DATA | current |
| `SL-REQ-003-own-request-details.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | details behavior coverage | current |
| `L1-MY-REQUESTS-READ-LIST.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | list UI | current |
| `L1-MY-REQUESTS-READ-LIST.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | list client behavior | current |
| `L1-MY-REQUESTS-LIST-FILTERS.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | filter UI | current |
| `L1-MY-REQUESTS-LIST-FILTERS.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | filter behavior | current |
| `L1-MY-REQUEST-DETAILS.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | details UI | current |
| `L1-MY-REQUEST-DETAILS.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | details behavior | current |
| `CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | `[CONCERN]` | `planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | server request validation boundary | current |

## 3. Update Rule

Update this register when scenarios, behavior items, slices or sidecars are added/renamed.

When a slice introduces local behavior wording that affects future work, create/update the scenario behavior item source first or mark `Source BI TBD` explicitly.

When a slice changes API contract direction but not scenario behavior, sync the relevant API docs, slice questions register and implementation notes register rather than inventing behavior items.
