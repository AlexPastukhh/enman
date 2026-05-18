# SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal

Status: implemented slice draft refactor / implementation not rechecked in this pass  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: server command slice  
Primary purpose: Employee explicitly starts an AgreementProposalExchange for an approved request by sending the first Employee-authored proposal version  
Depends on:

* `SL-EMP-REQ-004 — Approve Request Review`
* Employee auth/session
* CSRF / unsafe command protection
* L2 domain agreement proposal model

Implementation direction:

```text
AgreementProposalExchange.StartByEmployee(
    approvedRequest,
    document,
    comment,
    employee,
    startedAt)
```

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was not rechecked in this pass.
UI/client page flow and redirects are out of scope for this pass.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-13D — Start Agreement Exchange
```

Related scenarios:

```text
SC-07B — Employee Request Review Actions
SC-13B — Agreement Exchange Details
SC-13A — Agreement Exchange List
```

UI scenario:

```text
missing / pending dedicated UI source for start agreement exchange action
```

Cross-cutting behavior:

```text
CC-SEC-CSRF-001 — Unsafe Command Protection
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for future client sidecar only
```

Data source:

```text
pending scenario-data source for agreement document reference, optional proposal comment and created exchange/proposal summary
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior names until source-sync files are completed.
```

Concern umbrella:

```text
none for this server slice;
CSRF is cross-cutting security concern.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-13D: pending / v000 if source registry is applied
SC-13B: pending / v000 if source registry is applied
CC-SEC-CSRF-001: v001 if source registry is applied
```

Domain baseline:

```text
DOM-v001 if source-sync/domain registry is applied;
otherwise pending domain baseline.
```

Slice derivation map:

```text
pending / add row for SL-AGR-EXCH-001 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| Employee explicitly starts exchange for approved request | SC-13D pending | provided/partially provided by `AgreementProposalExchange.StartByEmployee` | expose API command, resolve current Employee, load approved request, call domain method, persist exchange | approval itself does not start exchange |
| Start includes first Employee proposal | SC-13D pending | provided by domain start method | require document reference, create optional comment, pass Employee actor | not an empty exchange start |
| Exchange stores `ClientAccountId` | SC-13D pending | provided/partially provided by domain using approved request owner | ensure approved request exposes owner ClientAccountId; persist exchange | required for future Client ownership checks |
| First proposal version is `1` | SC-13D pending | provided by domain version model | persist first proposal and active version | `AgreementProposalVersion.First` |
| First proposal author is Employee | SC-13D pending | provided by domain proposal author model | pass authenticated Employee to domain | proposal stores `Sender = Employee`, `SenderId = employee.Id` |
| Exchange status becomes `AwaitingClientConfirmation` | SC-13D pending | provided by domain lifecycle | persist exchange status | client can respond later |
| Duplicate exchange for same request is rejected | SC-13D pending | not domain-only / application + DB guard | check existing exchange by requestId and prefer unique DB constraint | use current Error/ProblemDetails mapper |
| Request status is not changed by this command | SC-13D pending | partly domain boundary decision | do not mutate request status during start exchange first pass | request remains Approved |
| No `ResponsibleEmployeeId` guard | SC-13D pending | domain/access policy decision | do not add exchange-level employee ownership guard | proposal authors track Employee per version |
| Unsafe command is CSRF-protected | CC-SEC-CSRF-001-v001 | not domain behavior | apply current CSRF/antiforgery boundary | full CSRF matrix belongs to cross-cutting tests |

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
  out of scope; future/legacy client sidecar

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
  - old draft was written as implementation-ready checklist even though implementation may already exist;
  - old draft had no explicit page-flow/redirect out-of-scope note.

source:
  - stable behavior item IDs are not yet assigned in source registry.

implementation:
  - not checked in this pass.

UI:
  - not touched in this pass.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection and no UI/redirect flow inspection.
