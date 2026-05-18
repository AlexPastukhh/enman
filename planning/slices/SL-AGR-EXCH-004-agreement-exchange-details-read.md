# SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details

Status: implemented slice draft refactor / implementation not rechecked in this pass  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: shared server read endpoint + shared details read model  
Primary purpose: Client or Employee reads one visible `AgreementProposalExchange` with request summary, active proposal and full proposal history  
Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* `SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version`
* `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`
* `AgreementProposalExchange.ClientAccountId`
* Client/Employee auth/session
* agreement exchange persistence/read projection

Implementation direction:

```text
GET /api/agreement-exchanges/{exchangeId}

Client branch:
  read exchange only when exchange.ClientAccountId == current ClientAccountId

Employee branch:
  first pass: any active Employee can see/service agreement exchanges
```

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was not rechecked in this pass.
Client UI/page flow, redirect audit, runtime UI refactor, tests and generated artifacts are out of scope for this pass.
```

---

## 0. Scenario Sources

Business scenarios:

```text
SC-13B — Client Agreement Proposal Details / Response
SC-13C — Employee Agreements
SC-13D — Employee Agreement Proposal Create / Send Version, as source of proposal history semantics
SC-13E — Agreement Final Refusal, as related final-state/details context
```

Related scenarios:

```text
SC-13A — Client/Employee Agreement Exchange List
SC-14 — Agreement Documents, for AgreementDocumentRef metadata references only
```

UI scenario status:

```text
missing / pending dedicated UI source for Client and Employee agreement exchange details pages
```

Cross-cutting behavior:

```text
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for paired/future client sidecar
```

Data source:

```text
pending scenario-data source for details DTO fields, request summary fields, proposal history rows and document reference display fields
```

Behavior items status:

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
SC-13B: pending / v000 if source registry is applied
SC-13C: pending / v000 if source registry is applied
SC-13D: pending / v000 if source registry is applied
SC-13E: pending / v000 if source registry is applied
SC-14: pending / v000 if source registry is applied
```

Domain baseline:

```text
DOM-v001 if source-sync/domain registry is applied;
otherwise pending domain baseline.
```

Slice derivation map:

```text
pending / add row for SL-AGR-EXCH-004 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| Client opens own exchange details | SC-13B pending | depends on persisted `ClientAccountId` | filter read projection by current ClientAccountId | not UI-only access |
| Client cannot open another client's exchange | SC-13B pending | depends on persisted `ClientAccountId` | return not-found/not-visible result | avoid cross-account exposure |
| Employee opens visible exchange details | SC-13C pending | first-pass policy, not exchange-level domain owner | validate active Employee if supported and return employee-visible exchange | any active Employee first pass |
| No `ResponsibleEmployeeId` guard | SC-13C pending | domain/access policy decision | do not filter details by ResponsibleEmployeeId | proposal authors track actual Employee per version |
| Details show exchange status | SC-13B/13C pending | persisted exchange state | project status in details DTO | list remains summary-only |
| Details show request summary | SC-13B/13C pending | read projection responsibility | return compact request context | not full request details DTO |
| Details show active proposal | SC-13B/13D pending | exchange active proposal state | project active proposal, version, sender and document ref | command rules remain elsewhere |
| Details show full proposal history | SC-13B/13D pending | exchange owns proposal versions | return all proposal versions ordered by version | list slice excludes full history |
| Details show document references, not bytes | SC-14 pending | `AgreementDocumentRef` metadata reference | return metadata/reference DTO only | download/storage slice owns bytes |
| Details read does not mutate lifecycle | SC-13B/13C pending | read-only projection | do not call proposal send/accept/final-refuse methods | no side effects on GET |
| UI may show action slots but server commands enforce rules | SC-13B/13E pending | command slices/domain own lifecycle | details DTO can support future UI, but no command security here | UI is not authorization |

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
    planning/slices/l2/L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md

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
  - old draft lacked Scenario Sources section;
  - old draft lacked Source / Domain / Slice Coverage Snapshot;
  - old draft lacked Implementation Sync Status;
  - old draft had behavior coverage and verification notes, but not current Behavior-to-Test Trace with escape/refactor risk;
  - old draft used "Visual Scenario Flow" wording in a server/API read draft;
  - old draft mixed server and client/UI action-slot notes without explicitly preserving page-flow/UI audit as out of scope.

source:
  - stable behavior item IDs are not yet assigned in source registry.

implementation:
  - not checked in this pass.

UI:
  - runtime UI, page-flow and redirect behavior are not touched in this pass.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection, no tests inspection, no generated artifact regeneration and no UI/redirect flow inspection.
```

