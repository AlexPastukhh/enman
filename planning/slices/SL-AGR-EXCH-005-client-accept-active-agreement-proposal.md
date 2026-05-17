# SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal

Status: implementation-ready server draft
Package: `[L2] Agreement Proposal Exchange`
Slice type: Client backend/API command slice
Primary purpose: Client accepts the active Employee proposal in an existing agreement exchange

Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`
* `SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details`
* existing `AgreementProposalExchange.ClientAccountId`
* auth/session
* CSRF boundary

---

## 1. Current Domain Prerequisite

Current domain already has the required client ownership state:

```text
AgreementProposalExchange.ClientAccountId
```

Current domain direction is already correct:

```text
StartByEmployee fills ClientAccountId.

ClientAcceptActiveProposal checks:
  client.Id == ClientAccountId
```

So this slice does **not** need to add `ClientAccountId`. It only needs to expose the API/application command around the existing domain behavior.

---

## 2. Slice Overview

Target behavior:

```text
Client opens agreement exchange details.

Exchange is AwaitingClientConfirmation.

Active proposal was sent by Employee.

Client accepts the active proposal.

System marks:
  AgreementProposalExchange.Status = Accepted
  active AgreementProposal.State = Accepted

No new proposal version is created.

Client refetches exchange details/list and sees Accepted state.
```

Important:

```text
This is a command slice.

This is Client-only first pass.

Employee accept is out of scope.

Accept does not create a new proposal version.

Accept does not upload documents.

Accept does not send counter-proposal.

Accept returns no response DTO.
```

---

## 3. Scope

```text
- add Client accept active proposal endpoint;
- require Client role;
- require CSRF;
- resolve current Client account id from authenticated session;
- load ClientAccount if domain method requires ClientAccount instance;
- load AgreementProposalExchange by exchangeId;
- call AgreementProposalExchange.ClientAcceptActiveProposal(client, now);
- persist exchange/proposal accepted state atomically;
- return 204 No Content on success;
- add integration tests with DB state assertions.
```

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/accept
```

Auth:

```csharp
[Authorize(Roles = "Client")]
```

---

## 4. Out of Scope

| Out of scope                            | Owner                                            |
| --------------------------------------- | ------------------------------------------------ |
| Agreement exchange list                 | `SL-AGR-EXCH-003`                                |
| Agreement exchange details read         | `SL-AGR-EXCH-004`                                |
| Initial exchange creation               | `SL-AGR-EXCH-001`                                |
| Counter-proposal version creation       | `SL-AGR-EXCH-002`                                |
| Employee accept active proposal         | future slice only if scenario/domain requires it |
| Final refusal                           | `SL-AGR-EXCH-006`                                |
| File download / binary document serving | future document/file slice                       |
| Document upload/storage                 | future document/storage slice                    |
| Client UI button implementation         | future client sidecar                            |
| Adding `ClientAccountId` to domain      | already done/current prerequisite                |

---

## 5. Visual Scenario Flow

```text
Client opens Agreement Exchange details
        ↓
System shows active Employee proposal
        ↓
Client chooses “Accept”
        ↓
System verifies Client owns the exchange through domain guard
        ↓
System verifies active proposal is acceptable by Client
        ↓
System accepts active proposal and exchange
        ↓
Command succeeds without response body
        ↓
Client refreshes agreement exchange details/list
        ↓
System shows Accepted state
```

Scenario flow table:

| Step | Actor/system  | Behavior                                              | Status |
| ---- | ------------- | ----------------------------------------------------- | ------ |
| F01  | Client        | Opens agreement exchange details.                     | target |
| F02  | System        | Shows active Employee proposal.                       | target |
| F03  | Client        | Clicks “Accept”.                                      | target |
| F04  | System        | Verifies authenticated Client owns the exchange.      | target |
| F05  | System        | Verifies exchange lifecycle allows Client acceptance. | target |
| F06  | System        | Marks active proposal and exchange as Accepted.       | target |
| F07  | System        | Returns command success without body.                 | target |
| F08  | Client/System | Refetches agreement exchange details/list.            | target |

---

## 6. Visual Implementation Flow

```text
[HTTP POST]
POST /api/agreement-exchanges/{exchangeId}/accept
        ↓
[CSRF boundary]
validate unsafe request protection
        ↓
[Auth boundary]
require authenticated Client session
        ↓
[Session context]
read current client account id from app cookie identity
        ↓
[Route binding]
exchangeId is positive long
        ↓
[Command handler]
load ClientAccount by current account id if needed
load AgreementProposalExchange by exchangeId
        ↓
[Domain]
exchange.ClientAcceptActiveProposal(client, now)
        ↓
[Persistence]
save exchange.Status = Accepted
save activeProposal.State = Accepted
        ↓
[Response]
204 No Content
```

Implementation flow table:

| Step | Layer              | Responsibility                                                                         |
| ---- | ------------------ | -------------------------------------------------------------------------------------- |
| I01  | Route / Controller | Exposes `POST /api/agreement-exchanges/{exchangeId}/accept`.                           |
| I02  | CSRF boundary      | Applies unsafe-request protection.                                                     |
| I03  | Auth boundary      | Allows authenticated Client only.                                                      |
| I04  | Session context    | Reads current client account id from claims.                                           |
| I05  | Route binding      | Binds `exchangeId` as positive `long`; no body/query validator.                        |
| I06  | Command handler    | Loads Client and AgreementProposalExchange.                                            |
| I07  | Domain             | Calls `exchange.ClientAcceptActiveProposal(client, now)`.                              |
| I08  | Domain invariant   | Checks `client.Id == ClientAccountId`, exchange status, active sender, proposal state. |
| I09  | Persistence        | Saves exchange/proposal accepted state atomically.                                     |
| I10  | API response       | Returns `204 No Content`; failures use existing ProblemDetails mapper.                 |

Guardrail:

```text
Controller/application layer must not manually set exchange status or proposal state.

Controller/application layer says:
  “Client accepts active proposal.”

Domain decides whether this is allowed and how exchange/proposal state changes.
```

---

## 7. API Contract

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/accept
```

Route:

```text
exchangeId: long, positive
```

Request body:

```text
none
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
Client already has exchangeId.

Accepted state is visible through exchange details/list after refetch.

No command DTO is needed.
```

Error responses:

```text
401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Client

404 NotFound
  exchange does not exist or is not visible to current Client

422 UnprocessableEntity
  domain lifecycle rejection:
  - exchange does not belong to current Client;
  - exchange is not AwaitingClientConfirmation;
  - active proposal was not sent by Employee;
  - active proposal is already Accepted/Superseded;
  - exchange is already Accepted/FinallyRefused;
  - exchange cannot be accepted now.

400 Bad Request
  antiforgery failure if current project CSRF boundary uses 400
```

Note:

```text
If project convention hides ownership failures as 404, application/repository can map not-owned exchange to 404.

Domain must still keep ClientAccountId guard.
```

---

## 8. Domain Rules

Current domain prerequisite:

```text
AgreementProposalExchange.ClientAccountId exists.

ClientAcceptActiveProposal already calls ownership guard.
```

Preconditions:

```text
- current actor is Client;
- exchange exists;
- exchange.ClientAccountId == client.Id;
- exchange.Status == AwaitingClientConfirmation;
- active proposal exists;
- active proposal.Sender == Employee;
- active proposal.State is awaiting/active according to current domain;
- exchange.Status is not Accepted;
- exchange.Status is not FinallyRefused.
```

Domain call:

```csharp
exchange.ClientAcceptActiveProposal(client, acceptedAt);
```

On success:

```text
- exchange.Status = AgreementExchangeStatus.Accepted;
- activeProposal.State = AgreementProposalState.Accepted;
- no new proposal version is created;
- ActiveProposalVersion remains unchanged;
- proposal count remains unchanged;
- proposal version history remains intact.
```

Timestamp note:

```text
acceptedAt is passed to the domain method for current/future audit compatibility.

First pass persistence/tests should not require AcceptedAt unless domain is explicitly extended to store it.
```

---

## 9. Application / Handler Direction

Use existing project style. Preferred command shape:

```csharp
public sealed record ClientAcceptActiveAgreementProposalCommand(
    long ExchangeId,
    long ClientAccountId)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;
```

Handler/application flow:

```text
1. Resolve current Client account id from session.
2. Load ClientAccount by current account id if domain method requires ClientAccount instance.
3. Load AgreementProposalExchange by exchangeId.
4. If missing/not visible, return NotFound-style Error.
5. Call exchange.ClientAcceptActiveProposal(client, now).
6. If domain failure, return UnitResult failure with domain errors.
7. SaveChanges.
8. Return UnitResult success.
```

Do not add per-command status enum:

```text
Do not add:
- ClientAcceptAgreementProposalCommandStatus
- AcceptAgreementProposalCommandStatus
```

Use existing:

```csharp
UnitResult<IReadOnlyList<Error>>
```

`AgreementExchangeStatus` is persisted domain state, not command execution status.

---

## 10. Validation / FluentValidation

No body validation.

Route validation:

```text
exchangeId must be positive.
```

Accepted implementation:

```text
route constraint {exchangeId:long:min(1)}
```

FluentValidation is not needed first pass.

Do not put these into FluentValidation:

```text
- exchange exists;
- Client owns exchange;
- active proposal sender;
- exchange status;
- proposal state;
- accepted/finally refused lifecycle;
- DB reads;
- mutations.
```

Those are application/domain responsibilities.

---

## 11. Security / Protection

Security layers:

```text
1. Auth role:
   endpoint requires Client.

2. Session identity:
   client id comes from authenticated account id, not request body.

3. Visibility:
   missing/not-owned exchange should not be readable/actionable by this Client.

4. Domain participant guard:
   client.Id == exchange.ClientAccountId.

5. Domain lifecycle guard:
   status/active proposal must allow Client acceptance.
```

Guardrail:

```text
UI button visibility is not authorization.

