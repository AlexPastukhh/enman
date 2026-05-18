# SL-EMP-REQ-002 — Employee Request Details Read

Status: implemented slice draft refactor / implementation evidence inspected read-only  
Package: `[Employee] [Requests]`  
Slice type: backend/API read slice  
Primary purpose: Employee reads one employee-visible request details payload by request id  
Parent slice: `SL-EMP-REQ-001 — Employee Request List Read`  
Implementation direction: Dapper/read projection, not aggregate repository / EF DTO shaping

Depends on:

* `SL-EMP-REQ-001 — Employee Request List Read`
* Employee auth/session
* `Employee : Account` target identity direction
* request-owned Review persistence
* applicant party persistence
* Dapper / SQL read projection infrastructure

Implementation evidence note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was inspected read-only from the uploaded repository snapshot to avoid stale route/DTO/test names.

Runtime code, tests and generated artifacts are not changed by this archive.
Client runtime UI/page flow is not changed by this archive.
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
SC-13D — Employee Agreement Proposal Create / Send Version, only after approved request details host Start Agreement Exchange action
```

UI scenario:

```text
missing / pending dedicated UI source for Employee request details page;
current client sidecar documents the implemented page behavior.
```

Cross-cutting behavior:

```text
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for paired client sidecar
CC-SEC-CSRF-001 — not directly applicable to this GET read slice; applies to hosted command sidecars only
```

Data source:

```text
request details payload from request, applicant party and request review persistence;
current implementation projects directly with Dapper.
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior names and current test names until source-sync files are completed.
```

Concern umbrella:

```text
none for this server read slice.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-07A: pending / v000 if source registry is applied
SC-06: pending / v000 if source registry is applied
SC-07B: pending / v000 if source registry is applied
```

Domain baseline:

```text
DOM-v001 if source-sync/domain registry is applied;
otherwise pending domain baseline.

Current target concepts used by the implemented read projection:
- Employee derives from Account for session identity;
- Request owns Review state;
- ApplicantParty provides applicant summary/contact;
- ConnectionRequest stores request status, details, object address and created timestamp.
```

Slice derivation map:

```text
pending / add row for SL-EMP-REQ-002 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain / persistence disposition | This slice responsibility | Current implementation evidence | Notes |
|---|---|---|---|---|---|
| Employee opens one request details by id | SC-07A pending | request exists in `L1ClientRequests` | expose `GET /api/employee/requests/{requestId}` | `EmployeeRequestsController.GetRequestDetails` | Employee role only |
| Signed-out user cannot read details | SC-07A pending | auth boundary | return 401 before query | `GetEmployeeRequestDetails_WithoutAuth_ReturnsUnauthorized` | public boundary proof |
| Client account cannot read Employee details | SC-07A pending | auth/role boundary | return 403 | `GetEmployeeRequestDetails_WithClientAccount_ReturnsForbidden` | Employee-only endpoint |
| Missing request returns not found | SC-07A pending | query returns no row | return 404 | `GetEmployeeRequestDetails_ForMissingRequest_ReturnsNotFound` | missing/not-visible behavior currently same shape |
| Details payload includes request status/type/details/createdAt | SC-07A pending | request read projection | project fields to `EmployeeRequestDetailsDto` | `GetEmployeeRequestDetails_ReturnsDetailsPayloadWithNotStartedReview` | response DTO exists |
| Details payload includes applicant summary/contact | SC-07A pending | ApplicantParty read projection | project applicant id/type/display/email/phone | same integration test | display name formatted from name parts |
| Details payload includes object address | SC-07A pending | owned address columns | format address string | same integration test | no nested address DTO first pass |
| Review not started is visible | SC-07A/07B pending | no review row or non-review status | derive `NotStarted` | same integration test | no mutation |
| Review started by current Employee is visible | SC-07A/07B pending | review row has `Started` and current employee id | derive `StartedByCurrentEmployee` | `GetEmployeeRequestDetails_ReturnsStartedReviewStateForCurrentAndAnotherEmployee` | uses session Employee id |
| Review started by another Employee is visible | SC-07A/07B pending | review row has `Started` and different employee id | derive `StartedByAnotherEmployee` | same integration test | blocks current Employee actions client-side |
| Completed review state is visible | SC-07A/07B pending | request/review status is approved/rejected | derive `Approved` / `Rejected` | `GetEmployeeRequestDetails_ReturnsCompletedReviewStates` | read only |
| No review command is executed | SC-07A pending | command slices own mutations | GET details does not start/approve/reject | read endpoint and tests | command buttons are separate sidecars |
| Agreement exchange state is not returned here | SC-13D pending | agreement exchange slices own it | only request details plus review state | current DTO has no agreement exchange DTO | StartAgreementExchange UI is hosted client-side after approved state |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-current-doc-sync
```

Implemented files inspected read-only:

```text
server:
  EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs
  EnergyManagement.Server/L1/Application/Queries/EmployeeRequestDetailsQuery.cs
  EnergyManagement.Server/L1/Application/Queries/EmployeeRequestDetailsHandler.cs
  EnergyManagement.Server/L1/Api/L1Dtos.cs

