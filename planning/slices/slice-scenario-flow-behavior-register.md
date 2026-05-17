# Slice Scenario Flow / Behavior Source Register

Status: active source register / L1 + near-final L2 Employee Review Agreement sources synchronized

## 1. Rule

Scenario Flow and Behavior Items come from scenario source artifacts:

```text
[SCENARIO] planning/diagrams/scenario-text-specs/
[DATA] planning/diagrams/scenario-data/
[UI-SCENARIO] planning/diagrams/scenario-ui-specs/
[BEHAVIOR] planning/diagrams/scenario-behavior-items/
[CONCERN] planning/slices/cross-cutting/
```

Domain drafts are domain-design input. Account/Employee hierarchy decisions are domain-design input, not scenario behavior:

```text
[DOMAIN-DRAFT] planning/tables/domain-drafts/
```

Do not invent scenario flow or behavior items locally inside a slice when source artifacts exist.

Do not use domain drafts as a replacement for scenario files.

## 2. L1 Source Map

| Slice / sidecar | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `SC-10-applicant-data.md` | add ApplicantParty and create-flow first-of-type default/current initialization | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `SC-10-applicant-data.md` | flat account-level ApplicantParty list read | implemented/current |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `SC-10B-my-applicant-parties.md` | same-page explicit default/current action | implemented backend/current |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `SC-04-client-request-creation.md` | request target flow | current |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` | `SC-05-my-requests-own-request-details.md` | My Requests list | current |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` | `SC-05-my-requests-own-request-details.md` | own request details | current |

## 3. L2 Domain / Clarification Sources

Domain-design input:

```text
planning/tables/domain-drafts/domain-draft-02.md
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md
```

Scenario/domain clarification sources:

```text
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md
planning/diagrams/scenario-clarifications/L2-agreement-scenario-slice-followup-cleanup.md
```

Current L2 status/navigation:

```text
planning/l2-current-planning-status.md
planning/slices/l2/README.md
```

Temporary Employee visibility policy note:

```text
planning/slices/l2/L2-employee-temporary-visibility-policy.md
```

This policy note clarifies first-pass backend authorization/read filtering for employee dashboard/list/details reads. It does not replace scenario sources.

Behavior source family:

```text
planning/diagrams/scenario-behavior-items/
```

## 4. L2 Employee / Review Source Map

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-EMP-REQ-001-employee-request-list-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-06-employee-request-dashboard.md`, employee request DATA/behavior files | Employee request list/dashboard read endpoint + filters | drafted |
| `L2-EMP-DASH-001-employee-request-dashboard.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-06-employee-request-dashboard.md`, employee request DATA/behavior files | Employee request dashboard client read UI; may host StartReview row entry point | drafted |
| `SL-EMP-REQ-002-employee-request-details-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-07A-employee-request-details.md`, details DATA/behavior files | Employee request details read endpoint | drafted |
| `L2-EMP-DETAILS-001-employee-request-details.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-07A-employee-request-details.md`, details DATA/behavior files | Employee request details client read UI and action availability display; may host StartReview details entry point | drafted |
| `SL-EMP-REQ-003-start-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | StartReview backend command; command may be initiated from dashboard row or details action area | drafted |
| `L2-REVIEW-START-001-start-request-review.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | StartReview client action/mutation, one feature with dashboard and details entry points | drafted |
| `SL-EMP-REQ-004-approve-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | ApproveReview backend command; details-only first pass | drafted |
| `L2-REVIEW-APPROVE-001-approve-request-review.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | ApproveReview client action/mutation; details-only first pass | drafted |
| `SL-EMP-REQ-005-reject-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | RejectReview backend command; optional feedback/body; details-only first pass | drafted |
| `L2-REVIEW-REJECT-001-reject-request-review.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | RejectReview client action/form/mutation; details-only first pass; optional feedback | drafted |

## 5. L2 Agreement Exchange Source Map

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13D`, `SC-14`, agreement behavior files, `CC-CSRF-001` | Employee starts exchange from Approved request with initial Employee proposal/version 1; stores ClientAccountId | drafted |
| `L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13D`, `SC-14`, employee request details sidecar, `CC-CSRF-001` | Employee request details action to start exchange; no Client start | drafted |
| `SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13B`, `SC-13D`, `SC-14`, agreement behavior files, `CC-CSRF-001` | Shared Client/Employee counter-proposal versions inside existing exchange | drafted |
| `L2-AGR-EXCH-SEND-PROPOSAL-001-send-agreement-proposal-version.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13B`, `SC-13D`, exchange details sidecar, `CC-CSRF-001` | Shared Client/Employee send proposal feature from agreement exchange details action slot | drafted |
| `SL-AGR-EXCH-003-agreement-exchange-list-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13A`, `SC-13C`, agreement DATA/behavior files | Shared Client/Employee agreement exchange list read; list summary only | drafted |
| `L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13A`, `SC-13C`, agreement DATA/behavior files | Shared list query/widget with actor-specific page shells | drafted |
| `SL-AGR-EXCH-004-agreement-exchange-details-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13B`, `SC-13C`, `SC-13D`, `SC-13E`, agreement DATA/behavior files | Shared Client/Employee agreement exchange details read with proposal history | drafted |
| `L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13B`, `SC-13C`, `SC-13D`, `SC-13E`, agreement DATA/behavior files | Shared details query/widget with actor-specific page shells/action slots | drafted |
| `SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13B`, agreement behavior files, `CC-CSRF-001` | Client-only accept active Employee proposal; no new proposal version | drafted |
| `L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13B`, exchange details sidecar, `CC-CSRF-001` | Client-only accept action in Client agreement exchange details action area | drafted |
| `SL-AGR-EXCH-006-final-refuse-agreement-exchange.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13E`, agreement final refusal direction, `CC-CSRF-001` | Employee final-refuse backend command; exchange becomes FinallyRefused, related request becomes AgreementExchangeFailed | drafted |
| `L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13E`, agreement exchange details/action-slot direction, `CC-CSRF-001` | Employee final-refuse client command sidecar; Employee details action area only | drafted |
| `SL-DOC-*` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-14-agreement-documents.md`, agreement documents DATA/behavior files | AgreementDocumentRef metadata references and future document/storage slices | planned source |

## 6. L2 Drafting Guardrails

```text
- Draft one slice at a time.
- A scenario can be implemented by multiple slices.
- Extension/follow-up slices must be named but not implemented in the current slice.
- Scenario Flow is the scenario portion relevant to that slice, not the full scenario family.
- Implementation Flow must not be mistaken for Scenario Flow.
- Implementation details are not behavior items.
- Domain draft informs domain boundaries but does not replace scenario source files.
- Command client sidecars consume shared cross-cutting concerns; they do not implement local CSRF/session mechanics.
- StartReview has one command sidecar with two UI entry points; do not duplicate it into two sidecars.
- Initial agreement exchange creation and counter-proposal versioning are separate slice boundaries.
- Client and Employee counter-proposal sends stay in one slice until actor-specific handling diverges materially.
- Agreement exchange list and details are separate read slices.
- RejectReview feedback is optional unless implementation intentionally changes it.
- Counter-proposal replacement is SupersededByCounterProposal, not ordinary Rejected.
```

## 7. Update Rule

Update this register when scenario files, behavior items, slices or sidecars are added/renamed.

When a future L2 implementation slice is drafted, point it to the relevant scenario files and to `domain-draft-02.md` only as domain-design input.