---

## 1. Scope

This slice owns:

```text
- one shared details endpoint for Client and Employee;
- current role and current account id derived from app cookie identity;
- Client branch filters by AgreementProposalExchange.ClientAccountId;
- Employee branch uses first-pass employee visibility policy;
- one details read model / response DTO;
- exchange status projection;
- compact request summary projection;
- active proposal projection;
- full proposal version history projection;
- proposal sender and sender id projection per proposal version;
- AgreementDocumentRef metadata projection, not binary file bytes;
- currentActorSide or equivalent UI derivation field if implemented by contract;
- 200 OK read response;
- not-found/not-visible behavior;
- no lifecycle mutation;
- API integration test plan with visibility, projection and no-mutation assertions.
```

Endpoint:

```http
GET /api/agreement-exchanges/{exchangeId}
```

Auth:

```csharp
[Authorize(Roles = "Client,Employee")]
```

First-pass response:

```text
AgreementExchangeDetailsResponseDto
```

This slice does **not** send proposal versions.

This slice does **not** start agreement exchange.

This slice does **not** accept/refuse agreement exchange.

This slice does **not** download or upload files.

This slice does **not** introduce `ResponsibleEmployeeId`.

This slice does **not** own client page layout, runtime UI or redirects.

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Agreement exchange list / compact list rows | `SL-AGR-EXCH-003` |
| Start exchange with first Employee proposal | `SL-AGR-EXCH-001` |
| Send counter-proposal / next proposal version | `SL-AGR-EXCH-002` |
| Client accept active proposal | `SL-AGR-EXCH-005` |
| Final refusal | `SL-AGR-EXCH-006` |
| Binary file download / serving | future document/file slice |
| Document upload/storage adapter | future document/storage slice / `CC-DOC-*` family |
| Complex employee assignment / department visibility | future visibility slice |
| `ResponsibleEmployeeId` authorization guard | explicitly not this slice |
| Full request details DTO merge | request/details slices or page composition, not this exchange details endpoint |
| Client page shells/details widget runtime UI | paired client sidecar planning, implementation later |
| Page-flow / redirect audit | separate future audit, not this pass |
| Command button implementation | command client sidecars |
| Runtime implementation audit | separate mode if user asks |
| Tests / generated artifacts changes | separate implementation/archive work |

Important boundary:

```text
This is a read/details slice.

No agreement lifecycle transition happens here.
No proposal is sent here.
No accept/refuse action happens here.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-001
  Owns initial exchange creation and first Employee proposal.

SL-AGR-EXCH-002
  Owns sending later Client/Employee proposal versions.

SL-AGR-EXCH-003
  Owns shared server list endpoint/read projection.

L2-AGR-EXCH-LIST-001.client
  Owns shared client list query/model/list widget and actor page shells.

SL-AGR-EXCH-004
  Owns shared server details endpoint/read projection.

L2-AGR-EXCH-DETAILS-001.client
  Owns shared client details query/model/details widget and actor page shells as a docs draft only in this archive.

SL-AGR-EXCH-005
  Owns Client accept active proposal command.

SL-AGR-EXCH-006
  Owns Employee final refusal command and request failure orchestration.

SC-14 / document slices
  Own document storage/download behavior; this read slice returns metadata refs only.

OpenAPI/generated artifact workflow
  Owns regeneration/checks if API contract changes during implementation.
```

---

## 4. Scenario Flow

```text
Client or Employee opens Agreement Exchange details page
        ↓
System resolves current role/account from session
        ↓
System loads visible exchange by exchangeId
        ↓
Client branch filters by ClientAccountId
        ↓
Employee branch uses employee visibility first-pass policy
        ↓
System returns exchange details:
  status, request summary, active proposal, proposal history, document refs
        ↓
User reads details and may use future command actions hosted elsewhere
```

Scenario flow table:

| Step | Actor / System layer | User-visible / system responsibility |
|---|---|---|
| S01 | Client or Employee | Opens one agreement exchange details page. |
| S02 | System | Resolves current role and account id. |
| S03 | System | Applies branch-specific visibility. |
| S04 | System | Projects details with request summary, active proposal and history. |
| S05 | System | Returns document refs, not binary bytes. |
| S06 | Client/UI | Renders details and action slots owned by other sidecars. |

