# Slice Questions Register

Status: active / consolidated slice questions and decisions register

This is the canonical active register for slice-layer questions and decisions. Historical `SLICE-QUESTIONS.md` content has been consolidated here; do not add new active slice questions to `SLICE-QUESTIONS.md`.

## Question Statuses

```text
accepted
blocked
assumption
future-review
superseded
stale
```

## 1. General Current-State Rule

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `GEN-CURRENT-STATE-Q-001` | current implementation | accepted | How should agents answer current-state questions? | Inspect GitHub/current branch, not uploaded archives or old drafts. | Prevents stale status answers. |

## 2. Client API / UI / CSS Placement Decisions

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `CL-API-PLACEMENT-Q-001` | client architecture | accepted | Should business endpoint wrappers live in `shared/api`? | No for new drafts. `shared/api` owns transport/generated infrastructure only. | New read/command wrappers move to entities/features. |
| `CL-API-PLACEMENT-Q-002` | generated types | accepted | Can entities/features import generated OpenAPI types directly? | Yes. Generated types are shared infrastructure; business aliases live in owning entity/feature API files. | Avoids business-aware shared wrappers. |
| `CL-API-PLACEMENT-Q-003` | migration | accepted | Should existing shared business wrappers be mass-migrated now? | No. Treat them as transitional compatibility and migrate only in concrete slice/cleanup scope. | Avoids broad client churn. |
| `CL-API-PLACEMENT-Q-004` | read placement | accepted | Where do read endpoint wrappers live? | `entities/<entity>/api`. | Employee dashboard/details, Agreement exchange reads. |
| `CL-API-PLACEMENT-Q-005` | command placement | accepted | Where do command/mutation endpoint wrappers live? | `features/<business-action>/api`. | Review commands, Agreement exchange commands. |
| `CL-LAYER-Q-001` | shared API | superseded | `shared/api` is the low-level client/server boundary grouped by layer. | Superseded for business-specific wrappers; shared remains transport/generated boundary only. | Do not copy old shared wrapper shape into new drafts. |
| `Q-CLIENT-UI-001` | client docs | accepted | Should app header include decorative public nav first pass? | No. Header is flow-based only. | App shell and UI foundation. |
| `Q-CLIENT-CSS-001` | client docs | accepted | Is CSS part of slice implementation ownership? | Yes. CSS ownership follows page/widget/entity/feature/shared boundaries. | All client slice drafts. |
| `Q-CLIENT-FLOW-001` | client draft format | accepted | Should Visual Client Implementation Flow use `does`? | No. Use `needed to`. | Client slice template. |
| `Q-CLIENT-FLOW-002` | client draft format | accepted | Should every dependency have `visual`? | No. Only UI-rendering dependencies get `visual`. | Client slice template. |
| `Q-CLIENT-FLOW-003` | client draft format | accepted | What should `visual` describe? | Only the block's role in the parent layout, not child internals. | CSS ownership and draft readability. |

