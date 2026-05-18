# L2-EMP-DETAILS-001.client — Employee Request Details

Status: implemented client-sidecar draft refactor / implementation evidence inspected read-only  
Parent server slice: `SL-EMP-REQ-002 — Employee Request Details Read`  
Scenario: `SC-07A — Employee Request Details`  
Slice type: L2 client read sidecar  
Actor: Employee  
Placement: Employee request details route/page  
Architecture direction: read/details slice maps to `pages + entities`; command actions stay in feature sidecars.

Implementation evidence note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was inspected read-only from the uploaded repository snapshot to avoid stale page/API/test names.

Runtime code, tests, generated artifacts and runtime UI are not changed by this archive.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-07A — Employee Request Details
```

Related scenarios:

```text
SC-06 — Employee Request Dashboard
SC-07B — Employee Request Review Actions
SC-13D — Employee Agreement Proposal Create / Send Version, for Start Agreement Exchange action after request approval
```

Server source:

```text
SL-EMP-REQ-002 — Employee Request Details Read
```

Cross-cutting behavior:

```text
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility
CC-SEC-CSRF-001 — applies to hosted unsafe command features only, not the read query
```

Data source:

```text
EmployeeRequestDetailsDto generated from `GET /api/employee/requests/{requestId}`
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior names and current test names until source-sync files are completed.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-07A: pending / v000 if source registry is applied
SC-06: pending / v000 if source registry is applied
SC-07B: pending / v000 if source registry is applied
SL-EMP-REQ-002: paired server draft in same archive
```

Domain/server baseline:

```text
server read endpoint returns request status/type/details, applicant summary/contact, object address, createdAt and reviewState;
server owns Employee auth and current Employee-relative review-state derivation;
client renders returned state and does not enforce command security.
```

Slice derivation map:

```text
pending / add row for L2-EMP-DETAILS-001.client during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Server/domain disposition | This client slice responsibility | Current implementation evidence | Notes |
|---|---|---|---|---|---|
| Employee opens request details page | SC-07A pending | server details endpoint exists | render `/employee/requests/:requestId` page | `EmployeeRequestDetailsPage.tsx` | route id parsed and validated |
| Non-Employee cannot view details page safely | SC-07A pending | server returns 401/403 | show sign-in/access state | `EmployeeRequestDetailsPage.test.tsx` | UI is not security boundary |
| Missing/invalid request shows safe state | SC-07A pending | server returns 404 | show not-found empty state | page tests | invalid route id also not-found |
| Request details are displayed | SC-07A pending | server DTO fields | render status/type/details/address/createdAt | `EmployeeRequestDetailsView.tsx` / test | read-only display |
| Applicant/client review data displayed | SC-07A pending | applicant summary DTO | render display name/email/phone | `EmployeeApplicantReviewData` / details view test | no edit behavior |
| Review state displayed | SC-07A/07B pending | server-derived reviewState | render state panel | `EmployeeReviewStatePanel`, details view test | NotStarted/Started/Approved/Rejected |
| Action availability displayed | SC-07A/07B pending | client derives from read state for current implementation | render action availability panel and action slots | `EmployeeReviewActionAvailabilityPanel`, page tests | future server actionAvailability may replace derivation |
| StartReview action hosted | SC-07B pending | command sidecar owns mutation | page hosts `StartReviewButton` with disabled state | page implementation/test | command not owned by read sidecar |
| Approve/Reject actions hosted | SC-07B pending | command sidecars own mutations | page hosts approve/reject features | page implementation/test | read sidecar composes only |
| Start Agreement Exchange hosted after approval | SC-13D pending | agreement start sidecar owns mutation | page hosts `StartAgreementExchangeForm` when approved | page implementation/test | not part of server details slice |
| Details query wrapper lives in entity API | SC-07A pending | generated contract exists | use `entities/employee-request/api/getEmployeeRequestDetails.ts` | API wrapper/test | no business wrapper in `shared/api` |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-current-doc-sync
```

Implemented files inspected read-only:

```text
client:
  energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.tsx
  energymanagement.client/src/pages/employee/requests/details/employeeRequestDetailsPage.css
  energymanagement.client/src/entities/employee-request/api/getEmployeeRequestDetails.ts
  energymanagement.client/src/entities/employee-request/api/employeeRequestApiTypes.ts
  energymanagement.client/src/entities/employee-request/model/useEmployeeRequestDetailsQuery.ts
  energymanagement.client/src/entities/employee-request/model/employeeRequestQueryKeys.ts
  energymanagement.client/src/entities/employee-request/model/employeeRequestTypes.ts
  energymanagement.client/src/entities/employee-request/model/reviewActionAvailability.ts
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDetailsView.tsx
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDetailsEmptyState.tsx
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestStatusPanel.tsx
  energymanagement.client/src/entities/employee-request/ui/EmployeeApplicantReviewData.tsx
  energymanagement.client/src/entities/employee-request/ui/EmployeeReviewStatePanel.tsx
  energymanagement.client/src/entities/employee-request/ui/EmployeeReviewActionAvailabilityPanel.tsx

