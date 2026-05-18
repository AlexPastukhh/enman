# SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version

Status: draft / server implementation-ready after `AgreementProposalExchange.ClientAccountId` cleanup
Package: `[L2] Agreement Proposal Exchange`
Slice type: shared server command endpoint + shared DTO + role-based service branch
Depends on: `SL-AGR-EXCH-001`, Client/Employee auth/session, CSRF, agreement exchange persistence
Current implementation status: domain methods exist; shared endpoint/application service are planned.

## 1. Slice Overview

Target behavior:

```text id="4qskdv"
Client or Employee opens agreement exchange details.

Exchange already exists.

Current user sends own agreement proposal document version.

System validates that this side can respond now.

System supersedes previous active proposal.

System creates next proposal version.

System switches exchange waiting state to the other side.

Command returns 204 No Content.
```

This slice implements **counter-proposal exchange**, not initial exchange creation.

Initial exchange creation remains in:

```text id="dlmvav"
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
```

## 2. Scope

Implemented scope:

```text id="r5bd3d"
- one shared command endpoint for Client and Employee;
- route is request-scoped: POST /api/requests/{requestId}/agreement-exchange/proposals;
- role resolved from current app cookie session;
- one shared request DTO;
- controller branches only by current role;
- explicit service method for Client branch;
- explicit service method for Employee branch;
- Client branch calls AgreementProposalExchange.ClientSendOwnVersion(...);
- Employee branch calls AgreementProposalExchange.EmployeeSendNewVersion(...);
- Client actions protected by exchange.ClientAccountId;
- Employee actions allowed for any active Employee first pass;
- previous active proposal becomes SupersededByCounterProposal;
- next proposal version is created;
- command returns 204 No Content;
- failures use existing Error / ProblemDetails mapping;
- no per-command status enum.
```

## 3. Out of Scope

| Out-of-scope item                           | Owner                            |
| ------------------------------------------- | -------------------------------- |
| Start exchange with first employee proposal | `SL-AGR-EXCH-001`                |
| Empty exchange start                        | not supported by current domain  |
| Exchange list page                          | `SL-AGR-EXCH-003`                |
| Exchange details/full history read          | separate details slice           |
| Client accept active proposal               | future accept slice              |
| Final refusal                               | future refusal slice             |
| Binary file upload/storage                  | future file/document slice       |
| Separate Client/Employee endpoints          | future only if behavior diverges |
| `ResponsibleEmployeeId` as guard            | explicitly not this slice        |
| Per-command status enums                    | explicitly not used              |

Important boundary:

```text id="vat7tk"
This endpoint does not create AgreementProposalExchange.

It only adds a new proposal version to an existing exchange.
```

## 4. Visual Scenario Flow

```text id="wcsj4z"
User opens agreement exchange details
        ↓
Exchange shows active proposal version
        ↓
User chooses Send own version
        ↓
User provides agreement document reference
        ↓
User optionally provides comment
        ↓
User submits
        ↓
 ┌────────────────────────────────┬────────────────────────────────┐
 │ accepted                       │ not accepted                   │
 ▼                                ▼
Previous proposal is superseded    User sees validation/error
Next version is created            feedback and can correct input
        ↓
Exchange waits for the other side
        ↓
No accept/final refusal happens here
```

Scenario flow intentionally does not mention controller, handler, repository, EF, CSRF token or OpenAPI.

## 5. Scenario Slice Flow