## 3. Slice Taxonomy / Migration / Testing Decisions

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `Q-MIGRATION-001` | slice docs | accepted | Should all old `.client.md` drafts be moved now? | No. Add new structure and index first; migrate drafts gradually. | Avoids broken links and noisy diff. |
| `Q-MIGRATION-002` | slice docs | accepted | Should future folders use L1/L2 grouping? | No. New docs are grouped by client/server/cross-cutting responsibility. L1/L2 is legacy only. | Folder structure and navigation. |
| `Q-TAXONOMY-001` | slice docs | accepted | Are behavior items smaller than slices? | Yes. A slice may implement an independent chain of behavior items. | Scenario/behavior/slice explanations. |
| `Q-TAXONOMY-002` | cross-cutting docs | accepted | Do cross-cutting implementation docs still need scenario behavior sources? | Yes. Common behavior must be reflected in scenario-cross-cutting sources before slice drafting. | Common behavior source discipline. |
| `Q-TAXONOMY-003` | cross-cutting docs | accepted | Is `planning/slices/cross-cutting/` an implementation dump? | No. It is for umbrella/coordination docs. Side-specific implementation goes to client/server slice drafts. | Cross-cutting placement clarity. |
| `Q-NAMING-001` | slice docs | accepted | How do we mark a client-only or server-only slice draft? | Use `SINGLE-` prefix at the start of the file name. | Navigation and pairing clarity. |
| `Q-TEST-001` | slice docs | accepted | What must slice tests prove? | Behavior items and scenario outcomes, not implementation flow. | All slice draft test plans. |
| `Q-TEST-002` | slice docs | accepted | Can implementation details appear in tests? | Yes, but only as setup/action/observation mechanisms, not primary proof. | Test stability and behavior proof. |
| `Q-TEST-003` | slice docs | accepted | What extra test quality questions are required? | Escape risk and refactor risk must be stated. | Prevents weak and fragile tests. |
| `Q-TEST-004` | slice docs | accepted | Should direct DB setup be allowed in integration tests? | Yes, only for scenario preconditions; behavior proof still goes through public boundary. | Server integration tests. |

## 4. L2 Account / Employee Identity Decisions

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `L2-EMP-ACCOUNT-Q-001` | domain identity | accepted | Should Employee be `Account`-derived or linked by `AccountId`? | `Employee : Account`; TPH in `L1Accounts`; `Employee.Id == Account.Id`. | Removes accountId vs employeeId ambiguity. |
| `L2-EMP-ACCOUNT-Q-002` | auth/session | accepted | What does `ClaimTypes.NameIdentifier` mean for Employee endpoints? | It stores `Account.Id`; for Employee sessions that is also `Employee.Id`. | Handlers load Employee by id from claim. |
| `L2-EMP-ACCOUNT-Q-003` | modeling | accepted | Is Employee a separate profile entity linked by AccountId? | No for L2 target. Treat that shape as compatibility/drift if present. | Prevents `EmployeeProfile(AccountId)` target design. |
| `L2-EMP-ACCOUNT-Q-004` | domain actor | accepted | Should review/agreement methods receive `EmployeeRef`? | No. Methods receive `Employee`; owned state stores scalar `Employee.Id` fields. | Keeps review model consistent. |

## 5. L2 Employee Review Decisions

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `L2-REVIEW-Q-001` | aggregate | accepted | Is Review an aggregate? | No. Request owns RequestReview; no Review repository. | Review commands load Request. |
| `L2-REVIEW-Q-002` | entry points | accepted | Does StartReview have one or two client sidecars? | One sidecar with two entry points: dashboard/list row and details action area. | Avoids duplicate StartReview features. |
| `L2-REVIEW-Q-003` | ApproveReview scope | accepted | Does ApproveReview create AgreementProposalExchange? | No. It only approves request review and sets request Approved. | Exchange starts separately. |
| `L2-REVIEW-Q-004` | RejectReview feedback | accepted | Is rejection feedback required? | Current direction: optional. Missing/blank feedback allowed unless implementation intentionally changes it. | Client must not block submit solely for missing feedback. |
| `L2-REVIEW-Q-005` | approve/reject placement | accepted | Are approve/reject available from dashboard first pass? | No. Details-only first pass. | Prevents risky list-row final decisions. |

