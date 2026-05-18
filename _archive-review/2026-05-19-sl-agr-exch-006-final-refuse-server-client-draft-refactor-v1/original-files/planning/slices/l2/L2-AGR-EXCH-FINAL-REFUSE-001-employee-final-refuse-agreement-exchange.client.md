# L2-AGR-EXCH-FINAL-REFUSE-001.client — Employee Final Refuse Agreement Exchange

Status: client command sidecar draft / Employee-only first pass / Employee details action slot / nullable body / `204 No Content` / blocked until `SL-AGR-EXCH-006` server endpoint and generated OpenAPI contract exist
Parent server slice: `SL-AGR-EXCH-006 — Final Refuse Agreement Exchange`
Host read sidecar: `L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages`
Actor: Employee
Slice type: client command sidecar
Placement: Employee agreement exchange details action area only

## 1. Scope

This sidecar owns:

```text
- Employee Final Refuse action from agreement exchange details;
- first-pass placement only on Employee agreement exchange details page;
- rendering Final Refuse only when details state says or implies Employee can final-refuse;
- disabled/blocked state when final refusal is unavailable;
- visible unavailable reason when available;
- optional final refusal reason field;
- nullable/optional request body support;
- client-side shape validation for reason only;
- optional confirmation before final negative decision;
- submit command:
  POST /api/agreement-exchanges/{exchangeId}/final-refuse;
- pending state while command is in flight;
- duplicate-submit protection;
- visible validation/success/error feedback;
- refresh agreement exchange details after success;
- refresh agreement exchange list after success if cache exists;
- refresh related request details after success only if request details query key/cache exists;
- no Client final refusal;
- no accept behavior;
- no counter-proposal;
- no proposal version creation.
```

This is a final negative decision by Employee. It affects AgreementProposalExchange and related ConnectionRequest on the server, but the client only submits the command and refetches reads.

## 2. Out of Scope

```text
- backend endpoint implementation -> SL-AGR-EXCH-006;
- Agreement Exchange list read -> SL-AGR-EXCH-003;
- Agreement Exchange details read -> SL-AGR-EXCH-004;
- initial exchange creation -> SL-AGR-EXCH-001;
- counter-proposal version creation -> SL-AGR-EXCH-002;
- Client accept active proposal -> SL-AGR-EXCH-005;
- Client-side final refusal;
- document upload/download;
- manually setting request/exchange statuses in client;
- local CSRF mechanics;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api.
```

Important guardrail:

```text
Client final refusal is out of scope.
Do not infer or implement Client final refusal from the shared details page.
```

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

SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
  Owns backend Employee final-refuse command:
    POST /api/agreement-exchanges/{exchangeId}/final-refuse.

L2-AGR-EXCH-FINAL-REFUSE-001.client
  Owns Employee final-refuse form/action/mutation.

L2-AGR-EXCH-ACCEPT-001.client
  Owns Client accept active proposal.

L2-AGR-EXCH-SEND-PROPOSAL-001.client
  Owns send proposal version.
```

## 4. Visual UI / Scenario Flow

```text
Employee opens agreement exchange details
        ↓
Details page shows active agreement exchange
        ↓
Employee sees Final Refuse action
        ↓
Employee opens final refusal form
        ↓
Employee may provide refusal reason
        ↓
Employee clicks Final Refuse
        ↓
Optional confirmation appears
        ↓
Employee confirms
        ↓
Final-refuse command is sent
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ command accepted             │ command rejected             │
 ▼                              ▼
details/list/request refresh    validation/error feedback visible
        ↓                       previous state remains safe
exchange status shows FinallyRefused
related request status shows AgreementExchangeFailed
```

In ordinary words:

Employee opens agreement exchange details. If the exchange is active and final refusal is allowed, Employee can submit final refusal with an optional reason. On success, exchange details and list are refreshed, and request details are refreshed only if that query/cache exists. On failure, the form shows validation or command feedback and does not pretend the exchange was refused.

Scenario flow table:

| Step | Layer                 | Responsibility                                                  |
| ---- | --------------------- | --------------------------------------------------------------- |
| S01  | Employee              | Opens Employee agreement exchange details.                      |
| S02  | Details read state    | Shows active exchange and proposal state.                       |
| S03  | Employee action area  | Shows Final Refuse when available.                              |
| S04  | Optional reason       | Employee may provide final refusal reason.                      |
| S05  | Optional confirmation | Confirms final negative decision.                               |
| S06  | Command feature       | Sends Final Refuse command.                                     |
| S07  | Accepted outcome      | Details/list/request reads refetch where present.               |
| S08  | Rejected outcome      | Validation/error feedback visible; previous state remains safe. |

## 5. Visual Client Implementation Flow

```text
[Employee Details Page Shell]
pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx

Lives here:
  EmployeeAgreementExchangeDetailsPage

Uses:
  useAgreementExchangeDetailsQuery(exchangeId)
  AgreementExchangeDetailsView
  FinalRefuseAgreementExchangeForm through details action slot

Owns:
  employee route/page shell;
  exchangeId route param parsing;
  employee title/back link;
  passing viewerRole="Employee";
  passing Final Refuse action into details action slot when available.

Does not own:
  final-refuse mutation;
  command endpoint wrapper;
  server lifecycle/security rules;
  Client final refusal.
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
  final-refuse mutation;
  final-refuse endpoint wrapper;
  lifecycle/security rules.
```

```text
[Command Feature UI]
features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.tsx

Owns:
  Final Refuse form/button;
  optional reason field;
  optional confirmation;
  client-side reason validation;
  pending state;
  disabled/unavailable state;
  command error feedback;
  accessible label/copy.

Does not own:
  details query;
  details page shell;
  accept/counter-proposal commands.
```

```text
[Command Feature Model]
features/agreement-exchange/final-refuse/model/useFinalRefuseAgreementExchangeMutation.ts

Owns:
  mutation;
  details/list/request invalidation after success;
  command error propagation to UI.

Uses:
  finalRefuseAgreementExchange(exchangeId, payload?)
  agreementExchangeQueryKeys.details(exchangeId)
  agreementExchangeQueryKeys.list(...)
  employee/request details query keys only if available
```

```text
[Command Feature API]
features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.ts
features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchangeApiTypes.ts

Owns:
  POST endpoint wrapper:
    POST /api/agreement-exchanges/{exchangeId}/final-refuse

Uses:
  shared/api/fetchJson
  shared/api/generated/openapi-types.ts after generated contract exists
```

Implementation flow table:

| Step | Layer            | Responsibility                                                    |
| ---- | ---------------- | ----------------------------------------------------------------- |
| I01  | Employee page    | Hosts Final Refuse form in details action slot.                   |
| I02  | Details widget   | Provides read layout and action slot only.                        |
| I03  | Feature UI       | Handles optional reason, validation, confirmation, pending/error. |
| I04  | Feature model    | Submits mutation and invalidates relevant reads.                  |
| I05  | Feature API      | Calls final-refuse endpoint via `fetchJson`.                      |
| I06  | Shared API infra | Handles generic unsafe request/ProblemDetails behavior.           |

## 6. API Contract

Endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/final-refuse
```

Auth:

```text
Employee only
```

Request body:

```ts
type FinalRefuseAgreementExchangeRequest = {
  reason?: string | null;
};
```

Body handling:

```text
body omitted -> allowed
null body -> allowed
reason omitted -> allowed
reason null -> allowed
reason valid string -> allowed
reason blank / whitespace-only -> validation error
reason longer than 2000 -> validation error
```

Success:

```http
204 No Content
```

Response body:

```text
none
```

Feature-owned generated alias after OpenAPI exists:

```ts
// features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchangeApiTypes.ts
import type { components } from "../../../../shared/api/generated/openapi-types";

export type FinalRefuseAgreementExchangeRequest =
  components["schemas"]["FinalRefuseAgreementExchangeDto"];
```

Feature-owned API wrapper direction:

```ts
// features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.ts
import { fetchJson } from "../../../../shared/api/fetchJson";
import type { FinalRefuseAgreementExchangeRequest } from "./finalRefuseAgreementExchangeApiTypes";

export const finalRefuseAgreementExchange = (
  exchangeId: number,
  payload?: FinalRefuseAgreementExchangeRequest,
): Promise<void> =>
  fetchJson<void>(
    `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}/final-refuse`,
    {
      method: "POST",
      body: payload === undefined ? undefined : JSON.stringify(payload),
    },
  );
```

Do not add:

```text
src/shared/api/agreementExchangeApi.ts
```

## 7. Reason Handling

