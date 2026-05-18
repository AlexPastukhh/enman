# L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal

Status: implemented client-sidecar draft refactor / implementation not rechecked in this pass  
Parent server slice: `SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal`  
Host read sidecar: `L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages`  
Actor: ClientAccount  
Slice type: client command sidecar  
Placement: Client agreement exchange details action area only  
Draft refactor mode: docs-only; runtime implementation was not rechecked in this pass

Server contract direction:

```text
POST /api/agreement-exchanges/{exchangeId}/accept
no body
204 No Content
```

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was not rechecked in this pass.
Deep UI redesign, route/page-flow audit and redirects are out of scope for this pass.

No behavior decision from the previous draft was intentionally removed.
The old draft's details-only placement, Client-only boundary, optional confirmation,
no-body/204 contract, feature-owned API wrapper, no shared/api business wrapper,
no Employee accept, no new proposal version, details/list refetch and test expectations are preserved.
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
SC-13E — Agreement Final Refusal, as mutually exclusive action context
SC-14 — Agreement Documents, document reference context only
```

Server source:

```text
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
```

Host read source:

```text
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages
```

UI scenario:

```text
missing / pending dedicated UI source for Client Accept action.
This client draft is the current implementation-facing UI/action source until that UI source exists.
```

Cross-cutting behavior:

```text
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility
CC-SEC-CSRF-001 — Unsafe Command Protection, through shared API/fetch infrastructure
```

Data source:

```text
pending scenario-data source for action availability, accepted state copy and error copy.
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior labels until source-sync files are completed.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-13B: pending / v000 if source registry is applied
SL-AGR-EXCH-005: paired server draft refactored in this archive
```

Server/domain baseline:

```text
server accepts only authenticated Client;
server derives ClientAccountId from session;
server/domain enforce ClientAccountId ownership, exchange lifecycle and active proposal sender;
server returns 204 No Content;
client refetches details/list.
```

Slice derivation map:

```text
pending / add row for L2-AGR-EXCH-ACCEPT-001.client during source-sync map update.
```

Coverage snapshot:

| Behavior label / provisional behavior | Source/version | Server/domain disposition | This client slice responsibility | Notes |
|---|---|---|---|---|
| `AGR-ACCEPT-UI-B01` Client can trigger Accept from details | SC-13B pending | server owns command | render Client details action and call mutation | details action slot only |
| `AGR-ACCEPT-UI-B02` Accept visible only when available | SC-13B pending | server still authoritative | use actionAvailability if present; fallback derivation is UX only | no security via UI |
| `AGR-ACCEPT-UI-B03` Optional confirmation | UI/product pending | not server behavior | support confirm/cancel before mutation if enabled | not mandatory by server |
| `AGR-ACCEPT-UI-B04` No request body | SL-AGR-EXCH-005 | server contract | wrapper sends no JSON body | exchangeId route only |
| `AGR-ACCEPT-UI-B05` 204 success handled | SL-AGR-EXCH-005 | server returns no content | mutation returns void and refetches reads | no response DTO |
| `AGR-ACCEPT-UI-B06` Accepted state shown after refetch | SC-13B pending | details/list read DTOs | invalidate details/list queries | no optimistic final state |
| `AGR-ACCEPT-UI-B07` Error feedback visible | CC-CLIENT-FEEDBACK-001 | ProblemDetails from server | show command error, preserve previous state | stale-state safe |
| `AGR-ACCEPT-UI-B08` Employee accept not rendered | accepted boundary | server has no Employee accept here | only wire into Client details page | Employee details page must not show this action |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync / implementation-not-rechecked
```

Implemented files:

```text
client:
  not rechecked in this pass

server:
  paired server draft refactored in this archive:
    planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md

tests:
  not rechecked in this pass
```

Checked against:

```text
source versions:
  pending source-sync registry

server/domain baseline:
  current paired server draft

slice derivation map version:
  pending
```

Known drift corrected by this refactor:

```text
docs:
  - old client draft lacked Source / Domain / Slice Coverage Snapshot;
  - old client draft lacked Implementation Sync Status;
  - old client draft had verification plan but no Behavior-to-Test Trace;
  - old client draft lacked a dedicated Historical Server Notes / Contract Block;
  - old client draft was not marked as implemented-slice sync/refactor.

source:
  - stable behavior item IDs are not yet assigned in source registry.

implementation:
  - not checked in this pass.

UI:
  - deep route/page-flow/redirect audit was not performed.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection and no deep UI/redirect flow inspection.
```

