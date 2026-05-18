# SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List

Status: draft
Package: `[L2] Agreement Proposal Exchange`
Slice type: shared read page + shared read endpoint + shared client entity
Depends on: `SL-AGR-EXCH-001`, auth/session, agreement exchange persistence, `AgreementProposalExchange.ClientAccountId` domain/persistence change
Current implementation status: agreement exchange domain exists; list read model/page/API are planned.

## 1. Slice Overview

Target behavior:

```text
Client or Employee opens Agreement Exchanges page.

System shows list of agreement exchanges available to current session.

Client sees only exchanges where AgreementProposalExchange.ClientAccountId equals current client account id.

Employee sees exchanges according to employee visibility policy.

First pass: any active Employee can see/service agreement exchanges.

Each list row shows exchange summary:
- request id / request display info;
- exchange status;
- active proposal version;
- active proposal sender;
- last activity / created date.

User opens an exchange details page from the list.
```

Important:

```text
This is a read/list slice.

No agreement lifecycle transition happens here.

No proposal is sent here.

No accept/refuse action happens here.
```

## 2. Scope

Implemented scope:

```text
- shared list endpoint for Client and Employee;
- shared client API/query/model;
- shared list page/component where possible;
- current role and current account id derived from app cookie identity;
- Client branch filters by AgreementProposalExchange.ClientAccountId;
- Employee branch uses employee visibility policy;
- first pass Employee visibility: any active Employee can see/service;
- list returns summaries, not full proposal history;
- page routes can differ by shell, but list component is shared.
```

## 3. Out of Scope

| Out-of-scope item                        | Owner                      |
| ---------------------------------------- | -------------------------- |
| Start exchange with initial proposal     | `SL-AGR-EXCH-001`          |
| Send counter-proposal                    | `SL-AGR-EXCH-002`          |
| Exchange details / full proposal history | `SL-AGR-EXCH-004`          |
| Accept active proposal                   | future accept slice        |
| Final refusal                            | future refusal slice       |
| File download / binary document serving  | future document/file slice |
| Complex employee assignment visibility   | future visibility slice    |
| Changing exchange lifecycle              | domain command slices      |
| AgreementExchangeActor abstraction       | not used in this draft     |

## 4. Visual Scenario Flow

```text
User opens Agreement Exchanges page
        ↓
System reads current session role and account id
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ Client session               │ Employee session             │
 ▼                              ▼
Load only client-owned           Load employee-visible
agreement exchanges              agreement exchanges
        ↓                              ↓
Show exchange list with status / active version / request summary
        ↓
User selects exchange
        ↓
Navigate to exchange details
```

Scenario flow intentionally does not mention repository, EF, DTO validators or OpenAPI.

## 5. Scenario Slice Flow

| Step | Actor/system       | Behavior                                                                            | Status |
| ---- | ------------------ | ----------------------------------------------------------------------------------- | ------ |
| F01  | Client or Employee | Opens Agreement Exchanges page.                                                     | target |
| F02  | System             | Determines current role and current account id from session.                        | target |
| F03  | System             | For Client, loads only exchanges where `ClientAccountId == currentClientAccountId`. | target |
| F04  | System             | For Employee, loads exchanges visible to active employees.                          | target |
| F05  | System             | Shows list rows with exchange status and active proposal summary.                   | target |
| F06  | User               | Opens exchange details from a row.                                                  | target |
| F07  | System             | Does not perform proposal send/accept/refuse from list loading.                     | target |

## 6. API Contract

Recommended shared endpoint:

```http
GET /api/agreement-exchanges
```

Alternative L1-scoped endpoint:

```http
GET /api/l1/agreement-exchanges
```

Auth:

```csharp
[Authorize(Roles = "Client,Employee")]
```

Query filters first pass:

```text
status?: AgreementExchangeStatus
```

Paging can be added later:

```text
page?: number
pageSize?: number
```

Response direction:

```csharp
public sealed record AgreementExchangeListResponseDto(
    IReadOnlyList<AgreementExchangeListItemDto> Exchanges);

public sealed record AgreementExchangeListItemDto(
    long RequestId,
    long ExchangeId,
    string ExchangeStatus,
    int ActiveProposalVersion,
    string ActiveProposalSender,
    long ActiveProposalSenderId,
    string? RequestDisplayName,
    string? ObjectAddress,
    DateTimeOffset CreatedAt,
    DateTimeOffset? LastActivityAt);
```

Optional later:

```csharp
IReadOnlyList<string> AvailableActions
```

## 7. Questions / Decisions

| ID                     | Status   | Question                                              | Decision / current direction                                                                     | Impact                                                 |
| ---------------------- | -------- | ----------------------------------------------------- | ------------------------------------------------------------------------------------------------ | ------------------------------------------------------ |
| `SL-AGR-EXCH-003-Q001` | accepted | Separate Client/Employee list endpoints?              | No first pass. Use shared endpoint.                                                              | Shared UI/client model.                                |
| `SL-AGR-EXCH-003-Q002` | accepted | Separate Client/Employee frontend API wrappers?       | No first pass. Use `entities/agreement-exchange/api`.                                            | Avoid duplicate client code.                           |
| `SL-AGR-EXCH-003-Q003` | accepted | Does list include full proposal history?              | No. Summary only.                                                                                | Details endpoint owns history.                         |
| `SL-AGR-EXCH-003-Q004` | accepted | How does Client access work?                          | Filter by `AgreementProposalExchange.ClientAccountId == currentClientAccountId`.                 | Requires `ClientAccountId` in domain/persistence.      |
| `SL-AGR-EXCH-003-Q005` | accepted | How does Employee access work first pass?             | Any active Employee can see/service exchanges.                                                   | No `ResponsibleEmployeeId` guard.                      |
| `SL-AGR-EXCH-003-Q006` | accepted | Should exchange store responsible employee?           | No as authorization guard. Optional audit only.                                                  | Any active Employee can continue exchange.             |
| `SL-AGR-EXCH-003-Q007` | accepted | Should proposal sender info be shown?                 | Yes, active proposal sender and version should be in summary.                                    | Helps UI decide next action.                           |
| `SL-AGR-EXCH-003-Q008` | accepted | Should buttons be protected by UI only?               | No. UI shows buttons by role/status, server commands still enforce rules.                        | Security remains server/domain-side.                   |
| `SL-AGR-EXCH-003-Q009` | accepted | Should list endpoint return `availableActions`?       | Optional. First pass can derive from role/status.                                                | Avoid overbuilding.                                    |
| `SL-AGR-EXCH-003-Q010` | accepted | Request details vs exchange list/details?             | Separate read endpoints. UI composes pages.                                                      | Keeps DTOs smaller.                                    |
| `SL-AGR-EXCH-003-Q011` | accepted | Do we introduce `AgreementExchangeActor` abstraction? | No first pass. Use current role/current account id and explicit client/employee service methods. | Keeps implementation/direct documentation simpler.     |
| `SL-AGR-EXCH-003-Q012` | accepted | Where are employees documented?                       | Per proposal version through `AgreementProposal.Author(Sender=Employee, SenderId=employee.Id)`.  | Employee is not exchange-level owner.                  |
| `SL-AGR-EXCH-003-Q013` | accepted | What participant reference is stored on exchange?     | `ClientAccountId`.                                                                               | Client ownership can be enforced and queried directly. |

## 8. Domain / Persistence Requirement

Before or as part of agreement exchange read/command implementation:

```csharp
public sealed class AgreementProposalExchange : L1Entity
{
    public long RequestId { get; private set; }

    public long ClientAccountId { get; private set; }

    public AgreementExchangeStatus Status { get; private set; }

    public AgreementProposalVersion ActiveProposalVersion { get; private set; }

    private readonly List<AgreementProposal> _proposals = new();
}
```

Meaning:

```text
ClientAccountId is the client-side participant/ownership reference.

It is used for:
- client read filtering;
- client command protection;
- future client accept/refuse protection.
```

Do not add this as authorization guard:

```csharp
public long ResponsibleEmployeeId { get; private set; }
```

Reason:

```text
Any active Employee can see/service agreement exchanges in first pass.

Employees are tracked as senders of individual proposal versions, not as exchange-level owners.
```

Proposal sender tracking remains per proposal:

```text
AgreementProposal.Author.Sender
AgreementProposal.Author.SenderId
```

## 9. Read Model Behavior

Client branch:

```text
current session role = Client
current account id = current ClientAccountId

Query exchanges where:
  exchange.ClientAccountId == currentClientAccountId

Return only client-owned exchanges.
```

Employee branch:

```text
current session role = Employee
current account id = current EmployeeId

First pass:
  verify Employee exists/active if current infrastructure supports it;
  return employee-visible exchanges.

Current visibility policy:
  any active Employee can see/service agreement exchanges.
```

Common summary fields:

```text
- exchange id
- request id
- request display data
- exchange status
- active proposal version
- active proposal sender
- created at
- last activity at
```

No domain transition is executed in this slice.

## 10. Backend Implementation Notes

Do not introduce `AgreementExchangeActor` in this slice.

Use explicit service methods:

```csharp
public interface IAgreementExchangeReadService
{
    Task<Result<AgreementExchangeListResponse, IReadOnlyList<Error>>> ListForClientAsync(
        long clientAccountId,
        AgreementExchangeStatus? status,
        CancellationToken cancellationToken);

    Task<Result<AgreementExchangeListResponse, IReadOnlyList<Error>>> ListForEmployeeAsync(
        long employeeId,
        AgreementExchangeStatus? status,
        CancellationToken cancellationToken);
}
```

Controller flow:

```text
1. Read current role from app session.
2. Read current account id from NameIdentifier claim.
3. Validate optional status filter.
4. If role = Client:
   call ListForClientAsync(currentAccountId, status).
5. If role = Employee:
   call ListForEmployeeAsync(currentAccountId, status).
6. Success -> 200 AgreementExchangeListResponseDto.
7. Failure -> existing ProblemDetails/error mapper.
```

No per-query status enum.

Failures use existing `Error` / `ProblemDetails` mapping.

## 11. Repository / Query Notes

Because this is a read slice, projection query is OK.

Repository/query direction:

```csharp
Task<IReadOnlyList<AgreementExchangeListItemResponse>> ListForClientAsync(
    long clientAccountId,
    AgreementExchangeStatus? status,
    CancellationToken cancellationToken);

Task<IReadOnlyList<AgreementExchangeListItemResponse>> ListForEmployeeAsync(
    long employeeId,
    AgreementExchangeStatus? status,
    CancellationToken cancellationToken);
```

First pass employee query can ignore `employeeId` after active employee validation.

Important:

```text
Client list must not rely on indirect UI route ownership.

It must filter by persisted exchange.ClientAccountId.

Employee list must not rely on ResponsibleEmployeeId, because there is no exchange-level employee owner in first pass.
```

## 12. Server API Notes

Controller direction:

```csharp
[ApiController]
[Route("api/agreement-exchanges")]
public sealed class AgreementExchangesController : ProjectController
{
    [Authorize(Roles = "Client,Employee")]
    [HttpGet(Name = "ListAgreementExchanges")]
    public async Task<IActionResult> List(
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        // resolve role + account id
        // validate filter
        // branch:
        //   Client -> readService.ListForClientAsync(...)
        //   Employee -> readService.ListForEmployeeAsync(...)
        // map success/failure
    }
}
```

Alternative route:

```csharp
[Route("api/l1/agreement-exchanges")]
```

Decision:

```text
Use one shared list endpoint first pass.

Do not create separate /api/l1/... and /api/employee/... list endpoints unless behavior diverges later.
```

## 13. Client Architecture

Shared entity ownership:

```text
entities/agreement-exchange/api/listAgreementExchanges.ts
entities/agreement-exchange/api/agreementExchangeApiTypes.ts
entities/agreement-exchange/model/agreementExchangeTypes.ts
entities/agreement-exchange/model/useAgreementExchangeListQuery.ts
```

Shared UI/widget:

```text
widgets/agreement-exchange-list/AgreementExchangeList
```

