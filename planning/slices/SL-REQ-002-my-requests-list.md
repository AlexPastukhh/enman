# SL-REQ-002 — My Requests List

Status: implemented backend/API read slice  
Package: `[L1]`  
Source scenario: `SC-05 My Requests / Own Request Details`  
Slice type: backend / API / persistence read slice with dependent client UI/details slices  
Current implementation status: implemented server read endpoint and integration-tested; My Requests client UI and own request details remain separate follow-ups

## 1. Slice Overview

Observable behavior:

```text
Authenticated L1 client opens My Requests list.
System returns only that client's request summaries.
Client can optionally filter by request status.
Empty list is a normal successful state.
```

Backend scope implemented by this slice:

```text
protected L1 read endpoint
-> authenticated account id from L1 auth context
-> optional status filter
-> current account request summaries only
-> newest first by CreatedAt DESC
-> 200 OK array body
```

Out of this slice:

```text
- own request details endpoint;
- review feedback/details payload;
- My Requests client UI;
- request creation UI;
- paging;
- employee/admin request list;
- CSRF/antiforgery.
```

## 2. Sources / Source Behavior Items

Scenario / planning sources:

```text
planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md
planning/diagrams/scenario-data/SC-05-my-requests-data.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
```

Behavior/data items covered:

```text
SC-05-DATA-01 — My Requests list visible DATA:
- request summary visible enough to identify the request;
- request status: InReview / Approved / Rejected.

SC-05-DATA-02 — Request filter DATA:
- status.
```

Details are intentionally left to `SC-05-DATA-03` and future `SL-REQ-003 — Own Request Details`.

## 3. Visual Scenario Flow

```text
Authenticated L1 client
opens My Requests list
        ↓
System resolves current L1 account from cookie
        ↓
System loads requests owned by that account
        ↓
optional status filter is applied
        ↓
System returns newest request summaries first
        ↓
Client can identify each request and see current status
```

Empty state:

```text
Authenticated L1 client has no requests
        ↓
GET /api/l1/requests
        ↓
200 OK []
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Opens My Requests list. | SC-05 | source behavior |
| F02 | API/Auth boundary | Derives current L1 client account id from auth context. | L1 auth/session boundary | backend/API slice |
| F03 | System | Loads request summaries for current account only. | SC-05 invariant | backend/persistence slice |
| F04 | System | Applies optional status filter. | SC-05-DATA-02 | backend/API slice |
| F05 | System | Sorts newest first. | local read decision | backend/persistence slice |
| F06 | API | Returns `200 OK` with array body. | API contract | backend/API slice |
| F07 | Client UI | Renders My Requests page/list. | dependent client sidecar | out of scope |
| F08 | Client UI | Opens selected request details. | `SL-REQ-003` | out of scope |

## 5. Visual Implementation Flow

```text
API Controller
[Authorize] GET /api/l1/requests
Query: status?
        ↓
TryGetCurrentL1AccountId
        ↓
Application Query Handler
L1ListMyRequestsHandler
        ↓
Validate optional status filter
        ↓
ClientRequestRepository
join L1ClientRequests -> L1ApplicantParties
filter ApplicantParty.ClientAccountId == current account id
filter Status when provided
order CreatedAt DESC, Id DESC
        ↓
API DTO mapping
L1MyRequestSummaryDto[]
        ↓
200 OK
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current implementation note |
|---|---|---|---|
| I01 | API Controller | Expose protected list endpoint. | `[Authorize] GET /api/l1/requests`. |
| I02 | API/Auth boundary | Derive current account id from L1 claims. | No `accountId` query/body input. |
| I03 | Query handler | Validate optional status filter. | Unknown value returns validation ProblemDetails / 422. |
| I04 | Repository | Read current account request summaries. | Filters through applicant party ownership. |
| I05 | Repository | Sort newest first. | `CreatedAt DESC`, then `Id DESC`. |
| I06 | API mapping | Return structural summary DTOs. | `requestId`, `requestType`, `status`, `createdAt`, `summary`, `objectAddress`. |

## 7. API Contract

