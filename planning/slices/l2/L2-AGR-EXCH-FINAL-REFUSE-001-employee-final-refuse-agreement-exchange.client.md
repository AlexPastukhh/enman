# L2-AGR-EXCH-FINAL-REFUSE-001.client — Employee Final Refuse Agreement Exchange

Status: implemented client-sidecar draft refactor / implementation spot-checked from uploaded repo snapshot / runtime UI not changed in this pass  
Parent server slice: `SL-AGR-EXCH-006 — Final Refuse Agreement Exchange`  
Host read sidecar: `L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages`  
Actor: Employee  
Slice type: client command sidecar  
Placement: Employee agreement exchange details action area only

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime client/server implementation was read only to align paths, route names, DTO names and tests.
No runtime UI/code/test/generated files are changed by this archive.
Deep UI redesign and page-flow/redirect audit are out of scope for this pass.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-13E — Agreement Final Refusal
```

Related scenario context:

```text
SC-13C — Employee Agreements
SC-13B — Client Agreement Proposal Details / Response
SC-13D — Employee Agreement Proposal Create / Send Version
```

UI scenario:

```text
missing / pending dedicated UI source for Employee final refusal action.
```

Server source:

```text
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Cross-cutting behavior:

```text
CC-SEC-CSRF-001 — Unsafe Command Protection
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility
CC-CLIENT-FORM-VALIDATION-001 — applies to optional reason field validation shape
```

Data source:

```text
pending scenario-data source for final refusal reason copy/length and post-command read states.
```

Behavior items:

```text
SC-13E local behavior item names are used until source registry / derivation map is completed:
- L2-AGR-FINAL-001
- L2-AGR-FINAL-002
- L2-AGR-FINAL-003
- L2-REQ-AGR-FAIL-001
- L2-AGR-BOUNDARY-001
- L2-AGR-FINAL-REASON-001
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-13E: pending / v000 if source registry is applied
SL-AGR-EXCH-006: paired server draft in same archive
```

Server/domain baseline:

```text
Uploaded repo snapshot confirms:
- route: POST /api/agreement-exchanges/{exchangeId}/final-refuse
- success: 204 No Content
- request DTO: FinalRefuseAgreementExchangeDto { reason?: string | null }
- reason optional / null allowed
- blank reason invalid
- max reason length = 2000
- server calls exchange.FinalRefuseProposal and request.MarkAgreementExchangeFailed
```

Slice derivation map:

```text
pending / add or update row for L2-AGR-EXCH-FINAL-REFUSE-001.client during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Server/domain disposition | This client slice responsibility | Notes |
|---|---|---|---|---|
| `L2-AGR-FINAL-001` — Employee can finally refuse active exchange | SC-13E pending | server endpoint/domain transition exists | render Employee final-refuse action from Employee agreement exchange details | UI availability is UX only |
| `L2-AGR-FINAL-002` — exchange becomes `FinallyRefused` | SC-13E pending | server persists exchange state | refetch details/list after success | no optimistic final status first pass |
| `L2-AGR-FINAL-003` — final refusal creates no proposal version | SC-13E pending | server/domain responsibility | client sends final-refuse command only; does not send document/proposal payload | no proposal form here |
| `L2-REQ-AGR-FAIL-001` — request becomes `AgreementExchangeFailed` | SC-13E pending | server application service mutates related request | invalidate related request details query only if requestId/query key exists | request read is separate owner |
| `L2-AGR-FINAL-REASON-001` — reason optional, blank invalid | SC-13E pending | server validates nullable reason | optional textarea; empty submits no payload; whitespace-only blocked client-side | server remains source of truth |
| Client final refusal is out of scope | SC-13E pending | endpoint requires Employee | do not render this action on Client details page | no Client final-refuse wiring |
| CSRF unsafe command protection | CC-SEC-CSRF-001 | shared fetch/CSRF infra handles unsafe request | feature API wrapper uses `fetchJson` | full CSRF matrix is cross-cutting |
| ProblemDetails/error feedback | CC-CLIENT-FEEDBACK-001 | server returns validation/problem details | form shows command/validation error feedback | no silent success on rejection |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync
```

Implemented files checked in uploaded repo snapshot:

```text
client:
  energymanagement.client/src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.ts
  energymanagement.client/src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchangeApiTypes.ts
  energymanagement.client/src/features/agreement-exchange/final-refuse/model/finalRefuseAgreementExchangeAvailability.ts
  energymanagement.client/src/features/agreement-exchange/final-refuse/model/useFinalRefuseAgreementExchangeMutation.ts
  energymanagement.client/src/features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.tsx
  energymanagement.client/src/features/agreement-exchange/final-refuse/ui/finalRefuseAgreementExchangeFormConst.ts
  energymanagement.client/src/features/agreement-exchange/final-refuse/ui/finalRefuseAgreementExchangeForm.css

client tests:
  energymanagement.client/src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.test.ts
  energymanagement.client/src/features/agreement-exchange/final-refuse/model/finalRefuseAgreementExchangeAvailability.test.ts
  energymanagement.client/src/features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.test.tsx

server:
  paired server draft refactored in this archive:
    planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md
```

Checked against:

```text
source versions:
  pending source-sync registry

server contract:
  route/DTO/success behavior checked from uploaded repo snapshot

runtime tests:
  not executed in this docs-only pass
```

Known drift corrected by this refactor:

```text
docs:
  - old client draft lacked Scenario Sources block;
  - old client draft lacked Source / Domain / Slice Coverage Snapshot;
  - old client draft lacked Implementation Sync Status;
  - old test plan lacked Behavior-to-Test Trace with escape/refactor risk;
  - old draft did not list concrete current client files/tests;
  - old draft did not distinguish current runtime evidence from docs-only archive scope.

implementation:
  - current runtime implementation appears ahead of the old client planning draft;
  - this archive does not change runtime implementation.

source:
  - stable source registry / derivation map row remains pending.
```

Last sync note:

```text
Docs-only refactor using uploaded repo code as evidence. No runtime UI/code/test changes and no test execution in this pass.
```

---

## 1. Scope

This sidecar owns:

```text
- Employee Final Refuse action from agreement exchange details;
- first-pass placement only on Employee agreement exchange details page/action slot;
- rendering Final Refuse only when details state says or implies Employee can final-refuse;
- disabled/blocked state when final refusal is unavailable;
- visible unavailable reason when available;
- optional final refusal reason field;
- client-side reason shape validation;
- optional confirmation before final negative decision;
- feature-owned API wrapper for POST /api/agreement-exchanges/{exchangeId}/final-refuse;
- no request body when reason is omitted/empty;
- optional `{ reason }` payload when valid reason is provided;
- pending state while command is in flight;
- duplicate-submit protection;
- visible validation/error feedback;
- refresh agreement exchange details after success;
- refresh agreement exchange list after success if cache exists;
- refresh related request details after success only if request id/query key exists;
- no Client final refusal;
- no accept behavior;
- no counter-proposal;
- no proposal version creation.
```

Current implementation evidence:

```text
features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.ts
  POSTs to /api/agreement-exchanges/{exchangeId}/final-refuse
  sends no body when payload is undefined
  sends JSON body when payload exists

features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.tsx
  trims valid reason
  blocks whitespace-only reason
  blocks reason > 2000
  supports optional confirmation
  passes requestId for optional request-details invalidation
```

---

## 2. Out of Scope

```text
- backend endpoint implementation -> SL-AGR-EXCH-006;
- Agreement Exchange list read -> SL-AGR-EXCH-003 / L2-AGR-EXCH-LIST-001.client;
- Agreement Exchange details read -> SL-AGR-EXCH-004 / L2-AGR-EXCH-DETAILS-001.client;
- initial exchange creation -> SL-AGR-EXCH-001 / L2-AGR-EXCH-START-001.client;
- counter-proposal version creation -> SL-AGR-EXCH-002 / L2-AGR-EXCH-SEND-PROPOSAL-001.client;
- Client accept active proposal -> SL-AGR-EXCH-005 / L2-AGR-EXCH-ACCEPT-001.client;
- Client-side final refusal;
- document upload/download;
- manually setting request/exchange statuses in client;
- local CSRF mechanics beyond using shared fetch infrastructure;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api;
- runtime UI refactor in this archive;
- page-flow / redirect audit in this archive.
```

Important guardrail:

```text
Client final refusal is out of scope.
Do not infer or implement Client final refusal from the shared details page.
```

---

## 3. Related Slices / Owners

