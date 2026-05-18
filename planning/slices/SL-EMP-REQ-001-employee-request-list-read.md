# SL-EMP-REQ-001 — Employee Request List Read

Status: implemented slice draft refactor / implementation evidence inspected read-only / runtime not changed  
Package: `[Employee] [Requests]`  
Slice type: server/API read slice + Dapper read projection  
Primary purpose: Employee reads the review dashboard request list with compact row data and review-state filters  
Depends on:

* Employee auth/session
* `Employee : Account` identity direction
* `ConnectionRequest` / `ClientRequest` persistence
* request-owned `RequestReview` persistence
* ApplicantParty persistence for applicant display summary
* server validation / ProblemDetails mapping
* paired client sidecar `L2-EMP-DASH-001.client`

Implementation direction:

```text
GET /api/employee/requests?status=...&reviewState=...

Employee branch:
  authenticated Employee role reads review-relevant requests.

Current implementation evidence:
  controller resolves current employee account id from claims;
  Dapper projection reads request rows and left-joins review rows;
  status filter is applied in SQL;
  reviewState filter is applied after review-state derivation;
  no mutation happens.
```

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was inspected read-only for current names, routes, DTOs and tests.
Runtime code, tests and generated artifacts were not changed in this archive.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-06 — Employee Request Dashboard
```

Related scenario context:

```text
SC-07A — Employee Request Details
SC-07B — Employee Request Review Actions
```

UI scenario:

```text
missing / pending dedicated UI source for Employee request dashboard layout.
Current client sidecar records implemented page/widget/filter behavior.
```

Cross-cutting behavior:

```text
CC-VALIDATION-001 — server request/query validation and ProblemDetails mapping
CC-CLIENT-FEEDBACK-001 — client error / feedback visibility, for paired client sidecar
CC-API-001 — OpenAPI/generated contract artifacts if API shape changes
```

Data source:

```text
current implementation DTOs in EnergyManagement.Server/L1/Api/L1Dtos.cs;
scenario-data source for Employee dashboard row fields remains pending.
```

Behavior items:

```text
stable source behavior item IDs are pending source registry;
this draft uses provisional behavior names and current implementation evidence.
```

Concern umbrella:

```text
none for this server read slice.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-06: pending / v000 if source registry is applied
SC-07A: pending / v000 if source registry is applied
SC-07B: pending / v000 if source registry is applied
```

Domain baseline:

```text
DOM-v001 if source-sync/domain registry is applied;
otherwise current domain/read-model evidence from repo code.
```

Slice derivation map:

```text
pending / add or update row for SL-EMP-REQ-001 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain / implementation disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| Employee opens request dashboard | SC-06 pending | API/controller boundary implemented | expose Employee-only list endpoint | paired client page owns visual page shell |
| Unauthenticated user cannot list employee requests | SC-06 pending | ASP.NET auth boundary | return 401 through endpoint auth | covered by integration test |
| Client account cannot list employee requests | SC-06 pending | role authorization | return 403 for non-Employee role | covered by integration test |
| Employee sees compact request rows | SC-06 pending | Dapper projection implemented | return request id/type/status/applicant/address/createdAt/reviewState | no full details text in list |
| Employee sees review state per row | SC-06/SC-07B pending | implemented in read projection helper | derive NotStarted / StartedByCurrentEmployee / StartedByAnotherEmployee / Approved / Rejected | current employee id is used for relative started label |
| Status filter narrows rows | SC-06 pending | FluentValidation + SQL status filter | validate `RequestStatus` enum and apply filter | current API query param `status` |
| Review-state filter narrows rows | SC-06 pending | FluentValidation + post-projection filter | validate `EmployeeRequestReviewState` and filter derived rows | current API query param `reviewState` |
| Invalid filters return validation problem | CC-VALIDATION-001 | validator implemented | map invalid enum values to validation ProblemDetails | field names from `L1FieldNames.EmployeeRequestList` |
| List read does not mutate request/review | SC-06 pending | read handler only | no domain mutation, no SaveChanges | Dapper read projection only |
| Dashboard commands are not owned by this slice | SC-07B pending | command slices own mutation | this read endpoint may feed row action availability but does not start/approve/reject | paired client may host StartReview action slot only |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync
```

Implemented files inspected read-only:

```text
server:
  EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs
  EnergyManagement.Server/L1/Application/Queries/EmployeeRequestListQuery.cs
  EnergyManagement.Server/L1/Application/Queries/EmployeeRequestListHandler.cs
  EnergyManagement.Server/L1/Api/Validation/EmployeeRequestListQueryDtoValidator.cs
  EnergyManagement.Server/L1/Api/L1Dtos.cs