| Endpoint | Method | Query | Response body | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/requests` | GET | none | `L1MyRequestSummaryDto[]` | 200, 401, 422, 500 | target L1 | yes |
| `/api/l1/requests?status=InReview` | GET | `status` | `L1MyRequestSummaryDto[]` | 200, 401, 422, 500 | target L1 | yes |
| `/api/l1/requests?status=Approved` | GET | `status` | `L1MyRequestSummaryDto[]` | 200, 401, 422, 500 | target L1 | yes |
| `/api/l1/requests?status=Rejected` | GET | `status` | `L1MyRequestSummaryDto[]` | 200, 401, 422, 500 | target L1 | yes |

Response item:

```text
requestId
requestType
status
createdAt
summary
objectAddress
```

Contract rules:

```text
- client does not send accountId/clientAccountId;
- list is scoped to the authenticated L1 account;
- invalid status filter returns 422 validation ProblemDetails;
- response body is required because the client needs list data;
- own request details are not part of this endpoint.
```

## 8. Questions / Decisions

Open / future-review items first:

| ID | Area | Status | Question | Current direction |
|---|---|---|---|---|
| SL-REQ-Q-001 | Details/read model | open | What exact own request details response does the client need after selecting a list item? | Future `SL-REQ-003 — Own Request Details`. |
| SL-REQ-Q-009 | Paging | future review | When should My Requests list add paging? | Not needed for first server read slice; add when UX/data volume requires it. |

Accepted decisions:

| ID | Area | Decision |
|---|---|---|
| SL-REQ-Q-010 | Summary shape | First list summary includes `requestId`, `requestType`, `status`, `createdAt`, `summary`, `objectAddress`. |
| SL-REQ-Q-011 | Invalid filter | Invalid status filter returns existing validation ProblemDetails with status 422. |
| SL-REQ-Q-012 | Sort | Default sort is newest first by `CreatedAt DESC`, then `Id DESC`. |

## 9. Extension / Change Points

| ID | Type | Area | Current boundary | Future pressure |
|---|---|---|---|---|
| CP-REQ-LIST-PAGING-001 | extension | paging | No paging now. | Add page/pageSize or cursor when UX/data volume requires it. |
| CP-REQ-LIST-FILTER-001 | extension | filters | Status only. | Add request type/text/date filters only after UI/source behavior requires them. |
| CP-REQ-DETAILS-001 | dependent slice | details | List summary only. | Own request details belongs to `SL-REQ-003`. |

## 10. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Scenario behavior item | How slice covers it | Draft/file location | Status |
|---|---|---|---|
| SC-05-DATA-01 — request summary visible enough to identify request | Summary DTO includes id, type, created date, details summary and object address. | API Contract / Implementation Flow | covered |
| SC-05-DATA-01 — request status visible | Summary DTO includes `status`. | API Contract | covered |
| SC-05-DATA-02 — status filter | Optional `status` query filters by InReview/Approved/Rejected. | API Contract / Tests | covered |
| Client can view only own requests | Repository filters through applicant party account ownership. | Implementation Flow | covered |
| Empty My Requests state | Endpoint returns `200 OK []`. | Scenario Flow / Tests | covered |
| Own request details | Explicitly split to `SL-REQ-003`. | Questions / Follow-ups | deferred |

## 11. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `ListMyRequests_WithoutAuth_ReturnsUnauthorized` | Protected endpoint requires auth. | API/auth | implemented |
| `ListMyRequests_WithNoRequests_ReturnsEmptyArray` | Empty state is successful `[]`. | API/read | implemented |
| `ListMyRequests_ReturnsCurrentAccountRequestSummaries` | Summary fields/status/address are returned. | API/read/persistence | implemented |
| `ListMyRequests_DoesNotReturnAnotherAccountRequests` | Ownership scoping uses current account. | API/read/persistence | implemented |
| `ListMyRequests_WithStatusFilter_ReturnsMatchingRequests` | Status filter works. | API/read/persistence | implemented |
| `ListMyRequests_WithInvalidStatusFilter_ReturnsValidationProblem` | Invalid status returns 422 ProblemDetails. | API/error contract | implemented |
| `ListMyRequests_ReturnsNewestFirst` | Default newest-first sorting. | API/read/persistence | implemented |
| OpenAPI/check workflow | New path and DTOs are generated. | Tooling/API contract | required after implementation |

## 12. Dependent / Follow-up Slices

```text
SL-REQ-003 — Own Request Details
future My Requests client sidecar
future request creation client sidecar
future paging/filter extension
future browser E2E after client UI exists
CC-CSRF-001 unsafe browser command protection remains separate and does not affect this GET read endpoint directly
```

## 13. Implementation Checklist

```text
[x] GET /api/l1/requests exists
[x] endpoint is protected by authorization
[x] account id comes from L1 auth context
[x] no accountId query parameter is exposed
[x] empty list returns 200 OK []
[x] list returns current account request summaries
[x] another account's requests are excluded
[x] status filter supports InReview / Approved / Rejected
[x] invalid status returns validation ProblemDetails / 422
[x] default sort is newest first
[x] OpenAPI metadata added
[x] integration tests cover server read behavior
[ ] My Requests client UI
[ ] own request details endpoint
[ ] paging
[ ] E2E browser flow
```
