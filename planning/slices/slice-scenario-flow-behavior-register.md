# Slice Scenario Flow / Behavior Source Register

Status: active source register  
Scope: maps each active/planned slice or `.client.md` sidecar to the scenario flow, DATA, UI scenario and behavior item sources it must consume

## 1. Purpose

This register exists because slice/client chats must not invent Scenario Flow or Behavior Items from local questions, extension notes or implementation notes.

It answers:

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

Those registers help find questions, extension pressure and future notes.

They are not the authoritative source for Scenario Flow or Behavior Items.

## 3. Required Use

Before writing or updating any of these sections:

```text
Visual Scenario Flow
Scenario Slice Flow
Visual UI / Scenario Flow
Behavior Coverage
Covered Scenario / UI Behavior Items
E2E scenario references
```

the chat must:

```text
1. Find the slice/client sidecar in this register.
2. Read the linked scenario text/DATA/UI/behavior files.
3. Use the linked behavior item IDs in Behavior Coverage.
4. If `[SOURCE-GAP]` exists, do not finalize behavior coverage; mark Source BI TBD and raise the gap.
5. Update this register when new scenario/UI/behavior files or slices are added.
```

## 4. Source Map

| Slice / sidecar | Source marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | backend request creation scenario flow | current |
| `SL-REQ-001-create-connection-request.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-04-request-creation-data.md` | request/applicant visible/input DATA | current |
| `SL-REQ-001-create-connection-request.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md` | future client-visible request flow | current |
| `SL-REQ-001-create-connection-request.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md` | behavior coverage | current |
| `SL-REQ-001-create-connection-request.client.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | future request client sidecar | planned |
| `SL-REQ-001-create-connection-request.client.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-04-request-creation-data.md` | future request client sidecar | planned |
| `SL-REQ-001-create-connection-request.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md` | future request client sidecar UI flow/tests | planned |
| `SL-REQ-001-create-connection-request.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md` | future request client sidecar coverage | planned |
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | applicant data backend scenario flow | current |
| `SL-APPL-001-create-individual-applicant-party.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-10-applicant-data.md` | applicant data DATA | current |
| `SL-APPL-001-create-individual-applicant-party.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | client/account page outcome | current |
| `SL-APPL-001-create-individual-applicant-party.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | behavior coverage | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | applicant create client sidecar | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[DATA]` | `planning/diagrams/scenario-data/SC-10-applicant-data.md` | form/read-only DATA | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | client visible flow/tests | current |
| `SL-APPL-001-create-individual-applicant-party.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | behavior coverage | current |
| `SL-APPL-002-read-current-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | current applicant read backend draft | planned |
| `SL-APPL-002-read-current-individual-applicant-party.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | Account page exists/missing states | planned |
| `SL-APPL-002-read-current-individual-applicant-party.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | behavior coverage | planned |
| `SL-APPL-002-read-current-individual-applicant-party.client.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | current applicant read client sidecar | planned |
| `SL-APPL-002-read-current-individual-applicant-party.client.md` | `[UI-SCENARIO]` | `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md` | Account page load/read state | planned |
| `SL-APPL-002-read-current-individual-applicant-party.client.md` | `[BEHAVIOR]` | `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | behavior coverage | planned |
| `SL-AUTH-003-logout.md` | `[SOURCE-GAP]` | Source behavior IDs not yet attached | backend logout behavior coverage currently uses temporary Source BI TBD | gap |
| `SL-AUTH-003-logout.client.md` | `[SOURCE-GAP]` | Dedicated logout UI behavior IDs not yet attached | client logout behavior coverage currently uses temporary Source/UI BI TBD | gap |
| `SL-ACC-001-register-client-account.md` | `[SOURCE-GAP]` | Dedicated SC-01 behavior items not attached in current source register | registration behavior coverage should be source-linked later | gap |
| `SL-AUTH-001-login-client-account.md` | `[SOURCE-GAP]` | Dedicated SC-02 behavior items not attached in current source register | login behavior coverage should be source-linked later | gap |
| `SL-AUTH-002-current-user.md` | `[SOURCE-GAP]` | Dedicated current-user/session behavior items not attached | current-user behavior coverage should be source-linked later | gap |

## 5. Update Rule

When adding or changing a scenario, UI scenario, behavior item file, slice, or `.client.md`:

```text
- update this register in the same archive;
- update scenario behavior item indexes;
- update slice README/navigation if new slice files are added;
- mark `[SOURCE-GAP]` explicitly when source behavior IDs are not attached yet;
- do not silently leave a slice with invented behavior items.
```

## 6. Status Values

```text
current
planned
gap
superseded
```