```

---

## 1. Scope

This slice owns:

```text
- protected Employee command endpoint;
- CSRF-protected unsafe request;
- current Employee derived from app cookie identity;
- approved ConnectionRequest loaded by requestId;
- agreement document reference accepted from request body;
- optional proposal comment accepted from request body;
- AgreementProposalExchange aggregate created;
- exchange ClientAccountId populated from approved request owner;
- first AgreementProposal version created by Employee;
- proposal author stores Sender = Employee and SenderId = employee.Id;
- exchange status becomes AwaitingClientConfirmation;
- active proposal version becomes 1;
- command returns 204 No Content;
- duplicate exchange for same request is rejected through existing Error/ProblemDetails mapping;
- API integration test plan with DB/persisted state assertions.
```

Endpoint:

```http
POST /api/employee/requests/{requestId}/agreement-exchange/start
```

Request body:

```json
{
  "documentRef": "agreement-document-reference",
  "comment": "optional employee proposal comment"
}
```

Target transition:

```text
ConnectionRequest.Approved
        ↓ explicit Employee start command
AgreementProposalExchange.AwaitingClientConfirmation
        ↓
ActiveProposalVersion = 1
Proposal[1].Author.Sender = Employee
Proposal[1].Author.SenderId = current employee id
```

This slice does **not** make `ApproveReview` create an exchange.

This slice does **not** change request status first pass.

This slice does **not** upload binary files.

This slice does **not** introduce `ResponsibleEmployeeId`.

This slice does **not** return agreement exchange details/list data.

This slice does **not** own UI redirect/page flow.

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Approve review | `SL-EMP-REQ-004` |
| Automatic exchange creation during approve | explicitly not this slice |
| Agreement exchange list/read summary | `SL-AGR-EXCH-003` |
| Agreement exchange details/read history | `SL-AGR-EXCH-004` |
| Send counter-proposal/revised proposal | `SL-AGR-EXCH-002` |
| Client accept proposal | `SL-AGR-EXCH-005` |
| Final refusal | `SL-AGR-EXCH-006` |
| Responsible employee assignment / ownership lock | future assignment/queue slice, if ever needed |
| Department/region employee visibility | future permission slice |
| Binary file upload/storage | future file/document slice |
| Real document generation | future document generation slice |
| Client UI | future/legacy client exchange sidecar |
| Employee dashboard redesign | employee page/UI refactor workflow |
| Page redirects/navigation after start | page-flow/redirect audit, not this server draft |
| Error mapping cleanup | separate cleanup if existing errors are not expressive enough |

Important boundary:

```text
ApproveReview does not start agreement exchange.

ApproveReview only completes review decision and sets request status to Approved.

Agreement exchange starts only through this explicit command.
```

---

## 3. Related Slices / Owners

```text
SL-EMP-REQ-004
  Owns review approval and request approved state.

SL-AGR-EXCH-001
  Owns explicit exchange start with first Employee proposal.

SL-AGR-EXCH-002
  Owns later proposal/counter-proposal version sending.

SL-AGR-EXCH-003
  Owns agreement exchange list/read summary.

SL-AGR-EXCH-004
  Owns agreement exchange details/read history.

SL-AGR-EXCH-005
  Owns Client accept active proposal.

SL-AGR-EXCH-006
  Owns final refusal.

Domain
  Owns target domain concepts:
  AgreementProposalExchange,
  AgreementProposal,
  AgreementProposalVersion.First,
  AgreementExchangeStatus.AwaitingClientConfirmation,
  AgreementDocumentRef,
  ProposalComment,
  AgreementProposal.Author.Sender,
  AgreementProposal.Author.SenderId,
  ClientAccountId,
  no ResponsibleEmployeeId first pass.

CC-SEC-CSRF-001
  Owns antiforgery token/session context for unsafe browser requests.

Validation / ProblemDetails cross-cutting rules
  Own route/body shape validation and error mapping conventions.

OpenAPI/generated artifact workflow
  Owns regeneration/checks if API contract changes.