server:
  paired server draft refactored in this archive:
    planning/slices/SL-EMP-REQ-002-employee-request-details-read.md

tests:
  energymanagement.client/src/entities/employee-request/api/getEmployeeRequestDetails.test.ts
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDetailsView.test.tsx
  energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
```

Checked against:

```text
source versions:
  pending source-sync registry

server contract:
  generated `EmployeeRequestDetailsDto` exists in current client generated OpenAPI types

slice derivation map version:
  pending

current implementation snapshot:
  uploaded repo zip inspected read-only
```

Known drift corrected by this refactor:

```text
- old client draft said server contract was pending, but current code has endpoint/generated DTO/API wrapper;
- old client draft described future action slots, but current page already hosts Start/Approve/Reject and Start Agreement Exchange features;
- old client draft lacked Source / Domain / Slice Coverage Snapshot;
- old client draft lacked Implementation Sync Status;
- old verification plan lacked current Behavior-to-Test Trace with actual test names.
```

Known remaining drift / follow-up:

```text
source:
  - stable behavior item IDs and final UI scenario source are not assigned.

client:
  - current page derives action availability client-side from read state;
  - future server-provided actionAvailability could replace or supplement derivation.

implementation:
  - tests were inspected but not executed in this docs-only pass.
```

Last sync note:

```text
Docs-only refactor with read-only implementation evidence. No runtime implementation inspection beyond file reading, no runtime changes, and no page-flow/redirect audit.
```

---

## 1. Scope

This client sidecar owns:

```text
- Employee request details route/page;
- route param parsing for requestId;
- Employee session branch;
- loading / sign-in / access denied / not-found / error / success states;
- entity-owned details query and API wrapper;
- generated DTO aliases near employee-request entity;
- request status/type/details/address display;
- applicant/client review data display;
- review state display;
- action availability display;
- action-slot composition for StartReview / ApproveReview / RejectReview / StartAgreementExchange feature sidecars;
- navigation back to Employee dashboard;
- no command implementation inside this read sidecar.
```

Current route:

```text
/employee/requests/:requestId
```

Current endpoint consumed:

```http
GET /api/employee/requests/{requestId}
```

---

## 2. Out of Scope

```text
- Employee request details server endpoint implementation -> SL-EMP-REQ-002;
- Employee request dashboard/list implementation -> L2-EMP-DASH-001.client;
- Start Review mutation -> L2-REVIEW-START-001.client / feature sidecar;
- Approve Review mutation -> approve feature sidecar;
- Reject Review mutation / feedback form -> reject feature sidecar;
- Start Agreement Exchange mutation -> L2-AGR-EXCH-START-001.client / feature sidecar;
- proposal/agreement exchange details after approval -> agreement exchange slices;
- server auth/visibility/security enforcement;
- local CSRF mechanics except through hosted command features;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api;
- runtime UI redesign or page-flow/redirect audit.
```

Important:

```text
This page composes command feature components, but command behavior is not owned by this read sidecar.
```

---

## 3. Related Slices / Owners

```text
SL-EMP-REQ-001 — Employee Request List Read
  Owns employee dashboard/list read endpoint and compact row DTO.

