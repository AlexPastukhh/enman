# Slice Scenario Flow / Behavior Source Register

Status: active source register / My Requests sidecars synchronized

## 1. Rule

Scenario Flow and Behavior Items come from scenario source artifacts:

```text
[SCENARIO] planning/diagrams/scenario-text-specs/
[DATA] planning/diagrams/scenario-data/
[UI-SCENARIO] planning/diagrams/scenario-ui-specs/
[BEHAVIOR] planning/diagrams/scenario-behavior-items/
[CONCERN] planning/slices/cross-cutting/
```

Questions/extension/implementation registers do not replace source files.

## 2. Source Map

| Slice / sidecar | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | add ApplicantParty | current |
| `SL-APPL-001-create-individual-applicant-party.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | coverage | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | Account page UI | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | read templates/list | planned |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | future management read | planned |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | explicit default selection | planned |
| `SL-APPL-004-applicant-party-creation-application-service.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | shared creation logic | planned |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | request target flow | current target |
| `SL-REQ-001-create-connection-request.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md` | coverage | current target |
| `SL-REQ-001-create-connection-request.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md` | future request UI | planned |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | My Requests list | implemented backend |
| `SL-REQ-002-my-requests-list.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-05-my-requests-data.md` | summary/status/filter data | implemented backend |
| `SL-REQ-002-my-requests-list.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | list behavior | implemented backend |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | own request details | implemented backend |
| `SL-REQ-003-own-request-details.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-05-my-requests-data.md` | details data | implemented backend |
| `SL-REQ-003-own-request-details.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | details behavior | implemented backend |
| `L1-MY-REQUESTS-READ-LIST.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | list UI | first-stage implemented client |
| `L1-MY-REQUESTS-READ-LIST.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | list behavior | first-stage implemented client |
| `L1-MY-REQUESTS-LIST-FILTERS.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | filter UI | implementation-ready client |
| `L1-MY-REQUESTS-LIST-FILTERS.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | filter behavior | implementation-ready client |
| `L1-MY-REQUEST-DETAILS.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | details UI | implementation-ready client |
| `L1-MY-REQUEST-DETAILS.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md` | details behavior | implementation-ready client |

## 3. Update Rule

Update this register when scenarios, behavior items, slices or sidecars are added/renamed.

If a sidecar behavior item is only written inside the sidecar, that is a source gap. Move or mirror it into a scenario behavior item file when it is scenario/UI behavior.