---

## 1. Scope

This sidecar owns:

```text
- Client Accept active proposal action from agreement exchange details;
- first-pass placement only on Client agreement exchange details page;
- rendering Accept only when details state says or implies Client can accept;
- disabled/blocked state when accept is unavailable;
- visible unavailable reason when available;
- optional confirmation before final positive decision;
- submit command:
  POST /api/agreement-exchanges/{exchangeId}/accept;
- no request body;
- no response DTO;
- pending state while command is in flight;
- duplicate-submit protection;
- visible success/error feedback;
- refresh agreement exchange details after success;
- refresh agreement exchange list after success if cache exists;
- no Employee accept;
- no counter-proposal;
- no final refuse;
- no proposal version creation;
- no AcceptedAt UI/display requirement first pass.
```

This is a final positive decision by Client.

It accepts the current active Employee proposal.

It does not create a new proposal version and does not upload/send a proposal.

---

## 2. Out of Scope

```text
- backend endpoint implementation -> SL-AGR-EXCH-005;
- Agreement Exchange list read -> SL-AGR-EXCH-003;
- Agreement Exchange details read -> SL-AGR-EXCH-004;
- initial exchange creation -> SL-AGR-EXCH-001;
- counter-proposal version creation -> SL-AGR-EXCH-002;
- Employee accept active proposal;
- final refusal -> SL-AGR-EXCH-006;
- request lifecycle mutation after accept;
- document upload/download;
- local CSRF mechanics;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api;
- deep UI redesign;
- route/redirect audit beyond the existing placement direction.
```

Important guardrail:

```text
Employee accept is out of scope.
Do not infer or implement Employee accept from the shared details page.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
  Owns shared list read endpoint and list read model.

L2-AGR-EXCH-LIST-001.client
  Owns shared list query/model/widget and Client/Employee page shells.

SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
  Owns shared details read endpoint and details DTO.

L2-AGR-EXCH-DETAILS-001.client
  Owns shared details widget and Client/Employee details page shells.

SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
  Owns backend Client accept command:
    POST /api/agreement-exchanges/{exchangeId}/accept.

L2-AGR-EXCH-ACCEPT-001.client
  Owns Client Accept button/action/mutation.

SL-AGR-EXCH-006 / L2-AGR-EXCH-FINAL-REFUSE-001.client
  Own final refusal server/client behavior.

L2-AGR-EXCH-SEND-PROPOSAL-001.client
  Owns send proposal version action.
```

---

## 4. Visual UI / Scenario Flow

```text
Client opens agreement exchange details
        ↓
Details page shows active Employee proposal
        ↓
Client sees Accept action
        ↓
Client clicks Accept
        ↓
Optional confirmation appears
        ↓
Client confirms
        ↓
Accept command is sent
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ command accepted             │ command rejected             │
 ▼                              ▼
details/list refresh            error feedback visible
        ↓                       previous state remains safe
exchange status shows Accepted
active proposal state shows Accepted
```

In ordinary words:

Client opens the agreement exchange details page. If the active proposal was sent by Employee and exchange lifecycle allows acceptance, Client can accept it. On success, the UI refetches details/list and shows `AgreementExchangeStatus.Accepted` and `AgreementProposalState.Accepted`. On failure, the previous details state remains safe and an error is shown.

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Client | Opens Client agreement exchange details. |
| S02 | Details read state | Shows active Employee proposal. |
| S03 | Client action area | Shows Accept when available. |
| S04 | Optional confirmation | Confirms final positive decision if enabled. |
| S05 | Command feature | Sends Accept command with no body. |
| S06 | Accepted outcome | Details/list refetch and show Accepted state. |
| S07 | Rejected outcome | Error feedback visible; previous state remains safe. |

---

## 5. Visual Client Implementation Flow

```text
[Client Details Page Shell]
pages/agreements/details/ClientAgreementExchangeDetailsPage.tsx

Lives here:
  ClientAgreementExchangeDetailsPage

Uses:
  useAgreementExchangeDetailsQuery(exchangeId)
  AgreementExchangeDetailsView
  AcceptAgreementProposalButton through details action slot

Owns:
  client route/page shell;
  exchangeId route param parsing;
  client title/back link;
  passing viewerRole="Client";
  passing Accept action into details action slot when available.

Does not own:
  accept mutation;
  command endpoint wrapper;
  server lifecycle/security rules;
  Employee accept.
```

