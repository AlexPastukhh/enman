# Slice Questions Register

Status: active / client API placement, L2 review sidecars and AgreementProposalExchange slice boundaries synchronized

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `CL-API-PLACEMENT-Q-001` | `planning/client/client-api-placement-decision.md` | client architecture | accepted | Should business endpoint wrappers live in `shared/api`? | No for new drafts. `shared/api` owns transport/generated infrastructure only. | New read/command wrappers move to entities/features. |
| `CL-API-PLACEMENT-Q-002` | same | generated types | accepted | Can entities/features import generated OpenAPI types directly? | Yes. Generated types are shared infrastructure; business aliases live in owning entity/feature API files. | Avoids business-aware shared wrappers. |
| `CL-API-PLACEMENT-Q-003` | same | migration | accepted | Should existing shared business wrappers be mass-migrated now? | No. Treat them as transitional compatibility and migrate only in concrete slice/cleanup scope. | Avoids broad client churn. |
| `CL-API-PLACEMENT-Q-004` | same | read placement | accepted | Where do read endpoint wrappers live? | `entities/<entity>/api`. | Employee dashboard/details and ApplicantParty reads. |
| `CL-API-PLACEMENT-Q-005` | same | command placement | accepted | Where do command/mutation endpoint wrappers live? | `features/<business-action>/api`. | StartReview, make-current-default, create actions. |
| `CL-LAYER-Q-001` | old docs | shared API | superseded | `shared/api` is the low-level client/server boundary grouped by layer. | Superseded for business-specific wrappers; shared remains transport/generated boundary only. | Do not copy old shared wrapper shape into new drafts. |

## L2 Employee Details Client Questions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `Q-L2-EMP-DETAILS-CLIENT-001` | `planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md` | server contract | blocked | What is exact details read endpoint and generated DTO? | Use `SL-EMP-REQ-002` server/OpenAPI once available. Current DTO sketch is derived from SC-07A only. | API wrapper and tests. |
| `Q-L2-EMP-DETAILS-CLIENT-002` | same | generated contract | blocked | What are exact generated operation/type names? | Use generated OpenAPI after server implementation. | `employeeRequestApiTypes.ts`. |
| `Q-L2-EMP-DETAILS-CLIENT-003` | same | routing | assumption | First route path? | Candidate: `/employee/requests/:requestId`; final path follows server/client route decision. | Route setup. |
| `Q-L2-EMP-DETAILS-CLIENT-004` | same | DTO design | assumption | Should action availability be server-provided? | Prefer server-provided action availability. Client should not guess unless contract explicitly provides all required fields. | DTO design and tests. |
| `Q-L2-EMP-DETAILS-CLIENT-005` | same | read-vs-command | accepted | Is this read or command sidecar? | Read sidecar. Review commands are future feature sidecars. | Placement in `pages + entities`. |
| `Q-L2-EMP-DETAILS-CLIENT-006` | same | scope | accepted | Does this sidecar execute start/approve/reject? | No. It only shows action availability / slots. | Scope boundary. |
| `Q-L2-EMP-DETAILS-CLIENT-007` | same | API placement | accepted | Where does details endpoint wrapper live? | `entities/employee-request/api/getEmployeeRequestDetails.ts`. | New API ownership policy. |
| `Q-L2-EMP-DETAILS-CLIENT-008` | same | shared API | accepted | Can we add `shared/api/employeeRequestApi.ts` for details read? | No. `shared/api` is generic infrastructure only. | Prevents shared API dump. |
| `Q-L2-EMP-DETAILS-CLIENT-009` | same | future data | accepted | Does details include documents/review history? | No, future refinement. | Scope boundary. |
| `Q-L2-EMP-DETAILS-CLIENT-010` | same | DTO misuse | accepted | Can `StartReviewResponseDto` be used as details DTO? | No. It is compact command result only. | Prevents command/read contract coupling. |

## L2 Account / Employee Identity Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `L2-EMP-ACCOUNT-Q-001` | `domain-draft-02-account-employee-tph-decision.md`, L2 slices | domain identity | accepted | Should Employee be `Account`-derived or linked by `AccountId`? | `Employee : Account`; TPH in `L1Accounts`; `Employee.Id == Account.Id`. | Removes accountId vs employeeId ambiguity. |
| `L2-EMP-ACCOUNT-Q-002` | same | auth/session | accepted | What does `ClaimTypes.NameIdentifier` mean for Employee endpoints? | It stores `Account.Id`; for Employee sessions that is also `Employee.Id`. | Handlers load Employee by id from claim. |
| `L2-EMP-ACCOUNT-Q-003` | same | modeling | accepted | Is Employee a separate profile entity linked by AccountId? | No for L2 target. Treat that shape as compatibility/drift if present. | Prevents `EmployeeProfile(AccountId)` target design. |
| `L2-EMP-ACCOUNT-Q-004` | review/agreement slices | domain actor | accepted | Should review/agreement methods receive `EmployeeRef`? | No. Methods receive `Employee`; owned state stores scalar `Employee.Id` fields. | Keeps new review model consistent. |