| Step | User/system        | Behavior                                             | Status                          |
| ---- | ------------------ | ---------------------------------------------------- | ------------------------------- |
| F01  | Client or Employee | Opens exchange details.                              | existing/future read side       |
| F02  | System             | Shows active proposal version and sender.            | existing/future read side       |
| F03  | User               | Chooses to send own version.                         | target                          |
| F04  | User               | Provides agreement document reference.               | target                          |
| F05  | User               | Optionally provides proposal comment.                | target                          |
| F06  | System             | Resolves current role and account id from session.   | target                          |
| F07  | System             | Routes command to Client or Employee service method. | target                          |
| F08  | Domain             | Validates participant/lifecycle/turn rules.          | target/current domain + cleanup |
| F09  | Domain             | Supersedes previous active proposal.                 | target/current domain           |
| F10  | Domain             | Creates next proposal version.                       | target/current domain           |
| F11  | Domain             | Switches exchange waiting state to other side.       | target/current domain           |
| F12  | System             | Returns `204 No Content`.                            | target                          |

## 6. API Contract

Endpoint:

```http id="e701r9"
POST /api/requests/{requestId}/agreement-exchange/proposals
```

Auth:

```csharp id="83u16k"
[Authorize(Roles = "Client,Employee")]
[RequireAntiforgeryToken]
```

Request body:

```json id="ikzgxh"
{
  "document": {
    "storageKey": "agreements/request-1-v2.pdf",
    "originalFileName": "request-1-v2.pdf",
    "contentType": "application/pdf",
    "sizeBytes": 4096
  },
  "comment": "Please review updated agreement."
}
```

DTO direction:

```csharp id="duuh0i"
public sealed record SendAgreementProposalVersionDto(
    AgreementDocumentRefDto? Document,
    string? Comment);
```

Success:

```text id="bjwnst"
204 No Content
```

Failures:

```text id="6xv2f8"
Use existing Error / ProblemDetails mapping.

Expected categories:
- 400 invalid/missing CSRF or malformed body;
- 401 unauthenticated;
- 403 authenticated but role/action not allowed;
- 404 exchange/request not found if mapper supports it;
- 422 validation/lifecycle errors;
- 500 unexpected errors.
```

Route rationale:

```text id="34e5eo"
Use request-scoped route because current lookup is by requestId.

Do not use /api/agreement-exchanges/{requestId}/proposals because that looks like {exchangeId}.
```

## 7. Questions / Decisions