```text
SL-AGR-EXCH-006
  Owns backend Employee final-refuse command.

L2-AGR-EXCH-FINAL-REFUSE-001.client
  Owns Employee final-refuse feature UI/model/API planning.

L2-AGR-EXCH-DETAILS-001.client
  Owns shared details page/widget and action slot.

L2-AGR-EXCH-LIST-001.client
  Owns shared agreement exchange list refresh target.

Employee request details query owner
  Owns request details refetch target when requestId/query key exists.

shared/api
  Owns generic transport, ProblemDetails/ApiError, CSRF helpers and generated types only.
```

---

## 4. Visual UI / Scenario Flow

```text
Employee opens agreement exchange details
        ↓
Details page shows active agreement exchange
        ↓
Employee sees Final Refuse action when available
        ↓
Employee may enter optional final refusal reason
        ↓
Employee clicks Final Refuse
        ↓
Optional confirmation appears if enabled
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
related request can show AgreementExchangeFailed
```

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Employee | Opens Employee agreement exchange details. |
| S02 | Details read state | Shows active exchange/proposal state. |
| S03 | Employee action area | Shows or disables Final Refuse based on read state. |
| S04 | Final-refuse form | Accepts optional reason and validates shape. |
| S05 | Optional confirmation | Confirms final negative decision if enabled. |
| S06 | Feature API/model | Sends command and tracks pending/error states. |
| S07 | Accepted outcome | Invalidates agreement exchange details/list and request details when available. |
| S08 | Rejected outcome | Shows validation/ProblemDetails feedback; does not fake success. |

---

## 5. Visual Client Implementation Flow

### Employee Details Page Shell

```text
EmployeeAgreementExchangeDetailsPage
  from: pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx
  needed to: host Employee agreement exchange details and pass final-refuse feature into the details action slot.
  visual: Employee details page shell with action area for Employee-only commands.
```

Uses:

```text
AgreementExchangeDetailsView
  from: widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
  needed to: render shared details content and action slot.
  visual: shared details layout containing status, request summary, active proposal and proposal history.

FinalRefuseAgreementExchangeForm
  from: features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.tsx
  needed to: render Employee final-refusal form/action.
  visual: action panel/form with optional reason textarea, submit button, confirmation and feedback.
```

### Command Feature UI

```text
FinalRefuseAgreementExchangeForm
  from: features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.tsx
  needed to: own final-refusal user input, validation, confirmation, pending/error UI and submit behavior.
  visual: compact command form in the Employee details action area.
```

Uses:

```text
useFinalRefuseAgreementExchangeMutation
  from: features/agreement-exchange/final-refuse/model/useFinalRefuseAgreementExchangeMutation.ts
  needed to: submit final-refuse command and invalidate related reads after success/error.

finalRefuseAgreementExchangeFormConst
  from: features/agreement-exchange/final-refuse/ui/finalRefuseAgreementExchangeFormConst.ts
  needed to: centralize labels/copy/reason length for the form.
```

### Command Feature Model

```text
useFinalRefuseAgreementExchangeMutation
  from: features/agreement-exchange/final-refuse/model/useFinalRefuseAgreementExchangeMutation.ts
  needed to: own React Query mutation and read invalidation.
```

Uses:

```text
finalRefuseAgreementExchange
  from: features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.ts
  needed to: execute the POST command.

agreementExchangeQueryKeys
  from: entities/agreement-exchange/model/agreementExchangeQueryKeys.ts
  needed to: invalidate details/list reads after command completion.

employeeRequestQueryKeys
  from: entities/employee-request/model/employeeRequestQueryKeys.ts
  needed to: invalidate related request details only when requestId is available.
```

### Command Feature API

```text
finalRefuseAgreementExchange
  from: features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.ts
  needed to: wrap POST /api/agreement-exchanges/{exchangeId}/final-refuse and keep business command wrapper out of shared/api.
```

Uses:

```text
fetchJson
  from: shared/api/fetchJson.ts
  needed to: execute generic HTTP request and apply shared CSRF/error behavior.

FinalRefuseAgreementExchangeRequest
  from: features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchangeApiTypes.ts
  needed to: type optional reason payload.
```

### Availability Helper

```text
getFinalRefuseAgreementExchangeAvailability
  from: features/agreement-exchange/final-refuse/model/finalRefuseAgreementExchangeAvailability.ts
  needed to: derive UI availability from details state for display only.
```

