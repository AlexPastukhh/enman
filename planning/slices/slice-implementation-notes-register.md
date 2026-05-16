# Slice Implementation Notes Register

Status: active / My Requests sidecars synchronized

| ID | Related slice | Layer | Note | Status |
|---|---|---|---|---|
| `NOTE-APPL-CREATE-001` | `SL-APPL-001` | API/client | Standalone create returns ApplicantPartyId for identity/cache/future actions. | accepted |
| `NOTE-APPL-CREATE-002` | `SL-APPL-001` | behavior | Creating ApplicantParty is additive; integration coverage verifies second create does not delete/deactivate first. | implemented for standalone create |
| `NOTE-APPL-CREATE-003` | `SL-APPL-003` | default | Explicit per-type default-template persistence remains future; current standalone create does not implement default selection. | future review |
| `NOTE-APPL-SVC-001` | `SL-APPL-001` / `SL-APPL-004` | application | Shared creation service exists, has no SaveChanges, and standalone handler commits. | implemented for standalone create |
| `NOTE-REQ-APPL-001` | `SL-REQ-001` | API | Target request create uses explicit Existing/New applicant context. | accepted |
| `NOTE-REQ-APPL-002` | `SL-REQ-001` | transaction | New applicant + request must be atomic. | accepted |
| `NOTE-REQ-APPL-003` | future request `.client.md` | client UX | Future UI can choose any owned saved ApplicantParty; default is only initial prefill. | future review |
| `NOTE-APPL-CLIENT-001` | `SL-APPL-001.client` | testing | E2E asserts visible state, not refetch mechanics. | accepted |
| `NOTE-MYREQ-LIST-001` | `L1-MY-REQUESTS-READ-LIST.client` | client | First-stage list client exists without filters/details. | current |
| `NOTE-MYREQ-FILTER-001` | `L1-MY-REQUESTS-LIST-FILTERS.client` | client architecture | Introduce `MyRequestsFilters` model; status is first supported filter. | implementation-ready |
| `NOTE-MYREQ-FILTER-002` | `L1-MY-REQUESTS-LIST-FILTERS.client` | URL/query | Page owns URL query params and passes filters/callbacks down. | implementation-ready |
| `NOTE-MYREQ-DETAIL-001` | `L1-MY-REQUEST-DETAILS.client` | client | Details route/page and API wrapper are planned; current client route/API wrapper does not include details yet. | implementation-ready |
| `NOTE-AGENT-SCOPE-001` | implementation prompts | workflow | Implementation prompts must not grant docs/domain/generated-artifact mutation permission unless explicitly requested. | accepted |
| `NOTE-VALIDATION-001` | server API slices | validation | Add request-level FluentValidation planning for API input/query shape, separate from application/domain validation. | accepted |
