# L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages

Status: implemented client-sidecar draft refactor / implementation not rechecked in this pass  
Parent server slice: `SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details`  
Slice type: client read sidecar  
Actors: ClientAccount, Employee  
Architecture direction: shared entity read/query/details widget, separate actor page shells/routes.

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass paired with the server details read draft.

Runtime implementation was not rechecked in this pass.
Runtime UI, deep UI redesign and page-flow/redirect audit are out of scope for this pass.
```

---

## 0. Scenario Sources

Business scenarios:

```text
SC-13B — Client Agreement Proposal Details / Response
SC-13C — Employee Agreements
SC-13D — Employee Agreement Proposal Create / Send Version, for proposal-history semantics
SC-13E — Agreement Final Refusal, for final-state/details context
```

UI scenario:

```text
missing / pending dedicated UI source for Client and Employee agreement exchange details pages
```

Server source:

```text
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
```

Cross-cutting behavior:

```text
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility
CC-CLIENT-FORM-VALIDATION-001 — not directly applicable unless filters/forms are added later
```

Data source:

```text
pending scenario-data source for details fields, proposal history display, empty/not-found/access copy and document reference display
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
SC-13C: pending / v000 if source registry is applied
SC-13D: pending / v000 if source registry is applied
SC-13E: pending / v000 if source registry is applied
SL-AGR-EXCH-004: paired server draft in same archive
```

Server/domain baseline:

```text
server details endpoint must filter by current session role/account;
client-side visibility is UX only and not security;
details returns document references, not file bytes.
```

Slice derivation map:

```text
pending / add row for L2-AGR-EXCH-DETAILS-001.client during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Server/domain disposition | This client slice responsibility | Notes |
|---|---|---|---|---|
| Client opens own exchange details | SC-13B pending | server filters by ClientAccountId | render Client details page shell and call shared query | no client-side owner filtering as security |
| Employee opens visible exchange details | SC-13C pending | server returns employee-visible details | render Employee details page shell and call shared query | any active Employee first pass |
| Exchange status is visible | SC-13B/13C pending | server returns status | render status panel/field | exact enum/string follows generated OpenAPI |
| Request summary is visible | SC-13B/13C pending | server returns compact request summary | render request summary block | not full request details page |
| Active proposal is visible | SC-13B/13D pending | server returns active proposal | render active proposal panel | no command mutation |
| Full proposal history is visible | SC-13B/13D pending | server returns proposal versions | render proposal history ordered as response gives | list slice remains summary-only |
| Document refs are visible | SC-14 pending | server returns metadata refs | render document metadata/reference rows only | no bytes/download behavior |
| Actor-specific page shell/copy | UI source pending | not server behavior | Client/Employee shells provide copy/back links | deep UI redesign out of scope |
| Optional command action slots | command slices | commands own mutations and security | expose planning placement only; no command execution | future sidecars |
| Loading/error/not-found/success states | cross-cutting UI | server returns HTTP status/problem | render visible states | detailed copy pending UI source |

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
    planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md

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
  - old client draft lacked Scenario Sources section;
  - old client draft lacked Source / Domain / Slice Coverage Snapshot;
  - old client draft lacked Implementation Sync Status;
  - old verification plan lacked Behavior-to-Test Trace with escape/refactor risk;
  - old visual implementation flow used ownership language but not the current `from`, `needed to`, `visual` structure;
  - old draft did not explicitly mark runtime UI/page-flow/redirect audit as out of scope.

source:
  - stable behavior item IDs and final UI scenario are not yet assigned.

implementation:
  - not checked in this pass.

UI:
  - runtime UI, deep visual redesign and redirects are not touched in this pass.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection and no page-flow/redirect audit.
