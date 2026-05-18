# L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal

Status: implemented client-sidecar draft refactor / implementation not rechecked in this pass  
Parent server slice: `SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal`  
Host read sidecar: `L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages`  
Actor: ClientAccount  
Slice type: client command sidecar planning draft  
Placement: Client agreement exchange details action area only  
Architecture direction: feature-owned command wrapper/mutation/action, hosted by Client details page; runtime UI implementation not changed in this archive.

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
SC-13E — Agreement Final Refusal, out-of-scope terminal alternative
```

UI scenario:

```text
missing / pending dedicated UI source for Client accept action and confirmation behavior.
```

Server source:

```text
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
```

Cross-cutting behavior:

```text
CC-SEC-CSRF-001 — Unsafe Command Protection
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility
```

Data source:

```text
pending scenario-data source for visible accept action availability, disabled reason, error feedback and post-accept read refresh.
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior names until source-sync files are completed.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-13B: pending / v000 if source registry is applied
SL-AGR-EXCH-005: paired server draft in same archive
CC-CLIENT-FEEDBACK-001: pending / v001 if source registry is applied
CC-SEC-CSRF-001: v001 if source registry is applied
```

Server/domain baseline:

```text
server command is Client-only;
server/domain own ClientAccountId ownership, active proposal sender, lifecycle and no-new-version rules;
client action availability is UX only and not authorization.
```

Slice derivation map:

```text
pending / add row for L2-AGR-EXCH-ACCEPT-001.client during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Server/domain disposition | This client slice responsibility | Notes |
|---|---|---|---|---|
| Client sees Accept action when active Employee proposal can be accepted | SC-13B pending | server/domain remain authoritative | plan Client details action slot/button visibility | visible affordance only |
| Client sends accept command | SC-13B pending | server owns POST command and domain transition | feature API wrapper/mutation planning | no request body |
| Client does not send actor/state authority | SC-13B pending | server resolves Client from session | client sends only route exchangeId | no clientId/proposalId/status/acceptedAt |
| Pending duplicate-submit protection | CC-CLIENT-FEEDBACK pending | not server behavior | disable action while mutation pending | prevents UX double-click, not security |
| Error feedback on rejection | CC-CLIENT-FEEDBACK pending | server returns ProblemDetails/Error | show visible command error and keep previous state safe | stale UI can be rejected |
| Success refetches details/list | SC-13B/13A pending | server returns 204; reads expose Accepted state | invalidate/refetch details and list queries | no optimistic Accepted state first pass |
| No proposal version created | SC-13B pending | server/domain invariant | client does not render/send proposal form here | counter-proposal sidecar owns send |
| Employee accept not shown | SC-13B pending | server endpoint Client-only | Employee details page does not host Client Accept action | first-pass guardrail |
| Confirmation is optional/future-review | UI pending | not server behavior | component can support confirmation; product decision pending | preserve old future-review question |
| Runtime UI implementation not touched | current archive scope | not server behavior | planning draft only; no TSX/CSS/test changes | user said UI not to touch now |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync
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

server contract:
  pending generated OpenAPI confirmation

slice derivation map version:
  pending
```

Known drift:

```text
docs:
  - old client draft lacked Scenario Sources;
  - old client draft lacked Source / Domain / Slice Coverage Snapshot;
  - old client draft lacked Implementation Sync Status;
  - old test plan lacked Behavior-to-Test Trace with escape/refactor risk;
  - old draft mixed visual implementation planning with no explicit docs-only/runtime UI boundary.

source:
  - stable behavior item IDs and final UI scenario are not yet assigned.

implementation:
  - not checked in this pass.

UI:
  - runtime UI implementation, deep UI redesign, CSS changes and page-flow/redirect audit are not touched in this pass.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection and no page-flow/redirect audit.
