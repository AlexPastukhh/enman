# L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages

Status: client read sidecar draft / shared details endpoint and shared details widget first pass
Parent server slice: `SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details`
Slice type: client read sidecar
Actors: ClientAccount, Employee
Architecture direction: shared entity read/query/details widget, separate actor page shells/routes.

## 0. Key Decision

First pass uses **one shared details endpoint**, **one shared entity query/model**, and **one shared details widget**:

```text
entities/agreement-exchange/api/*
entities/agreement-exchange/model/*
widgets/agreement-exchange-details/*
```

Client and Employee pages are still separate shells/routes:

```text
Client:
  client agreement exchange details page

Employee:
  employee agreement exchange details page
```

But both consume the same query/model/details widget:

```text
useAgreementExchangeDetailsQuery(exchangeId)
AgreementExchangeDetailsView
AgreementProposalHistory
```

Do not introduce actor-specific client wrappers while response shape is common:

```text
do not add:
  getClientAgreementExchangeDetails.ts
  getEmployeeAgreementExchangeDetails.ts
  clientAgreementExchangeApi
  employeeAgreementExchangeApi
```

Use one shared entity wrapper:

```text
entities/agreement-exchange/api/getAgreementExchangeDetails.ts
entities/agreement-exchange/api/agreementExchangeApiTypes.ts
```

## 1. Scope

This client sidecar owns:

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
- currentActorSide display/helper usage;
- optional action slot placement for future command sidecars;
- no command execution in this read sidecar.
```

Details page shows full proposal history. The list page stays summary-only.

## 2. Out of Scope

```text
- backend endpoint implementation -> SL-AGR-EXCH-004;
- Agreement Exchange list page/read list -> SL-AGR-EXCH-003 / L2-AGR-EXCH-LIST-001.client;
- Start Agreement Exchange command;
- Send counter-proposal command;
- Accept active proposal command;
- Final refuse command;
- file download / binary document serving;
- document upload/storage;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api;
- actor-specific API wrappers while response shape is common.
```

Important:

```text
Details read may show action slots, but it does not execute actions.
Command features own their own buttons/forms/mutations.
```

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
  Owns shared backend list endpoint/read model.

L2-AGR-EXCH-LIST-001.client
  Owns shared list query/model/list widget and actor page shells.

SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
  Owns shared backend details endpoint/read model.

L2-AGR-EXCH-DETAILS-001.client
  Owns shared client details query/model/details widget and actor page shells.

Future command sidecars:
  Start exchange with initial Employee proposal
  Send counter-proposal
  Accept active proposal
  Final refuse exchange

shared/api:
  Owns generic transport, ProblemDetails/ApiError, CSRF helpers and generated types only.
```

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

In ordinary words:

Client and Employee open different details routes, but both pages use the same shared agreement exchange details query and details widget. The server filters access by session role. The details UI shows proposal history and document references, but does not mutate exchange state.

Scenario flow table:

| Step | Layer                 | Responsibility                                                               |
| ---- | --------------------- | ---------------------------------------------------------------------------- |
| S01  | Client page shell     | Opens client exchange details route.                                         |
| S02  | Employee page shell   | Opens employee exchange details route.                                       |
| S03  | Shared query          | Calls shared details endpoint with `exchangeId`.                             |
| S04  | Server                | Filters details by session role/visibility.                                  |
| S05  | Shared details widget | Renders exchange status, request summary, active proposal, proposal history. |
| S06  | Page shell            | Provides actor-specific title, back link and future action placement.        |
| S07  | Future action slot    | Command sidecars may render role/status-specific actions later.              |

## 5. Visual Client Implementation Flow

```text
[Client Page Shell]
pages/agreements/details/ClientAgreementExchangeDetailsPage.tsx

Owns:
  client route/page composition;
  reading exchangeId route param;
  client title/copy;
  client loading/error/not-found/access branches;
  client back link to "Мои договоры";
  client-specific details navigation conventions.

Uses:
  useAgreementExchangeDetailsQuery(exchangeId)
  AgreementExchangeDetailsView
```

```text
[Employee Page Shell]
pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx

Owns:
  employee route/page composition;
  reading exchangeId route param;
  employee title/copy;
  employee loading/error/not-found/access branches;
  employee back link to Employee agreement exchanges dashboard;
  employee-specific details navigation conventions.

Uses:
  useAgreementExchangeDetailsQuery(exchangeId)
  AgreementExchangeDetailsView
```

