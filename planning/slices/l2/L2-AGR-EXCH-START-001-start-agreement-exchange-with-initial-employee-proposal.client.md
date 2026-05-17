# L2-AGR-EXCH-START-001.client — Start Agreement Exchange With Initial Employee Proposal

Status: client command sidecar draft / Employee-only first pass / Employee request details action slot / preferred `POST /api/agreement-exchanges` / preferred response with `exchangeId` / blocked until `SL-AGR-EXCH-001` server endpoint and generated OpenAPI contract exist  
Parent server slice: `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`  
Host read sidecar: `L2-EMP-DETAILS-001.client — Employee Request Details`  
Actor: Employee  
Slice type: client command sidecar  
Placement: Employee request details action area only

## 0. Contract Alignment Note

Current server slice/source direction may still describe:

```text
POST /api/employee/requests/{requestId}/agreement-exchange/start
success: 204 No Content
```

This client sidecar records the newer client-preferred contract direction:

```text
POST /api/agreement-exchanges
success: 200/201 with exchangeId
```

Implementation rule:

```text
Do not guess the final route/response.
Use generated OpenAPI after `SL-AGR-EXCH-001` server implementation is finalized.

If OpenAPI confirms 204 No Content, use the fallback stay-on-request-details/refetch behavior.
If OpenAPI confirms exchangeId response, navigate to Employee agreement exchange details.
```

## 1. Scope

This sidecar owns:

```text
- Employee Start Agreement Exchange action from Employee request details;
- first-pass placement only on Employee request details page;
- rendering Start Agreement Exchange only when request details state says or implies exchange can be started;
- disabled/blocked state when start is unavailable;
- visible unavailable reason when available;
- initial Employee proposal form;
- initial proposal fields according to generated server contract;
- proposal document/reference fields according to generated server contract;
- optional comment/notes if generated server contract supports it;
- submit command that creates AgreementProposalExchange with initial Employee proposal;
- pending state while command is in flight;
- duplicate-submit protection;
- visible validation/success/error feedback;
- refresh Employee request details after success;
- refresh shared agreement exchange list after success if cache exists;
- navigate to Employee agreement exchange details when response contains exchangeId;
- fallback stay-on-request-details behavior if server returns 204 No Content;
- no Client start exchange;
- no send-proposal inside existing exchange;
- no accept;
- no final refuse.
```

This command creates the first `AgreementProposalExchange` for an approved request and creates the initial Employee proposal. It is not request review approve, not counter-proposal, not accept and not final refuse.

## 2. Out of Scope

```text
- backend endpoint implementation -> SL-AGR-EXCH-001;
- Employee request details read endpoint -> L2-EMP-DETAILS-001.client / SL-EMP-REQ-002;
- Agreement Exchange list read -> SL-AGR-EXCH-003 / L2-AGR-EXCH-LIST-001.client;
- Agreement Exchange details read -> SL-AGR-EXCH-004 / L2-AGR-EXCH-DETAILS-001.client;
- send proposal version inside existing exchange -> SL-AGR-EXCH-002 / L2-AGR-EXCH-SEND-PROPOSAL-001.client;
- Client accept active proposal -> SL-AGR-EXCH-005 / L2-AGR-EXCH-ACCEPT-001.client;
- Employee final refuse -> SL-AGR-EXCH-006 / L2-AGR-EXCH-FINAL-REFUSE-001.client;
- document upload/storage if proposal document is uploaded separately;
- file download/binary serving;
- local CSRF mechanics;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api.
```

Important guardrail:

```text
Start Agreement Exchange happens from Employee request details because exchange does not exist yet.

After exchange exists, proposal negotiation happens from Agreement Exchange details.
```

## 3. Related Slices / Owners

```text
L2-EMP-DETAILS-001.client — Employee Request Details
  Owns Employee request details page/read state/action slot.

SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
  Owns backend command that creates AgreementProposalExchange
  with initial Employee proposal.

L2-AGR-EXCH-START-001.client
  Owns Employee UI/form/mutation for starting exchange.

SL-AGR-EXCH-003 / L2-AGR-EXCH-LIST-001.client
  Own shared agreement exchange list after exchange exists.

SL-AGR-EXCH-004 / L2-AGR-EXCH-DETAILS-001.client
  Own shared agreement exchange details after exchange exists.

L2-AGR-EXCH-SEND-PROPOSAL-001.client
  Owns sending next proposal version inside existing exchange.

L2-AGR-EXCH-ACCEPT-001.client
  Owns Client accept active proposal.

L2-AGR-EXCH-FINAL-REFUSE-001.client
  Owns Employee final refuse.
```

## 4. Visual UI / Scenario Flow