Scenario meaning:

```text
The details read provides visibility, context and proposal history.
It does not decide lifecycle command permissions by itself.
It does not execute lifecycle commands.
```

---

## 5. Implementation Flow

```text
[HTTP GET]
GET /api/agreement-exchanges/{exchangeId}
        ↓
[Auth]
Client or Employee app cookie required
        ↓
[Route binding]
exchangeId is positive long
        ↓
[Controller]
read current role and account id from session
        ↓
[Branch]
Client role   -> readRepository.GetDetailsForClientAsync(clientAccountId, exchangeId)
Employee role -> readRepository.GetDetailsForEmployeeAsync(employeeId, exchangeId)
        ↓
[Read projection]
load exchange, request summary, active proposal and proposal versions
        ↓
[Response]
200 OK AgreementExchangeDetailsResponseDto
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, route binding, current actor context, response mapping.

Validator / binding:
  route shape only first pass.
  No body/query validator needed unless a future query is added.

Read repository / read service:
  current role branch;
  current account visibility;
  read projection query;
  no mutation.

Projection/query:
  filters one exchange;
  projects request summary, active proposal and proposal history.

Domain:
  owns persisted facts used by projection.
  No domain lifecycle method is called in this read slice.

Client:
  paired sidecar owns UI/query/details widget planning; runtime UI is not touched here.
```

Do **not** call from this slice:

```text
ClientSendOwnVersion
EmployeeSendNewVersion
ClientAcceptActiveProposal
FinalRefuseProposal
MarkAgreementExchangeFailed
```

---

## 6. API Contract

### Endpoint

```http
GET /api/agreement-exchanges/{exchangeId}
```

Auth:

```csharp
[Authorize(Roles = "Client,Employee")]
```

Route:

```text
exchangeId: positive long
```

Request body:

```text
none
```

Query:

```text
none first pass
```

Response direction:

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

public sealed record AgreementExchangeRequestSummaryDto(
    long RequestId,
    string RequestStatus,
    string? RequestDisplayName,
    string? ObjectAddress);

public sealed record AgreementProposalDetailsDto(
    long ProposalId,
    int Version,
    string Sender,
    long SenderId,
    string State,
    AgreementDocumentRefDto Document,
    string? Comment,
    DateTimeOffset CreatedAt);

public sealed record AgreementDocumentRefDto(
    string StorageKey,
    string OriginalFileName,
    string ContentType,
    long SizeBytes);
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
  unsupported role or inactive/invalid Employee if current project mapping uses forbidden

404 NotFound
  exchange does not exist or is not visible to the current actor

500 InternalServerError
  unexpected server failure