Client start-exchange sidecar
  Future/client owner for feature API wrapper, mutation, invalidation and visible action/form behavior.
```

---

## 4. Scenario Flow

```text
[Signed-in Employee]
opens approved connection request
        ↓
System shows request as Approved
        ↓
Employee chooses “Start agreement exchange”
        ↓
Employee provides agreement document reference
        ↓
Employee optionally provides proposal comment
        ↓
System validates request/document/comment/duplicate state
        ↓
System creates AgreementProposalExchange
        ↓
System copies ClientAccountId from approved request owner
        ↓
System creates proposal version 1 authored by Employee
        ↓
Exchange waits for client confirmation
        ↓
Command succeeds without response body
        ↓
Client refreshes agreement exchange/read state
```

Scenario flow table:

| Step | Actor / System layer | User-visible / system responsibility |
|---|---|---|
| S01 | Signed-in Employee | Opens approved request details. |
| S02 | System | Shows request as Approved. |
| S03 | Employee | Chooses start agreement exchange action. |
| S04 | Employee | Provides document reference. |
| S05 | Employee | Optionally provides comment. |
| S06 | System | Validates command preconditions. |
| S07 | System | Creates exchange and first Employee proposal. |
| S08 | System | Stores ClientAccountId and awaits client confirmation. |
| S09 | System | Returns command success without body. |
| S10 | Client/System | Refreshes read state. |

Scenario meaning:

```text
Start agreement exchange is the explicit first proposal command.

It is not review approval.
It is not an empty exchange start.
It is not a file upload.
It is not client acceptance.
```

---

## 5. Implementation Flow

```text
[HTTP POST]
POST /api/employee/requests/{requestId}/agreement-exchange/start
        ↓
[CSRF boundary]
validate unsafe request protection
        ↓
[Auth / Employee context]
resolve current Employee
        ↓
[Route binding / DTO validation]
requestId is positive long
documentRef is required
comment is optional
        ↓
[Command handler]
load Employee
load ConnectionRequest aggregate by requestId
ensure no AgreementProposalExchange exists for requestId
        ↓
[Value objects]
create AgreementDocumentRef
create ProposalComment only when comment has meaningful text
        ↓
[Domain]
AgreementProposalExchange.StartByEmployee(...)
        ↓
[Persistence]
add AgreementProposalExchange
save exchange, ClientAccountId, proposal version 1 and author
        ↓
[Response]
204 No Content
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, route binding, CSRF attribute, response mapping.

Validator:
  route/body shape and document/comment shape.
  No business lifecycle validation.

Command handler:
  current Employee resolution;
  request aggregate load;
  duplicate exchange guard;
  document/comment value object creation;
  repository/application add;
  SaveChanges.

Domain:
  validates approved request and active Employee;
  owns exchange lifecycle start;
  owns proposal version/author/status state.

Persistence:
  persists exchange aggregate and proposal version.

Client:
  future rendering/action UI, mutation, refetch behavior and redirects.
