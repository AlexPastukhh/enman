# SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List

Status: implemented slice draft refactor / implementation not rechecked in this pass  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: shared server read endpoint + shared read model  
Primary purpose: Client or Employee reads a filtered list of visible AgreementProposalExchanges  
Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* `AgreementProposalExchange.ClientAccountId`
* Client/Employee auth/session
* agreement exchange persistence/read projection

Implementation direction:

```text
GET /api/agreement-exchanges

Client branch:
  list exchanges where exchange.ClientAccountId == current ClientAccountId

Employee branch:
  first pass: any active Employee can see/service agreement exchanges
```

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was not rechecked in this pass.
Client UI/page flow and redirects are out of scope for this server draft pass.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-13A — Agreement Exchange List
```

Related scenarios:

```text
SC-13B — Agreement Exchange Details
SC-13D — Start Agreement Exchange
```

UI scenario:

```text
missing / pending dedicated UI source for Client and Employee agreement exchange list pages
```

Cross-cutting behavior:

```text
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for future client sidecar
```

Data source:

```text
pending scenario-data source for agreement exchange list row summary fields and optional filters
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior names until source-sync files are completed.
```

Concern umbrella:

```text
none for this server read slice.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-13A: pending / v000 if source registry is applied
SC-13B: pending / v000 if source registry is applied
```

Domain baseline:

```text
DOM-v001 if source-sync/domain registry is applied;
otherwise pending domain baseline.
```

Slice derivation map:

```text
pending / add row for SL-AGR-EXCH-003 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| Client sees own agreement exchanges | SC-13A pending | depends on persisted `ClientAccountId` | filter read projection by current ClientAccountId | no UI-only ownership |
| Client cannot see another client's exchange | SC-13A pending | depends on persisted `ClientAccountId` | enforce server-side filter | list absence is protection result |
| Employee sees employee-visible exchanges | SC-13A pending | first-pass policy, not domain ownership | validate active Employee if supported and return employee-visible exchanges | any active Employee first pass |
| No exchange-level Employee owner guard | SC-13A pending | domain/access policy decision | do not filter by `ResponsibleEmployeeId` | proposal authors track Employee per version |
| List shows exchange summary | SC-13A pending | read projection responsibility | return compact row DTO | no full history |
| List shows active proposal version/sender | SC-13A/SC-13B pending | from exchange active proposal state | project active version/sender/sender id | helps UI decide visible actions |
| Full proposal history is not in list | SC-13B pending | details read responsibility | keep list DTO small | owned by `SL-AGR-EXCH-004` |
| Optional status filter is validated | SC-13A pending | API/query responsibility | parse/validate status and apply filter | invalid status returns validation problem |
| Commands remain server/domain protected | SC-13A pending | command slices/domain responsibility | list may support UX decisions but is not security boundary | UI buttons are not security |
| One shared endpoint first pass | SC-13A pending | not domain behavior | branch by current role/account in server read service | no separate Client/Employee endpoints |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync
```

Implemented files:

```text
server:
  not rechecked in this pass

client:
  paired client draft refactored in this archive:
    planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md

tests:
  not rechecked in this pass
```

Checked against:

```text
source versions:
  pending source-sync registry

domain baseline:
  pending / DOM-v001 if source-sync files are applied

slice derivation map version:
  pending
```

Known drift:

```text
docs:
  - old draft lacked Source / Domain / Slice Coverage Snapshot;
  - old draft lacked Implementation Sync Status;
  - old draft used behavior coverage but not Behavior-to-Test Trace;
  - old draft mixed server and client ownership; paired client sidecar is now separate but linked.

source:
  - stable behavior item IDs are not yet assigned in source registry.

implementation:
  - not checked in this pass.

UI:
  - not touched in this server pass.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection and no UI/redirect flow inspection.
