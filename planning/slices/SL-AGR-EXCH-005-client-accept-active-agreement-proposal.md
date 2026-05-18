# SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal

Status: implemented slice draft refactor / implementation not rechecked in this pass  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: Client server/API command slice  
Primary purpose: Client accepts the active Employee proposal in an existing AgreementProposalExchange  
Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`
* `SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details`
* `AgreementProposalExchange.ClientAccountId`
* Client auth/session
* CSRF / unsafe command protection

Implementation direction:

```text
POST /api/agreement-exchanges/{exchangeId}/accept

ClientAcceptActiveProposal(client, acceptedAt)

Success:
  204 No Content
  response body: none
```

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was not rechecked in this pass.
Runtime UI, page-flow, redirects, tests and generated artifacts are out of scope for this pass.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-13B — Client Agreement Proposal Details / Response
```

Related scenarios:

```text
SC-13A — Client Agreements / Agreement Exchange List
SC-13D — Employee Agreement Proposal Create / Send Version, as prerequisite/source of Employee proposal
SC-13E — Agreement Final Refusal, out-of-scope alternative terminal path
```

UI scenario:

```text
missing / pending dedicated UI source for Client accept action.
```

Cross-cutting behavior:

```text
CC-SEC-CSRF-001 — Unsafe Command Protection
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for paired client sidecar only
```

Data source:

```text
pending scenario-data source for accept command visible states, problem details mapping and post-accept read refresh.
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior names until source-sync files are completed.
```

Concern umbrella:

```text
none for this server command slice;
CSRF is cross-cutting security concern.
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
DOM-v001 if source-sync/domain registry is applied;
otherwise pending domain baseline.
```

Slice derivation map:

```text
pending / add row for SL-AGR-EXCH-005 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| Client accepts active Employee proposal | SC-13B pending | provided/partially provided by `AgreementProposalExchange.ClientAcceptActiveProposal` | expose Client-only API command, resolve Client actor, call domain, persist result | final positive Client decision |
| Client ownership is enforced | SC-13B pending | provided by `ClientAccountId` guard | resolve Client from session, pass actor to domain, avoid clientId in body | ownership failures may map as 404/422 per project convention |
| Active proposal must be Employee-authored | SC-13B pending | domain lifecycle/participant guard | do not bypass domain turn/author check | client cannot accept own active proposal |
| Exchange becomes Accepted | SC-13B pending | domain state transition | persist accepted exchange state atomically | visible through details/list refetch |
| Active proposal becomes Accepted | SC-13B pending | domain proposal state transition | persist active proposal state atomically | no new proposal version |
| Accept creates no new proposal version | SC-13B pending | domain lifecycle rule | command only accepts existing active version | proposal count and active version unchanged |
| Success returns no response DTO | server contract direction | API/application decision | return `204 No Content` | details/list read slices own refreshed state |
| Employee accept is out of scope | SC-13B pending | not supported first pass | reject by auth role and do not document Employee accept as current behavior | future only by explicit source/domain decision |
| Unsafe command is CSRF-protected | CC-SEC-CSRF-001-v001 | not domain behavior | apply current antiforgery boundary | full matrix belongs to cross-cutting tests |
| Runtime UI implementation is not touched | client sidecar only | not server behavior | server draft links future client sidecar; no UI code changes | docs-only archive |

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
    planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md

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
  - old draft lacked Scenario Sources;
  - old draft lacked Source / Domain / Slice Coverage Snapshot;
  - old draft lacked Implementation Sync Status;
  - old draft had test notes but not Behavior-to-Test Trace with escape/refactor risk;
  - old draft did not explicitly separate docs-only refactor from runtime/UI implementation work.

source:
  - stable behavior item IDs are not yet assigned in source registry.

implementation:
  - not checked in this pass.

UI:
  - runtime UI/page-flow/redirect implementation not touched in this pass.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection, no runtime UI implementation, no test changes and no generated artifact changes.
```

---

## 1. Scope

This slice owns:

```text
- Client-only accept active proposal API command;
- endpoint POST /api/agreement-exchanges/{exchangeId}/accept;
- Client role auth;
- CSRF / antiforgery protection for unsafe command;
- actor resolution from authenticated session;
- no clientId / employeeId / proposalId / status in request body;
- route exchangeId binding;
- loading AgreementProposalExchange by exchangeId;
- loading ClientAccount if the current domain method requires Client object;
- calling AgreementProposalExchange.ClientAcceptActiveProposal(client, acceptedAt/now);
- persisting exchange/proposal accepted state atomically;
- returning 204 No Content on success;
- mapping ownership/lifecycle failures through existing ProblemDetails/Error mapper;
- API integration test plan with DB/no-mutation assertions.
```

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/accept
```

Success:

```http
204 No Content
```

Response body:

```text
none
```

This slice does **not** create an exchange.

This slice does **not** send a counter-proposal.

This slice does **not** create a proposal version.

This slice does **not** accept Employee actor.

This slice does **not** return refreshed details/list DTO.

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Agreement exchange list read | `SL-AGR-EXCH-003` |
| Agreement exchange details read | `SL-AGR-EXCH-004` |
| Initial exchange creation | `SL-AGR-EXCH-001` |
| Counter-proposal version creation | `SL-AGR-EXCH-002` |
| Employee accept active proposal | future source/domain decision only |
| Final refusal | `SL-AGR-EXCH-006` |
| File download / binary document serving | future document/file slice |
| Document upload/storage | future document/storage slice |
| Request lifecycle mutation after accept | explicit future source/domain decision if needed |
| Runtime client button/form implementation | paired client sidecar future implementation; not this archive |
| Runtime UI/page-flow/redirect audit | separate UI/page-flow audit mode |
| Tests/runtime implementation audit | separate implemented-sync/audit mode |
| Generated OpenAPI/types update | implementation archive/tool workflow only |
| Adding `ClientAccountId` to domain | already current prerequisite / source-sync if drift found |

Important boundary:

```text
This docs-only archive updates planning drafts only.
It does not change runtime UI, server code, tests, generated artifacts, navigation or redirects.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-001
  Owns exchange creation and first Employee proposal.

SL-AGR-EXCH-002
  Owns sending later proposal versions / counter-proposals.

SL-AGR-EXCH-003
  Owns shared agreement exchange list/read projection.

SL-AGR-EXCH-004
  Owns shared agreement exchange details/read projection.

SL-AGR-EXCH-005
  Owns Client accept active proposal server command.

L2-AGR-EXCH-ACCEPT-001.client
  Owns future Client accept action/mutation planning and UI-side behavior proof.

SL-AGR-EXCH-006
  Owns Employee final refusal.

Domain / persistence
  Owns AgreementProposalExchange.ClientAccountId,
  AgreementExchangeStatus.Accepted,
  AgreementProposalState.Accepted,
  proposal version history and no-new-version invariant.

CC-SEC-CSRF-001
  Owns cross-cutting unsafe browser request protection.

OpenAPI/generated artifact workflow
  Owns regeneration/checks if API contract changes.
```

---

## 4. Scenario Flow

```text
Client opens Agreement Exchange details
        ↓
System shows active Employee proposal
        ↓
Client chooses Accept
        ↓
System resolves authenticated Client from session
        ↓
System checks Client owns exchange through domain guard
        ↓
System checks active proposal is acceptable by Client
        ↓
System marks active proposal Accepted
        ↓
System marks exchange Accepted
        ↓
Command returns 204 No Content
        ↓
Client refetches details/list and sees Accepted state
```

Scenario flow table:

| Step | Actor / System layer | User-visible / system responsibility |
|---|---|---|
| S01 | Client | Opens agreement exchange details. |
| S02 | System | Shows active Employee proposal through details read. |
| S03 | Client | Chooses Accept as final positive decision. |
| S04 | System | Resolves Client actor from authenticated session. |
| S05 | System/domain | Verifies `client.Id == exchange.ClientAccountId`. |
| S06 | System/domain | Verifies lifecycle/turn/active proposal author allow Client accept. |
| S07 | Domain | Marks exchange and active proposal Accepted. |
| S08 | API | Returns `204 No Content`. |
| S09 | Client/read slices | Refetch details/list and show Accepted state. |

Scenario meaning:

```text
Accept is a terminal positive command.
It does not create a new proposal version.
It does not upload/send a document.
```

---

## 5. Implementation Flow

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
read current Client account id from app cookie identity
        ↓
[Route binding]
exchangeId is positive long
        ↓
[Command handler]
load ClientAccount if domain requires Client instance
load AgreementProposalExchange by exchangeId
        ↓
[Domain]
exchange.ClientAcceptActiveProposal(client, now)
        ↓
[Persistence]
save exchange.Status = Accepted
save activeProposal.State = Accepted
without creating proposal version
        ↓
[Response]
204 No Content
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth, CSRF attribute/filter, route binding, response mapping.

Command handler/application:
  actor resolution input, load Client/Exchange, call domain, save atomically.

Domain:
  ownership, lifecycle, active proposal author/state, accepted state transition, no-new-version invariant.

Persistence:
  stores exchange/proposal state changes in one unit of work.

Client sidecar:
  future feature-owned mutation/button planning, not runtime implementation in this archive.
```

