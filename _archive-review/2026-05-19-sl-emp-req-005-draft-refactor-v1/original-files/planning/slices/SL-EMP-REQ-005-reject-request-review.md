# SL-EMP-REQ-005 — Reject Request Review

Status: full backend/API command slice draft / implementation-ready after StartReview foundation
Package: `[Employee] [Requests]`
Source scenario: `SC-07B — Employee Request Review`
Slice type: backend/API command slice with client command-sidecar implementation notes
Current implementation status: domain rejection methods exist; API endpoint, application command, client mutation and generated contract are planned in this slice.

## 1. Slice Overview

Target behavior:

```text
Employee opens employee request details.

Request is currently InReview.

Review was started by current Employee.

Employee rejects the request and provides rejection feedback.

System completes the review as Rejected.

Request becomes Rejected.

Employee request list/details show rejected state after refresh.

No agreement proposal flow starts.
```

Command endpoint direction:

```text
POST /api/employee/requests/{requestId}/review/reject
→ 204 No Content
```

No response DTO.

Reason:

```text
Reject is a command.
Updated request status/review state must be read through employee list/details after refetch.
```

Existing domain support:

```text
ConnectionRequest.RejectReview(employee, feedback, decidedAt)
RequestReview.Reject(employee, feedback, decidedAt)
RejectionFeedback
```

## 2. Scope

Implemented scope:

```text
- protected Employee command endpoint;
- CSRF-protected unsafe request;
- current Employee derived from app cookie identity;
- request aggregate loaded by requestId;
- reject allowed only for review started by current Employee;
- API requires non-empty rejection feedback;
- valid rejection feedback is stored;
- request status becomes Rejected;
- review status becomes Rejected;
- command returns 204 No Content;
- client invalidates/refetches employee request list/details.
```

## 3. Out of Scope

| Out-of-scope item                      | Owner / destination                          |
| -------------------------------------- | -------------------------------------------- |
| Start review                           | `SL-EMP-REQ-003`                             |
| Approve review                         | `SL-EMP-REQ-004`                             |
| Agreement proposal exchange            | future agreement slices                      |
| Final refusal / agreement refusal      | future agreement lifecycle decision          |
| Department/assignment visibility       | future employee visibility/permissions slice |
| Optional rejection feedback policy     | future domain/API decision if required       |
| Employee auth / Windows auth changes   | cross-cutting auth slice                     |
| Rewriting employee dashboard UI        | client page slice                            |
| Changing StartReview response contract | not this slice                               |

## 4. Visual Scenario Flow

```text
Employee opens employee request details
        ↓
Request is shown as currently under review by this Employee
        ↓
Employee chooses Reject
        ↓
UI asks for rejection feedback
        ↓
Employee enters feedback and submits
        ↓
 ┌────────────────────────────────┬────────────────────────────────┐
 │ accepted                       │ not accepted                   │
 ▼                                ▼
Request becomes Rejected           Employee sees validation/error
Review is completed as Rejected    feedback and can correct input
        ↓
Employee request details/list
show rejected state after refresh
        ↓
No agreement proposal flow starts
```

Scenario flow intentionally does not mention DTOs, controller, handler, repository, CSRF token, OpenAPI or database columns.

## 5. Scenario Slice Flow

| Step | Actor/system | Behavior                                                                                  | Status             |
| ---- | ------------ | ----------------------------------------------------------------------------------------- | ------------------ |
| F01  | Employee     | Opens request details.                                                                    | target             |
| F02  | System       | Shows request as `InReview`.                                                              | existing read side |
| F03  | System       | Shows review state as `StartedByCurrentEmployee`.                                         | existing read side |
| F04  | Employee     | Chooses reject action.                                                                    | target             |
| F05  | UI           | Requires rejection feedback.                                                              | target             |
| F06  | Employee     | Submits rejection feedback.                                                               | target             |
| F07  | System       | Accepts valid rejection command.                                                          | target             |
| F08  | System       | Completes review as `Rejected`.                                                           | target             |
| F09  | System       | Changes request status to `Rejected`.                                                     | target             |
| F10  | System       | Keeps agreement proposal exchange absent.                                                 | target             |
| F11  | System       | Does not accept reject when review is missing, completed, or started by another employee. | target             |
| F12  | Client       | Refreshes list/details read state after success.                                          | target             |