```

---

## 1. Scope

This slice owns:

```text
- one shared list endpoint for Client and Employee;
- current role and current account id derived from app cookie identity;
- Client branch filters by AgreementProposalExchange.ClientAccountId;
- Employee branch uses first-pass employee visibility policy;
- optional status filter, if implemented;
- compact AgreementExchange list DTO;
- active proposal version/sender summary;
- request display summary fields when available;
- 200 OK read response;
- no lifecycle mutation;
- API integration test plan with visibility and projection assertions.
```

Endpoint:

```http
GET /api/agreement-exchanges
```

First-pass response:

```text
AgreementExchangeListResponseDto
```

This slice does **not** send proposal versions.

This slice does **not** start agreement exchange.

This slice does **not** accept/refuse agreement exchange.

This slice does **not** return full proposal history.

This slice does **not** introduce `ResponsibleEmployeeId`.

This slice does **not** own client page layout or redirects.

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Start exchange with first employee proposal | `SL-AGR-EXCH-001` |
| Send counter-proposal / own version | `SL-AGR-EXCH-002` |
| Exchange details / full history read | `SL-AGR-EXCH-004` |
| Client accept active proposal | `SL-AGR-EXCH-005` |
| Final refusal | `SL-AGR-EXCH-006` |
| Binary file upload/storage | future file/document slice |
| Separate Client/Employee list endpoints | future only if behavior diverges |
| `ResponsibleEmployeeId` as authorization guard | explicitly not this slice |
| Request details full DTO merge | request/details slices |
| Client UI/page shells/list widget | paired client sidecar |
| Page redirects/navigation | page-flow/redirect audit, not this server draft |
| Lifecycle command protection | command slices and domain |

Important boundary:

```text
This is a read/list slice.

No agreement lifecycle transition happens here.

No proposal is sent here.

No accept/refuse action happens here.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-001
  Owns initial exchange creation.

SL-AGR-EXCH-002
  Owns sending later proposal versions.

SL-AGR-EXCH-003
  Owns shared server list endpoint/read projection.

L2-AGR-EXCH-LIST-001.client
  Owns shared client query/model/list widget and actor page shells.

SL-AGR-EXCH-004
  Owns agreement exchange details/full history read.

SL-AGR-EXCH-005
  Owns Client accept active proposal.

SL-AGR-EXCH-006
  Owns final refusal.

Domain / persistence
  Owns target persisted concepts:
  AgreementProposalExchange.RequestId,
  AgreementProposalExchange.ClientAccountId,
  AgreementExchangeStatus,
  ActiveProposalVersion,
  AgreementProposal.Author.Sender,
  AgreementProposal.Author.SenderId,
  no ResponsibleEmployeeId first pass.

Validation / ProblemDetails cross-cutting rules
  Own query filter validation and error mapping conventions.

OpenAPI/generated artifact workflow
  Owns regeneration/checks if API contract changes.
```

---

## 4. Scenario Flow

```text
[Client or Employee]
opens Agreement Exchanges page
        ↓
System resolves current role/account from session
        ↓
System queries visible agreement exchanges
        ↓
Client branch filters by ClientAccountId
        ↓
Employee branch uses employee visibility first-pass policy
        ↓
System returns compact list rows
        ↓
User opens exchange details from row
```

Scenario flow table:

| Step | Actor / System layer | User-visible / system responsibility |
|---|---|---|
| S01 | Client or Employee | Opens agreement exchange list page. |
| S02 | System | Resolves current role and account id. |
| S03 | System | Applies branch-specific visibility. |
| S04 | System | Applies optional status filter. |
| S05 | System | Projects compact rows. |
| S06 | Client/UI | Renders list and navigates to details. |

Scenario meaning:

```text
The list provides visibility and navigation.

It does not decide lifecycle command permissions by itself.
It does not expose full proposal history.
```

---

## 5. Implementation Flow

```text
[HTTP GET]
GET /api/agreement-exchanges?status=...
        ↓
[Auth]
Client or Employee app cookie required
        ↓
[Query validation]
status filter parsed/validated if present
        ↓
[Controller]
read current role and account id from session
        ↓
[Branch]
Client role   -> readService.ListForClientAsync(...)
Employee role -> readService.ListForEmployeeAsync(...)
        ↓
[Read service / projection]
apply visibility and optional filter
project compact list items
        ↓
[Response]
200 OK AgreementExchangeListResponseDto
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, query binding, response mapping;
  may branch by role only to call read service method.

Validator:
  query shape/status value only.
  No ownership/lifecycle validation.

Read service:
  current role branch;
  current account validation if needed;
  read projection query;
  no mutation.

Projection/query:
  filters rows;
  projects summary DTO.

Domain:
  owns persisted facts used by projection.
  No domain lifecycle method is called in this read slice.

Client:
  paired sidecar owns UI/query/list widget.
```

---

## 6. API Contract

### Endpoint

```http
GET /api/agreement-exchanges
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

Success:

```http
200 OK
```

Failure categories:

```text
401 Unauthorized
  no authenticated session

403 Forbidden
  unsupported role / cannot access shared endpoint

422 UnprocessableEntity
  invalid status filter, if current mapper uses validation problem

500 InternalServerError
  unexpected server failure
```

---