```

---

## 6. API Contract

### Endpoint

```http
POST /api/employee/requests/{requestId}/agreement-exchange/start
```

Route shape:

```csharp
[ApiController]
[Route("api/employee/requests/{requestId:long:min(1)}/agreement-exchange")]
public sealed class EmployeeAgreementExchangeController : ProjectController
{
    [Authorize(Roles = "Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("start", Name = "EmployeeStartAgreementExchange")]
    public async Task<IActionResult> Start(...)
}
```

Route:

```text
requestId: long, positive
```

### Request body

```json
{
  "documentRef": "string",
  "comment": "string | null"
}
```

Request DTO:

```csharp
public sealed record EmployeeStartAgreementExchangeDto(
    string? DocumentRef,
    string? Comment);
```

DTO validation:

```text
documentRef required
documentRef not whitespace
documentRef max length = AgreementDocumentRef.MaxLength, if exposed
comment optional
comment max length = ProposalComment.MaxLength, if provided
```

Domain validation:

```text
AgreementDocumentRef.Create(documentRef)
ProposalComment.Create(comment), only if comment is not blank/null
AgreementProposalExchange.StartByEmployee(...)
```

Blank comment behavior:

```text
Blank/null comment should be treated as no comment.

If ProposalComment.Create rejects blank values, call it only when comment has meaningful text.
```

### Success response

```http
204 No Content
```

Response body:

```text
none
```

Reason:

```text
- start exchange is a command;
- created exchange/proposal state is read through exchange list/details after refetch;
- no StartAgreementExchange response DTO is needed.
```

### Error responses

```text
400 Bad Request
  malformed JSON / antiforgery failure if current project CSRF boundary uses 400

401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Employee / cannot access employee API

404 NotFound
  request does not exist or is not visible/loadable to Employee

409 Conflict
  duplicate exchange for same request, if current mapper supports conflict

422 UnprocessableEntity
  validation or domain lifecycle rejection:
  - missing/blank/too-long documentRef;
  - too-long comment;
  - request is not Approved;
  - request is not a ConnectionRequest;
  - request has no valid owner ClientAccountId;
  - employee cannot start agreement exchange;
  - duplicate exchange if current mapper still uses 422.

500 InternalServerError
  unexpected server failure
```

---

## 7. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-001-Q001` | accepted | Does approve automatically start exchange? | No. Exchange starts by explicit employee command. | Keeps review and agreement lifecycle separated. |
| `SL-AGR-EXCH-001-Q002` | accepted | Is this “empty start” or “start with first proposal”? | Start with initial employee proposal. | Body requires document ref. |
| `SL-AGR-EXCH-001-Q003` | accepted | Who can start exchange? | Employee with app cookie role Employee. | Auth boundary. |
| `SL-AGR-EXCH-001-Q004` | accepted | Request precondition? | ConnectionRequest must be `Approved`. | Domain rule. |
| `SL-AGR-EXCH-001-Q005` | accepted | First proposal version? | Version `1`. | Domain uses `AgreementProposalVersion.First`. |
| `SL-AGR-EXCH-001-Q006` | accepted | Initial exchange status? | `AwaitingClientConfirmation`. | Client can respond later. |
| `SL-AGR-EXCH-001-Q007` | accepted | Duplicate exchange for same request? | Reject through existing Error/ProblemDetails mapping. Prefer conflict semantics, but error mapping cleanup can be separate. | Repository/application guard. |
| `SL-AGR-EXCH-001-Q008` | accepted | Does this command change request status? | No first pass. Request remains `Approved`; exchange has own status. | Avoids new request status decision. |
| `SL-AGR-EXCH-001-Q009` | accepted | File upload? | Out of scope. Store document reference only. | No binary storage. |
| `SL-AGR-EXCH-001-Q010` | accepted | Success response? | `204 No Content`. | Read state comes later from read endpoint. |
| `SL-AGR-EXCH-001-Q011` | accepted | CSRF? | Required. Unsafe browser command. | Server tests. |
| `SL-AGR-EXCH-001-Q012` | accepted | Command status enum? | Do not introduce per-command status enum. Use `Result` / `UnitResult<IReadOnlyList<Error>>` + existing Error mapping. | Avoids application status enum noise. |
| `SL-AGR-EXCH-001-Q013` | accepted | Controller placement? | Use separate `EmployeeAgreementExchangeController`. | Prevents `EmployeeRequestsController` from growing after Start/Approve/Reject. |
| `SL-AGR-EXCH-001-Q014` | accepted | Endpoint route shape? | `POST /api/employee/requests/{requestId}/agreement-exchange/start`. | Keeps exchange scoped under employee request. |
| `SL-AGR-EXCH-001-Q015` | accepted | Comment behavior? | `comment` is optional; null/blank means no comment. | Create `ProposalComment` only for meaningful text. |
| `SL-AGR-EXCH-001-Q016` | accepted | Document input? | First pass stores `AgreementDocumentRef` only. | No binary upload/document generation. |
| `SL-AGR-EXCH-001-Q017` | accepted | Repository shape? | Add agreement exchange repository with `GetByRequestIdAsync` and `Add`. | Supports duplicate guard and aggregate persistence. |
| `SL-AGR-EXCH-001-Q018` | accepted | DB uniqueness? | Prefer unique exchange per `RequestId` if practical. | Prevents duplicate exchanges under race conditions. |
| `SL-AGR-EXCH-001-Q019` | accepted | Failure HTTP mapping? | Use existing Error/ProblemDetails mapper. Do not add command statuses for HTTP mapping. | Error mapping cleanup remains separate. |
| `SL-AGR-EXCH-001-Q020` | accepted | 409 vs 422 for duplicate? | Do not block this slice on mapping cleanup. Use current mapper; prefer future `409 Conflict` if/when mapping supports it. | Keeps slice implementable now. |
| `SL-AGR-EXCH-001-Q021` | accepted | Should exchange store client-side participant id? | Yes. Add `AgreementProposalExchange.ClientAccountId`. | Enables domain ownership checks for future client actions. |
| `SL-AGR-EXCH-001-Q022` | accepted | Where does `ClientAccountId` come from? | From approved request owner, preferably `approvedRequest.ClientAccountId` or `approvedRequest.GetOwnerClientAccountId()`. | Start command initializes exchange participant. |
| `SL-AGR-EXCH-001-Q023` | accepted | Should exchange store `ResponsibleEmployeeId` as guard? | No first pass. Any active Employee can service exchange; proposal authors track employee sender per version. | Avoids employee ownership lock/assignment model. |

---

## 8. Domain Behavior

Current target domain behavior:

```text
AgreementProposalExchange.StartByEmployee:
- requires approvedRequest not null;
- requires approvedRequest.Id > 0;
- requires approvedRequest.Status = Approved;
- obtains ClientAccountId from approvedRequest owner;
- requires ClientAccountId > 0;
- requires document;
- requires employee;
- requires employee.Id > 0;
- checks employee.EnsureCanStartAgreementExchange();
- creates exchange;
- sets RequestId;
- sets ClientAccountId;
- sets Status = AwaitingClientConfirmation;
- sets ActiveProposalVersion = First;
- creates employee proposal version 1;
- stores document/comment/employee author/createdAt.
```

Connection request approve behavior is separate:

```text
ConnectionRequest.ApproveReview:
- requires review exists;
- requires request Status = InReview;
- completes review as approved;
- sets request Status = Approved;
- does not create agreement exchange.
```

Target `AgreementProposalExchange` state:

```text
RequestId
ClientAccountId
Status
ActiveProposalVersion
Proposals
```

Target first-pass Employee access:

```text
No ResponsibleEmployeeId guard.
Any active Employee can service the exchange.
Employee authorship is recorded per proposal version.
```

---

## 9. Application Result Model

Use existing `Result` / `UnitResult` + `Error` model.

Do **not** add:

```csharp
EmployeeStartAgreementExchangeCommandStatus
```

Do **not** use `AgreementExchangeStatus` as command execution result.

Preferred command shape:

```csharp
public sealed record EmployeeStartAgreementExchangeCommand(
    long EmployeeId,
    long RequestId,
    string DocumentRef,
    string? Comment)
    : IRequest<UnitResult<IReadOnlyList<Error>>>;
```

Controller mapping direction:

```text
Success:
  UnitResult.Success -> 204 No Content

Failure:
  existing ProblemDetails/error mapper decides response from Error codes
```

If current Error mapping cannot distinguish not found / forbidden / duplicate conflict cleanly, do not add per-command status enum as a workaround in this slice. Improve Error codes / ProblemDetails mapping separately.

---

## 10. Persistence / Repository Notes

Repository decision:

```csharp
public interface IAgreementProposalExchangeRepository
{
    Task<AgreementProposalExchange?> GetByRequestIdAsync(
        long requestId,
        CancellationToken cancellationToken);

    void Add(AgreementProposalExchange exchange);
}
```

EF persistence needs to support:

```text
AgreementProposalExchange:
  Id
  RequestId
  ClientAccountId
  Status
  ActiveProposalVersion
  CreatedAt
  FinalRefusedByEmployeeId nullable
  FinalRefusedAt nullable
  FinalRefusalReason nullable

AgreementProposal:
  Id
  AgreementProposalExchangeId
  Version
  Author sender/id
  State
  Document ref
  Comment nullable
  CreatedAt
```

Recommended exchange indexes:

```text
IX_L1AgreementProposalExchanges_ClientAccountId
UX_L1AgreementProposalExchanges_RequestId
```

---

## 11. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Resolve current authenticated Employee server-side. |
| Authorization/visibility | yes | Handler owns request visibility/reviewability check. |
| Antiforgery / unsafe requests | yes | POST command must follow current CSRF/unsafe-request policy. |
| Request validation / ProblemDetails | yes | Route + document/comment DTO validation. |
| OpenAPI / generated artifacts | yes | API contract changes require generated artifact workflow. |
| Transaction / atomicity | yes | Exchange and first proposal must persist atomically. |
| No partial write | yes | Failed validation/lifecycle checks must not create exchange/proposal. |
| Duplicate/race prevention | yes | Application duplicate guard plus preferred DB uniqueness. |
| Concurrency / stale state | yes | Command re-checks request/exchange state even if UI looked startable. |
| File/document boundary | yes | Stores document reference only; no binary upload. |
| Clock/audit actor fields | yes | Use server UTC time and authenticated Employee id. |
| Privacy / cross-account data exposure | yes | Do not expose client private fields in command response. |
| Employee ownership lock | no first pass | Do not add `ResponsibleEmployeeId`; proposal authors track sender per version. |
| Client feedback / accessibility | future | Future client sidecar owns form/button/error UX. |
| Redirect/page flow | future | Page-flow audit, not this server slice. |
| Testing responsibility split | yes | API integration + DB assertions; no repository mocks as primary proof. |

---

## 12. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Employee starts exchange for approved request | covered | command calls `AgreementProposalExchange.StartByEmployee(...)`. |
| Exchange stores client-side participant | covered | `ClientAccountId` copied from approved request owner. |
| Initial proposal document is required | covered | DTO + `AgreementDocumentRef` validation. |
| Comment is optional | covered | only creates `ProposalComment` when non-blank. |
| First proposal version is 1 | covered | domain uses `AgreementProposalVersion.First`. |
| First proposal is authored by Employee | covered | proposal author stores `Sender = Employee`, `SenderId = employee.Id`. |
| Exchange waits for client confirmation | covered | domain sets `AwaitingClientConfirmation`. |
| Not-approved request cannot start exchange | covered | domain/application lifecycle rejection. |
| Duplicate exchange cannot be started | covered | repository/application duplicate guard + preferred unique `RequestId`. |
| Approve does not create exchange | covered | explicit command boundary. |
| No binary file upload | covered | document reference only. |
| No employee ownership lock | covered | no `ResponsibleEmployeeId`; any active Employee can service first pass. |
| Command does not return details payload | covered | `204 No Content`. |
| Client start action UI | out of scope | future client sidecar. |
| Redirect/page flow after start | out of scope | future page-flow/redirect audit. |

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
| Employee starts exchange for approved request | POST returns `204`; exchange row exists | API integration + DB assertion | auth fixture, approved request setup, HTTP POST, DB read | Low if exchange/proposal state is asserted | Low/Medium: schema/helper refactor may affect DB assertion | `StartAgreementExchange_ForApprovedRequest_CreatesExchangeAndReturnsNoContent` |
| Exchange stores ClientAccountId | exchange ClientAccountId equals approved request owner | API integration + DB assertion | HTTP POST, DB read | Low if asserted | Low/Medium | same success test |
| First proposal version is 1 | active proposal version and proposal version are 1 | API integration + DB assertion | HTTP POST, DB read | Low | Low/Medium | same success test |
| First proposal is authored by Employee | proposal author sender/id is current Employee | API integration + DB assertion | auth fixture, HTTP POST, DB read | Low if actor id asserted | Low | same success test |
| Exchange waits for client confirmation | exchange status is `AwaitingClientConfirmation` | API integration + DB assertion | HTTP POST, DB read | Low | Low/Medium | same success test |
| DocumentRef is required | missing/blank documentRef returns `422`; no exchange created | API integration + no-mutation assertion | body variants, DB snapshot/read | Low if no exchange assertion exists | Low | `StartAgreementExchange_WhenDocumentMissing_ReturnsValidationProblemAndDoesNotCreateExchange` |
| Comment is optional | null/blank comment still succeeds and stores no comment | API integration + DB assertion | HTTP POST with null/blank comment | Low | Low | `StartAgreementExchange_WithBlankComment_TreatsAsNoComment` |
| Not-approved request cannot start exchange | returns `422`; no exchange/proposal created | API integration + no-mutation assertion | request status setup, HTTP POST, DB read | Low if no exchange/proposal assertion exists | Low/Medium | `StartAgreementExchange_WhenRequestNotApproved_ReturnsValidationProblemAndDoesNotCreateExchange` |
| Duplicate exchange cannot be started | second POST returns conflict/validation problem; original exchange unchanged | API integration + DB assertion | first exchange setup, second HTTP POST, DB snapshot | Low if proposal count/status unchanged asserted | Low/Medium | `StartAgreementExchange_WhenExchangeAlreadyExists_ReturnsProblemAndDoesNotCreateSecondExchange` |
| Approve alone does not create exchange | ApproveReview command leaves exchange table empty | integration boundary test | approve command + DB read | Low if exchange count asserted | Low/Medium | belongs to approve/exchange boundary coverage |
| No ResponsibleEmployeeId guard | exchange has no employee owner guard; proposal author still stored | DB/model assertion or domain test if relevant | metadata/DB read | Medium | Medium | optional architecture guard |
| Unsafe command is protected | missing/invalid CSRF rejected by command family smoke | API integration smoke | POST without token | Medium if no no-mutation assertion | Low | one CSRF smoke; full matrix belongs to `CC-SEC-CSRF-001` |

### API boundary / access

```text
- unauthenticated start exchange returns 401;
- non-Employee/client account returns 403;
- authenticated Employee can start exchange for approved visible request.
```

### Command success

```text
- POST start exchange returns 204 No Content;
- response body is empty;
- AgreementProposalExchange is created;
- exchange RequestId = requestId;
- exchange ClientAccountId = approved request owner;
- exchange Status = AwaitingClientConfirmation;
- exchange ActiveProposalVersion = 1;
- one proposal is created;
- proposal Version = 1;
- proposal Author.Sender = Employee;
- proposal Author.SenderId = current Employee id;
- proposal DocumentRef is stored;
- proposal Comment is stored only when meaningful text exists;
- request Status remains Approved first pass.
```

### Lifecycle / no-write tests

```text
- missing/not-visible request returns 404 or visibility-safe problem;
- not-approved request returns lifecycle problem and creates no exchange;
- invalid documentRef/comment creates no exchange;
- duplicate exchange creates no second exchange/proposal;
- failed command does not change request status;
- failed command does not create proposal version.
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
- exchange details/list payload in command response;
- start response DTO;
- ApproveReview implementation except boundary “approve does not create exchange” if needed;
- client accept/counter-proposal/final refusal;
- client UI;
- redirect/page flow;
- binary file upload;
- document generation;
- repository mock call-order as primary proof;
- unit tests unless reusable helper/domain logic is introduced.
```

---

## 14. Backend Implementation Direction / Current Refactor Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[ ] Confirm endpoint is present or mark implementation drift.
[ ] Confirm success is 204 No Content.
[ ] Confirm no start-exchange response DTO exists.
[ ] Confirm Employee id is not accepted in request body.
[ ] Confirm Employee comes from authenticated context.
[ ] Confirm requestId route uses positive long constraint or equivalent validation.
[ ] Confirm documentRef is required/not whitespace/max length.
[ ] Confirm comment is optional and blank means no comment.
[ ] Confirm Request aggregate is loaded by requestId.
[ ] Confirm request must be Approved or mark implementation drift.
[ ] Confirm ClientAccountId is copied from approved request owner.
[ ] Confirm no ResponsibleEmployeeId guard is introduced.
[ ] Confirm duplicate exchange guard exists.
[ ] Confirm DB uniqueness per RequestId exists or is tracked as follow-up.
[ ] Confirm AgreementDocumentRef is created/validated.
[ ] Confirm optional ProposalComment is created only for meaningful text.
[ ] Confirm AgreementProposalExchange.StartByEmployee or equivalent domain method is used.
[ ] Confirm exchange + proposal version 1 are persisted atomically.
[ ] Confirm lifecycle failures map to ProblemDetails.
[ ] Confirm no new per-command status enum is introduced in target direction; if implementation already has one, mark as future architecture cleanup.
[ ] Confirm focused API integration tests with DB state assertions exist.
[ ] Confirm OpenAPI/type generation is current if contract changed.
[ ] Confirm ApproveReview is not changed to create exchange.
[ ] Confirm command does not return details/list row.
```

This pass did not perform implementation verification.

---

## 15. Historical Client Notes / Future Client Sidecar

The original draft is a server command slice. Client action/form behavior should be handled in a separate client sidecar.

Future client sidecar should own:

```text
features/agreement-exchange/start/api/startAgreementExchange.ts
features/agreement-exchange/start/model/useStartAgreementExchangeMutation.ts
start exchange action UI/form
documentRef/comment client validation
query invalidation/refetch for employee request details and agreement exchange list/details
visible error/success feedback
page-flow/redirect behavior if any
```

Client API placement rule remains:

```text
command endpoint wrappers go to features/*/api;
do not add business wrappers to shared/api.
```

---

## 16. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/employee/requests/{requestId}/agreement-exchange/start
request body: EmployeeStartAgreementExchangeDto
responses:
  204
  400
  401
  403
  404
  409, if duplicate conflict mapping exists
  422
  500
```

Generated artifacts must be updated through tools only:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd run check:api
```

If duplicate exchange still maps to `422` for now, keep OpenAPI aligned with actual implementation and add `409` only after error mapping cleanup.

---

## 17. Dependent / Follow-up Slices

```text
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Separate later work:

```text
- client sidecar draft refactor;
- page flow / redirects audit;
- UI refactoring workflow.
```

---

## 18. Guardrail Summary

```text
Start initial exchange is a server command slice.
This is not empty exchange start.
It starts exchange with first Employee proposal version.
ApproveReview does not create AgreementProposalExchange.
Request must already be Approved.
Document reference is required.
Comment is optional.
Exchange stores ClientAccountId from the approved request owner.
ClientAccountId is required for future client-side exchange ownership guards.
Employee ownership id is not used as first-pass access guard.
Proposal authors are stored per proposal version.
Success response is 204 No Content.
Use separate EmployeeAgreementExchangeController.
Do not add per-command status enum.
Do not use AgreementExchangeStatus as command execution result.
Domain/application Error codes drive HTTP failure mapping.
If error mapping is weak, improve it separately.
Do not upload binary files in this slice.
Do not implement client accept/counter-proposal/final refusal in this slice.
Do not change employee auth in this slice.
Do not mix this into ApproveReview handler.
UI/page redirects are out of scope for this server draft refactor.
```
