# Slice Scenario Flow / Behavior Source Register

Status: active source register / UI scenario source rules synchronized

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

## 2. UI Scenario Source Rule

UI scenario specs live in:

```text
planning/diagrams/scenario-ui-specs/
```

UI scenario specs define visible user requirements:

```text
user goal
screen entry points
screen composition
visible data
actions
visible states
actor-specific differences
feedback/validation requirements
accessibility notes
```

Client slice drafts translate UI scenario specs into implementation:

```text
pages/entities/widgets/features/shared ownership
API hooks/wrappers
CSS ownership
tests
implementation checklist
```

Before refreshing a client slice draft or implementing a major UI change:

```text
1. Check whether the relevant UI scenario source exists.
2. If missing/stub/partial, update UI scenario first.
3. Then update client slice draft.
4. Then implement.
```

## 3. UI Scenario Source Map

| UI area | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| App shell / home / auth flow | `[UI-SCENARIO]` | `APP-UI-001-app-shell-home-auth-flow-ui.md` | Header, home, login/register/account signed-out flow | current first pass |
| Request creation | `[UI-SCENARIO]` | `SC-04-request-creation-ui.md` | Create request UI and applicant prefill behavior | partial / normalized |
| My Requests | `[UI-SCENARIO]` | `SC-05-my-requests-ui.md` | Own requests list, filters, details entry | mostly complete / normalized |
| Account Applicant Parties section | `[UI-SCENARIO]` | `SC-10-applicant-data-ui.md` | Account page Applicant Parties section and add action | current canonical |
| My Applicant Parties standalone | `[UI-SCENARIO]` | `SC-10B-my-applicant-parties-ui.md` | Deprecated standalone/future wording source | deprecated / replaced by SC-10 |
| Employee Request Dashboard | `[UI-SCENARIO]` | `SC-06-employee-request-dashboard-ui.md` | Employee dashboard/list UI | missing |
| Employee Request Details | `[UI-SCENARIO]` | `SC-07A-employee-request-details-ui.md` | Employee request details UI | missing |
| Employee Review Actions | `[UI-SCENARIO]` | `SC-07B-employee-request-review-actions-ui.md` | Start/approve/reject review UI | missing |
| Agreement Exchange List | `[UI-SCENARIO]` | `SC-13A-agreement-exchange-list-ui.md` | Shared Client/Employee exchange list UI | missing |
| Agreement Exchange Details | `[UI-SCENARIO]` | `SC-13B-agreement-exchange-details-ui.md` | Shared Client/Employee exchange details UI | missing |
| Start Agreement Exchange | `[UI-SCENARIO]` | `SC-13D-start-agreement-exchange-ui.md` | Employee start exchange action UI | missing |
| Final Refuse Exchange | `[UI-SCENARIO]` | `SC-13E-final-refuse-exchange-ui.md` | Employee final refusal UI | missing |
| Agreement Documents | `[UI-SCENARIO]` | `SC-14-agreement-documents-ui.md` | AgreementDocumentRef/document UI | future / missing |

## 4. L1 Source Map

| Slice / sidecar | Marker | Source file | Applies to | Status |
|---|---|---|---|---|
| `SL-APPL-001-create-individual-applicant-party.md` | `[SCENARIO]` / `[UI-SCENARIO]` | `SC-10-applicant-data.md`, `SC-10-applicant-data-ui.md` | add ApplicantParty and create-flow first-of-type default/current initialization | current |
| `SL-APPL-002-account-applicant-parties-read.md` | `[SCENARIO]` / `[UI-SCENARIO]` | `SC-10-applicant-data.md`, `SC-10-applicant-data-ui.md` | flat account-level ApplicantParty list read inside Account page section | implemented/current |
| `SL-APPL-003-select-current-default-applicant-party-template.md` | `[SCENARIO]` / `[UI-SCENARIO]` | `SC-10B-my-applicant-parties.md`, `SC-10-applicant-data-ui.md` | same Account page section explicit default/current action | implemented backend/current |
| `SL-REQ-001-create-connection-request.md` | `[SCENARIO]` / `[UI-SCENARIO]` | `SC-04-client-request-creation.md`, `SC-04-request-creation-ui.md` | request target flow | current |
| `SL-REQ-002-my-requests-list.md` | `[SCENARIO]` / `[UI-SCENARIO]` | `SC-05-my-requests-own-request-details.md`, `SC-05-my-requests-ui.md` | My Requests list | current |
| `SL-REQ-003-own-request-details.md` | `[SCENARIO]` / `[UI-SCENARIO]` | `SC-05-my-requests-own-request-details.md`, `SC-05-my-requests-ui.md` | own request details entry; details UI source still needs dedicated expansion | current |

## 5. L2 / Agreement Exchange Source Notes

Existing L2/server/client slice maps remain in legacy draft files and slice index during migration.

For UI work, use `UI-SCENARIO-READINESS.md` to identify missing UI scenario sources before updating client sidecar drafts.

## 6. Drafting Guardrails

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
- ApplicantParty UI is Account page section first pass; `SC-10B` standalone My Applicant Parties UI is deprecated/replaced by `SC-10`.
```

## 7. Update Rule

Update this register when scenario files, behavior items, slices or sidecars are added/renamed.

When a future implementation slice is drafted, point it to the relevant scenario files and to domain drafts only as domain-design input.
