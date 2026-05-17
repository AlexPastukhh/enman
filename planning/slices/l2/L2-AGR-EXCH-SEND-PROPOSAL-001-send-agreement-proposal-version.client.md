# L2-AGR-EXCH-SEND-PROPOSAL-001.client — Send Agreement Proposal Version

Status: client command sidecar draft / shared details action slot / blocked until `SL-AGR-EXCH-002` server endpoint and generated OpenAPI contract exist
Parent server slice: `SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version`
Host read sidecar: `L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages`
Actors: ClientAccount, Employee
Slice type: client command sidecar
Placement: shared Agreement Exchange details action area first pass

Server details draft says details read returns exchange status, request summary, active proposal, full proposal history, document references and current actor side, but does not send proposals/accept/refuse; UI may show buttons, while server commands remain authoritative. 

## 0. Key Decision

First pass uses **one shared command feature** from the shared details UI:

```text
features/agreement-exchange/send-proposal/*
```

Not two separate client/employee features.

Client and Employee have different page shells:

```text
/pages/agreements/:exchangeId
/pages/employee/agreements/:exchangeId
```

But both pages render the same details widget and may pass the same command feature into the details action slot:

```text
AgreementExchangeDetailsView
  renderActions={... SendAgreementProposalForm ...}
```

## 1. Scope

This sidecar owns:

```text
- send new Agreement Proposal version action from exchange details;
- one shared form/action for Client and Employee;
- role-aware UI copy based on currentActorSide/viewerRole;
- showing form only when details state says or implies proposal can be sent;
- proposal payload form fields from generated server contract;
- optional comment/notes if server contract supports it;
- document reference/attachment field only as defined by contract;
- POST command to send new proposal version;
- pending state while command is in flight;
- duplicate-submit protection;
- visible validation/error feedback;
- refresh agreement exchange details after success;
- refresh agreement exchange list if cache exists;
- no accept behavior;
- no final refuse behavior;
- no start exchange behavior.
```

This is the “continue negotiation / send next proposal version” action. It is not the initial exchange creation, unless server contract intentionally reuses the same command after an exchange already exists.

## 2. Out of Scope

```text
- backend endpoint implementation -> SL-AGR-EXCH-002;
- Agreement Exchange list read;
- Agreement Exchange details read;
- Start Agreement Exchange with initial Employee proposal;
- Accept active proposal;
- Final refuse exchange;
- document upload/storage implementation;
- file download/binary serving;
- actor-specific client/employee command wrappers while endpoint/body are common;
- local CSRF mechanics;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api.
```

## 3. Related Owners

```text
SL-AGR-EXCH-002
  Owns backend command to append/send the next proposal version.

L2-AGR-EXCH-SEND-PROPOSAL-001.client
  Owns shared Client/Employee proposal form/action/mutation.

L2-AGR-EXCH-DETAILS-001.client
  Owns shared details read page/widget and action slot.

Future accept proposal sidecar
  Owns accept active proposal action.

Future final refuse sidecar
  Owns final refusal action.

Document/file slices
  Own upload/download/storage if proposal document handling needs separate infrastructure.
```

## 4. Scenario Flow

```text
Client or Employee opens Agreement Exchange details
        ↓
Details page shows active proposal and currentActorSide
        ↓
UI determines whether current actor can send next proposal
        ↓
Actor fills proposal form
        ↓
Actor submits
        ↓
Command sends new proposal version
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ command accepted             │ command rejected             │
 ▼                              ▼
details/list refresh            error feedback visible
        ↓                       previous state remains safe
proposal history shows
new latest version
```

In ordinary words:

The user opens the shared exchange details page. If it is their turn or the server-provided state allows sending a new proposal, the page shows a shared proposal form. On success, details refetch and the new version appears in proposal history. On failure, the form stays safe and shows feedback.

## 5. Client Implementation Flow

```text
[Client Details Page Shell]
pages/agreements/details/ClientAgreementExchangeDetailsPage.tsx

Uses:
  AgreementExchangeDetailsView
  SendAgreementProposalForm in action slot

Owns:
  client route/page shell;
  client wording/back link;
  passing viewerRole="Client".
```

```text
[Employee Details Page Shell]
pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx

Uses:
  AgreementExchangeDetailsView
  SendAgreementProposalForm in action slot

Owns:
  employee route/page shell;
  employee wording/back link;
  passing viewerRole="Employee".
```