Important:

```text
Availability helper is UX only.
Server remains authority for Employee role, lifecycle and request failure transition.
```

---

## 6. Client API / Server Contract

Server endpoint:

```http
POST /api/agreement-exchanges/{exchangeId}/final-refuse
```

Request:

```ts
type FinalRefuseAgreementExchangeRequest = {
  reason?: string | null;
};
```

Success:

```http
204 No Content
```

Current feature wrapper shape:

```ts
export type FinalRefuseAgreementExchangeInput = {
  exchangeId: number;
  payload?: FinalRefuseAgreementExchangeRequest;
};

export const finalRefuseAgreementExchange = ({
  exchangeId,
  payload,
}: FinalRefuseAgreementExchangeInput): Promise<void> =>
  fetchJson<void>(
    `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}/final-refuse`,
    payload === undefined
      ? { method: "POST" }
      : { method: "POST", body: JSON.stringify(payload) },
  );
```

Do not add:

```text
src/shared/api/agreementExchangeApi.ts
```

No response DTO is required.

---

## 7. Reason Handling

Client behavior:

```text
empty field:
  submit payload: undefined

valid reason with surrounding spaces:
  trim and submit { reason: trimmedReason }

whitespace-only reason:
  show client validation error and do not submit

reason > 2000 after trim:
  show client validation error and do not submit
```

Current copy/constant source:

```text
features/agreement-exchange/final-refuse/ui/finalRefuseAgreementExchangeFormConst.ts
```

Current maximum:

```text
finalRefusalReasonMaxLength = 2000
```

Server remains source of truth even when client validates first.

---

## 8. Action Availability

Current availability helper direction:

```text
canFinalRefuse = true only when:
- currentActorSide == "Employee"
- exchangeStatus is AwaitingClientConfirmation or AwaitingEmployeeResponse
- exchangeStatus is not Accepted or FinallyRefused
```

Current unavailable reasons include:

```text
Agreement exchange is already completed.
Only an Employee can finally refuse an agreement exchange.
Agreement exchange is not in an active state that can be finally refused.
```

Important:

```text
This is display logic only.
It must not be treated as authorization.
```

---

## 9. Expected Read State After Refetch

After success, client refetches relevant reads and expects server state to show:

```text
exchangeStatus = FinallyRefused
related request status = AgreementExchangeFailed, if that request read is visible/refetched
proposal count unchanged
active proposal/version unchanged unless details read intentionally derives otherwise
```

Current mutation invalidates:

```text
agreementExchangeQueryKeys.details(exchangeId)
agreementExchangeQueryKeys.all
employeeRequestQueryKeys.details(requestId), only when requestId is provided
```

No optimistic final status update first pass.

---

## 10. Security / Protection

Client sends:

```text
exchangeId in route
optional reason payload only
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
- disables submit while pending;
- blocks duplicate submit;
- shows validation feedback before submit for client-known invalid reason shape;
- shows command/ProblemDetails feedback on server rejection;
- refetches reads after success/error according to mutation rules;
- does not pretend the exchange was refused before server success.
```

Server remains authoritative:

```text
UI button visibility is not authorization.
```

---

## 11. Questions / Decisions