```

Notes:

```text
- Do not return raw ClientAccountId first pass.
- Do not return document bytes.
- Optional AvailableActions DTO is a future enhancement, not required for this read slice.
```

---

## 7. Validation / ProblemDetails

Route/body shape validation:

```text
- `exchangeId` must be a positive long by route constraint / model binding;
- no request body;
- no query first pass;
- no FluentValidation validator needed first pass unless project convention requires one for route validation.
```

Domain/application/read validation:

```text
- Client visibility is enforced by `ClientAccountId` filter;
- Employee visibility follows active Employee / first-pass any-active-Employee policy;
- missing/not-visible exchange maps to 404;
- lifecycle/turn/active proposal validity is not validated here because this is read-only.
```

Auth/visibility:

```text
- no session -> 401;
- non Client/Employee -> 403;
- Client reading another ClientAccount exchange -> 404, not data leak;
- Employee branch must not require ResponsibleEmployeeId first pass.
```

CSRF:

```text
not applicable; this is safe GET read endpoint.
```

Do not put these into FluentValidation:

```text
ownership
visibility
active proposal sender
exchange lifecycle
actor turn
command permission
```

---

## 8. Domain Behavior

Domain facts consumed by this read slice:

```text
AgreementProposalExchange.Id
AgreementProposalExchange.RequestId
AgreementProposalExchange.ClientAccountId
AgreementProposalExchange.Status
AgreementProposalExchange.ActiveProposalVersion
AgreementProposalExchange proposal versions
AgreementProposal.Author.Sender
AgreementProposal.Author.SenderId
AgreementDocumentRef metadata reference
```

Guardrails:

```text
AgreementProposalExchange and Request are separate aggregates.
Request approval enables exchange start but does not create the exchange automatically.
AgreementProposalExchange owns proposal versions.
AgreementProposal is child entity, not aggregate.
AgreementProposalVersion is local per exchange.
AgreementDocumentRef is metadata reference, not bytes/storage adapter.
No `ResponsibleEmployeeId` guard first pass.
Proposal sender identity is tracked per proposal version.
```

Read behavior:

```text
No domain mutation method is called.
No proposal state is changed.
No request status is changed.
```

---

## 9. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Resolve current Client or Employee server-side. |
| Authorization/visibility | yes | Client by `ClientAccountId`; Employee by first-pass active Employee policy. |
| Antiforgery / unsafe requests | no | Safe GET; command slices own CSRF. |
| Validation / ProblemDetails | yes | Route binding, 401/403/404/500 mapping; no body/query first pass. |
| OpenAPI / generated artifacts | yes | API contract changes require generated artifact workflow. |
| Transaction / atomicity | no write | Read-only projection; no transaction semantics beyond read consistency. |
| No partial write | yes | GET must not mutate request/exchange/proposals. |
| Idempotency / double-submit | no | Read endpoint; command slices own duplicate-submit behavior. |
| Concurrency / stale state | indirect | Details may be stale after commands; client refetch belongs to sidecars. |
| Privacy / cross-account data exposure | yes | Client must not receive another client's exchange. |
| File/document boundary | yes | Return refs only; no bytes/download/upload. |
| Clock/audit actor fields | no write | Projection may display created/last activity timestamps. |
| Client feedback / accessibility | paired sidecar | Client draft owns visible loading/error/not-found states. |
| Redirect/page flow | out of scope | Page-flow/redirect audit is separate. |
| Testing responsibility split | yes | API integration + projection/visibility/no-mutation assertions. |

---

## 10. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-004-Q001` | accepted | Shared Client/Employee details endpoint? | Yes, one shared endpoint first pass. | Shared server read model and shared client details query. |
| `SL-AGR-EXCH-004-Q002` | accepted | Does details include full proposal history? | Yes. Details owns history; list stays summary-only. | Response includes proposal versions ordered by version. |
| `SL-AGR-EXCH-004-Q003` | accepted | Does details return file bytes? | No. Return `AgreementDocumentRef` metadata only. | File download/storage stays future document slice. |
| `SL-AGR-EXCH-004-Q004` | accepted | How does Client access work? | Filter by `AgreementProposalExchange.ClientAccountId == currentClientAccountId`. | Prevents cross-account exposure. |
| `SL-AGR-EXCH-004-Q005` | accepted | How does Employee access work first pass? | Any active Employee can see/service exchanges. | No `ResponsibleEmployeeId` filter. |
| `SL-AGR-EXCH-004-Q006` | accepted | Should details expose raw `ClientAccountId`? | No first pass. It is server protection state, not UI data. | Avoid leaking owner ids. |
| `SL-AGR-EXCH-004-Q007` | accepted | Does details execute commands or determine command permission? | No. Commands enforce their own auth/lifecycle. | UI action slots are not security. |
| `SL-AGR-EXCH-004-Q008` | accepted | Should details merge full request details DTO? | No. Return compact request summary only. | Rich request details remain request read slices/page composition. |
| `SL-AGR-EXCH-004-Q009` | accepted | Use `AgreementExchangeActor` abstraction? | No first pass. Use current role/current account id and explicit branch methods. | Keeps implementation direct. |
| `SL-AGR-EXCH-004-Q010` | accepted | Use application command service for read? | No. Query/read repository projection is enough. | No lifecycle orchestration here. |
| `SL-AGR-EXCH-004-Q011` | accepted | What status for missing/not-visible exchange? | 404 not found/not visible. | Avoid visibility leaks. |
| `SL-AGR-EXCH-004-Q012` | accepted | Is page-flow/redirect behavior part of this pass? | No. Separate future audit. | Archive remains docs-only planning refactor. |

Original draft had no stable question IDs; IDs above are new for the preserved decisions and must not be reused for different meanings later.

---

## 11. Extension / Change Points

