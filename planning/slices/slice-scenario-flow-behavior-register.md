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

Domain drafts are domain-design input only:

```text
[DOMAIN-DRAFT] planning/tables/domain-drafts/
```

Do not invent scenario flow or behavior items locally inside a slice when source artifacts exist.

Do not use domain drafts as a replacement for scenario files.

## 2. L1 Source Map

| Slice / sidecar | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` | `SC-10-applicant-data.md` | add ApplicantParty and first-of-type default/current initialization | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` | `SC-10-applicant-data.md` | flat account-level ApplicantParty list read | implemented/current |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` | `SC-10B-my-applicant-parties.md` | same-page explicit default/current action | implemented backend/current |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` | `SC-04-client-request-creation.md` | request target flow | current |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` | `SC-05-my-requests-own-request-details.md` | My Requests list | current |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` | `SC-05-my-requests-own-request-details.md` | own request details | current |

## 3. L2 Employee / Review / Agreement Source Map

Domain-design input:

```text
planning/tables/domain-drafts/domain-draft-02.md
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md
```

Scenario/domain clarification source:

```text
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md
planning/diagrams/scenario-clarifications/L2-agreement-scenario-slice-followup-cleanup.md
```

Temporary Employee visibility policy note:

```text
planning/slices/l2/L2-employee-temporary-visibility-policy.md
```

Behavior source family:

```text
planning/diagrams/scenario-behavior-items/
```

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-EMP-REQ-001-employee-request-list-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-06-employee-request-dashboard.md`, employee request DATA/behavior files | Employee request list/dashboard read endpoint + filters | drafted / implementation evidence may exist |
| `L2-EMP-DASH-001-employee-request-dashboard.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-06-employee-request-dashboard.md`, employee request DATA/behavior files | Employee request dashboard client read UI; may host StartReview row entry point | drafted / implementation evidence may exist |
| `SL-EMP-REQ-002-employee-request-details-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-07A-employee-request-details.md`, details DATA/behavior files | Employee request details read endpoint | drafted / implementation evidence may exist |
| `L2-EMP-DETAILS-001-employee-request-details.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-07A-employee-request-details.md`, details DATA/behavior files | Employee request details client read UI and action availability display; may host StartReview and StartAgreementExchange action slots | drafted / implementation evidence may exist |
| `SL-EMP-REQ-003-start-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` | `SC-07B-employee-request-review.md`, review behavior files | StartReview backend command; dashboard row or details action area entry point | drafted / implementation evidence may exist |
| `L2-REVIEW-START-001-start-request-review.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | StartReview client action/mutation, one feature with dashboard and details entry points | drafted |
| `SL-EMP-REQ-004-approve-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | ApproveReview backend command; does not create exchange | drafted |
| `SL-EMP-REQ-005-reject-request-review.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | RejectReview backend command; optional feedback/body | drafted |
| `L2-REVIEW-APPROVE-001.client` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | ApproveReview client action/mutation | planned |
| `L2-REVIEW-REJECT-001.client` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-07B-employee-request-review.md`, review behavior files, `CC-CSRF-001` | RejectReview client action/form/mutation; optional feedback | planned |

## 4. Agreement Exchange Canonical Source Map

| Slice / sidecar | Marker | Source files | Applies to | Status |
|---|---|---|---|---|
| `SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13D`, `SC-14`, agreement behavior files, `CC-CSRF-001` | Employee starts exchange from Approved request with initial Employee proposal/version 1 | drafted |
| `L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13D`, `L2-EMP-DETAILS-001.client`, `CC-CSRF-001` | Employee request details action/form for starting exchange before exchange exists | drafted |
| `SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13B`, `SC-13D`, `SC-14`, agreement behavior files, `CC-CSRF-001` | Client and Employee counter-proposal versions inside existing exchange | drafted |
| `L2-AGR-EXCH-SEND-PROPOSAL-001-send-agreement-proposal-version.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13B`, `SC-13D`, agreement details sidecar, `CC-CSRF-001` | Shared Client/Employee send proposal feature from agreement exchange details action slot | drafted |
| `SL-AGR-EXCH-003-agreement-exchange-list-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13A`, `SC-13C`, agreement DATA/behavior files | Shared Client/Employee agreement exchange list read | drafted |
| `L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13A`, `SC-13C`, agreement list read sidecar | Shared list query/widget with actor-specific page shells | drafted |
| `SL-AGR-EXCH-004-agreement-exchange-details-read.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13B`, `SC-13D`, `SC-13E`, `SC-14`, agreement DATA/behavior files | Shared Client/Employee exchange details, active proposal and version history read | drafted |
| `L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-13B`, `SC-13D`, `SC-13E`, agreement details sidecar | Shared details widget with actor-specific page shells/action slots | drafted |
| `SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13B`, agreement behavior files, `CC-CSRF-001` | Client-only accept active Employee proposal command; no new version | drafted |
| `L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13B`, agreement details sidecar, `CC-CSRF-001` | Client-only Accept action in Client agreement exchange details action area | drafted |
| `SL-AGR-EXCH-006-final-refuse-agreement-exchange.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13E`, agreement behavior files, `CC-CSRF-001` | Employee final-refuse backend command; exchange becomes FinallyRefused and related request becomes AgreementExchangeFailed | drafted |
| `L2-AGR-EXCH-FINAL-REFUSE-001-employee-final-refuse-agreement-exchange.client.md` | `[SCENARIO]` / `[BEHAVIOR]` / `[CONCERN]` | `SC-13E`, agreement details sidecar, `CC-CSRF-001` | Employee final-refuse client command sidecar; Employee details action area only | drafted |
| `SL-DOC-*` | `[SCENARIO]` / `[DATA]` / `[BEHAVIOR]` | `SC-14-agreement-documents.md`, agreement documents DATA/behavior files | AgreementDocumentRef metadata references / future document lifecycle | planned source |

## 5. L2 Drafting Guardrails

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
- StartAgreementExchange has one Employee-only sidecar hosted by Employee request details because exchange does not exist yet.
- Initial agreement exchange creation and counter-proposal versioning are separate slice boundaries.
- Client and Employee counter-proposal sends stay in one slice until actor-specific handling diverges materially.
- AgreementProposalExchange list/details are separate read slices.
- Counter-proposal replacement is SupersededByCounterProposal, not Rejected.
```

## 6. Update Rule

Update this register when scenario files, behavior items, slices or sidecars are added/renamed.

When a future L2 implementation slice is drafted, point it to the relevant scenario files and to `domain-draft-02.md` only as domain-design input.