tests:
  Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRequestListIntegrationTests.cs

client paired sidecar:
  planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
```

Checked against:

```text
source versions:
  pending source-sync registry

domain baseline:
  current repo evidence inspected read-only; formal domain baseline registry pending

slice derivation map version:
  pending
```

Known drift:

```text
docs:
  - old draft said full backend/API read slice draft / not implemented;
  - current code and tests show this slice is implemented;
  - old draft lacked Scenario Sources;
  - old draft lacked Source / Domain / Slice Coverage Snapshot;
  - old draft lacked Implementation Sync Status;
  - old draft lacked Behavior-to-Test Trace with escape/refactor risk;
  - old draft did not record actual route, DTO, filters, handler and integration tests.

implementation:
  - current implementation evidence inspected read-only;
  - tests were not executed in this archive;
  - implementation currently uses role + claim identity and Dapper projection;
  - separate active Employee entity validation is not clearly evidenced in the list endpoint; do not overclaim it here.

source:
  - stable behavior item IDs and source registry rows are still pending.
```

Last sync note:

```text
Docs-only refactor using uploaded repo zip as read-only implementation evidence.
No runtime code, tests or generated artifacts changed.
```

---

## 1. Slice Overview

This slice owns the Employee dashboard list endpoint.

Current endpoint:

```http
GET /api/employee/requests?status=...&reviewState=...
```

Current route owner:

```text
EmployeeRequestsController.ListRequests
```

Current response:

```text
EmployeeRequestListResponseDto
  requests: EmployeeRequestListItemDto[]
```

Current row fields:

```text
requestId
requestType
status
applicantDisplayName
objectAddress
createdAt
reviewState
```

Current filter query params:

```text
status?: RequestStatus
reviewState?: EmployeeRequestReviewState
```

This is a read/list slice.

It does **not** load full request details.

It does **not** start review.

It does **not** approve review.

It does **not** reject review.

It does **not** create AgreementProposalExchange.

---

## 2. Scope

This slice owns:

```text
- Employee-only request list endpoint;
- current Employee id resolution from authenticated session/claims;
- optional status filter validation;
- optional reviewState filter validation;
- Dapper/read projection over request/applicant/review data;
- compact list response DTO;
- review-state derivation relative to current Employee;
- ordering by CreatedAt desc and Id desc;
- 200 OK response with rows;
- 401/403 auth boundary behavior;
- 422 validation problem for invalid filter values;
- integration test plan and actual integration evidence for access/filter/projection behavior.
```

Endpoint:

```http
GET /api/employee/requests
```

Current query:

```text
status?: string
reviewState?: string
```

Current success response:

```http
200 OK EmployeeRequestListResponseDto
```

---

## 3. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Employee request details endpoint | `SL-EMP-REQ-002 — Employee Request Details Read` |
| Start review command | `SL-EMP-REQ-003 — Start Request Review` |
| Approve review command | `SL-EMP-REQ-004 — Approve Request Review` |
| Reject review command | `SL-EMP-REQ-005 — Reject Request Review` |
| Employee dashboard page/UI implementation | `L2-EMP-DASH-001.client` |
| Details page UI | `L2-EMP-DETAILS-001.client` |
| AgreementProposalExchange | agreement exchange slice family |
| Department/assignment/personal queue policy | future Employee visibility slice |
| Pagination/sorting beyond current ordering | future extension |
| Command action authorization | command slices + domain/application |
| Request/review mutation | command slices |

Important boundary:

```text
This endpoint provides read state for dashboard rows.

It is not a command endpoint.

It does not make row actions secure by itself; command endpoints still authorize and validate commands.
```

---

## 4. Scenario Flow

```text
[Employee]
opens Employee Request Dashboard
        ↓
[System]
requires authenticated Employee role
        ↓
[System]
reads optional status/reviewState filters
        ↓
[System]
validates filter enum values
        ↓
[System]
projects review-relevant request rows
        ↓