```

---

## 1. Scope

This client sidecar owns planning for:

```text
- Client Accept active proposal action from agreement exchange details;
- first-pass placement only on Client agreement exchange details page;
- rendering Accept only when details read state says or implies Client can accept;
- disabled/blocked Accept state when action is unavailable;
- visible unavailable reason when available;
- optional confirmation before final positive decision;
- feature-owned command API wrapper:
  POST /api/agreement-exchanges/{exchangeId}/accept;
- no request body;
- pending state while command is in flight;
- duplicate-submit protection while pending;
- visible command validation/error feedback;
- visible success feedback or read-state refresh after success;
- refresh agreement exchange details after success;
- refresh agreement exchange list after success if cache exists;
- no optimistic Accepted state first pass;
- no Employee accept;
- no counter-proposal form;
- no final refuse action;
- no proposal version creation;
- no AcceptedAt UI/display requirement first pass.
```

This is a final positive decision by Client.

It accepts the current active Employee proposal.

It does **not** create a new proposal version and does **not** upload/send a proposal.

This archive does **not** implement runtime UI files.

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Backend endpoint implementation | `SL-AGR-EXCH-005` runtime implementation task |
| Agreement Exchange list read | `SL-AGR-EXCH-003` / `L2-AGR-EXCH-LIST-001.client` |
| Agreement Exchange details read | `SL-AGR-EXCH-004` / `L2-AGR-EXCH-DETAILS-001.client` |
| Initial exchange creation | `SL-AGR-EXCH-001` / start sidecar |
| Counter-proposal version creation | `SL-AGR-EXCH-002` / send-proposal sidecar |
| Employee accept active proposal | future source/domain decision only |
| Final refusal | `SL-AGR-EXCH-006` / final-refuse sidecar |
| Request lifecycle mutation after accept | future explicit source/domain decision only |
| Document upload/download | future document/file slices |
| Local CSRF mechanics implementation | shared/CSRF infrastructure / runtime implementation task |
| Manual generated OpenAPI/type edits | forbidden; generated workflow only |
| Business-specific wrappers in `shared/api` | forbidden for new client work |
| Runtime UI/TSX/CSS implementation | out of scope for this docs-only archive |
| Page-flow / redirect audit | separate UI/page-flow mode |
| Runtime tests | out of scope for this docs-only archive |

Important guardrail:

```text
Employee accept is out of scope.
Do not infer or implement Employee accept from the shared details page.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-003
  Owns shared list read endpoint and list read model.

L2-AGR-EXCH-LIST-001.client
  Owns shared list query/model/widget and Client/Employee page shells.

SL-AGR-EXCH-004
  Owns shared details read endpoint and details DTO.

L2-AGR-EXCH-DETAILS-001.client
  Owns shared details widget and Client/Employee details page shells/action slots.

SL-AGR-EXCH-005
  Owns backend Client accept command:
    POST /api/agreement-exchanges/{exchangeId}/accept.

L2-AGR-EXCH-ACCEPT-001.client
  Owns Client Accept button/action/mutation planning.

L2-AGR-EXCH-SEND-PROPOSAL-001.client
  Owns counter-proposal/send proposal action.

L2-AGR-EXCH-FINAL-REFUSE-001.client
  Owns Employee final refusal action.

shared/api
  Owns generic transport, ProblemDetails/ApiError, CSRF helpers and generated types only.
```

---

## 4. Visual UI / Scenario Flow

```text
Client opens agreement exchange details
        ↓
Details page shows active Employee proposal
        ↓
Client sees Accept action when details state allows it
        ↓
Client clicks Accept
        ↓
Optional confirmation appears if product enables confirmation
        ↓
Client confirms
        ↓
Accept command is sent with exchangeId only
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ command accepted             │ command rejected/stale       │
 ▼                              ▼
details/list refetch            error feedback visible
        ↓                       previous state remains safe
