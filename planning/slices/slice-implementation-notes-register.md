# Slice Implementation Notes Register

Status: active / ApplicantParty flat-list read model, validation and My Requests filters synchronized

| ID | Related slice | Layer | Note | Status |
|---|---|---|---|---|
| `NOTE-APPL-PAGE-001` | `SL-APPL-001.client` / `SL-APPL-002` / `SL-APPL-003` | client/page | ApplicantParty management uses one Applicant Parties page / section, not separate Account-page and My-Applicant-Parties-page scenarios. | accepted |
| `NOTE-APPL-PAGE-002` | `SL-APPL-002` | API/client | Read model should return one flat `applicantParties[]` list with `isCurrentDefault`; client groups current/default and other saved cards. | implementation-ready |
| `NOTE-APPL-PAGE-003` | `SL-APPL-003` | client/API | Make default/current is explicit future same-page action; create does not silently switch default/current after first-of-type initialization. | accepted |
| `NOTE-APPL-PAGE-004` | `SL-APPL-002` | API/naming | Current implementation marker `IsCurrentActiveVersion` maps to API `isCurrentDefault`; domain/persistence rename is later cleanup, not this slice. | accepted |
| `NOTE-APPL-CREATE-001` | `SL-APPL-001` | API/client | Standalone create returns ApplicantPartyId for identity/cache/future actions. | accepted |
| `NOTE-APPL-CREATE-002` | `SL-APPL-001` | behavior | Creating ApplicantParty is additive; integration coverage verifies second create does not delete/deactivate first. | implemented for standalone create |
| `NOTE-APPL-CREATE-003` | `SL-APPL-003` | default | Explicit per-type default-template persistence remains future; current standalone create does not implement silent default switching. | future review |
| `NOTE-APPL-SVC-001` | `SL-APPL-001` / `SL-APPL-004` | application | Shared creation service exists, has no SaveChanges, and standalone handler commits. | implemented for standalone create |
| `NOTE-REQ-APPL-001` | `SL-REQ-001` | API | Target request create uses explicit Existing/New applicant context. | accepted |
| `NOTE-REQ-APPL-002` | `SL-REQ-001` | transaction | New applicant + request must be atomic. | accepted |
| `NOTE-REQ-APPL-003` | future request `.client.md` | client UX | Future UI can choose any owned saved ApplicantParty; default/current is only initial prefill. | future review |
| `NOTE-REQ-APPL-004` | `SL-REQ-001` / future read/history | request history | Existing requests are not changed by ApplicantParty create or default/current changes. | accepted |
| `NOTE-APPL-CLIENT-001` | `SL-APPL-001.client` | testing | E2E asserts visible state, not refetch mechanics. | accepted |
| `NOTE-DRAFT-SCOPE-001` | slice drafts / implementation prompts | workflow | Every non-trivial slice draft must include Scope, Out of scope, Related slices / owners and Future extension points; implementation prompts must preserve these boundaries. | accepted |
| `NOTE-MYREQ-LIST-001` | `L1-MY-REQUESTS-READ-LIST.client` | client | First-stage list client exists without filters/details. | current |
| `NOTE-MYREQ-FILTER-001` | `L1-MY-REQUESTS-LIST-FILTERS.client` | client architecture | Introduce `MyRequestsFilters` model; status is first supported filter. | implementation-ready |
| `NOTE-MYREQ-FILTER-002` | `L1-MY-REQUESTS-LIST-FILTERS.client` | URL/query | Page owns URL query params and passes filters/callbacks down. | implementation-ready |
| `NOTE-MYREQ-FILTER-003` | `L1-MY-REQUESTS-LIST-FILTERS.client` | query/cache | Query key includes filters; filter changes should naturally fetch without manual refetch. | implementation-ready |
| `NOTE-MYREQ-FILTER-004` | `L1-MY-REQUESTS-LIST-FILTERS.client` | invalid URL | Invalid URL status should be handled safely before calling the list API, while backend remains safe with 422 if reached. | assumption |
| `NOTE-MYREQ-DETAIL-001` | `L1-MY-REQUEST-DETAILS.client` | client | Details route/page and API wrapper are planned; current client route/API wrapper does not include details yet. | implementation-ready |
| `NOTE-AGENT-SCOPE-001` | implementation prompts | workflow | Implementation prompts must not grant docs/domain/generated-artifact mutation permission unless explicitly requested. | accepted |
| `NOTE-VALIDATION-001` | `CC-VALIDATION-001` | validation | Add request-level FluentValidation for API input/query shape, separate from application/domain validation. | accepted |
| `NOTE-VALIDATION-002` | `CC-VALIDATION-001` | controller safety | Validate before nested DTO dereference so malformed nested payloads return 422 instead of controller exceptions. | accepted |
| `NOTE-VALIDATION-003` | `CC-VALIDATION-001` | request create | `L1CreateConnectionRequestDtoValidator` should cover Existing/New branch rules and allow handler cleanup. | implementation-ready |
| `NOTE-VALIDATION-004` | `CC-VALIDATION-001` | applicant create | `L1CreateIndividualApplicantPartyDtoValidator` should cover fullName/email/phone input before controller mapping. | implementation-ready |
| `NOTE-VALIDATION-005` | `CC-VALIDATION-001` | My Requests status query | Status query validation should move out of handler or be reduced after endpoint/query validation exists. | implementation-ready |
| `NOTE-VALIDATION-006` | `CC-VALIDATION-001` | tests | Primary proof should be API integration invalid-input tests; validator unit tests are optional for reusable helpers. | accepted |
