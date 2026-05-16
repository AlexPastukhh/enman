# SL-REQ-003 — Own Request Details

Status: implemented backend/API read slice  
Package: `[L1]`  
Source scenario: `SC-05 My Requests / Own Request Details`  
Slice type: backend / API / persistence read slice with dependent client UI slices  
Current implementation status: implemented server read endpoint and integration-tested; request details client UI remains future work

## 1. Slice Overview

Observable behavior:

```text
Authenticated L1 client selects one own request.
System returns that request details.
Missing request and another client's request are both hidden as Not Found.
Status-specific review result is included when it exists.
```

Backend scope implemented by this slice:

```text
protected L1 request details endpoint
-> request id route parameter
-> authenticated account id from L1 auth context
-> ownership-scoped request lookup
-> submitted request data
-> status + createdAt + request type
-> review result for Approved / Rejected when available
```

Out of this slice:

```text
- My Requests list behavior, owned by SL-REQ-002;
- details client UI;
- create-new-from-feedback button;
- applicant snapshot;
- agreement/documents/notifications;
- employee review command;
- CSRF/antiforgery.
```

## 2. Sources / Source Behavior Items

Scenario / planning sources:

```text
planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md
planning/diagrams/scenario-data/SC-05-my-requests-data.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REQ-002-my-requests-list.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
```

Behavior/data items covered:

```text
SC-05-DATA-03 — Own Request Details visible DATA:
- request status;
- submitted request data visible to the client;
- status-specific review result/feedback.
```

Status-specific data:

```text
InReview: no review result yet.
Approved: approved review result and decision date when available.
Rejected: rejection reason/feedback and decision date when available.
```

## 3. Visual Scenario Flow

```text
Authenticated L1 client
selects request from My Requests list
        ↓
System resolves current L1 account from cookie
        ↓
System looks up request by id scoped to current account
        ↓
request exists and belongs to account?
        ↓ yes
System returns submitted request content and current status
        ↓
if reviewed, System also returns review decision/result
```

Hidden / missing state:

```text
request does not exist OR belongs to another account
        ↓
404 Not Found
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Selects a request from My Requests. | SC-05 | source behavior |
| F02 | API/Auth boundary | Derives current L1 account id from auth context. | L1 auth/session boundary | backend/API slice |
| F03 | System | Loads request by id scoped to current account. | SC-05 invariant | backend/persistence slice |
| F04 | System | Hides missing/not-owned requests as 404. | security/API decision | backend/API slice |
| F05 | System | Returns status, type, createdAt and submitted request content. | SC-05-DATA-03 | backend/API slice |
| F06 | System | Returns review result when available. | SC-05-DATA-03 | backend/API slice |
| F07 | Client UI | Renders own request details page. | future client sidecar | out of scope |
| F08 | Client UI | Offers create-new-from-feedback action for rejected request. | future details UI | out of scope |

## 5. Visual Implementation Flow

```text
API Controller
[Authorize] GET /api/l1/requests/{requestId}
        ↓
TryGetCurrentL1AccountId
        ↓
Application Query Handler
L1GetMyRequestDetailsHandler
        ↓
ClientRequestRepository
join L1ClientRequests -> L1ApplicantParties
where request.Id == requestId
where ApplicantParty.ClientAccountId == current account id
        ↓
not found -> 404
found -> details read model
        ↓
API DTO mapping
L1MyRequestDetailsDto
        ↓
200 OK
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current implementation note |
|---|---|---|---|
| I01 | API Controller | Expose protected details endpoint. | `[Authorize] GET /api/l1/requests/{requestId:long}`. |
| I02 | API/Auth boundary | Derive current account id from L1 claims. | No `accountId` query/body input. |
| I03 | Query handler | Request own details by id. | Returns Maybe-style found/missing result. |
| I04 | Repository | Scope lookup to current account. | Filters through applicant party ownership. |
| I05 | Repository | Read submitted request data. | Details + object address. |
| I06 | Repository | Read review result columns when present. | `ReviewDecision`, `ReviewDecidedAt`, `ReviewRejectionReason`. |
| I07 | API mapping | Return details DTO or 404. | Missing and not-owned both return 404. |

## 7. API Contract

| Endpoint | Method | Route | Response body | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/requests/{requestId}` | GET | `requestId: long` | `L1MyRequestDetailsDto` | 200, 401, 404, 500 | target L1 | yes |

Response:

```text
requestId
requestType
status
createdAt
submittedRequest:
  details
  objectAddress
reviewResult:
  decision
  decidedAt
  rejection:
    reason