[System]
derives reviewState relative to current Employee
        ↓
[System]
returns compact dashboard rows
        ↓
[Client]
renders dashboard list and optional row actions owned by command sidecars
```

Scenario flow table:

| Step | Actor / layer | Responsibility |
|---|---|---|
| S01 | Employee | Opens dashboard/list page. |
| S02 | Server auth | Allows Employee role only. |
| S03 | API validation | Validates `status` and `reviewState` query params. |
| S04 | Read projection | Reads request/applicant/review rows. |
| S05 | Read projection | Derives reviewState relative to current Employee id. |
| S06 | API response | Returns compact rows. |
| S07 | Client sidecar | Renders list, filters, empty/loading/error states and row navigation. |

---

## 5. Implementation Flow

```text
[HTTP GET]
GET /api/employee/requests?status=...&reviewState=...
        ↓
[Auth]
Employee role required
        ↓
[Controller]
create EmployeeRequestListQueryDto(status, reviewState)
        ↓
[FluentValidation]
validate RequestStatus and EmployeeRequestReviewState enum values
        ↓
[Session]
TryGetCurrentEmployeeId(out employeeId)
        ↓
[MediatR]
send EmployeeRequestListQuery(employeeId, status, reviewState)
        ↓
[Handler]
normalize blank filters to null
        ↓
[Dapper]
query dbo.L1ClientRequests + L1ApplicantParties + L1RequestReviews
        ↓
[Projection]
format applicantDisplayName and objectAddress
        ↓
[Projection]
derive EmployeeRequestReviewState relative to employeeId
        ↓
[Filter]
apply reviewState filter after derivation
        ↓
[Response]
200 OK EmployeeRequestListResponseDto
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth, query DTO validation, current employee id extraction, DTO mapping.

Validator:
  query shape only: status/reviewState enum values.

Handler/read projection:
  Dapper SQL, row projection, review-state derivation, post-projection reviewState filter.

Domain:
  persisted RequestStatus / RequestReviewStatus values and review data.

Client:
  paired sidecar owns page, filters, query hook, list widget and visible states.
```

---

## 6. API Contract

### Endpoint

```http
GET /api/employee/requests
```

Auth:

```csharp
[Authorize(Roles = "Employee")]
```

Query:

```csharp
string? status
string? reviewState
```

Current query DTO:

```csharp
public sealed record EmployeeRequestListQueryDto(
    string? Status,
    string? ReviewState);
```

Current application query:

```csharp
public sealed record EmployeeRequestListQuery(
    long EmployeeId,
    string? Status,
    string? ReviewState)
    : IRequest<Result<EmployeeRequestListResponse, IReadOnlyList<Error>>>;
```

Current response DTO:

```csharp
public sealed record EmployeeRequestListResponseDto(
    IReadOnlyList<EmployeeRequestListItemDto> Requests);

public sealed record EmployeeRequestListItemDto(
    long RequestId,
    string RequestType,
    string Status,
    string ApplicantDisplayName,
    string ObjectAddress,
    DateTimeOffset CreatedAt,
    string ReviewState);
```

Current review-state enum:

```csharp
public enum EmployeeRequestReviewState
{
    NotStarted = 1,
    StartedByCurrentEmployee = 2,
    StartedByAnotherEmployee = 3,
    Approved = 4,
    Rejected = 5
}
```

Success:

```http
200 OK
```

Failure categories:

```text
401 Unauthorized
  no authenticated session or current employee id cannot be resolved

403 Forbidden
  authenticated but not Employee role

422 UnprocessableEntity
  invalid status or reviewState filter

500 InternalServerError
  unexpected server failure
```

---

## 7. Validation / FluentValidation

Validator:

```text
EmployeeRequestListQueryDtoValidator
```

Shape validation:

```text
status:
  optional;
  blank/whitespace is treated as no filter;
  if present, must parse as RequestStatus.

reviewState:
  optional;
  blank/whitespace is treated as no filter;
  if present, must parse as EmployeeRequestReviewState.