L2-EMP-DASH-001.client
  Owns Employee request dashboard/list page and navigation to details.

SL-EMP-REQ-002 — Employee Request Details Read
  Owns employee request details endpoint and details DTO.

L2-EMP-DETAILS-001.client
  Owns Employee request details route/page, read UI and action-slot composition.

SL-EMP-REQ-003 / L2-REVIEW-START-001.client
  Own StartReview command and button/mutation behavior.

SL-EMP-REQ-004 / approve client feature
  Own ApproveReview command behavior.

SL-EMP-REQ-005 / reject client feature
  Own RejectReview command and feedback form behavior.

SL-AGR-EXCH-001 / L2-AGR-EXCH-START-001.client
  Own Start Agreement Exchange after an approved request.

shared/api
  Owns generic transport, ProblemDetails/ApiError, CSRF helpers and generated types only.
```

---

## 4. Visual UI / Scenario Flow

```text
[Signed-in Employee]
opens Employee request details route
        ↓
[Details Page]
parses requestId and checks Employee session
        ↓
[Entity Query]
loads EmployeeRequestDetailsDto
        ↓
[Details View]
shows request status/type/details/address
        ↓
[Applicant Area]
shows applicant display/contact data
        ↓
[Review State Area]
shows NotStarted / StartedByCurrentEmployee / StartedByAnotherEmployee / Approved / Rejected
        ↓
[Action Area]
shows hosted feature actions according to read state
        ↓
Employee can return to dashboard
```

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Employee | Opens `/employee/requests/:requestId`. |
| S02 | Page shell | Parses request id and checks Employee session. |
| S03 | Entity query | Loads details through entity-owned API wrapper. |
| S04 | Details view | Renders request and applicant data. |
| S05 | Review state panel | Renders current review state. |
| S06 | Action availability panel | Renders available/blocked action messaging. |
| S07 | Hosted action features | Render Start/Approve/Reject/StartAgreementExchange controls. |
| S08 | Error states | Render sign-in/access/not-found/error safely. |

---

## 5. Visual Client Implementation Flow

### Page Layer

```text
EmployeeRequestDetailsPage
  from: pages/employee/requests/details/EmployeeRequestDetailsPage.tsx
  needed to: compose Employee request details route, state branches and hosted action slots.
  visual: page-level layout with back link, heading, state messages and details content.
```

Uses:

```text
useParams
  from: react-router-dom
  needed to: read `requestId` route param.

useNavigate
  from: react-router-dom
  needed to: navigate to Employee agreement exchange details after StartAgreementExchange returns exchangeId.

useSession
  from: entities/session/model/useSession
  needed to: render Employee/non-Employee/signed-out branches.

useEmployeeRequestDetailsQuery
  from: entities/employee-request/model/useEmployeeRequestDetailsQuery.ts
  needed to: load Employee request details.

EmployeeRequestDetailsView
  from: entities/employee-request/ui/EmployeeRequestDetailsView.tsx
  needed to: render read-only request details and action slot.
  visual: central details card/content block.

EmployeeRequestDetailsEmptyState
  from: entities/employee-request/ui/EmployeeRequestDetailsEmptyState.tsx
  needed to: show not-found/invalid id state.
  visual: empty state panel.

