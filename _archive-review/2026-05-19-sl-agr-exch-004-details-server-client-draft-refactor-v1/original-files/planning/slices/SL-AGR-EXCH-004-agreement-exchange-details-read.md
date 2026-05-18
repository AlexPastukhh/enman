# SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details

Status: implementation-ready server draft after `SL-AGR-EXCH-003` list direction
Package: `[L2] Agreement Proposal Exchange`
Slice type: shared backend/API read details slice
Primary purpose: Client or Employee reads one agreement exchange with proposal history

Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`
* agreement exchange persistence
* `AgreementProposalExchange.ClientAccountId`
* auth/session

## 1. Numbering / docs note

```text
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

If GitHub still has old `SL-AGR-EXCH-004` as Accept and `005` as Final Refuse, docs should be renamed/synchronized before committing.

---

## 2. Slice Overview

Target behavior:

```text
Client or Employee opens one Agreement Exchange details page.

System reads current session role and account id.

System loads agreement exchange by exchangeId.

Client can read only exchanges where:
  AgreementProposalExchange.ClientAccountId == current client account id.

Employee can read according to employee visibility policy.

First pass:
  any active Employee can see/service agreement exchanges.

System returns:
- exchange status;
- request summary;
- active proposal;
- full proposal version history;
- document references;
- current actor side.

No lifecycle transition happens here.
No proposal is sent here.
No accept/refuse action happens here.
```

This is a shared read/details endpoint for both roles.

---

## 3. Scope

```text
- add shared agreement exchange details endpoint;
- authorize Client and Employee roles;
- resolve current role and account id from session;
- for Client, filter by exchange.ClientAccountId;
- for Employee, validate active Employee if current infrastructure supports it;
- first pass Employee visibility: any active Employee can see/service;
- use query handler + read repository / Dapper projection;
- return exchange details DTO;
- return request summary context;
- return active proposal;
- return full proposal version history;
- return document references only, not file bytes;
- return currentActorSide for UI derivation;
- do not mutate exchange/request/proposals;
- add integration tests for Client/Employee access and projection.
```

Endpoint:

```http
GET /api/agreement-exchanges/{exchangeId}
```

Auth:

```csharp
[Authorize(Roles = "Client,Employee")]
```

---

## 4. Out of Scope

| Out of scope                            | Owner                          |
| --------------------------------------- | ------------------------------ |
| Agreement exchange list                 | `SL-AGR-EXCH-003`              |
| Start exchange with initial proposal    | `SL-AGR-EXCH-001`              |
| Send counter-proposal                   | `SL-AGR-EXCH-002`              |
| Accept active proposal                  | `SL-AGR-EXCH-005`              |
| Final refusal                           | `SL-AGR-EXCH-006`              |
| File download / binary document serving | future document/file slice     |
| Document upload/storage                 | future document/storage slice  |
| Complex employee assignment visibility  | future visibility slice        |
| Available actions DTO                   | optional future enhancement    |
| AgreementExchangeActor abstraction      | not used first pass            |
| Application command service             | not needed for this read slice |
| UI command buttons implementation       | future client command sidecars |

---

## 5. Visual Scenario Flow

```text
User opens Agreement Exchange details page
        ↓
System reads current role and current account id
        ↓
System loads exchange by exchangeId
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ Client session               │ Employee session             │
 ▼                              ▼
Verify exchange.ClientAccountId Verify active Employee can view
equals current account id        serviceable exchanges
        ↓                              ↓
Return exchange details:
  status, request summary, active proposal, proposal history
        ↓
UI shows exchange state and possible buttons by role/status
```

Scenario flow table:

| Step | Actor/system       | Behavior                                                                        | Status |
| ---- | ------------------ | ------------------------------------------------------------------------------- | ------ |
| F01  | Client or Employee | Opens one agreement exchange details page.                                      | target |
| F02  | System             | Determines current role and account id from session.                            | target |
| F03  | System             | Loads exchange by `exchangeId`.                                                 | target |
| F04  | System             | For Client, verifies `ClientAccountId == current account id`.                   | target |
| F05  | System             | For Employee, verifies active Employee and first-pass visibility.               | target |
| F06  | System             | Returns exchange status, request summary, active proposal and proposal history. | target |
| F07  | System             | Does not perform proposal send/accept/final-refuse.                             | target |
| F08  | UI                 | May show buttons from returned state, but server commands remain authoritative. | target |

---

## 6. Visual Implementation Flow

