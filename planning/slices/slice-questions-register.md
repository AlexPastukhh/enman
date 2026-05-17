# Slice Questions Register

Status: active / near-final L2 review and AgreementProposalExchange decisions synchronized

## 1. Current-State Rule

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `GEN-CURRENT-STATE-Q-001` | all planning docs | current implementation | accepted | How should agents answer current-state questions? | Inspect GitHub/current branch, not uploaded archives or old drafts. | Prevents stale status answers. |

## 2. Client API Placement Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `CL-API-PLACEMENT-Q-001` | `planning/client/client-api-placement-decision.md` | client architecture | accepted | Should business endpoint wrappers live in `shared/api`? | No for new drafts. `shared/api` owns transport/generated infrastructure only. | New read/command wrappers move to entities/features. |
| `CL-API-PLACEMENT-Q-002` | same | generated types | accepted | Can entities/features import generated OpenAPI types directly? | Yes. Generated types are shared infrastructure; business aliases live in owning entity/feature API files. | Avoids business-aware shared wrappers. |
| `CL-API-PLACEMENT-Q-003` | same | migration | accepted | Should existing shared business wrappers be mass-migrated now? | No. Treat them as transitional compatibility and migrate only in concrete slice/cleanup scope. | Avoids broad client churn. |
| `CL-API-PLACEMENT-Q-004` | same | read placement | accepted | Where do read endpoint wrappers live? | `entities/<entity>/api`. | Employee dashboard/details, Agreement exchange reads. |
| `CL-API-PLACEMENT-Q-005` | same | command placement | accepted | Where do command/mutation endpoint wrappers live? | `features/<business-action>/api`. | Review commands, Agreement exchange commands. |
| `CL-LAYER-Q-001` | old docs | shared API | superseded | `shared/api` is the low-level client/server boundary grouped by layer. | Superseded for business-specific wrappers; shared remains transport/generated boundary only. | Do not copy old shared wrapper shape into new drafts. |

## 3. L2 Account / Employee Identity Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `L2-EMP-ACCOUNT-Q-001` | `domain-draft-02-account-employee-tph-decision.md`, L2 slices | domain identity | accepted | Should Employee be `Account`-derived or linked by `AccountId`? | `Employee : Account`; TPH in `L1Accounts`; `Employee.Id == Account.Id`. | Removes accountId vs employeeId ambiguity. |
| `L2-EMP-ACCOUNT-Q-002` | same | auth/session | accepted | What does `ClaimTypes.NameIdentifier` mean for Employee endpoints? | It stores `Account.Id`; for Employee sessions that is also `Employee.Id`. | Handlers load Employee by id from claim. |
| `L2-EMP-ACCOUNT-Q-003` | same | modeling | accepted | Is Employee a separate profile entity linked by AccountId? | No for L2 target. Treat that shape as compatibility/drift if present. | Prevents `EmployeeProfile(AccountId)` target design. |
| `L2-EMP-ACCOUNT-Q-004` | review/agreement slices | domain actor | accepted | Should review/agreement methods receive `EmployeeRef`? | No. Methods receive `Employee`; owned state stores scalar `Employee.Id` fields. | Keeps new review model consistent. |

## 4. L2 Employee Review Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `L2-REVIEW-Q-001` | SC-07B / review slices | aggregate | accepted | Is Review an aggregate? | No. Request owns RequestReview; no Review repository. | Review commands load Request. |
| `L2-REVIEW-Q-002` | SC-07B / StartReview | entry points | accepted | Does StartReview have one or two client sidecars? | One sidecar with two entry points: dashboard/list row and details action area. | Avoids duplicate StartReview features. |
| `L2-REVIEW-Q-003` | ApproveReview | scope | accepted | Does ApproveReview create AgreementProposalExchange? | No. It only approves request review and sets request Approved. | Exchange starts via SL-AGR-EXCH-001. |
| `L2-REVIEW-Q-004` | RejectReview | feedback | accepted | Is rejection feedback required? | Current direction: optional. Missing/blank feedback allowed unless implementation intentionally changes it. | Client must not block submit solely for missing feedback. |
| `L2-REVIEW-Q-005` | Approve/Reject client sidecars | placement | accepted | Are approve/reject available from dashboard first pass? | No. Details-only first pass. | Prevents risky list-row final decisions. |

## 5. L2 AgreementProposalExchange Slice Boundary Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `L2-AGR-EXCH-Q-001` | agreement slice family | slice split | accepted | Should initial employee proposal and counter-proposal be one implementation slice? | No. Initial employee proposal creates exchange and version 1, so it is `SL-AGR-EXCH-001`. | Avoids create-vs-respond handler branching. |
| `L2-AGR-EXCH-Q-002` | same | counter-proposal split | accepted | Should ClientSendOwnVersion and EmployeeSendNewVersion be split now? | No. Use one `SL-AGR-EXCH-002` with two actor branches until UI/permissions/document handling diverge. | Avoids duplicate near-identical slices. |
| `L2-AGR-EXCH-Q-003` | same | initial exchange | accepted | Can exchange start without initial document? | No in current domain direction. `StartByEmployee(...)` creates exchange with initial document/proposal version. | Full draft must require document reference. |
| `L2-AGR-EXCH-Q-004` | same | revision request | accepted | Is Applicant Request Revision a separate slice? | No for current domain direction. It is represented by client sending own version through `ClientSendOwnVersion(...)`. | Prevents extra revision-request-only slice. |
| `L2-AGR-EXCH-Q-005` | same | final refusal | accepted | Is final refusal a proposal version or separate entity? | No. It is direct exchange state; it does not create a proposal version and no `AgreementFinalRefusal` entity is needed now. | Keeps final refusal slice focused. |

