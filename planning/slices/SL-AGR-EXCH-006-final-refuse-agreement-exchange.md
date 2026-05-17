# SL-AGR-EXCH-006 — Final Refuse Agreement Exchange

Status: implementation-ready server draft
Package: `[L2] Agreement Proposal Exchange`
Slice type: Employee backend/API command slice
Primary purpose: Employee finally refuses active agreement exchange and marks related request as agreement-exchange failed

Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`
* `SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details`
* `SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal`
* agreement exchange persistence
* request persistence
* auth/session
* CSRF boundary

---

## 1. Slice Overview

Target behavior:

```text
Employee opens Agreement Exchange details.

Exchange is active:
  AwaitingClientConfirmation
  or AwaitingEmployeeResponse.

Employee chooses final refusal.

Employee may optionally provide final refusal reason.

System finally refuses the exchange.

System marks related ConnectionRequest as AgreementExchangeFailed.

No proposal version is created.

No active proposal is accepted.

Employee/Client refetch exchange/request reads and see refused/failed state.
```

Important:

```text
This is Employee-only.

This is not client rejection.

This is not request review rejection.

This is not counter-proposal.

This is not accept.

This command affects two aggregates:
  AgreementProposalExchange
  ConnectionRequest

Application layer orchestrates both aggregates.
Domain methods own state transitions.
```

---

## 2. Scope

```text
- add Employee final-refuse endpoint;
- require Employee role;
- require CSRF;
- resolve current Employee from authenticated session;
- accept optional nullable request body;
- validate optional final refusal reason shape;
- load Employee;
- load AgreementProposalExchange by exchangeId;
- load related ConnectionRequest by exchange.RequestId;
- call exchange.FinalRefuseProposal(employee, reason, now);
- call request.MarkAgreementExchangeFailed(exchange.Id, now);
- persist both aggregates atomically;
- return 204 No Content on success;
- add integration tests with DB state assertions.
```

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/final-refuse
```

Auth:

```csharp
[Authorize(Roles = "Employee")]
```

---

## 3. Out of Scope

| Out of scope                                    | Owner                            |
| ----------------------------------------------- | -------------------------------- |
| Agreement exchange list                         | `SL-AGR-EXCH-003`                |
| Agreement exchange details read                 | `SL-AGR-EXCH-004`                |
| Initial exchange creation                       | `SL-AGR-EXCH-001`                |
| Counter-proposal version creation               | `SL-AGR-EXCH-002`                |
| Client accept active proposal                   | `SL-AGR-EXCH-005`                |
| Client-side final refusal                       | future slice only if needed      |
| File download / binary document serving         | future document/file slice       |
| Document upload/storage                         | future document/storage slice    |
| Adding `ResponsibleEmployeeId` guard            | explicitly not needed first pass |
| Returning exchange/request details from command | read slices after refetch        |

---

## 4. Visual Scenario Flow

```text
Employee opens Agreement Exchange details
        ↓
System shows active agreement exchange
        ↓
Employee chooses “Final refuse”
        ↓
Employee optionally provides refusal reason
        ↓
System validates reason shape if provided
        ↓
System verifies Employee can final-refuse agreement exchange
        ↓
System marks exchange as FinallyRefused
        ↓
System marks request as AgreementExchangeFailed
        ↓
Command succeeds without response body
        ↓
Employee/Client refreshes agreement exchange/request details
```

Scenario flow table:

| Step | Actor/system | Behavior                                                 | Status |
| ---- | ------------ | -------------------------------------------------------- | ------ |
| F01  | Employee     | Opens agreement exchange details.                        | target |
| F02  | System       | Shows active exchange state.                             | target |
| F03  | Employee     | Chooses final refusal.                                   | target |
| F04  | Employee     | Optionally enters final refusal reason.                  | target |
| F05  | System       | Validates request body shape if body/reason is provided. | target |
| F06  | System       | Verifies exchange lifecycle allows final refusal.        | target |
| F07  | System       | Marks exchange as `FinallyRefused`.                      | target |
| F08  | System       | Marks request as `AgreementExchangeFailed`.              | target |
| F09  | System       | Returns command success without body.                    | target |

---

## 5. Visual Implementation Flow