exchange status shows Accepted  no optimistic Accepted state
active proposal state Accepted
```

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Client | Opens Client agreement exchange details. |
| S02 | Details read state | Shows active Employee proposal and current status. |
| S03 | Client action area | Shows Accept when available or a disabled/unavailable state. |
| S04 | Optional confirmation | Confirms final positive decision if enabled. |
| S05 | Command feature | Sends POST accept with no body. |
| S06 | Accepted outcome | Invalidates/refetches details/list and shows Accepted state. |
| S07 | Rejected outcome | Shows error feedback; previous details state remains safe. |

---

## 5. Visual Layout / Screen Composition

This is a planning draft only. Runtime UI is not changed in this archive.

Target host composition:

```text
[ClientAgreementExchangeDetailsPage]
  PageHeader / BackLink
  AgreementExchangeDetailsView
    StatusPanel
    RequestSummary
    ActiveProposalPanel
    ProposalHistory
    ActionSlot
      AcceptAgreementProposalButton
  FeedbackArea
```

Visible states to plan for future implementation:

```text
loading:
  details page loading state from details read sidecar

not found / access denied:
  details page state from details read sidecar

available action:
  Accept visible in Client action area when details/read state allows it

unavailable action:
  disabled/hidden Accept with reason if server/read model supplies reason

pending:
  Accept disabled; duplicate-submit blocked

error:
  command ProblemDetails/error visible near action area or page feedback area

success:
  details/list refetch; Accepted state visible after server read confirms it
```

No runtime CSS/page layout is changed by this archive.

---

## 6. Visual Client Implementation Flow

### Client details page shell

```text
ClientAgreementExchangeDetailsPage
  from: pages/agreements/details/ClientAgreementExchangeDetailsPage.tsx
  needed to: host the Client agreement exchange details route and pass Client action slot into shared details view.
  visual: page-level details screen with page header, back link and details content area.
```

Uses:

```text
useAgreementExchangeDetailsQuery
  from: entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.ts
  needed to: load the current exchange details/read state before action availability is shown.

AgreementExchangeDetailsView
  from: widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
  needed to: render shared exchange status, active proposal and proposal history.
  visual: main details content block that hosts optional action slot.

AcceptAgreementProposalButton
  from: features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton.tsx
  needed to: render the Client-only accept action when hosted by the Client details page.
  visual: action button/confirmation/error block inside the details action area.
```

### Shared details widget

```text
AgreementExchangeDetailsView
  from: widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
  needed to: expose action slot without owning command mutations.
  visual: details layout; action slot appears near active proposal/action area.
```

Does not own:

```text
accept mutation;
accept endpoint wrapper;
server lifecycle/security rules;
Employee accept.
```

### Command feature UI

```text
AcceptAgreementProposalButton
  from: features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton.tsx
  needed to: provide Client Accept button, optional confirmation, pending/disabled/error/success feedback.
  visual: Client-only action control with accessible label and inline feedback.
```

Uses:

```text
useAcceptAgreementProposalMutation
  from: features/agreement-exchange/accept-proposal/model/useAcceptAgreementProposalMutation.ts
  needed to: run POST command and invalidate/refetch relevant reads.
```

### Command feature model

```text
useAcceptAgreementProposalMutation
  from: features/agreement-exchange/accept-proposal/model/useAcceptAgreementProposalMutation.ts
  needed to: own mutation state, command error propagation, and query invalidation after success.
```

Uses:

```text
acceptAgreementProposal
  from: features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.ts
  needed to: call the server command endpoint.

agreementExchangeQueryKeys
  from: entities/agreement-exchange/model/agreementExchangeQueryKeys.ts
  needed to: invalidate details/list reads after success.
```

### Command feature API

```text
acceptAgreementProposal
  from: features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.ts
  needed to: wrap POST /api/agreement-exchanges/{exchangeId}/accept outside shared/api.
```

Uses:

```text
fetchJson
  from: shared/api/fetchJson.ts
  needed to: execute generic POST request and parse ProblemDetails/ApiError behavior.
