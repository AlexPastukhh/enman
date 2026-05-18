# SL-AGR-EXCH-001 вЂ” Start Agreement Exchange With Initial Employee Proposal

Status: draft / server implementation-ready  
Package: `[L2] Agreement Proposal Exchange`  
Source scenario: approved request moves from review decision to agreement proposal exchange  
Slice type: server command slice  
Depends on: `SL-EMP-REQ-004 вЂ” Approve Request Review`, Employee auth/session, CSRF, L2 domain agreement proposal model  
Current implementation status: domain model exists; API endpoint/application command/persistence repository are planned in this slice.

## 1. Slice Overview

Target behavior:

```text
Employee opens approved connection request.
Request review has already been approved.
Employee starts agreement proposal exchange by sending the first agreement proposal document.
System creates AgreementProposalExchange.
System stores the request owner ClientAccountId on the exchange.
System creates proposal version 1 authored by Employee.
Exchange waits for client confirmation.
Command returns 204 No Content.
```

Command endpoint direction:

```text
POST /api/employee/requests/{requestId}/agreement-exchange/start
в†’ 204 No Content
```

No response DTO.

Reason:

```text
This is a command.
Created exchange/proposal state should be read through future agreement exchange read endpoint/details refresh.
```

Current domain direction:

```text
AgreementProposalExchange.StartByEmployee(
    approvedRequest,
    document,
    comment,
    employee,
    startedAt)
```

This method creates the exchange, stores the exchange client-side participant, creates the first employee proposal version, and sets exchange status to `AwaitingClientConfirmation`.

## 2. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-001-Q001` | accepted | Does approve automatically start exchange? | No. Exchange starts by explicit employee command. | Keeps review and agreement lifecycle separated. |
| `SL-AGR-EXCH-001-Q002` | accepted | Is this вЂњempty startвЂќ or вЂњstart with first proposalвЂќ? | Start with initial employee proposal. | Body requires document ref. |
| `SL-AGR-EXCH-001-Q003` | accepted | Who can start exchange? | Employee with app cookie role Employee. | Auth boundary. |
| `SL-AGR-EXCH-001-Q004` | accepted | Request precondition? | ConnectionRequest must be `Approved`. | Domain rule. |
| `SL-AGR-EXCH-001-Q005` | accepted | First proposal version? | Version `1`. | Domain uses `AgreementProposalVersion.First`. |
| `SL-AGR-EXCH-001-Q006` | accepted | Initial exchange status? | `AwaitingClientConfirmation`. | Client can respond later. |
| `SL-AGR-EXCH-001-Q007` | accepted | Duplicate exchange for same request? | Reject through existing Error/ProblemDetails mapping. Prefer conflict semantics, but error mapping cleanup can be separate. | Repository/application guard. |
| `SL-AGR-EXCH-001-Q008` | accepted | Does this command change request status? | No first pass. Request remains `Approved`; exchange has own status. | Avoids new request status decision. |
| `SL-AGR-EXCH-001-Q009` | accepted | File upload? | Out of scope. Store document reference only. | No binary storage. |
| `SL-AGR-EXCH-001-Q010` | accepted | Success response? | `204 No Content`. | Read state comes later from read endpoint. |
| `SL-AGR-EXCH-001-Q011` | accepted | CSRF? | Required. Unsafe browser command. | Server tests. |
| `SL-AGR-EXCH-001-Q012` | accepted | Command status enum? | Do not introduce per-command status enum. Use `Result` / `UnitResult<IReadOnlyList<Error>>` + existing Error mapping. | Avoids application status enum noise. |
| `SL-AGR-EXCH-001-Q013` | accepted | Controller placement? | Use separate `EmployeeAgreementExchangeController`. | Prevents `EmployeeRequestsController` from growing after Start/Approve/Reject. |
| `SL-AGR-EXCH-001-Q014` | accepted | Endpoint route shape? | `POST /api/employee/requests/{requestId}/agreement-exchange/start`. | Keeps exchange scoped under employee request. |
| `SL-AGR-EXCH-001-Q015` | accepted | Comment behavior? | `comment` is optional; null/blank means no comment. | Create `ProposalComment` only for meaningful text. |
| `SL-AGR-EXCH-001-Q016` | accepted | Document input? | First pass stores `AgreementDocumentRef` only. | No binary upload/document generation. |
| `SL-AGR-EXCH-001-Q017` | accepted | Repository shape? | Add agreement exchange repository with `GetByRequestIdAsync` and `Add`. | Supports duplicate guard and aggregate persistence. |
| `SL-AGR-EXCH-001-Q018` | accepted | DB uniqueness? | Prefer unique exchange per `RequestId` if practical. | Prevents duplicate exchanges under race conditions. |
| `SL-AGR-EXCH-001-Q019` | accepted | Failure HTTP mapping? | Use existing Error/ProblemDetails mapper. Do not add command statuses for HTTP mapping. | Error mapping cleanup remains separate. |
| `SL-AGR-EXCH-001-Q020` | accepted | 409 vs 422 for duplicate? | Do not block this slice on mapping cleanup. Use current mapper; prefer future `409 Conflict` if/when mapping supports it. | Keeps slice implementable now. |
| `SL-AGR-EXCH-001-Q021` | accepted | Should exchange store client-side participant id? | Yes. Add `AgreementProposalExchange.ClientAccountId`. | Enables domain ownership checks for future client actions. |
| `SL-AGR-EXCH-001-Q022` | accepted | Where does `ClientAccountId` come from? | From approved request owner, preferably `approvedRequest.ClientAccountId` or `approvedRequest.GetOwnerClientAccountId()`. | Start command initializes exchange participant. |
| `SL-AGR-EXCH-001-Q023` | accepted | Should exchange store `ResponsibleEmployeeId` as guard? | No first pass. Any active Employee can service exchange; proposal authors track employee sender per version. | Avoids employee ownership lock/assignment model. |