```text
[HTTP POST]
POST /api/agreement-exchanges/{exchangeId}/final-refuse
        ↓
[CSRF boundary]
validate unsafe request protection
        ↓
[Auth boundary]
require authenticated Employee session
        ↓
[Session context]
read current employee account id from app cookie identity
        ↓
[Route binding]
exchangeId is positive long
        ↓
[Body handling]
accept nullable body:
  FinalRefuseAgreementExchangeDto? dto
        ↓
[Body validation]
validate optional finalRefusalReason only if provided
        ↓
[Command handler / application orchestration]
load Employee
load AgreementProposalExchange
load ConnectionRequest by exchange.RequestId
        ↓
[Domain]
exchange.FinalRefuseProposal(employee, reason, now)
        ↓
[Domain]
request.MarkAgreementExchangeFailed(exchange.Id, now)
        ↓
[Persistence]
save exchange + request changes atomically
        ↓
[Response]
204 No Content
```

Implementation flow table:

| Step | Layer                           | Responsibility                                                         |
| ---- | ------------------------------- | ---------------------------------------------------------------------- |
| I01  | Route / Controller              | Exposes `POST /api/agreement-exchanges/{exchangeId}/final-refuse`.     |
| I02  | CSRF boundary                   | Applies unsafe-request protection.                                     |
| I03  | Auth boundary                   | Allows authenticated Employee only.                                    |
| I04  | Session context                 | Reads current employee id from claims.                                 |
| I05  | Route binding                   | Binds `exchangeId` as positive `long`.                                 |
| I06  | Body handling                   | Accepts nullable request body.                                         |
| I07  | FluentValidation                | Validates optional reason shape only.                                  |
| I08  | Command handler / orchestration | Loads Employee, Exchange, Request.                                     |
| I09  | Domain — Exchange               | Calls `exchange.FinalRefuseProposal(employee, reason, now)`.           |
| I10  | Domain — Request                | Calls `request.MarkAgreementExchangeFailed(exchange.Id, now)`.         |
| I11  | Persistence                     | Saves both aggregate changes atomically.                               |
| I12  | API response                    | Returns `204 No Content`; failures use existing ProblemDetails mapper. |

Guardrail:

```text
Application layer must not manually set:

exchange.Status = ...
request.Status = ...

It must call domain methods:

exchange.FinalRefuseProposal(...)
request.MarkAgreementExchangeFailed(...)
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

Request body:

```ts
type FinalRefuseAgreementExchangeDto = {
  reason?: string | null;
};
```

Body handling:

```text
Request body is optional.

Controller should accept nullable DTO:

[FromBody] FinalRefuseAgreementExchangeDto? dto

No body:
  reason = null

Null body:
  reason = null

reason = null:
  reason = null

reason = valid string:
  create FinalRefusalReason

reason = blank string:
  422 validation error
```

Success:

```http
204 No Content
```

Response body:

```text
none
```

Reason:

```text
Employee already has exchangeId.

Final refusal state is visible through exchange/request reads after refetch.

No command DTO is needed.
```

Error responses:

```text
401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Employee

404 NotFound
  exchange or related request does not exist

422 UnprocessableEntity
  request body validation or domain lifecycle rejection:
  - reason is blank/too long when provided;
  - Employee cannot final-refuse agreements;
  - exchange is already Accepted;
  - exchange is already FinallyRefused;
  - exchange is not in an active refusal-allowed status;
  - related request is not Approved;
  - request cannot be marked AgreementExchangeFailed.

400 Bad Request
  antiforgery failure if current project CSRF boundary uses 400
```

---

## 7. Validation / FluentValidation

Route validation:

```text
exchangeId must be positive.
```

Body validation:

```text
reason:
  optional;
  missing body -> allowed;
  null body -> allowed;
  missing/null reason -> allowed;
  provided blank reason -> 422;
  provided reason length > 2000 -> 422;
  valid reason -> trim and create FinalRefusalReason.