```text
[Entity API Layer]
entities/agreement-exchange/api/getAgreementExchangeDetails.ts
entities/agreement-exchange/api/agreementExchangeApiTypes.ts

Owns:
  shared details endpoint wrapper;
  generated DTO aliases near agreement-exchange entity.

Uses:
  shared/api/fetchJson
  shared/api/generated/openapi-types
```

```text
[Entity Model Layer]
entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.ts
entities/agreement-exchange/model/agreementExchangeQueryKeys.ts
entities/agreement-exchange/model/agreementExchangeTypes.ts

Owns:
  React Query details hook;
  details query key;
  details view model helpers if needed.
```

```text
[Widget Layer]
widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
widgets/agreement-exchange-details/AgreementExchangeStatusPanel.tsx
widgets/agreement-exchange-details/AgreementExchangeRequestSummary.tsx
widgets/agreement-exchange-details/AgreementActiveProposalPanel.tsx
widgets/agreement-exchange-details/AgreementProposalHistory.tsx
widgets/agreement-exchange-details/AgreementDocumentRefList.tsx

Owns:
  common details layout;
  exchange status display;
  request summary display;
  active proposal display;
  full proposal history display;
  document refs display;
  optional action slot placement.

Does not own:
  actor-specific page shell;
  command mutations;
  server visibility;
  file download bytes.
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
  getAgreementExchangeDetails()
  agreement exchange business wrappers
```

Implementation flow table:

| Step | Layer            | Responsibility                                               |
| ---- | ---------------- | ------------------------------------------------------------ |
| I01  | Client page      | Parses `exchangeId` and renders client details shell.        |
| I02  | Employee page    | Parses `exchangeId` and renders employee details shell.      |
| I03  | Entity query     | Loads shared agreement exchange details.                     |
| I04  | Entity API       | Calls shared details endpoint.                               |
| I05  | Shared API infra | Executes generic GET and ProblemDetails mapping.             |
| I06  | Widget           | Renders shared details content and proposal history.         |
| I07  | Page shell       | Supplies actor-specific title/back link/future action slots. |

## 6. Client API / Server Contract

Target endpoint from server draft:

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

Target response shape:

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

The server draft explicitly includes request summary, active proposal, proposal list, document refs and `currentActorSide`; document references are returned, not file bytes. 

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

## 7. Role-Based UI Rules

Shared widget may receive page-level props:

```ts
type AgreementExchangeDetailsViewProps = {
  details: AgreementExchangeDetails;
  viewerRole: "Client" | "Employee";
  renderActions?(details: AgreementExchangeDetails): React.ReactNode;
};
```

`viewerRole` is UI-only:

```text
viewerRole may affect wording, labels, empty states, back link and visual hints.
viewerRole must not be treated as authorization.
```

Server remains the security boundary.

## 8. Request Details / Exchange Details Composition Rule

Do not merge request details and exchange details into one huge frontend concept.

This details widget shows exchange details and the request summary returned by exchange details endpoint.

If a page needs richer request data later, it can compose:

```text
request details query
agreement exchange details query
```

But this sidecar does not change request details DTOs and does not move exchange proposal history into request details.

## 9. Security / Protection

Client-side visibility is UX only.

Server owns:

```text
auth/session
current role resolution
ClientAccountId filtering
Employee active-session visibility
exchange visibility
command permissions
lifecycle checks
```

The server draft states that client access is protected by `ClientAccountId`, Employee access first pass allows active Employee visibility, and command endpoints still must protect writes separately; UI button visibility is not authorization. 

Client behavior:

```text
- page shows not-found/access state for 404/403;
- page does not expose raw ClientAccountId;
- page does not decide ownership;
- widget does not execute commands.
```

## 10. Questions / Decisions

### Blocked / unresolved

| ID                            | Status  | Question                                        | Current direction                                          |
| ----------------------------- | ------- | ----------------------------------------------- | ---------------------------------------------------------- |
| `Q-L2-AGR-DETAILS-CLIENT-001` | blocked | Exact generated endpoint path?                  | Use `SL-AGR-EXCH-004` OpenAPI after server implementation. |
| `Q-L2-AGR-DETAILS-CLIENT-002` | blocked | Exact generated DTO names?                      | Alias generated DTOs in `entities/agreement-exchange/api`. |
| `Q-L2-AGR-DETAILS-CLIENT-003` | blocked | Are date/status/sender fields strings or enums? | Follow generated OpenAPI.                                  |
| `Q-L2-AGR-DETAILS-CLIENT-004` | blocked | How are document refs linked to download later? | Show refs only; file download slice owns links/bytes.      |

### Accepted