| ID                     | Status   | Question                                              | Decision / current direction                                                                | Impact                                             |
| ---------------------- | -------- | ----------------------------------------------------- | ------------------------------------------------------------------------------------------- | -------------------------------------------------- |
| `SL-AGR-EXCH-002-Q001` | accepted | One endpoint or separate Client/Employee endpoints?   | One shared endpoint first pass.                                                             | Shared frontend/API path.                          |
| `SL-AGR-EXCH-002-Q002` | accepted | One slice or two?                                     | One slice with Client and Employee branches.                                                | Avoids duplicated mirrored slices.                 |
| `SL-AGR-EXCH-002-Q003` | accepted | Does this endpoint create exchange?                   | No. Exchange must already exist.                                                            | Initial creation remains in `SL-AGR-EXCH-001`.     |
| `SL-AGR-EXCH-002-Q004` | accepted | How is current side determined?                       | From current app session role and account id.                                               | Controller/service branch by role.                 |
| `SL-AGR-EXCH-002-Q005` | accepted | Do we introduce `AgreementExchangeActor` abstraction? | No first pass. Use current role/current account id and explicit service methods.            | Keeps implementation simple.                       |
| `SL-AGR-EXCH-002-Q006` | accepted | Client ownership rule?                                | Domain checks `client.Id == exchange.ClientAccountId`.                                      | Prevents client acting on чужой exchange.          |
| `SL-AGR-EXCH-002-Q007` | accepted | Employee ownership rule?                              | No `ResponsibleEmployeeId` guard first pass. Any active Employee can service exchange.      | Different employees can continue same exchange.    |
| `SL-AGR-EXCH-002-Q008` | accepted | Where are employee identities stored?                 | Per proposal version via `AgreementProposal.Author(Sender=Employee, SenderId=employee.Id)`. | Audit/history preserved.                           |
| `SL-AGR-EXCH-002-Q009` | accepted | Client branch domain method?                          | `AgreementProposalExchange.ClientSendOwnVersion(...)`.                                      | Creates client counter-proposal.                   |
| `SL-AGR-EXCH-002-Q010` | accepted | Employee branch domain method?                        | `AgreementProposalExchange.EmployeeSendNewVersion(...)`.                                    | Creates employee counter-proposal.                 |
| `SL-AGR-EXCH-002-Q011` | accepted | Success response?                                     | `204 No Content`.                                                                           | Read state comes from exchange details/list.       |
| `SL-AGR-EXCH-002-Q012` | accepted | Return proposal DTO?                                  | No. This is command endpoint.                                                               | Avoids command/read mixing.                        |
| `SL-AGR-EXCH-002-Q013` | accepted | Document input?                                       | Store document reference only.                                                              | No binary upload.                                  |
| `SL-AGR-EXCH-002-Q014` | accepted | Comment behavior?                                     | Optional; null/blank means no comment.                                                      | Create `ProposalComment` only for meaningful text. |
| `SL-AGR-EXCH-002-Q015` | accepted | Command status enum?                                  | Do not add. Use `UnitResult<IReadOnlyList<Error>>`.                                         | Error mapping remains centralized.                 |
| `SL-AGR-EXCH-002-Q016` | accepted | Wrong turn behavior?                                  | Domain lifecycle errors.                                                                    | No duplicate checks in UI only.                    |
| `SL-AGR-EXCH-002-Q017` | accepted | Accepted/finally refused exchange?                    | Cannot receive new version. Domain rejects.                                                 | Lifecycle invariant.                               |
| `SL-AGR-EXCH-002-Q018` | accepted | FluentValidation responsibility?                      | DTO shape only.                                                                             | No ownership/lifecycle in validators.              |
| `SL-AGR-EXCH-002-Q019` | accepted | Split later?                                          | Only if client/employee behavior diverges.                                                  | First pass stays shared.                           |
| `SL-AGR-EXCH-002-Q020` | accepted | Route shape?                                          | `POST /api/requests/{requestId}/agreement-exchange/proposals`.                              | Avoids confusing requestId with exchangeId.        |

## 8. Domain Requirements

`AgreementProposalExchange` must contain:

```csharp id="486tsg"
public long RequestId { get; private set; }

public long ClientAccountId { get; private set; }
```

Client-side domain methods must protect participant ownership:

```csharp id="vaxsr1"
if (client.Id != ClientAccountId)
{
    return UnitResult.Failure<IReadOnlyList<Error>>(
        [Errors.L1Domain.ClientCannotActOnThisAgreementExchange]);
}
```

Applies to:

```text id="10g3cs"
ClientSendOwnVersion(...)
ClientAcceptActiveProposal(...)
future client-side refusal, if added
```

Employee-side domain methods must **not** check fixed responsible employee:

```text id="mx9slr"
Do not add:
employee.Id == ResponsibleEmployeeId
```

Employee branch checks:

```text id="8916ik"
- employee exists;
- employee.Id > 0;
- employee.EnsureCanSendAgreementProposal();
- exchange status allows employee response;
- active proposal author is Client.
```

Proposal authors stay per version:

```text id="dl13bv"
AgreementProposal.Author.Sender
AgreementProposal.Author.SenderId
```

## 9. Domain Behavior

Client branch:

```text id="8cdp7u"
AgreementProposalExchange.ClientSendOwnVersion(
    document,
    comment,
    client,
    createdAt)
```

Expected behavior:

```text id="eo2nmt"
- client must be exchange.ClientAccountId;
- exchange status must be AwaitingClientConfirmation;
- active proposal author must be Employee;
- document is required;
- active proposal is marked SupersededByCounterProposal;
- next proposal version is created by Client;
- ActiveProposalVersion becomes next version;
- exchange status becomes AwaitingEmployeeResponse.
```

Employee branch:

```text id="hfm6th"
AgreementProposalExchange.EmployeeSendNewVersion(
    document,
    comment,
    employee,
    createdAt)
```