## 6. AgreementProposalExchange Slice Boundary Decisions

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `L2-AGR-EXCH-Q-001` | slice split | accepted | Should initial employee proposal and counter-proposal be one implementation slice? | No. Initial employee proposal creates exchange and version 1, so it is `SL-AGR-EXCH-001`. | Avoids create-vs-respond branching. |
| `L2-AGR-EXCH-Q-002` | counter-proposal split | accepted | Should ClientSendOwnVersion and EmployeeSendNewVersion be split now? | No. Use one `SL-AGR-EXCH-002` with two actor branches until behavior diverges. | Avoids duplicate near-identical slices. |
| `L2-AGR-EXCH-Q-003` | initial exchange | accepted | Can exchange start without initial document? | No. `StartByEmployee(...)` creates exchange with initial document/proposal version. | Full draft must require document reference. |
| `L2-AGR-EXCH-Q-004` | revision request | accepted | Is Applicant Request Revision a separate slice? | No. It is represented by client sending own version. | Prevents extra revision-request-only slice. |
| `L2-AGR-EXCH-Q-005` | final refusal | accepted | Is final refusal a proposal version or separate entity? | No. It is direct exchange state and creates no proposal version/entity. | Keeps final refusal slice focused. |

## 7. Agreement Exchange Participant / Read / Command Decisions

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `AGR-EXCH-PART-Q-001` | client ownership | accepted | How is Client access protected? | Persist `AgreementProposalExchange.ClientAccountId`; client domain methods guard `client.Id == ClientAccountId`. | Participant security. |
| `AGR-EXCH-PART-Q-002` | employee ownership | accepted | Does exchange have ResponsibleEmployeeId guard? | No first pass. Any active Employee can service exchange. | Avoids false employee ownership. |
| `AGR-EXCH-PART-Q-003` | proposal versions | accepted | Where is Employee/Client identity tracked in history? | Per proposal version through sender fields. | Supports different employees over time. |
| `AGR-EXCH-PART-Q-004` | actor abstraction | accepted | Introduce AgreementExchangeActor abstraction now? | No first pass. Use current role/account id and explicit service methods/branches. | Simpler implementation. |
| `AGR-EXCH-READ-Q-001` | endpoint shape | accepted | Use shared list/details endpoints for Client and Employee first pass? | Yes. Server branches by current session role/access; frontend uses shared entity wrappers/widgets. | Avoids duplicate read slices. |
| `AGR-EXCH-READ-Q-002` | client ownership | accepted | How is Client read access protected? | Persist and filter by `AgreementProposalExchange.ClientAccountId`. | Requires domain/persistence field. |
| `AGR-EXCH-READ-Q-003` | employee ownership | accepted | Does read side have ResponsibleEmployeeId guard? | No first pass. Any active Employee can service exchange. | Avoids false employee ownership. |
| `AGR-EXCH-READ-Q-004` | read implementation | accepted | Use application service for details read? | No. Use query handler + read repository / Dapper projection. | Keeps read slice projection-only. |
| `AGR-EXCH-READ-Q-005` | frontend ownership | accepted | Actor-specific wrappers first pass? | No while response shape is common. Use shared entity API/query/model and actor page shells. | Avoids duplicated client wrappers. |
| `Q-L2-AGR-CMD-001` | endpoint | accepted | One shared endpoint or separate Client/Employee endpoints for counter-proposal? | One shared endpoint first pass: `POST /api/requests/{requestId}/agreement-exchange/proposals`. | Shared server/client wrapper. |
| `Q-L2-AGR-CMD-002` | route | accepted | Should route use exchangeId or requestId? | Request-scoped route uses `{requestId}`. | Avoids confusing requestId with exchangeId. |
| `Q-L2-AGR-CMD-003` | actor model | accepted | Introduce AgreementExchangeActor now? | No first pass. Controller resolves role/account id; service branches explicitly; domain owns invariants. | Simpler implementation. |
| `Q-L2-AGR-CMD-004` | command result | accepted | Add per-command status enums? | No. Use existing Result/Error model. | Prevents status enum sprawl. |
| `Q-L2-AGR-CMD-005` | accept | accepted | Is accept Client-only? | Yes first pass. Employee accept out of scope. | Client-only endpoint/sidecar. |
| `Q-L2-AGR-CMD-006` | accept state | accepted | Does accept create a new version? | No. Exchange becomes Accepted; active proposal becomes Accepted; proposal count unchanged. | Test assertions. |
| `Q-L2-AGR-CMD-007` | final refusal | accepted | Is final refusal Employee-only? | Yes first pass. Client final refusal out of scope. | Employee-only endpoint/sidecar. |
| `Q-L2-AGR-CMD-008` | final refusal body | accepted | Is reason required? | No. Body/reason nullable; blank/whitespace-only reason invalid if provided. | Validator/controller body binding. |