## 6. API Contract

| Endpoint                                           | Method | Request body           | Response         | Statuses                          |
| -------------------------------------------------- | ------ | ---------------------- | ---------------- | --------------------------------- |
| `/api/employee/requests/{requestId}/review/reject` | POST   | `{ feedback: string }` | `204 No Content` | 204, 400, 401, 403, 404, 422, 500 |

Route:

```text
requestId: long, min(1)
```

Recommended route shape:

```csharp
[HttpPost("{requestId:long:min(1)}/review/reject", Name = "EmployeeRejectRequestReview")]
```

Request DTO:

```csharp
public sealed record EmployeeRejectRequestReviewDto(string Feedback);
```

DTO validation:

```text
feedback required
feedback not whitespace
feedback max length = RejectionFeedback.MaxLength
```

Domain validation:

```text
RejectionFeedback.Create(feedback)
```

Important decision:

```text
The stricter “feedback required” rule belongs to this API slice.

The existing domain method accepts nullable RejectionFeedback.
This slice should not change domain optionality unless a separate domain decision is made.
```

## 7. Questions / Decisions

| ID                    | Status   | Question                                           | Decision / current direction                                                                                                             | Impact                              |
| --------------------- | -------- | -------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------- |
| `SL-EMP-REQ-005-Q001` | accepted | Should reject return DTO?                          | No. Return `204 No Content`.                                                                                                             | Client refetches list/details.      |
| `SL-EMP-REQ-005-Q002` | accepted | Is feedback required?                              | Yes at API boundary for this slice. Domain currently allows nullable `RejectionFeedback`, but this endpoint requires non-empty feedback. | DTO/API validation.                 |
| `SL-EMP-REQ-005-Q003` | accepted | Who can reject?                                    | Only Employee who started the review.                                                                                                    | Domain lifecycle rule.              |
| `SL-EMP-REQ-005-Q004` | accepted | Can another Employee reject started review?        | No. Return lifecycle `422`.                                                                                                              | Prevents cross-employee completion. |
| `SL-EMP-REQ-005-Q005` | accepted | Can not-started request be rejected?               | No. Review must be started.                                                                                                              | Lifecycle `422`.                    |
| `SL-EMP-REQ-005-Q006` | accepted | Can already approved/rejected request be rejected? | No.                                                                                                                                      | Lifecycle `422`.                    |
| `SL-EMP-REQ-005-Q007` | accepted | Should reject start agreement flow?                | No.                                                                                                                                      | Agreement flow remains absent.      |
| `SL-EMP-REQ-005-Q008` | accepted | Should command be CSRF-protected?                  | Yes. Unsafe browser command.                                                                                                             | Server/client tests.                |
| `SL-EMP-REQ-005-Q009` | accepted | Employee identity model?                           | `NameIdentifier = Account.Id = Employee.Id` under Employee TPH.                                                                          | Same identity rule as StartReview.  |

## 8. Domain Behavior

Domain behavior after accepted API input:

```text
- Review must exist.
- Request status must be InReview.
- Review status must be Started.
- Current Employee must be same as StartedByEmployeeId.
- Provided feedback must be valid RejectionFeedback.
- Review status becomes Rejected.
- Review stores CompletedByEmployeeId.
- Review stores CompletedAt.
- Review stores RejectionFeedback.
- Request status becomes Rejected.
```

Existing direction:

```text
ConnectionRequest.RejectReview(employee, feedback, decidedAt)
  - requires Review exists;
  - requires request Status = InReview;
  - delegates completion to RequestReview;
  - sets request Status = Rejected.

RequestReview.Reject(employee, feedback, decidedAt)
  - requires review Status = Started;
  - requires StartedByEmployeeId == employee.Id;
  - stores completed employee/time/feedback;
  - sets review Status = Rejected.
```

Implementation note:

```text
RejectReview should protect against stale inactive Employee consistently with StartReview/ApproveReview direction.

If Employee.EnsureCanReview is not currently called during reject/approve completion, add it or centralize it in RequestReview.CanComplete.
```

## 9. Visual Implementation Flow

```text
[HTTP]
POST /api/employee/requests/{requestId}/review/reject
        ↓
[Auth]
Employee app cookie required
        ↓
[CSRF]
valid antiforgery token required
        ↓
[DTO validation]
feedback required / max length
        ↓
[Controller]
derive current Employee id from app session
        ↓
[Command]
EmployeeRejectRequestReviewCommand(employeeId, requestId, feedback)
        ↓
[Handler]
load Employee
load ConnectionRequest aggregate
create RejectionFeedback
call ConnectionRequest.RejectReview(employee, feedback, now)
        ↓
[Persistence]
SaveChanges
        ↓
[Response]
204 No Content
```

## 10. Backend Implementation Notes

Add command:

```csharp
public sealed record EmployeeRejectRequestReviewCommand(
    long EmployeeId,
    long RequestId,
    string Feedback) : IRequest<EmployeeRejectRequestReviewCommandResult>;
```

Suggested command statuses:

```csharp
public enum EmployeeRejectRequestReviewCommandStatus
{
    Rejected,
    NotFound,
    Forbidden,
    Invalid
}
```

Handler rules:

```text
1. Load Employee by EmployeeId.
2. If Employee not found -> Forbidden.
3. Load request aggregate by RequestId.
4. If request not found -> NotFound.
5. If not ConnectionRequest -> NotFound or Invalid, consistent with existing request handling.
6. Create RejectionFeedback from command.Feedback.
7. If feedback invalid -> Invalid.
8. Call connectionRequest.RejectReview(employee, feedback, now).
9. If domain failure -> Invalid.
10. SaveChangesAsync.
11. Return Rejected.
```

No partial mutation:

```text
If feedback validation or domain lifecycle validation fails,
request status and review state must remain unchanged.
```

## 11. Server API Notes

Controller action follows existing StartReview style:

```text
Employee role authorization
CSRF
current employee id from claims
MediatR command
status mapping to 204 / 404 / 403 / 422
```

Action shape:

```csharp
[Authorize(Roles = "Employee")]
[RequireAntiforgeryToken]
[HttpPost("{requestId:long:min(1)}/review/reject", Name = "EmployeeRejectRequestReview")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public async Task<IActionResult> RejectReview(
    long requestId,
    [FromBody] EmployeeRejectRequestReviewDto dto,
    CancellationToken cancellationToken)
```

Success mapping:

```text
EmployeeRejectRequestReviewCommandStatus.Rejected -> NoContent()
```

Invalid lifecycle/domain/DTO mapping:

```text
ProblemDetailsFromValidation(...)
```

## 12. Client Implementation Notes

API owner:

```text
features/employee-request/reject-review/api/rejectRequestReview.ts
```

Mutation owner:

```text
features/employee-request/reject-review/model/useRejectRequestReviewMutation.ts
```

Do not put wrapper in `shared/api`.

API function:

```ts
export type RejectRequestReviewInput = {
  requestId: number;
  feedback: string;
};

export const rejectRequestReview = async ({
  requestId,
  feedback,
}: RejectRequestReviewInput): Promise<void> => {
  await fetchJson<void>(`/api/employee/requests/${requestId}/review/reject`, {
    method: "POST",
    body: JSON.stringify({ feedback }),
  });
};
```

On success:

```text
invalidate/refetch employee request list query
invalidate/refetch employee request details query
```

UI enablement direction:

```text
Reject action is available when reviewState = StartedByCurrentEmployee.
```

Client validation:

```text
feedback required
feedback max length 1000
```

Server remains authoritative.

