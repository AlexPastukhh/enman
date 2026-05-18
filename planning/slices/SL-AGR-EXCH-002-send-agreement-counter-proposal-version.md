# SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version

Status: implemented slice draft refactor / implementation not rechecked in this pass  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: shared server command endpoint + shared DTO + role-based service branch  
Primary purpose: Client or Employee sends the next proposal version in an existing AgreementProposalExchange  
Depends on:

* `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`
* Client/Employee auth/session
* CSRF / unsafe command protection
* agreement exchange persistence
* `AgreementProposalExchange.ClientAccountId` cleanup

Implementation direction:

```text
Client branch:
  AgreementProposalExchange.ClientSendOwnVersion(...)

Employee branch:
  AgreementProposalExchange.EmployeeSendNewVersion(...)
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
SC-13B — Agreement Exchange Details
SC-13D — Start Agreement Exchange, as prerequisite only
```

Related scenarios:

```text
SC-13A — Agreement Exchange List
SC-13E — Final Refuse Exchange
SC-14 — Agreement Documents, future document/file flow only
```

UI scenario:

```text
missing / pending dedicated UI source for send proposal version action
```

Cross-cutting behavior:

```text
CC-SEC-CSRF-001 — Unsafe Command Protection
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for future client sidecar only
```

Data source:

```text
pending scenario-data source for proposal document reference, optional proposal comment and proposal history/version state
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
SC-13B: pending / v000 if source registry is applied
SC-13D: pending / v000 if source registry is applied
CC-SEC-CSRF-001: v001 if source registry is applied
```

Domain baseline:

```text
DOM-v001 if source-sync/domain registry is applied;
otherwise pending domain baseline.
```

Slice derivation map:

```text
pending / add row for SL-AGR-EXCH-002 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| Existing exchange receives next proposal version | SC-13B pending | provided/partially provided by exchange domain methods | expose shared command, load exchange by requestId, call role-specific domain method, persist result | does not create exchange |
| Shared endpoint accepts Client and Employee | SC-13B pending | not domain behavior | authorize Client/Employee, resolve current role/account, branch only to service method | one endpoint first pass |
| Client sends own version | SC-13B pending | provided by `ClientSendOwnVersion` | resolve current Client, enforce ownership through domain, persist next version | client must match `ClientAccountId` |
| Employee sends new version | SC-13B pending | provided by `EmployeeSendNewVersion` | resolve current Employee, pass actor to domain, persist next version | no `ResponsibleEmployeeId` guard |
| Previous active proposal is superseded | SC-13B pending | provided by domain lifecycle | persist active proposal state change | no history deletion |
| Next proposal version is created | SC-13B pending | provided by domain versioning | persist new proposal and active version | version increments |
| Waiting state switches to other side | SC-13B pending | provided by domain lifecycle | persist exchange status | Client send -> AwaitingEmployeeResponse; Employee send -> AwaitingClientConfirmation |
| Wrong turn is rejected | SC-13B pending | provided by domain lifecycle | map failure to ProblemDetails | UI visibility is not security |
| Accepted/finally refused exchange rejects new versions | SC-13B/SC-13E pending | provided by domain lifecycle | map failure and preserve state | no mutation |
| Document reference is required | SC-13B pending | API/domain value object responsibility | validate DTO and value object | no binary upload |
| Comment is optional | SC-13B pending | API/domain value object responsibility | create comment only for meaningful text | blank/null means no comment |
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
- one shared command endpoint for Client and Employee;
- route is request-scoped: POST /api/requests/{requestId}/agreement-exchange/proposals;
- role resolved from current app cookie session;
- one shared request DTO;
- controller branches only by current role;
- explicit service method for Client branch;
- explicit service method for Employee branch;
- Client branch calls AgreementProposalExchange.ClientSendOwnVersion(...);
- Employee branch calls AgreementProposalExchange.EmployeeSendNewVersion(...);
- Client actions protected by exchange.ClientAccountId;
- Employee actions allowed for any active Employee first pass;
- previous active proposal becomes SupersededByCounterProposal;
- next proposal version is created;
- exchange waiting state switches to the other side;
- command returns 204 No Content;
- failures use existing Error / ProblemDetails mapping;
- no per-command status enum;
- API integration test plan with DB/persisted state assertions.
```

Endpoint:

```http
POST /api/requests/{requestId}/agreement-exchange/proposals
```

Request body:

```json
{
  "document": {
    "storageKey": "agreements/request-1-v2.pdf",
    "originalFileName": "request-1-v2.pdf",
    "contentType": "application/pdf",
    "sizeBytes": 4096
  },
  "comment": "Please review updated agreement."
}
```

Target transitions:

```text
Client branch:
  AwaitingClientConfirmation
        ↓ ClientSendOwnVersion(...)
  AwaitingEmployeeResponse

Employee branch:
  AwaitingEmployeeResponse
        ↓ EmployeeSendNewVersion(...)
  AwaitingClientConfirmation
```

This slice does **not** create `AgreementProposalExchange`.

This slice does **not** start an empty exchange.

This slice does **not** accept an active proposal.

This slice does **not** finally refuse an exchange.

This slice does **not** upload binary files.

This slice does **not** introduce `ResponsibleEmployeeId`.

This slice does **not** return agreement exchange details/list data.

This slice does **not** own UI redirect/page flow.

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Start exchange with first employee proposal | `SL-AGR-EXCH-001` |
| Empty exchange start | not supported by current domain |
| Agreement exchange list/read summary | `SL-AGR-EXCH-003` |
| Agreement exchange details/full history read | `SL-AGR-EXCH-004` |
| Client accept active proposal | `SL-AGR-EXCH-005` |
| Final refusal | `SL-AGR-EXCH-006` |
| Binary file upload/storage | future file/document slice |
| Separate Client/Employee endpoints | future only if behavior diverges |
| `ResponsibleEmployeeId` as guard | explicitly not this slice |
| Per-command status enums | explicitly not used |
| Client/Employee proposal UI | future/legacy client sidecar |
| Page redirects/navigation after send | page-flow/redirect audit, not this server draft |

Important boundary:

```text
This endpoint does not create AgreementProposalExchange.

It only adds a new proposal version to an existing exchange.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-001
  Owns initial exchange creation with first Employee proposal.

SL-AGR-EXCH-002
  Owns sending next proposal versions from Client or Employee.

SL-AGR-EXCH-003
  Owns agreement exchange list/read summary.

SL-AGR-EXCH-004
  Owns agreement exchange details/full history read.

SL-AGR-EXCH-005
  Owns Client accept active proposal.

SL-AGR-EXCH-006
  Owns final refusal.

Domain
  Owns target domain concepts:
  AgreementProposalExchange,
  ClientAccountId,
  ClientSendOwnVersion,
  EmployeeSendNewVersion,
  AgreementProposal.Author.Sender,
  AgreementProposal.Author.SenderId,
  SupersededByCounterProposal,
  AwaitingClientConfirmation,
  AwaitingEmployeeResponse,
  no ResponsibleEmployeeId first pass.

CC-SEC-CSRF-001
  Owns antiforgery token/session context for unsafe browser requests.

Validation / ProblemDetails cross-cutting rules
  Own route/body shape validation and error mapping conventions.

OpenAPI/generated artifact workflow
  Owns regeneration/checks if API contract changes.

Client send-proposal sidecar
  Future/client owner for feature API wrapper, mutation, invalidation and visible action/form behavior.
```

---

## 4. Scenario Flow

```text
[Client or Employee]
opens agreement exchange details
        ↓
System shows active proposal version and sender
        ↓
User chooses “Send own version”
        ↓
User provides agreement document reference
        ↓
User optionally provides proposal comment
        ↓
System resolves current role and account id from session
        ↓
System validates participant/lifecycle/turn rules
        ↓
System supersedes previous active proposal
        ↓
System creates next proposal version
        ↓
System switches exchange waiting state to the other side
        ↓
Command succeeds without response body
        ↓
Client refreshes agreement exchange details/list read state
```

Scenario flow table:

| Step | Actor / System layer | User-visible / system responsibility |
|---|---|---|
| S01 | Client or Employee | Opens agreement exchange details. |
| S02 | System | Shows active proposal version and sender. |
| S03 | User | Chooses send own version. |
| S04 | User | Provides document reference. |
| S05 | User | Optionally provides comment. |
| S06 | System | Resolves current role/account from session. |
| S07 | System/Domain | Validates participant/lifecycle/turn rules. |
| S08 | Domain | Supersedes previous active proposal. |
| S09 | Domain | Creates next version. |
| S10 | Domain | Switches waiting state to other side. |
| S11 | System | Returns command success without body. |
| S12 | Client/System | Refreshes read state. |

