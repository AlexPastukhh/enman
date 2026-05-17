# Slice Questions Register

Status: active / L2 review command drafts synchronized

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `CL-API-PLACEMENT-Q-001` | `planning/client/client-api-placement-decision.md` | client architecture | accepted | Should business endpoint wrappers live in `shared/api`? | No for new drafts. `shared/api` owns transport/generated infrastructure only. | New read/command wrappers move to entities/features. |
| `CL-API-PLACEMENT-Q-002` | same | generated types | accepted | Can entities/features import generated OpenAPI types directly? | Yes. Generated types are shared infrastructure; business aliases live in owning entity/feature API files. | Avoids business-aware shared wrappers. |
| `CL-API-PLACEMENT-Q-003` | same | migration | accepted | Should existing shared business wrappers be mass-migrated now? | No. Treat them as transitional compatibility and migrate only in concrete slice/cleanup scope. | Avoids broad client churn. |
| `CL-API-PLACEMENT-Q-004` | same | read placement | accepted | Where do read endpoint wrappers live? | `entities/<entity>/api`. | Employee dashboard/details and ApplicantParty reads. |
| `CL-API-PLACEMENT-Q-005` | same | command placement | accepted | Where do command/mutation endpoint wrappers live? | `features/<business-action>/api`. | Start/approve/reject review, make-current-default, create actions. |
| `CL-LAYER-Q-001` | old docs | shared API | superseded | `shared/api` is the low-level client/server boundary grouped by layer. | Superseded for business-specific wrappers; shared remains transport/generated boundary only. | Do not copy old shared wrapper shape into new drafts. |

## L2 Review Command Questions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `Q-L2-REVIEW-START-CLIENT-013` | `L2-REVIEW-START-001-start-request-review.client.md` | entry points | accepted | Are there two StartReview entry points? | Yes. Dashboard/list row and details action area can both host the same feature action. | One feature, two host placements. |
| `Q-L2-REVIEW-START-CLIENT-014` | same | scope | accepted | Should this become two client slices? | No. Same user intent and same endpoint; keep one command sidecar. | Avoids duplicate tiny slices. |
| `Q-L2-REVIEW-START-CLIENT-016` | same | API contract | accepted | Should StartReview client use a response DTO? | No. Success is 204 No Content; refresh read endpoints. | Prevents command/read coupling. |
| `SL-EMP-REQ-004-Q-004` | `SL-EMP-REQ-004-approve-request-review.md` | API contract | accepted | Should approve success return DTO? | No. Return 204 No Content. | Read state comes from refetch. |
| `SL-EMP-REQ-004-Q-005` | same | agreement flow | accepted | Should approve create AgreementProposalExchange? | No. Approval only marks request/review approved. | Agreement flow stays separate. |
| `SL-EMP-REQ-005-Q001` | `SL-EMP-REQ-005-reject-request-review.md` | API contract | accepted | Should reject return DTO? | No. Return 204 No Content. | Client refetches list/details. |
| `SL-EMP-REQ-005-Q002` | same | validation | accepted | Is rejection feedback required? | Yes at API boundary for this slice. Domain optionality is not changed unless separately decided. | DTO/API validation. |
| `SL-EMP-REQ-005-Q003` | same | ownership | accepted | Who can reject? | Only Employee who started the review. | Domain lifecycle rule. |

## L2 Employee Details Client Questions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `Q-L2-EMP-DETAILS-CLIENT-001` | `planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md` | server contract | blocked | What is exact details read endpoint and generated DTO? | Use `SL-EMP-REQ-002` server/OpenAPI once available. Current DTO sketch is derived from SC-07A only. | API wrapper and tests. |
| `Q-L2-EMP-DETAILS-CLIENT-002` | same | generated contract | blocked | What are exact generated operation/type names? | Use generated OpenAPI after server implementation. | `employeeRequestApiTypes.ts`. |
| `Q-L2-EMP-DETAILS-CLIENT-005` | same | read-vs-command | accepted | Is this read or command sidecar? | Read sidecar. Review commands are future feature sidecars. | Placement in `pages + entities`. |
| `Q-L2-EMP-DETAILS-CLIENT-006` | same | scope | accepted | Does this sidecar execute start/approve/reject? | No. It only shows action availability / slots. | Scope boundary. |
| `Q-L2-EMP-DETAILS-CLIENT-007` | same | API placement | accepted | Where does details endpoint wrapper live? | `entities/employee-request/api/getEmployeeRequestDetails.ts`. | New API ownership policy. |
| `Q-L2-EMP-DETAILS-CLIENT-008` | same | shared API | accepted | Can we add `shared/api/employeeRequestApi.ts` for details read? | No. `shared/api` is generic infrastructure only. | Prevents shared API dump. |
| `Q-L2-EMP-DETAILS-CLIENT-010` | same | DTO misuse | accepted | Can `StartReviewResponseDto` be used as details DTO? | No. It is not a details DTO; current StartReview direction is 204 No Content. | Prevents command/read contract coupling. |

## L2 Account / Employee Identity Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `L2-EMP-ACCOUNT-Q-001` | `domain-draft-02-account-employee-tph-decision.md`, L2 slices | domain identity | accepted | Should Employee be `Account`-derived or linked by `AccountId`? | `Employee : Account`; TPH in `L1Accounts`; `Employee.Id == Account.Id`. | Removes accountId vs employeeId ambiguity. |
| `L2-EMP-ACCOUNT-Q-002` | same | auth/session | accepted | What does `ClaimTypes.NameIdentifier` mean for Employee endpoints? | It stores `Account.Id`; for Employee sessions that is also `Employee.Id`. | Handlers load Employee by id from claim. |
| `L2-EMP-ACCOUNT-Q-003` | same | modeling | accepted | Is Employee a separate profile entity linked by AccountId? | No for L2 target. Treat that shape as compatibility/drift if present. | Prevents `EmployeeProfile(AccountId)` target design. |
| `L2-EMP-ACCOUNT-Q-004` | review/agreement slices | domain actor | accepted | Should review/agreement methods receive `EmployeeRef`? | No. Methods receive `Employee`; owned state stores scalar `Employee.Id` fields. | Keeps new review model consistent. |