Guardrail:

```text
Controller/application layer must not manually set exchange status or proposal state.
Domain decides whether accept is allowed and which states change.
```

---

## 6. API Contract

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/accept
```

Route:

```text
exchangeId: long, positive
```

Auth:

```csharp
[Authorize(Roles = "Client")]
```

CSRF:

```text
required for unsafe browser command according to current antiforgery boundary.
```

Request body:

```text
none
```

DTO:

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

Failure categories:

```text
400 BadRequest
  antiforgery failure if current project CSRF boundary maps to 400

401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Client

404 NotFound
  exchange does not exist or is hidden as not visible/not owned

422 UnprocessableEntity
  domain/lifecycle problem, if project maps visible lifecycle failures this way:
  - exchange does not belong to current Client;
  - exchange is not AwaitingClientConfirmation;
  - active proposal was not sent by Employee;
  - active proposal already Accepted/Superseded;
  - exchange already Accepted/FinallyRefused;
  - exchange cannot be accepted now.

500 InternalServerError
  unexpected server failure
```

Generated artifacts:

```text
API shape changes require OpenAPI/type generation through repo tools only.
This docs-only archive does not include generated artifacts.
```

---

## 7. Validation / ProblemDetails

Route/body shape validation:

```text
- exchangeId must be a positive long.
- route constraint `{exchangeId:long:min(1)}` is enough first pass.
- request body must be absent/ignored; no body DTO.
```

FluentValidation:

```text
No command body validator is needed first pass because request body is none.
```

Domain/application validation:

```text
- exchange exists;
- Client actor exists if domain requires Client instance;
- Client owns exchange via ClientAccountId;
- exchange lifecycle allows Client accept;
- active proposal exists;
- active proposal was sent by Employee;
- active proposal can become Accepted;
- Accepted/FinallyRefused exchanges cannot continue.
```

Auth/visibility:

```text
- endpoint requires Client role;
- current Client id comes from session, not request body;
- not-owned exchange can map to 404 or domain/validation ProblemDetails according to project convention;
- domain must still guard `client.Id == ClientAccountId`.
```

CSRF:

```text
- unsafe command requires antiforgery protection;
- CSRF is not FluentValidation;
- full CSRF matrix belongs to cross-cutting tests.
```

---

## 8. Domain Behavior

Current domain prerequisite:

```text
AgreementProposalExchange.ClientAccountId exists.
ClientAcceptActiveProposal checks client.Id == ClientAccountId.
```

Domain call:

```csharp
exchange.ClientAcceptActiveProposal(client, acceptedAt);
```

Preconditions:

```text
- current actor is Client;
- exchange exists;
- exchange.ClientAccountId == client.Id;
- exchange.Status == AwaitingClientConfirmation;
- active proposal exists;
- active proposal.Sender == Employee;
- active proposal.State is active/awaiting according to current domain;
- exchange.Status is not Accepted;
- exchange.Status is not FinallyRefused.
```

On success:

```text
- exchange.Status = AgreementExchangeStatus.Accepted;
- activeProposal.State = AgreementProposalState.Accepted;
- ActiveProposalVersion remains unchanged;
- proposal count remains unchanged;
- proposal history remains intact;
- no new proposal version is created.
```

Timestamp note:

```text
acceptedAt/now can be passed to the domain for current/future audit compatibility.
First-pass client UI and tests should not require AcceptedAt unless persistence/DTO is explicitly extended.
```

Anti-terms / guardrails:

```text
Do not use vague Finalized status.
Do not add ResponsibleEmployeeId.
Do not add per-command status enums.
Do not treat UI action availability as security.
Do not put turn/lifecycle/ownership rules in FluentValidation.
```

---

## 9. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Current Client id is resolved from authenticated session. |
| Authorization/visibility | yes | Client-only endpoint; ownership through `ClientAccountId`. |
| Antiforgery / unsafe requests | yes | POST command must use current CSRF boundary. |
| Validation / ProblemDetails | yes | Route/body shape at API boundary; lifecycle/ownership via domain/application. |
| OpenAPI / generated artifacts | yes | Contract changes require generated artifact workflow, not manual edits. |
| Transaction / atomicity | yes | Exchange/proposal accepted states saved atomically. |
| No partial write | yes | Failed command must not mutate exchange/proposal or create versions. |
| Idempotency / double-submit | yes | Already Accepted second click should be rejected/no-mutation. |
| Concurrency / stale state | yes | Stale client UI cannot bypass domain lifecycle. |
| Privacy / cross-account exposure | yes | Client cannot accept another ClientAccount exchange. |
| File/document boundary | yes | Accept does not upload/download document bytes. |
| Clock/audit actor fields | limited | `acceptedAt` passed if domain supports it; no client requirement first pass. |
| Client feedback / accessibility | paired sidecar | Client draft owns future visible feedback planning. |
| Redirect/page flow | no | Out of scope for docs-only archive. |
| Testing responsibility split | yes | Server API integration + DB/no-mutation; client component/API tests later. |

---

## 10. Questions / Decisions

Original server draft did not contain stable question IDs. This refactor adds server-side IDs after preserving the original decisions and meanings.

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-005-Q001` | accepted | Is this Client-only first pass? | Yes. Endpoint requires Client role. | Employee accept remains out of scope. |
| `SL-AGR-EXCH-005-Q002` | accepted | Does command send a request body? | No body. `exchangeId` is route-only; actor comes from session. | No command DTO / no body validator. |
| `SL-AGR-EXCH-005-Q003` | accepted | Does success return a DTO? | No. Return `204 No Content`. | Details/list refetch shows Accepted state. |
| `SL-AGR-EXCH-005-Q004` | accepted | Does accept create a new proposal version? | No. It accepts active proposal and exchange only. | Tests must assert proposal count unchanged. |
| `SL-AGR-EXCH-005-Q005` | accepted | Which states are expected after accept? | `AgreementExchangeStatus.Accepted`; active `AgreementProposalState.Accepted`. | Avoid vague Finalized terminology. |
| `SL-AGR-EXCH-005-Q006` | accepted | Is `ClientAccountId` added here? | No. It is current prerequisite / domain guard. | Do not duplicate domain migration in this slice. |
| `SL-AGR-EXCH-005-Q007` | accepted | Is Employee accept implemented? | No. Future only with explicit source/domain decision. | Auth remains Client-only. |
| `SL-AGR-EXCH-005-Q008` | accepted | Is UI button visibility authorization? | No. Server/domain remain authoritative. | Security tests must use server boundary. |
| `SL-AGR-EXCH-005-Q009` | accepted | Is AcceptedAt required in UI/tests? | No first pass unless persistence/DTO is explicitly extended. | Avoid brittle timestamp assertions. |
| `SL-AGR-EXCH-005-Q010` | accepted | Are ownership/lifecycle rules FluentValidation? | No. Domain/application own them. | Validator stays absent/minimal. |
| `SL-AGR-EXCH-005-Q011` | accepted | Does command mutate request lifecycle? | No first pass. | Request lifecycle after accept is separate/future if needed. |
| `SL-AGR-EXCH-005-Q012` | accepted | Are per-command status enums needed? | No. Use existing `Error`/`ProblemDetails` mapping and domain state enum. | Avoid command-status enum proliferation. |