Scenario meaning:

```text
Send proposal version is the counter-proposal/versioning command for an existing exchange.

It is not initial exchange start.
It is not proposal accept.
It is not final refusal.
It is not binary file upload.
```

---

## 5. Implementation Flow

```text
[HTTP POST]
POST /api/requests/{requestId}/agreement-exchange/proposals
        ↓
[CSRF boundary]
validate unsafe request protection
        ↓
[Auth / Role context]
Client or Employee app cookie required
        ↓
[Route binding / DTO validation]
requestId is positive long
document is required
comment is optional
        ↓
[Controller]
read current role and account id from session
        ↓
[Branch]
Client role   -> service.SendClientProposalVersionAsync(...)
Employee role -> service.SendEmployeeProposalVersionAsync(...)
        ↓
[Service]
load account
load exchange by requestId with proposals
create AgreementDocumentRef
create optional ProposalComment
call role-specific domain method
        ↓
[Domain]
supersede previous active proposal
append next proposal
switch waiting state
        ↓
[Persistence]
save exchange aggregate changes
        ↓
[Response]
204 No Content
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, route binding, CSRF attribute, response mapping;
  may branch by role only to call service method.

Validator:
  route/body/document/comment shape.
  No ownership/lifecycle/turn validation.

Application service:
  current account load;
  exchange load by requestId;
  document/comment value object creation;
  role-specific domain method call;
  SaveChanges.

Domain:
  participant ownership, turn/lifecycle invariants, proposal versioning and status transition.

Persistence:
  preserves proposal history and exchange state.

Client:
  future rendering/action UI, mutation, refetch behavior and redirects.
```

---

## 6. API Contract

### Endpoint

```http
POST /api/requests/{requestId}/agreement-exchange/proposals
```

Auth:

```csharp
[Authorize(Roles = "Client,Employee")]
[RequireAntiforgeryToken]
```

Route:

```text
requestId: long, positive
```

Route rationale:

```text
Use request-scoped route because current lookup is by requestId.

Do not use /api/agreement-exchanges/{requestId}/proposals because that looks like {exchangeId}.
```

### Request body

```json
{
  "document": {
    "storageKey": "agreements/request-1-v2.pdf",
    "originalFileName": "request-1-v2.pdf",
    "contentType": "application/pdf",
    "sizeBytes": 4096
  },
  "comment": "Please review updated agreement."
}
```

DTO direction:

```csharp
public sealed record SendAgreementProposalVersionDto(
    AgreementDocumentRefDto? Document,
    string? Comment);
```

DTO validation:

```text
document required
document.storageKey required
document.originalFileName required
document.contentType required
document.sizeBytes > 0
comment optional
comment max length = ProposalComment.MaxLength, if provided
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
- send proposal is a command;
- proposal/exchange state is read through exchange details/list after refetch;
- no proposal response DTO is needed.
```

### Error responses

```text
400 Bad Request
  malformed JSON / antiforgery failure if current project CSRF boundary uses 400

401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but role/action is not allowed

404 NotFound
  exchange/request not found if mapper supports it

422 UnprocessableEntity
  validation or domain lifecycle rejection:
  - missing/invalid document;
  - comment too long;
  - wrong turn;
  - client is not exchange.ClientAccountId;
  - inactive Employee;
  - exchange already Accepted;
  - exchange FinallyRefused;
  - active proposal state/sender invalid.

500 InternalServerError
  unexpected server failure
```

---

