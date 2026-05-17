# Slice Scenario Flow / Behavior Source Register

Status: active source register / SL-APPL-003.client full sidecar synchronized

## 1. Rule

Scenario Flow and Behavior Items come from scenario source artifacts:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-behavior-items/
planning/slices/cross-cutting/
```

Scenario Flow is the part of a scenario that belongs to the current slice. It is not the whole scenario and not an implementation flow.

Behavior items are source behavior requirements. Implementation details are not behavior items.

## 2. Source Map

| Slice / sidecar | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | add ApplicantParty and first-of-type default/current initialization | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | Applicant Parties page add UI | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | flat account-level ApplicantParty list read | implemented backend |
| `SL-APPL-002-account-applicant-parties-read.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | flat read/list/card display for Applicant Parties page | draft/current depending runtime |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | explicit same-page make current/default backend command | implemented backend |
| `SL-APPL-003-select-current-default-applicant-party-template.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | same-page make current/default client action | full client sidecar draft |
| `SL-APPL-003-select-current-default-applicant-party-template.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | explicit selection, one default per type, existing requests unchanged | full client sidecar draft |
| `SL-APPL-004-applicant-party-creation-application-service.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | shared creation logic and first-of-type default/current prerequisite | implemented |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | request target flow | implemented backend |
| `SL-REQ-001-create-connection-request.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md` | request creation UI with Existing/New applicant context | implemented/drafted |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | My Requests list | implemented |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | own request details | implemented |
| `L1-MY-REQUESTS-READ-LIST.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | list UI | implemented |
| `L1-MY-REQUESTS-LIST-FILTERS.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | filter UI | implemented |
| `L1-MY-REQUEST-DETAILS.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md` | details UI | implemented |
| `CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | `[CONCERN]` | `planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | server request validation boundary | current |

## 3. Update Rule

Update this register when scenarios, behavior items, slices or sidecars are added/renamed.

When a slice introduces local behavior wording that affects future work, update scenario behavior source first or mark `Source BI TBD` explicitly.

SC-10B is a same-page future-management addendum for Applicant Parties, not a separate current user page.