Page placement can be role-shell specific:

```text
pages/client/agreement-exchanges
pages/employee/agreement-exchanges
```

But both use the same entity query and list widget.

Button visibility:

```text
Client:
  show client actions only when status/active sender allow it.

Employee:
  show employee actions only when status/active sender allow it.

Server remains authoritative.
```

## 14. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item                                       | How slice covers it                | Status                  |
| --------------------------------------------------- | ---------------------------------- | ----------------------- |
| Client sees only own exchanges                      | query filters by `ClientAccountId` | target                  |
| Client cannot see another client exchange           | persisted `ClientAccountId` filter | target                  |
| Employee sees visible exchanges                     | employee branch query              | target                  |
| Any active Employee can service exchange first pass | no responsible employee guard      | target                  |
| List shows exchange status                          | summary DTO                        | target                  |
| List shows active version                           | summary DTO                        | target                  |
| List shows active proposal sender                   | summary DTO                        | target                  |
| List does not include full history                  | details slice owns it              | target                  |
| UI uses shared component                            | shared entity/widget               | target                  |
| Server protects access                              | role + branch-specific query       | target                  |
| Employee sender history is preserved                | proposal-level author fields       | existing/current domain |

## 15. Test / Verification Plan

| Test / check                                    | Verifies                              | Layer              | Status |
| ----------------------------------------------- | ------------------------------------- | ------------------ | ------ |
| unauthenticated list -> 401                     | auth boundary                         | integration        | target |
| Client sees own exchange                        | client filter                         | integration        | target |
| Client does not see another client exchange     | ownership filter                      | integration        | target |
| Employee sees exchange                          | employee visibility first pass        | integration        | target |
| inactive Employee cannot list                   | active employee guard, if implemented | integration        | target |
| status filter works                             | query validation/filtering            | integration        | target |
| invalid status filter -> 422                    | DTO/query validation                  | integration        | target |
| response includes active proposal version       | projection                            | integration        | target |
| response includes active proposal sender        | projection                            | integration        | target |
| response does not include full proposal history | contract boundary                     | integration        | target |
| no `ResponsibleEmployeeId` filter is required   | employee visibility decision          | integration/domain | target |
| client query hook calls shared endpoint         | client unit                           | target             |        |
| list component renders exchange rows            | client UI/unit                        | target             |        |

## 16. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
GET /api/agreement-exchanges
responses:
  200 AgreementExchangeListResponseDto
  401
  403
  422
  500
```

Generated artifacts must be updated through tools only:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd run check:api
```

## 17. Dependent / Follow-up Slices

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-004 — Agreement Exchange Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

## 18. Implementation Checklist

```text
[ ] ensure AgreementProposalExchange has ClientAccountId
[ ] ensure StartByEmployee fills ClientAccountId from approved request owner
[ ] do not add ResponsibleEmployeeId as authorization guard
[ ] add list response DTOs
[ ] add optional query DTO/filter validation
[ ] add shared list endpoint
[ ] add read service with ListForClientAsync
[ ] add read service with ListForEmployeeAsync
[ ] add client branch query by ClientAccountId
[ ] add employee branch query by visibility policy
[ ] return 200 with summaries
[ ] add integration tests for client ownership filtering
[ ] add integration tests for employee visibility
[ ] add client entity API wrapper
[ ] add React Query hook
[ ] add shared list widget/page
[ ] regenerate OpenAPI/types
```

## 19. Guardrail Summary

```text
This is a read/list slice.

Do not perform lifecycle transitions here.

Use shared endpoint first pass.

Use shared client entity/query/model first pass.

Do not introduce AgreementExchangeActor abstraction in this draft.

Use current role/current account id from session.

Client access must filter by AgreementProposalExchange.ClientAccountId.

Employee access first pass allows any active Employee.

Do not add ResponsibleEmployeeId as exchange authorization guard.

Employee sender identity is tracked per proposal version.

Do not include full proposal history in list response.

Request details and exchange reads remain separate.

UI buttons are not security.

Server commands still enforce domain lifecycle and participant rules.
```
