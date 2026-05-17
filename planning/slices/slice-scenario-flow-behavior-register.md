# Slice Scenario Flow / Behavior Source Register

Status: active source register / L1 + L2 Employee Review Agreement sources synchronized

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
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | add ApplicantParty and create-flow first-of-type default/current initialization | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md` | flat account-level ApplicantParty list read | implemented/current |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md` | same-page explicit default/current action | implemented backend/current |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | request target flow | current |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | My Requests list | current |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` | `planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md` | own request details | current |

## 3. L2 Employee / Review / Agreement Source Map

Domain-design input:

```text
planning/tables/domain-drafts/domain-draft-02.md
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md
```

Scenario/domain clarification source:

```text
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
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

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-EMP-REQ-001-employee-request-list-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-06-employee-request-dashboard.md`, employee request DATA/behavior files | Employee request list/dashboard read endpoint + filters | drafted |
| `L2-EMP-DASH-001-employee-request-dashboard.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-06-employee-request-dashboard.md`, employee request DATA/behavior files | Employee request dashboard client read UI; may host StartReview row entry point | drafted |
| `SL-EMP-REQ-002-employee-request-details-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-07A-employee-request-details.md`, details DATA/behavior files | Employee request details read endpoint | drafted |
| `L2-EMP-DETAILS-001-employee-request-details.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-07A-employee-request-details.md`, details DATA/behavior files | Employee request details client read UI and action availability display; may host StartReview details entry point | drafted |
| `SL-EMP-REQ-003-start-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` | `SC-07B-employee-request-review.md`, review behavior files | StartReview backend command; command may be initiated from dashboard row or details action area | drafted |
| `L2-REVIEW-START-001-start-request-review.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | StartReview client action/mutation, one feature with dashboard and details entry points | drafted |
| `SL-EMP-REQ-004-approve-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | ApproveReview backend command | drafted |
| `SL-EMP-REQ-005-reject-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | RejectReview backend command and rejection feedback; includes client sidecar planning notes | drafted |
| `L2-REVIEW-APPROVE-001.client` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | ApproveReview client action/mutation | planned |
| `L2-REVIEW-REJECT-001.client` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | RejectReview client action/form/mutation | planned |
| `SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13C`, `SC-13D`, agreement behavior files | Start exchange from Approved request with initial Employee proposal/version 1 | planned boundary |
| `SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13B`, `SC-13D`, agreement behavior files | Client and Employee counter-proposal versions inside existing exchange | planned boundary |
| `SL-AGR-EXCH-003-read-agreement-exchange.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13A`, `SC-13B`, `SC-13C`, agreement DATA/behavior files | Read exchange state, active proposal and version history | planned boundary |
| `SL-AGR-EXCH-004-accept-active-agreement-proposal.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13B`, agreement behavior files | Accept active agreement proposal | planned boundary |
| `SL-AGR-EXCH-005-final-refuse-agreement-exchange.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13E`, agreement behavior files | Employee final refusal of exchange and request exchange-failed marking | planned boundary |
| `SL-DOC-*` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-14-agreement-documents.md`, agreement documents DATA/behavior files | AgreementDocumentRef metadata references | planned source |

## 4. L2 Drafting Guardrails

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
```

## 5. Update Rule

Update this register when scenario files, behavior items, slices or sidecars are added/renamed.

When a future L2 implementation slice is drafted, point it to the relevant scenario files and to `domain-draft-02.md` only as domain-design input.