Expected behavior:

```text id="2ih5r6"
- employee must be active/allowed to send agreement proposal;
- exchange status must be AwaitingEmployeeResponse;
- active proposal author must be Client;
- document is required;
- active proposal is marked SupersededByCounterProposal;
- next proposal version is created by Employee;
- ActiveProposalVersion becomes next version;
- exchange status becomes AwaitingClientConfirmation.
```

Common behavior:

```text id="5vdxa6"
- does not create new exchange;
- does not mutate request status;
- does not accept proposal;
- does not finally refuse exchange;
- preserves proposal version history.
```

## 10. Application Result Model

Do not add per-command status enum.

Preferred command/service result:

```csharp id="qzbxg0"
UnitResult<IReadOnlyList<Error>>
```

Controller mapping:

```text id="77w4ds"
Success -> 204 No Content
Failure -> existing ProblemDetails/error mapper
```

Important:

```text id="r1j9l6"
AgreementExchangeStatus is persisted domain state.

It is not command execution result.

If Error mapping is weak, improve Error/ProblemDetails mapping separately.
```

## 11. Visual Implementation Flow

```text id="r9jhqy"
[HTTP]
POST /api/requests/{requestId}/agreement-exchange/proposals
        ↓
[Auth]
Client or Employee app cookie required
        ↓
[CSRF]
valid antiforgery token required
        ↓
[DTO validation]
document required / comment max length
        ↓
[Controller]
read current role and account id from session
        ↓
[Branch]
Client role   -> service.SendClientProposalVersionAsync(...)
Employee role -> service.SendEmployeeProposalVersionAsync(...)
        ↓
[Service]
load account
load exchange by requestId
create AgreementDocumentRef
create optional ProposalComment
call domain method
        ↓
[Persistence]
SaveChanges
        ↓
[Response]
204 No Content
```

## 12. Backend Implementation Notes

Service interface direction:

```csharp id="6mj95s"
public interface IAgreementExchangeApplicationService
{
    Task<UnitResult<IReadOnlyList<Error>>> SendClientProposalVersionAsync(
        long clientAccountId,
        long requestId,
        AgreementDocumentRefInput document,
        string? comment,
        CancellationToken cancellationToken);

    Task<UnitResult<IReadOnlyList<Error>>> SendEmployeeProposalVersionAsync(
        long employeeId,
        long requestId,
        AgreementDocumentRefInput document,
        string? comment,
        CancellationToken cancellationToken);
}
```

Controller flow:

```text id="dqhno4"
1. Validate DTO.
2. Resolve current role.
3. Resolve current account id from NameIdentifier.
4. If role = Client:
   call SendClientProposalVersionAsync(currentAccountId, requestId, dto.Document, dto.Comment).
5. If role = Employee:
   call SendEmployeeProposalVersionAsync(currentAccountId, requestId, dto.Document, dto.Comment).
6. Success -> 204.
7. Failure -> existing ProblemDetails/error mapper.
```

Client service method:

```text id="b568iv"
1. Load ClientAccount by clientAccountId.
2. If not found -> existing auth/current client error.
3. Load AgreementProposalExchange by requestId.
4. If not found -> existing exchange not found error.
5. Create AgreementDocumentRef.
6. Create optional ProposalComment.
7. Call exchange.ClientSendOwnVersion(document, comment, client, now).
8. Domain checks client.Id == exchange.ClientAccountId.
9. SaveChangesAsync.
10. Return success.
```

Employee service method:

```text id="uk4efq"
1. Load Employee by employeeId.
2. If not found -> existing auth/current employee error.
3. Load AgreementProposalExchange by requestId.
4. If not found -> existing exchange not found error.
5. Create AgreementDocumentRef.
6. Create optional ProposalComment.
7. Call exchange.EmployeeSendNewVersion(document, comment, employee, now).
8. Domain checks employee capability and exchange lifecycle.
9. SaveChangesAsync.
10. Return success.
```