## 6. Agreement Exchange Participant / Ownership Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `AGR-EXCH-PART-Q-001` | agreement slices | client ownership | accepted | How is Client access protected? | Persist `AgreementProposalExchange.ClientAccountId`; client domain methods guard `client.Id == ClientAccountId`. | Participant security. |
| `AGR-EXCH-PART-Q-002` | agreement slices | employee ownership | accepted | Does exchange have ResponsibleEmployeeId guard? | No first pass. Any active Employee can service exchange. | Avoids false employee ownership. |
| `AGR-EXCH-PART-Q-003` | proposal versions | authorship | accepted | Where is Employee/Client identity tracked in history? | Per proposal version through `AgreementProposal.Author.Sender` and `SenderId`. | Supports different employees over time. |
| `AGR-EXCH-PART-Q-004` | agreement commands | actor abstraction | accepted | Introduce AgreementExchangeActor abstraction now? | No first pass. Use current role/account id and explicit service methods/branches. | Simpler implementation. |

## 7. Agreement Exchange Read Slice Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `AGR-EXCH-READ-Q-001` | SL-AGR-EXCH-003, SL-AGR-EXCH-004 | endpoint shape | accepted | Use shared list/details endpoints for Client and Employee first pass? | Yes. Server branches by current session role/access; frontend uses shared entity wrappers/widgets. | Avoids duplicate read slices. |
| `AGR-EXCH-READ-Q-002` | same | client ownership | accepted | How is Client access protected? | Persist and filter by `AgreementProposalExchange.ClientAccountId`. | Requires domain/persistence field. |
| `AGR-EXCH-READ-Q-003` | same | employee ownership | accepted | Does exchange have ResponsibleEmployeeId guard? | No first pass. Any active Employee can service exchange. | Avoids false employee ownership. |
| `AGR-EXCH-READ-Q-004` | SL-AGR-EXCH-004 | read implementation | accepted | Use application service for details read? | No. Use query handler + read repository / Dapper projection. | Keeps read slice projection-only. |
| `AGR-EXCH-READ-Q-005` | client list/details sidecars | frontend ownership | accepted | Actor-specific wrappers first pass? | No while response shape is common. Use shared entity API/query/model and actor page shells. | Avoids duplicated client wrappers. |

## 8. Agreement Exchange Command Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `Q-L2-AGR-CMD-001` | SL-AGR-EXCH-002 | endpoint | accepted | One shared endpoint or separate Client/Employee endpoints for counter-proposal? | One shared endpoint first pass: `POST /api/requests/{requestId}/agreement-exchange/proposals`. | Shared server/client wrapper. |
| `Q-L2-AGR-CMD-002` | SL-AGR-EXCH-002 | route | accepted | Should route use exchangeId or requestId? | Request-scoped route uses `{requestId}`. Do not use `/api/agreement-exchanges/{requestId}/proposals`. | Avoids confusing requestId with exchangeId. |
| `Q-L2-AGR-CMD-003` | SL-AGR-EXCH-002 | actor model | accepted | Introduce AgreementExchangeActor now? | No first pass. Controller resolves role/account id; service branches explicitly; domain owns invariants. | Simpler implementation. |
| `Q-L2-AGR-CMD-004` | exchange command slices | command result | accepted | Add per-command status enums? | No. Use `UnitResult<IReadOnlyList<Error>>` / existing Result/Error model. | Prevents status enum sprawl. |
| `Q-L2-AGR-CMD-005` | SL-AGR-EXCH-005 | accept | accepted | Is accept Client-only? | Yes first pass. Employee accept out of scope. | Client-only endpoint/sidecar. |
| `Q-L2-AGR-CMD-006` | SL-AGR-EXCH-005 | accept state | accepted | Does accept create a new version? | No. Exchange becomes Accepted; active proposal becomes Accepted; proposal count unchanged. | Test assertions. |
| `Q-L2-AGR-CMD-007` | SL-AGR-EXCH-006 | final refusal | accepted | Is final refusal Employee-only? | Yes first pass. Client final refusal out of scope. | Employee-only endpoint/sidecar. |
| `Q-L2-AGR-CMD-008` | SL-AGR-EXCH-006 | final refusal body | accepted | Is reason required? | No. Body/reason nullable; blank/whitespace-only reason invalid if provided. | Validator/controller body binding. |

## 9. Start Agreement Exchange Client Contract Questions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `Q-L2-AGR-START-CLIENT-001` | `L2-AGR-EXCH-START-001.client` | route | blocked | Exact route: request-scoped server route or preferred collection route? | Current server slice uses `POST /api/employee/requests/{requestId}/agreement-exchange/start`; client prefers `POST /api/agreement-exchanges` if response includes exchangeId. Generated OpenAPI decides. | Client wrapper/navigation. |
| `Q-L2-AGR-START-CLIENT-002` | same | response | blocked | Does success return exchangeId? | Preferred for navigation; fallback is 204 + refetch/stay on request details. | Post-success UX. |
| `Q-L2-AGR-START-CLIENT-003` | same | DTO | blocked | Exact initial proposal DTO fields? | Use generated DTO aliases after server contract exists. | Form/API wrapper. |

## 10. Stale / Superseded Decisions

```text
EmployeeRef target model -> superseded by Employee domain object + scalar EmployeeId state.
ResponsibleEmployeeId exchange guard -> rejected for first pass.
Counter-proposal as Rejected -> superseded by SupersededByCounterProposal.
SC-14 Client Data Verification -> stale/deprecated/deferred for current agreement-document context.
Per-command status enum for HTTP mapping -> rejected; use Result/Error mapping.
```