```

---

## 7. Styling / CSS Ownership

This archive does not change runtime CSS.

Future implementation ownership:

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Details page layout | page | `pages/agreements/details/*.css` | page container, spacing, page header/back link only |
| Shared details view | widget | `widgets/agreement-exchange-details/*.css` | details sections, history layout, action slot placement |
| Accept action | feature | `features/agreement-exchange/accept-proposal/ui/*.css` | button, confirmation, pending/error feedback only |
| Business display | entity/widget | existing entity/widget CSS | read-only proposal/status display |
| Tokens/base | global | `styles/*.css` | tokens/reset/base/app shell only |

Checklist for future runtime UI implementation:

```text
[ ] no broad global selector
[ ] no hover layout shift
[ ] no border-width change on hover
[ ] no feature CSS changes app shell
[ ] no page CSS reaches into feature internals
[ ] no business-specific CSS in shared/ui
[ ] uses tokens for color/spacing/radius where possible
[ ] pending/error/success states styled
[ ] manual visual checks listed
```

---

## 8. Client API / Server Contract

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/accept
```

Auth:

```text
Client only; server derives actor from session.
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

No request/response DTO alias is needed first pass because there is no body and no success body. If generated OpenAPI operation aliases are later required by project convention, keep aliases near the feature, not in `shared/api`.

---

## 9. Validation / Feedback / Error UI

Fields:

```text
No form fields first pass.
No body fields.
No field-level validation.
```

Submit:

```text
Accept sends route exchangeId only.
Client never sends clientId, employeeId, proposalId, status, actorSide, target state or acceptedAt.
```

Pending state:

```text
Disable Accept while mutation is in flight.
Prevent duplicate submit while pending.
Do not optimistically mark Accepted before server success.
```

Error feedback:

```text
Show server ProblemDetails/ApiError as visible command feedback.
Keep previous details state visible/safe after failure.
If details state is stale, server rejection is shown and details may be refetched if useful.
```

Success feedback:

```text
Invalidate/refetch exchange details.
Invalidate/refetch exchange list if cache exists.
Accepted state is shown only after refetch/read state confirms it.
```

CSRF/session/security failures:

```text
Do not blindly retry after CSRF/session/security failure.
Show visible error or session-expired flow according to shared error handling.
```

---

## 10. Accessibility / ARIA Contract

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|
| Accept action | `button` | visible text such as `Accept proposal` / localized copy | Enter/Space activate when enabled | no unless confirmation/status requires relation | `getByRole('button', { name: /accept/i })` |
| Confirmation dialog, if enabled | `dialog` / modal pattern | heading text | focus trapped; Escape/cancel closes | `aria-modal`, labelledby if custom dialog | `getByRole('dialog', { name: /accept/i })` |
| Pending feedback | text/status region | visible pending copy | no keyboard action | optional `aria-live="polite"` | `getByText(/pending|accepting/i)` |
| Error feedback | alert/status | ProblemDetails/error summary | focus remains safe; retry possible after user action | `role="alert"` or shared error component | `getByRole('alert')` |
| Disabled/unavailable reason | text near action | visible reason | disabled button not focusable unless design chooses explanation link | `aria-describedby` if button remains focusable | `getByText(reason)` |

Accessibility guardrail:

```text
Button visibility/disabled state is UX only.
Server remains authoritative for authorization and lifecycle.
```

---

## 11. Cross-Cutting Concerns

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | server-owned | Client does not send actor ids. |
| Authorization/visibility | server-owned | UI action visibility is not authorization. |
| Antiforgery / unsafe request | yes | Feature uses shared unsafe request/fetch boundary; full mechanics owned by CSRF infrastructure. |
| Validation / ProblemDetails | yes | Show visible command errors; no local field validation first pass. |
| OpenAPI / generated artifacts | yes | Wrapper follows generated route once confirmed; no manual generated edits. |
| Transaction/no partial write | server-owned | Client refetches; does not decide state. |
| Idempotency/double-submit | yes | UI blocks duplicate submit while pending; server handles stale/repeat command. |
| Concurrency/stale read | yes | Stale UI may show Accept; server rejection must be visible. |
| Privacy/cross-account exposure | server-owned | Not enforced by client. |
| Client feedback/accessibility | yes | Pending/error/success feedback and accessible action. |
| Redirect/page flow | no | Out of scope for this docs-only pass. |
| Runtime UI implementation | no | Planning only; no TSX/CSS/test files changed. |

---

## 12. Questions / Decisions

All original client question IDs are preserved with their original meaning.

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `Q-L2-AGR-ACCEPT-CLIENT-001` | blocked | Exact generated operation name? | Use OpenAPI after server implementation. | Feature wrapper naming can be adjusted after generation. |
| `Q-L2-AGR-ACCEPT-CLIENT-002` | blocked | Does generated OpenAPI expose this command as 204 no body? | Confirm after generation. | Client wrapper remains `Promise<void>` if 204/no body. |
| `Q-L2-AGR-ACCEPT-CLIENT-003` | accepted | Is this Client-only first pass? | Yes. Employee accept is out of scope. | Do not render Client Accept on Employee page. |
| `Q-L2-AGR-ACCEPT-CLIENT-004` | accepted | Does command send body? | No body. | Send only route exchangeId. |
| `Q-L2-AGR-ACCEPT-CLIENT-005` | accepted | Does success return DTO? | No, `204 No Content`. | Refetch reads after success. |
| `Q-L2-AGR-ACCEPT-CLIENT-006` | accepted | Does accept create proposal version? | No. | No proposal form/body here. |
| `Q-L2-AGR-ACCEPT-CLIENT-007` | accepted | Which statuses after refetch? | `AgreementExchangeStatus.Accepted`, `AgreementProposalState.Accepted`. | Avoid `Finalized` wording. |
| `Q-L2-AGR-ACCEPT-CLIENT-008` | accepted | Does client require AcceptedAt? | No. | No first-pass timestamp UI/test expectation. |
| `Q-L2-AGR-ACCEPT-CLIENT-009` | accepted | Where does wrapper live? | `features/agreement-exchange/accept-proposal/api`. | Business command wrapper stays out of `shared/api`. |
| `Q-L2-AGR-ACCEPT-CLIENT-010` | accepted | Placement? | Client agreement exchange details action area. | Details page hosts action slot. |
| `Q-L2-AGR-ACCEPT-CLIENT-011` | future review | Should confirmation be mandatory? | Component should support confirmation; product can decide. | Keep optional confirmation extension point. |
| `Q-L2-AGR-ACCEPT-CLIENT-012` | future review | Employee accept? | Out of first pass; only add with explicit domain/server support. | Do not infer from shared details page. |

---

## 13. Extension / Change Points

```text
- Optional confirmation can become mandatory by product decision without changing server contract.
- Details read may later expose `availableActions` / `canAcceptActiveProposal`.
- AcceptedAt can be displayed only after domain/persistence/API expose it explicitly.
- Employee accept requires explicit source/domain/server slice, not client inference.
- Page-flow/redirect behavior after success is separate audit/workflow.
- A shared command feedback component can be introduced through client cross-cutting feedback work.
```

---

## 14. Behavior Coverage

| Source / draft behavior | Status | How client sidecar covers it |
|---|---|---|
| Client can accept own active Employee proposal | covered | Client details action sends accept command. |
| Client cannot accept another Client's exchange | server-owned | UI shows error/safe state on rejection; no client-side authority. |
| Client cannot accept when lifecycle does not allow it | supported | Action disabled if read state says unavailable; server rejects stale attempts. |
| Accept sends no body | covered | Feature wrapper sends route exchangeId only. |
| Accept returns no DTO | covered | Mutation expects void/204 and refetches reads. |
| Accept does not create proposal version | covered by behavior planning | No proposal form/body; refetch shows unchanged version/count. |
| Active proposal becomes Accepted | read after success | Details/list refetch shows server state. |
| Exchange becomes Accepted | read after success | Details/list refetch shows server state. |
| AcceptedAt not displayed | covered | No UI expectation first pass. |
| Employee accept | out of scope | Do not render on Employee page. |
| Counter-proposal | out of scope | Send-proposal sidecar. |
| Final refuse | out of scope | Final-refuse sidecar. |
| Runtime UI implementation | out of archive scope | Planning draft only. |

---

## 15. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and visible client outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Visible/client outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Accept action visible when available | Client sees Accept action in details action area | component/page test | render Client details with mocked details state | Medium: does not prove server permission | Low if tested by accessible role/name | `ClientAgreementExchangeDetails_ShowsAcceptWhenAvailable` |
| Unavailable action is disabled/hidden with reason | Client sees disabled/hidden action and reason | component test | render button with unavailable props/read state | Medium | Low | `AcceptAgreementProposalButton_ShowsUnavailableReason` |
| Optional confirmation can be confirmed/cancelled | confirmation does not submit until confirmed; cancel closes | component test | user click, dialog interaction | Low for UI behavior | Medium if dialog implementation changes | `AcceptAgreementProposalButton_RequiresConfirmationWhenEnabled` |
| Accept sends command with exchangeId only | mutation calls feature wrapper with route exchangeId; no body | API/model test | mock fetchJson/wrapper call | Medium: wrapper test doesn't prove server behavior | Medium if over-asserting fetch internals; keep endpoint/body stable only | `acceptAgreementProposal_PostsNoBodyToAcceptEndpoint` |
| Pending blocks duplicate submit | button disabled while pending | component test | mocked mutation pending state | Low for UX double-click behavior | Low | `AcceptAgreementProposalButton_DisablesWhilePending` |
| Server rejection shows visible error | ProblemDetails/error visible and previous state remains | component test | mocked mutation error | Medium: does not prove real server mapping | Low | `AcceptAgreementProposalButton_ShowsErrorOnFailure` |
| Success refetches details/list | accepted state appears after read refresh or invalidation callback runs | component/model test | mocked mutation success and query invalidation | Medium: invalidation test may be implementation-coupled | Medium | `useAcceptAgreementProposalMutation_InvalidatesDetailsAndList` |
| No optimistic Accepted state first pass | UI does not mark Accepted before server/read success | component test | pending state before success | Medium | Low/Medium | `AcceptAgreementProposal_DoesNotOptimisticallyMarkAccepted` |
| Employee details page does not render Client Accept | Employee page lacks Client Accept action | page/component test | render Employee details page/read state | Medium | Low | `EmployeeAgreementExchangeDetails_DoesNotRenderClientAccept` |
| E2E Client accept | Client accepts proposal and sees Accepted after refetch | E2E planned | browser + real API/test seed | Low for integrated flow | Medium | future E2E |

### Component tests

```text
- Accept button renders enabled when available.
- Accept button renders disabled/hidden when unavailable.
- Unavailable reason is visible when provided.
- Optional confirmation can be confirmed/cancelled.
- Click calls mutation with exchangeId.
- Pending disables button.
- Error feedback is visible.
- Success callback/invalidation/refetch path is triggered.
- Button does not render counter/final-refuse controls.
- Employee details page does not render Client Accept action first pass.
```

### API/model tests

```text
- acceptAgreementProposal posts to /api/agreement-exchanges/{exchangeId}/accept.
- wrapper sends no request body.
- wrapper returns Promise<void> / handles 204.
- wrapper imports fetchJson from shared API infrastructure.
- mutation invalidates agreement exchange details query.
- mutation invalidates agreement exchange list query if present.
- no shared/api business wrapper exists.
```

### E2E planned

```text
Client session:
  open Client agreement exchange details
  active proposal was sent by Employee
  click Accept
  confirm if confirmation is enabled
  assert after refetch:
    exchange status shows Accepted
    active proposal state shows Accepted
```

### Non-goals

```text
- no Employee accept E2E here;
- no counter-proposal test here;
- no final-refuse test here;
- no AcceptedAt assertion first pass;
- no server DB internals in client tests;
- no React Query cache internals in E2E;
- no page-flow/redirect audit in this pass.
```

---

## 16. Suggested File Placement

Future implementation only; not changed by this archive.

```text
src/features/agreement-exchange/accept-proposal/api/
  acceptAgreementProposal.ts

src/features/agreement-exchange/accept-proposal/model/
  useAcceptAgreementProposalMutation.ts

src/features/agreement-exchange/accept-proposal/ui/
  AcceptAgreementProposalButton.tsx
  acceptAgreementProposalButtonConst.ts
  acceptAgreementProposalButton.css

src/pages/agreements/details/
  ClientAgreementExchangeDetailsPage.tsx

src/widgets/agreement-exchange-details/
  AgreementExchangeDetailsView.tsx

src/entities/agreement-exchange/model/
  agreementExchangeQueryKeys.ts

src/shared/api/
  fetchJson.ts
  generated/openapi-types.ts
  generic ProblemDetails / ApiError / CSRF helpers only
```

Do not add:

```text
src/shared/api/agreementExchangeApi.ts
src/features/agreement-exchange/accept-proposal/api/acceptAgreementProposalApiTypes.ts
```

---

## 17. Implementation Checklist / Current Refactor Checklist

```text
[ ] Confirm SL-AGR-EXCH-005 backend endpoint exists or mark implementation drift.
[ ] Confirm generated route and 204 response.
[ ] Confirm generated OpenAPI exposes no request body / no response body.
[ ] Add/confirm feature-owned acceptAgreementProposal API wrapper.
[ ] Do not add request/response DTO alias unless generated/project convention requires it.
[ ] Add/confirm useAcceptAgreementProposalMutation.
[ ] Add/confirm AcceptAgreementProposalButton.
[ ] Wire button into Client agreement exchange details action slot only.
[ ] Do not render Client Accept on Employee details page first pass.
[ ] Send no request body.
[ ] Do not send clientId/employeeId/proposalId/status/acceptedAt.
[ ] Refresh agreement exchange details after success.
[ ] Refresh agreement exchange list if present.
[ ] Show pending and error feedback.
[ ] Do not optimistically mark Accepted before read refresh.
[ ] Add component/API/model tests.
[ ] Add E2E after backend/test setup exists.
```

This pass did not perform implementation verification.

---

## 18. Next Step / Guardrail Summary

Next implementation step, when user enters runtime implementation mode:

```text
1. Confirm/apply SL-AGR-EXCH-005 backend endpoint.
2. Run OpenAPI/type generation.
3. Confirm generated path/operation and 204 response.
4. Implement feature-owned API wrapper.
5. Implement mutation.
6. Implement AcceptAgreementProposalButton.
7. Wire into Client agreement exchange details action slot only.
8. Add component/API/model tests.
9. Add E2E when backend/test setup is ready.
```

Guardrails:

```text
Client Accept is Client-only first pass.
Runtime UI implementation was not touched in this docs-only archive.
Do not render Client Accept on Employee details page.
Do not infer Employee accept from shared details UI.
Feature wrapper lives under features/agreement-exchange/accept-proposal/api.
Business endpoint wrapper does not live in shared/api.
Command sends no body and expects 204 No Content.
Client sends only exchangeId in route.
Client does not send actor ids, proposal id, status, target state or acceptedAt.
Client UI action availability is not authorization.
Server/domain own ClientAccountId ownership, active proposal sender and lifecycle checks.
Accept does not create proposal version.
Accepted state appears after server success/read refetch.
AcceptedAt is not required first pass.
Page-flow/redirect audit is separate later work.
```
