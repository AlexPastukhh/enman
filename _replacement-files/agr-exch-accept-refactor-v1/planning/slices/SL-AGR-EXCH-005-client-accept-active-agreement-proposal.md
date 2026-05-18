# SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal

Status: implemented slice draft refactor / implementation not rechecked in this pass  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: Client backend/API command slice  
Primary purpose: Client accepts the active Employee proposal in an existing AgreementProposalExchange  
Draft refactor mode: docs-only; runtime implementation was not rechecked in this pass

Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`
* `SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details`
* `AgreementProposalExchange.ClientAccountId`
* Client auth/session
* CSRF / unsafe command protection
* agreement exchange persistence

Implementation direction:

```text
POST /api/agreement-exchanges/{exchangeId}/accept
→ 204 No Content

Domain call:
  AgreementProposalExchange.ClientAcceptActiveProposal(client, acceptedAt)
```

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was not rechecked in this pass.
Client UI/page flow and redirects are out of scope for this server draft pass.

No behavior decision from the previous draft was intentionally removed.
The old draft's endpoint, Client-only boundary, no-body/204 contract,
ClientAccountId ownership guard, no-new-version rule, CSRF requirement,
ProblemDetails mapping direction, OpenAPI workflow and test expectations are preserved.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-13B — Client Agreement Proposal Details / Response
```

Related scenarios:

```text
SC-13A — Agreement Exchange List
SC-13D — Employee Agreement Proposal Create / Send Version, as prerequisite context
SC-13E — Agreement Final Refusal, as mutually exclusive command context
SC-14 — Agreement Documents, document reference context only
```

UI scenario:

```text
missing / pending dedicated UI source for Client Accept action.
Client behavior is currently carried by the paired client sidecar:
  L2-AGR-EXCH-ACCEPT-001.client
```

Cross-cutting behavior:

```text
CC-SEC-CSRF-001 — Unsafe Command Protection
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for paired client sidecar only
```

Data source:

```text
pending scenario-data source for accepted agreement exchange state and proposal state.
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior labels until source-sync files are completed.
```

Concern umbrella:

```text
CSRF is a cross-cutting security concern.
No dedicated umbrella concern is created for accept itself.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-13B: pending / v000 if source registry is applied
SC-13A: pending / v000 if source registry is applied
CC-SEC-CSRF-001: v001 if source registry is applied
```

Domain baseline:

```text
AgreementProposalExchange.ClientAccountId exists.
ClientAcceptActiveProposal checks client ownership and lifecycle.
AgreementExchangeStatus.Accepted and AgreementProposalState.Accepted are the accepted target states.
```

Slice derivation map:

```text
pending / add row for SL-AGR-EXCH-005 during source-sync map update.
```

Coverage snapshot:

| Behavior label / provisional behavior | Source/version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| `AGR-ACCEPT-B01` Client accepts own active Employee proposal | SC-13B pending | domain method owns state transition | expose Client-only POST command and call domain method | no request body |
| `AGR-ACCEPT-B02` Client cannot accept another Client's exchange | SC-13B pending | `ClientAccountId` guard | resolve current client from session; load visible exchange; preserve domain guard | UI visibility is not security |
| `AGR-ACCEPT-B03` Accept requires AwaitingClientConfirmation | SC-13B pending | domain lifecycle guard | return mapped ProblemDetails on lifecycle failure | do not implement in FluentValidation |
| `AGR-ACCEPT-B04` Active proposal must be Employee-authored | SC-13B pending | domain active proposal guard | rely on domain; add integration coverage | Client-authored proposal cannot be accepted by Client |
| `AGR-ACCEPT-B05` Accept sets exchange/proposal accepted states | SC-13B pending | domain state transition | persist aggregate and verify DB/read state | exact states are `Accepted` |
| `AGR-ACCEPT-B06` Accept creates no new proposal version | SC-13B pending | domain invariant | assert proposal count/version unchanged | no counter-proposal here |
| `AGR-ACCEPT-B07` Accept has no response DTO | slice/API direction | API contract | return `204 No Content` | read state comes from refetch |
| `AGR-ACCEPT-B08` Unsafe command is CSRF-protected | CC-SEC-CSRF-001 | framework/cross-cutting | require antiforgery boundary | use existing CSRF normalization |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync / implementation-not-rechecked
```

Implemented files:

```text
server:
  not rechecked in this pass

