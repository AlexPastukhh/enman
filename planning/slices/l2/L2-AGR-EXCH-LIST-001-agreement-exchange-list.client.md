# L2-AGR-EXCH-LIST-001.client — Agreement Exchange List Pages

Status: implemented client-sidecar draft refactor / implementation not rechecked in this pass  
Parent server slice: `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`  
Slice type: client read sidecar  
Actors: ClientAccount, Employee  
Architecture direction: shared entity read/query/list widget, separate actor page shells.

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was not rechecked in this pass.
Deep UI redesign and page-flow/redirect audit are out of scope for this pass.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-13A — Agreement Exchange List
```

UI scenario:

```text
missing / pending dedicated UI source for Client and Employee agreement exchange list pages
```

Server source:

```text
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
```

Cross-cutting behavior:

```text
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility
CC-CLIENT-FORM-VALIDATION-001 — only if filters become user-editable form fields
```

Data source:

```text
pending scenario-data source for list row summary fields, empty states and actor-specific labels
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
SC-13A: pending / v000 if source registry is applied
SL-AGR-EXCH-003: paired server draft in same archive
```

Domain/server baseline:

```text
server read endpoint must filter by current session role/account;
client-side filtering is UX only and not security.
```

Slice derivation map:

```text
pending / add row for L2-AGR-EXCH-LIST-001.client during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Server/domain disposition | This client slice responsibility | Notes |
|---|---|---|---|---|
| Client opens own agreement exchange list | SC-13A pending | server filters rows by ClientAccountId | render Client page shell and call shared query | page copy: `Мои договоры` |
| Employee opens employee exchange dashboard | SC-13A pending | server returns employee-visible exchanges | render Employee page shell and call shared query | any active Employee first pass |
| Shared list row summary is displayed | SC-13A pending | server returns compact DTO | render common list widget/rows | no full history |
| Actor-specific empty states | SC-13A pending | not server behavior | page shell provides title/empty text | keep simple first pass |
| Row opens actor-appropriate details page | SC-13A/SC-13B pending | details route/read owned elsewhere | page shell supplies `getDetailsHref` | redirect audit later |
| No command forms in list slice | SC-13A pending | command slices own mutations | list does not render command forms first pass | actions are future sidecars |
| Shared endpoint wrapper | SL-AGR-EXCH-003 | server exposes one endpoint | use `entities/agreement-exchange/api/listAgreementExchanges.ts` | no actor-specific wrappers |
| Loading/error/empty/success states | cross-cutting UI | not server behavior | render visible states with simple layout | no deep UI overhaul |

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
    planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md

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
  - old client draft lacked Source / Domain / Slice Coverage Snapshot;
  - old client draft lacked Implementation Sync Status;
  - old test plan lacked Behavior-to-Test Trace with escape/refactor risk;
  - old visual implementation flow used ownership language but not explicit `from`, `needed to`, `visual` structure.

source:
  - stable behavior item IDs and final UI scenario are not yet assigned.

implementation:
  - not checked in this pass.

UI:
  - deep UI redesign and redirects are not touched in this pass.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection and no page-flow/redirect audit.