```text
[Shared Details Widget]
widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx

Owns:
  read layout;
  active proposal display;
  proposal history display;
  optional action slot placement.

Does not own:
  accept mutation;
  accept endpoint wrapper;
  lifecycle/security rules.
```

```text
[Command Feature UI]
features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton.tsx

Owns:
  Accept button;
  optional confirmation;
  pending state;
  disabled/unavailable state;
  command error feedback;
  accessible label/copy.

Does not own:
  details query;
  details page shell;
  counter/final-refuse commands.
```

```text
[Command Feature Model]
features/agreement-exchange/accept-proposal/model/useAcceptAgreementProposalMutation.ts

Owns:
  mutation;
  details/list invalidation after success;
  command error propagation to UI.

Uses:
  acceptAgreementProposal(exchangeId)
  agreementExchangeQueryKeys.details(exchangeId)
  agreementExchangeQueryKeys.list(...)
```

```text
[Command Feature API]
features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.ts

Owns:
  POST endpoint wrapper:
    POST /api/agreement-exchanges/{exchangeId}/accept

Uses:
  shared/api/fetchJson
```

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Client details page | Hosts Accept action in Client details action slot only. |
| I02 | Shared details widget | Renders read state and optional action slot; does not own mutation. |
| I03 | Feature UI | Button/confirmation/pending/error states. |
| I04 | Feature model | Sends mutation and invalidates details/list reads. |
| I05 | Feature API | Calls generated/known endpoint through `fetchJson`. |
| I06 | Shared API infra | Handles generic fetch, CSRF, ProblemDetails behavior. |

---

## 6. API Contract

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/accept
```

Auth:

```text
Client only
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

Feature-owned API wrapper:

```ts
// features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.ts
import { fetchJson } from "../../../../shared/api/fetchJson";

export const acceptAgreementProposal = (
  exchangeId: number,
): Promise<void> =>
  fetchJson<void>(
    `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}/accept`,
    { method: "POST" },
  );
```

Do not add:

```text
src/shared/api/agreementExchangeApi.ts
```

No `acceptAgreementProposalApiTypes.ts` is required first pass because there is no request/response DTO.

If generated OpenAPI operation aliases are later required by project convention, keep aliases near the feature, not in `shared/api`.

---

## 7. Validation / ProblemDetails / Feedback

Client-side validation:

```text
none for command payload because no body is sent.
```

Input validation:

```text
exchangeId route param must be present and numeric/positive according to route parsing conventions.
If route param is invalid/missing, page-level not-found/bad-route handling owns it.
```

ProblemDetails feedback:

```text
- show visible command error feedback on rejection;
- preserve previous details state;
- do not optimistically mark Accepted;
- do not blind-retry unsafe command after CSRF/session/security failure.
```

CSRF behavior:

```text
local CSRF mechanics are out of scope;
shared API/fetch infrastructure owns antiforgery token handling.
```

---

## 8. Expected Read State After Refetch

After success, client refetches details/list and expects:

```text
exchange.Status = AgreementExchangeStatus.Accepted
activeProposal.State = AgreementProposalState.Accepted
ActiveProposalVersion unchanged
proposal count unchanged
no new proposal version created
```

Do not use `Finalized` wording unless domain later introduces a separate status.

`acceptedAt` may be passed to the domain method server-side for future audit compatibility, but first-pass client does not require, display, or assert accepted timestamp unless details DTO exposes it later.

---

## 9. Action Availability

Preferred source:

```text
details.actionAvailability.canAcceptActiveProposal
```

Fallback display-only derivation if details DTO has no action availability first pass:

```text
viewerRole == "Client"
exchangeStatus == "AwaitingClientConfirmation"
activeProposal.sender == "Employee"
activeProposal.state == "AwaitingClientConfirmation"
```

This fallback is UX only.

Server must still enforce:

```text
Client role
ClientAccountId ownership
active proposal sender
exchange lifecycle
no-new-version rule
```

The server draft explicitly keeps ownership/lifecycle in domain/application rules and says accept does not create a new proposal version.

---

## 10. Security / Protection

Client sends only:

```text
exchangeId in route
```

Client never sends:

```text
clientId
employeeId
proposalId as authority
status
actor side
target state
acceptedAt
```

Client behavior:

```text
- shows disabled/hidden action for unavailable state;
- shows pending state while accepting;
- shows ProblemDetails/error feedback on rejection;
- refetches details/list after success;
- does not optimistically mark Accepted before server success.
```

Server remains authoritative:

```text
UI button visibility is not authorization.
```

---

## 11. Questions / Decisions