No partial mutation:

```text id="1yh0vh"
If document/comment validation or domain lifecycle validation fails,
no new proposal version should be persisted and active version should not change.
```

## 13. DTO Validation

Shared validator direction:

```csharp id="6h9t3k"
public sealed class SendAgreementProposalVersionDtoValidator
    : AbstractValidator<SendAgreementProposalVersionDto>
{
    public SendAgreementProposalVersionDtoValidator()
    {
        RuleFor(x => x.Document)
            .NotNull();

        RuleFor(x => x.Document!.StorageKey)
            .NotEmpty();

        RuleFor(x => x.Document!.OriginalFileName)
            .NotEmpty();

        RuleFor(x => x.Document!.ContentType)
            .NotEmpty();

        RuleFor(x => x.Document!.SizeBytes)
            .GreaterThan(0);

        RuleFor(x => x.Comment)
            .MaximumLength(ProposalComment.MaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Comment));
    }
}
```

Validator must not check:

```text id="hcs5v1"
- ClientAccountId ownership;
- current turn;
- exchange status;
- active proposal author;
- employee visibility;
- lifecycle transitions.
```

## 14. Server API Notes

Controller direction:

```csharp id="gqih14"
[ApiController]
[Route("api/requests/{requestId:long:min(1)}/agreement-exchange")]
public sealed class AgreementExchangeProposalsController : ProjectController
{
    [Authorize(Roles = "Client,Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("proposals", Name = "SendAgreementProposalVersion")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendProposal(
        long requestId,
        [FromBody] SendAgreementProposalVersionDto dto,
        CancellationToken cancellationToken)
}
```

Controller should not contain lifecycle logic.

Allowed controller branch:

```text id="y6pcjp"
role -> service method
```

Not allowed:

```text id="05x5mf"
role -> manually inspect exchange status / active author / ownership in controller
```

## 15. Repository / Persistence Notes

Required repository usage:

```csharp id="zm8ykk"
Task<AgreementProposalExchange?> GetByRequestIdAsync(
    long requestId,
    CancellationToken cancellationToken);
```

The aggregate must be loaded with proposals so domain methods can:

```text id="76ocd8"
- find active proposal;
- mark active proposal superseded;
- calculate next version;
- append new proposal;
- update active version/status.
```

Persistence must preserve:

```text id="k81w4f"
- existing proposal history;
- superseded proposal state;
- new proposal version;
- active proposal version;
- exchange status;
- document ref;
- optional comment;
- author sender/id;
- createdAt.
```

## 16. Behavior Coverage

| Behavior item                               | How slice covers it                      | Status         |
| ------------------------------------------- | ---------------------------------------- | -------------- |
| Shared endpoint accepts Client and Employee | `[Authorize(Roles = "Client,Employee")]` | target         |
| Client sends own version                    | service calls `ClientSendOwnVersion`     | target         |
| Employee sends new version                  | service calls `EmployeeSendNewVersion`   | target         |
| Client cannot act on чужой exchange         | domain checks `ClientAccountId`          | target         |
| Any active Employee can service exchange    | no `ResponsibleEmployeeId` guard         | target         |
| Proposal sender is stored per version       | `AgreementProposal.Author`               | current domain |
| Previous proposal is superseded             | domain transition                        | current/target |
| Next version is created                     | domain transition                        | current/target |
| Turn switches after Client send             | status -> `AwaitingEmployeeResponse`     | current/target |
| Turn switches after Employee send           | status -> `AwaitingClientConfirmation`   | current/target |
| Wrong turn rejected                         | domain lifecycle errors                  | current/target |
| No response DTO                             | command returns 204                      | target         |

## 17. Test / Verification Plan