| Extension point | Owner / destination | Notes |
|---|---|---|
| Available actions DTO | future details/read enhancement or action policy slice | Optional; command endpoints remain authoritative. |
| File download links | future document/file slice | Details currently returns metadata refs only. |
| Employee assignment/department visibility | future visibility slice | Do not add ResponsibleEmployeeId first pass. |
| Rich request details composition | page/client read composition | Do not merge into exchange details DTO by default. |
| Pagination/history window | future if proposal history grows | Current details returns full history. |
| Audit trail display | future read extension | Proposal sender/time already available. |

---

## 12. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Client can open own exchange details | covered | query filters by `ClientAccountId`. |
| Client cannot open another client's exchange | covered | same filter returns not-found/not-visible. |
| Employee can open employee-visible exchange details | covered | employee branch read. |
| Any active Employee can service exchange first pass | covered | no ResponsibleEmployeeId guard. |
| Details show exchange status | covered | details DTO. |
| Details show request summary | covered | compact request summary DTO. |
| Details show active proposal | covered | active proposal fields. |
| Details show full proposal history | covered | proposal list ordered by version. |
| Details show proposal sender/senderId | covered | proposal-level author projection. |
| Details show document refs, not bytes | covered | document metadata DTO only. |
| Details does not mutate exchange/request/proposals | covered | safe GET/read projection. |
| List summary | out of scope | `SL-AGR-EXCH-003`. |
| Commands | out of scope | command slices. |
| Runtime UI / page-flow / redirects | out of scope | future UI/page-flow audit. |

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
| Client opens own exchange details | 200 response for exchange owned by current ClientAccountId | API integration + DB fixture | client auth fixture, seeded exchange, HTTP GET | Low if DTO fields and id asserted | Low/Medium: projection helper changes may require fixture updates | `GetAgreementExchangeDetails_ClientOwnExchange_ReturnsDetails` |
| Client cannot open another client's exchange | returns 404 and no other client's details leak | API integration | two client fixtures, HTTP GET by non-owner | Low if response body does not include foreign id/data | Low | `GetAgreementExchangeDetails_ClientOtherExchange_ReturnsNotFound` |
| Employee opens visible exchange details | active Employee receives details | API integration | employee auth fixture, seeded exchange, HTTP GET | Medium if employee visibility policy later changes | Low/Medium | `GetAgreementExchangeDetails_EmployeeVisibleExchange_ReturnsDetails` |
| No ResponsibleEmployeeId guard | Employee not tied to proposal sender can still read first pass | API integration | seed exchange with proposal sender employee A, auth employee B, HTTP GET | Low if employee B receives details by policy | Medium if future visibility changes intentionally | `GetAgreementExchangeDetails_EmployeeNotSender_CanReadFirstPass` |
| Missing exchange | missing id returns 404 | API integration | HTTP GET unknown id | Low | Low | `GetAgreementExchangeDetails_MissingExchange_ReturnsNotFound` |
| Details include request summary | response has compact request context | API integration/projection assertion | seeded request/exchange, HTTP GET | Low if summary fields asserted | Low/Medium | covered by success details test |
| Details include active proposal | response has active proposal version/sender/document ref | API integration/projection assertion | seeded proposal history, HTTP GET | Low | Low/Medium | `GetAgreementExchangeDetails_ReturnsActiveProposal` |
| Details include full proposal history | response includes all proposal versions ordered by version | API integration/projection assertion | seeded v1/v2/v3, HTTP GET | Low if order and count asserted | Low/Medium | `GetAgreementExchangeDetails_ReturnsProposalHistoryInVersionOrder` |
| Document refs only | response includes metadata and no binary bytes | API/contract test | HTTP GET and response shape assertion | Low for API shape if bytes field absence checked | Low | `GetAgreementExchangeDetails_ReturnsDocumentRefsOnly` |
| GET does not mutate | exchange/request/proposal state unchanged after details read | API integration + DB snapshot | DB snapshot before/after HTTP GET | Medium if snapshot too narrow | Low/Medium | `GetAgreementExchangeDetails_DoesNotMutateState` |
| Unauthenticated access | unauthenticated caller cannot read details | API integration | HTTP GET without auth | Low | Low | `GetAgreementExchangeDetails_Unauthenticated_ReturnsUnauthorized` |

### API boundary / access

```text
- unauthenticated details returns 401;
- unsupported role returns 403 if test fixture supports it;
- Client can read own exchange;
- Client cannot read another client's exchange;
- Employee can read employee-visible exchange first pass.
```

### Projection