### Blocked

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-ACCEPT-CLIENT-001` | blocked | Exact generated operation name? | Use OpenAPI after server implementation/generation. |
| `Q-L2-AGR-ACCEPT-CLIENT-002` | blocked | Does generated OpenAPI expose this command as 204 no body? | Confirm after generation. |

### Accepted

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-ACCEPT-CLIENT-003` | accepted | Is this Client-only first pass? | Yes. Employee accept is out of scope. |
| `Q-L2-AGR-ACCEPT-CLIENT-004` | accepted | Does command send body? | No body. |
| `Q-L2-AGR-ACCEPT-CLIENT-005` | accepted | Does success return DTO? | No, `204 No Content`. |
| `Q-L2-AGR-ACCEPT-CLIENT-006` | accepted | Does accept create proposal version? | No. |
| `Q-L2-AGR-ACCEPT-CLIENT-007` | accepted | Which statuses after refetch? | `AgreementExchangeStatus.Accepted`, `AgreementProposalState.Accepted`. |
| `Q-L2-AGR-ACCEPT-CLIENT-008` | accepted | Does client require AcceptedAt? | No. |
| `Q-L2-AGR-ACCEPT-CLIENT-009` | accepted | Where does wrapper live? | `features/agreement-exchange/accept-proposal/api`. |
| `Q-L2-AGR-ACCEPT-CLIENT-010` | accepted | Placement? | Client agreement exchange details action area. |

### Future review

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-ACCEPT-CLIENT-011` | future review | Should confirmation be mandatory? | Component should support confirmation; product can decide. |
| `Q-L2-AGR-ACCEPT-CLIENT-012` | future review | Employee accept? | Out of first pass; only add with explicit domain/server support. |

---

## 12. Extension / Change Points

| Change point | Current direction | Future owner |
|---|---|---|
| Mandatory confirmation | Optional/supportable, not forced by server | product/client UX decision |
| Action availability DTO | Preferred read source if present | details read/server/client details sidecar |
| Generated operation aliases | Optional if project convention requires | client implementation after OpenAPI generation |
| AcceptedAt display | Not first pass | future details DTO/UI enhancement |
| Employee accept | Out of scope | future scenario/domain/server/client slices if needed |
| Route/page redirect after accept | Not audited in this pass | future page-flow audit |

---

## 13. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior | How client sidecar covers it | Status |
|---|---|---|
| Client can accept own active Employee proposal | Accept button sends command from Client details page. | target/current draft |
| Client cannot accept another Client's exchange | Server rejects; UI shows error/safe state. | target/current draft |
| Client cannot accept when lifecycle does not allow it | Button disabled if details says so; server still rejects stale attempts. | target/current draft |
| Accept does not create new version | Client sends accept command only, then refetches. | target/current draft |
| Active proposal becomes Accepted | Refetch shows `AgreementProposalState.Accepted`. | target/current draft |
| Exchange becomes Accepted | Refetch shows `AgreementExchangeStatus.Accepted`. | target/current draft |
| ActiveProposalVersion unchanged | Client does not create version and relies on refetch. | target/current draft |
| AcceptedAt not displayed | No UI expectation first pass. | target/current draft |
| Employee accept | Out of scope. | target/current draft |
| Counter-proposal | Out of scope. | target/current draft |
| Final refuse | Out of scope. | target/current draft |

---

## 14. Test / Verification Plan

Component tests:

```text
- Accept button renders enabled when available;
- Accept button renders disabled/hidden when unavailable;
- unavailable reason is visible when provided;
- optional confirmation can be confirmed/cancelled;
- click calls mutation with exchangeId;
- pending disables button;
- error feedback is visible;
- success callback/invalidation runs;
- button does not render counter/final-refuse controls;
- Employee details page does not render Client Accept action first pass.
```

API/model tests:

```text
- acceptAgreementProposal posts to /api/agreement-exchanges/{exchangeId}/accept;
- wrapper sends no request body;
- wrapper returns Promise<void> / handles 204;
- wrapper imports fetchJson from shared API infrastructure;
- mutation invalidates agreement exchange details query;
- mutation invalidates agreement exchange list query if present;
- no shared/api business wrapper exists.
```

E2E planned:

```text
Client session:
  open /agreements/:exchangeId
  active proposal was sent by Employee
  click Accept
  confirm if confirmation is enabled
  assert after refetch:
    exchange status shows Accepted
    active proposal state shows Accepted