## 7. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-002-Q001` | accepted | One endpoint or separate Client/Employee endpoints? | One shared endpoint first pass. | Shared frontend/API path. |
| `SL-AGR-EXCH-002-Q002` | accepted | One slice or two? | One slice with Client and Employee branches. | Avoids duplicated mirrored slices. |
| `SL-AGR-EXCH-002-Q003` | accepted | Does this endpoint create exchange? | No. Exchange must already exist. | Initial creation remains in `SL-AGR-EXCH-001`. |
| `SL-AGR-EXCH-002-Q004` | accepted | How is current side determined? | From current app session role and account id. | Controller/service branch by role. |
| `SL-AGR-EXCH-002-Q005` | accepted | Do we introduce `AgreementExchangeActor` abstraction? | No first pass. Use current role/current account id and explicit service methods. | Keeps implementation simple. |
| `SL-AGR-EXCH-002-Q006` | accepted | Client ownership rule? | Domain checks `client.Id == exchange.ClientAccountId`. | Prevents client acting on another client's exchange. |
| `SL-AGR-EXCH-002-Q007` | accepted | Employee ownership rule? | No `ResponsibleEmployeeId` guard first pass. Any active Employee can service exchange. | Different employees can continue same exchange. |
| `SL-AGR-EXCH-002-Q008` | accepted | Where are employee identities stored? | Per proposal version via `AgreementProposal.Author(Sender=Employee, SenderId=employee.Id)`. | Audit/history preserved. |
| `SL-AGR-EXCH-002-Q009` | accepted | Client branch domain method? | `AgreementProposalExchange.ClientSendOwnVersion(...)`. | Creates client counter-proposal. |
| `SL-AGR-EXCH-002-Q010` | accepted | Employee branch domain method? | `AgreementProposalExchange.EmployeeSendNewVersion(...)`. | Creates employee counter-proposal. |
| `SL-AGR-EXCH-002-Q011` | accepted | Success response? | `204 No Content`. | Read state comes from exchange details/list. |
| `SL-AGR-EXCH-002-Q012` | accepted | Return proposal DTO? | No. This is command endpoint. | Avoids command/read mixing. |
| `SL-AGR-EXCH-002-Q013` | accepted | Document input? | Store document reference only. | No binary upload. |
| `SL-AGR-EXCH-002-Q014` | accepted | Comment behavior? | Optional; null/blank means no comment. | Create `ProposalComment` only for meaningful text. |
| `SL-AGR-EXCH-002-Q015` | accepted | Command status enum? | Do not add. Use `UnitResult<IReadOnlyList<Error>>`. | Error mapping remains centralized. |
| `SL-AGR-EXCH-002-Q016` | accepted | Wrong turn behavior? | Domain lifecycle errors. | No duplicate checks in UI only. |
| `SL-AGR-EXCH-002-Q017` | accepted | Accepted/finally refused exchange? | Cannot receive new version. Domain rejects. | Lifecycle invariant. |
| `SL-AGR-EXCH-002-Q018` | accepted | FluentValidation responsibility? | DTO shape only. | No ownership/lifecycle in validators. |
| `SL-AGR-EXCH-002-Q019` | accepted | Split later? | Only if client/employee behavior diverges. | First pass stays shared. |
| `SL-AGR-EXCH-002-Q020` | accepted | Route shape? | `POST /api/requests/{requestId}/agreement-exchange/proposals`. | Avoids confusing requestId with exchangeId. |

---

## 8. Domain Requirements

`AgreementProposalExchange` must contain:

```csharp
public long RequestId { get; private set; }

public long ClientAccountId { get; private set; }
```

Client-side domain methods must protect participant ownership:

```csharp
if (client.Id != ClientAccountId)
{
    return UnitResult.Failure<IReadOnlyList<Error>>(
        [Errors.L1Domain.ClientCannotActOnThisAgreementExchange]);
}
```

Applies to:

```text
ClientSendOwnVersion(...)
ClientAcceptActiveProposal(...)
future client-side refusal, if added
```

Employee-side domain methods must **not** check fixed responsible employee:

```text
Do not add:
employee.Id == ResponsibleEmployeeId
```

Employee branch checks:

```text
- employee exists;
- employee.Id > 0;
- employee.EnsureCanSendAgreementProposal();
- exchange status allows employee response;
- active proposal author is Client.
```

Proposal authors stay per version:

```text
AgreementProposal.Author.Sender
AgreementProposal.Author.SenderId
```

---

## 9. Domain Behavior

Client branch:

```text
AgreementProposalExchange.ClientSendOwnVersion(
    document,
    comment,
    client,
    createdAt)
```

Expected behavior:

```text
- client must be exchange.ClientAccountId;
- exchange status must be AwaitingClientConfirmation;
- active proposal author must be Employee;
- document is required;
- active proposal is marked SupersededByCounterProposal;
- next proposal version is created by Client;
- ActiveProposalVersion becomes next version;
- exchange status becomes AwaitingEmployeeResponse.
```