Even if UI hides the Accept button, server must enforce:
- Client role;
- ClientAccountId ownership;
- active proposal sender;
- exchange lifecycle.
```

---

## 12. Behavior Coverage

| Behavior item                                             | How slice covers it                                 | Status |
| --------------------------------------------------------- | --------------------------------------------------- | ------ |
| Client can accept own active Employee proposal            | `POST /api/agreement-exchanges/{exchangeId}/accept` | target |
| Client cannot accept another Client’s exchange            | `ClientAccountId` domain/query guard                | target |
| Client cannot accept when exchange is not awaiting Client | domain lifecycle guard                              | target |
| Client cannot accept client-authored active proposal      | active proposal sender guard                        | target |
| Accept does not create new version                        | domain rule / DB assertion                          | target |
| Active proposal becomes Accepted                          | domain state persisted                              | target |
| Exchange becomes Accepted                                 | domain state persisted                              | target |
| Accepted state visible after refetch                      | details/list read slices                            | target |
| Employee accept is not implemented here                   | out of scope                                        | target |

---

## 13. Test / Verification Plan

Primary verification: API integration tests with DB state assertions.

No unit tests by default unless reusable helper logic with branching is introduced.

API boundary:

```text
- unauthenticated accept -> 401;
- Employee calls client accept endpoint -> 403;
- Client accepts own exchange -> 204.
```

Success DB assertions:

```text
Given:
- exchange.ClientAccountId = current Client account id;
- exchange.Status = AwaitingClientConfirmation;
- active proposal sender = Employee;
- active proposal state = active/awaiting client according to current domain;
- ActiveProposalVersion = N.

When:
- Client accepts active proposal.

Expect:
- HTTP 204;
- response body empty;
- exchange.Status = Accepted;
- active proposal State = Accepted;
- ActiveProposalVersion remains N;
- proposal count unchanged;
- no new proposal version created.
```

Ownership / lifecycle failures:

```text
- Client cannot accept exchange owned by another Client -> 404 or 422 according to project mapping;
- Client cannot accept when exchange.Status = AwaitingEmployeeResponse -> 422;
- Client cannot accept when active proposal sender = Client -> 422;
- Client cannot accept already Accepted exchange -> 422;
- Client cannot accept FinallyRefused exchange -> 422;
- failed command does not change exchange status;
- failed command does not change active proposal state;
- failed command does not create proposal version.
```

Do not assert:

```text
- AcceptedAt timestamp, unless domain/persistence is explicitly extended.
```

Generated artifacts:

```text
- run OpenAPI generation;
- run API type generation;
- stage generated artifacts;
- run check:api.
```

---

## 14. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/agreement-exchanges/{exchangeId}/accept

204
400
401
403
404
422
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

## 15. Implementation Checklist

```text
[ ] confirm AgreementProposalExchange.ClientAccountId exists
[ ] confirm ClientAcceptActiveProposal checks client.Id == ClientAccountId
[ ] confirm ClientAcceptActiveProposal sets exchange.Status = Accepted
[ ] confirm ClientAcceptActiveProposal sets activeProposal.State = Accepted
[ ] add POST /api/agreement-exchanges/{exchangeId}/accept
[ ] require Client role
[ ] require CSRF
[ ] do not accept client id in body
[ ] resolve client id from session
[ ] add ClientAcceptActiveAgreementProposalCommand
[ ] add command handler
[ ] load ClientAccount if domain method requires it
[ ] load exchange by exchangeId
[ ] call exchange.ClientAcceptActiveProposal(client, now)
[ ] save exchange/proposal accepted state
[ ] return 204 No Content
[ ] add integration tests for success
[ ] add integration tests for ownership failure
[ ] add integration tests for lifecycle failures
[ ] assert no new proposal version is created
[ ] regenerate OpenAPI/types
[ ] do not implement Employee accept
[ ] do not implement counter-proposal
[ ] do not implement final refusal
[ ] do not return details/list DTO from command
```

---

## 16. Guardrail Summary

```text
Client accept is a command slice.

It is Client-only first pass.

Domain prerequisite already exists:
  AgreementProposalExchange.ClientAccountId
  ClientAcceptActiveProposal ownership guard

Use exact domain states:
  AgreementExchangeStatus.Accepted
  AgreementProposalState.Accepted

Do not use vague Finalized status unless domain is changed.

Do not create a new proposal version.

Do not create an exchange.

Do not send a counter-proposal.

Do not accept Employee actor here.

Do not add ResponsibleEmployeeId.

Do not add per-command status enums.

Do not rely on UI button visibility for security.

Do not put ownership/turn/lifecycle in FluentValidation.

Do keep ClientAccountId ownership guard in domain.

Do keep active proposal sender/lifecycle checks in domain.

Do not assert AcceptedAt first pass unless persistence is extended.

Do return 204 No Content on success.

Do refetch details/list after success.
```

Готово к имплементации: доменная часть ownership уже считается выполненной в текущем v23, поэтому остаётся API/application/tests/OpenAPI workflow.