```

Contract rules:

```text
- client does not send accountId/clientAccountId;
- another client's request returns 404, same as missing;
- InReview has reviewResult=null;
- Rejected maps domain/user-visible feedback text to rejection.reason;
- no applicant snapshot;
- no agreement/document/notification payload;
- no create-new-from-feedback server flag in first cut.
```

## 8. Questions / Decisions

Open / future-review items first:

| ID | Area | Status | Question | Current direction |
|---|---|---|---|---|
| SL-REQ-Q-013 | Applicant snapshot | future review | Should details include applicant snapshot? | No in first cut; add deliberately when UI/source behavior needs it. |
| SL-REQ-Q-014 | Create new from feedback | future review | Should backend return a `canCreateNewFromFeedback` flag? | No in first cut; client can derive action from `status === Rejected` until policy becomes non-trivial. |
| SL-REQ-Q-015 | Agreement/documents | future review | Should approved details include agreement/documents data? | No; future agreement/documents slices own that payload. |

Accepted decisions:

| ID | Area | Decision |
|---|---|---|
| SL-REQ-Q-001 | Details split | Details are separate from `SL-REQ-002` list and implemented here as `SL-REQ-003`. |
| SL-REQ-Q-016 | Missing/not-owned | Missing request and another account's request both return 404. |
| SL-REQ-Q-017 | Rejected feedback shape | Rejected details include `reviewResult.rejection.reason` mapped from `RejectionFeedback.Value` / persisted rejection reason. |

## 9. Extension / Change Points

| ID | Type | Area | Current boundary | Future pressure |
|---|---|---|---|---|
| CP-REQ-DETAILS-APPLICANT-001 | extension | applicant snapshot | Not included. | Add only if details UI/source behavior requires it. |
| CP-REQ-DETAILS-FEEDBACK-001 | extension | feedback action | No server flag for create-new-from-feedback. | Add policy flag only if eligibility becomes more complex than rejected status. |
| CP-REQ-DETAILS-DOCS-001 | extension | documents/agreement | Not included. | Future documents/agreement slices add payload or linked endpoints. |

## 10. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Scenario behavior item | How slice covers it | Draft/file location | Status |
|---|---|---|---|
| SC-05-DATA-03 — request status | Details DTO includes `status`. | API Contract | covered |
| SC-05-DATA-03 — submitted request data | Details DTO includes submitted details text and object address. | API Contract / Implementation Flow | covered |
| SC-05-DATA-03 — status-specific review result/feedback | Approved/Rejected return review result when review data exists; InReview returns null. | API Contract / Tests | covered |
| Client can view only own request details | Repository scopes lookup to current account and returns 404 for not-owned. | Implementation Flow | covered |
| My Requests list | Implemented by `SL-REQ-002`, not this slice. | Dependent / Follow-up | separate |

## 11. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `GetMyRequestDetails_WithoutAuth_ReturnsUnauthorized` | Protected endpoint requires auth. | API/auth | implemented |
| `GetMyRequestDetails_ForOwnInReviewRequest_ReturnsSubmittedRequestData` | Own details include status, type, createdAt, details/address and no review result. | API/read/persistence | implemented |
| `GetMyRequestDetails_ForMissingRequest_ReturnsNotFound` | Missing request returns 404. | API/read | implemented |
| `GetMyRequestDetails_ForAnotherAccountRequest_ReturnsNotFound` | Not-owned request returns 404. | API/read/security | implemented |
| `GetMyRequestDetails_ForRejectedRequest_ReturnsFeedbackAndSubmittedData` | Rejected details include decision, date, reason and original submitted data. | API/read/persistence | implemented |
| `GetMyRequestDetails_ForApprovedRequest_ReturnsApprovedDecision` | Approved details include decision and date. | API/read/persistence | implemented |
| OpenAPI/check workflow | New path and DTOs are generated. | Tooling/API contract | required after implementation |

## 12. Dependent / Follow-up Slices

```text
SL-REQ-002 — My Requests List
future My Requests / request details client sidecar
future create-new-from-feedback UI action
future employee review command
future agreement/documents/notifications
future browser E2E after client UI exists
CC-CSRF-001 unsafe browser command protection remains separate
```

## 13. Implementation Checklist

```text
[x] GET /api/l1/requests/{requestId} exists
[x] endpoint is protected by authorization
[x] account id comes from L1 auth context
[x] own request details return 200 OK
[x] missing request returns 404
[x] not-owned request returns 404
[x] response includes requestId/type/status/createdAt
[x] response includes submitted details and object address
[x] InReview response has reviewResult=null
[x] Rejected response includes reason and decision date
[x] Approved response includes approved decision and date
[x] no applicant snapshot in first cut
[x] no create-new-from-feedback flag in first cut
[x] integration tests cover server read behavior
[ ] request details client UI
[ ] create-new-from-feedback UI action
[ ] agreement/documents payload
[ ] E2E browser flow
[ ] CSRF broad unsafe command rollout
```