client:
  paired client draft refactored in this archive:
    planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md

server tests:
  Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRequestDetailsIntegrationTests.cs

client tests / companion evidence:
  energymanagement.client/src/entities/employee-request/api/getEmployeeRequestDetails.test.ts
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDetailsView.test.tsx
  energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
```

Checked against:

```text
source versions:
  pending source-sync registry

domain baseline:
  pending / DOM-v001 if source-sync files are applied

slice derivation map version:
  pending

current implementation snapshot:
  uploaded repo zip inspected read-only
```

Known drift corrected by this refactor:

```text
- old draft said not implemented, but current code implements endpoint/query/DTO/tests;
- old draft lacked Scenario Sources;
- old draft lacked Source / Domain / Slice Coverage Snapshot;
- old draft lacked Implementation Sync Status;
- old draft had a test plan but not current Behavior-to-Test Trace with actual test names;
- old draft did not mention current StartAgreementExchange action hosting on the implemented details page.
```

Known remaining drift / follow-up:

```text
source:
  - stable source behavior item IDs and source registry versions are still pending.

implementation:
  - current server details query returns any request by id for an authenticated Employee first pass;
  - if future employee assignment/visibility rules are introduced, this read projection must change.

client:
  - details page currently hosts Start/Approve/Reject and StartAgreementExchange action slots/forms;
  - command behavior belongs to command sidecars, not this read slice.

tests:
  - server integration tests cover auth, forbidden, not found, payload and review states;
  - no explicit no-mutation smoke is listed in current evidence.
```

Last sync note:

```text
Docs-only refactor with read-only implementation evidence. No runtime implementation changes and no test execution in this pass.
```

---

## 1. Scope

This slice owns:

```text
- Employee request details read endpoint;
- Employee-only auth boundary;
- current Employee account id derived from session claims;
- one request details payload by requestId;
- Dapper/read projection over request, applicant and review tables;
- request type/status/details/createdAt fields;
- applicant summary/contact fields;
- formatted object address string;
- compact review state derived relative to current Employee;
- 200 OK details response;
- 401/403/404 read failure boundaries;
- API/read integration tests for auth, payload and review-state coverage.
```

Endpoint:

```http
GET /api/employee/requests/{requestId}
```

Current response DTO:

```text
EmployeeRequestDetailsDto
```

This slice does **not** own:

```text
- dashboard/list filters;
- StartReview command;
- ApproveReview command;
- RejectReview command;
- StartAgreementExchange command;
- AgreementProposalExchange read or command DTOs;
- employee assignment/queue filtering beyond current first pass;
- action command mutation behavior;
- client page-flow/redirect audit.
```

---

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Employee request list / filters | `SL-EMP-REQ-001` |
| Employee dashboard UI | `L2-EMP-DASH-001.client` |
| Start review command | `SL-EMP-REQ-003` / `L2-REVIEW-START-001.client` |
| Approve review command | `SL-EMP-REQ-004` / approve client feature sidecar |
| Reject review command | `SL-EMP-REQ-005` / reject client feature sidecar |
| Start agreement exchange | `SL-AGR-EXCH-001` / `L2-AGR-EXCH-START-001.client` |
| Agreement exchange list/details | `SL-AGR-EXCH-003/004` and client sidecars |
| Employee profile/display-name DTO | future Employee profile/read slice |
| Review history/timestamps beyond current compact state | future details extension |
| FluentValidation body/query validator | not needed for this read endpoint first pass |
| Runtime UI refactor | out of this docs-only archive |

Important boundary:

```text
This is a read/details slice.