```

Current domain limit:

```text
FinalRefusalReason.MaxLength = 2000
```

Validator owns only:

```text
- body shape;
- reason blank/length when reason is provided;
- API field names in ProblemDetails.
```

Validator does **not** own:

```text
- employee exists;
- employee active/capability;
- exchange exists;
- request exists;
- exchange lifecycle;
- request lifecycle;
- accepted/finally refused state;
- DB reads;
- mutations.
```

Those belong to application/domain.

---

## 8. Domain Rules

Exchange preconditions:

```text
- current actor is Employee;
- employee exists;
- employee.Id > 0;
- employee.EnsureCanFinalRefuseAgreement() succeeds;
- exchange.Status is AwaitingClientConfirmation or AwaitingEmployeeResponse;
- exchange.Status is not Accepted;
- exchange.Status is not FinallyRefused.
```

Domain call:

```csharp
exchange.FinalRefuseProposal(employee, reason, refusedAt);
```

Exchange success state:

```text
exchange.Status = AgreementExchangeStatus.FinallyRefused
exchange.FinalRefusedByEmployeeId = employee.Id
exchange.FinalRefusedAt = refusedAt
exchange.FinalRefusalReason = reason
```

Request preconditions:

```text
- related request exists;
- request.Status == Approved.
```

Domain call:

```csharp
request.MarkAgreementExchangeFailed(exchange.Id, failedAt);
```

Request success state first pass:

```text
request.Status = RequestStatus.AgreementExchangeFailed
```

Do **not** assert additional request fields unless they exist in persistence:

```text
Do not require:
- AgreementExchangeFailedAt
- FailedAgreementExchangeId
- RejectedExchangeId

Only assert them if domain/persistence explicitly has them.
```

Important:

```text
No new proposal version is created.

ActiveProposalVersion remains unchanged.

Proposal history remains intact.

Employee is not exchange-level owner.

No ResponsibleEmployeeId guard.
```

---

## 9. Application / Handler Direction

Preferred command shape:

```csharp
public sealed record EmployeeFinalRefuseAgreementExchangeCommand(
    long ExchangeId,
    long EmployeeId,
    string? Reason)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;
```

Handler/application flow:

```text
1. Resolve current Employee id from session.
2. Validate nullable DTO/reason shape before command or in controller validation boundary.
3. Load Employee.
4. Load AgreementProposalExchange by exchangeId.
5. If exchange missing, return NotFound-style Error.
6. Build FinalRefusalReason?:
   - null if no body;
   - null if reason is null/missing;
   - FinalRefusalReason.Create(reason.Trim()) if provided.
7. Load ConnectionRequest by exchange.RequestId.
8. Call exchange.FinalRefuseProposal(employee, reason, now).
9. Call request.MarkAgreementExchangeFailed(exchange.Id, now).
10. SaveChanges once.
11. Return UnitResult success.
```

Order / atomicity note:

```text
Load exchange and request before SaveChanges.

If exchange final refusal succeeds but request mark failed fails, do not save partial state.

Use one unit of work / transaction boundary.
```

Do not add per-command status enum:

```text
Do not add:
- EmployeeFinalRefuseAgreementExchangeCommandStatus
- FinalRefuseAgreementExchangeCommandStatus
```

Use existing:

```csharp
UnitResult<IReadOnlyList<Error>>
```

`AgreementExchangeStatus` and `RequestStatus` are persisted domain states, not command execution statuses.

---

## 10. Security / Protection

Security layers:

```text
1. Auth role:
   endpoint requires Employee.

2. Session identity:
   employee id comes from authenticated account id, not request body.

3. Employee capability:
   domain checks employee.EnsureCanFinalRefuseAgreement().

4. Exchange lifecycle:
   domain checks active status and not Accepted/FinallyRefused.

5. Request lifecycle:
   request domain checks only Approved request can become AgreementExchangeFailed.
```

Guardrail:

```text
UI button visibility is not authorization.

Even if UI hides the Final Refuse button, server must enforce:
- Employee role;
- active Employee capability;
- exchange lifecycle;
- request lifecycle.
```

---

## 11. Behavior Coverage

| Behavior item                                    | How slice covers it                                       | Status |
| ------------------------------------------------ | --------------------------------------------------------- | ------ |
| Employee can finally refuse active exchange      | `POST /api/agreement-exchanges/{exchangeId}/final-refuse` | target |
| Client cannot final-refuse                       | Employee-only auth                                        | target |
| Any active Employee can final-refuse first pass  | no `ResponsibleEmployeeId` guard                          | target |
| Accepted exchange cannot be refused              | domain lifecycle guard                                    | target |
| Already refused exchange cannot be refused again | domain lifecycle guard                                    | target |
| Exchange becomes `FinallyRefused`                | domain state persisted                                    | target |
| Request becomes `AgreementExchangeFailed`        | request domain method persisted                           | target |
| Missing/null reason is allowed                   | nullable body handling                                    | target |
| Blank reason is rejected                         | validation                                                | target |
| No new proposal version is created               | DB assertion                                              | target |
| Proposal history remains intact                  | no proposal mutation                                      | target |

---

## 12. Test / Verification Plan

Primary verification: API integration tests with DB state assertions.

No unit tests by default unless reusable helper logic with branching is introduced.

API boundary:

```text
- unauthenticated final-refuse -> 401;
- Client calls final-refuse endpoint -> 403;
- Employee final-refuses active exchange -> 204.
```

Success DB assertions:

```text
Given:
- exchange.Status = AwaitingClientConfirmation or AwaitingEmployeeResponse;
- request.Status = Approved;
- proposal count = N.