client:
  paired client draft refactored in this archive:
    planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md

tests:
  not rechecked in this pass
```

Checked against:

```text
source versions:
  pending source-sync registry

domain baseline:
  current draft/domain direction from existing slice text

slice derivation map version:
  pending
```

Known drift corrected by this refactor:

```text
docs:
  - old draft lacked Source / Domain / Slice Coverage Snapshot;
  - old draft lacked Implementation Sync Status;
  - old draft had Behavior Coverage and Test Plan but no Behavior-to-Test Trace;
  - old draft had client notes in server draft without a dedicated historical/future-client section;
  - old draft was implementation-ready but not marked as implemented-slice sync/refactor.

source:
  - stable behavior item IDs are still not assigned in source registry.

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
- Client-only accept active agreement proposal command;
- one endpoint:
  POST /api/agreement-exchanges/{exchangeId}/accept;
- no request body;
- no response DTO;
- current ClientAccount derived from authenticated app session;
- loading ClientAccount if domain method requires the entity;
- loading AgreementProposalExchange by exchangeId;
- preserving ClientAccountId ownership protection;
- calling AgreementProposalExchange.ClientAcceptActiveProposal(client, now);
- persisting exchange accepted state and active proposal accepted state atomically;
- returning 204 No Content on success;
- mapping failures through existing Error/ProblemDetails mapping;
- integration verification for auth, CSRF, ownership, lifecycle, success and no-new-version behavior.
```

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/accept
```

This slice is only:

```text
Client accepts the current active Employee proposal.
```

It is not counter-proposal, final refusal, document upload, request review approve or agreement details read.

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Agreement exchange list | `SL-AGR-EXCH-003` |
| Agreement exchange details read | `SL-AGR-EXCH-004` |
| Initial exchange creation | `SL-AGR-EXCH-001` |
| Counter-proposal version creation | `SL-AGR-EXCH-002` |
| Employee accept active proposal | future slice only if scenario/domain requires it |
| Final refusal | `SL-AGR-EXCH-006` |
| File download / binary document serving | future document/file slice |
| Document upload/storage | `CC-DOC-001` / future hardening slices |
| Client UI button/action implementation | `L2-AGR-EXCH-ACCEPT-001.client` |
| Adding `ClientAccountId` to domain | already current prerequisite |
| Request lifecycle mutation after accept | separate future decision if needed |
| AcceptedAt display/read-model requirement | future enhancement unless details DTO explicitly exposes it |
| Error-mapping cleanup | separate cross-cutting/API cleanup if current mapper lacks precision |

Important boundary:

```text
Accepting a proposal is not the same as sending a counter-proposal.
Accept creates no new AgreementProposalVersion.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-001
  Owns creation of exchange and initial Employee proposal.

SL-AGR-EXCH-002
  Owns Client/Employee counter-proposal version sending.

SL-AGR-EXCH-003
  Owns agreement exchange list read.

SL-AGR-EXCH-004
  Owns agreement exchange details read.

SL-AGR-EXCH-005
  Owns Client accept active proposal server command.

L2-AGR-EXCH-ACCEPT-001.client
  Owns Client Accept action/button/mutation in the details UI.

SL-AGR-EXCH-006
  Owns Employee final refusal command.

CC-SEC-CSRF-001
  Owns unsafe command protection behavior.

CC-DOC-001
  Owns upload/storage support for proposal documents, not accept.
```

---

## 4. Visual Scenario Flow

```text
Client opens Agreement Exchange details
        ↓
System shows active Employee proposal
        ↓
Client chooses Accept
        ↓
System verifies authenticated Client owns the exchange
        ↓
System verifies exchange lifecycle allows Client acceptance
        ↓
System verifies active proposal was sent by Employee
        ↓
System accepts active proposal and exchange
        ↓
Command succeeds with 204 No Content
        ↓
Client refetches agreement exchange details/list
        ↓
Accepted state is visible in reads
```

Scenario flow table:

| Step | Actor/system | Behavior | Status |
|---|---|---|---|
| F01 | Client | Opens agreement exchange details. | target/current draft |
| F02 | System | Shows active Employee proposal. | target/current draft |
| F03 | Client | Chooses Accept. | target/current draft |
| F04 | System | Verifies authenticated Client owns the exchange. | target/current draft |
| F05 | System | Verifies exchange lifecycle allows Client acceptance. | target/current draft |
| F06 | System | Verifies active proposal was sent by Employee. | target/current draft |
| F07 | System | Marks exchange and active proposal as Accepted. | target/current draft |
| F08 | System | Returns 204 No Content. | target/current draft |
| F09 | Client/System | Refetches details/list and shows Accepted state. | paired client sidecar |

---

## 5. Visual Implementation Flow

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
exchangeId must be positive long
        ↓
[Command/Application]
load ClientAccount if domain method requires entity
load AgreementProposalExchange by exchangeId
        ↓
[Domain]
exchange.ClientAcceptActiveProposal(client, now)
        ↓
[Persistence]
save exchange.Status = Accepted
save activeProposal.State = Accepted
preserve ActiveProposalVersion and proposal count
        ↓
[Response]
204 No Content
```

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Route / Controller | Exposes `POST /api/agreement-exchanges/{exchangeId}/accept`. |
| I02 | CSRF boundary | Applies unsafe-request protection. |
| I03 | Auth boundary | Allows authenticated Client only. |
| I04 | Session context | Reads current client account id from claims/session. |
| I05 | Route binding | Binds `exchangeId` as positive `long`; no body/query validator. |
| I06 | Command handler | Loads Client and AgreementProposalExchange. |
| I07 | Domain | Calls `exchange.ClientAcceptActiveProposal(client, now)`. |
| I08 | Domain invariant | Checks `client.Id == ClientAccountId`, exchange status, active sender and proposal state. |
| I09 | Persistence | Saves accepted state atomically. |
| I10 | API response | Returns `204 No Content`; failures use existing ProblemDetails mapper. |

Guardrail:

```text
Controller/application layer must not manually set exchange status or proposal state.

Controller/application layer says:
  Client accepts active proposal.

Domain decides whether this is allowed and how exchange/proposal state changes.
```

---

## 6. API Contract

| Endpoint | Method | Body | Success | Statuses |
|---|---|---|---|---|
| `/api/agreement-exchanges/{exchangeId}/accept` | POST | none | `204 No Content` | 204, 400, 401, 403, 404, 422, 500 |

Route:

```text
exchangeId: long, positive
```

Request body:

```text
none
```

Success response:

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
No command response DTO is needed.
```

Expected error responses:

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
  antiforgery failure if current project CSRF boundary uses 400 ProblemDetails
