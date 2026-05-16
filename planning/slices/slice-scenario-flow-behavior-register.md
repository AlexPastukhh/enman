# Slice Scenario Flow / Behavior Source Register

Status: active source register / applicant-template-per-type synchronized

## 1. Rule

Scenario Flow and Behavior Items come from scenario source artifacts:

```text
[SCENARIO] planning/diagrams/scenario-text-specs/
[DATA] planning/diagrams/scenario-data/
[UI-SCENARIO] planning/diagrams/scenario-ui-specs/
[BEHAVIOR] planning/diagrams/scenario-behavior-items/
```

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

## 3. Update Rule

Update this register when scenarios, behavior items, slices or sidecars are added/renamed.