Employee branch:

```text
AgreementProposalExchange.EmployeeSendNewVersion(
    document,
    comment,
    employee,
    createdAt)
```

Expected behavior:

```text
- employee must be active/allowed to send agreement proposal;
- exchange status must be AwaitingEmployeeResponse;
- active proposal author must be Client;
- document is required;
- active proposal is marked SupersededByCounterProposal;
- next proposal version is created by Employee;
- ActiveProposalVersion becomes next version;
- exchange status becomes AwaitingClientConfirmation.
```

Common behavior:

```text
- does not create new exchange;
- does not mutate request status;
- does not accept proposal;
- does not finally refuse exchange;
- preserves proposal version history.
```

---

## 10. Application Result Model

Do not add per-command status enum.

Preferred command/service result:

```csharp
UnitResult<IReadOnlyList<Error>>
```

Controller mapping:

```text
Success -> 204 No Content
Failure -> existing ProblemDetails/error mapper
```

Important:

```text
AgreementExchangeStatus is persisted domain state.

It is not command execution result.

If Error mapping is weak, improve Error/ProblemDetails mapping separately.
```

---

## 11. DTO Validation

Shared validator direction:

```csharp
public sealed class SendAgreementProposalVersionDtoValidator
    : AbstractValidator<SendAgreementProposalVersionDto>
{
    public SendAgreementProposalVersionDtoValidator()
    {
        RuleFor(x => x.Document)
            .NotNull();

        RuleFor(x => x.Document!.StorageKey)
            .NotEmpty();

        RuleFor(x => x.Document!.OriginalFileName)
            .NotEmpty();

        RuleFor(x => x.Document!.ContentType)
            .NotEmpty();

        RuleFor(x => x.Document!.SizeBytes)
            .GreaterThan(0);

        RuleFor(x => x.Comment)
            .MaximumLength(ProposalComment.MaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Comment));
    }
}
```

Validator must not check:

```text
- ClientAccountId ownership;
- current turn;
- exchange status;
- active proposal author;
- employee visibility;
- lifecycle transitions.
```

---

## 12. Repository / Persistence Notes

Required repository usage:

```csharp
Task<AgreementProposalExchange?> GetByRequestIdAsync(
    long requestId,
    CancellationToken cancellationToken);
```

The aggregate must be loaded with proposals so domain methods can:

```text
- find active proposal;
- mark active proposal superseded;
- calculate next version;
- append new proposal;
- update active version/status.
```

Persistence must preserve:

```text
- existing proposal history;
- superseded proposal state;
- new proposal version;
- active proposal version;
- exchange status;
- document ref;
- optional comment;
- author sender/id;
- createdAt.
```

---

## 13. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Resolve current Client or Employee server-side. |
| Authorization/role branch | yes | Controller branches by role only to call the correct service method. |
| Client ownership | yes | Domain checks `client.Id == exchange.ClientAccountId`. |
| Employee exchange ownership | no first pass | No `ResponsibleEmployeeId` guard; any active Employee can service exchange. |
| Antiforgery / unsafe requests | yes | POST command must follow current CSRF/unsafe-request policy. |
| Request validation / ProblemDetails | yes | Route + document/comment DTO validation. |
| OpenAPI / generated artifacts | yes | API contract changes require generated artifact workflow. |
| Transaction / atomicity | yes | Supersede old proposal and create new version atomically. |
| No partial write | yes | Failed validation/lifecycle checks must not create proposal or change active version. |
| Concurrency / stale state | yes | Command re-checks exchange state even if UI looked sendable. |
| File/document boundary | yes | Stores document reference only; no binary upload. |
| Clock/audit actor fields | yes | Use server UTC time and authenticated actor id. |
| Privacy / cross-account data exposure | yes | Do not return private exchange/read data in command response. |
| Client feedback / accessibility | future | Future client sidecar owns form/button/error UX. |
| Redirect/page flow | future | Page-flow audit, not this server slice. |
| Testing responsibility split | yes | API integration + DB assertions; no repository mocks as primary proof. |

---