```text
[Shared Details Widget]
widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx

Owns:
  read layout;
  active proposal display;
  proposal history;
  action slot placement.

Does not own:
  send proposal mutation;
  accept/refuse commands;
  command endpoint wrapper.
```

```text
[Command Feature UI]
features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm.tsx

Owns:
  proposal form;
  submit button;
  pending/error state;
  role-aware copy;
  local shape validation that mirrors generated contract.
```

```text
[Command Feature Model]
features/agreement-exchange/send-proposal/model/useSendAgreementProposalMutation.ts

Owns:
  mutation;
  details/list invalidation after success;
  command error propagation.
```

```text
[Command Feature API]
features/agreement-exchange/send-proposal/api/sendAgreementProposal.ts
features/agreement-exchange/send-proposal/api/sendAgreementProposalApiTypes.ts

Owns:
  POST endpoint wrapper;
  generated request/response aliases near the feature.

Uses:
  shared/api/fetchJson
  shared/api/generated/openapi-types
```

## 6. API Contract Direction

Blocked until generated contract exists.

Target endpoint direction from `SL-AGR-EXCH-002`:

```http
POST /api/requests/{requestId}/agreement-exchange/proposals
```

Use the exact generated route from OpenAPI after server implementation. The details page may be routed by `exchangeId`, but the command wrapper should use `requestId` from the details DTO when the server route is request-scoped.

Target body direction:

```ts
type SendAgreementProposalRequest = {
  // exact fields come from generated OpenAPI
  document?: unknown;
  documentRef?: unknown;
  comment?: string | null;
};
```

Target response direction:

```text
Preferred:
  204 No Content + details refetch

Alternative:
  200/201 with proposal/exchange id if server needs it
```

Do not invent final field names. Use generated aliases:

```ts
// features/agreement-exchange/send-proposal/api/sendAgreementProposalApiTypes.ts
import type { components } from "../../../../shared/api/generated/openapi-types";

export type SendAgreementProposalRequest =
  components["schemas"]["<GeneratedRequestDtoName>"];
```

Wrapper direction:

```ts
// features/agreement-exchange/send-proposal/api/sendAgreementProposal.ts
import { fetchJson } from "../../../../shared/api/fetchJson";
import type { SendAgreementProposalRequest } from "./sendAgreementProposalApiTypes";

export const sendAgreementProposal = (
  requestId: number,
  payload: SendAgreementProposalRequest,
): Promise<void> =>
  fetchJson<void>(
    `/api/requests/${encodeURIComponent(String(requestId))}/agreement-exchange/proposals`,
    {
      method: "POST",
      body: JSON.stringify(payload),
    },
  );
```

Important:

```text
Do not add:
  shared/api/agreementExchangeApi.ts

Do not add first pass:
  sendClientAgreementProposal.ts
  sendEmployeeAgreementProposal.ts
```

One common command wrapper is enough while contract is shared.

## 7. Action Availability

Preferred source:

```text
details.actionAvailability.canSendProposal
```

If server details DTO does not include action availability first pass, UI may derive **display-only** availability from safe read fields:

```text
currentActorSide
exchangeStatus
activeProposal.sender
```

But this is UX only.

Server command must still re-check:

```text
auth/session
actor role
exchange visibility
whose turn / active proposal sender
exchange lifecycle
document/proposal validity
```

## 8. Protection / Validation

Client validation:

```text
- only validates form shape required by generated contract;
- string length / empty fields if contract says required;
- document ref field shape if contract says required;
- does not decide actor permissions or turn ownership.
```

Server protection:

```text
- Client can only act on own exchange;
- Employee can act under first-pass employee-visible policy;
- active proposal / turn rules are server-side;
- command checks lifecycle even if button was visible.
```

`viewerRole` affects labels and wording only. It is not authorization.

## 9. Questions / Decisions

| ID                              | Status   | Question                                                                    | Current direction                             |
| ------------------------------- | -------- | --------------------------------------------------------------------------- | --------------------------------------------- |
| `Q-L2-AGR-SEND-PROP-CLIENT-001` | blocked  | Exact endpoint path?                                                        | Use generated OpenAPI from `SL-AGR-EXCH-002`. |
| `Q-L2-AGR-SEND-PROP-CLIENT-002` | blocked  | Exact request DTO fields?                                                   | Do not invent; use generated DTO aliases.     |
| `Q-L2-AGR-SEND-PROP-CLIENT-003` | blocked  | Is document upload already done before submit, or included in this command? | Follow server/document slice contract.        |
| `Q-L2-AGR-SEND-PROP-CLIENT-004` | blocked  | Success response 204 or DTO?                                                | Prefer 204 + refetch, confirm via OpenAPI.    |
| `Q-L2-AGR-SEND-PROP-CLIENT-005` | accepted | One feature for Client and Employee?                                        | Yes, shared feature from shared details.      |
| `Q-L2-AGR-SEND-PROP-CLIENT-006` | accepted | Separate actor-specific wrappers?                                           | No while endpoint/body are common.            |
| `Q-L2-AGR-SEND-PROP-CLIENT-007` | accepted | Does client submit actor id?                                                | No. Server resolves actor from session.       |
| `Q-L2-AGR-SEND-PROP-CLIENT-008` | accepted | Does details read execute command?                                          | No. Details only provides action slot.        |