```

---

## 1. Scope

This client sidecar owns planning for:

```text
- shared Agreement Exchange details query/model;
- shared Agreement Exchange details widget;
- Client agreement exchange details page shell;
- Employee agreement exchange details page shell;
- role/page-specific title, empty/not-found/access copy and navigation;
- loading/error/not-found/success read states;
- exchange status display;
- request summary display;
- active proposal display;
- full proposal version history display;
- proposal sender/senderId display;
- document references display, not file bytes;
- currentActorSide display/helper usage if server contract exposes it;
- optional action slot placement for future command sidecars;
- no command execution in this read sidecar.
```

The details page shows full proposal history.

The list page stays summary-only.

This docs-only pass does not implement runtime UI.

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Backend endpoint implementation | `SL-AGR-EXCH-004` implementation work |
| Agreement Exchange list page/read list | `SL-AGR-EXCH-003` / `L2-AGR-EXCH-LIST-001.client` |
| Start Agreement Exchange command | `SL-AGR-EXCH-001` / start command client sidecar |
| Send counter-proposal command | `SL-AGR-EXCH-002` / send proposal sidecar |
| Client accept active proposal | `SL-AGR-EXCH-005` / accept sidecar |
| Employee final refusal | `SL-AGR-EXCH-006` / final-refuse sidecar |
| File download / binary document serving | future document/file slice |
| Document upload/storage | future document/storage slice / `CC-DOC-*` family |
| Local CSRF mechanics | shared/cross-cutting unsafe command concern; not safe GET details |
| Manual generated OpenAPI/type edits | generated artifact workflow only |
| Business-specific wrappers in `shared/api` | forbidden; use entity API wrapper |
| Actor-specific API wrappers while response shape is common | explicitly not first pass |
| Runtime UI implementation/refactor | separate implementation mode |
| Deep UI/page-flow/redirect audit | separate future audit |

Important:

```text
Details read may show action slots, but it does not execute actions.
Command features own their own buttons/forms/mutations.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-003
  Owns shared backend list endpoint/read model.

L2-AGR-EXCH-LIST-001.client
  Owns shared list query/model/list widget and actor page shells.

SL-AGR-EXCH-004
  Owns shared backend details endpoint/read model.

L2-AGR-EXCH-DETAILS-001.client
  Owns shared client details query/model/details widget and actor page shells.

L2-AGR-EXCH-START-001.client
  Owns Employee start exchange action from request details, not this details read.

L2-AGR-EXCH-SEND-PROPOSAL-001.client
  Owns send proposal action in exchange details.

L2-AGR-EXCH-ACCEPT-001.client
  Owns Client accept action in exchange details.

L2-AGR-EXCH-FINAL-REFUSE-001.client
  Owns Employee final refuse action in exchange details.

shared/api
  Owns generic transport, ProblemDetails/ApiError, CSRF helpers and generated types only.
```

---

## 4. Visual UI / Scenario Flow

```text
[Signed-in Client]
opens one agreement exchange details page
        ↓
[Client Page Shell]
uses shared agreement exchange details query
        ↓
[AgreementExchangeDetailsView]
shows exchange status, request summary,
active proposal and proposal history
        ↓
Client may see future client action slots
if command sidecars are wired


[Signed-in Employee]
opens one agreement exchange details page
        ↓
[Employee Page Shell]
uses shared agreement exchange details query
        ↓
[AgreementExchangeDetailsView]
shows exchange status, request summary,
active proposal and proposal history
        ↓
Employee may see future employee action slots
if command sidecars are wired
```

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Client page shell | Opens client exchange details route. |
| S02 | Employee page shell | Opens employee exchange details route. |
| S03 | Shared query | Calls shared details endpoint with `exchangeId`. |
| S04 | Server | Filters details by session role/visibility. |
| S05 | Shared details widget | Renders exchange status, request summary, active proposal, proposal history. |
| S06 | Page shell | Provides actor-specific title, back link and future action placement. |
| S07 | Future action slot | Command sidecars may render role/status-specific actions later. |

This section preserves the old visual flow direction but remains a planning draft. Runtime UI is not changed in this archive.

---

## 5. Visual Layout / Screen Composition

Target first-pass composition:

```text
[AppShell]
  Header / Navigation
  main.pageContainer
    PageHeader
      title / status context
      back link
    DetailsStateBoundary
      loading state
      error/not-found/access state
      success state
        AgreementExchangeDetailsView
          StatusPanel
          RequestSummary
          ActiveProposalPanel
          ProposalHistory
          DocumentRefList
          ActionSlot (empty or future sidecars)
```

States:

```text
loading:
  visible loading message/skeleton owned by page/widget implementation later

not-found/access:
  visible not-found/access copy; exact copy pending UI source

error:
  visible ProblemDetails/ApiError feedback