The fact that the current client details page hosts command action slots does not move command ownership into this server read slice.
```

---

## 3. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns employee request dashboard/list read endpoint and filters.

L2-EMP-DASH-001.client
  Owns Employee request dashboard/list page.

SL-EMP-REQ-002
  Owns Employee request details endpoint/read projection.

L2-EMP-DETAILS-001.client
  Owns Employee request details page/read UI and action-slot composition.

SL-EMP-REQ-003 / 004 / 005
  Own StartReview / ApproveReview / RejectReview command behavior.

L2-REVIEW-START-001.client and approve/reject client features
  Own command buttons/forms/mutations.

SL-AGR-EXCH-001 / L2-AGR-EXCH-START-001.client
  Own Start Agreement Exchange action after approved request.

Domain / persistence
  Own Request, RequestReview, ApplicantParty, Address persisted facts used by projection.
```

---

## 4. Scenario Flow

```text
[Signed-in Employee]
opens request details from Employee dashboard
        ↓
System resolves current Employee from session
        ↓
System loads request, applicant and review data by requestId
        ↓
System derives review state relative to current Employee
        ↓
System returns details payload
        ↓
Client page renders request/applicant/review state and hosted command action slots
```

Scenario flow table:

| Step | Actor / System layer | User-visible / system responsibility |
|---|---|---|
| S01 | Employee | Opens a request details page from dashboard/details link. |
| S02 | System | Requires Employee session. |
| S03 | System | Loads one request by `requestId`. |
| S04 | System | Projects request details, applicant summary, address and review state. |
| S05 | System | Returns `EmployeeRequestDetailsDto`. |
| S06 | Client/UI | Renders details and state; command sidecars may render actions. |

Scenario meaning:

```text
Details gives an Employee enough information to understand the request and current review state.

Details read does not itself start, approve, reject or start an agreement exchange.
```

---

## 5. Implementation Flow

```text
[HTTP GET]
GET /api/employee/requests/{requestId}
        ↓
[Auth]
Employee app cookie/session required
        ↓
[Controller]
TryGetCurrentEmployeeId(out employeeId)
        ↓
[Query]
EmployeeRequestDetailsQuery(employeeId, requestId)
        ↓
[Handler / Dapper]
query L1ClientRequests + L1ApplicantParties + L1RequestReviews
        ↓
[Projection]
format applicant display name
format object address
derive reviewState from review status / request status / currentEmployeeId
        ↓
[Response]
200 OK EmployeeRequestDetailsDto
or 401 / 403 / 404 / 500
```

Implementation ownership:

```text
Controller:
  HTTP boundary, Employee auth guard, route binding, session Employee id, response mapping.

Validator:
  none for body/query first pass.

Query handler:
  Dapper read projection;
  review-state derivation;
  Maybe.None for missing row.

Domain:
  owns persisted statuses and review lifecycle semantics.
  No domain lifecycle method is called in this read slice.

Client:
  paired sidecar owns page/rendering/action-slot composition.
```

---

## 6. API Contract

### Endpoint

```http
GET /api/employee/requests/{requestId}
```

Route:

```text
requestId: long
```

Auth:

```csharp
[Authorize(Roles = "Employee")]
```

Request body:

```text
none
```

Query:

```text
none
```

Current response direction:

```csharp
public sealed record EmployeeRequestDetailsDto(
    long RequestId,
    string RequestType,
    string Status,
    EmployeeRequestApplicantSummaryDto Applicant,
    string ObjectAddress,
    string Details,
    DateTimeOffset CreatedAt,
    string ReviewState);

public sealed record EmployeeRequestApplicantSummaryDto(
    long ApplicantPartyId,
    string ApplicantPartyType,
    string DisplayName,
    string? Email,
    string? PhoneNumber);
```

Current review state values:

```text
NotStarted
StartedByCurrentEmployee
StartedByAnotherEmployee
Approved
Rejected
```