```

Ownership mapping note:

```text
If project convention hides ownership failures as 404, application/repository can map not-owned exchange to 404.
Domain must still keep ClientAccountId guard.
```

---

## 7. Validation / ProblemDetails

DTO validation:

```text
none; no request body.
```

Route validation:

```text
exchangeId must be positive.
```

Accepted implementation:

```text
route constraint {exchangeId:long:min(1)}
```

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

These are application/domain responsibilities.

ProblemDetails direction:

```text
Use existing Error/ProblemDetails mapping.
Do not add per-command HTTP status enums.
Do not add legacy ServerValidationError entries for lifecycle/domain failures.
```

CSRF failure direction:

```text
Use shared antiforgery normalization from the cross-cutting security concern.
CSRF failure is not FluentValidation and not DTO validation.
```

---

## 8. Domain Behavior

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

Use existing project result/error style.

Preferred command shape:

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
4. If missing/not visible, return NotFound-style Error according to project convention.
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

## 10. Cross-Cutting Concerns / Considerations

| Concern | Decision / responsibility |
|---|---|
| Auth/session | Require Client role; derive ClientAccountId from authenticated session. |
| Authorization/ownership | ClientAccountId guard is domain/application responsibility; UI button visibility is not authorization. |
| CSRF / unsafe request | Required for POST; use shared antiforgery behavior. |
| Validation | No body validator; route positive constraint only. Lifecycle is domain/application. |
| ProblemDetails | Use existing Error/ProblemDetails mapping; no per-command status enum. |
| No-mutation failure safety | Failed command must not change exchange/proposal state or create versions. |
| Concurrency/stale state | If another action changes exchange before accept, domain rejects and client refetches. |
| OpenAPI/generated artifacts | API contract changes require generated OpenAPI/types. |
| Client feedback | Owned by paired client sidecar. |
| Testing | API integration tests with DB state assertions are primary proof. |

---

## 11. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-005-Q001` | accepted | Who can accept? | Client only first pass. | Employee accept out of scope. |
| `SL-AGR-EXCH-005-Q002` | accepted | Does accept have a body? | No request body. | No DTO/validator first pass. |
| `SL-AGR-EXCH-005-Q003` | accepted | Success response? | `204 No Content`. | Read state comes from refetch. |
| `SL-AGR-EXCH-005-Q004` | accepted | Does accept create a proposal version? | No. | Proposal count/version unchanged. |
| `SL-AGR-EXCH-005-Q005` | accepted | Which states are set? | `AgreementExchangeStatus.Accepted`, `AgreementProposalState.Accepted`. | Avoid vague `Finalized` wording. |
| `SL-AGR-EXCH-005-Q006` | accepted | Where is ownership enforced? | Domain guard with `ClientAccountId`, plus application visibility/loading. | UI is not security. |
| `SL-AGR-EXCH-005-Q007` | accepted | Does Client send clientId? | No. Client id comes from session. | Prevents spoofing. |
| `SL-AGR-EXCH-005-Q008` | accepted | Should handler add per-command status enum? | No. Use Result/Error mapping. | Avoid enum noise. |
| `SL-AGR-EXCH-005-Q009` | accepted | Should FluentValidation check lifecycle? | No. Domain/application check lifecycle. | Keep validators boundary-only. |
| `SL-AGR-EXCH-005-Q010` | future review | Should AcceptedAt be stored/read? | Not required first pass. | Future audit/read enhancement. |
| `SL-AGR-EXCH-005-Q011` | future review | Should ownership failures be 404 or 422? | Follow current mapper/convention; prefer hiding not-owned as not found if supported. | Error mapping may be cleanup. |
| `SL-AGR-EXCH-005-Q012` | future review | Employee accept? | Not current direction. Add only with explicit scenario/domain decision. | Avoid accidental feature creep. |

---

## 12. Extension / Change Points

| Change point | Current direction | Future owner |
|---|---|---|
| AcceptedAt persistence/display | Not required first pass | future audit/read enhancement |
| Employee accept | Out of scope | future scenario/domain slice if needed |
| Conflict mapping `409` vs lifecycle `422` | Keep current mapper | error-mapping cleanup |
| Action availability DTO | Optional read-side enhancement | `SL-AGR-EXCH-004` / client details sidecar |
| Acceptance consequences for documents/request | No request lifecycle mutation in this slice | future agreement document/finalization slice |
| Concurrency token/optimistic lock | Not first pass | future hardening |

---

## 13. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How slice covers it | Status |
|---|---|---|
| Client can accept own active Employee proposal | `POST /api/agreement-exchanges/{exchangeId}/accept` calls domain method | target/current draft |
| Client cannot accept another Client's exchange | session-derived client id + `ClientAccountId` guard | target/current draft |
| Client cannot accept when exchange is not awaiting Client | domain lifecycle guard | target/current draft |
| Client cannot accept client-authored active proposal | active proposal sender guard | target/current draft |
| Accept does not create new version | domain invariant / DB assertion | target/current draft |
| Active proposal becomes Accepted | domain state persisted | target/current draft |
| Exchange becomes Accepted | domain state persisted | target/current draft |
| Accepted state visible after refetch | details/list read slices | paired slices |
| Employee accept is not implemented here | out of scope | target/current draft |
| No body/DTO success | API returns 204 | target/current draft |

---

## 14. Test / Verification Plan

Primary verification: API integration tests with DB state assertions.

Do not add unit tests by default.

Unit tests are allowed only if this slice introduces reusable helper logic with non-trivial branching. Even then, keep unit tests focused on that helper only.

API boundary:

```text
- unauthenticated accept -> 401;
- Employee calls client accept endpoint -> 403;
- Client accepts own exchange -> 204;
- missing/invalid CSRF -> 400 antiforgery ProblemDetails according to project CSRF normalization.
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

## 15. Behavior-to-Test Trace

| Behavior item | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Client-only endpoint | non-Client rejected | integration | `[Authorize(Roles = "Client")]` + auth setup | Employee/anonymous can accept | role attributes moved | unauthenticated 401, Employee 403 |
| CSRF protected POST | unsafe POST without token rejected | integration | RequireAntiforgeryToken / shared CSRF filter | CSRF bypass | controller refactor omits attribute | missing CSRF -> 400 ProblemDetails |
| Current Client from session | no clientId accepted from body | integration/code review | claims/session context | spoofed client id | future DTO accidentally adds id | success/failure tests with mixed clients |
| Own exchange accepted | exchange and active proposal become Accepted | integration DB state | domain method + SaveChanges | command returns success without mutation | repository/service refactor | success DB assertion |
| Other client's exchange rejected | no state change for non-owner | integration DB state | query visibility + domain ClientAccountId guard | cross-account mutation | query filter/domain guard removed | mixed-client ownership test |
| Wrong lifecycle rejected | 422/no mutation | integration | domain lifecycle guard | invalid accept state | status enum changes | AwaitingEmployeeResponse/Accepted/FinallyRefused tests |
| Client-authored active proposal rejected | 422/no mutation | integration/domain | active proposal sender guard | Client accepts own counter-proposal | method refactor | client-authored active proposal failure test |
| No new proposal version | proposal count unchanged | integration DB state | domain accept mutates active proposal only | accidental version creation | future proposal service reuse | proposal count and ActiveProposalVersion unchanged |
| No response DTO | HTTP 204 empty body | integration/contract | controller returns NoContent | client expects DTO | controller refactor | success response body empty |

---

## 16. Implementation Direction / Current Refactor Checklist

```text
[ ] confirm AgreementProposalExchange.ClientAccountId exists
[ ] confirm ClientAcceptActiveProposal checks client.Id == ClientAccountId
[ ] confirm ClientAcceptActiveProposal sets exchange.Status = Accepted
[ ] confirm ClientAcceptActiveProposal sets activeProposal.State = Accepted
[ ] add/confirm POST /api/agreement-exchanges/{exchangeId}/accept
[ ] require Client role
[ ] require CSRF
[ ] do not accept client id in body
[ ] resolve client id from session
[ ] add/confirm ClientAcceptActiveAgreementProposalCommand
[ ] add/confirm command handler
[ ] load ClientAccount if domain method requires it
[ ] load exchange by exchangeId
[ ] call exchange.ClientAcceptActiveProposal(client, now)
[ ] save exchange/proposal accepted state
[ ] return 204 No Content
[ ] map failures through existing Error/ProblemDetails mapping
[ ] add integration tests for success
[ ] add integration tests for ownership failure
[ ] add integration tests for lifecycle failures
[ ] assert no new proposal version is created
[ ] regenerate OpenAPI/types if endpoint/contract changes
[ ] do not implement Employee accept
[ ] do not implement counter-proposal
[ ] do not implement final refusal
[ ] do not return details/list DTO from command
```

---

## 17. Historical Client Notes / Future Client Sidecar

Paired client sidecar:

```text
L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal
```

Client ownership:

```text
- Client details action slot only;
- Accept button/action/mutation;
- optional confirmation;
- pending/error/success feedback;
- details/list invalidation after success;
- no optimistic Accepted state before server success.
```

Server draft must not implement UI details or page redirects.

The server contract remains:

```text
POST /api/agreement-exchanges/{exchangeId}/accept
no body
204 No Content
```

---

## 18. OpenAPI / Generated Artifacts

Expected OpenAPI addition / confirmation:

```text
POST /api/agreement-exchanges/{exchangeId}/accept
responses:
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

git add .\Shared\openapi.json .\energymanagement.client\src\sharedpi\generated\openapi-types.ts

npm.cmd run check:api
```

Generated artifacts must come from repo commands, not manual edits.

---

## 19. Dependent / Follow-up Slices

```text
L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
L2-AGR-EXCH-FINAL-REFUSE-001.client — Employee Final Refuse Agreement Exchange
future agreement document finalization/download/read slices, if needed
future AcceptedAt/audit/read enhancement, if needed
```

---

## 20. Guardrail Summary

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