```

Non-goals:

```text
- no Employee accept E2E here;
- no counter-proposal test here;
- no final-refuse test here;
- no AcceptedAt assertion first pass;
- no server DB internals in client tests;
- no React Query cache internals in E2E.
```

---

## 15. Behavior-to-Test Trace

| Behavior item | Client-visible outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Accept rendered only for Client action slot | Accept control appears in Client details only | component | Client page passes action slot | Employee page gets Client action | shared details wiring changes | Client renders; Employee does not render |
| Unavailable state safe | button hidden/disabled + reason if present | component | actionAvailability/fallback derivation | user attempts invalid action from UI | read DTO changes | unavailable state test |
| Confirmation supported | confirm triggers mutation; cancel does not | component | optional confirmation state | accidental mutation on cancel | button refactor | confirm/cancel tests |
| No body POST | wrapper sends POST with no body | API/model | feature-owned wrapper | server rejects unexpected body | fetch wrapper changes | wrapper request test |
| 204 handled | mutation resolves void and invalidates reads | model | `Promise<void>` wrapper + query invalidation | UI waits for DTO | OpenAPI generation changes | mutation success/invalidation test |
| Error feedback visible | ProblemDetails/error shown; previous state remains | component/model | mutation error propagation | silent failure | error boundary refactor | error feedback test |
| No optimistic Accepted state | UI refetches instead of setting final state | component/model | query invalidation only | false accepted UI on server reject | optimistic update added | no optimistic state assertion |
| No shared/api business wrapper | wrapper stays feature-owned | static/code review/test | import path convention | shared/api grows business endpoints | refactor moves wrapper | import/path test or review |

---

## 16. Implementation Direction / Current Refactor Checklist

```text
[ ] confirm SL-AGR-EXCH-005 backend endpoint exists
[ ] confirm generated route and 204 response
[ ] add/confirm feature-owned acceptAgreementProposal API wrapper
[ ] do not add request/response DTO alias unless generated/project convention requires it
[ ] add/confirm useAcceptAgreementProposalMutation
[ ] add/confirm AcceptAgreementProposalButton
[ ] wire button into Client agreement exchange details action slot only
[ ] do not render Client Accept on Employee details page first pass
[ ] send no request body
[ ] do not send clientId/employeeId/proposalId/status
[ ] refresh agreement exchange details after success
[ ] refresh agreement exchange list if present
[ ] show pending and error feedback
[ ] add component/API/model tests
[ ] add E2E after backend/test setup exists
```

---

## 17. Historical Server Notes / Contract Block

Server owner:

```text
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
```

Server contract:

```text
POST /api/agreement-exchanges/{exchangeId}/accept
Client-only
no request body
204 No Content
```

Server/domain guarantees expected by this sidecar:

```text
- current Client comes from session;
- ClientAccountId ownership enforced by server/domain;
- exchange lifecycle enforced by server/domain;
- active proposal sender/state enforced by server/domain;
- no new proposal version created;
- exchange/proposal accepted state persisted atomically;
- failures returned through existing ProblemDetails/Error mapping.
```

Client draft must not implement or duplicate these rules as authority.

---

## 18. OpenAPI / Generated Artifacts

Expected OpenAPI operation:

```text
POST /api/agreement-exchanges/{exchangeId}/accept
204 No Content
```

Client type generation expectation:

```text
No request/response DTO alias is required first pass because the command has no body and returns no body.
If the project generator creates operation types and implementation convention requires them, keep feature-local aliases near the accept feature.
```

Generated artifacts must come from repo commands, not manual edits.

---

## 19. Dependent / Follow-up Slices

```text
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
L2-AGR-EXCH-FINAL-REFUSE-001.client — Employee Final Refuse Agreement Exchange
future AcceptedAt/audit/details UI enhancement, if needed
future action availability DTO/read-side enhancement, if needed
```

---

## 20. Guardrail Summary

```text
Client Accept is a client command sidecar.

It is Client-only first pass.

It lives in Client agreement exchange details action area only.

It sends:
  POST /api/agreement-exchanges/{exchangeId}/accept

It sends no body.

It expects 204 No Content.

It does not create a new proposal version.

It does not upload a document.

It does not send a counter-proposal.

It does not implement Employee accept.

It does not implement final refusal.

It does not send clientId, employeeId, status, actor side, target state or acceptedAt.

It does not optimistically mark Accepted before server success.

It refetches details/list after success.

It uses feature-owned API wrapper, not business wrapper in shared/api.

Server remains authoritative for ownership, role, lifecycle and active proposal sender/state.
```
