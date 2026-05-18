# L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal

Status: client command sidecar draft / Client-only first pass / details action slot / `204 No Content` / blocked until `SL-AGR-EXCH-005` server endpoint and generated OpenAPI contract exist
Parent server slice: `SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal`
Host read sidecar: `L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages`
Actor: ClientAccount
Slice type: client command sidecar
Placement: Client agreement exchange details action area only

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

This is a final positive decision by Client. It accepts the current active Employee proposal. It does **not** create a new proposal version and does **not** upload/send a proposal.

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
- business-specific wrappers in shared/api.
```

Important guardrail:

```text
Employee accept is out of scope.
Do not infer or implement Employee accept from the shared details page.
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

SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
  Owns backend Client accept command:
    POST /api/agreement-exchanges/{exchangeId}/accept.

L2-AGR-EXCH-ACCEPT-001.client
  Owns Client Accept button/action/mutation.

Future final-refuse client sidecar
  Owns final refusal action.

Counter-proposal sidecar
  Owns send proposal version action.
```

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

| Step | Layer                 | Responsibility                                       |
| ---- | --------------------- | ---------------------------------------------------- |
| S01  | Client                | Opens Client agreement exchange details.             |
| S02  | Details read state    | Shows active Employee proposal.                      |
| S03  | Client action area    | Shows Accept when available.                         |
| S04  | Optional confirmation | Confirms final positive decision if enabled.         |
| S05  | Command feature       | Sends Accept command with no body.                   |
| S06  | Accepted outcome      | Details/list refetch and show Accepted state.        |
| S07  | Rejected outcome      | Error feedback visible; previous state remains safe. |

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

No `acceptAgreementProposalApiTypes.ts` is required first pass because there is no request/response DTO. If generated OpenAPI operation aliases are later required by project convention, keep aliases near the feature, not in `shared/api`.

## 7. Expected Read State After Refetch

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

## 8. Action Availability

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

This is UX only.

Server must still enforce:

```text
Client role
ClientAccountId ownership
active proposal sender
exchange lifecycle
no-new-version rule
```

The server draft explicitly keeps ownership/lifecycle in domain/application rules and says accept does not create a new proposal version. 

## 9. Security / Protection

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

## 10. Questions / Decisions

### Blocked

| ID                           | Status  | Question                                                   | Current direction                        |
| ---------------------------- | ------- | ---------------------------------------------------------- | ---------------------------------------- |
| `Q-L2-AGR-ACCEPT-CLIENT-001` | blocked | Exact generated operation name?                            | Use OpenAPI after server implementation. |
| `Q-L2-AGR-ACCEPT-CLIENT-002` | blocked | Does generated OpenAPI expose this command as 204 no body? | Confirm after generation.                |

### Accepted

| ID                           | Status   | Question                             | Current direction                                                      |
| ---------------------------- | -------- | ------------------------------------ | ---------------------------------------------------------------------- |
| `Q-L2-AGR-ACCEPT-CLIENT-003` | accepted | Is this Client-only first pass?      | Yes. Employee accept is out of scope.                                  |
| `Q-L2-AGR-ACCEPT-CLIENT-004` | accepted | Does command send body?              | No body.                                                               |
| `Q-L2-AGR-ACCEPT-CLIENT-005` | accepted | Does success return DTO?             | No, `204 No Content`.                                                  |
| `Q-L2-AGR-ACCEPT-CLIENT-006` | accepted | Does accept create proposal version? | No.                                                                    |
| `Q-L2-AGR-ACCEPT-CLIENT-007` | accepted | Which statuses after refetch?        | `AgreementExchangeStatus.Accepted`, `AgreementProposalState.Accepted`. |
| `Q-L2-AGR-ACCEPT-CLIENT-008` | accepted | Does client require AcceptedAt?      | No.                                                                    |
| `Q-L2-AGR-ACCEPT-CLIENT-009` | accepted | Where does wrapper live?             | `features/agreement-exchange/accept-proposal/api`.                     |
| `Q-L2-AGR-ACCEPT-CLIENT-010` | accepted | Placement?                           | Client agreement exchange details action area.                         |

### Future review

| ID                           | Status        | Question                          | Current direction                                                |
| ---------------------------- | ------------- | --------------------------------- | ---------------------------------------------------------------- |
| `Q-L2-AGR-ACCEPT-CLIENT-011` | future review | Should confirmation be mandatory? | Component should support confirmation; product can decide.       |
| `Q-L2-AGR-ACCEPT-CLIENT-012` | future review | Employee accept?                  | Out of first pass; only add with explicit domain/server support. |

## 11. Behavior Coverage

| Behavior                                              | How client sidecar covers it                                             |
| ----------------------------------------------------- | ------------------------------------------------------------------------ |
| Client can accept own active Employee proposal        | Accept button sends command from Client details page.                    |
| Client cannot accept another Client’s exchange        | Server rejects; UI shows error/safe state.                               |
| Client cannot accept when lifecycle does not allow it | Button disabled if details says so; server still rejects stale attempts. |
| Accept does not create new version                    | Client sends accept command only, then refetches.                        |
| Active proposal becomes Accepted                      | Refetch shows `AgreementProposalState.Accepted`.                         |
| Exchange becomes Accepted                             | Refetch shows `AgreementExchangeStatus.Accepted`.                        |
| ActiveProposalVersion unchanged                       | Client does not create version and relies on refetch.                    |
| AcceptedAt not displayed                              | No UI expectation first pass.                                            |
| Employee accept                                       | Out of scope.                                                            |
| Counter-proposal                                      | Out of scope.                                                            |
| Final refuse                                          | Out of scope.                                                            |

## 12. Verification Plan

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

## 13. Suggested File Placement

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

## 14. Implementation Checklist

```text
[ ] confirm SL-AGR-EXCH-005 backend endpoint exists
[ ] confirm generated route and 204 response
[ ] add feature-owned acceptAgreementProposal API wrapper
[ ] do not add request/response DTO alias unless generated/project convention requires it
[ ] add useAcceptAgreementProposalMutation
[ ] add AcceptAgreementProposalButton
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

## 15. Next Step

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