```text
no body:
  allowed

null body:
  allowed

missing reason:
  allowed

reason null:
  allowed

empty UI field:
  submit as no body or { reason: null } according to generated/server binding

whitespace-only provided reason:
  invalid; show client validation error; do not silently convert to null

valid reason:
  trim before submit if project convention does this

reason > 2000 chars:
  invalid; show client validation error
```

Server remains source of truth.

## 8. Expected Read State After Refetch

After success, client refetches relevant reads and expects:

```text
exchange.Status = AgreementExchangeStatus.FinallyRefused
related request.Status = RequestStatus.AgreementExchangeFailed
proposal count unchanged
ActiveProposalVersion unchanged
no new proposal version created
```

If details DTO later exposes final refusal audit fields, UI may show:

```text
FinalRefusedByEmployeeId
FinalRefusedAt
FinalRefusalReason
```

But first-pass client should not require these fields unless generated DTO includes them.

The command affects two aggregates on the server through domain methods:

```text
exchange.FinalRefuseProposal(...)
request.MarkAgreementExchangeFailed(...)
```

The application layer must not manually set statuses. 

## 9. Action Availability

Preferred source:

```text
details.actionAvailability.canFinalRefuseAgreementExchange
```

Fallback display-only derivation if details DTO has no action availability first pass:

```text
viewerRole == "Employee"
exchangeStatus == "AwaitingClientConfirmation"
  or exchangeStatus == "AwaitingEmployeeResponse"
```

This is UX only.

Server must still enforce:

```text
Employee role
Employee capability
exchange lifecycle
request lifecycle
no-new-proposal-version rule
```

## 10. Security / Protection

Client sends:

```text
exchangeId in route
optional reason
```

Client never sends:

```text
employeeId
requestId as authority
target exchange status
target request status
actor side
finalRefusedAt
```

Client behavior:

```text
- shows disabled/hidden action for unavailable state;
- shows pending state while refusing;
- shows validation/ProblemDetails feedback on rejection;
- refetches details/list/request after success where relevant;
- does not optimistically mark FinallyRefused or AgreementExchangeFailed before server success.
```

Server remains authoritative:

```text
UI button visibility is not authorization.
```

## 11. Questions / Decisions

### Blocked

| ID                                 | Status  | Question                                                   | Current direction                                                     |
| ---------------------------------- | ------- | ---------------------------------------------------------- | --------------------------------------------------------------------- |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-001` | blocked | Exact generated operation name?                            | Use OpenAPI after server implementation.                              |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-002` | blocked | Exact generated request DTO name?                          | Expected `FinalRefuseAgreementExchangeDto`; confirm after generation. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-003` | blocked | Body when no reason?                                       | Follow generated/server binding: omitted body or `{ reason: null }`.  |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-004` | blocked | Does generated OpenAPI expose this command as 204 no body? | Confirm after generation.                                             |

### Accepted

| ID                                 | Status   | Question                                    | Current direction                                                                  |
| ---------------------------------- | -------- | ------------------------------------------- | ---------------------------------------------------------------------------------- |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-005` | accepted | Is this Employee-only first pass?           | Yes. Client final refusal is out of scope.                                         |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-006` | accepted | Is reason required?                         | No. Nullable/optional body.                                                        |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-007` | accepted | Does success return DTO?                    | No, `204 No Content`.                                                              |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-008` | accepted | Does final refusal create proposal version? | No.                                                                                |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-009` | accepted | Which states after refetch?                 | `AgreementExchangeStatus.FinallyRefused`, `RequestStatus.AgreementExchangeFailed`. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-010` | accepted | Where does wrapper live?                    | `features/agreement-exchange/final-refuse/api`.                                    |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-011` | accepted | Placement?                                  | Employee agreement exchange details action area.                                   |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-012` | accepted | Is request details refresh mandatory?       | No. Only if request details query/cache exists.                                    |

## 12. Behavior Coverage

| Behavior                                        | How client sidecar covers it                                              |
| ----------------------------------------------- | ------------------------------------------------------------------------- |
| Employee can finally refuse active exchange     | FinalRefuse form sends command from Employee details page.                |
| Client cannot final-refuse                      | No Client final-refuse UI; server rejects anyway.                         |
| Any active Employee can final-refuse first pass | Client sends no owner id; server policy decides.                          |
| Missing/null reason allowed                     | Form allows empty reason and sends omitted/null body per contract.        |
| Whitespace-only reason rejected                 | Client validation shows error; server still validates.                    |
| Exchange becomes FinallyRefused                 | Refetch shows `AgreementExchangeStatus.FinallyRefused`.                   |
| Request becomes AgreementExchangeFailed         | Request/details refetch can show `RequestStatus.AgreementExchangeFailed`. |
| Proposal history unchanged                      | Client sends command only, then refetches.                                |
| Accept                                          | Out of scope.                                                             |
| Counter-proposal                                | Out of scope.                                                             |