## 14. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Shared endpoint accepts Client and Employee | covered | `[Authorize(Roles = "Client,Employee")]`. |
| Client sends own version | covered | service calls `ClientSendOwnVersion`. |
| Employee sends new version | covered | service calls `EmployeeSendNewVersion`. |
| Client cannot act on another client's exchange | covered | domain checks `ClientAccountId`. |
| Any active Employee can service exchange | covered | no `ResponsibleEmployeeId` guard. |
| Proposal sender is stored per version | covered | `AgreementProposal.Author`. |
| Previous proposal is superseded | covered | domain transition. |
| Next version is created | covered | domain transition. |
| Turn switches after Client send | covered | status becomes `AwaitingEmployeeResponse`. |
| Turn switches after Employee send | covered | status becomes `AwaitingClientConfirmation`. |
| Wrong turn is rejected | covered | domain lifecycle errors. |
| Accepted/finally refused exchange cannot receive new version | covered | domain lifecycle errors. |
| No response DTO | covered | command returns `204 No Content`. |
| No binary upload | covered | document reference only. |
| Client/Employee UI | out of scope | future client sidecar. |
| Redirect/page flow after send | out of scope | future page-flow/redirect audit. |

---

## 15. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Client sends own version for own exchange | POST returns `204`; next Client proposal created | API integration + DB assertion | client auth fixture, exchange setup, HTTP POST, DB read | Low if proposal version/author/status asserted | Low/Medium: schema/helper refactor may affect DB assertion | `ClientSendProposalVersion_CreatesNextClientVersionAndReturnsNoContent` |
| Client cannot act on another client's exchange | returns `404/422`; exchange/proposals unchanged | API integration + no-mutation assertion | different client auth, HTTP POST, DB snapshot | Low if no-mutation asserted | Low/Medium | `ClientSendProposalVersion_WhenOtherClientExchange_ReturnsProblemAndDoesNotChangeExchange` |
| Employee sends new version after client version | POST returns `204`; next Employee proposal created | API integration + DB assertion | employee auth fixture, exchange setup, HTTP POST, DB read | Low if proposal version/author/status asserted | Low/Medium | `EmployeeSendProposalVersion_AfterClientVersion_CreatesNextEmployeeVersionAndReturnsNoContent` |
| Any active Employee can service exchange | different active Employee can send Employee version | API integration + DB assertion | exchange setup with prior employee proposal/client version, different employee auth | Low if author id asserted | Medium | `EmployeeSendProposalVersion_WithDifferentActiveEmployee_Succeeds` |
| Previous proposal is superseded | old active proposal state becomes SupersededByCounterProposal | API integration + DB assertion | HTTP POST, DB read | Low if old proposal state asserted | Low/Medium | success branch tests |
| Turn switches after Client send | exchange status becomes AwaitingEmployeeResponse | API integration + DB assertion | HTTP POST, DB read | Low | Low/Medium | client success test |
| Turn switches after Employee send | exchange status becomes AwaitingClientConfirmation | API integration + DB assertion | HTTP POST, DB read | Low | Low/Medium | employee success test |
| Wrong turn rejected | returns `422`; active version/proposal count unchanged | API integration + no-mutation assertion | wrong status/active sender setup, HTTP POST, DB snapshot | Low if count/active version/status asserted | Low/Medium | client twice / employee awaiting client tests |
| Accepted/finally refused exchange cannot receive new version | returns `422`; no new proposal | API integration + no-mutation assertion | terminal exchange setup, HTTP POST, DB snapshot | Low | Low/Medium | terminal lifecycle tests |
| Document required | missing/invalid document returns `422`; no new proposal | API integration + no-mutation assertion | body variants, DB read | Low if no proposal assertion exists | Low | document validation tests |
| No response DTO | command response has no body | API integration | HTTP response assertion | Low | Low | success tests |
| Unsafe command is protected | missing/invalid CSRF rejected by command family smoke | API integration smoke | POST without token | Medium if no no-mutation assertion | Low | one CSRF smoke; full matrix belongs to `CC-SEC-CSRF-001` |

### API boundary / access

```text
- unauthenticated send proposal returns 401;
- unsupported role returns 403;
- Client can send only for own exchange;
- Employee can send when lifecycle allows Employee response.
```

### Command success

```text
- POST returns 204 No Content;
- response body is empty;
- previous active proposal state = SupersededByCounterProposal;
- new proposal version is previous version + 1;
- ActiveProposalVersion becomes new version;
- new proposal stores document ref;
- optional comment stored only for meaningful text;
- author Sender/SenderId match current actor;
- exchange status switches to other side.
```