---

## 11. Extension / Change Points

```text
- Optional confirmation UX belongs to client sidecar implementation.
- AvailableActions DTO may later come from details read slice.
- AcceptedAt audit/display can be added only if domain/persistence/DTO explicitly supports it.
- Employee accept remains future-only and needs source/domain decision.
- Request lifecycle mutation after accept is not assumed; add a separate slice/source decision if needed.
- Concurrency handling can be strengthened with rowversion/ETag later if project needs it.
```

---

## 12. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Client can accept own active Employee proposal | covered | Client-only POST command calls domain accept. |
| Client cannot accept another ClientAccount exchange | covered | session actor + `ClientAccountId` guard. |
| Client cannot accept when exchange is not awaiting Client | covered | domain lifecycle guard. |
| Client cannot accept client-authored active proposal | covered | active proposal sender/turn guard. |
| Accept marks active proposal Accepted | covered | domain transition persisted. |
| Accept marks exchange Accepted | covered | domain transition persisted. |
| Accept creates no proposal version | covered | no-new-version invariant and tests. |
| Command returns no response body | covered | `204 No Content`. |
| Accepted state visible after refetch | supported | read slices own details/list. |
| Employee accept | out of scope | future only. |
| Counter-proposal | out of scope | `SL-AGR-EXCH-002`. |
| Final refusal | out of scope | `SL-AGR-EXCH-006`. |
| Runtime client button/form | out of archive scope | paired client draft only; no UI code changes. |
| Page-flow/redirect audit | out of scope | future UI/page-flow mode. |