```text
Employee opens approved request details
        ↓
Request details show Agreement Exchange can be started
        ↓
Employee opens Start Agreement Exchange action
        ↓
Employee fills initial proposal form
        ↓
Employee submits
        ↓
Start Agreement Exchange command is sent
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ command accepted             │ command rejected             │
 ▼                              ▼
exchange created                validation/error feedback visible
initial proposal created        form remains safe/editable
        ↓
request details/list refresh
        ↓
navigate to Employee exchange details
if exchangeId is returned
```

In ordinary words:

Employee opens an approved request. If no agreement exchange exists yet and the request state allows starting one, Employee submits an initial proposal. The server creates the exchange and the first proposal. The client refreshes request/exchange reads and preferably navigates to Employee exchange details.

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Employee | Opens approved Employee request details. |
| S02 | Request details read state | Shows that Agreement Exchange can be started. |
| S03 | Employee action area | Shows Start Agreement Exchange action when available. |
| S04 | Initial proposal form | Employee fills generated-contract proposal fields. |
| S05 | Command feature | Sends Start Agreement Exchange command. |
| S06 | Accepted outcome | Request details/list refreshed; exchange list refreshed if present. |
| S07 | Navigation outcome | Navigate to Employee exchange details if `exchangeId` returned. |
| S08 | Rejected outcome | Validation/error feedback visible; form remains safe/editable. |

## 5. Visual Client Implementation Flow

```text
[Employee Request Details Page]
pages/employee/requests/details/EmployeeRequestDetailsPage.tsx

Lives here:
  EmployeeRequestDetailsPage

Uses:
  useEmployeeRequestDetailsQuery(requestId)
  EmployeeRequestDetailsView
  StartAgreementExchangeForm through request details action slot

Owns:
  request details shell;
  requestId route param;
  request read states;
  action slot placement;
  passing StartAgreementExchangeForm when available.

Does not own:
  Start Agreement Exchange mutation;
  agreement exchange endpoint wrapper;
  proposal form internals;
  agreement exchange details layout.
```

```text
[Command Feature UI]
features/agreement-exchange/start-exchange/ui/StartAgreementExchangeForm.tsx

Owns:
  initial proposal form;
  proposal document/reference inputs from generated contract;
  optional comment field if supported;
  submit button;
  pending state;
  client-side shape validation;
  validation/error feedback;
  optional success callback/navigation handoff.

Does not own:
  request details page shell;
  exchange details page shell;
  backend lifecycle rules.
```

```text
[Command Feature Model]
features/agreement-exchange/start-exchange/model/useStartAgreementExchangeMutation.ts

Owns:
  mutation;
  request details invalidation after success;
  agreement exchange list invalidation after success if present;
  optional navigation handoff if response contains exchangeId;
  command error propagation to UI.

Uses:
  startAgreementExchange(payload)
  employeeRequestQueryKeys.details(requestId)
  agreementExchangeQueryKeys.list(...)
```

```text
[Command Feature API]
features/agreement-exchange/start-exchange/api/startAgreementExchange.ts
features/agreement-exchange/start-exchange/api/startAgreementExchangeApiTypes.ts

Owns:
  command endpoint wrapper;
  generated request/response aliases near feature.

Uses:
  shared/api/fetchJson
  shared/api/generated/openapi-types
```

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Employee request details page | Hosts Start Agreement Exchange form in action slot. |
| I02 | Feature UI | Handles initial proposal fields, validation, pending and errors. |
| I03 | Feature model | Submits mutation and invalidates request/exchange reads. |
| I04 | Feature API | Calls generated start exchange endpoint via `fetchJson`. |
| I05 | Shared API infra | Handles generic unsafe request/ProblemDetails behavior. |
| I06 | Router/navigation | Navigates to Employee exchange details if `exchangeId` returned. |

## 6. API Contract Direction

Exact contract is blocked until `SL-AGR-EXCH-001` server/OpenAPI.

Preferred route family:

```http
POST /api/agreement-exchanges
```

Preferred request direction:

```ts
type StartAgreementExchangeRequest = {
  requestId: number;
  initialProposal: <GeneratedInitialEmployeeProposalDto>;
};
```

Preferred success response:

```ts
type StartAgreementExchangeResponse = {
  exchangeId: number;
};
```

Preferred success status:

```text
201 Created or 200 OK with exchangeId
```

Reason:

```text
Client can navigate to:
  /employee/agreements/:exchangeId
```

Fallback if server returns `204 No Content`:

```text
- stay on Employee request details;
- refetch request details;
- refetch agreement exchange list if present;
- do not navigate unless exchangeId can be discovered through refreshed data.
```

Feature-owned generated aliases after OpenAPI exists:

```ts
// features/agreement-exchange/start-exchange/api/startAgreementExchangeApiTypes.ts
import type { components } from "../../../../shared/api/generated/openapi-types";

export type StartAgreementExchangeRequest =
  components["schemas"]["<GeneratedStartAgreementExchangeRequestDtoName>"];

export type StartAgreementExchangeResponse =
  components["schemas"]["<GeneratedStartAgreementExchangeResponseDtoName>"];
```

Feature-owned API wrapper direction:

```ts
// features/agreement-exchange/start-exchange/api/startAgreementExchange.ts
import { fetchJson } from "../../../../shared/api/fetchJson";
import type {
  StartAgreementExchangeRequest,
  StartAgreementExchangeResponse,
} from "./startAgreementExchangeApiTypes";

export const startAgreementExchange = (
  payload: StartAgreementExchangeRequest,
): Promise<StartAgreementExchangeResponse> =>
  fetchJson<StartAgreementExchangeResponse>(
    "/api/agreement-exchanges",
    {
      method: "POST",
      body: JSON.stringify(payload),
    },
  );
```

Do not add:

```text
src/shared/api/agreementExchangeApi.ts
```

## 7. Action Availability

Preferred source from Employee request details:

```text
requestDetails.actionAvailability.canStartAgreementExchange
```

Fallback display-only derivation if no explicit action availability first pass:

```text
viewerRole == "Employee"
requestStatus == "Approved"
agreementExchangeSummary == null
```

This fallback is UX only.

Server must still enforce:

```text
Employee role
Employee capability
request visibility
request lifecycle: Approved
no existing exchange if domain requires uniqueness
initial proposal payload validity
proposal author = current Employee
```

Important:

```text
Approve Request Review does not create AgreementProposalExchange.
Start Agreement Exchange is a separate Employee command after request approval.
```

## 8. Security / Protection

Client sends:

```text
requestId
initial proposal payload
```

Client never sends:

```text
employeeId
target exchange status
proposal version number
actor side as authority
request status
```

Client behavior:

```text
- shows disabled/hidden action for unavailable state;
- shows pending state while starting exchange;
- shows validation/ProblemDetails feedback on rejection;
- refetches request details and exchange list after success;
- does not optimistically create exchange before server success.
```

Server owns:

```text
auth/session
Employee resolution
request visibility
request lifecycle
exchange uniqueness
initial proposal creation
proposal author = current Employee
```

UI button visibility is not authorization.

## 9. Questions / Decisions

### Blocked

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-START-CLIENT-001` | blocked | Exact endpoint route? | Prefer `POST /api/agreement-exchanges`; confirm via OpenAPI. |
| `Q-L2-AGR-START-CLIENT-002` | blocked | Exact initial proposal DTO fields? | Use generated DTO aliases. |
| `Q-L2-AGR-START-CLIENT-003` | blocked | Does response include `exchangeId`? | Prefer yes for navigation. |
| `Q-L2-AGR-START-CLIENT-004` | blocked | How proposal document is represented? | Follow server/document contract. |
| `Q-L2-AGR-START-CLIENT-005` | blocked | Which request details field exposes availability? | Prefer explicit `canStartAgreementExchange`. |
| `Q-L2-AGR-START-CLIENT-013` | blocked | Current server slice still uses request-scoped route + 204. Should server slice be changed to the preferred exchange-root contract? | Do not change client implementation until generated OpenAPI resolves this. |

### Accepted

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-START-CLIENT-006` | accepted | Is this Employee-only first pass? | Yes. |
| `Q-L2-AGR-START-CLIENT-007` | accepted | Is this send proposal inside existing exchange? | No. This creates exchange. |
| `Q-L2-AGR-START-CLIENT-008` | accepted | Does client send Employee id? | No. |
| `Q-L2-AGR-START-CLIENT-009` | accepted | Where does wrapper live? | `features/agreement-exchange/start-exchange/api`. |
| `Q-L2-AGR-START-CLIENT-010` | accepted | Placement? | Employee request details action area. |
| `Q-L2-AGR-START-CLIENT-011` | accepted | Does this belong in exchange details? | No, exchange does not exist yet. |
| `Q-L2-AGR-START-CLIENT-012` | accepted | Does Approve Review create exchange? | No. Separate command. |

## 10. Behavior Coverage

| Behavior | How client sidecar covers it |
|---|---|
| Employee starts exchange for approved request | Start form submits command from Employee request details. |
| Initial Employee proposal is provided | Form sends generated initial proposal payload. |
| Exchange becomes visible in agreement list/details | Client refetches agreement exchange list and navigates/refetches details. |
| Request details reflect exchange started | Client invalidates request details after success. |
| Employee id is not spoofed | Client sends no Employee id. |
| Existing exchange cannot be duplicated | Server rejects; UI shows error. |
| Counter-proposal | Out of scope. |
| Accept | Out of scope. |
| Final refuse | Out of scope. |

