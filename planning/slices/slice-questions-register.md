# Slice Questions Register

Status: active / validation and My Requests filters synchronized

## 1. Questions / Decisions

| ID | Local file(s) | Area | Status | Question | Current direction |
|---|---|---|---|---|---|
| `SL-APPL-Q-001` | `SL-APPL-001` | create model | accepted | Does create replace existing ApplicantParties? | No. Create is additive. |
| `SL-APPL-Q-002` | `SL-APPL-001` / `SL-APPL-003` | default | future review | What happens to default on create? | Current standalone create preserves existing applicants; explicit per-type default-template persistence remains future. |
| `SL-APPL-Q-003` | `SL-APPL-001` | API identity | accepted | Should standalone create return ApplicantPartyId? | Yes; API support, not behavior item. |
| `SL-APPL-Q-004` | `SL-APPL-001.client` | UI details | accepted | Separate details page required? | No. Details inline. |
| `SL-APPL-Q-005` | `SL-APPL-001` / `SL-APPL-004` | service | implemented | Shared creation service? | Yes; service validates/creates/adds and has no SaveChanges; outer handler commits. |
| `SL-REQ-Q-APPL-001` | `SL-REQ-001` | applicant context | accepted | Current active only? | No. Explicit Existing/New context. |
| `SL-REQ-Q-APPL-002` | `SL-REQ-001` | Existing branch | accepted | Can selected existing be non-default? | Yes, any owned saved ApplicantParty. |
| `SL-REQ-Q-APPL-003` | `SL-REQ-001` | New branch | accepted | Two client calls? | No, one atomic server operation. |
| `SL-REQ-Q-APPL-004` | `SL-REQ-001` | response | open | Return request id? | Not required unless direct detail navigation appears. |
| `SL-REQ-Q-APPL-005` | `SL-REQ-001` | history | future review | Snapshot or reference? | Future read/history decision. |
| `SL-MYREQ-FILTER-Q-001` | `L1-MY-REQUESTS-LIST-FILTERS.client` | client filters | accepted | Is status a one-off filter? | No. Status is first entry in extensible My Requests filter model. |
| `SL-MYREQ-FILTER-Q-002` | `L1-MY-REQUESTS-LIST-FILTERS.client` | URL ownership | accepted | Who owns filter URL query params? | Page owns URL query params; filter UI receives value/callbacks. |
| `SL-MYREQ-FILTER-Q-003` | `L1-MY-REQUESTS-LIST-FILTERS.client` | controlled component | accepted | Why does filter UI receive current filters? | It is a controlled component and must show state after reload/direct URL. |
| `SL-MYREQ-FILTER-Q-004` | `L1-MY-REQUESTS-LIST-FILTERS.client` | shared API | accepted | Should API helper accept a filter object? | Yes, `listMyRequests(filters?: MyRequestsFilters)` maps supported filters to query string. |
| `SL-MYREQ-FILTER-Q-005` | `L1-MY-REQUESTS-LIST-FILTERS.client` | invalid URL | assumption | What happens for invalid status URL value? | Prefer safe invalid state with reset action or safe normalization; do not crash or mislead. |
| `SL-MYREQ-FILTER-Q-006` | `L1-MY-REQUESTS-LIST-FILTERS.client` | empty state | accepted direction | Should filtered empty state differ from regular empty state? | Yes; use a filtered empty state with reset action. |
| `SL-MYREQ-DETAIL-Q-001` | `L1-MY-REQUEST-DETAILS.client` | route state | accepted | Where does requestId live? | Route param is page-owned and parsed before entity query. |
| `SL-MYREQ-DETAIL-Q-002` | `L1-MY-REQUEST-DETAILS.client` | 404 UX | accepted | Missing/not-owned details behavior? | Render not-found state and link back to My Requests. |
| `SL-MYREQ-DETAIL-Q-003` | `L1-MY-REQUEST-DETAILS.client` | feedback action | future review | Should details offer create-new-from-feedback? | Not in first details sidecar; add later with request creation support. |
| `CC-VAL-Q-001` | `CC-VALIDATION-001` | validation mechanism | accepted for first pass | Manual validators or shared pipeline? | Start with manual controller-level validation, matching existing legacy pattern. |
| `CC-VAL-Q-002` | `CC-VALIDATION-001` | controller safety | accepted | Do validators make controller mapping safe? | Yes. Validate before nested DTO dereference. |
| `CC-VAL-Q-003` | `CC-VALIDATION-001` | handler cleanup | accepted | Should handlers repeat rules already checked by FluentValidation? | No, not as normal architecture. |
| `CC-VAL-Q-004` | `CC-VALIDATION-001` | value objects | accepted | Should value-object factories be used in validators? | Yes, for pure input validation. |
| `CC-VAL-Q-005` | `CC-VALIDATION-001` | domain invariants | accepted | Should domain validation remain? | Yes, as final invariant guard. |
| `CC-VAL-Q-006` | `CC-VALIDATION-001` | tests | accepted | Should validator unit tests be default? | No. Prefer API integration invalid variants; unit-test reusable helpers only when needed. |
| `CC-VAL-Q-007` | `CC-VALIDATION-001` | errors | future review | Global exception handling for unexpected post-validation failures? | Separate cross-cutting concern. |
| `CC-VAL-Q-008` | `CC-VALIDATION-001` | validation mechanism | open | Should L1 eventually use validation pipeline/filter? | Future ADR after manual pattern stabilizes. |

## 2. Superseded

```text
- one current active ApplicantParty per account;
- request creation uses server-selected single current active ApplicantParty as target;
- ApplicantPartyId as behavior item;
- E2E asserting refetch;
- status filter as one-off My Requests UI button;
- relying on L1 handlers as the normal place for request-shape/branch DTO validation after FluentValidation is introduced.
```