```text
- response includes exchangeStatus;
- response includes request summary;
- response includes activeProposal;
- response includes all proposal versions ordered by Version;
- response includes proposal sender/senderId;
- response includes document refs only;
- response does not include binary bytes or raw ClientAccountId first pass.
```

### No-write tests

```text
- GET details does not change exchange status;
- GET details does not create proposals;
- GET details does not supersede/accept/refuse proposals;
- GET details does not change related request status.
```

### Generated artifacts

```text
- run OpenAPI generation if API contract changed;
- run API type generation if OpenAPI changed;
- stage generated artifacts;
- run check:api.
```

### What not to test here

```text
- proposal send/accept/final-refuse behavior;
- file download bytes;
- client widget layout;
- redirect/page flow;
- repository mock call-order as primary proof.
```

---

## 14. Implementation Direction / Current Refactor Checklist

This checklist is for a future implementation verification pass. It is not a claim that implementation was checked now.

```text
[ ] Confirm endpoint is present or mark implementation drift.
[ ] Confirm route is GET /api/agreement-exchanges/{exchangeId} or update draft/source if actual route differs.
[ ] Confirm auth accepts Client and Employee.
[ ] Confirm request body is none.
[ ] Confirm response is 200 OK with AgreementExchangeDetailsResponseDto.
[ ] Confirm Client branch filters by ClientAccountId.
[ ] Confirm Client cannot read another client's exchange.
[ ] Confirm Employee branch does not require ResponsibleEmployeeId.
[ ] Confirm active Employee policy is explicit if implemented.
[ ] Confirm request summary is projected.
[ ] Confirm active proposal is projected.
[ ] Confirm full proposal history is projected and ordered by version.
[ ] Confirm proposal sender and sender id are projected.
[ ] Confirm document refs are projected without binary bytes.
[ ] Confirm raw ClientAccountId is not returned first pass unless source decision changes.
[ ] Confirm no lifecycle mutation happens in read endpoint.
[ ] Confirm no AgreementExchangeActor abstraction is introduced in target direction.
[ ] Confirm focused API integration tests with visibility/projection/no-mutation assertions exist.
[ ] Confirm OpenAPI/type generation is current if contract changed.
```

This pass did not perform implementation verification.

---

## 15. Historical Client Notes / Future Client Sidecar

Paired client draft in this archive:

```text
planning/slices/l2/L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md
```

Future/client sidecar owns planning for:

```text
- shared entity API wrapper;
- shared details query/model;
- shared details widget;
- Client and Employee page shells;
- visible loading/error/not-found/success states;
- actor-specific back links and copy;
- optional action slots for command sidecars;
- component/entity/E2E test planning.
```

This server draft does not implement runtime client UI.

Deep UI redesign, page-flow and redirect audit are explicitly out of scope for this docs-only archive.

---

## 16. OpenAPI / Generated Artifacts

Expected OpenAPI addition/confirmation:

```text
GET /api/agreement-exchanges/{exchangeId}
responses:
  200 AgreementExchangeDetailsResponseDto
  401
  403
  404
  500
```

Generated artifacts must be updated through repo commands only during implementation work:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
npm.cmd run check:api
```

This docs-only archive does not change generated artifacts.

---

## 17. Dependent / Follow-up Slices

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Separate later work:

```text
- implementation verification pass;
- page flow / redirects audit;
- runtime UI refactoring workflow;
- source registry/behavior item ID sync;
- document/file download/storage slice.
```

Next draft-only refactor candidate:

```text
user-selected next slice from current archive/order.
```

---

## 18. Guardrail Summary

```text
This is a read/details slice.
Use shared endpoint first pass.
Use current role/current account id from session.
Client access must filter by AgreementProposalExchange.ClientAccountId.
Employee access first pass allows any active Employee.
Do not add ResponsibleEmployeeId as exchange authorization guard.
Do not introduce AgreementExchangeActor abstraction in this draft.
Do not introduce application command service for this read slice.
Details includes full proposal history.
List remains summary-only.
Document refs are metadata only; no bytes/download/upload here.
Do not return raw ClientAccountId first pass unless source decision changes.
Request details and exchange details remain separate reads.
UI buttons/action slots are not security.
Server command slices still enforce lifecycle and participant rules.
No proposal send/accept/final-refuse occurs here.
No runtime implementation, tests, generated artifacts or UI/page-flow code were touched in this pass.
```