## 11. Verification Plan

Component tests:

```text
- form renders when start exchange is available;
- form hidden/disabled when unavailable;
- unavailable reason is visible when provided;
- proposal fields follow generated contract;
- submit calls mutation with requestId and proposal payload;
- pending disables submit;
- validation error visible;
- server error feedback visible;
- success callback/navigation runs when exchangeId is returned;
- fallback success keeps user on request details when server returns 204;
- no accept/final-refuse/counter-proposal controls rendered.
```

API/model tests:

```text
- startAgreementExchange posts to generated endpoint;
- wrapper uses generated request/response aliases if response exists;
- wrapper handles 204 fallback if generated contract has no response DTO;
- wrapper imports fetchJson from shared API infrastructure;
- mutation invalidates employee request details;
- mutation invalidates agreement exchange list if present;
- mutation navigates or exposes exchangeId on success if returned;
- no shared/api business wrapper exists.
```

E2E planned:

```text
Employee session:
  open approved Employee request details
  fill initial proposal form
  submit Start Agreement Exchange
  assert exchange created/visible
  assert navigation to /employee/agreements/:exchangeId if response supports it
```

Non-goals:

```text
- no accept/final-refuse E2E here;
- no counter-proposal E2E here;
- no file upload/storage test unless contract includes it;
- no server DB internals in client tests;
- no React Query internals in E2E.
```

## 12. Suggested File Placement

```text
src/features/agreement-exchange/start-exchange/api/
  startAgreementExchange.ts
  startAgreementExchangeApiTypes.ts

src/features/agreement-exchange/start-exchange/model/
  useStartAgreementExchangeMutation.ts

src/features/agreement-exchange/start-exchange/ui/
  StartAgreementExchangeForm.tsx
  startAgreementExchangeFormConst.ts
  startAgreementExchangeForm.css

src/pages/employee/requests/details/
  EmployeeRequestDetailsPage.tsx

src/entities/employee-request/model/
  employeeRequestQueryKeys.ts

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
src/features/agreement-exchange/send-proposal/api/startAgreementExchange.ts
src/pages/agreements/details/ClientAgreementExchangeDetailsPage start-exchange wiring
```

## 13. Next Step

```text
1. Confirm/apply SL-AGR-EXCH-001 backend endpoint.
2. Run OpenAPI/type generation.
3. Confirm generated route, request DTO and response DTO.
4. Implement feature-owned API wrapper.
5. Implement mutation.
6. Implement StartAgreementExchangeForm.
7. Wire into Employee request details action slot only.
8. Add component/API/model tests.
9. Add E2E when backend/test setup is ready.
```

## 14. Implementation Checklist

```text
[ ] confirm final generated endpoint route
[ ] confirm whether success returns exchangeId or 204 No Content
[ ] confirm generated request DTO name
[ ] confirm generated response DTO name, if any
[ ] confirm initial proposal/document field names from OpenAPI
[ ] add feature-owned generated type aliases near feature
[ ] add startAgreementExchange API wrapper in features/agreement-exchange/start-exchange/api
[ ] do not add shared/api/agreementExchangeApi.ts
[ ] add useStartAgreementExchangeMutation
[ ] invalidate employee request details after success
[ ] invalidate agreement exchange list if cache exists
[ ] navigate to /employee/agreements/:exchangeId only when exchangeId is returned
[ ] implement 204 fallback stay-on-request-details behavior
[ ] add StartAgreementExchangeForm
[ ] wire form into Employee request details action slot only
[ ] do not wire into Client pages
[ ] do not wire into Agreement Exchange details page
[ ] do not implement counter-proposal in this sidecar
[ ] do not implement accept/final-refuse in this sidecar
[ ] add component tests
[ ] add API/model tests
[ ] add E2E only after backend/test setup is ready
```

## 15. Guardrail Summary

```text
This is a client command sidecar for starting an exchange from Employee request details.

Exchange does not exist yet, so this action does not belong in Agreement Exchange details.

Do not implement Client start exchange.

Do not implement counter-proposal here.

Do not implement accept/final-refuse here.

Do not send employeeId from the client.

Do not choose proposal version on the client.

Do not manually edit generated OpenAPI/types.

Do not add a business wrapper to shared/api.

Generated OpenAPI decides the route, request DTO and success response.

Preferred UX is navigation to Employee agreement exchange details when exchangeId is returned.

Fallback UX is refetch and remain on Employee request details when success is 204 No Content.
```
