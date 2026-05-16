# Slice Questions Register

Status: active / ApplicantParty flat-list read model, validation and My Requests filters synchronized

## 1. Questions / Decisions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `SL-APPL-Q-002` | `SL-APPL-001` / `SL-APPL-003` | default/current | future review | What happens to default/current on create? | First-of-type may initialize default/current; additional same-type create does not switch silently; explicit same-page default/current action remains future. | ApplicantParty page read model, future default/current command, request creation prefill. |
| `SL-APPL-Q-006` | `SC-10` / `SC-10B` / `SL-APPL-001` / `SL-APPL-001.client` / `SL-APPL-002` / `SL-APPL-003` | page model | accepted direction | Is ApplicantParty management split into Account page section and separate My Applicant Parties page? | No. Use one Applicant Parties page / section; SC-10B is same-page future management addendum, not a separate current user page. | Scenario wording, UI sidecar placement, backend read slice scope, future same-page actions. |
| `SL-APPL-Q-007` | `SC-10` / `SC-10B` / `slice-extension-points-register` | lifecycle | future review | Should delete/archive/hide be current L1 behavior? | No. Keep delete/archive lifecycle as future extension/change pressure until a dedicated lifecycle slice exists. | Prevents current behavior/test pollution and protects future contract/process design. |
| `SL-APPL-Q-008` | `SL-APPL-002` / `planning/api/client-server-contract-principles.md` | API read contract | accepted direction | Should `GET /api/l1/applicant-parties` return grouped arrays? | No. Return one flat `applicantParties[]` list. | API DTO shape, generated types and client grouping responsibility. |
| `SL-APPL-Q-009` | `SL-APPL-002` / future client sidecar | client grouping | accepted direction | Who groups current/default templates vs other saved ApplicantParties? | Client groups the flat list by `isCurrentDefault`. | Keeps API as read-model facts, not page layout structure. |
| `SL-APPL-Q-010` | `SL-APPL-002` | default marker naming | future review | Should `IsCurrentActiveVersion` be renamed? | Later cleanup/default-template naming task; API exposes `isCurrentDefault` now. | Domain/persistence naming cleanup; not part of read slice implementation. |
| `SL-REQ-Q-APPL-004` | `SL-REQ-001` | response | open | Return request id? | Not required unless direct detail navigation appears. | Request creation command response and future client navigation. |
| `SL-REQ-Q-APPL-005` | `SL-REQ-001` | history | future review | Snapshot or reference? | Future read/history decision. | Request history/details and ApplicantParty snapshot/reference model. |
| `SL-MYREQ-FILTER-Q-005` | `L1-MY-REQUESTS-LIST-FILTERS.client` | invalid URL | assumption | What happens for invalid status URL value? | Prefer safe invalid state with reset action or safe normalization; do not crash or mislead. | Client URL parsing, safe UI state and API call prevention. |
| `SL-MYREQ-FILTER-Q-006` | `L1-MY-REQUESTS-LIST-FILTERS.client` | empty state | accepted direction | Should filtered empty state differ from regular empty state? | Yes; use a filtered empty state with reset action. | Client user feedback and tests. |
| `SL-MYREQ-DETAIL-Q-003` | `L1-MY-REQUEST-DETAILS.client` | feedback action | future review | Should details offer create-new-from-feedback? | Not in first details sidecar; add later with request creation support. | Future rejected-request UX. |
| `CC-VAL-Q-007` | `CC-VALIDATION-001` | errors | future review | Global exception handling for unexpected post-validation failures? | Separate cross-cutting concern. | Future backend error handling architecture. |
| `CC-VAL-Q-008` | `CC-VALIDATION-001` | validation mechanism | open | Should L1 eventually use validation pipeline/filter? | Future ADR after manual pattern stabilizes. | Cross-cutting validation architecture. |
| `SL-APPL-Q-001` | `SL-APPL-001` | create model | accepted | Does create replace existing ApplicantParties? | No. Create is additive. | Current create behavior and tests. |
| `SL-APPL-Q-003` | `SL-APPL-001` | API identity | accepted | Should standalone create return ApplicantPartyId? | Yes; API support, not behavior item. | API/client identity support. |
| `SL-APPL-Q-004` | `SL-APPL-001.client` | UI details | accepted | Separate details page required? | No. Details inline/cards. | Client page scope. |
| `SL-APPL-Q-005` | `SL-APPL-001` / `SL-APPL-004` | service | implemented | Shared creation service? | Yes; service validates/creates/adds and has no SaveChanges; outer handler commits. | Application service boundary. |
| `SL-REQ-Q-APPL-001` | `SL-REQ-001` | applicant context | accepted | Current active only? | No. Explicit Existing/New context. | Request creation API shape. |
| `SL-REQ-Q-APPL-002` | `SL-REQ-001` | Existing branch | accepted | Can selected existing be non-default? | Yes, any owned saved ApplicantParty. | Request creation selection behavior. |
| `SL-REQ-Q-APPL-003` | `SL-REQ-001` | New branch | accepted | Two client calls? | No, one atomic server operation. | Request creation atomicity. |
| `SL-MYREQ-FILTER-Q-001` | `L1-MY-REQUESTS-LIST-FILTERS.client` | client filters | accepted | Is status a one-off filter? | No. Status is first entry in extensible My Requests filter model. | Client filter architecture. |
| `SL-MYREQ-FILTER-Q-002` | `L1-MY-REQUESTS-LIST-FILTERS.client` | URL ownership | accepted | Who owns filter URL query params? | Page owns URL query params; filter UI receives value/callbacks. | Client page/feature split. |
| `SL-MYREQ-FILTER-Q-003` | `L1-MY-REQUESTS-LIST-FILTERS.client` | controlled component | accepted | Why does filter UI receive current filters? | It is a controlled component and must show state after reload/direct URL. | Client state rendering and tests. |
| `SL-MYREQ-FILTER-Q-004` | `L1-MY-REQUESTS-LIST-FILTERS.client` | shared API | accepted | Should API helper accept a filter object? | Yes, `listMyRequests(filters?: MyRequestsFilters)` maps supported filters to query string. | Shared API and entity query contract. |
| `SL-MYREQ-DETAIL-Q-001` | `L1-MY-REQUEST-DETAILS.client` | route state | accepted | Where does requestId live? | Route param is page-owned and parsed before entity query. | Details page routing and query boundary. |
| `SL-MYREQ-DETAIL-Q-002` | `L1-MY-REQUEST-DETAILS.client` | 404 UX | accepted | Missing/not-owned details behavior? | Render not-found state and link back to My Requests. | Client security/UX semantics. |
| `CC-VAL-Q-001` | `CC-VALIDATION-001` | validation mechanism | accepted for first pass | Manual validators or shared pipeline? | Start with manual controller-level validation, matching existing legacy pattern. | First-pass implementation scope. |
| `CC-VAL-Q-002` | `CC-VALIDATION-001` | controller safety | accepted | Do validators make controller mapping safe? | Yes. Validate before nested DTO dereference. | Controller 422-vs-500 safety. |
| `CC-VAL-Q-003` | `CC-VALIDATION-001` | handler cleanup | accepted | Should handlers repeat rules already checked by FluentValidation? | No, not as normal architecture. | Handler cleanup and validation boundary. |
| `CC-VAL-Q-004` | `CC-VALIDATION-001` | value objects | accepted | Should value-object factories be used in validators? | Yes, for pure input validation. | Validator implementation direction. |
| `CC-VAL-Q-005` | `CC-VALIDATION-001` | domain invariants | accepted | Should domain validation remain? | Yes, as final invariant guard. | Domain safety. |
| `CC-VAL-Q-006` | `CC-VALIDATION-001` | tests | accepted | Should validator unit tests be default? | No. Prefer API integration invalid variants; unit-test reusable helpers only when needed. | Test planning. |

## 2. Superseded

```text
- one current active ApplicantParty per account;
- separate current scenario split between "Account page applicant section" and "My Applicant Parties page";
- backend API response for SL-APPL-002 as currentDefaults + otherApplicantParties grouped arrays;
- treating the one-page-vs-two-page documentation correction as a user-visible behavior item;
- request creation uses server-selected single current active ApplicantParty as target;
- ApplicantPartyId as behavior item;
- E2E asserting refetch;
- status filter as one-off My Requests UI button;
- relying on L1 handlers as the normal place for request-shape/branch DTO validation after FluentValidation is introduced.
```
