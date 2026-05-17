# Slice Questions Register

Status: active / L2 review, client API placement and AgreementProposalExchange canonical slice decisions synchronized

## 1. Client API Placement Questions

| ID | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|
| `CL-API-PLACEMENT-Q-001` | client architecture | accepted | Should business endpoint wrappers live in `shared/api`? | No for new drafts. `shared/api` owns transport/generated infrastructure only. | New read/command wrappers move to entities/features. |
| `CL-API-PLACEMENT-Q-002` | generated types | accepted | Can entities/features import generated OpenAPI types directly? | Yes. Generated types are shared infrastructure; business aliases live in owning entity/feature API files. | Avoids business-aware shared wrappers. |
| `CL-API-PLACEMENT-Q-003` | migration | accepted | Should existing shared business wrappers be mass-migrated now? | No. Treat them as transitional compatibility and migrate only in concrete slice/cleanup scope. | Avoids broad client churn. |
| `CL-API-PLACEMENT-Q-004` | read placement | accepted | Where do read endpoint wrappers live? | `entities/<entity>/api`. | Employee and agreement read wrappers. |
| `CL-API-PLACEMENT-Q-005` | command placement | accepted | Where do command/mutation endpoint wrappers live? | `features/<business-action>/api`. | Review and agreement command wrappers. |

## 2. L2 Account / Employee Identity Decisions

| ID | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|
| `L2-EMP-ACCOUNT-Q-001` | domain identity | accepted | Should Employee be `Account`-derived or linked by `AccountId`? | `Employee : Account`; TPH in `L1Accounts`; `Employee.Id == Account.Id`. | Removes accountId vs employeeId ambiguity. |
| `L2-EMP-ACCOUNT-Q-002` | auth/session | accepted | What does `ClaimTypes.NameIdentifier` mean for Employee endpoints? | It stores `Account.Id`; for Employee sessions that is also `Employee.Id`. | Handlers load Employee by id from claim. |
| `L2-EMP-ACCOUNT-Q-003` | modeling | accepted | Is Employee a separate profile entity linked by AccountId? | No for L2 target. Treat that shape as compatibility/drift if present. | Prevents `EmployeeProfile(AccountId)` target design. |
| `L2-EMP-ACCOUNT-Q-004` | domain actor | accepted | Should review/agreement methods receive `EmployeeRef`? | No. Methods receive `Employee`; owned state stores scalar `Employee.Id` fields. | Keeps new review/agreement model consistent. |

## 3. L2 Employee Details Client Questions

| ID | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|
| `Q-L2-EMP-DETAILS-CLIENT-001` | server contract | blocked | What is exact details read endpoint and generated DTO? | Use `SL-EMP-REQ-002` server/OpenAPI. Current DTO sketch is scenario-derived only until contract exists. | API wrapper and tests. |
| `Q-L2-EMP-DETAILS-CLIENT-002` | generated contract | blocked | What are exact generated operation/type names? | Use generated OpenAPI after server implementation. | `employeeRequestApiTypes.ts`. |
| `Q-L2-EMP-DETAILS-CLIENT-003` | routing | assumption | First route path? | Candidate: `/employee/requests/:requestId`; final path follows route decision. | Route setup. |
| `Q-L2-EMP-DETAILS-CLIENT-004` | DTO design | assumption | Should action availability be server-provided? | Prefer server-provided action availability. Client should not guess unless contract provides all required fields. | DTO design and tests. |
| `Q-L2-EMP-DETAILS-CLIENT-005` | read-vs-command | accepted | Is this read or command sidecar? | Read sidecar. Review/agreement commands are feature sidecars hosted by action slots. | Scope boundary. |
| `Q-L2-EMP-DETAILS-CLIENT-006` | scope | accepted | Does this sidecar execute start/approve/reject/start-exchange? | No. It shows action availability / slots. | Scope boundary. |

## 4. L2 Review Decisions

| ID | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|
| `Q-L2-REVIEW-001` | model | accepted | Is Review an aggregate? | No. Request owns RequestReview; no Review repository. | Review commands load/mutate Request. |
| `Q-L2-REVIEW-002` | StartReview UX | accepted | Can StartReview be initiated from dashboard and details? | Yes. One command sidecar, two entry points. | Avoids duplicate sidecars. |
| `Q-L2-REVIEW-003` | ApproveReview | accepted | Does approval create AgreementProposalExchange? | No. Approval only completes review and makes request Approved. | Exchange starts through SL-AGR-EXCH-001. |
| `Q-L2-REVIEW-004` | RejectReview feedback | accepted | Is rejection feedback required? | No current direction: feedback/body is optional unless server/OpenAPI intentionally changes it. | Client must not block empty feedback by default. |

## 5. AgreementProposalExchange Slice Boundary Decisions