## 7. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-003-Q001` | accepted | Separate Client/Employee list endpoints? | No first pass. Use shared endpoint. | Shared UI/client model. |
| `SL-AGR-EXCH-003-Q002` | accepted | Separate Client/Employee frontend API wrappers? | No first pass. Use `entities/agreement-exchange/api`. | Avoid duplicate client code. |
| `SL-AGR-EXCH-003-Q003` | accepted | Does list include full proposal history? | No. Summary only. | Details endpoint owns history. |
| `SL-AGR-EXCH-003-Q004` | accepted | How does Client access work? | Filter by `AgreementProposalExchange.ClientAccountId == currentClientAccountId`. | Requires `ClientAccountId` in domain/persistence. |
| `SL-AGR-EXCH-003-Q005` | accepted | How does Employee access work first pass? | Any active Employee can see/service exchanges. | No `ResponsibleEmployeeId` guard. |
| `SL-AGR-EXCH-003-Q006` | accepted | Should exchange store responsible employee? | No as authorization guard. Optional audit only. | Any active Employee can continue exchange. |
| `SL-AGR-EXCH-003-Q007` | accepted | Should proposal sender info be shown? | Yes, active proposal sender and version should be in summary. | Helps UI decide next action. |
| `SL-AGR-EXCH-003-Q008` | accepted | Should buttons be protected by UI only? | No. UI shows buttons by role/status, server commands still enforce rules. | Security remains server/domain-side. |
| `SL-AGR-EXCH-003-Q009` | accepted | Should list endpoint return `availableActions`? | Optional. First pass can derive from role/status. | Avoid overbuilding. |
| `SL-AGR-EXCH-003-Q010` | accepted | Request details vs exchange list/details? | Separate read endpoints. UI composes pages. | Keeps DTOs smaller. |
| `SL-AGR-EXCH-003-Q011` | accepted | Do we introduce `AgreementExchangeActor` abstraction? | No first pass. Use current role/current account id and explicit client/employee service methods. | Keeps implementation/direct documentation simpler. |
| `SL-AGR-EXCH-003-Q012` | accepted | Where are employees documented? | Per proposal version through `AgreementProposal.Author(Sender=Employee, SenderId=employee.Id)`. | Employee is not exchange-level owner. |
| `SL-AGR-EXCH-003-Q013` | accepted | What participant reference is stored on exchange? | `ClientAccountId`. | Client ownership can be enforced and queried directly. |

---

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