success:
  details content is visible with no command form by default
```

Responsive/styling note:

```text
Deep UI redesign and CSS changes are not part of this docs-only archive.
```

---

## 6. Visual Client Implementation Flow

### Client Page Shell

```text
ClientAgreementExchangeDetailsPage
  from: pages/agreements/details/ClientAgreementExchangeDetailsPage.tsx
  needed to: compose the Client details route with shared query/details widget and Client-specific copy/navigation.
  visual: page-level container, Client title/back link to "Мои договоры", visible loading/error/not-found/success area.
```

Uses:

```text
useAgreementExchangeDetailsQuery
  from: entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.ts
  needed to: load one shared agreement exchange details payload through the entity model layer.

AgreementExchangeDetailsView
  from: widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
  needed to: render common details content and optional future action slot.
  visual: main details block inside the page content area.
```

### Employee Page Shell

```text
EmployeeAgreementExchangeDetailsPage
  from: pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx
  needed to: compose the Employee details route with shared query/details widget and Employee-specific copy/navigation.
  visual: page-level container, Employee title/back link to Employee agreement exchanges dashboard, visible loading/error/not-found/success area.
```

Uses:

```text
useAgreementExchangeDetailsQuery
  from: entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.ts
  needed to: load the same shared server details endpoint.

AgreementExchangeDetailsView
  from: widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
  needed to: render common details content and optional future action slot.
  visual: main details block inside the page content area.
```

### Entity API Layer

```text
getAgreementExchangeDetails
  from: entities/agreement-exchange/api/getAgreementExchangeDetails.ts
  needed to: wrap the shared details endpoint and keep business endpoint wrappers out of shared/api.
```

Uses:

```text
fetchJson
  from: shared/api/fetchJson.ts
  needed to: execute generic GET request and parse response/errors.

AgreementExchangeDetails
  from: entities/agreement-exchange/api/agreementExchangeApiTypes.ts
  needed to: expose generated OpenAPI DTO aliases near the entity.
```

### Entity Model Layer

```text
useAgreementExchangeDetailsQuery
  from: entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.ts
  needed to: own React Query loading/error/success state for one exchange details payload.

agreementExchangeQueryKeys
  from: entities/agreement-exchange/model/agreementExchangeQueryKeys.ts
  needed to: centralize query keys for list/details invalidation.

agreementExchangeTypes
  from: entities/agreement-exchange/model/agreementExchangeTypes.ts
  needed to: hold small view-model helpers only if generated DTOs need local shape normalization.
```

### Widget Layer

```text
AgreementExchangeDetailsView
  from: widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
  needed to: render shared details shell and delegate sections.
  visual: readable details layout with status, request summary, active proposal and history sections.

AgreementExchangeStatusPanel
  from: widgets/agreement-exchange-details/AgreementExchangeStatusPanel.tsx
  needed to: render exchange status and high-level state.
  visual: compact status area near top of details.

AgreementExchangeRequestSummary
  from: widgets/agreement-exchange-details/AgreementExchangeRequestSummary.tsx
  needed to: render compact request context included in details DTO.
  visual: summary block, not full request details page.

AgreementActiveProposalPanel
  from: widgets/agreement-exchange-details/AgreementActiveProposalPanel.tsx
  needed to: render active proposal version/sender/document/comment summary.
  visual: prominent active proposal section.

AgreementProposalHistory
  from: widgets/agreement-exchange-details/AgreementProposalHistory.tsx
  needed to: render all proposal versions returned by server.
  visual: ordered history/timeline/list section.

AgreementDocumentRefList
  from: widgets/agreement-exchange-details/AgreementDocumentRefList.tsx
  needed to: render document metadata refs without bytes/download behavior.
  visual: simple metadata list/links placeholder if future file slice supplies URLs.
