# SL-AGR-EXCH-006 — Final Refuse Agreement Exchange

Status: implemented slice draft refactor / implementation spot-checked from uploaded repo snapshot / runtime not changed in this pass  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: Employee backend/API command slice  
Primary purpose: Employee finally refuses an active agreement exchange and marks the related request as agreement-exchange failed  
Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* `SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version`
* `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`
* `SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details`
* `SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal`
* `AgreementProposalExchange.FinalRefuseProposal(...)`
* `ConnectionRequest.MarkAgreementExchangeFailed(...)`
* Employee auth/session
* CSRF / unsafe command protection

Implementation direction:

```text
POST /api/agreement-exchanges/{exchangeId}/final-refuse

Employee-only command:
  optional nullable reason
  exchange.FinalRefuseProposal(employee, reason, now)
  request.MarkAgreementExchangeFailed(exchange.Id, now)
  save both aggregate changes atomically
  return 204 No Content
```

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation files were read only to align names/routes/DTOs/tests:
- no runtime code changes;
- no test changes;
- no generated artifacts changed;
- no runtime UI refactor;
- no page-flow / redirect audit.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-13E — Agreement Final Refusal
```

Related scenarios:

```text
SC-13B — Client Agreement Proposal Details / Response
SC-13C — Employee Agreements
SC-13D — Employee Agreement Proposal Create / Send Version
SC-14 — Agreement Documents, future document/file flow only
```

UI scenario:

```text
missing / pending dedicated UI source for Employee final refusal action.
```

Cross-cutting behavior:

```text
CC-SEC-CSRF-001 — Unsafe Command Protection
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for paired client sidecar only
```

Data source:

```text
pending scenario-data source for final refusal reason copy/length and post-command read states.
```

Behavior items:

```text
SC-13E has local behavior item names:
- L2-AGR-FINAL-001
- L2-AGR-FINAL-002
- L2-AGR-FINAL-003
- L2-REQ-AGR-FAIL-001
- L2-AGR-BOUNDARY-001
- L2-AGR-FINAL-REASON-001

A stable source registry / derivation map row is still pending.
```

Concern umbrella:

```text
none for this server slice;
CSRF is a cross-cutting security concern.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-13E: pending / v000 if source registry is applied
SC-13B/13C/13D: related context only
CC-SEC-CSRF-001: v001 if source registry is applied
```

Domain baseline:

```text
DOM-v001 if source-sync/domain registry is applied;
otherwise pending domain baseline.

Current uploaded repo snapshot confirms domain classes/methods exist:
- AgreementProposalExchange.FinalRefuseProposal(Employee, FinalRefusalReason?, DateTimeOffset)
- FinalRefusalReason.Create(string)
- FinalRefusalReason.MaxLength = 2000
- ConnectionRequest.MarkAgreementExchangeFailed(long, DateTimeOffset)
- RequestStatus.AgreementExchangeFailed
```

Slice derivation map:

```text
pending / add or update row for SL-AGR-EXCH-006 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| `L2-AGR-FINAL-001` — Employee can final-refuse active exchange | SC-13E pending | provided by `AgreementProposalExchange.FinalRefuseProposal` | expose Employee-only API command and call domain method | allowed statuses are `AwaitingClientConfirmation` / `AwaitingEmployeeResponse` |
| `L2-AGR-FINAL-002` — exchange becomes `FinallyRefused` and records employee/time/reason | SC-13E pending | provided by exchange domain state | persist exchange state after domain call | implementation has `FinalRefusedByEmployeeId`, `FinalRefusedAt`, `FinalRefusalReason` |
| `L2-AGR-FINAL-003` — final refusal does not create proposal version | SC-13E pending | domain command does not append proposal | API command must not create proposal/document | tests assert proposal count unchanged |
| `L2-REQ-AGR-FAIL-001` — related request becomes `AgreementExchangeFailed` | SC-13E pending | provided by `ConnectionRequest.MarkAgreementExchangeFailed` | load related request and call request domain method in same application flow | only approved request can be marked failed |
| `L2-AGR-BOUNDARY-001` — exchange/request stay separate aggregates | SC-13E pending | aggregate boundary decision | application service orchestrates both aggregates; exchange does not mutate request directly | one SaveChanges after both domain calls |
| `L2-AGR-FINAL-REASON-001` — reason optional, blank invalid | SC-13E pending | value object + API validation | accept nullable body; validate provided blank/too-long reason | MaxLength = 2000 |
| Client final refusal is out of scope | SC-13E pending | not provided first pass | endpoint requires Employee role | paired client sidecar does not wire Client final refusal |
| No `ResponsibleEmployeeId` guard | SC-13C/13E pending | access policy decision | do not add exchange-level Employee ownership guard | any active Employee first pass |
| Unsafe command is CSRF-protected | CC-SEC-CSRF-001 | cross-cutting server/client concern | apply current antiforgery boundary | full CSRF matrix belongs to cross-cutting tests |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync
```

Implemented files checked in uploaded repo snapshot:

```text
server:
  EnergyManagement.Server/L1/Controllers/AgreementExchangesController.cs
  EnergyManagement.Server/L1/Application/Services/AgreementExchangeApplicationService.cs
  EnergyManagement.Server/L1/Api/L1Dtos.cs
  EnergyManagement.Server/L1/Api/Validation/FinalRefuseAgreementExchangeDtoValidator.cs