## 3. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How slice covers it | Status |
|---|---|---|
| Employee starts exchange for approved request | command calls `AgreementProposalExchange.StartByEmployee(...)` | target/current domain |
| Exchange stores client-side participant | `ClientAccountId` is copied from approved request owner at start | target |
| Initial proposal document is required | DTO + `AgreementDocumentRef` validation | target |
| Comment is optional | only creates `ProposalComment` when non-blank | target |
| First proposal version is 1 | domain uses `AgreementProposalVersion.First` | current domain |
| First proposal is authored by Employee | domain creates Employee-authored proposal with `Sender = Employee`, `SenderId = employee.Id` | current domain |
| Exchange waits for client confirmation | domain sets `AwaitingClientConfirmation` | current domain |
| Not approved request cannot start exchange | domain rejects non-Approved request | current domain |
| Duplicate exchange cannot be started | repository/application duplicate guard + preferred unique `RequestId` | target |
| Approve does not create exchange | separate command boundary | target/current implementation |
| No binary file upload | document reference only | target |
| No employee ownership lock | no `ResponsibleEmployeeId` guard; any active Employee can service first pass | target |

## 4. Scope

Implemented scope:

```text
- protected Employee command endpoint;
- CSRF-protected unsafe request;
- current Employee derived from app cookie identity;
- approved ConnectionRequest loaded by requestId;
- agreement document reference accepted from request body;
- optional proposal comment accepted from request body;
- AgreementProposalExchange aggregate created;
- exchange ClientAccountId populated from approved request owner;
- first AgreementProposal version created by Employee;
- proposal author stores Sender = Employee and SenderId = employee.Id;
- exchange status becomes AwaitingClientConfirmation;
- active proposal version becomes 1;
- command returns 204 No Content;
- duplicate exchange for same request is rejected through existing Error/ProblemDetails mapping.
```

## 5. Out of Scope

| Out-of-scope item | Owner / destination |
|---|---|
| Approve review | `SL-EMP-REQ-004` |
| Automatic exchange creation during approve | explicitly not this slice |
| Agreement exchange list/details reads | `SL-AGR-EXCH-003` / `SL-AGR-EXCH-004` |
| Client accept proposal | `SL-AGR-EXCH-005` |
| Client counter-proposal | `SL-AGR-EXCH-002` |
| Employee revised proposal | `SL-AGR-EXCH-002` |
| Final refusal | `SL-AGR-EXCH-006` |
| Responsible employee assignment / ownership lock | future assignment/queue slice, if ever needed |
| Department/region employee visibility | future permission slice |
| Binary file upload/storage | future file/document slice |
| Real document generation | future document generation slice |
| Client UI | client exchange slice |
| Employee dashboard redesign | employee page slice |
| Error mapping cleanup | separate cleanup if existing errors are not expressive enough |

Important boundary:

```text
ApproveReview does not start agreement exchange.
ApproveReview only completes review decision and sets request status to Approved.
Agreement exchange starts only through this explicit command.
```

## 6. Visual Scenario Flow