## 13. Verification Plan

Component tests:

```text
- FinalRefuse form renders enabled when available;
- disabled/hidden when unavailable;
- unavailable reason is visible when provided;
- reason field is optional;
- empty reason does not block submit;
- whitespace-only reason shows validation error;
- too-long reason shows validation error;
- optional confirmation can be confirmed/cancelled;
- submit calls mutation with exchangeId and optional payload;
- pending disables submit;
- error feedback is visible;
- success callback/invalidation runs;
- Client details page does not render Employee Final Refuse action first pass.
```

API/model tests:

```text
- finalRefuseAgreementExchange posts to /api/agreement-exchanges/{exchangeId}/final-refuse;
- wrapper supports omitted payload;
- wrapper supports { reason: string };
- wrapper sends no body / null reason according to chosen contract for empty reason;
- wrapper returns Promise<void> / handles 204;
- wrapper imports fetchJson from shared API infrastructure;
- mutation invalidates agreement exchange details query;
- mutation invalidates agreement exchange list query if present;
- mutation invalidates related request details query only if query key/cache exists;
- no shared/api business wrapper exists.
```

E2E planned:

```text
Employee session:
  open /employee/agreements/:exchangeId
  exchange is active
  click Final Refuse
  optionally enter reason
  confirm
  assert after refetch:
    exchange status shows FinallyRefused
    related request shows AgreementExchangeFailed if visible on page
```

Non-goals:

```text
- no Client final-refuse E2E here;
- no accept test here;
- no counter-proposal test here;
- no server DB internals in client tests;
- no React Query cache internals in E2E.
```

## 14. Suggested File Placement

```text
src/features/agreement-exchange/final-refuse/api/
  finalRefuseAgreementExchange.ts
  finalRefuseAgreementExchangeApiTypes.ts

src/features/agreement-exchange/final-refuse/model/
  useFinalRefuseAgreementExchangeMutation.ts

src/features/agreement-exchange/final-refuse/ui/
  FinalRefuseAgreementExchangeForm.tsx
  finalRefuseAgreementExchangeFormConst.ts
  finalRefuseAgreementExchangeForm.css

src/pages/employee/agreements/details/
  EmployeeAgreementExchangeDetailsPage.tsx

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
src/pages/agreements/details/ClientAgreementExchangeDetailsPage final-refuse wiring
```

## 15. Implementation Checklist

```text
[ ] confirm SL-AGR-EXCH-006 backend endpoint exists
[ ] confirm generated operation path/name
[ ] confirm generated request DTO name
[ ] confirm nullable body / optional reason binding
[ ] add feature-owned API wrapper
[ ] add generated request DTO alias near feature
[ ] support omitted payload
[ ] support { reason: string } payload
[ ] do not send employeeId from client
[ ] add useFinalRefuseAgreementExchangeMutation
[ ] invalidate agreement exchange details query after success
[ ] invalidate agreement exchange list query if cache exists
[ ] invalidate related request details query only if query key/cache exists
[ ] add FinalRefuseAgreementExchangeForm
[ ] allow empty reason
[ ] reject whitespace-only reason in client validation
[ ] reject too-long reason in client validation
[ ] add optional confirmation
[ ] wire form into Employee agreement exchange details action slot only
[ ] do not wire Client final-refuse UI
[ ] do not implement accept here
[ ] do not implement counter-proposal here
[ ] add component tests
[ ] add API/model tests
[ ] add E2E when backend/test setup is ready
```

## 16. Next Step

```text
1. Confirm/apply SL-AGR-EXCH-006 backend endpoint.
2. Run OpenAPI/type generation.
3. Confirm generated path/operation/request DTO and 204 response.
4. Implement feature-owned API wrapper.
5. Implement mutation.
6. Implement FinalRefuseAgreementExchangeForm.
7. Wire into Employee agreement exchange details action slot only.
8. Add component/API/model tests.
9. Add E2E when backend/test setup is ready.
```