server abstractions/repositories involved:
  EnergyManagement.Server/L1/Application/Abstractions/IAgreementExchangeApplicationService.cs
  EnergyManagement.Server/L1/Application/Abstractions/IAgreementProposalExchangeRepository.cs
  EnergyManagement.Server/L1/Application/Abstractions/IClientRequestRepository.cs
  EnergyManagement.Server/L1/Application/Abstractions/IEmployeeRepository.cs

domain:
  Domain.EnergyManagement/L1/AgreementProposals/AgreementProposalExchange.cs
  Domain.EnergyManagement/L1/AgreementProposals/FinalRefusalReason.cs
  Domain.EnergyManagement/L1/Requests/ConnectionRequest.cs
  Domain.EnergyManagement/L1/Requests/RequestStatus.cs

tests:
  Tests.EnergyManagement/Integration/L1/AgreementExchanges/AgreementExchangeFinalRefusalIntegrationTests.cs
  Tests.EnergyManagement/Domain/AgreementProposals/AgreementProposalExchangeTests.cs
  Tests.EnergyManagement/Domain/Requests/ConnectionRequestAgreementExchangeFailureTests.cs

paired client implementation evidence:
  energymanagement.client/src/features/agreement-exchange/final-refuse/**
```

Checked against:

```text
source versions:
  pending source-sync registry

domain baseline:
  uploaded repo snapshot confirms current final-refusal domain API and tests

slice derivation map version:
  pending

runtime tests:
  not executed in this docs-only pass
```

Known drift corrected by this refactor:

```text
docs:
  - old draft lacked Scenario Sources block;
  - old draft lacked Source / Domain / Slice Coverage Snapshot;
  - old draft lacked Implementation Sync Status;
  - old test plan was not a Behavior-to-Test Trace with escape/refactor risk;
  - old draft did not list concrete current implementation/test files;
  - old draft did not separate code evidence from docs-only archive scope.

implementation:
  - code was read for names/routes/DTO/test evidence only;
  - no runtime/code/test changes are included in this archive.

source:
  - stable source registry / derivation map rows remain pending.
```

Last sync note:

```text
Docs-only refactor using uploaded repo code as evidence. No runtime implementation changes and no test execution in this pass.
```

---

## 1. Scope

This slice owns:

```text
- Employee final-refuse command endpoint;
- Employee role requirement;
- CSRF protection for unsafe browser command;
- current Employee id resolved from app session/claims;
- optional nullable request body;
- optional final refusal reason shape validation;
- loading Employee;
- loading AgreementProposalExchange by exchangeId;
- loading related ConnectionRequest by exchange.RequestId;
- calling exchange.FinalRefuseProposal(employee, reason, now);
- calling request.MarkAgreementExchangeFailed(exchange.Id, now);
- saving both aggregate changes atomically;
- 204 No Content success response;
- ProblemDetails mapping through existing project conventions;
- integration/domain test plan and actual-test trace.
```

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/final-refuse
```

Current implementation evidence:

```csharp
[Authorize(Roles = "Employee")]
[RequireAntiforgeryToken]
[HttpPost("{exchangeId:long:min(1)}/final-refuse", Name = "EmployeeFinalRefuseAgreementExchange")]
public async Task<IActionResult> FinalRefuse(
    long exchangeId,
    [FromBody] FinalRefuseAgreementExchangeDto? dto,
    CancellationToken cancellationToken)
```

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Agreement exchange list read | `SL-AGR-EXCH-003` |
| Agreement exchange details read | `SL-AGR-EXCH-004` |
| Start exchange with initial Employee proposal | `SL-AGR-EXCH-001` |
| Send counter-proposal / new proposal version | `SL-AGR-EXCH-002` |
| Client accept active proposal | `SL-AGR-EXCH-005` |
| Client-side final refusal | future explicit scenario/slice only |
| Request review rejection | `SL-EMP-REQ-005` |
| Binary document download/storage | future document/file slice |
| Adding `ResponsibleEmployeeId` guard | explicitly not first pass |
| Returning exchange/request details from command | read slices after refetch |
| Runtime/client UI implementation changes | not included in this docs archive |
| Generated OpenAPI/type edits | not included in this docs archive |

Important boundary:

```text
This command changes agreement exchange and related request state.
It does not create proposal versions.
It does not accept an active proposal.
It does not send a counter-proposal.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-001
  Owns initial exchange creation and first Employee proposal.

SL-AGR-EXCH-002
  Owns sending proposal versions inside an existing exchange.

SL-AGR-EXCH-003
  Owns shared agreement exchange list read.

SL-AGR-EXCH-004
  Owns shared agreement exchange details read and proposal history display source.

SL-AGR-EXCH-005
  Owns Client accept active proposal.

SL-AGR-EXCH-006
  Owns Employee final refusal and request AgreementExchangeFailed orchestration.

L2-AGR-EXCH-FINAL-REFUSE-001.client
  Owns Employee-side final refusal UI/form/mutation planning.

Domain / persistence
  Owns `AgreementProposalExchange.FinalRefuseProposal`, `FinalRefusalReason`, and `ConnectionRequest.MarkAgreementExchangeFailed`.

Validation / ProblemDetails cross-cutting rules
  Own request shape validation and error mapping conventions.

OpenAPI/generated artifact workflow
  Owns regeneration/checks if API contract changes.
```

---

## 4. Scenario Flow

```text
Employee opens Agreement Exchange details
        ↓
System shows active agreement exchange
        ↓
Employee chooses Final Refuse
        ↓
Employee may optionally provide refusal reason
        ↓
System validates reason shape if provided
        ↓
System verifies Employee can final-refuse the exchange
        ↓
System marks exchange as FinallyRefused
        ↓
System marks related ConnectionRequest as AgreementExchangeFailed
        ↓
Command succeeds without response body
        ↓
Employee/Client refresh exchange/request reads
```

Scenario flow table:

| Step | Actor/system | Behavior |
|---|---|---|
| F01 | Employee | Opens agreement exchange details. |
| F02 | System | Shows active exchange state. |
| F03 | Employee | Chooses final refusal. |
| F04 | Employee | Optionally enters final refusal reason. |
| F05 | System | Validates request body shape if body/reason is provided. |
| F06 | System/domain | Verifies Employee capability and exchange lifecycle. |
| F07 | System/domain | Marks exchange as `FinallyRefused`. |
| F08 | System/domain | Marks related request as `AgreementExchangeFailed`. |
| F09 | System | Returns `204 No Content`. |
| F10 | UI/read side | Refetches exchange/request reads. |

---

## 5. Implementation Flow

```text
[HTTP POST]
POST /api/agreement-exchanges/{exchangeId}/final-refuse
        ↓
[CSRF boundary]
RequireAntiforgeryToken
        ↓
[Auth boundary]
Employee role required
        ↓
[Session context]
TryGetCurrentL1AccountId -> Employee account id
        ↓
[Body handling]
FinalRefuseAgreementExchangeDto? dto
        ↓
[Validation]
validate dto only when dto != null
        ↓
[Application service]
EmployeeFinalRefuseAgreementExchangeAsync(employeeId, exchangeId, dto?.Reason)
        ↓
[Load]
Employee, AgreementProposalExchange, related ConnectionRequest
        ↓
[Domain]
FinalRefusalReason? created from optional reason
exchange.FinalRefuseProposal(employee, reason, now)
request.MarkAgreementExchangeFailed(exchange.Id, now)
        ↓
[Persistence]
SaveChangesAsync once
        ↓
[Response]
204 No Content
```

Implementation ownership:

```text
Controller:
  route/auth/CSRF/session/body-validation boundary and response mapping.

Validator:
  optional reason shape only.

Application service:
  loads Employee, Exchange, related Request;
  creates optional FinalRefusalReason;
  orchestrates two aggregates;
  saves once after both domain calls succeed.

Domain:
  owns final refusal lifecycle and request AgreementExchangeFailed transition.

Client/UI:
  paired sidecar owns form/mutation/refetch behavior.
```

Guardrail:

```text
Application layer must not manually set:
- exchange.Status
- request.Status
- proposal state

It must call domain methods and persist their result.
```

---

## 6. API Contract

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/final-refuse
```

Route:

```text
exchangeId: long, positive
```

Auth:

```csharp
[Authorize(Roles = "Employee")]
```

CSRF:

```csharp
[RequireAntiforgeryToken]
```

Request body:

```csharp
public sealed record FinalRefuseAgreementExchangeDto(
    [property: JsonPropertyName("reason")] string? Reason);
```

Body handling:

```text
body omitted -> allowed
null body -> allowed
reason omitted -> allowed
reason null -> allowed
valid reason string -> allowed and trimmed by FinalRefusalReason.Create
blank / whitespace-only reason -> validation/domain error
reason length > FinalRefusalReason.MaxLength -> validation/domain error
```

Success:

```http
204 No Content
```

Error categories:

```text
400 Bad Request
  antiforgery failure if current project CSRF boundary uses 400.

401 Unauthorized
  no authenticated session.

403 Forbidden
  authenticated but not Employee.

404 Not Found
  route/resource missing according to existing project mapping.

422 Unprocessable Entity
  validation/domain failure:
  - blank/too-long reason when provided;
  - employee missing/invalid;
  - exchange missing;
  - related request missing;
  - exchange already Accepted;
  - exchange already FinallyRefused;
  - exchange status does not allow final refusal;
  - related request is not Approved.

500 Internal Server Error
  unexpected server failure.
```

---

## 7. Validation / FluentValidation

Current validator:

```text
FinalRefuseAgreementExchangeDtoValidator
```

Validation ownership:

```text
reason is optional.
If reason is null, validation passes.
If reason is whitespace-only, validation fails on field `reason`.
If reason length > FinalRefusalReason.MaxLength, validation fails on field `reason`.
```

Current limit:

```text
FinalRefusalReason.MaxLength = 2000
```

Do not put these in FluentValidation:

```text
- Employee exists;
- Employee can final-refuse;
- exchange exists;
- request exists;
- exchange lifecycle;
- request lifecycle;
- current exchange status;
- active proposal state;
- DB reads;
- mutations.
```

Those are application/domain responsibilities.

---

## 8. Domain Rules

Exchange final refusal:

```csharp
exchange.FinalRefuseProposal(employee, reason, refusedAt);
```

Current domain success state:

```text
exchange.Status = AgreementExchangeStatus.FinallyRefused
exchange.FinalRefusedByEmployeeId = employee.Id
exchange.FinalRefusedAt = refusedAt
exchange.FinalRefusalReason = reason
```

Exchange rejection cases include:

```text
- accepted exchange cannot be refused;
- already finally refused exchange cannot be refused again;
- non-active status cannot be finally refused now;
- invalid Employee cannot final-refuse.
```

Request failure:

```csharp
connectionRequest.MarkAgreementExchangeFailed(exchange.Id, now);
```

Current request success state:

```text
request.Status = RequestStatus.AgreementExchangeFailed
```

Request rejection cases include:

```text
- missing/invalid exchange id;
- only Approved request can be marked AgreementExchangeFailed.
```

Important:

```text
No proposal version is created.
ActiveProposalVersion remains unchanged.
Proposal history remains intact.
No ResponsibleEmployeeId guard is introduced first pass.
```

---

## 9. Application / Handler Direction

Current implementation uses application service rather than a separate MediatR command handler:

```csharp
EmployeeFinalRefuseAgreementExchangeAsync(
    long employeeId,
    long exchangeId,
    string? reason,
    CancellationToken cancellationToken)
```

Current application flow:

```text
1. Load Employee by employeeId.
2. Load AgreementProposalExchange by exchangeId.
3. Load ClientRequest by exchange.RequestId and require ConnectionRequest.
4. Create FinalRefusalReason? from optional reason.
5. Get DateTimeOffset.UtcNow once.
6. Call exchange.FinalRefuseProposal(employee, reason, now).
7. Call connectionRequest.MarkAgreementExchangeFailed(exchange.Id, now).
8. SaveChangesAsync once.
9. Return UnitResult success.
```

No per-command status enum:

```text
Do not add FinalRefuseAgreementExchangeCommandStatus.
Use existing UnitResult<IReadOnlyList<Error>> / ProblemDetails mapping conventions.
```

Atomicity rule:

```text
If exchange refusal succeeds but request failure transition fails, do not save partial state.
Save only after both domain method calls succeed.
```

---

## 10. Security / Protection

Security layers:

```text
1. Auth role:
   endpoint requires Employee.

2. CSRF:
   unsafe browser command is protected with RequireAntiforgeryToken.

3. Session identity:
   employee id comes from authenticated account id, not request body.

4. Employee capability:
   domain checks Employee final-refusal capability.

5. Exchange lifecycle:
   domain checks active/refusable status.

6. Request lifecycle:
   request domain checks only Approved request can become AgreementExchangeFailed.
```

Guardrail:

```text
UI action visibility is not authorization.
Server/domain must reject invalid or stale final-refusal attempts.
```

---

## 11. Behavior Coverage

| Behavior item / behavior | Status | Covered by this slice |
|---|---|---|
| Employee can finally refuse active exchange | covered | Employee-only endpoint + domain final-refuse method. |
| Client cannot final-refuse | covered | `[Authorize(Roles="Employee")]` and client sidecar out of scope. |
| Any active Employee can service first pass | supported | no `ResponsibleEmployeeId` guard. |
| Accepted exchange cannot be refused | covered | domain guard + integration test. |
| Already refused exchange cannot be refused again | domain-covered | domain tests cover already-finally-refused guard. |
| Exchange becomes `FinallyRefused` | covered | domain state persisted and integration test asserts it. |
| Related request becomes `AgreementExchangeFailed` | covered | application service calls request domain method and tests assert request status. |
| Missing body/null reason allowed | covered | nullable DTO and integration test without body. |
| Blank reason rejected | covered | validator + integration test asserts no mutation. |
| Too-long reason rejected | covered | validator + integration test asserts no mutation. |
| No new proposal version is created | covered | integration test asserts proposal count unchanged. |
| Runtime client form/mutation | paired sidecar | documented by client draft; runtime code not changed here. |

---

## 12. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned / actual test |
|---|---|---|---|---|---|---|
| Unauthenticated caller cannot final-refuse | unsafe command requires auth | API integration | HTTP POST without auth | Low: endpoint boundary checked | Low | `Final_refuse_without_auth_returns_unauthorized` |
| Missing CSRF is rejected | unsafe command protected | API integration | Employee client POST without token | Low for CSRF smoke | Low/Medium: antiforgery wiring changes may need test helper update | `Final_refuse_without_csrf_returns_bad_request` |
| Client cannot final-refuse | Client role rejected | API integration | Client auth fixture + HTTP POST | Low: role boundary checked | Low | `Client_cannot_final_refuse_exchange` |
| Employee final-refuses active exchange with reason | exchange becomes FinallyRefused and request becomes AgreementExchangeFailed | API integration + DB assertion | seeded active exchange/request, POST with reason, DB reads | Low: persisted outcome asserted | Low/Medium: schema/helper refactor can affect setup | `Employee_final_refuses_active_exchange_with_reason` |
| Missing body is allowed | null reason is stored, exchange/request transition succeeds | API integration + DB assertion | POST without body | Low | Low | `Employee_final_refuses_active_exchange_without_body_and_stores_null_reason` |
| Blank reason rejected | validation problem and no mutation | API integration + DB snapshot/assertion | POST whitespace reason | Low if exchange/request unchanged asserted | Low | `Final_refuse_with_blank_reason_returns_validation_problem_and_does_not_mutate` |
| Too-long reason rejected | validation problem and no mutation | API integration + DB assertion | POST 2001-char reason | Low | Low | `Final_refuse_with_too_long_reason_returns_validation_problem_and_does_not_mutate` |
| Accepted exchange cannot be refused | domain rejection and no mutation | API integration + DB assertion | seeded accepted exchange, POST | Low | Low/Medium | `Employee_cannot_final_refuse_accepted_exchange` |
| Related request must be Approved | domain rejection and no mutation | API integration + DB assertion | seeded non-approved related request, POST | Low | Low/Medium | `Employee_cannot_final_refuse_when_related_request_is_not_approved` |
| Domain final-refuse succeeds from AwaitingClientConfirmation | exchange state/audit fields set | Domain test | direct aggregate method call | Medium: domain only, not API/persistence | Low | `FinalRefuseProposal_succeeds_from_awaiting_client_confirmation` |
| Domain final-refuse succeeds from AwaitingEmployeeResponse with null reason | null reason accepted | Domain test | direct aggregate method call | Medium | Low | `FinalRefuseProposal_succeeds_from_awaiting_employee_response_and_accepts_null_reason` |
| Request mark failed transition works | approved request becomes AgreementExchangeFailed | Domain test | direct request domain call | Medium | Low | `MarkAgreementExchangeFailed_succeeds_for_approved_request` |

### Required server verification themes

```text
- public HTTP boundary proof;
- CSRF smoke for unsafe command;
- Employee-only auth;
- nullable/optional reason behavior;
- persisted exchange state;
- persisted request state;
- no proposal version creation;
- no-mutation on validation/domain failures.
```

### What not to test here

```text
- Client final refusal;
- accept/counter-proposal behavior;
- full UI/page-flow behavior;
- file upload/download;
- implementation call order as primary proof;
- exact SaveChanges count as primary proof.
```

---

## 13. OpenAPI / Generated Artifacts

Current API contract evidence expects:

```text
POST /api/agreement-exchanges/{exchangeId}/final-refuse
204
400
401
403
404
422
500
FinalRefuseAgreementExchangeDto? body with reason?: string | null
```

Generated artifacts are not changed by this archive.

When implementation changes API shape, use repo tooling rather than manual edits:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
npm.cmd --prefix energymanagement.client run check:api
```

---

## 14. Implementation Checklist / Current Sync Checklist

```text
[x] current code exposes POST /api/agreement-exchanges/{exchangeId}/final-refuse
[x] current code requires Employee role
[x] current code requires CSRF
[x] current code accepts FinalRefuseAgreementExchangeDto? dto
[x] current DTO has reason?: string | null
[x] current validator allows null reason
[x] current validator rejects blank reason
[x] current validator rejects reason > 2000
[x] current application service loads Employee
[x] current application service loads AgreementProposalExchange
[x] current application service loads related ConnectionRequest
[x] current application service creates optional FinalRefusalReason
[x] current application service calls exchange.FinalRefuseProposal
[x] current application service calls request.MarkAgreementExchangeFailed
[x] current application service saves once after both domain calls succeed
[x] current success response is 204 No Content
[x] current tests include auth/CSRF/role/success/validation/lifecycle coverage
[ ] tests were not executed in this docs-only pass
[ ] source registry / derivation map row still pending
```

---

## 15. Guardrail Summary

```text
Final refusal is an Employee command slice.
It is not Client rejection.
It is not request review rejection.
It is not accept.
It is not counter-proposal.
It affects two aggregates: AgreementProposalExchange and ConnectionRequest.
Application service orchestrates both aggregates.
Domain owns both state transitions.
Do not manually set statuses in application layer.
Do not add ResponsibleEmployeeId first pass.
Do not add per-command status enums.
Do not create proposal versions.
Do not require final refusal reason.
Do reject blank reason if provided.
Do not require request failed timestamp/id fields unless domain/persistence explicitly has them.
Do return 204 No Content on success.
UI visibility is not security.
```
