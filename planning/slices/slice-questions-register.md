# Slice Questions Register

Status: active / My Requests sidecars synchronized

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
| `SL-MYREQ-FILTER-Q-003` | `L1-MY-REQUESTS-LIST-FILTERS.client` | invalid URL | assumption | What happens for invalid status URL value? | Normalize/ignore safely or show a safe state; do not crash or mislead. |
| `SL-MYREQ-DETAIL-Q-001` | `L1-MY-REQUEST-DETAILS.client` | route state | accepted | Where does requestId live? | Route param is page-owned and parsed before entity query. |
| `SL-MYREQ-DETAIL-Q-002` | `L1-MY-REQUEST-DETAILS.client` | 404 UX | accepted | Missing/not-owned details behavior? | Render not-found state and link back to My Requests. |
| `SL-MYREQ-DETAIL-Q-003` | `L1-MY-REQUEST-DETAILS.client` | feedback action | future review | Should details offer create-new-from-feedback? | Not in first details sidecar; add later with request creation support. |
| `CC-VALIDATION-Q-001` | `CC-VALIDATION-001` | L1 validation | open | Manual per-controller validators or shared validation pipeline/filter? | Plan per slice; decide when first L1 validator is implemented. |

## 2. Superseded

```text
- one current active ApplicantParty per account;
- request creation uses server-selected single current active ApplicantParty as target;
- ApplicantPartyId as behavior item;
- E2E asserting refetch;
- status filter as one-off My Requests UI button.
```