```text
Employee opens approved request details
        в†“
Request is shown as Approved
        в†“
Employee chooses Start agreement exchange
        в†“
Employee provides agreement document reference
        в†“
Employee optionally provides proposal comment
        в†“
Employee submits
        в†“
 в”Њв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”¬в”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”Ђв”ђ
 в”‚ accepted                       в”‚ not accepted                   в”‚
 в–ј                                в–ј
Agreement exchange is created      Employee sees validation/error
ClientAccountId is stored          feedback and can correct input
Proposal version 1 is created
        в†“
Exchange waits for client confirmation
        в†“
No client decision is made yet
```

Scenario flow intentionally does not mention controller, handler, repository, EF, OpenAPI, CSRF token or database columns.

## 7. Scenario Slice Flow

| Step | Actor/system | Behavior | Status |
|---|---|---|---|
| F01 | Employee | Opens approved request details. | existing/future read side |
| F02 | System | Shows request as `Approved`. | existing after approve |
| F03 | Employee | Chooses start agreement exchange action. | target |
| F04 | Employee | Provides agreement document reference. | target |
| F05 | Employee | Optionally provides proposal comment. | target |
| F06 | System | Validates command can start exchange for approved request. | target |
| F07 | System | Creates agreement exchange. | target |
| F08 | System | Stores `ClientAccountId` from approved request owner. | target |
| F09 | System | Creates first employee proposal version. | target |
| F10 | System | Marks exchange as waiting for client confirmation. | target |
| F11 | System | Rejects duplicate exchange start for same request. | target |
| F12 | System | Does not change request status in this slice unless later explicitly decided. | target |

## 8. API Contract

| Endpoint | Method | Request body | Response | Statuses |
|---|---|---|---|---|
| `/api/employee/requests/{requestId}/agreement-exchange/start` | POST | `{ documentRef: string, comment?: string }` | `204 No Content` | 204, 400, 401, 403, 404, 409/422, 500 |

Route:

```text
requestId: long, min(1)
```

Route shape:

```csharp
[ApiController]
[Route("api/employee/requests/{requestId:long:min(1)}/agreement-exchange")]
public sealed class EmployeeAgreementExchangeController : ProjectController
{
    [Authorize(Roles = "Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("start", Name = "EmployeeStartAgreementExchange")]
    public async Task<IActionResult> Start(...)
}
```

Request DTO:

```csharp
public sealed record EmployeeStartAgreementExchangeDto(
    string? DocumentRef,
    string? Comment);
```

DTO validation:

```text
documentRef required
documentRef not whitespace
documentRef max length = AgreementDocumentRef.MaxLength, if exposed
comment optional
comment max length = ProposalComment.MaxLength, if provided
```

Domain validation:

```text
AgreementDocumentRef.Create(documentRef)
ProposalComment.Create(comment), only if comment is not blank/null
AgreementProposalExchange.StartByEmployee(...)
```

Decision on blank comment:

```text
Blank/null comment should be treated as no comment.
If ProposalComment.Create rejects blank values, call it only when comment has meaningful text.
```

## 9. Domain Behavior

Current target domain behavior:

```text
AgreementProposalExchange.StartByEmployee:
- requires approvedRequest not null;
- requires approvedRequest.Id > 0;
- requires approvedRequest.Status = Approved;
- obtains ClientAccountId from approvedRequest owner;
- requires ClientAccountId > 0;
- requires document;
- requires employee;
- requires employee.Id > 0;
- checks employee.EnsureCanStartAgreementExchange();
- creates exchange;
- sets RequestId;
- sets ClientAccountId;
- sets Status = AwaitingClientConfirmation;
- sets ActiveProposalVersion = First;
- creates employee proposal version 1;
- stores document/comment/employee author/createdAt.
```

Connection request approve behavior is separate:

```text
ConnectionRequest.ApproveReview:
- requires review exists;
- requires request Status = InReview;
- completes review as approved;
- sets request Status = Approved;
- does not create agreement exchange.
```

Target `AgreementProposalExchange` state:

```text
RequestId
ClientAccountId
Status
ActiveProposalVersion
Proposals
```

Target first-pass Employee access:

```text
No ResponsibleEmployeeId guard.
Any active Employee can service the exchange.
Employee authorship is recorded per proposal version.
```

## 10. Application Result Model

Use existing `Result` / `UnitResult` + `Error` model.

Do **not** add:

```csharp
EmployeeStartAgreementExchangeCommandStatus
```

Do **not** use `AgreementExchangeStatus` as command execution result.

Preferred command shape:

```csharp
public sealed record EmployeeStartAgreementExchangeCommand(
    long EmployeeId,
    long RequestId,
    string DocumentRef,
    string? Comment)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;
```

Controller mapping direction:

```text
Success:
  UnitResult.Success -> 204 No Content

Failure:
  existing ProblemDetails/error mapper decides response from Error codes
```

If current Error mapping cannot distinguish not found / forbidden / duplicate conflict cleanly, do not add per-command status enum as a workaround in this slice. Improve Error codes / ProblemDetails mapping separately.

## 11. Visual Implementation Flow

```text
[HTTP]
POST /api/employee/requests/{requestId}/agreement-exchange/start
        в†“
[Auth]
Employee app cookie required
        в†“
[CSRF]
valid antiforgery token required
        в†“
[DTO validation]
documentRef required / comment max length
        в†“
[Controller]
derive current Employee id from app session
        в†“
[Command]
EmployeeStartAgreementExchangeCommand(employeeId, requestId, documentRef, comment)
        в†“
[Handler]
load Employee
load ConnectionRequest aggregate
ensure no AgreementProposalExchange exists for request
create AgreementDocumentRef
create optional ProposalComment
call AgreementProposalExchange.StartByEmployee(...)
        в†“
[Domain]
exchange stores RequestId and ClientAccountId
exchange creates Employee proposal version 1
        в†“
[Persistence]
add AgreementProposalExchange
SaveChanges
        в†“
[Response]
204 No Content
```

## 12. Backend Implementation Notes

Handler rules:

```text
1. Load Employee by EmployeeId.
2. If Employee not found -> return existing forbidden/current employee error.
3. Load request by RequestId.
4. If request not found -> return existing request not found/request required error.
5. If request is not ConnectionRequest -> return existing not-found or lifecycle error.
6. Ensure request can expose owner ClientAccountId.
7. Check whether exchange already exists for RequestId.
8. If exchange exists -> return agreement-exchange-already-exists error.
9. Create AgreementDocumentRef from command.DocumentRef.
10. If document invalid -> return domain errors.
11. Create ProposalComment only if command.Comment is not null/whitespace.
12. If comment invalid -> return domain errors.
13. Call AgreementProposalExchange.StartByEmployee(connectionRequest, document, comment, employee, now).
14. If domain failure -> return domain errors.
15. Add exchange aggregate to context/repository.
16. SaveChangesAsync.
17. Return success.
```

No partial mutation:

```text
If document/comment validation or domain lifecycle validation fails,
no exchange/proposal should be persisted.
```

## 13. Persistence / Repository Notes

Repository decision:

```csharp
public interface IAgreementProposalExchangeRepository
{
    Task<AgreementProposalExchange?> GetByRequestIdAsync(
        long requestId,
        CancellationToken cancellationToken);

    void Add(AgreementProposalExchange exchange);
}
```

EF persistence needs to support:

```text
AgreementProposalExchange:
  Id
  RequestId
  ClientAccountId
  Status
  ActiveProposalVersion
  CreatedAt
  FinalRefusedByEmployeeId nullable
  FinalRefusedAt nullable
  FinalRefusalReason nullable

AgreementProposal:
  Id
  AgreementProposalExchangeId
  Version
  Author sender/id
  State
  Document ref
  Comment nullable
  CreatedAt
```

Recommended exchange indexes:

```text
IX_L1AgreementProposalExchanges_ClientAccountId
UX_L1AgreementProposalExchanges_RequestId
```

## 14. Server API Notes

Use separate controller:

```csharp
[ApiController]
[Route("api/employee/requests/{requestId:long:min(1)}/agreement-exchange")]
public sealed class EmployeeAgreementExchangeController : ProjectController
{
}
```

Endpoint:

```csharp
[Authorize(Roles = "Employee")]
[RequireAntiforgeryToken]
[HttpPost("start", Name = "EmployeeStartAgreementExchange")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public async Task<IActionResult> Start(
    long requestId,
    [FromBody] EmployeeStartAgreementExchangeDto dto,
    CancellationToken cancellationToken)
```

Reason:

```text
EmployeeRequestsController already owns request list/details and review commands.
Agreement exchange will grow into its own package, so use a dedicated controller from the first slice.
```

## 15. DTO Validation

Validator:

```csharp
public sealed class EmployeeStartAgreementExchangeDtoValidator
    : AbstractValidator<EmployeeStartAgreementExchangeDto>
{
    public EmployeeStartAgreementExchangeDtoValidator()
    {
        RuleFor(x => x.DocumentRef)
            .NotEmpty()
            .MaximumLength(AgreementDocumentRef.MaxLength);

        RuleFor(x => x.Comment)
            .MaximumLength(ProposalComment.MaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Comment));
    }
}
```