### Blocked / pending source-sync

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-001` | blocked | Dedicated UI scenario source? | Missing/pending; current draft uses SC-13E + runtime UI evidence. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-002` | blocked | Final generated OpenAPI alias source? | Current local type is hand-written; generated alias can replace it if project convention changes. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-003` | blocked | Should request details always refetch? | Only when requestId/query key exists. |

### Accepted

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-004` | accepted | Is this Employee-only first pass? | Yes. Client final refusal is out of scope. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-005` | accepted | Is reason required? | No. Empty reason submits no payload. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-006` | accepted | Does success return DTO? | No, `204 No Content`. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-007` | accepted | Does final refusal create proposal version? | No. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-008` | accepted | Which states after refetch? | `AgreementExchangeStatus.FinallyRefused`, `RequestStatus.AgreementExchangeFailed`. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-009` | accepted | Where does command wrapper live? | `features/agreement-exchange/final-refuse/api`. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-010` | accepted | Placement? | Employee agreement exchange details action area. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-011` | accepted | Should wrapper live in shared/api? | No. Keep business command wrapper in feature API. |
| `Q-L2-AGR-FINAL-REFUSE-CLIENT-012` | accepted | Is confirmation mandatory? | Feature supports optional confirmation; product/page can decide. |

---

## 12. Behavior Coverage

| Behavior | Status | How client sidecar covers it |
|---|---|---|
| Employee can finally refuse active exchange | covered | Employee details action uses final-refuse feature form/mutation. |
| Client cannot final-refuse | covered by UI scope | no Client final-refuse wiring; server still authoritative. |
| Missing/empty reason allowed | covered | empty field submits `payload: undefined`. |
| Valid reason submitted trimmed | covered | form trims and sends `{ reason }`. |
| Whitespace-only reason rejected client-side | covered | form shows validation error and does not submit. |
| Too-long reason rejected client-side | covered | max 2000 check. |
| Command sends no target statuses | covered | wrapper only sends optional reason payload. |
| Exchange state after success comes from refetch | covered | mutation invalidates exchange details/list. |
| Related request state after success comes from refetch | supported | mutation invalidates request details when requestId is available. |
| No proposal version creation | supported | no proposal/document payload; server owns behavior. |
| Accept/counter-proposal | out of scope | separate sidecars. |

---

## 13. Test / Verification Plan

Primary rule:

```text
Tests verify visible client behavior and scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Visible/client outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned / actual test |
|---|---|---|---|---|---|---|
| Wrapper posts final-refuse without body when reason omitted | command sends POST with no body and resolves void on 204 | API wrapper test | mocked fetch + CSRF token | Medium: does not prove server domain behavior | Medium: fetch wrapper shape changes may require test update | `posts Employee final-refuse command without a request body when reason is omitted` |
| Wrapper posts final-refuse with reason | command sends `{ reason }` payload | API wrapper test | mocked fetch + JSON body assertion | Medium | Medium | `posts Employee final-refuse command with optional reason payload` |
| Employee active exchange availability | action is available for Employee in active status | model/unit test | details fixture + availability helper | Medium: display-only proof | Low | `allows Employee to finally refuse an active exchange` |
| Client details blocked | Client does not get final-refuse availability | model/unit test | details fixture | Medium | Low | `blocks Client-side details` |
| Completed exchange blocked | unavailable reason for completed status | model/unit test | details fixture | Medium | Low | `blocks completed exchange` |
| Empty reason allowed | form submits with payload undefined | component test | render form, click submit, mutation spy | Medium: client-only, server behavior covered by integration | Low/Medium | `submits final refusal without payload when reason is empty` |
| Valid reason trimmed | form submits trimmed reason | component test | user typing + mutation spy | Medium | Low/Medium | `submits final refusal with trimmed reason` |
| Whitespace reason rejected | visible validation error and no mutation | component test | user typing + role alert | Low for client validation behavior | Low | `shows validation feedback for whitespace-only reason` |
| Confirmation supported | confirmation step appears and can submit | component test | `requireConfirmation` prop + user click | Low for UI behavior | Medium if confirmation UX changes | `shows confirmation step when confirmation is required` |
| Disabled unavailable state | unavailable reason visible and no submit | component test | disabled prop + mutation spy | Low | Low | `shows unavailable reason and does not submit when disabled` |
| Pending/error feedback | button disabled and error visible | component test | mocked mutation state | Low | Medium if copy changes | `shows pending state and command error feedback` |
| Integrated final refusal | browser flow shows FinallyRefused / request failed after refetch | E2E planned | real API + seeded state | Low when added | Medium | future E2E |

### Component tests

```text
- form renders optional reason field;
- empty reason submits no payload;
- valid reason is trimmed;
- whitespace-only reason shows validation error;
- too-long reason shows validation error;
- optional confirmation can be confirmed/cancelled;
- disabled/unavailable state prevents submit;
- pending state disables form;
- command error is shown.
```

### API/model tests

```text
- wrapper posts to /api/agreement-exchanges/{exchangeId}/final-refuse;
- wrapper sends no body when payload is undefined;
- wrapper sends JSON body when reason payload exists;
- wrapper uses fetchJson/shared API infrastructure;
- mutation invalidates exchange details/list;
- mutation invalidates request details only when requestId exists;
- availability helper allows Employee active statuses and blocks Client/completed statuses.
```

### E2E planned