## L2 AgreementProposalExchange Slice Boundary Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `L2-AGR-EXCH-Q-001` | `planning/slices/l2/L2-agreement-exchange-slice-family.md` | slice split | accepted | Should initial employee proposal and counter-proposal be one implementation slice? | No. Initial employee proposal creates the exchange and version 1, so it is `SL-AGR-EXCH-001`. | Avoids create-vs-respond handler branching. |
| `L2-AGR-EXCH-Q-002` | same | counter-proposal split | accepted | Should ClientSendOwnVersion and EmployeeSendNewVersion be split now? | No. Use one `SL-AGR-EXCH-002` with two actor branches until UI/permissions/document handling diverge. | Avoids duplicate near-identical slices. |
| `L2-AGR-EXCH-Q-003` | same | initial exchange | accepted | Can exchange start without initial document? | No in current domain direction. `StartByEmployee(...)` creates exchange with initial document/proposal version. | Full draft must require document reference. |
| `L2-AGR-EXCH-Q-004` | same | revision request | accepted | Is Applicant Request Revision a separate slice? | No for current domain direction. It is represented by client sending own version through `ClientSendOwnVersion(...)`. | Prevents extra вЂњrevision request onlyвЂќ slice. |
| `L2-AGR-EXCH-Q-005` | same | final refusal | accepted | Is final refusal a proposal version or separate entity? | No. It is direct exchange state; it does not create a proposal version and no `AgreementFinalRefusal` entity is needed now. | Keeps final refusal slice focused. |

## Agreement Exchange Read Slice Decisions

Marker: AGR-EXCH-READ-SLICE-DECISIONS-2026-05

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| AGR-EXCH-READ-Q-001 | SL-AGR-EXCH-003, SL-AGR-EXCH-004 | endpoint shape | accepted | Use shared list/details endpoints for Client and Employee first pass? | Yes. Server branches by current session role/access; frontend uses shared entity wrappers/widgets. | Avoids duplicate read slices. |
| AGR-EXCH-READ-Q-002 | same | client ownership | accepted | How is Client access protected? | Persist and filter by AgreementProposalExchange.ClientAccountId. | Requires domain/persistence field. |
| AGR-EXCH-READ-Q-003 | same | employee ownership | accepted | Does exchange have ResponsibleEmployeeId guard? | No first pass. Any active Employee can service exchange. | Avoids false employee ownership. |
| AGR-EXCH-READ-Q-004 | SL-AGR-EXCH-004 | read implementation | accepted | Use application service for details read? | No. Use query handler + read repository / Dapper projection. | Keeps read slice projection-only. |
| AGR-EXCH-READ-Q-005 | L2-AGR-EXCH-LIST-001.client, L2-AGR-EXCH-DETAILS-001.client | frontend ownership | accepted | Actor-specific wrappers first pass? | No while response shape is common. Use shared entity API/query/model and actor page shells. | Avoids duplicated client wrappers. |


<!-- L2-AGR-EXCH-COMMAND-SLICES-SYNC -->
## Agreement Exchange Command Questions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| Q-L2-AGR-CMD-001 | SL-AGR-EXCH-002 | endpoint | accepted | One shared endpoint or separate Client/Employee endpoints for counter-proposal? | One shared endpoint first pass: POST /api/requests/{requestId}/agreement-exchange/proposals. | Shared server/client wrapper. |
| Q-L2-AGR-CMD-002 | SL-AGR-EXCH-002 | actor model | accepted | Introduce AgreementExchangeActor now? | No first pass. Controller resolves role/account id; service branches explicitly; domain owns invariants. | Simpler implementation. |
| Q-L2-AGR-CMD-003 | SL-AGR-EXCH-002, SL-AGR-EXCH-005 | ownership | accepted | How are client actions protected? | AgreementProposalExchange.ClientAccountId; client domain methods guard client.Id == ClientAccountId. | Participant security. |
| Q-L2-AGR-CMD-004 | exchange command slices | employee ownership | accepted | Is there ResponsibleEmployeeId guard? | No first pass. Any active Employee can service exchange; proposal sender is tracked per version. | Avoids false employee ownership. |
| Q-L2-AGR-CMD-005 | SL-AGR-EXCH-005 | accept | accepted | Is accept Client-only? | Yes first pass. Employee accept out of scope. | Client-only endpoint/sidecar. |
| Q-L2-AGR-CMD-006 | SL-AGR-EXCH-005 | accept state | accepted | Does accept create a new version? | No. Exchange becomes Accepted; active proposal becomes Accepted; proposal count unchanged. | Test assertions. |


<!-- AGR-EXCH-FINAL-REFUSE-SYNC -->
## Agreement exchange final refusal decisions

```text
SL-AGR-EXCH-006:
- Employee-only first pass;
- Client final refusal is out of scope;
- nullable body is allowed;
- missing/null reason is allowed;
- blank/whitespace-only reason is invalid;
- no ResponsibleEmployeeId guard;
- no command status enum;
- final refusal affects AgreementProposalExchange and related ConnectionRequest;
- application orchestrates both aggregate domain methods;
- success returns 204 No Content.
```
<!-- /AGR-EXCH-FINAL-REFUSE-SYNC -->