### Lifecycle / no-write tests

```text
- missing exchange returns mapped problem;
- invalid document/comment does not change exchange;
- wrong turn does not change active proposal/version/status;
- Accepted exchange does not receive new proposal;
- FinallyRefused exchange does not receive new proposal;
- Client acting on another client's exchange does not change exchange;
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
- proposal response DTO;
- initial exchange start;
- client accept active proposal;
- final refusal;
- binary file upload;
- client UI;
- redirect/page flow;
- repository mock call-order as primary proof;
- unit tests unless reusable helper/domain logic is introduced.
```

---

## 16. Backend Implementation Direction / Current Refactor Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[ ] Confirm endpoint is present or mark implementation drift.
[ ] Confirm success is 204 No Content.
[ ] Confirm no proposal response DTO exists.
[ ] Confirm route is request-scoped and does not confuse requestId with exchangeId.
[ ] Confirm controller accepts Client and Employee roles.
[ ] Confirm CSRF protection is present.
[ ] Confirm controller branches only by role to service method.
[ ] Confirm controller does not contain lifecycle logic.
[ ] Confirm DTO document is required and valid.
[ ] Confirm comment is optional and blank means no comment.
[ ] Confirm exchange is loaded by requestId with proposals.
[ ] Confirm Client branch calls ClientSendOwnVersion.
[ ] Confirm Employee branch calls EmployeeSendNewVersion.
[ ] Confirm ClientAccountId ownership is enforced by domain.
[ ] Confirm no ResponsibleEmployeeId guard is introduced.
[ ] Confirm proposal author Sender/SenderId are stored per version.
[ ] Confirm old proposal is superseded.
[ ] Confirm next version is created and active version updated.
[ ] Confirm exchange status switches to other side.
[ ] Confirm no request status mutation occurs.
[ ] Confirm lifecycle failures map to ProblemDetails.
[ ] Confirm no new per-command status enum is introduced in target direction.
[ ] Confirm focused API integration tests with DB state assertions exist.
[ ] Confirm OpenAPI/type generation is current if contract changed.
```

This pass did not perform implementation verification.

---

## 17. Historical Client Notes / Future Client Sidecar

The original draft is a shared server command endpoint. Client/Employee action/form behavior should be handled in a separate client sidecar.

Future client sidecar should own:

```text
features/agreement-exchange/send-proposal/api/sendAgreementProposalVersion.ts
features/agreement-exchange/send-proposal/model/useSendAgreementProposalVersionMutation.ts
shared Client/Employee send proposal action UI/form
document/comment client validation
role/status/active sender visibility only as UX
query invalidation/refetch for agreement exchange details/list
visible error/success feedback
page-flow/redirect behavior if any
```

Client API placement rule remains:

```text
command endpoint wrappers go to features/*/api;
do not add business wrappers to shared/api.
```

---

## 18. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/requests/{requestId}/agreement-exchange/proposals
request body: SendAgreementProposalVersionDto
responses:
  204
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
npm.cmd run check:api
```

---

## 19. Dependent / Follow-up Slices

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Separate later work:

```text
- client sidecar draft refactor;
- page flow / redirects audit;
- UI refactoring workflow.
```

---

## 20. Guardrail Summary

```text
This is one shared command endpoint first pass.
Route is POST /api/requests/{requestId}/agreement-exchange/proposals.
Do not use /api/agreement-exchanges/{requestId}/proposals because it suggests exchangeId.
Do not split Client/Employee endpoints unless behavior diverges later.
Do not introduce AgreementExchangeActor abstraction.
Use current role/current account id from session.
Controller may branch by role only to call service method.
Controller must not contain lifecycle logic.
Application service orchestrates loading/value object creation/domain calls.
Domain owns participant/lifecycle/turn invariants.
AgreementProposalExchange must store ClientAccountId.
Client actions must check client.Id == ClientAccountId.
Employee is not fixed exchange-level owner.
Any active Employee can service exchange first pass.
Proposal sender identity is tracked per proposal version.
No binary upload.
No response DTO.
No per-command status enum.
Failures go through existing Error/ProblemDetails mapping.
UI/page redirects are out of scope for this server draft refactor.
```