When:
- Employee final-refuses exchange.

Expect:
- HTTP 204;
- response body empty;
- exchange.Status = FinallyRefused;
- exchange.FinalRefusedByEmployeeId = current Employee id;
- exchange.FinalRefusedAt is set;
- exchange.FinalRefusalReason = submitted reason or null;
- request.Status = AgreementExchangeFailed;
- proposal count remains N;
- ActiveProposalVersion unchanged.
```

Reason validation:

```text
- missing body -> allowed, stores null reason;
- null body -> allowed, stores null reason;
- missing reason -> allowed, stores null reason;
- null reason -> allowed, stores null reason;
- blank reason -> 422;
- too long reason -> 422;
- validation failure does not mutate exchange/request.
```

Lifecycle / no-write failures:

```text
- missing exchange -> 404;
- accepted exchange -> 422;
- already finally refused exchange -> 422;
- related request not Approved -> 422;
- inactive Employee / Employee without capability -> 422 or existing project mapping;
- failed command does not change exchange status;
- failed command does not change request status;
- failed command does not set final refusal audit fields;
- failed command does not create proposal version.
```

Do not assert:

```text
- request failed timestamp;
- request failed exchange id;
- request rejected exchange id;

unless those fields actually exist in domain/persistence.
```

Generated artifacts:

```text
- run OpenAPI generation;
- run API type generation;
- stage generated artifacts;
- run check:api.
```

---

## 13. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/agreement-exchanges/{exchangeId}/final-refuse

204
400
401
403
404
422
500
```

Request body:

```text
FinalRefuseAgreementExchangeDto?
  reason?: string | null
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

## 14. Implementation Checklist

```text
[ ] add POST /api/agreement-exchanges/{exchangeId}/final-refuse
[ ] require Employee role
[ ] require CSRF
[ ] do not accept employee id in body
[ ] resolve employee id from session
[ ] add nullable FinalRefuseAgreementExchangeDto with optional reason
[ ] controller accepts FinalRefuseAgreementExchangeDto? dto
[ ] add validator for reason only
[ ] missing body/null reason is allowed
[ ] blank reason is rejected
[ ] add EmployeeFinalRefuseAgreementExchangeCommand
[ ] add command handler
[ ] load Employee
[ ] load AgreementProposalExchange by exchangeId
[ ] load ConnectionRequest by exchange.RequestId
[ ] build FinalRefusalReason? only if reason provided
[ ] call exchange.FinalRefuseProposal(employee, reason, now)
[ ] call request.MarkAgreementExchangeFailed(exchange.Id, now)
[ ] save both aggregate changes atomically
[ ] return 204 No Content
[ ] add integration tests for success
[ ] add integration tests for missing/null reason
[ ] add integration tests for validation failures
[ ] add integration tests for lifecycle failures
[ ] assert request.Status = AgreementExchangeFailed
[ ] assert no new proposal version is created
[ ] do not assert non-existent request failure timestamp/id
[ ] regenerate OpenAPI/types
[ ] do not implement Client final refusal
[ ] do not implement accept here
[ ] do not implement counter-proposal here
[ ] do not return details/list DTO from command
```

---

## 15. Guardrail Summary

```text
Final refusal is an Employee command slice.

It affects two aggregates:
  AgreementProposalExchange
  ConnectionRequest

Application/handler orchestrates both aggregates.

Domain owns transitions:
  exchange.FinalRefuseProposal(...)
  request.MarkAgreementExchangeFailed(...)

Do not manually set statuses in application layer.

Do not add ResponsibleEmployeeId.

Do not add per-command status enums.

Do not rely on UI button visibility for security.

Do not put lifecycle/ownership/status checks in FluentValidation.

Do validate only optional reason shape in FluentValidation.

Do allow no body/null body/null reason first pass.

Do reject blank reason when provided.

Do only assert request.Status = AgreementExchangeFailed unless more fields exist.

Do return 204 No Content on success.

Do refetch exchange/request reads after success.
```

Готово к имплементации.
