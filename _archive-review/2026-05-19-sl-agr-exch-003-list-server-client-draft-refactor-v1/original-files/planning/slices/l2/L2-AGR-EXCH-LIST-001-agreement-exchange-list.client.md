# L2-AGR-EXCH-LIST-001.client — Agreement Exchange List Pages

Status: client read sidecar draft / shared list endpoint and shared list widget first pass
Parent server slice: `SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`
Slice type: client read sidecar
Actors: ClientAccount, Employee
Architecture direction: shared entity read/query/list widget, separate actor page shells.

## 0. Key Decision

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

## 1. Scope

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

## 2. Out of Scope

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
- business-specific wrappers in shared/api.
```

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
  Owns shared backend list endpoint/read model.

L2-AGR-EXCH-LIST-001.client
  Owns shared client query/model/list widget and actor page shells.

Future Client Exchange Details client sidecar
  Owns Client-facing exchange details page.

Future Employee Exchange Details client sidecar
  Owns Employee-facing exchange details page.

Agreement Exchange command sidecars
  Own start/counter/accept/refuse action UI and mutations.

shared/api
  Owns generic transport, ProblemDetails/ApiError, CSRF helpers and generated types only.
```

## 4. Server Visibility Direction

First-pass read endpoint is shared:

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

## 5. Shared API / Separate Page Shell Rule

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

## 6. Visual UI / Scenario Flow

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

In ordinary words:

Client and Employee open different pages, but both pages use the same shared Agreement Exchange list query and list widget. The server decides which rows are visible based on the current session role. The list page only shows summary information and navigates to details.

Scenario flow table:

| Step | Layer               | Responsibility                                             |
| ---- | ------------------- | ---------------------------------------------------------- |
| S01  | Client page shell   | Opens “Мои договоры”.                                      |
| S02  | Employee page shell | Opens Employee exchange dashboard.                         |
| S03  | Shared query        | Calls shared list endpoint.                                |
| S04  | Server              | Filters rows by session role.                              |
| S05  | Shared list widget  | Renders common exchange rows.                              |
| S06  | Page shell          | Provides actor-specific title, empty state and navigation. |
| S07  | Row navigation      | Opens actor-appropriate exchange details page.             |

## 7. Visual Client Implementation Flow

```text
[Client Page Shell]
pages/agreements/my/ClientAgreementExchangesPage.tsx

Owns:
  client route/page composition;
  page title: "Мои договоры";
  client empty state;
  client-specific navigation to client details route.

Uses:
  useAgreementExchangeListQuery()
  AgreementExchangeList
```

```text
[Employee Page Shell]
pages/employee/agreements/dashboard/EmployeeAgreementExchangesDashboardPage.tsx

Owns:
  employee route/page composition;
  employee dashboard title/copy;
  employee empty state;
  employee-specific navigation to employee details route.

Uses:
  useAgreementExchangeListQuery()
  AgreementExchangeList
```

```text
[Entity API Layer]
entities/agreement-exchange/api/listAgreementExchanges.ts
entities/agreement-exchange/api/agreementExchangeApiTypes.ts

Owns:
  shared read endpoint wrapper;
  generated DTO aliases near agreement-exchange entity.

Uses:
  shared/api/fetchJson
  shared/api/generated/openapi-types
```

```text
[Entity Model Layer]
entities/agreement-exchange/model/useAgreementExchangeListQuery.ts
entities/agreement-exchange/model/agreementExchangeQueryKeys.ts
entities/agreement-exchange/model/agreementExchangeTypes.ts

Owns:
  React Query hook;
  query key;
  local view model helpers if needed.
```

```text
[Widget Layer]
widgets/agreement-exchange-list/AgreementExchangeList.tsx
widgets/agreement-exchange-list/AgreementExchangeRow.tsx
widgets/agreement-exchange-list/agreementExchangeList.css

Owns:
  common list layout;
  row rendering;
  loading/empty/error display hooks if passed from page;
  row click/link callback.

Does not own:
  actor-specific page shell;
  command buttons/mutations;
  server visibility.
```

```text
[Shared API Infrastructure]
shared/api/fetchJson.ts
shared/api/generated/openapi-types.ts
shared/api/ApiError / ProblemDetails helpers
shared/api/antiforgery helpers

Owns:
  generic request execution;
  generated type source;
  generic error parsing.

Does not own:
  listAgreementExchanges()
  agreement exchange business wrappers
```

Implementation flow table:

| Step | Layer            | Responsibility                                                    |
| ---- | ---------------- | ----------------------------------------------------------------- |
| I01  | Client page      | Renders client “Мои договоры” shell.                              |
| I02  | Employee page    | Renders Employee dashboard shell.                                 |
| I03  | Entity query     | Loads shared agreement exchange list.                             |
| I04  | Entity API       | Calls shared list endpoint.                                       |
| I05  | Shared API infra | Executes generic GET and ProblemDetails mapping.                  |
| I06  | Widget           | Renders shared list rows.                                         |
| I07  | Page shell       | Supplies actor-specific title, empty state and detail navigation. |

## 8. Client API / Server Contract

Target endpoint:

```http
GET /api/agreement-exchanges
```

or actual generated OpenAPI route.

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

## 9. Role-Based UI Rules

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

## 10. Security / Protection

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
AgreementExchangeActorContext
AgreementExchangeVisibilityPolicy
AgreementExchangeActionPolicy
AgreementExchangeReadProjector
```

This avoids huge role `if` blocks inside controllers/handlers.

## 11. Questions / Decisions

### Blocked / unresolved

| ID                         | Status  | Question                        | Current direction                                          |
| -------------------------- | ------- | ------------------------------- | ---------------------------------------------------------- |
| `Q-L2-AGR-LIST-CLIENT-001` | blocked | Exact generated endpoint path?  | Use `SL-AGR-EXCH-003` OpenAPI after server implementation. |
| `Q-L2-AGR-LIST-CLIENT-002` | blocked | Exact generated DTO names?      | Alias generated DTOs in `entities/agreement-exchange/api`. |
| `Q-L2-AGR-LIST-CLIENT-003` | blocked | Which filters exist first pass? | Use only server-supported filters; do not invent.          |

### Accepted

| ID                         | Status   | Question                                        | Current direction                                        |
| -------------------------- | -------- | ----------------------------------------------- | -------------------------------------------------------- |
| `Q-L2-AGR-LIST-CLIENT-004` | accepted | One shared list endpoint?                       | Yes, first pass.                                         |
| `Q-L2-AGR-LIST-CLIENT-005` | accepted | One shared frontend query/model/list widget?    | Yes.                                                     |
| `Q-L2-AGR-LIST-CLIENT-006` | accepted | Separate page shells?                           | Yes, Client and Employee routes/pages differ.            |
| `Q-L2-AGR-LIST-CLIENT-007` | accepted | Separate client/employee API wrappers?          | No, not while response shape is common.                  |
| `Q-L2-AGR-LIST-CLIENT-008` | accepted | Employee exchange-level owner?                  | No. Any active Employee may service exchange first pass. |
| `Q-L2-AGR-LIST-CLIENT-009` | accepted | Full proposal history in list?                  | No. Details slice owns full history.                     |
| `Q-L2-AGR-LIST-CLIENT-010` | accepted | Merge request details and exchange details DTO? | No. Pages may compose multiple queries.                  |

## 12. Behavior Coverage

| Behavior                                  | How client sidecar covers it                                 |
| ----------------------------------------- | ------------------------------------------------------------ |
| Client sees own agreement exchanges       | Client page calls shared query; server filters by session.   |
| Employee sees employee-visible exchanges  | Employee page calls shared query; server filters by session. |
| Shared row summary is displayed           | `AgreementExchangeList` renders common DTO rows.             |
| Proposal active sender is visible         | Row shows `activeProposalSender` / version info.             |
| Proposal author identity can be displayed | Row can show sender type/id where provided.                  |
| Full proposal history                     | Out of scope; details read slice.                            |
| Commands                                  | Out of scope; command sidecars.                              |
| Security                                  | Server responsibility; client UI is not security boundary.   |

## 13. Verification Plan

Component tests:

```text
- Client page renders client title and empty state.
- Employee page renders employee title and empty state.
- Both pages use shared AgreementExchangeList.
- AgreementExchangeList renders requestId/exchangeId/status/version/sender/address/dates.
- Row click/link uses actor-specific details href from page shell.
- List does not render full proposal history.
- List does not render command forms.
```

Entity API/query tests:

```text
- listAgreementExchanges calls shared endpoint.
- wrapper imports fetchJson from shared API infrastructure.
- wrapper uses generated DTO aliases from entity api types.
- query hook exposes loading/error/success data.
- no client/employee-specific API wrapper exists first pass.
```

E2E planned:

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

Non-goals:

```text
- do not test command lifecycle here;
- do not test accept/refuse/counter-proposal here;
- do not assert server filtering internals from client tests;
- do not test full proposal history here;
- do not assert React Query cache internals in E2E.
```

## 14. Suggested File Placement

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

## 15. Next Step

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

## 16. Implementation Checklist

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