```

Field names:

```text
L1FieldNames.EmployeeRequestList.Status
L1FieldNames.EmployeeRequestList.ReviewState
```

Error code:

```text
Error.Errors.General.ValueIsInvalid.Code
```

Validator does **not** own:

```text
- Employee exists;
- Employee is active;
- request visibility policy;
- review-state derivation;
- row projection;
- command action availability;
- DB reads.
```

---

## 8. Read Model / Projection Rules

Current SQL source tables:

```text
dbo.L1ClientRequests AS request
dbo.L1ApplicantParties AS applicant
dbo.L1RequestReviews AS review
```

Current join direction:

```text
request INNER JOIN applicant
request LEFT JOIN review
```

Current status filter:

```sql
WHERE (@Status IS NULL OR request.Status = @Status)
```

Current order:

```sql
ORDER BY request.CreatedAt DESC, request.Id DESC
```

Current row projection:

```text
RequestId
RequestType
Status
CreatedAt
Details
ObjectAddress_* fields
Applicant full-name fields
ReviewStatus
StartedByEmployeeId
```

Current response formatting:

```text
ApplicantDisplayName = LastName FirstName MiddleName, skipping blanks
ObjectAddress = comma-separated non-empty address parts
ReviewState = derived EmployeeRequestReviewState.ToString()
```

Review-state derivation:

```text
ReviewStatus = Started:
  StartedByEmployeeId == currentEmployeeId -> StartedByCurrentEmployee
  otherwise -> StartedByAnotherEmployee

ReviewStatus = Approved:
  Approved

ReviewStatus = Rejected:
  Rejected

No review / unknown review status:
  fallback to request status if request status is Approved or Rejected;
  otherwise NotStarted.
```

ReviewState filter:

```text
Applied after projection because StartedByCurrentEmployee vs StartedByAnotherEmployee depends on current employee id.
```

---

## 9. Security / Protection

```text
Employee dashboard read is Employee role only.

Client role receives 403.

Unauthenticated receives 401.

Current employee id is derived from session claims and used for relative review-state derivation.
```

Do not rely on dashboard row action visibility for command security.

Command slices still enforce:

```text
StartReview;
ApproveReview;
RejectReview;
CSRF;
request/review lifecycle;
current Employee ownership of started review where required.
```

Do not claim department/assignment/personal queue filtering first pass.

Current first-pass visibility is broad Employee review visibility.

---

## 10. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns server/API Employee request list endpoint and compact row projection.

L2-EMP-DASH-001.client
  Owns Employee dashboard page, filters, query hook, list widget and visible states.

SL-EMP-REQ-002 / L2-EMP-DETAILS-001.client
  Own details read endpoint/page.

SL-EMP-REQ-003 / L2-REVIEW-START-001.client
  Own StartReview command and action behavior.

SL-EMP-REQ-004 / approve client sidecar
  Own ApproveReview command/action behavior.

SL-EMP-REQ-005 / reject client sidecar
  Own RejectReview command/action behavior.

Agreement exchange slices
  Own agreement proposal exchange after request approval.
```

---

## 11. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Employee can list review-relevant requests | implemented evidence | `GET /api/employee/requests` with Employee role. |
| Unauthenticated cannot list employee requests | implemented evidence | auth returns 401. |
| Client account cannot list employee requests | implemented evidence | role auth returns 403. |
| Empty dashboard returns empty list | implemented evidence | response `requests: []`. |
| Compact row data returned | implemented evidence | DTO includes request/applicant/address/createdAt/reviewState. |
| Review state NotStarted | implemented evidence | no review / non-completed request state. |
| Review state StartedByCurrentEmployee | implemented evidence | review Started and StartedByEmployeeId equals current employee id. |
| Review state StartedByAnotherEmployee | implemented evidence | review Started and StartedByEmployeeId differs. |
| Review state Approved | implemented evidence | review/request approved state. |
| Review state Rejected | implemented evidence | review/request rejected state. |
| Status filter works | implemented evidence | SQL status filter and integration test. |
| ReviewState filter works | implemented evidence | post-projection filter and integration test. |
| Invalid filters return validation problem | implemented evidence | validator + integration tests. |
| List read does not mutate data | supported by implementation shape | Dapper read-only query, no SaveChanges. |
| Start/approve/reject actions | out of scope | command slices own mutations. |

---