```text
[HTTP GET]
GET /api/agreement-exchanges/{exchangeId}
        ↓
[Auth boundary]
require authenticated Client or Employee session
        ↓
[Session context]
read current role and current account id from app cookie identity
        ↓
[Route binding]
exchangeId is positive long
        ↓
[Query handler]
branch by role:
  Client -> repository.GetDetailsForClientAsync(currentAccountId, exchangeId)
  Employee -> repository.GetDetailsForEmployeeAsync(currentAccountId, exchangeId)
        ↓
[Read repository / Dapper]
Client branch:
  query exchange details where ClientAccountId == currentAccountId

Employee branch:
  verify active Employee if supported
  query employee-visible exchange details
        ↓
[Read projection]
load exchange, request summary, active proposal and proposal versions
        ↓
[DTO mapping]
project shared AgreementExchangeDetailsResponseDto
        ↓
[Response]
200 OK with details
or 401 / 403 / 404 / 500 ProblemDetails
```

Implementation flow table:

| Step | Layer                      | Responsibility                                                               |
| ---- | -------------------------- | ---------------------------------------------------------------------------- |
| I01  | Route / Controller         | Exposes shared `GET /api/agreement-exchanges/{exchangeId}` endpoint.         |
| I02  | Auth boundary              | Allows authenticated `Client` and `Employee` sessions only.                  |
| I03  | Session context            | Reads current role and account id from claims.                               |
| I04  | Route binding              | Binds `exchangeId` as positive `long`; no body/query validator first pass.   |
| I05  | Query handler              | Branches by role and calls matching read repository method.                  |
| I06  | Read repository — Client   | Queries details where `ClientAccountId == currentClientAccountId`.           |
| I07  | Read repository — Employee | Verifies/uses employee visibility policy; no `ResponsibleEmployeeId` filter. |
| I08  | Dapper projection          | Loads exchange, request summary, active proposal and proposal history.       |
| I09  | DTO mapping                | Projects document references, not binary bytes.                              |
| I10  | API response               | Returns `200 OK`; missing/not visible returns `404`.                         |

Guardrail:

```text
Implementation flow must not call domain mutation methods.

Do not call:
- ClientSendOwnVersion
- EmployeeSendNewVersion
- ClientAcceptActiveProposal
- FinalRefuseProposal
- MarkAgreementExchangeFailed

Details read is projection-only.
```

---

## 7. API Contract

Endpoint:

```http
GET /api/agreement-exchanges/{exchangeId}
```

Route:

```text
exchangeId: long, positive
```

Request body:

```text
none
```

Query:

```text
none first pass
```

Response:

```csharp
public sealed record AgreementExchangeDetailsResponseDto(
    long ExchangeId,
    long RequestId,
    string ExchangeStatus,
    int ActiveProposalVersion,
    AgreementExchangeRequestSummaryDto Request,
    AgreementProposalDetailsDto ActiveProposal,
    IReadOnlyList<AgreementProposalDetailsDto> Proposals,
    string CurrentActorSide,
    DateTimeOffset CreatedAt,
    DateTimeOffset? LastActivityAt);
```

Request summary:

```csharp
public sealed record AgreementExchangeRequestSummaryDto(
    long RequestId,
    string RequestStatus,
    string? RequestDisplayName,
    string? ObjectAddress);
```

Proposal details:

```csharp
public sealed record AgreementProposalDetailsDto(
    long ProposalId,
    int Version,
    string Sender,
    long SenderId,
    string State,
    AgreementDocumentRefDto Document,
    string? Comment,
    DateTimeOffset CreatedAt);
```

Document reference:

```csharp
public sealed record AgreementDocumentRefDto(
    string StorageKey,
    string OriginalFileName,
    string ContentType,
    long SizeBytes);
```

Do **not** return `ClientAccountId` in the details response first pass. It is required for server filtering/protection, but UI should not need the raw owner id.

Optional later:

```csharp
IReadOnlyList<string> AvailableActions
```

First pass can omit this.

---

## 8. Error Responses

```text
401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Client/Employee, or Employee account is not active/valid

404 NotFound
  exchange does not exist or is not visible to current actor

500 ServerError
  unexpected server error
```

Route malformed behavior follows existing ASP.NET/project convention.

No FluentValidation validator is needed first pass because there is no body/query.

---

## 9. Read Model Behavior

Client branch:

```text
current role = Client
current account id = current ClientAccountId

Query where:
  exchange.Id == exchangeId
  exchange.ClientAccountId == currentClientAccountId

If no row:
  return 404
```

Employee branch:

```text
current role = Employee
current account id = current EmployeeId

First pass:
  verify Employee exists and is active if current infrastructure supports it.

Query employee-visible exchange by exchangeId.

Current visibility policy:
  any active Employee can see/service agreement exchanges.

If no row:
  return 404
```