---

## 13. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Client accepts own active Employee proposal | API returns 204 and persisted exchange/proposal become Accepted | API integration + DB assertion | Client auth fixture, seeded exchange/proposal, HTTP POST, DB read | Low: persisted state and response are asserted | Low: endpoint/domain internals can refactor if behavior stays | `AcceptAgreementProposal_ClientAcceptsOwnEmployeeProposal_ReturnsNoContentAndPersistsAcceptedState` |
| Success has no response DTO | HTTP 204 with empty body | API integration / contract test | HTTP POST, response assertion | Low | Low | same success test / contract assertion |
| Accept creates no proposal version | proposal count unchanged and ActiveProposalVersion unchanged | API integration + DB assertion | DB snapshot before/after | Low if count/version asserted | Low/Medium: schema helper changes may affect setup | `AcceptAgreementProposal_DoesNotCreateProposalVersion` |
| Client cannot accept another Client's exchange | not-owned exchange returns 404 or mapped domain problem and no mutation | API integration + no-mutation assertion | two Client fixtures, HTTP POST, DB snapshot | Low if not-owned status and unchanged state asserted | Low | `AcceptAgreementProposal_OtherClientExchange_ReturnsFailureAndDoesNotMutate` |
| Employee cannot call Client accept endpoint | Employee session rejected | API integration | Employee auth fixture, HTTP POST | Low | Low | `AcceptAgreementProposal_Employee_ReturnsForbidden` |
| Unauthenticated actor rejected | unauthenticated request returns 401 | API integration | no auth cookie, HTTP POST | Low | Low | `AcceptAgreementProposal_Unauthenticated_ReturnsUnauthorized` |
| Wrong exchange lifecycle rejected | AwaitingEmployeeResponse / Accepted / FinallyRefused state fails and no mutation | API integration + no-mutation assertion | seeded lifecycle states, HTTP POST, DB snapshot | Low if failed state unchanged asserted | Low/Medium | `AcceptAgreementProposal_InvalidLifecycle_ReturnsValidationProblemAndDoesNotMutate` |
| Client-authored active proposal rejected | command fails and proposal/exchange unchanged | API integration + DB assertion | seed active proposal Sender=Client | Low | Low/Medium | `AcceptAgreementProposal_WhenActiveProposalFromClient_ReturnsFailureAndDoesNotMutate` |
| CSRF protection applies | unsafe POST without token is rejected | API integration smoke | Client session, missing token/header, HTTP POST | Medium if only status asserted; full matrix belongs elsewhere | Low | command-family CSRF smoke or `CC-SEC-CSRF-001` test |
| Generated contract remains current | OpenAPI shows POST accept 204/no body | generated artifact check | repo generation/check commands | Medium; contract check does not prove behavior | Low | `check:api` / OpenAPI artifact check |

### API boundary tests

```text
- unauthenticated accept returns 401;
- Employee role returns 403;
- Client success returns 204 No Content;
- invalid route id follows project route/model binding convention;
- missing exchange returns 404.
```

### Success DB assertions