StartReviewButton / ApproveReviewButton / RejectReviewForm
  from: features/employee-request/*
  needed to: compose review command actions in action slot.
  visual: command controls inside details action area.

StartAgreementExchangeForm
  from: features/agreement-exchange/start-exchange/ui/StartAgreementExchangeForm.tsx
  needed to: compose start agreement exchange action after request approval.
  visual: command form/control inside details action area.
```

### Entity API Layer

```text
getEmployeeRequestDetails
  from: entities/employee-request/api/getEmployeeRequestDetails.ts
  needed to: wrap `GET /api/employee/requests/{requestId}` and keep business endpoint wrappers out of `shared/api`.
```

Uses:

```text
fetchJson
  from: shared/api/fetchJson.ts
  needed to: execute generic GET request and parse response/errors.

EmployeeRequestDetailsDto
  from: entities/employee-request/api/employeeRequestApiTypes.ts
  needed to: expose generated OpenAPI DTO alias near the entity.
```

### Entity Model Layer

```text
useEmployeeRequestDetailsQuery
  from: entities/employee-request/model/useEmployeeRequestDetailsQuery.ts
  needed to: own React Query loading/error/success state for details.

employeeRequestQueryKeys.details(requestId)
  from: entities/employee-request/model/employeeRequestQueryKeys.ts
  needed to: centralize details cache identity and command invalidation target.

getEmployeeReviewActionAvailability
  from: entities/employee-request/model/reviewActionAvailability.ts
  needed to: derive current client-side action availability from read state until server actionAvailability exists.
```

### Entity UI Layer

```text
EmployeeRequestDetailsView
  from: entities/employee-request/ui/EmployeeRequestDetailsView.tsx
  needed to: render details sections and optional action slot.
  visual: article/card-style details block.

EmployeeRequestStatusPanel
  from: entities/employee-request/ui/EmployeeRequestStatusPanel.tsx
  needed to: show request status/type/details/address/createdAt.
  visual: status/details panel.

EmployeeApplicantReviewData
  from: entities/employee-request/ui/EmployeeApplicantReviewData.tsx
  needed to: show applicant display/contact data.
  visual: applicant information panel.

EmployeeReviewStatePanel
  from: entities/employee-request/ui/EmployeeReviewStatePanel.tsx
  needed to: show review state label.
  visual: review state panel.

EmployeeReviewActionAvailabilityPanel
  from: entities/employee-request/ui/EmployeeReviewActionAvailabilityPanel.tsx
  needed to: show available/blocked review action messaging and render action slot.
  visual: action area/panel under review state.
```

---

## 6. Client API / Server Contract

Endpoint:

```http
GET /api/employee/requests/{requestId}
```

Current wrapper:

```ts
export const getEmployeeRequestDetails = (
  requestId: number,
): Promise<EmployeeRequestDetailsDto> =>
  fetchJson<EmployeeRequestDetailsDto>(
    `/api/employee/requests/${encodeURIComponent(String(requestId))}`,
    { method: "GET" },
  );
```

Current generated aliases:

```ts
export type EmployeeRequestDetailsDto =
  components["schemas"]["EmployeeRequestDetailsDto"];

export type EmployeeRequestApplicantSummaryDto =
  components["schemas"]["EmployeeRequestApplicantSummaryDto"];
```

Current response fields:

```text
requestId
requestType
status
applicant
objectAddress
details
createdAt
reviewState
```

Do not add:

```text
src/shared/api/employeeRequestApi.ts
```

---

## 7. Role / State UI Rules

Session branches:

```text
no session:
  show sign-in state

session but role != Employee:
  show access denied state

Employee + invalid route id:
  show not-found state

Employee + 404 from API:
  show not-found state

Employee + 401/403 from API:
  show access denied state

Employee + other error:
  show generic error state

Employee + data:
  render EmployeeRequestDetailsView
```

Current action slot behavior:

```text
NotStarted / InReview:
  StartReviewButton can be enabled.

StartedByCurrentEmployee:
  ApproveReviewButton and RejectReviewForm can be enabled.

StartedByAnotherEmployee:
  review actions are blocked/disabled.

Approved:
  StartAgreementExchangeForm can be enabled.

Rejected / AgreementExchangeFailed:
  review/start exchange actions are blocked unless future behavior changes.
```

Important:

```text
This availability is UX only.
Server command endpoints still enforce authorization and lifecycle.
```

---

## 8. Security / Protection

Client sends only:

```text
requestId in route for read endpoint
```

Client never sends for read:

```text
employeeId
reviewState as authority
action permission as authority
target status
```

Server owns:

```text
auth/session
Employee role
current Employee id
read visibility
command permissions
request/review lifecycle
```

Client owns:

```text
safe state rendering;
accessible visible state;
query enabled guard for invalid/missing request id;
ProblemDetails/ApiError mapping to page states.
```

---

## 9. Questions / Decisions

### Blocked / follow-up

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-EMP-DETAILS-CLIENT-001` | follow-up | Should actionAvailability be server-provided? | Current implementation derives client-side from read state; future server DTO can replace/supplement. |
| `Q-L2-EMP-DETAILS-CLIENT-002` | follow-up | Should Employee assignment/visibility narrow details reads? | Future visibility/queue slice; current first pass is broad Employee visibility. |
| `Q-L2-EMP-DETAILS-CLIENT-003` | follow-up | Should details show review timestamps/history? | Not first pass; future details refinement. |
| `Q-L2-EMP-DETAILS-CLIENT-004` | follow-up | Should approved details expose existing exchange id? | Agreement exchange/read-start follow-up; not owned by this read slice. |

### Accepted

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-EMP-DETAILS-CLIENT-005` | accepted | Is this read or command sidecar? | Read sidecar. Commands are feature sidecars. |
| `Q-L2-EMP-DETAILS-CLIENT-006` | accepted | Does this sidecar execute start/approve/reject? | No. It composes feature components only. |
| `Q-L2-EMP-DETAILS-CLIENT-007` | accepted | Where does details endpoint wrapper live? | `entities/employee-request/api/getEmployeeRequestDetails.ts`. |
| `Q-L2-EMP-DETAILS-CLIENT-008` | accepted | Can we add `shared/api/employeeRequestApi.ts`? | No. `shared/api` is generic infrastructure only. |
| `Q-L2-EMP-DETAILS-CLIENT-009` | accepted | Does details include agreement exchange details? | No. Agreement exchange slices own that. |
| `Q-L2-EMP-DETAILS-CLIENT-010` | accepted | Can StartReview response be used as details DTO? | No. Details refetch/query owns read state. |
| `Q-L2-EMP-DETAILS-CLIENT-011` | accepted | Does invalid route id call API? | No. Query disabled/not-found state. |
| `Q-L2-EMP-DETAILS-CLIENT-012` | accepted | Can StartAgreementExchange action be hosted here? | Yes as feature sidecar composition after Approved state; mutation remains out of read sidecar. |

---

## 10. Behavior Coverage

| Behavior | How client sidecar covers it | Current evidence |
|---|---|---|
| Employee opens request details page | route page renders and calls details query | `EmployeeRequestDetailsPage.test.tsx` |
| Details API wrapper calls correct endpoint | `getEmployeeRequestDetails` wraps `/api/employee/requests/{requestId}` | `getEmployeeRequestDetails.test.ts` |
| Signed-out state is safe | sign-in state rendered | page test |
| Non-Employee state is safe | access denied rendered | page test |
| Invalid/missing request state is safe | empty/not-found rendered | page tests |
| Request/applicant/details data visible | details view renders DTO fields | `EmployeeRequestDetailsView.test.tsx` |
| NotStarted review state visible | state panel and action availability text | details view/page tests |
| StartedByCurrentEmployee action availability visible | approve/reject enabled, start disabled | page test |
| StartedByAnotherEmployee blocked state visible | blocked state rendered | details view test |
| Approved request can host StartAgreementExchange | StartAgreementExchangeForm enabled | page test |
| Commands are not owned here | feature components are composed, mutations live in features | file placement/evidence |

---

## 11. Test / Verification Plan

Primary rule:

```text
Tests verify visible client behavior and scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Visible/client outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Employee details page loads for Employee session | page title/details/action state visible | page/component test | mocked session, mocked details query, route render | Medium: does not prove server access | Low if visible text/roles asserted | `loads and renders Employee request details for Employee session` |
| StartedByCurrentEmployee enables approve/reject | approve/reject visible and enabled, start disabled | page/component test | mocked details response | Medium: server still authoritative | Low | `renders Approve action when review is started by current Employee` |
| Approved request hosts StartAgreementExchange | Start Agreement Exchange action enabled | page/component test | mocked approved details response | Medium: command behavior not proven | Low/Medium | `renders Start Agreement Exchange action when request is approved` |
| Signed-out user sees sign-in state | sign-in prompt visible | page/component test | mocked no session | Low for UI branch | Low | `shows sign-in state without session` |
| Non-Employee sees access state | access denied visible | page/component test | mocked Client session | Low for UI branch | Low | `shows access state for non-Employee session` |
| Invalid route id shows not-found | empty/not-found state visible | page/component test | invalid route param | Low | Low | `shows not-found state for invalid route id` |
| API 404 shows not-found | empty/not-found state visible | page/component test | mocked ApiError 404 | Low | Low | `shows not-found state for 404 response` |
| Details view renders request/applicant data | request/applicant/review data visible | component test | render `EmployeeRequestDetailsView` with DTO | Low | Low | `renders employee request details and applicant review data` |
| StartedByAnotherEmployee blocked state visible | blocked marker/reason visible | component test | render details view with reviewState | Low | Low | `renders blocked action state when another Employee started review` |
| Optional action slot works | externally supplied action renders | component test | render details view with renderReviewActions | Low | Low/Medium | `renders optional future review action slot` |
| API wrapper calls endpoint | GET called with encoded request id | API test | stub fetch | Medium: wrapper only, not server | Medium if fetchJson internals change | `gets employee request details by request id`; `encodes the request id in the endpoint path` |

### Current test files

```text
energymanagement.client/src/entities/employee-request/api/getEmployeeRequestDetails.test.ts
energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDetailsView.test.tsx
energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
```

### E2E planned

```text
Employee session:
  open /employee/requests/:requestId
  assert status/details/applicant/review state visible
  assert expected action availability visible
```

### Non-goals

```text
- do not test StartReview mutation here;
- do not test ApproveReview mutation here;
- do not test RejectReview mutation here;
- do not test StartAgreementExchange mutation here;
- do not assert server DB internals from client tests;
- do not assert React Query internals beyond stable query hook behavior.
```

---

## 12. Styling / CSS Ownership

Current CSS ownership stays unchanged by this archive.

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Details page layout | page | `pages/employee/requests/details/employeeRequestDetailsPage.css` | page container, state blocks, spacing only |
| Details content | entity UI | `entities/employee-request/ui/employeeRequestDetails.css` | read-only details panels and action area layout |
| Command controls | feature | feature-owned CSS files | button/form/pending/error feedback for command actions |
| Shared shell | global/shared layout | `shared/ui/layout/*` / global styles | Header/Footer/app shell only |

Checklist:

```text
[ ] no broad global selector
[ ] no page CSS reaches into feature internals
[ ] no feature CSS changes page shell
[ ] loading/error/not-found/access states styled
[ ] action slot layout does not force command internals
```

No runtime CSS changes are included in this docs-only archive.

---

## 13. Accessibility / ARIA Contract

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|
| Page heading | `h1` | visible text `Employee Request Details` | n/a | `aria-labelledby` on section already used | `getByRole("heading", { name: "Employee Request Details" })` |
| Details article heading | `h2` | visible text `Request #id` | n/a | `aria-labelledby` on article already used | `getByRole("heading", { name: "Request #42" })` |
| Access/error state | `div` / `p` | visible text | n/a | `role="alert"` for access/error where applicable | `getByRole("alert")` where stable |
| Back link | `a` / `Link` | visible text | Enter activates | no | `getByRole("link", ...)` |
| Command actions | `button` / form controls | feature-owned labels | Space/Enter; form rules feature-owned | feature-owned | command feature tests |

---

## 14. Suggested File Placement

Current implementation already uses:

```text
src/pages/employee/requests/details/
  EmployeeRequestDetailsPage.tsx
  employeeRequestDetailsPage.css
  EmployeeRequestDetailsPage.test.tsx

src/entities/employee-request/api/
  getEmployeeRequestDetails.ts
  getEmployeeRequestDetails.test.ts
  employeeRequestApiTypes.ts

src/entities/employee-request/model/
  useEmployeeRequestDetailsQuery.ts
  employeeRequestQueryKeys.ts
  employeeRequestTypes.ts
  reviewActionAvailability.ts

src/entities/employee-request/ui/
  EmployeeRequestDetailsView.tsx
  EmployeeRequestDetailsView.test.tsx
  EmployeeRequestDetailsEmptyState.tsx
  EmployeeRequestStatusPanel.tsx
  EmployeeApplicantReviewData.tsx
  EmployeeReviewStatePanel.tsx
  EmployeeReviewActionAvailabilityPanel.tsx
  employeeRequestDetails.css
  employeeRequestDetailsConst.ts

src/features/employee-request/start-review/
  StartReview command feature, hosted but not owned here

src/features/employee-request/approve-review/
  Approve command feature, hosted but not owned here

src/features/employee-request/reject-review/
  Reject command feature, hosted but not owned here

src/features/agreement-exchange/start-exchange/
  Start Agreement Exchange feature, hosted but not owned here
```

Do not add:

```text
src/shared/api/employeeRequestApi.ts
```

---

## 15. Next Step

```text
1. Keep server/client details docs synced with current route and DTO names.
2. If future source registry is added, replace provisional behavior labels with stable IDs.
3. If server adds actionAvailability, update read DTO and simplify client derivation.
4. If employee assignment/visibility is introduced, update server projection and details tests.
5. Keep hosted command feature ownership separate from this read sidecar.
```

Separate later work:

```text
- StartReview client sidecar refactor;
- approve/reject client sidecar creation/location sync;
- StartAgreementExchange client sidecar refactor;
- page-flow/redirect audit, if explicitly requested.
```

---

## 16. Implementation Checklist / Current Refactor Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[x] page route exists: /employee/requests/:requestId
[x] entity API wrapper exists: getEmployeeRequestDetails
[x] query key exists: employeeRequestQueryKeys.details(requestId)
[x] query hook exists: useEmployeeRequestDetailsQuery
[x] generated DTO alias exists: EmployeeRequestDetailsDto
[x] details view exists
[x] details page handles Employee/non-Employee/signed-out/not-found/error states
[x] details page hosts StartReview / ApproveReview / RejectReview feature actions
[x] details page hosts StartAgreementExchange feature action for approved request
[x] API wrapper tests exist
[x] details view tests exist
[x] page tests exist
[ ] source registry behavior IDs assigned, future source-sync task
[ ] future server-provided actionAvailability decision resolved
[ ] page-flow/redirect audit, if requested separately
```

This archive does not modify runtime implementation.

---

## 17. Guardrail Summary

```text
This is a client read sidecar.
Use entity-owned read wrapper: entities/employee-request/api/getEmployeeRequestDetails.ts.
Do not add business endpoint wrappers to shared/api.
Render safe sign-in/access/not-found/error states.
Render request status/type/details/address/applicant/review state.
Compose command feature actions but do not own command mutations here.
Start/Approve/Reject are feature sidecars.
StartAgreementExchange is an agreement-exchange feature sidecar.
Client action availability is UX only.
Server command endpoints remain authoritative.
Do not perform runtime UI redesign in docs-only refactor archive.
```