Do not rely only on DTO validation:

```text
Domain value objects remain authoritative.
```

## 16. Test / Verification Plan

Primary verification: API integration tests with DB/domain state assertions.

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| unauthenticated start -> 401 | auth boundary | integration | target |
| Client role start -> 403 | role boundary | integration | target |
| missing CSRF -> 400 antiforgery ProblemDetails | CSRF boundary | integration | target |
| request not found -> mapped ProblemDetails | command lookup | integration | target |
| request not approved -> mapped lifecycle ProblemDetails | lifecycle | integration/domain | target |
| missing/blank documentRef -> 422 | DTO/API validation | integration | target |
| too long documentRef/comment -> 422 | DTO/domain validation | integration/domain | target |
| approved request + valid document -> 204 | success contract | integration | target |
| success creates exchange row | persistence | integration | target |
| success stores exchange ClientAccountId | client participant ownership | integration/domain | target |
| success creates proposal version 1 | persistence/domain | integration/domain | target |
| success sets active version 1 | domain/persistence | integration/domain | target |
| success sets exchange AwaitingClientConfirmation | domain/persistence | integration/domain | target |
| success stores employee author | identity | integration/domain | target |
| duplicate start -> mapped conflict/lifecycle ProblemDetails | duplicate guard | integration | target |
| approve command alone does not create exchange | boundary between review and exchange | integration | target |

## 17. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/employee/requests/{requestId}/agreement-exchange/start
request body: EmployeeStartAgreementExchangeDto
responses:
  204
  400
  401
  403
  404
  409
  422
  500
```

Generated artifacts must be updated through tools only:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd run check:api
```

If duplicate exchange still maps to `422` for now, keep OpenAPI aligned with actual implementation and add `409` only after error mapping cleanup.

## 18. Dependent / Follow-up Slices

```text
SL-AGR-EXCH-002 вЂ” Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 вЂ” Agreement Exchange List Page / Read List`nSL-AGR-EXCH-004 вЂ” Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 вЂ” Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 вЂ” Final Refuse Agreement Exchange
```

## 19. Implementation Checklist

```text
[ ] add EmployeeStartAgreementExchangeDto
[ ] add EmployeeStartAgreementExchangeDtoValidator
[ ] add EmployeeStartAgreementExchangeCommand returning UnitResult<IReadOnlyList<Error>>
[ ] do not add per-command status enum
[ ] add EmployeeStartAgreementExchangeHandler
[ ] add agreement exchange repository abstraction
[ ] add agreement exchange EF repository
[ ] add L1DbContext DbSet/mapping if missing
[ ] add AgreementProposalExchange.ClientAccountId
[ ] populate ClientAccountId from approved request owner in StartByEmployee
[ ] add ClientAccountId EF mapping/index
[ ] enforce unique exchange per request
[ ] add separate EmployeeAgreementExchangeController
[ ] require Employee role
[ ] require CSRF token
[ ] validate documentRef/comment
[ ] create AgreementDocumentRef
[ ] create optional ProposalComment
[ ] call AgreementProposalExchange.StartByEmployee
[ ] persist exchange aggregate
[ ] return 204 No Content on success
[ ] map failures through existing Error/ProblemDetails mapping
[ ] add domain tests if missing
[ ] add integration tests for auth/CSRF/validation/lifecycle/success/duplicate
[ ] regenerate OpenAPI/types
```

## 20. Guardrail Summary

```text
Start initial exchange is a server command slice.
This is not empty exchange start.
It starts exchange with first Employee proposal version.
ApproveReview does not create AgreementProposalExchange.
Request must already be Approved.
Document reference is required.
Comment is optional.
Exchange stores ClientAccountId from the approved request owner.
ClientAccountId is required for future client-side exchange ownership guards.
Employee ownership id is not used as first-pass access guard.
Proposal authors are stored per proposal version.
Success response is 204 No Content.
Use separate EmployeeAgreementExchangeController.
Do not add per-command status enum.
Do not use AgreementExchangeStatus as command execution result.
Domain/application Error codes drive HTTP failure mapping.
If error mapping is weak, improve it separately.
Do not upload binary files in this slice.
Do not implement client accept/counter-proposal/final refusal in this slice.
Do not change employee auth in this slice.
Do not mix this into ApproveReview handler.
```