```text
Employee session:
  open Employee agreement exchange details
  exchange is active
  submit final refusal with optional reason
  assert after refetch that exchange shows FinallyRefused
  assert related request shows AgreementExchangeFailed if visible on page
```

### Non-goals

```text
- no Client final-refuse E2E here;
- no accept/counter-proposal tests here;
- no server DB internals in client tests;
- no React Query internal cache-shape assertions in E2E;
- no page-flow/redirect audit in this draft refactor.
```

---

## 14. Styling / CSS Ownership

Current feature CSS owner:

```text
features/agreement-exchange/final-refuse/ui/finalRefuseAgreementExchangeForm.css
```

Ownership rule:

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Final refusal form | feature | `finalRefuseAgreementExchangeForm.css` | form layout, textarea, buttons, feedback within feature only |
| Details action slot | widget/page | details widget/page css | placement of action area only |
| Page shell | page | Employee details page css | page spacing/header/back link only |
| Tokens/base | global | global styles | tokens/reset/base only |

Checklist:

```text
[ ] no global business selector
[ ] no page CSS reaches into feature internals
[ ] no feature CSS changes app shell
[ ] no hover layout shift
[ ] loading/pending/error states styled
[ ] form remains usable with keyboard
```

No runtime CSS changes are included in this archive.

---

## 15. Accessibility / ARIA Contract

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|
| Reason field | `textarea` | label “Final refusal reason” | Tab focus, text input | no extra ARIA required if label connected | `getByLabelText("Final refusal reason")` |
| Submit | `button type="submit"` | button text “Final refuse” / pending text | Enter/Space activates | disabled state native | `getByRole("button", { name: "Final refuse" })` |
| Confirmation group | `div role="group"` | `aria-label="Final refusal confirmation"` | buttons keyboard accessible | role/label used | `getByRole("group", { name: ... })` |
| Validation/command error | `p role="alert"` | text content | announced by screen reader | role alert | `getByRole("alert")` |

---

## 16. Suggested File Placement

Current implementation already uses:

```text
src/features/agreement-exchange/final-refuse/api/
  finalRefuseAgreementExchange.ts
  finalRefuseAgreementExchangeApiTypes.ts

src/features/agreement-exchange/final-refuse/model/
  finalRefuseAgreementExchangeAvailability.ts
  useFinalRefuseAgreementExchangeMutation.ts

src/features/agreement-exchange/final-refuse/ui/
  FinalRefuseAgreementExchangeForm.tsx
  finalRefuseAgreementExchangeFormConst.ts
  finalRefuseAgreementExchangeForm.css
```

Related existing tests:

```text
src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.test.ts
src/features/agreement-exchange/final-refuse/model/finalRefuseAgreementExchangeAvailability.test.ts
src/features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.test.tsx
```

Do not add:

```text
src/shared/api/agreementExchangeApi.ts
Client final-refuse wiring under client agreement details page
```

---

## 17. Implementation Checklist / Current Sync Checklist

```text
[x] feature-owned API wrapper exists
[x] wrapper posts to /api/agreement-exchanges/{exchangeId}/final-refuse
[x] wrapper sends no body when payload is undefined
[x] wrapper sends JSON body when payload exists
[x] request type has reason?: string | null
[x] availability helper blocks Client side
[x] availability helper blocks completed exchange
[x] form allows empty reason
[x] form trims valid reason
[x] form rejects whitespace-only reason
[x] form rejects reason > 2000
[x] form supports optional confirmation
[x] form shows pending/error feedback
[x] mutation invalidates exchange details/list
[x] mutation invalidates request details when requestId exists
[x] API/model/component tests exist
[ ] tests were not executed in this docs-only pass
[ ] dedicated UI scenario source is still pending
[ ] source registry / derivation map row still pending
```

---

## 18. Guardrail Summary

```text
This is Employee-only final refusal UI/feature planning.
Do not add Client final refusal.
Do not implement accept here.
Do not implement counter-proposal here.
Do not send employeeId/requestId as authority.
Do not send target statuses from client.
Do not optimistically mark FinallyRefused before server success.
Reason is optional.
Whitespace-only reason is invalid.
Max reason length is 2000.
Wrapper lives in feature API, not shared/api.
Server remains authority for Employee role, lifecycle and request transition.
UI availability is not authorization.
Runtime UI/code is not changed by this archive.
```
