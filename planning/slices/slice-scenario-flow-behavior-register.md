# Slice Scenario Flow / Behavior Source Register

Status: active source register / ApplicantParty template-per-type model synchronized  
Scope: maps each active/planned slice or `.client.md` sidecar to scenario flow, DATA, UI scenario and behavior item sources it must consume

## 1. Purpose

Slice/client chats must not invent Scenario Flow or Behavior Items from local questions, extension notes or implementation notes.

This register answers:

```text
For this slice/client sidecar, where do Scenario Flow and Behavior Items come from?
```

## 2. Core Rule

Scenario Flow and Behavior Items come from scenario source artifacts:

```text
[SCENARIO]      planning/diagrams/scenario-text-specs/
[DATA]          planning/diagrams/scenario-data/
[UI-SCENARIO]   planning/diagrams/scenario-ui-specs/
[BEHAVIOR]      planning/diagrams/scenario-behavior-items/
[SECURITY]      scenario security/validation addenda
[CONCERN]       cross-cutting concern behavior items
[CLARIFICATION] scenario clarifications
[SOURCE-GAP]    source missing/not attached yet
```

They do not come from:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

## 3. Source Map

| Slice / sidecar | Source marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | standalone applicant create scenario flow | current |
| `SL-APPL-001-create-individual-applicant-party.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-10-applicant-data.md` | applicant DATA/default template DATA | current |
| `SL-APPL-001-create-individual-applicant-party.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | Account page applicant UI behavior | current |
| `SL-APPL-001-create-individual-applicant-party.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | behavior coverage | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | Account Applicant Parties read flow | planned |
| `SL-APPL-002-account-applicant-parties-read.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-10-applicant-data.md` | saved list/defaults DATA | planned |
| `SL-APPL-002-account-applicant-parties-read.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | Account page defaults/list UI | planned |
| `SL-APPL-002-account-applicant-parties-read.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | behavior coverage | planned |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | explicit default selection | planned |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md` | default selection data | planned |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10B-my-applicant-parties-ui.md` | default selection UI behavior | planned |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md` | behavior coverage | planned |
| `SL-APPL-004-applicant-party-creation-application-service.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | helper service supports applicant creation | planned |
| `SL-APPL-004-applicant-party-creation-application-service.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | helper service supports request creation with new applicant | planned |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | current/future request creation scenario flow | current |
| `SL-REQ-001-create-connection-request.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-04-request-creation-data.md` | request/applicant data | current |
| `SL-REQ-001-create-connection-request.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md` | future request client-visible flow | current |
| `SL-REQ-001-create-connection-request.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md` | behavior coverage | current |
| `SL-REQ-004-create-request-with-applicant-context.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | target applicantContext redesign | planned |
| `SL-REQ-004-create-request-with-applicant-context.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-04-request-creation-data.md` | applicantContext data | planned |
| `SL-REQ-004-create-request-with-applicant-context.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md` | future request UI | planned |
| `SL-REQ-004-create-request-with-applicant-context.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md` | behavior coverage | planned |
| `SL-AUTH-003-logout.md` | `[SOURCE-GAP]` | Source behavior IDs not yet attached | logout behavior coverage currently uses temporary Source BI TBD | gap |
| `SL-ACC-001-register-client-account.md` | `[SOURCE-GAP]` | Dedicated SC-01 behavior items not attached | registration behavior coverage should be source-linked later | gap |
| `SL-AUTH-001-login-client-account.md` | `[SOURCE-GAP]` | Dedicated SC-02 behavior items not attached | login behavior coverage should be source-linked later | gap |
| `SL-AUTH-002-current-user.md` | `[SOURCE-GAP]` | Dedicated current-user/session behavior items not attached | current-user behavior coverage should be source-linked later | gap |

## 4. Update Rule

When adding or changing a scenario, UI scenario, behavior item file, slice, or `.client.md`:

```text
- update this register in the same archive;
- update scenario behavior item indexes;
- update slice README/navigation if new slice files are added;
- mark `[SOURCE-GAP]` explicitly when source behavior IDs are not attached yet;
- do not silently leave a slice with invented behavior items.
```