```

### Shared API Infrastructure

```text
fetchJson / ApiError / ProblemDetails / generated OpenAPI types
  from: shared/api/*
  needed to: provide generic transport and generated contract primitives only.
```

Not owned here:

```text
shared/api/agreementExchangeApi.ts
getClientAgreementExchangeDetails.ts
getEmployeeAgreementExchangeDetails.ts
command mutation wrappers
CSRF mechanics for unsafe commands
```

---

## 7. Styling / CSS Ownership

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Client details page layout | page | `pages/agreements/details/*.css` | page container, spacing, page-level states only |
| Employee details page layout | page | `pages/employee/agreements/details/*.css` | page container, spacing, page-level states only |
| Details widget | widget | `widgets/agreement-exchange-details/*.css` | reusable details block layout and sections |
| Read-only proposal/document display | entity/widget | widget CSS or entity display CSS if extracted | read-only display only |
| Tokens/base | global | `styles/*.css` | tokens/reset/base/app shell only |

Checklist for future UI implementation:

```text
[ ] no broad global selector
[ ] no hover layout shift
[ ] no border-width change on hover
[ ] no feature CSS changes app shell
[ ] no page CSS reaches into feature internals
[ ] no business-specific CSS in shared/ui
[ ] uses tokens for color/spacing/radius where possible
[ ] loading/empty/error/not-found states styled
[ ] manual visual checks listed
```

This docs-only archive does not change CSS/runtime UI.

---

## 8. Client API / Server Contract

Target endpoint:

```http
GET /api/agreement-exchanges/{exchangeId}
```

Request body:

```text
none
```

Query:

```text
none first pass
```

Target response direction:

```ts
type AgreementExchangeDetailsResponseDto = {
  exchangeId: number;
  requestId: number;
  exchangeStatus: string;
  activeProposalVersion: number;
  request: AgreementExchangeRequestSummaryDto;
  activeProposal: AgreementProposalDetailsDto;
  proposals: AgreementProposalDetailsDto[];
  currentActorSide: string;
  createdAt: string;
  lastActivityAt?: string | null;
};
```

Generated alias placement:

```ts
// entities/agreement-exchange/api/agreementExchangeApiTypes.ts
import type { components } from "../../../shared/api/generated/openapi-types";

export type AgreementExchangeDetails =
  components["schemas"]["AgreementExchangeDetailsResponseDto"];

export type AgreementProposalDetails =
  components["schemas"]["AgreementProposalDetailsDto"];

export type AgreementDocumentRef =
  components["schemas"]["AgreementDocumentRefDto"];
```

Entity API wrapper:

```ts
// entities/agreement-exchange/api/getAgreementExchangeDetails.ts
import { fetchJson } from "../../../shared/api/fetchJson";
import type { AgreementExchangeDetails } from "./agreementExchangeApiTypes";

export const getAgreementExchangeDetails = (
  exchangeId: number,
): Promise<AgreementExchangeDetails> =>
  fetchJson<AgreementExchangeDetails>(
    `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}`,
  );
```

Important:

```text
Do not create:
  shared/api/agreementExchangeApi.ts

Do not create first pass:
  getClientAgreementExchangeDetails.ts
  getEmployeeAgreementExchangeDetails.ts
```

---

## 9. Validation / Feedback / Error UI

Field validation:

```text
none first pass; details read has route param only.
```

Route/input behavior:

```text
- missing/invalid exchangeId route state should show a page-level not-found/error state;
- query hook should be disabled for invalid/missing exchangeId if implementation needs that guard.
```

Server error handling:

```text
401:
  auth/session flow handles unauthenticated state.

403:
  show access/forbidden state.

404:
  show not-found/not-visible state without implying whether the exchange exists for another account.

500 / network:
  show visible error feedback using shared ApiError/ProblemDetails handling.
```

Server errors must not be swallowed silently.

Client does not validate ownership or lifecycle.

---

## 10. Accessibility / ARIA Contract

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|
| Page heading | `h1` | page title | standard | no | `getByRole('heading', { name: /agreement|договор/i })` |
| Back link | `a` / router link | link text | Enter activates | no | `getByRole('link', { name: /back|назад|мои договоры/i })` |
| Status panel | `section` with heading | section heading | standard reading order | `aria-labelledby` if sectioned | `getByRole('region', { name: /status/i })` if implemented |
| Request summary | `section` | heading | standard | `aria-labelledby` if sectioned | heading/text assertions |
| Active proposal | `section` | heading | standard | `aria-labelledby` if sectioned | heading/text assertions |
| Proposal history | `section` / `list` | heading / list semantics | standard list navigation | no if semantic list used | `getByRole('list', { name: /history|versions/i })` if named |
| Document refs | `list` | heading | standard | no if semantic list used | text/list assertions |
| Error state | `div` / `section` | visible text | standard | `role="alert"` for immediate errors if appropriate | `getByRole('alert')` or visible text |

---

## 11. Cross-Cutting Concerns

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session | yes | Server owns access; client renders auth/access states. |
| Authorization/visibility | server | Client UI is not security. |
| Antiforgery / unsafe requests | no | Safe GET details; command sidecars own CSRF. |
| Generated OpenAPI types | yes | Use generated aliases in entity API. |
| Business API placement | yes | Read wrapper belongs in `entities/agreement-exchange/api`. |
| Error feedback | yes | Show visible not-found/access/error states. |
| Loading/empty/success states | yes | Page/widget owns visible read states. |
| Accessibility | yes | Headings, regions/lists and accessible links. |
| Document/file boundary | yes | Refs only; bytes/download slice later. |
| Command action slots | future | Command sidecars own buttons/forms/mutations. |
| Page-flow/redirect audit | out of scope | Not touched in this docs-only pass. |
| Styling/CSS | future implementation | Ownership documented; no runtime CSS changes here. |

---

## 12. Questions / Decisions

### Blocked / unresolved

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-DETAILS-CLIENT-001` | blocked | Exact generated endpoint path? | Use `SL-AGR-EXCH-004` OpenAPI after server implementation. |
| `Q-L2-AGR-DETAILS-CLIENT-002` | blocked | Exact generated DTO names? | Alias generated DTOs in `entities/agreement-exchange/api`. |
| `Q-L2-AGR-DETAILS-CLIENT-003` | blocked | Are date/status/sender fields strings or enums? | Follow generated OpenAPI. |
| `Q-L2-AGR-DETAILS-CLIENT-004` | blocked | How are document refs linked to download later? | Show refs only; file download slice owns links/bytes. |

### Accepted

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-DETAILS-CLIENT-005` | accepted | One shared details endpoint? | Yes, first pass. |
| `Q-L2-AGR-DETAILS-CLIENT-006` | accepted | One shared frontend details query/model/widget? | Yes. |
| `Q-L2-AGR-DETAILS-CLIENT-007` | accepted | Separate page shells? | Yes, Client and Employee routes/pages differ. |
| `Q-L2-AGR-DETAILS-CLIENT-008` | accepted | Separate client/employee API wrappers? | No, not while response shape is common. |
| `Q-L2-AGR-DETAILS-CLIENT-009` | accepted | Full proposal history in details? | Yes. |
| `Q-L2-AGR-DETAILS-CLIENT-010` | accepted | Document bytes in details? | No, refs only. |
| `Q-L2-AGR-DETAILS-CLIENT-011` | accepted | Command buttons executed here? | No, future command sidecars own actions. |

All existing client question IDs are preserved with their original meanings.

---

## 13. Extension / Change Points

| Extension point | Owner / destination | Notes |
|---|---|---|
| Command action slot rendering | command sidecars | Details widget may host slots; command features own mutations. |
| Document download links | future document/file slice | Details shows refs only until file slice exists. |
| Actor-specific copy | future UI source sync | Exact wording pending dedicated UI scenario. |
| Available actions DTO | server/details or policy extension | Optional future enhancement. |
| Rich request details composition | page-level composition | Do not merge full request details into exchange details by default. |
| Dedicated accessibility audit | future UI pass | This draft records target contract only. |

---

## 14. Behavior Coverage

| Source / draft behavior | Status | How client sidecar covers it |
|---|---|---|
| Client opens own agreement exchange details | covered | Client page calls shared details query; server filters by session. |
| Employee opens visible agreement exchange details | covered | Employee page calls shared details query; server filters by session. |
| Exchange status is visible | covered | Details widget renders `exchangeStatus`. |
| Request summary is visible | covered | Details widget renders `request` summary. |
| Active proposal is visible | covered | Details widget renders `activeProposal`. |
| Full proposal history is visible | covered | Details widget renders `proposals` ordered as response gives. |
| Proposal sender identity is visible | covered | Details widget renders sender/senderId. |
| Document refs are visible | covered | Details widget renders document metadata only. |
| Details read does not mutate exchange | covered | No command handlers/forms in this sidecar. |
| Client-side ownership/security | out of scope | Server responsibility. |
| Commands | out of scope | Future feature sidecars. |
| Runtime UI/page-flow/redirects | out of scope | Future UI/page-flow audit. |

---

## 15. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and visible scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Visible/client outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Client page renders details shell | Client title/back link and details state are visible | component/page test | render Client page with mocked query response | Medium: does not prove server filtering | Low if assertions use accessible text/roles | `ClientAgreementExchangeDetailsPage_RendersClientShellAndDetails` |
| Employee page renders details shell | Employee title/back link and details state are visible | component/page test | render Employee page with mocked query response | Medium: does not prove server filtering | Low | `EmployeeAgreementExchangeDetailsPage_RendersEmployeeShellAndDetails` |
| Shared details widget renders status | exchange status visible | component test | render `AgreementExchangeDetailsView` with DTO | Low for UI display | Low | `AgreementExchangeDetailsView_RendersStatus` |
| Request summary visible | compact request context visible | component test | render details DTO with request summary | Low | Low | `AgreementExchangeDetailsView_RendersRequestSummary` |
| Active proposal visible | active proposal version/sender/document/comment visible | component test | render details DTO | Low | Low | `AgreementExchangeDetailsView_RendersActiveProposal` |
| Proposal history visible | all proposal versions visible in order | component test | render DTO with multiple proposals | Low if count/order visible | Low/Medium if markup changes and tests over-query internals | `AgreementExchangeDetailsView_RendersProposalHistory` |
| Document refs visible, no bytes | document metadata shown, no binary content UI | component test | render DTO with document refs | Medium: absence of bytes is partly contract/server proof | Low | `AgreementExchangeDetailsView_RendersDocumentRefsOnly` |
| No command forms by default | details read page does not execute/render command forms unless sidecar injected | component test | render page/widget without action slot | Medium; commands may be added intentionally later | Low if tied to current scope | `AgreementExchangeDetailsView_DoesNotRenderCommandFormsByDefault` |
| Shared entity wrapper calls shared endpoint | wrapper requests `/api/agreement-exchanges/{exchangeId}` | API wrapper test | mock fetchJson | Medium: does not prove server behavior | Medium if over-asserting internals | `getAgreementExchangeDetails_CallsSharedEndpoint` |
| Query exposes loading/error/success | visible states render | component/query test | mock query states | Low for UI states | Low | page state tests |
| Not-found/access/error states | page shows visible feedback for 404/403/500 | component/page test | mock ApiError/ProblemDetails | Medium if only text asserted and not role | Low/Medium | `AgreementExchangeDetailsPage_RendersNotFoundAccessAndErrorStates` |
| E2E Client details | Client opens details and sees status/history | E2E | seeded backend + browser | Low for integrated flow | Medium | future E2E |
| E2E Employee details | Employee opens details and sees status/history | E2E | seeded backend + browser | Low for integrated flow | Medium | future E2E |

### Component tests

```text
- Client page renders client title/back link.
- Employee page renders employee title/back link.
- Details view renders exchange status.
- Details view renders request summary.
- Details view renders active proposal.
- Details view renders proposal history.
- Details view renders proposal sender/senderId.
- Details view renders document refs.
- Details view does not render command forms by default.
- Page renders loading/error/not-found/success states.
```

### Entity API/query tests

```text
- getAgreementExchangeDetails calls shared endpoint with exchangeId.
- wrapper imports fetchJson from shared API infrastructure.
- wrapper uses generated DTO aliases from entity api types after contract exists.
- query hook is disabled for invalid/missing exchangeId if implementation needs that guard.
- query hook exposes loading/error/success data.
- no client/employee-specific API wrapper exists first pass.
```

### E2E planned

```text
Client session:
  open client agreement exchange details
  assert status, request summary, active proposal and history visible

Employee session:
  open employee agreement exchange details
  assert status, request summary, active proposal and history visible
```

### Non-goals

```text
- do not test counter-proposal/accept/final-refuse command behavior here;
- do not test file download here;
- do not assert server filtering internals from client tests;
- do not assert React Query cache internals in E2E;
- do not test redirect/page-flow policy until page-flow audit.
```

---

## 16. Suggested File Placement

```text
src/entities/agreement-exchange/api/
  getAgreementExchangeDetails.ts
  agreementExchangeApiTypes.ts

src/entities/agreement-exchange/model/
  agreementExchangeQueryKeys.ts
  agreementExchangeTypes.ts
  useAgreementExchangeDetailsQuery.ts

src/widgets/agreement-exchange-details/
  AgreementExchangeDetailsView.tsx
  AgreementExchangeStatusPanel.tsx
  AgreementExchangeRequestSummary.tsx
  AgreementActiveProposalPanel.tsx
  AgreementProposalHistory.tsx
  AgreementDocumentRefList.tsx
  agreementExchangeDetails.css
  agreementExchangeDetailsConst.ts

src/pages/agreements/details/
  ClientAgreementExchangeDetailsPage.tsx
  clientAgreementExchangeDetailsPage.css

src/pages/employee/agreements/details/
  EmployeeAgreementExchangeDetailsPage.tsx
  employeeAgreementExchangeDetailsPage.css

src/shared/api/
  fetchJson.ts
  generated/openapi-types.ts
  generic ProblemDetails / ApiError / CSRF helpers only
```

Do not add:

```text
src/shared/api/agreementExchangeApi.ts
src/entities/agreement-exchange/api/getClientAgreementExchangeDetails.ts
src/entities/agreement-exchange/api/getEmployeeAgreementExchangeDetails.ts
```

These are planning locations only. This archive does not create runtime UI/code files.

---

## 17. Implementation Direction / Current Refactor Checklist

This checklist is for a future implementation verification pass. It is not a claim that implementation was checked now.

```text
[ ] Confirm/apply SL-AGR-EXCH-004 backend endpoint.
[ ] Confirm generated endpoint and DTO names.
[ ] Add/confirm entities/agreement-exchange/api/getAgreementExchangeDetails.ts.
[ ] Update/confirm entities/agreement-exchange/api/agreementExchangeApiTypes.ts.
[ ] Add/confirm useAgreementExchangeDetailsQuery.
[ ] Add/confirm shared AgreementExchangeDetailsView widget.
[ ] Add/confirm status/request summary/active proposal panels.
[ ] Add/confirm proposal history component.
[ ] Add/confirm document reference display component.
[ ] Add/confirm Client agreement exchange details page shell.
[ ] Add/confirm Employee agreement exchange details page shell.
[ ] Use one shared endpoint wrapper first pass.
[ ] Do not add actor-specific details wrappers first pass.
[ ] Do not add shared/api/agreementExchangeApi.ts.
[ ] Render loading/error/not-found/success states.
[ ] Keep command buttons/forms out of this read sidecar except optional slots.
[ ] Add component/entity query tests.
[ ] Add E2E smoke when server/test setup is ready.
[ ] Regenerate OpenAPI/types if this implementation package changes API shape.
```

This pass did not perform implementation verification.

---

## 18. Next Step

```text
1. Confirm/apply SL-AGR-EXCH-004 backend endpoint.
2. Run OpenAPI/type generation.
3. Confirm generated endpoint and DTO names.
4. Add shared entity API wrapper.
5. Add shared details query/model.
6. Add shared details widget.
7. Add Client details page shell.
8. Add Employee details page shell.
9. Add component/entity tests.
10. Add E2E once server/test setup is ready.
```

Separate later work:

```text
- page flow / redirects audit;
- runtime UI refactoring workflow;
- source registry/behavior item ID sync;
- file download/document storage slice.
```

---

## 19. Guardrail Summary

```text
Use one shared details endpoint and one shared frontend query/model/details widget first pass.
Keep Client and Employee page shells separate.
Do not create client/employee-specific API wrappers while response shape is common.
Keep business endpoint wrappers out of shared/api.
Server filters visibility; client UI is not security.
Client access is protected by ClientAccountId server-side.
Employee is not exchange-level owner.
Do not use ResponsibleEmployeeId as guard.
Details includes full proposal history.
List remains summary-only.
Document refs are metadata only; no bytes/download/upload here.
Do not render command forms by default in this read sidecar.
Command sidecars own send/accept/final-refuse mutations.
Runtime UI/page-flow/redirect audit is separate later work.
No runtime code, tests or generated artifacts were changed in this docs-only pass.
```