| ID | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|
| `L2-AGR-EXCH-Q-001` | slice split | accepted | Should initial employee proposal and counter-proposal be one implementation slice? | No. Initial employee proposal creates the exchange and version 1, so it is `SL-AGR-EXCH-001`. | Avoids create-vs-respond branching. |
| `L2-AGR-EXCH-Q-002` | counter-proposal split | accepted | Should ClientSendOwnVersion and EmployeeSendNewVersion be split now? | No. Use one `SL-AGR-EXCH-002` with two actor branches until behavior diverges. | Avoids duplicate near-identical slices. |
| `L2-AGR-EXCH-Q-003` | initial exchange | accepted | Can exchange start without initial document? | No. `StartByEmployee(...)` creates exchange with initial document/proposal version. | Full draft requires document reference. |
| `L2-AGR-EXCH-Q-004` | revision request | accepted | Is Applicant Request Revision a separate slice? | No for current domain direction. It is represented by client sending own version through `ClientSendOwnVersion(...)`. | Prevents extra revision-only slice. |
| `L2-AGR-EXCH-Q-005` | final refusal | accepted | Is final refusal a proposal version or separate entity? | No. It is direct exchange state; it does not create proposal version and no `AgreementFinalRefusal` entity is needed now. | Keeps final refusal slice focused. |
| `L2-AGR-EXCH-Q-006` | read split | accepted | Is one generic read slice enough? | No. Canonical split: `003` list read, `004` details read. | Prevents old 003/004/005 numbering drift. |

## 6. Agreement Exchange Read Decisions

| ID | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|
| `AGR-EXCH-READ-Q-001` | endpoint shape | accepted | Use shared list/details endpoints for Client and Employee first pass? | Yes. Server branches by current session role/access; frontend uses shared entity wrappers/widgets. | Avoids duplicate read slices. |
| `AGR-EXCH-READ-Q-002` | client ownership | accepted | How is Client access protected? | Persist and filter by `AgreementProposalExchange.ClientAccountId`. | Requires domain/persistence field. |
| `AGR-EXCH-READ-Q-003` | employee ownership | accepted | Does exchange have `ResponsibleEmployeeId` guard? | No first pass. Any active Employee can service exchange. | Avoids false employee ownership. |
| `AGR-EXCH-READ-Q-004` | read implementation | accepted | Use application service for details read? | No. Use query handler + read repository / Dapper projection. | Keeps read slice projection-only. |
| `AGR-EXCH-READ-Q-005` | frontend ownership | accepted | Actor-specific wrappers first pass? | No while response shape is common. Use shared entity API/query/model and actor page shells. | Avoids duplicated client wrappers. |

## 7. Agreement Exchange Command Decisions

| ID | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|
| `Q-L2-AGR-CMD-001` | counter-proposal endpoint | accepted | One shared endpoint or separate Client/Employee endpoints? | One shared endpoint first pass: `POST /api/requests/{requestId}/agreement-exchange/proposals`. | Shared server/client wrapper. |
| `Q-L2-AGR-CMD-002` | actor model | accepted | Introduce AgreementExchangeActor now? | No first pass. Controller resolves role/account id; service branches explicitly; domain owns invariants. | Simpler implementation. |
| `Q-L2-AGR-CMD-003` | ownership | accepted | How are client actions protected? | `AgreementProposalExchange.ClientAccountId`; client domain methods guard `client.Id == ClientAccountId`. | Participant security. |
| `Q-L2-AGR-CMD-004` | employee ownership | accepted | Is there ResponsibleEmployeeId guard? | No first pass. Any active Employee can service exchange; proposal sender is tracked per version. | Avoids false employee ownership. |
| `Q-L2-AGR-CMD-005` | accept | accepted | Is accept Client-only? | Yes first pass. Employee accept out of scope. | Client-only endpoint/sidecar. |
| `Q-L2-AGR-CMD-006` | accept state | accepted | Does accept create a new version? | No. Exchange becomes Accepted; active proposal becomes Accepted; proposal count unchanged. | Test assertions. |
| `Q-L2-AGR-CMD-007` | command result | accepted | Add per-command status enums? | No. Use `UnitResult<IReadOnlyList<Error>>` / existing Result/Error mapping. | Prevents enum noise. |

## 8. Agreement Exchange Start Client Questions

| ID | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|
| `Q-L2-AGR-START-CLIENT-001` | endpoint route | blocked | Exact start endpoint route? | Server draft currently has `POST /api/employee/requests/{requestId}/agreement-exchange/start`; preferred client UX asks for `POST /api/agreement-exchanges`. Generated OpenAPI decides final. | API wrapper and navigation. |
| `Q-L2-AGR-START-CLIENT-002` | initial proposal DTO | blocked | Exact initial proposal DTO fields? | Use generated DTO aliases. | Form/API types. |
| `Q-L2-AGR-START-CLIENT-003` | response DTO | blocked | Does response include exchangeId? | Preferred yes for navigation; fallback is 204 with refetch/stay. | Navigation after success. |
| `Q-L2-AGR-START-CLIENT-004` | placement | accepted | Where does StartAgreementExchange UI live? | Employee request details action area, because exchange does not exist yet. | Page/feature placement. |
| `Q-L2-AGR-START-CLIENT-005` | scope | accepted | Is this send proposal inside existing exchange? | No. This creates exchange and initial proposal. | Prevents feature mixing. |

## 9. Agreement Exchange Final Refusal Decisions

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