| ID                            | Status   | Question                                        | Current direction                             |
| ----------------------------- | -------- | ----------------------------------------------- | --------------------------------------------- |
| `Q-L2-AGR-DETAILS-CLIENT-005` | accepted | One shared details endpoint?                    | Yes, first pass.                              |
| `Q-L2-AGR-DETAILS-CLIENT-006` | accepted | One shared frontend details query/model/widget? | Yes.                                          |
| `Q-L2-AGR-DETAILS-CLIENT-007` | accepted | Separate page shells?                           | Yes, Client and Employee routes/pages differ. |
| `Q-L2-AGR-DETAILS-CLIENT-008` | accepted | Separate client/employee API wrappers?          | No, not while response shape is common.       |
| `Q-L2-AGR-DETAILS-CLIENT-009` | accepted | Full proposal history in details?               | Yes.                                          |
| `Q-L2-AGR-DETAILS-CLIENT-010` | accepted | Document bytes in details?                      | No, refs only.                                |
| `Q-L2-AGR-DETAILS-CLIENT-011` | accepted | Command buttons executed here?                  | No, future command sidecars own actions.      |

## 11. Behavior Coverage

| Behavior                                | How client sidecar covers it                                         |
| --------------------------------------- | -------------------------------------------------------------------- |
| Client opens own exchange details       | Client page calls shared details query; server filters by session.   |
| Employee opens visible exchange details | Employee page calls shared details query; server filters by session. |
| Exchange status is visible              | Details widget renders `exchangeStatus`.                             |
| Request summary is visible              | Details widget renders `request` summary.                            |
| Active proposal is visible              | Details widget renders `activeProposal`.                             |
| Full proposal history is visible        | Details widget renders `proposals` ordered as returned.              |
| Proposal sender identity is visible     | Details widget renders sender/senderId.                              |
| Document refs are visible               | Details widget renders document metadata only.                       |
| Details read does not mutate exchange   | No command handlers/forms in this sidecar.                           |
| Commands                                | Out of scope; future feature sidecars.                               |

## 12. Verification Plan

Component tests:

```text
- Client page renders client title/back link.
- Employee page renders employee title/back link.
- Both pages use shared AgreementExchangeDetailsView.
- Details view renders exchange status.
- Details view renders request summary.
- Details view renders active proposal.
- Details view renders proposal history.
- Details view renders proposal sender/senderId.
- Details view renders document refs.
- Details view does not render command forms by default.
```

Entity API/query tests:

```text
- getAgreementExchangeDetails calls shared endpoint with exchangeId.
- wrapper imports fetchJson from shared API infrastructure.
- wrapper uses generated DTO aliases from entity api types after contract exists.
- query hook is disabled for invalid/missing exchangeId.
- query hook exposes loading/error/success data.
- no client/employee-specific API wrapper exists first pass.
```

E2E planned:

```text
Client session:
  open client agreement exchange details
  assert status, request summary, active proposal and history visible

Employee session:
  open employee agreement exchange details
  assert status, request summary, active proposal and history visible
```

Non-goals:

```text
- do not test counter-proposal/accept/final-refuse command behavior here;
- do not test file download here;
- do not assert server filtering internals from client tests;
- do not assert React Query cache internals in E2E.
```

## 13. Suggested File Placement

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

## 14. Next Step

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

Numbering note: the server draft says docs may need sync if old `SL-AGR-EXCH-004/005` names still refer to accept/final-refuse. That should be handled before committing planning docs to avoid slice-code mismatch.

## 15. Implementation Checklist

```text
[ ] confirm/apply SL-AGR-EXCH-004 backend endpoint
[ ] confirm generated endpoint and DTO names
[ ] add entities/agreement-exchange/api/getAgreementExchangeDetails.ts
[ ] update entities/agreement-exchange/api/agreementExchangeApiTypes.ts
[ ] add useAgreementExchangeDetailsQuery
[ ] add shared AgreementExchangeDetailsView widget
[ ] add status/request summary/active proposal panels
[ ] add proposal history component
[ ] add document reference display component
[ ] add Client agreement exchange details page shell
[ ] add Employee agreement exchange details page shell
[ ] use one shared endpoint wrapper first pass
[ ] do not add actor-specific details wrappers first pass
[ ] do not add shared/api/agreementExchangeApi.ts
[ ] render loading/error/not-found/success states
[ ] keep command buttons/forms out of this read sidecar except optional slots
[ ] add component/entity query tests
[ ] add E2E smoke when server/test setup is ready
[ ] regenerate OpenAPI/types if this implementation package changes API shape
```