Common projection:

```text
- exchange id
- request id
- exchange status
- active proposal version
- request summary
- active proposal
- all proposal versions ordered by version
- proposal sender per version
- document refs
- created at
- last activity at
```

`LastActivityAt` first pass can be derived as:

```text
max(proposals.CreatedAt) ?? exchange.CreatedAt
```

---

## 10. Backend Implementation Notes

Do not introduce `AgreementExchangeActor`.

Do not introduce application service for this read slice.

Recommended shape:

```csharp
public sealed record AgreementExchangeDetailsQuery(
    long ExchangeId,
    long CurrentAccountId,
    string CurrentRole)
    : IRequest<Result<AgreementExchangeDetailsResponseDto, IReadOnlyList<Error>>>;
```

Query handler:

```csharp
public sealed class AgreementExchangeDetailsQueryHandler
{
    private readonly IAgreementExchangeReadRepository _repository;

    public async Task<Result<AgreementExchangeDetailsResponseDto, IReadOnlyList<Error>>> Handle(
        AgreementExchangeDetailsQuery query,
        CancellationToken cancellationToken)
    {
        if (query.CurrentRole == "Client")
        {
            return await _repository.GetDetailsForClientAsync(
                query.CurrentAccountId,
                query.ExchangeId,
                cancellationToken);
        }

        if (query.CurrentRole == "Employee")
        {
            return await _repository.GetDetailsForEmployeeAsync(
                query.CurrentAccountId,
                query.ExchangeId,
                cancellationToken);
        }

        return Result.Failure(...);
    }
}
```

Read repository:

```csharp
public interface IAgreementExchangeReadRepository
{
    Task<AgreementExchangeDetailsResponseDto?> GetDetailsForClientAsync(
        long clientAccountId,
        long exchangeId,
        CancellationToken cancellationToken);

    Task<AgreementExchangeDetailsResponseDto?> GetDetailsForEmployeeAsync(
        long employeeId,
        long exchangeId,
        CancellationToken cancellationToken);
}
```

Controller direction:

```csharp
[ApiController]
[Route("api/agreement-exchanges")]
public sealed class AgreementExchangesController : ProjectController
{
    [Authorize(Roles = "Client,Employee")]
    [HttpGet("{exchangeId:long:min(1)}", Name = "GetAgreementExchangeDetails")]
    public async Task<IActionResult> GetDetails(
        long exchangeId,
        CancellationToken cancellationToken)
    {
        // resolve current role + account id
        // create AgreementExchangeDetailsQuery
        // query handler returns DTO or failure
        // success -> 200
        // not found/not visible -> 404
        // other failures -> ProblemDetails
    }
}
```

Read implementation can use Dapper/read projection. Do not load the aggregate just to shape DTO.

---

## 11. Repository / Query Notes

The repository owns projection and role-specific filtering.

Client query:

```text
WHERE e.Id = @exchangeId
  AND e.ClientAccountId = @clientAccountId
```

Employee query first pass:

```text
-- verify active Employee if implemented as part of query or helper check
WHERE e.Id = @exchangeId
```

No `ResponsibleEmployeeId` filter.

Projection should load:

```text
AgreementProposalExchange
request summary fields
active proposal
all proposal versions ordered by Version
document reference fields
```

For Dapper implementation, either:

```text
- one multi-result query:
  1) exchange/request row
  2) proposals rows
```

or:

```text
- one joined query and in-memory grouping
```

Preferred first pass: multi-result query if project already uses Dapper and it keeps mapping simple.

---

## 12. Security / Protection

Important:

```text
UI button visibility is not authorization.
```

Details endpoint protects reads:

```text
Client:
  persisted ClientAccountId filter.

Employee:
  active Employee validation + first-pass employee visibility.
```

Command endpoints still must protect writes:

```text
- actor role;
- ClientAccountId ownership for client actions;
- active Employee for employee actions;
- whose turn;
- active proposal sender;
- exchange lifecycle.
```

Do not put these into FluentValidation:

```text
- ownership
- visibility
- active proposal sender
- exchange status
- actor turn
- lifecycle
```

---

## 13. Behavior Coverage

