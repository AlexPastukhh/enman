# Slice Scenario Flow / Behavior Source Register

Status: active source register / validation, My Requests filters and ApplicantParty one-page direction synchronized

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
| `SL-APPL-001-create-individual-applicant-party.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | additive create / no replacement / no silent default switch | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | add ApplicantParty action/form on same Applicant Parties page | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | one-page ApplicantParty UI behavior coverage | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | read default/current templates + other saved ApplicantParties for one page | planned |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | same-page future management addendum, not separate current page | future |
| `SL-APPL-002-account-applicant-parties-read.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | page read behavior / no request rewrite | current target |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | explicit make default/current action on same Applicant Parties page | planned |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | future explicit default selection / existing requests unchanged | future |
| `SL-APPL-004-applicant-party-creation-application-service.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | shared additive creation logic | planned |
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

ApplicantParty delete/archive lifecycle remains future extension pressure only until a dedicated lifecycle slice exists.