Success:

```http
200 OK
```

Failure categories:

```text
401 Unauthorized
  no authenticated session or employee id cannot be resolved

403 Forbidden
  authenticated non-Employee role

404 NotFound
  request row is missing / not returned by read projection

500 InternalServerError
  unexpected server failure
```

No `422` is expected for this read endpoint first pass because there is no body/query validator.

---

## 7. Read Model Behavior

Current SQL projection loads:

```text
request.Id
request.RequestType
request.Status
request.CreatedAt
request.Details
owned object address columns
applicant.Id
applicant.ApplicantPartyType
applicant FullName parts
applicant.Email
applicant.PhoneNumber
review.Status
review.StartedByEmployeeId
```

Applicant display name:

```text
LastName FirstName MiddleName, skipping blank parts.
```

Object address:

```text
PostalCode, Region, City, Street, House, Building, Apartment, skipping blank parts.
```

Review-state derivation:

```text
if review.Status == Started and review.StartedByEmployeeId == currentEmployeeId:
  StartedByCurrentEmployee

if review.Status == Started and review.StartedByEmployeeId != currentEmployeeId:
  StartedByAnotherEmployee

if review.Status == Approved:
  Approved

if review.Status == Rejected:
  Rejected

if no review status but request.Status == Approved:
  Approved

if no review status but request.Status == Rejected:
  Rejected

otherwise:
  NotStarted
```

Read-only rule:

```text
GET details must not create Review, change Request.Status, change Review.Status, or start agreement exchange.
```

---

## 8. Validation / ProblemDetails

No request body.

No query.

No FluentValidation validator is required first pass.

```text
- route binding handles requestId shape;
- missing request / not-returned request is query-handler/controller responsibility;
- malformed route follows ASP.NET route/model-binding behavior.
```

Do not put these into validators:

```text
- Employee exists;
- current user is Employee;
- request visibility;
- request belongs to employee-visible pool;
- review state;
- DB reads;
- transactions;
- mutations.
```

---

## 9. Security / Protection

Security/read boundaries:

```text
- endpoint requires Employee role;
- Employee id is derived from claims/session, not from route/body;
- details projection uses current Employee id only to derive reviewState;
- command permissions are still enforced by command endpoints/domain, not by this GET response alone.
```

Current first-pass visibility:

```text
All authenticated active Employee sessions can read review-relevant requests first pass.
```

Future visibility extension:

```text
department/region/assignment/personal queue filtering can narrow this projection later.
```

Guardrail:

```text
Do not expose Employee private profile data in this response.
Do not accept employeeId from client.
Do not rely on UI button availability for command security.
```

---

## 10. Behavior Coverage

| Behavior item / behavior | Server/system outcome | Current evidence | Status |
|---|---|---|---|
| Unauthenticated user cannot read details | 401 | `GetEmployeeRequestDetails_WithoutAuth_ReturnsUnauthorized` | covered |
| Client account cannot read Employee details | 403 | `GetEmployeeRequestDetails_WithClientAccount_ReturnsForbidden` | covered |
| Missing request returns not found | 404 | `GetEmployeeRequestDetails_ForMissingRequest_ReturnsNotFound` | covered |
| Employee receives details payload | DTO has id/type/status/details/createdAt/address/applicant/reviewState | `GetEmployeeRequestDetails_ReturnsDetailsPayloadWithNotStartedReview` | covered |
| Not-started review is shown | `reviewState = NotStarted` | same payload test | covered |
| Started by current Employee is shown | `reviewState = StartedByCurrentEmployee` | `GetEmployeeRequestDetails_ReturnsStartedReviewStateForCurrentAndAnotherEmployee` | covered |
| Started by another Employee is shown | `reviewState = StartedByAnotherEmployee` | same test | covered |
| Completed review states are shown | `Approved` / `Rejected` | `GetEmployeeRequestDetails_ReturnsCompletedReviewStates` | covered |
| Details read does not execute commands | no command endpoint called from GET | current endpoint shape | supported |
| List/filter behavior | out of scope | `SL-EMP-REQ-001` | out of scope |
| Start/approve/reject behavior | out of scope | command slices | out of scope |

---