```text
Given:
- exchange.ClientAccountId = current Client account id;
- exchange.Status = AwaitingClientConfirmation;
- active proposal sender = Employee;
- active proposal state = active/awaiting client according to current domain;
- ActiveProposalVersion = N.

Expect after POST:
- HTTP 204;
- response body empty;
- exchange.Status = Accepted;
- active proposal State = Accepted;
- ActiveProposalVersion remains N;
- proposal count unchanged;
- no new proposal version created.
```

### No-mutation failures

```text
- not-owned exchange does not change status/proposal state;
- wrong lifecycle does not change status/proposal state;
- client-authored active proposal does not change status/proposal state;
- already Accepted/FinallyRefused exchange remains unchanged;
- failed command does not create proposal version.
```

### What not to test here

```text
- Employee accept behavior;
- counter-proposal behavior;
- final refusal behavior;
- file upload/download;
- runtime UI button placement;
- redirect/page-flow behavior;
- AcceptedAt timestamp unless domain/persistence explicitly stores it;
- repository mock call order as primary proof.
```

---

## 14. Implementation Direction / Current Refactor Checklist

```text
[ ] Confirm AgreementProposalExchange.ClientAccountId exists or mark implementation drift.
[ ] Confirm ClientAcceptActiveProposal checks client.Id == ClientAccountId.
[ ] Confirm ClientAcceptActiveProposal sets exchange.Status = Accepted.
[ ] Confirm ClientAcceptActiveProposal sets activeProposal.State = Accepted.
[ ] Confirm ClientAcceptActiveProposal does not create proposal version.
[ ] Confirm endpoint exists as POST /api/agreement-exchanges/{exchangeId}/accept or mark route drift.
[ ] Confirm endpoint requires Client role.
[ ] Confirm endpoint requires CSRF for unsafe browser request.
[ ] Confirm command accepts no request body.
[ ] Confirm client id is resolved from session, not body.
[ ] Confirm success is 204 No Content.
[ ] Confirm response body is none.
[ ] Confirm failures map through existing Error/ProblemDetails conventions.
[ ] Confirm API integration success test asserts persisted Accepted state.
[ ] Confirm failure tests assert no mutation and no new proposal version.
[ ] Confirm OpenAPI/generated artifacts are updated if contract changed.
```

This pass did not perform implementation verification.

---

## 15. Historical Client Notes / Future Client Sidecar

Paired client sidecar:

```text
planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
```

Future client sidecar should own:

```text
- Client agreement exchange details action placement;
- feature-owned API wrapper:
  features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.ts;
- mutation hook;
- Accept button/action UI;
- optional confirmation UX;
- pending/disabled/error/success feedback;
- details/list invalidation/refetch;
- accessibility and component/API/model tests.
```

This server draft does not implement runtime client UI.

UI/page-flow/redirect audit remains separate later work.

---

## 16. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/agreement-exchanges/{exchangeId}/accept
responses:
  204 No Content
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
npm.cmd --prefix energymanagement.client run generate:api-types
npm.cmd run check:api
```

This docs-only archive does not include generated artifacts and does not claim they are current.

---

## 17. Dependent / Follow-up Slices

```text
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
L2-AGR-EXCH-ACCEPT-001.client — Client accept action sidecar
```

Separate later work:

```text
- runtime implementation audit;
- runtime UI/client implementation;
- page flow / redirects audit;
- source registry / behavior item ID sync.
```

---

## 18. Guardrail Summary

```text
Client accept is a Client-only command slice.
Accept returns 204 No Content.
Response body is none.
No request body is sent.
Client id comes from authenticated session.
CSRF is required for unsafe POST.
Domain owns ownership/turn/lifecycle checks.
AgreementProposalExchange.ClientAccountId is required and preserved.
Do not add ResponsibleEmployeeId.
Do not add per-command status enums.
Do not put ownership/turn/lifecycle in FluentValidation.
Do not create a proposal version.
Do not create an exchange.
Do not send a counter-proposal.
Do not final-refuse here.
Do not implement Employee accept first pass.
Do not rely on UI button visibility for security.
Do not assert AcceptedAt first pass unless explicitly added to persistence/DTO.
Runtime implementation, tests, generated artifacts, UI/page-flow/redirects were not checked or changed in this pass.
```