---

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
- active proposal sender id
- created at
- last activity at
```

No domain transition is executed in this slice.

---

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

---

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

---

## 12. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Resolve current Client or Employee server-side. |
| Authorization/role branch | yes | Controller branches by role only to call read service method. |
| Client ownership | yes | Query filters by `exchange.ClientAccountId`. |
| Employee exchange ownership | no first pass | No `ResponsibleEmployeeId` guard; any active Employee can see/service exchange. |
| Request validation / ProblemDetails | yes | Status query filter validation. |
| OpenAPI / generated artifacts | yes | API contract changes require generated artifact workflow. |
| No mutation | yes | Read endpoint must not change exchange/proposal/request state. |
| File/document boundary | no | No documents uploaded/read here beyond summary refs if included. |
| Privacy / cross-account data exposure | yes | Client must not receive another client's exchange. |
| Client feedback / accessibility | paired sidecar | Client draft owns list loading/error/empty states. |
| Redirect/page flow | future | Page-flow audit, not this server slice. |
| Testing responsibility split | yes | API integration + projection/visibility assertions. |

---

## 13. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Client sees only own exchanges | covered | query filters by `ClientAccountId`. |
| Client cannot see another client exchange | covered | persisted `ClientAccountId` filter. |
| Employee sees visible exchanges | covered | employee branch query. |
| Any active Employee can service exchange first pass | covered | no responsible employee guard. |
| List shows exchange status | covered | summary DTO. |
| List shows active version | covered | summary DTO. |
| List shows active proposal sender | covered | summary DTO. |
| List does not include full history | covered | details slice owns it. |
| Server protects access | covered | role + branch-specific query. |
| Employee sender history is preserved | supported | proposal-level author fields. |
| UI uses shared component | out of server scope | paired client sidecar. |
| Commands | out of scope | command slices. |
| Redirect/page flow | out of scope | future page-flow/redirect audit. |

---

## 14. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Client sees own exchanges | response contains only exchanges with current ClientAccountId | API integration + DB fixture | client auth fixture, multiple exchanges, HTTP GET | Low if cross-client exchange absence is asserted | Low/Medium: projection helper/schema refactor may affect test setup | `ListAgreementExchanges_ClientSeesOwnExchangesOnly` |
| Client cannot see another client's exchange | other client's exchange absent from response | API integration | two client fixtures, HTTP GET | Low if absence asserted by ids | Low | same ownership filter test |
| Employee sees employee-visible exchanges | active Employee receives list rows | API integration | employee auth fixture, seeded exchanges, HTTP GET | Medium if employee visibility policy later changes | Low/Medium | `ListAgreementExchanges_EmployeeSeesVisibleExchanges` |
| Inactive Employee cannot list, if supported | response is forbidden/problem | API integration | inactive employee fixture, HTTP GET | Medium; depends on auth/employee policy | Low/Medium | optional if infrastructure supports inactive Employee |
| Status filter works | response contains only requested status | API integration | seeded statuses, HTTP GET with status | Low if both included/excluded ids asserted | Low | `ListAgreementExchanges_WithStatusFilter_ReturnsMatchingRows` |
| Invalid status filter rejected | returns `422` validation/problem | API integration | HTTP GET invalid status | Medium if only status code asserted; Low if field/code checked | Low | `ListAgreementExchanges_WithInvalidStatusFilter_ReturnsValidationProblem` |
| Summary includes active proposal version/sender | response row has active version/sender/sender id | API integration/projection test | seeded exchange/proposal, HTTP GET | Low if fields asserted | Low/Medium | `ListAgreementExchanges_ReturnsActiveProposalSummary` |
| Full history is excluded | response does not include full proposal collection | contract/API test | HTTP GET + response shape assertion | Low | Low | success response contract test |
| No mutation | request/exchange/proposal state unchanged after GET | integration smoke or DB snapshot | HTTP GET, DB snapshot | Medium; not necessary for every read test | Low/Medium | optional read no-mutation smoke |

### API boundary / access

```text
- unauthenticated list returns 401;
- unsupported role returns 403;
- Client can list own exchanges;
- Employee can list employee-visible exchanges.
```

### Read success

```text
- GET returns 200 OK;
- response body is AgreementExchangeListResponseDto;
- rows contain exchangeId/requestId/status/active version/active sender/request display/dates;
- Client branch filters by ClientAccountId;
- Employee branch does not require ResponsibleEmployeeId;
- list does not include full proposal history.
```

### Validation / no-write tests

```text
- invalid status filter returns validation problem;
- read endpoint does not mutate exchange/proposal/request state.
```

### Generated artifacts

```text
- run OpenAPI generation if API contract changed;
- run API type generation if OpenAPI changed;
- stage generated artifacts;
- run check:api.
```

### What not to test

```text
- command lifecycle transitions;
- proposal send/accept/refuse behavior;
- full proposal history;
- client UI component layout;
- redirect/page flow;
- repository mock call-order as primary proof;
- unit tests unless reusable projection helper logic is introduced.
```

---

## 15. Client Pairing

Paired client sidecar:

```text
planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md
```

Client sidecar owns:

```text
- shared entity API wrapper;
- shared query/model;
- shared list widget;
- Client and Employee page shells;
- visible loading/error/empty/success states;
- actor-specific details navigation.
```

Server draft does not own those UI details.

---

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

---

## 17. Dependent / Follow-up Slices

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-004 — Agreement Exchange Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Separate later work:

```text
- page flow / redirects audit;
- UI refactoring workflow;
- source registry/behavior item ID sync.
```

---

## 18. Backend Implementation Direction / Current Refactor Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[ ] Confirm endpoint is present or mark implementation drift.
[ ] Confirm route is GET /api/agreement-exchanges or update draft/source if actual route differs.
[ ] Confirm auth accepts Client and Employee.
[ ] Confirm optional status filter validation.
[ ] Confirm list response DTO exists.
[ ] Confirm Client branch filters by ClientAccountId.
[ ] Confirm Client cannot see another client's exchange.
[ ] Confirm Employee branch does not require ResponsibleEmployeeId.
[ ] Confirm active proposal version/sender/sender id are projected.
[ ] Confirm full proposal history is not returned.
[ ] Confirm no lifecycle mutation happens in read endpoint.
[ ] Confirm no AgreementExchangeActor abstraction is introduced in target direction.
[ ] Confirm focused API integration tests with visibility/projection assertions exist.
[ ] Confirm OpenAPI/type generation is current if contract changed.
```

This pass did not perform implementation verification.

---

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
UI/page redirects are out of scope for this server draft refactor.
```
