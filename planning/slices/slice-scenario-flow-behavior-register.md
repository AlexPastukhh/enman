# Slice Scenario Flow / Behavior Source Register

Status: active source register / L1 + L2 Employee Review Agreement sources synchronized

## 1. Rule

Scenario Flow and Behavior Items come from scenario source artifacts:

```text
[SCENARIO] planning/diagrams/scenario-text-specs/
[DATA] planning/diagrams/scenario-data/
[UI-SCENARIO] planning/diagrams/scenario-ui-specs/
[BEHAVIOR] planning/diagrams/scenario-behavior-items/
[DOMAIN-DRAFT] planning/tables/domain-drafts/
[CONCERN] planning/slices/cross-cutting/
```

Do not invent scenario flow or behavior items locally inside a slice when source artifacts exist.

## 2. L1 Source Map

| Slice / sidecar | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | add ApplicantParty and create-flow first-of-type default/current initialization | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | flat account-level ApplicantParty list read | implemented/current |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | same-page explicit default/current action | implemented backend/current |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | request target flow | current |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | My Requests list | current |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | own request details | current |

## 3. L2 Employee / Review / Agreement Source Map

Primary domain source:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

Scenario/domain clarification source:

```text
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
```

Behavior source:

```text
planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md
```

| Future slice family | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-EMP-*` | `[SCENARIO]` / `[DOMAIN-DRAFT]` | `SC-06-employee-request-dashboard.md`, `SC-07A-employee-request-details.md`, `domain-draft-02.md` | employee dashboard/details read models | planned source |
| `SL-REVIEW-*` | `[SCENARIO]` / `[DOMAIN-DRAFT]` | `SC-07B-employee-request-review.md`, `domain-draft-02.md` | StartReview / ApproveReview / RejectReview | planned source |
| `SL-AGR-*` | `[SCENARIO]` / `[DOMAIN-DRAFT]` | `SC-13A..E`, `SC-14`, `domain-draft-02.md` | AgreementProposalExchange, proposal versions, final refusal, agreement documents | planned source |
| `SL-DOC-*` | `[SCENARIO]` / `[DOMAIN-DRAFT]` | `SC-14-agreement-documents.md`, `domain-draft-02.md` | AgreementDocumentRef metadata references | planned source |

## 4. L2 Drafting Guardrails

```text
- Draft one slice at a time.
- A scenario can be implemented by multiple slices.
- Extension/follow-up slices must be named but not implemented in the current slice.
- Scenario Flow is the scenario portion relevant to that slice, not the full scenario family.
- Implementation Flow must not be mistaken for Scenario Flow.
- Implementation details are not behavior items.
```

## 5. Update Rule

Update this register when scenario files, behavior items, slices or sidecars are added/renamed.

When a future L2 implementation slice is drafted, point it to the relevant SC-06/SC-07/SC-13/SC-14 files and to `domain-draft-02.md` for domain boundaries.