## 8. Start Agreement Exchange Client Contract Questions

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `Q-L2-AGR-START-CLIENT-001` | route | blocked | Exact route: request-scoped server route or preferred collection route? | Current server slice uses `POST /api/employee/requests/{requestId}/agreement-exchange/start`; client prefers `POST /api/agreement-exchanges` if response includes exchangeId. Generated OpenAPI decides. | Client wrapper/navigation. |
| `Q-L2-AGR-START-CLIENT-002` | response | blocked | Does success return exchangeId? | Preferred for navigation; fallback is 204 + refetch/stay on request details. | Post-success UX. |
| `Q-L2-AGR-START-CLIENT-003` | DTO | blocked | Exact initial proposal DTO fields? | Use generated DTO aliases after server contract exists. | Form/API wrapper. |

## 9. Archive Workflow Decisions

These are archive/documentation workflow decisions retained here because they were previously part of the slice question register. Move them to a dedicated documentation/archive register later only if needed.

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `Q-ARCHIVE-001` | archive workflow | accepted | Should large archives replace many existing docs without preserving originals? | No. Replacement archives must preserve originals under unique archive-review folders. | Prevents accidental loss of guardrails/navigation. |
| `Q-ARCHIVE-002` | archive workflow | accepted | Should archive raw/derived notes use one shared project file? | No. Use archive-local unique folder names. | Keeps author logs and audit notes isolated. |
| `Q-ARCHIVE-003` | archive workflow | accepted | How should high-risk replacement archives be applied? | Use safe merge archive, post-apply merge review, then smaller correction archive if needed. | Reduces docs merge risk. |
| `Q-ARCHIVE-004` | archive workflow | accepted | Should chat list the archive plan before creating a replacement archive? | Yes. List scope, original snapshots, high-risk files and review points. | Makes archive scope explicit before applying. |

## 10. Implemented Slice Sync Decisions

Implemented sync helper artifacts were removed. Keep the workflow and retain useful decisions here.

| ID | Area | Status | Question | Current direction | Impact |
|---|---|---|---|---|---|
| `Q-IMPL-SYNC-001` | implemented slice sync | accepted | Can an implemented slice draft be rewritten directly to the newest template? | No. First run sync against sources, draft, code and tests. | Prevents docs from drifting away from actual implementation. |
| `Q-IMPL-SYNC-002` | implemented slice sync | accepted | What if implementation differs from the old draft? | Do not assume code is wrong. First classify source/domain/code/test/docs drift. | Avoids replacing target behavior with accidental current behavior. |
| `Q-IMPL-SYNC-003` | implemented slice sync | accepted | What block should implemented drafts include? | Add an implementation/status/drift block near the top when implemented sync is performed. | Makes implemented/current/stale/test drift explicit. |
| `Q-IMPL-SYNC-004` | implemented slice sync | accepted | What happens if expected behavior changed during implemented draft sync? | Stop and use source/version sync first once that workflow exists. Until then, do not silently rewrite source meaning from implementation evidence. | Keeps source meaning as root of truth. |

## 11. Stale / Superseded Decisions

```text
EmployeeRef target model -> superseded by Employee domain object + scalar EmployeeId state.
ResponsibleEmployeeId exchange guard -> rejected for first pass.
Counter-proposal as Rejected -> superseded by SupersededByCounterProposal.
SC-14 Client Data Verification -> stale/deprecated/deferred for current agreement-document context.
Per-command status enum for HTTP mapping -> rejected; use Result/Error mapping.
```