```

---

## 1. Key Decision

First pass uses **one shared read model and one shared frontend list ownership**:

```text
entities/agreement-exchange/api/*
entities/agreement-exchange/model/*
widgets/agreement-exchange-list/*
```

Client and Employee may have different route/page shells:

```text
Client:
  client agreement exchanges page
  "Мои договоры"

Employee:
  employee agreement exchanges page
  "Agreement exchanges dashboard"
```

But both pages consume the same query/model/list widget:

```text
useAgreementExchangeListQuery()
AgreementExchangeList
AgreementExchangeRow
```

Do not introduce actor-specific wrappers first pass:

```text
do not add:
  clientAgreementExchangeApi
  employeeAgreementExchangeApi
  listClientAgreementExchanges.ts
  listEmployeeAgreementExchanges.ts
```

Use one shared entity wrapper while response shape is common:

```text
entities/agreement-exchange/api/listAgreementExchanges.ts
entities/agreement-exchange/api/agreementExchangeApiTypes.ts
```

---

## 2. Scope

This client sidecar owns:

```text
- shared Agreement Exchange list query/model;
- shared Agreement Exchange list widget;
- Client agreement exchanges page shell;
- Employee agreement exchanges page shell;
- role-based page copy/title/empty states;
- loading/error/empty/success list states;
- common row summary display;
- navigation from row to actor-appropriate exchange details page;
- common list filters only when supported by shared server contract;
- no full proposal history in list;
- no request details DTO merge;
- no command execution from this list slice first pass.
```

The list summary includes:

```text
requestId
exchangeId
exchangeStatus
activeProposalVersion
activeProposalSender
activeProposalSenderId
requestDisplayName / objectAddress if available
createdAt
lastActivityAt
```

Full proposal history belongs to exchange details/read slice, not list.

---

## 3. Out of Scope

```text
- backend endpoint implementation details beyond consuming generated contract;
- exchange details full proposal history;
- request details full DTO;
- start exchange command;
- send counter-proposal command;
- accept active proposal command;
- final refuse command;
- actor-specific API wrappers while response shape is common;
- employee exchange-level ownership / ResponsibleEmployeeId guard;
- local CSRF mechanics;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api;
- deep UI redesign;
- page flow / redirect audit.
```

---

## 4. Related Slices / Owners

```text
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
  Owns shared backend list endpoint/read model.

L2-AGR-EXCH-LIST-001.client
  Owns shared client query/model/list widget and actor page shells.

L2-AGR-EXCH-DETAILS-001.client
  Owns Client/Employee-facing exchange details pages.

Agreement Exchange command sidecars
  Own start/counter/accept/refuse action UI and mutations.

shared/api
  Owns generic transport, ProblemDetails/ApiError, CSRF helpers and generated types only.
```

---

## 5. Server Visibility Direction

First-pass read endpoint:

```http
GET /api/agreement-exchanges
```

or the actual route from generated OpenAPI after server implementation.

Server filters rows by current session role.

```text
Client:
  sees only exchanges where
  AgreementProposalExchange.ClientAccountId == current client account id

Employee:
  first pass sees employee-visible exchanges
  any active Employee can service/continue exchange
```

Important:

```text
Employee is not exchange-level owner.
Do not use ResponsibleEmployeeId as guard.
Different Employees may continue the same exchange.
```

Proposal versions still store author data:

```text
AgreementProposal.Author.Sender
AgreementProposal.Author.SenderId
```

The UI may show who sent a proposal version/active proposal using these fields.

---

## 6. Shared API / Separate Page Shell Rule

This slice intentionally demonstrates the general slicing rule:

```text
Same endpoint does not always mean same page.
Same DTO does not always mean one actor journey.
Slice boundaries do not have to go vertically through all app layers.
```

For this first pass:

```text
API/query/model/list widget:
  shared

Page shell/route/copy:
  actor-specific
```

That means:

```text
Client page:
  uses shared query/list widget
  provides client title/empty state/navigation

Employee page:
  uses shared query/list widget
  provides employee title/empty state/navigation
```

---

## 7. Visual UI / Scenario Flow

```text
[Signed-in Client]
opens "Мои договоры"
        ↓
[Client Page Shell]
uses shared agreement exchange query
        ↓
[AgreementExchangeList]
shows only current client's exchanges
        ↓
Client opens exchange details


[Signed-in Employee]
opens Employee agreement exchange dashboard
        ↓
[Employee Page Shell]
uses shared agreement exchange query
        ↓
[AgreementExchangeList]
shows employee-visible exchanges
        ↓
Employee opens exchange details
```

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Client page shell | Opens `Мои договоры`. |
| S02 | Employee page shell | Opens Employee exchange dashboard. |
| S03 | Shared query | Calls shared list endpoint. |
| S04 | Server | Filters rows by session role. |
| S05 | Shared list widget | Renders common exchange rows. |
| S06 | Page shell | Provides actor-specific title, empty state and navigation. |
| S07 | Row navigation | Opens actor-appropriate exchange details page. |

---

## 8. Visual Client Implementation Flow

### Client Page Shell

```text
ClientAgreementExchangesPage
  from: pages/agreements/my/ClientAgreementExchangesPage.tsx
  needed to: compose the Client list route with shared query/list and Client-specific copy/navigation.
  visual: page-level container, title "Мои договоры", simple empty/error/loading area.
```

Uses:

```text
useAgreementExchangeListQuery
  from: entities/agreement-exchange/model/useAgreementExchangeListQuery.ts
  needed to: load the shared agreement exchange list through the entity model layer.

AgreementExchangeList
  from: widgets/agreement-exchange-list/AgreementExchangeList.tsx
  needed to: render common rows and list states.
  visual: shared list/card/table block inside the page content area.
```

### Employee Page Shell

```text
EmployeeAgreementExchangesDashboardPage
  from: pages/employee/agreements/dashboard/EmployeeAgreementExchangesDashboardPage.tsx
  needed to: compose the Employee list route with shared query/list and Employee-specific copy/navigation.
  visual: page-level dashboard container, title/copy for Employee work queue.
```

Uses:

```text
useAgreementExchangeListQuery
  from: entities/agreement-exchange/model/useAgreementExchangeListQuery.ts
  needed to: load the same shared server list endpoint.

AgreementExchangeList
  from: widgets/agreement-exchange-list/AgreementExchangeList.tsx
  needed to: render common rows and list states.
  visual: shared list/card/table block inside the page content area.
```

### Entity API Layer

```text
listAgreementExchanges
  from: entities/agreement-exchange/api/listAgreementExchanges.ts
  needed to: wrap the shared read endpoint and keep business endpoint wrappers out of shared/api.
```

Uses:

```text
fetchJson
  from: shared/api/fetchJson.ts
  needed to: execute generic GET request and parse response/errors.

AgreementExchangeListResponse
  from: entities/agreement-exchange/api/agreementExchangeApiTypes.ts
  needed to: expose generated OpenAPI DTO aliases near the entity.
```

### Entity Model Layer

```text
useAgreementExchangeListQuery
  from: entities/agreement-exchange/model/useAgreementExchangeListQuery.ts
  needed to: own React Query loading/error/success state for the shared list.

agreementExchangeQueryKeys
  from: entities/agreement-exchange/model/agreementExchangeQueryKeys.ts
  needed to: centralize query keys for list/details invalidation.

agreementExchangeTypes
  from: entities/agreement-exchange/model/agreementExchangeTypes.ts
  needed to: hold small view-model helpers only if generated DTOs need local shape normalization.
```

### Widget Layer

```text
AgreementExchangeList
  from: widgets/agreement-exchange-list/AgreementExchangeList.tsx
  needed to: render the common list shell and delegate each row.
  visual: simple, readable list block with consistent row spacing and no dense decorative styling.

AgreementExchangeRow
  from: widgets/agreement-exchange-list/AgreementExchangeRow.tsx
  needed to: render one exchange summary and details link target supplied by page shell.
  visual: compact summary row/card; no command form inside row.

AgreementExchangeListEmptyState
  from: widgets/agreement-exchange-list/AgreementExchangeListEmptyState.tsx
  needed to: render actor-specific empty state text provided by page shell.
  visual: calm empty panel, not a modal.
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
business-specific wrappers in shared/api
clientAgreementExchangeApi.ts
employeeAgreementExchangeApi.ts
```

---

## 9. Client API / Server Contract

Target endpoint:

```http
GET /api/agreement-exchanges
```

Target response direction:

```ts
type AgreementExchangeListResponseDto = {
  exchanges: AgreementExchangeListItemDto[];
};

type AgreementExchangeListItemDto = {
  requestId: number;
  exchangeId: number;
  exchangeStatus: string;
  activeProposalVersion: number | null;
  activeProposalSender: "Client" | "Employee" | null;
  activeProposalSenderId: number | null;
  requestDisplayName?: string | null;
  objectAddress?: string | null;
  createdAt: string;
  lastActivityAt: string;
};
```

Generated alias placement:

```ts
// entities/agreement-exchange/api/agreementExchangeApiTypes.ts
import type { components } from "../../../shared/api/generated/openapi-types";

export type AgreementExchangeListResponse =
  components["schemas"]["AgreementExchangeListResponseDto"];

export type AgreementExchangeListItem =
  components["schemas"]["AgreementExchangeListItemDto"];
```

Entity API wrapper:

```ts
// entities/agreement-exchange/api/listAgreementExchanges.ts
import { fetchJson } from "../../../shared/api/fetchJson";
import type { AgreementExchangeListResponse } from "./agreementExchangeApiTypes";

export const listAgreementExchanges =
  (): Promise<AgreementExchangeListResponse> =>
    fetchJson<AgreementExchangeListResponse>("/api/agreement-exchanges");
```

Important:

```text
Do not create:
  shared/api/agreementExchangeApi.ts

Do not create first pass:
  clientAgreementExchangeApi.ts
  employeeAgreementExchangeApi.ts
  listClientAgreementExchanges.ts
  listEmployeeAgreementExchanges.ts
```

---

## 10. Role-Based UI Rules

The shared widget may receive page-level props:

```ts
type AgreementExchangeListProps = {
  exchanges: AgreementExchangeListItem[];
  viewerRole: "Client" | "Employee";
  getDetailsHref(exchange: AgreementExchangeListItem): string;
  emptyStateTitle: string;
  emptyStateDescription: string;
};
```

Page shells own role language:

```text
Client:
  title: "Мои договоры"
  empty: "У вас пока нет договоров."

Employee:
  title: "Agreement exchanges"
  empty: "No agreement exchanges need attention."
```

The widget can use `viewerRole` only for display wording and row navigation. It must not enforce security.

---

## 11. Security / Protection

Client-side buttons/visibility are UX only.

Server owns:

```text
auth/session
current role resolution
ClientAccount visibility
Employee active-session visibility
exchange lifecycle checks
proposal sender rules
command permissions
```

FluentValidation owns only shape:

```text
query params
ids
enum values
string lengths
request body shape for command slices
```

FluentValidation does not own:

```text
actor can see exchange
actor can continue exchange
whose turn it is
whether active proposal can be accepted/refused
```

For server implementation, prefer app service/policies:

```text
AgreementExchangeApplicationService
AgreementExchangeVisibilityPolicy
AgreementExchangeActionPolicy
AgreementExchangeReadProjector
```

---

## 12. Questions / Decisions

### Blocked / unresolved

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-LIST-CLIENT-001` | blocked | Exact generated endpoint path? | Use `SL-AGR-EXCH-003` OpenAPI after server implementation. |
| `Q-L2-AGR-LIST-CLIENT-002` | blocked | Exact generated DTO names? | Alias generated DTOs in `entities/agreement-exchange/api`. |
| `Q-L2-AGR-LIST-CLIENT-003` | blocked | Which filters exist first pass? | Use only server-supported filters; do not invent. |

### Accepted

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-LIST-CLIENT-004` | accepted | One shared list endpoint? | Yes, first pass. |
| `Q-L2-AGR-LIST-CLIENT-005` | accepted | One shared frontend query/model/list widget? | Yes. |
| `Q-L2-AGR-LIST-CLIENT-006` | accepted | Separate page shells? | Yes, Client and Employee routes/pages differ. |
| `Q-L2-AGR-LIST-CLIENT-007` | accepted | Separate client/employee API wrappers? | No, not while response shape is common. |
| `Q-L2-AGR-LIST-CLIENT-008` | accepted | Employee exchange-level owner? | No. Any active Employee may service exchange first pass. |
| `Q-L2-AGR-LIST-CLIENT-009` | accepted | Full proposal history in list? | No. Details slice owns full history. |
| `Q-L2-AGR-LIST-CLIENT-010` | accepted | Merge request details and exchange details DTO? | No. Pages may compose multiple queries. |

---

## 13. Behavior Coverage

| Source / draft behavior | Status | How client sidecar covers it |
|---|---|---|
| Client sees own agreement exchanges | covered | Client page calls shared query; server filters by session. |
| Employee sees employee-visible exchanges | covered | Employee page calls shared query; server filters by session. |
| Shared row summary is displayed | covered | `AgreementExchangeList` renders common DTO rows. |
| Proposal active sender is visible | covered | Row shows active proposal sender/version info. |
| Proposal author identity can be displayed | covered | Row can show sender type/id where provided. |
| Loading/error/empty/success states are visible | covered | page/query/widget render visible states. |
| Full proposal history | out of scope | details read slice. |
| Commands | out of scope | command sidecars. |
| Security | out of client scope | server responsibility; client UI is not security boundary. |
| Redirect/page flow | out of scope | future page-flow/redirect audit. |

---

## 14. Test / Verification Plan

Primary rule:

```text
Tests verify visible client behavior and scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Visible/client outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Client page renders own agreements shell | Client title/empty/list state is visible | component/page test | render Client page with mocked query response | Medium: does not prove server filtering | Low if assertions use accessible text/roles | `ClientAgreementExchangesPage_RendersClientShellAndList` |
| Employee page renders dashboard shell | Employee title/empty/list state is visible | component/page test | render Employee page with mocked query response | Medium: does not prove server filtering | Low | `EmployeeAgreementExchangesDashboardPage_RendersEmployeeShellAndList` |
| Both pages use shared list widget | common row summary is rendered consistently | component test | render pages/list with same DTO data | Medium: can be bypassed by duplicating internal UI unless row output is asserted | Medium if testing component names instead of visible output | page tests + `AgreementExchangeList_RendersRows` |
| Row displays summary fields | status/version/sender/request display/dates visible | component test | render `AgreementExchangeList` with DTO rows | Low for visual row behavior | Low | `AgreementExchangeList_RendersExchangeSummaryRows` |
| Row navigates to actor-specific details | link href comes from page shell callback | component/page test | render row/page and inspect link href | Low for link behavior, not full redirect | Low/Medium if routes change | `AgreementExchangeRow_UsesProvidedDetailsHref` |
| Full history is absent | list does not render proposal history block | component test | render row with summary data only | Medium; absence tests can be weak | Low | `AgreementExchangeList_DoesNotRenderProposalHistory` |
| No command forms in list slice | list page has no send/accept/refuse form | component test | render page/list | Medium; commands may appear later intentionally | Low if tied to current scope | `AgreementExchangeList_DoesNotRenderCommandFormsFirstPass` |
| Entity wrapper calls shared endpoint | wrapper uses `/api/agreement-exchanges` | API wrapper test | mock fetchJson | Medium: does not prove server behavior | Medium if over-asserting internals | `listAgreementExchanges_CallsSharedEndpoint` |
| Query exposes loading/error/success | visible states render | component/query test | mock query states | Low for UI states | Low | list/page state tests |
| E2E Client list | Client sees client-visible exchanges and opens details | E2E | seeded backend + browser | Low for integrated flow | Medium | future E2E |
| E2E Employee list | Employee sees employee-visible exchanges and opens details | E2E | seeded backend + browser | Low for integrated flow | Medium | future E2E |

### Component tests

```text
- Client page renders client title and empty state.
- Employee page renders employee title and empty state.
- Both pages render shared row summary output.
- AgreementExchangeList renders requestId/exchangeId/status/version/sender/address/dates.
- Row click/link uses actor-specific details href from page shell.
- List does not render full proposal history.
- List does not render command forms.
```

### Entity API/query tests

```text
- listAgreementExchanges calls shared endpoint.
- wrapper imports fetchJson from shared API infrastructure.
- wrapper uses generated DTO aliases from entity api types.
- query hook exposes loading/error/success data.
- no client/employee-specific API wrapper exists first pass.
```

### E2E planned

```text
Client session:
  open "Мои договоры"
  assert only client-visible exchanges appear
  open exchange details

Employee session:
  open Employee agreement exchange dashboard
  assert employee-visible exchanges appear
  open exchange details
```

### Non-goals

```text
- do not test command lifecycle here;
- do not test accept/refuse/counter-proposal here;
- do not assert server filtering internals from client tests;
- do not test full proposal history here;
- do not assert React Query cache internals in E2E;
- do not test redirect/page-flow policy until page-flow audit.
```

---

## 15. Suggested File Placement

```text
src/entities/agreement-exchange/api/
  listAgreementExchanges.ts
  agreementExchangeApiTypes.ts

src/entities/agreement-exchange/model/
  agreementExchangeQueryKeys.ts
  agreementExchangeTypes.ts
  useAgreementExchangeListQuery.ts

src/widgets/agreement-exchange-list/
  AgreementExchangeList.tsx
  AgreementExchangeRow.tsx
  AgreementExchangeListEmptyState.tsx
  agreementExchangeList.css
  agreementExchangeListConst.ts

src/pages/agreements/my/
  ClientAgreementExchangesPage.tsx
  clientAgreementExchangesPage.css

src/pages/employee/agreements/dashboard/
  EmployeeAgreementExchangesDashboardPage.tsx
  employeeAgreementExchangesDashboardPage.css

src/shared/api/
  fetchJson.ts
  generated/openapi-types.ts
  generic ProblemDetails / ApiError / CSRF helpers only
```

Do not add:

```text
src/shared/api/agreementExchangeApi.ts
src/entities/agreement-exchange/api/listClientAgreementExchanges.ts
src/entities/agreement-exchange/api/listEmployeeAgreementExchanges.ts
```

---

## 16. Next Step

```text
1. Confirm/apply SL-AGR-EXCH-003 backend endpoint.
2. Run OpenAPI/type generation.
3. Confirm generated endpoint and DTO names.
4. Add shared entity API wrapper.
5. Add shared query/model.
6. Add shared list widget.
7. Add Client page shell.
8. Add Employee page shell.
9. Add component/entity tests.
10. Add E2E once server/test setup is ready.
```

Separate later work:

```text
- page flow / redirects audit;
- UI refactoring workflow;
- source registry/behavior item ID sync.
```

---

## 17. Implementation Checklist / Current Refactor Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[ ] confirm/apply SL-AGR-EXCH-003 backend endpoint
[ ] confirm generated endpoint and DTO names
[ ] add entities/agreement-exchange/api/listAgreementExchanges.ts
[ ] add entities/agreement-exchange/api/agreementExchangeApiTypes.ts
[ ] add agreementExchangeQueryKeys
[ ] add useAgreementExchangeListQuery
[ ] add shared AgreementExchangeList widget
[ ] add AgreementExchangeRow
[ ] add Client agreement exchanges page shell
[ ] add Employee agreement exchanges page shell
[ ] use one shared endpoint wrapper first pass
[ ] do not add actor-specific API wrappers first pass
[ ] do not add shared/api/agreementExchangeApi.ts
[ ] render loading/error/empty/success states
[ ] render exchange status, active version and active proposal sender
[ ] navigate rows to actor-specific details pages
[ ] add component/entity query tests
[ ] add E2E smoke when server/test setup is ready
[ ] regenerate OpenAPI/types if this implementation package changes API shape
```

This pass did not perform implementation verification.

---

## 18. Guardrail Summary

```text
Use one shared list endpoint and one shared frontend query/model/list widget first pass.
Keep Client and Employee page shells separate.
Do not create client/employee-specific API wrappers while response shape is common.
Keep business endpoint wrappers out of shared/api.
Server filters visibility; client UI is not security.
Employee is not exchange-level owner.
Do not use ResponsibleEmployeeId as guard.
Do not include full proposal history in list.
Do not render command forms in this list slice first pass.
Page-flow/redirect audit is separate later work.
```