## 12. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Unauthenticated cannot list | request without auth returns 401 | API integration | HTTP GET `/api/employee/requests` without auth | Low: proves public auth boundary | Low | actual `ListEmployeeRequests_WithoutAuth_ReturnsUnauthorized` |
| Client role cannot list | Client account receives 403 | API integration | Client auth fixture + HTTP GET | Low: catches role leak | Low | actual `ListEmployeeRequests_WithClientAccount_ReturnsForbidden` |
| Empty list | no rows returns empty `requests` | API integration | reset DB, Employee auth, HTTP GET | Low | Low | actual `ListEmployeeRequests_WithNoRequests_ReturnsEmptyList` |
| Compact rows + review states | response contains row fields and all review state labels | API integration + DB fixtures | create requests/reviews, HTTP GET, response assertions | Low: checks visible API output, not internal calls | Low/Medium: fixture helpers/schema changes may need updates | actual `ListEmployeeRequests_ReturnsCompactRowsWithReviewStates` |
| Started by current employee | row reviewState is `StartedByCurrentEmployee` | API integration | review fixture with StartedByEmployeeId=current | Low | Low/Medium | covered by compact rows test |
| Started by another employee | row reviewState is `StartedByAnotherEmployee` | API integration | review fixture with different StartedByEmployeeId | Low | Low/Medium | covered by compact rows test |
| Status filter | only matching request status returned | API integration | HTTP GET with `status=Approved` | Low if excluded rows are absent | Low | actual `ListEmployeeRequests_WithStatusFilter_ReturnsMatchingRows` |
| ReviewState filter | only matching derived reviewState returned | API integration | HTTP GET with `reviewState=StartedByAnotherEmployee` | Low | Low | actual `ListEmployeeRequests_WithReviewStateFilter_ReturnsMatchingRows` |
| Invalid status filter | validation problem returned | API integration | HTTP GET invalid `status` | Medium if only status checked; field/code check could strengthen | Low | actual `ListEmployeeRequests_WithInvalidStatusFilter_ReturnsValidationProblem` |
| Invalid reviewState filter | validation problem returned | API integration | HTTP GET invalid `reviewState` | Medium if only status checked; field/code check could strengthen | Low | actual `ListEmployeeRequests_WithInvalidReviewStateFilter_ReturnsValidationProblem` |
| Read does not mutate data | GET does not change request/review state | optional integration smoke | DB snapshot before/after GET | Medium: current tests infer from read-only path but do not explicitly prove no mutation | Low | optional future no-mutation smoke |

### Actual test evidence

```text
Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRequestListIntegrationTests.cs
```

Covers:

```text
- 401 unauthenticated;
- 403 Client account;
- empty list;
- compact rows with review states;
- status filter;
- reviewState filter;
- invalid status filter;
- invalid reviewState filter.
```

### Follow-up test improvements

```text
- assert validation ProblemDetails field names/codes for invalid filters;
- add cheap no-mutation smoke only if needed;
- add explicit ordering test if CreatedAt/Id order becomes important UI behavior.
```

---

## 13. OpenAPI / Generated Artifacts

Current endpoint is already represented in generated client types in the implementation evidence.

If API shape changes later, regenerate through tools:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
```

Generated artifacts must come from repo commands, not manual edits.

This docs-only archive does not include generated artifacts.

---

## 14. Implementation Checklist / Current Evidence Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[x] endpoint `GET /api/employee/requests` exists in current implementation evidence
[x] Employee role auth exists
[x] status query param exists
[x] reviewState query param exists
[x] EmployeeRequestListQueryDtoValidator validates filters
[x] EmployeeRequestListQuery exists
[x] EmployeeRequestListHandler uses Dapper projection
[x] EmployeeRequestListResponseDto exists
[x] EmployeeRequestListItemDto exists
[x] EmployeeRequestReviewState enum exists
[x] compact row fields are projected
[x] current employee id is used for relative review-state derivation
[x] status filter is applied
[x] reviewState filter is applied
[x] integration tests exist for access/filter/projection behavior
[ ] tests were not executed in this docs-only archive
[ ] source registry / slice derivation map rows remain pending
```

---

## 15. Guardrail Summary

```text
This is a server read/list slice.
Do not mutate request/review state here.
Do not start review here.
Do not approve review here.
Do not reject review here.
Do not create AgreementProposalExchange here.
Use Employee role auth.
Use current employee id only for relative review-state derivation in this read model.
Do not claim department/assignment queue filtering first pass.
Validate only query shape in FluentValidation.
Keep command protection in command slices/domain/application.
Use paired client sidecar for visual dashboard, filters and row action hosting.
```