| Test / check                                         | Verifies                      | Layer              | Status |
| ---------------------------------------------------- | ----------------------------- | ------------------ | ------ |
| unauthenticated send -> 401                          | auth boundary                 | integration        | target |
| missing CSRF -> 400                                  | CSRF boundary                 | integration        | target |
| unsupported role -> 403                              | role boundary                 | integration        | target |
| missing document -> 422                              | DTO validation                | integration        | target |
| invalid document fields -> 422                       | DTO/domain validation         | integration/domain | target |
| too long comment -> 422                              | DTO validation                | integration        | target |
| exchange not found -> mapped ProblemDetails          | lookup                        | integration        | target |
| client sends for own exchange -> 204                 | client success                | integration/domain | target |
| client cannot send for another client exchange       | `ClientAccountId` guard       | integration/domain | target |
| client send creates next version                     | versioning                    | integration/domain | target |
| client send supersedes employee proposal             | proposal state                | integration/domain | target |
| client send sets status AwaitingEmployeeResponse     | exchange status               | integration/domain | target |
| employee sends after client version -> 204           | employee success              | integration/domain | target |
| different active employee can send employee version  | no responsible employee guard | integration/domain | target |
| employee send creates next version                   | versioning                    | integration/domain | target |
| employee send supersedes client proposal             | proposal state                | integration/domain | target |
| employee send sets status AwaitingClientConfirmation | exchange status               | integration/domain | target |
| client cannot send twice in row                      | wrong turn                    | integration/domain | target |
| employee cannot send when awaiting client            | wrong turn                    | integration/domain | target |
| accepted exchange cannot receive new version         | lifecycle                     | integration/domain | target |
| finally refused exchange cannot receive new version  | lifecycle                     | integration/domain | target |

## 18. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text id="k0joi2"
POST /api/requests/{requestId}/agreement-exchange/proposals
request body: SendAgreementProposalVersionDto
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

```powershell id="wpd560"
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd run check:api
```

## 19. Dependent / Follow-up Slices

```text id="29ya33"
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

## 20. Implementation Checklist

```text id="cnkpnk"
[ ] ensure AgreementProposalExchange has ClientAccountId
[ ] ensure client domain methods check client.Id == ClientAccountId
[ ] do not add ResponsibleEmployeeId guard
[ ] add shared SendAgreementProposalVersionDto
[ ] add shared SendAgreementProposalVersionDtoValidator
[ ] add shared endpoint
[ ] add AgreementExchangeApplicationService
[ ] add SendClientProposalVersionAsync
[ ] add SendEmployeeProposalVersionAsync
[ ] controller resolves current role/current account id
[ ] controller branches only to service method
[ ] create AgreementDocumentRef
[ ] create optional ProposalComment
[ ] load exchange with proposals
[ ] client branch calls ClientSendOwnVersion
[ ] employee branch calls EmployeeSendNewVersion
[ ] persist aggregate changes
[ ] return 204 No Content
[ ] use existing Error/ProblemDetails mapping
[ ] do not add per-command status enum
[ ] add integration tests for both branches
[ ] regenerate OpenAPI/types
```

## 21. Guardrail Summary

```text id="ecyz1k"
This is one shared command endpoint first pass.

Route is POST /api/requests/{requestId}/agreement-exchange/proposals.

Do not use /api/agreement-exchanges/{requestId}/proposals because it suggests exchangeId.

Do not split Client/Employee endpoints unless behavior diverges later.

Do not introduce AgreementExchangeActor abstraction.

Use current role/current account id from session.

Controller may branch by role only to call service method.

Controller must not contain lifecycle logic.

Application service orchestrates loading/value object creation/domain calls.

Domain owns participant/lifecycle/turn invariants.

AgreementProposalExchange must store ClientAccountId.

Client actions must check client.Id == ClientAccountId.

Employee is not fixed exchange-level owner.

Any active Employee can service exchange first pass.

Proposal sender identity is tracked per proposal version.

No binary upload.

No response DTO.

No per-command status enum.

Failures go through existing Error/ProblemDetails mapping.
```