## 11. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Unauthenticated user cannot read details | request is rejected with 401 | API integration | HTTP GET without auth | Low: public boundary proof | Low | `GetEmployeeRequestDetails_WithoutAuth_ReturnsUnauthorized` |
| Client account cannot read Employee details | request is rejected with 403 | API integration | client auth cookie, HTTP GET | Low: role boundary proof | Low | `GetEmployeeRequestDetails_WithClientAccount_ReturnsForbidden` |
| Missing request returns not found | missing id returns 404 | API integration | employee auth, HTTP GET missing id | Low | Low | `GetEmployeeRequestDetails_ForMissingRequest_ReturnsNotFound` |
| Details payload is returned | response includes details/applicant/address/review state | API integration | seeded request/applicant, HTTP GET, response assertions | Low if all required fields asserted | Low/Medium: DTO shape changes require test update | `GetEmployeeRequestDetails_ReturnsDetailsPayloadWithNotStartedReview` |
| Review not started is visible | response `reviewState = NotStarted` | API integration | seeded request without review | Low | Low | same payload test |
| Review started by current Employee is visible | response `StartedByCurrentEmployee` | API integration | seeded review row with current employee id | Low | Low/Medium: helper/schema changes affect setup | `GetEmployeeRequestDetails_ReturnsStartedReviewStateForCurrentAndAnotherEmployee` |
| Review started by another Employee is visible | response `StartedByAnotherEmployee` | API integration | seeded review row with other employee id | Low | Low/Medium | same test |
| Completed review state is visible | response `Approved` / `Rejected` | API integration | seeded request/review completed states | Low | Low/Medium | `GetEmployeeRequestDetails_ReturnsCompletedReviewStates` |
| GET does not mutate request/review | state remains unchanged | optional API integration + DB snapshot | HTTP GET, DB snapshot before/after | Medium if omitted | Low/Medium | optional future no-mutation smoke |

### Additional recommended checks

```text
- if future visibility rules are introduced, add not-visible request returns 404/403 according to project convention;
- if request details DTO gains actionAvailability, add focused payload and client rendering tests;
- if assignment/queue filtering is introduced, keep current all-active-Employees first-pass behavior documented as replaced/deprecated.
```

### What not to test here

```text
- StartReview / ApproveReview / RejectReview mutations;
- StartAgreementExchange mutation;
- dashboard list filters;
- React UI rendering;
- repository mock call order;
- generated TypeScript as primary behavior proof.
```

---

## 12. OpenAPI / Generated Artifacts

Current OpenAPI includes:

```text
GET /api/employee/requests/{requestId}
200 EmployeeRequestDetailsDto
401
403
404
500
```

Generated artifacts are implementation artifacts and are **not** changed by this docs-only archive.

If API contract changes later:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
npm.cmd --prefix energymanagement.client run check:api
```

---

## 13. Implementation Checklist / Current Refactor Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[x] endpoint exists: GET /api/employee/requests/{requestId}
[x] Employee role auth exists
[x] current Employee id is resolved from session/claims
[x] EmployeeRequestDetailsQuery exists
[x] EmployeeRequestDetailsHandler exists
[x] Dapper projection exists
[x] EmployeeRequestDetailsDto exists
[x] EmployeeRequestApplicantSummaryDto exists
[x] reviewState derivation exists
[x] 401/403/404 integration tests exist
[x] payload/review-state integration tests exist
[ ] no-mutation smoke exists, optional/future
[ ] source registry behavior IDs assigned, future source-sync task
[ ] future employee assignment/visibility model applied, if required
```

This archive does not modify runtime implementation.

---

## 14. Guardrail Summary

```text
This is a read/details slice.
Use GET /api/employee/requests/{requestId}.
Require Employee role.
Resolve Employee id from session, not client input.
Use Dapper/read projection for DTO shaping.
Do not load/mutate aggregate just to shape DTO.
Do not start/approve/reject review here.
Do not start agreement exchange here.
Do not add body/query validation first pass.
Do not put visibility/lifecycle into FluentValidation.
Do not add Employee profile/display-name scope here.
Do not add AgreementProposalExchange details here.
Current first-pass Employee visibility is broad; future assignment rules require a scoped follow-up.
```