| Behavior item                                       | How slice covers it                | Status                |
| --------------------------------------------------- | ---------------------------------- | --------------------- |
| Client can open own exchange details                | query filters by `ClientAccountId` | target                |
| Client cannot open another client exchange          | same filter returns 404            | target                |
| Employee can open visible exchange details          | employee branch query              | target                |
| Any active Employee can service exchange first pass | no responsible employee guard      | target                |
| Details show exchange status                        | details DTO                        | target                |
| Details show active proposal                        | details DTO                        | target                |
| Details show proposal history                       | details DTO                        | target                |
| Details show document refs, not bytes               | document ref DTO                   | target                |
| Details does not mutate exchange                    | read-only endpoint                 | target                |
| Employee sender history is preserved                | proposal-level sender fields       | target/current domain |

---

## 14. Test / Verification Plan

Primary verification: API/read integration tests.

No unit tests by default unless reusable helper logic with branching is introduced.

API boundary:

```text
- unauthenticated details -> 401;
- authenticated non Client/Employee -> 403 if such test setup exists;
- missing exchange -> 404.
```

Client access:

```text
- Client gets own exchange details -> 200;
- Client cannot get another client exchange details -> 404;
- response includes only expected exchange data.
```

Employee access:

```text
- active Employee gets exchange details -> 200;
- inactive Employee cannot get details -> 403 or existing project failure mapping;
- no ResponsibleEmployeeId filter is required.
```

Projection:

```text
- response includes exchangeStatus;
- response includes request summary;
- response includes activeProposal;
- response includes all proposal versions ordered by Version;
- response includes proposal sender/senderId;
- response includes document refs;
- response does not include binary bytes.
```

No-mutation safety:

```text
- GET details does not change exchange status;
- GET details does not create proposals;
- GET details does not supersede/accept/refuse proposals.
```

Generated artifacts:

```text
- run OpenAPI generation;
- run API type generation;
- stage generated artifacts;
- run check:api.
```

---

## 15. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
GET /api/agreement-exchanges/{exchangeId}

200 AgreementExchangeDetailsResponseDto
401
403
404
500
```

Generation workflow:

```powershell
npm.cmd run generate:openapi
npm.cmd run generate:api-types

git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm.cmd run check:api
```

Generated artifacts must come from repo commands, not manual edits.

---

## 16. Implementation Checklist

```text
[ ] ensure AgreementProposalExchange has ClientAccountId
[ ] do not add ResponsibleEmployeeId as authorization guard
[ ] add AgreementExchangeDetailsResponseDto
[ ] add AgreementExchangeRequestSummaryDto
[ ] add AgreementProposalDetailsDto
[ ] add AgreementDocumentRefDto or reuse existing compatible DTO
[ ] add shared details endpoint
[ ] add AgreementExchangeDetailsQuery
[ ] add AgreementExchangeDetailsQueryHandler
[ ] add IAgreementExchangeReadRepository
[ ] add GetDetailsForClientAsync
[ ] add GetDetailsForEmployeeAsync
[ ] add client branch query by ClientAccountId
[ ] add employee branch query by first-pass visibility policy
[ ] include request summary
[ ] include active proposal
[ ] include proposal history ordered by version
[ ] include document refs only, not file bytes
[ ] return 200 with details
[ ] return 404 for missing/not-visible exchange
[ ] add integration tests for client ownership filtering
[ ] add integration tests for employee visibility
[ ] add projection integration tests
[ ] add no-mutation smoke if cheap
[ ] regenerate OpenAPI/types
[ ] do not send proposal here
[ ] do not accept proposal here
[ ] do not final-refuse here
```

---

## 17. Guardrail Summary

```text
This is a read/details slice.

Use shared endpoint first pass.

Use query handler + read repository / Dapper projection.

Do not introduce application service for this read slice.

Do not introduce AgreementExchangeActor abstraction.

Use current role/current account id from session.

Client access must filter by AgreementProposalExchange.ClientAccountId.

Employee access first pass allows any active Employee.

Do not add ResponsibleEmployeeId as exchange authorization guard.

Employee sender identity is tracked per proposal version.

Details includes full proposal history.

List remains summary-only.

Document bytes are not returned here.

Request details and exchange reads remain separate.

UI buttons are not security.

Server commands still enforce domain lifecycle and participant rules.
```

## Готовность к имплементации

Draft готов к имплементации при трёх подтверждениях:

```text
1. Route family confirmed:
   GET /api/agreement-exchanges/{exchangeId}

2. AgreementProposalExchange.ClientAccountId exists or is added before/details with migration/test DB update.

3. Persistence has enough data:
   exchange, request summary, active proposal, proposal versions, document refs.
```

Единственный docs-риск: numbering conflict с уже существующими `SL-AGR-EXCH-004/005` в GitHub. Это надо синхронизировать в planning, чтобы details не конфликтовал с accept/final-refuse.