## 13. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item                                     | How slice covers it                                   | Status                |
| ------------------------------------------------- | ----------------------------------------------------- | --------------------- |
| Employee can reject own started review            | command completes review as Rejected                  | target                |
| Employee must provide feedback                    | API validator + RejectionFeedback validation          | target                |
| Request becomes Rejected                          | domain sets request status                            | target/current domain |
| Review stores completion metadata                 | RequestReview stores completed employee/time/feedback | target/current domain |
| Another employee cannot reject                    | RequestReview blocks StartedByEmployeeId mismatch     | target/current domain |
| Not-started review cannot be rejected             | ConnectionRequest requires Review exists              | target/current domain |
| Already completed review cannot be rejected again | RequestReview requires Status = Started               | target/current domain |
| Reject does not start agreement flow              | command only changes request/review state             | target                |
| Client reads updated state after refetch          | no command DTO, read endpoints remain source          | target                |

## 14. Test / Verification Plan

| Test / check                                              | Verifies                  | Layer              | Status |
| --------------------------------------------------------- | ------------------------- | ------------------ | ------ |
| unauthenticated reject -> 401                             | auth boundary             | integration        | target |
| Client role reject -> 403                                 | role boundary             | integration        | target |
| missing CSRF -> 400 antiforgery ProblemDetails            | CSRF boundary             | integration        | target |
| request not found -> 404                                  | command lookup            | integration        | target |
| empty feedback -> 422                                     | DTO/API validation        | integration        | target |
| whitespace feedback -> 422                                | DTO/API validation        | integration        | target |
| feedback too long -> 422                                  | DTO/domain validation     | integration/domain | target |
| review not started -> 422 and no mutation                 | lifecycle                 | integration/domain | target |
| review started by another employee -> 422 and no mutation | ownership/lifecycle       | integration/domain | target |
| current employee rejects -> 204 empty body                | success contract          | integration        | target |
| success sets request status Rejected                      | domain/persistence        | integration/domain | target |
| success sets review status Rejected                       | domain/persistence        | integration/domain | target |
| success stores CompletedByEmployeeId                      | identity                  | integration/domain | target |
| success stores RejectionFeedback                          | persistence               | integration/domain | target |
| already approved -> 422 and no mutation                   | lifecycle                 | integration/domain | target |
| already rejected -> 422 and no duplicate mutation         | lifecycle                 | integration/domain | target |
| details after success shows Rejected                      | read model refresh target | integration        | target |
| client wrapper sends POST body                            | client API owner          | client unit        | target |
| mutation invalidates list/details                         | client query behavior     | client unit        | target |

## 15. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/employee/requests/{requestId}/review/reject
request body: EmployeeRejectRequestReviewDto
responses:
  204
  400
  401
  403
  404
  422
  500
```

Generated artifacts must be updated through tools only:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd run check:api
```

## 16. Dependent / Follow-up Slices

```text
SL-EMP-REQ-003 — Start Request Review
SL-EMP-REQ-004 — Approve Request Review
future agreement proposal exchange slices
future employee visibility/assignment slices
future optional rejection feedback policy decision
```

## 17. Implementation Checklist

```text
[ ] add EmployeeRejectRequestReviewDto
[ ] add EmployeeRejectRequestReviewDtoValidator
[ ] add EmployeeRejectRequestReviewCommand
[ ] add EmployeeRejectRequestReviewCommandResult/status
[ ] add EmployeeRejectRequestReviewHandler
[ ] add controller endpoint
[ ] require Employee role
[ ] require CSRF token
[ ] validate feedback at API boundary
[ ] create RejectionFeedback
[ ] call ConnectionRequest.RejectReview
[ ] return 204 No Content on success
[ ] add domain tests for reject lifecycle
[ ] add integration tests for auth/CSRF/validation/lifecycle/success
[ ] add client feature API wrapper
[ ] add client mutation and invalidation
[ ] regenerate OpenAPI/types
```

## 18. Guardrail Summary

```text
RejectReview is a command.

No User Story section in this draft.

Scenario flow describes user/system behavior only.

Implementation flow is separate.

Success response is 204 No Content.

Do not return RejectReviewResponseDto.

Client must refetch read endpoints after success.

Only the Employee who started review can reject.

Feedback is required at API boundary in this slice.

Do not change domain optional feedback policy unless separately decided.

CSRF is required.

Do not implement Approve in this slice.

Do not change Employee auth in this slice.

Do not add assignment/department visibility.
```