## 10. Behavior Coverage

| Behavior                                     | How client sidecar covers it                          |
| -------------------------------------------- | ----------------------------------------------------- |
| Client can send next proposal when allowed   | Shared form submits command from client details page. |
| Employee can send next proposal when allowed | Same form submits command from employee details page. |
| New version appears in history               | Details query refetch after success.                  |
| Actor id is not spoofed                      | Client sends no actor id.                             |
| Invalid/stale action is rejected             | Server rejection shown as command error.              |
| Accept active proposal                       | Out of scope.                                         |
| Final refuse                                 | Out of scope.                                         |
| Start exchange                               | Out of scope.                                         |

## 11. Verification Plan

Component tests:

```text
- form renders in details action slot when allowed;
- form can render client copy;
- form can render employee copy;
- submit calls mutation with requestId and payload;
- pending disables submit;
- error feedback visible;
- success callback/invalidation triggered;
- no accept/refuse controls rendered.
```

API/model tests:

```text
- sendAgreementProposal posts to generated request-scoped endpoint;
- wrapper imports fetchJson from shared API infrastructure;
- wrapper uses generated DTO alias near feature;
- mutation invalidates exchange details query;
- mutation invalidates agreement exchange list query if present;
- no client/employee-specific wrapper exists.
```

E2E planned:

```text
Client session:
  open /agreements/:exchangeId
  send proposal version
  see new version in proposal history after refresh

Employee session:
  open /employee/agreements/:exchangeId
  send proposal version
  see new version in proposal history after refresh
```

Non-goals:

```text
- no accept/refuse command tests here;
- no file upload/storage tests here unless contract includes it;
- no server lifecycle internals in client tests;
- no React Query internal assertions in E2E.
```

## 12. Suggested File Placement

```text
src/features/agreement-exchange/send-proposal/api/
  sendAgreementProposal.ts
  sendAgreementProposalApiTypes.ts

src/features/agreement-exchange/send-proposal/model/
  useSendAgreementProposalMutation.ts

src/features/agreement-exchange/send-proposal/ui/
  SendAgreementProposalForm.tsx
  sendAgreementProposalFormConst.ts
  sendAgreementProposalForm.css

src/pages/agreements/details/
  ClientAgreementExchangeDetailsPage.tsx

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

Do **not** add:

```text
src/shared/api/agreementExchangeApi.ts
src/features/agreement-exchange/send-proposal/api/sendClientAgreementProposal.ts
src/features/agreement-exchange/send-proposal/api/sendEmployeeAgreementProposal.ts
```

## 13. Implementation Checklist

```text
[ ] confirm SL-AGR-EXCH-002 backend endpoint exists
[ ] confirm generated endpoint path and DTO names
[ ] confirm command uses requestId route or generated route
[ ] add feature-owned API wrapper
[ ] add generated request DTO alias near feature
[ ] add useSendAgreementProposalMutation
[ ] add SendAgreementProposalForm
[ ] ensure Client and Employee shells use same feature
[ ] wire form into shared details action slot
[ ] do not add actor-specific wrappers first pass
[ ] do not send clientId or employeeId
[ ] refresh agreement exchange details after success
[ ] refresh agreement exchange list if present
[ ] show pending and error feedback
[ ] add component/API/model tests
[ ] add E2E after backend/test setup exists
```

## 14. Next Step

```text
1. Confirm/apply SL-AGR-EXCH-002 backend endpoint.
2. Run OpenAPI/type generation.
3. Confirm endpoint path, request DTO and success response.
4. Implement feature-owned API wrapper.
5. Implement mutation.
6. Implement SendAgreementProposalForm.
7. Wire form into shared details action slot for Client and Employee shells.
8. Add component/API/model tests.
9. Add E2E once backend/test setup is ready.
```
