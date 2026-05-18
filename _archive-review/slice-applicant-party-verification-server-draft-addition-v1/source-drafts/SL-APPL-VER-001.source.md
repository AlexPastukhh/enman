Ниже обновлённый server draft в стиле текущих server slice drafts.

---

# SL-APPL-VER-001 — Run Mock ApplicantParty Verification From Employee Request Review

Status: planned server draft / deferred verification extension / mock first pass
Package: `[L2] Employee Request Review` + `[L1] ApplicantParty`
Slice type: Employee backend/API command slice
Primary purpose: Employee runs deterministic mock verification for the `ApplicantParty` linked to a request; successful mock result marks `ApplicantParty` as `Verified`.

Depends on:

* `SL-EMP-REQ-001 — Employee Request List Read`
* `SL-EMP-REQ-002 — Employee Request Details Read`
* `SL-EMP-REQ-003 — Start Request Review`
* `SL-EMP-REQ-004 — Approve Request Review`
* `ApplicantParty` aggregate
* Employee auth/session
* CSRF / unsafe command protection

Implementation direction:

```text
Employee request/review context
  -> resolve request by requestId
  -> resolve ApplicantPartyId from request.ApplicantPartyId
  -> load ApplicantParty
  -> run deterministic mock verification service
  -> if mock result is Passed:
       applicantParty.MarkVerified()
  -> persist ApplicantParty verification state
  -> return verification result DTO
```

Current-domain note:

```text
Current ApplicantParty verification domain is simple:

ApplicantParty.VerificationStatus
ApplicantParty.MarkVerified()
ApplicantParty.CanMarkVerified()
ApplicantPartyVerificationStatus.Unverified
ApplicantPartyVerificationStatus.Verified

Therefore this first-pass mock slice should not add Failed / Unavailable persisted states.
```

---

## 0. Scenario Sources

Business scenario / extension source:

```text
Deferred / legacy Client Data Verification scenario notes.

Important:
Current SC-14 in the active Agreement Exchange planning area may mean Agreement Documents.
Do not accidentally treat Agreement Documents SC-14 as Client Data Verification source.
```

Related scenario context:

```text
SC-07A — Employee Request Details
SC-07B — Employee Request Review
```

Suggested future source-sync name:

```text
SC-REQ-VER — ApplicantParty Verification During Employee Request Review
```

Behavior items:

```text
VER-CMD-START-001 — Employee starts ApplicantParty verification in request/review context.
VER-UCQ-001 — Verification is not triggered by standalone ApplicantParty create/edit.
VER-STATE-001 — ApplicantParty owns persisted verification state.
VER-READ-001 — Employee dashboard/details/review surfaces show current ApplicantParty verification state.
```

Source note:

```text
Scenario text is source of truth for user/business flow.
ApplicantParty domain owns verification state.
Employee request/review context owns the command entry point.
Read models expose the result to dashboard/details/review UI.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-07A: current Employee request details scenario
SC-07B: current Employee request review scenario
Client data verification extension source: pending canonical source-sync
```

Domain baseline:

```text
ApplicantParty aggregate owns verification state.

Current persisted state model supports:
  Unverified
  Verified
```

Slice derivation map:

```text
pending / add row for SL-APPL-VER-001 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior                     | Source version            | Domain disposition                             | This slice responsibility                                          | Notes                                          |
| -------------------------------------------------------- | ------------------------- | ---------------------------------------------- | ------------------------------------------------------------------ | ---------------------------------------------- |
| Employee starts verification from request/review context | pending source-sync       | not domain-only                                | expose Employee command by `requestId`                             | client does not send applicantPartyId          |
| Server resolves ApplicantParty through request           | SC-07A/SC-07B context     | application/read responsibility                | load request, read `request.ApplicantPartyId`, load ApplicantParty | prevents arbitrary ApplicantParty verification |
| Mock verifier returns result                             | extension behavior        | application service responsibility             | call deterministic mock service                                    | no random result                               |
| ApplicantParty verification state changes                | ApplicantParty domain     | domain-owned                                   | call `ApplicantParty.MarkVerified()` after successful mock result  | no manual status set in handler                |
| Standalone ApplicantParty edit does not verify           | extension behavior        | existing ApplicantParty flows remain unchanged | do not hook create/edit flows                                      | request/review context only                    |
| Dashboard/details/review show status                     | read model responsibility | not command-owned                              | follow-up read-model extension                                     | can be separate slice if needed                |
| ApproveReview is not gated first pass                    | review command behavior   | not this slice                                 | do not change approve/reject behavior                              | optional later slice                           |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
planned / not implemented as a dedicated command slice
```

Current implementation evidence:

```text
domain:
  ApplicantParty has VerificationStatus and MarkVerified direction.

server:
  Employee request list/details surfaces exist and can host future verification state in read DTOs.
  Employee request commands currently use command result/status enum style.

client:
  Employee dashboard/details/review surfaces exist and can host future verification panel/action.
```

Known drift / limitation:

```text
Current ApplicantParty enum has only Unverified and Verified.

This mini slice should return:
  mockResult = Passed

and persist:
  ApplicantPartyVerificationStatus.Verified

If product needs Failed / Unavailable / History / CheckedAt / CheckedByEmployeeId,
that is a separate domain/read-model extension.
```

Last sync note:

```text
Server draft only.
Client panel and read-model wiring are follow-up slices.
```

---

## 1. Slice Overview

Target behavior:

```text
Employee opens request dashboard/details/review area.

System shows current ApplicantParty verification state:
  not required / unverified / verified
depending on request/read-model rules.

If verification is required and can be run, Employee clicks "Run verification".

System resolves the request by requestId.

System reads request.ApplicantPartyId.

System loads ApplicantParty by ApplicantPartyId.

System runs deterministic mock verification.

Mock result is Passed.

System calls ApplicantParty.MarkVerified().

ApplicantParty verification state becomes Verified.

System returns compact verification result.

Request review state is not changed.

Request is not approved or rejected.

Agreement exchange is not created.
```

This is an Employee request-context command around the `ApplicantParty` aggregate.

The command is intentionally not a standalone ApplicantParty action.

---

## 2. Scope

This slice owns:

```text
- Employee-only command endpoint;
- CSRF protection for unsafe command;
- route by requestId;
- current Employee resolved from session;
- current temporary Employee visibility policy;
- request loading by requestId;
- ApplicantPartyId resolution from request.ApplicantPartyId;
- ApplicantParty loading by ApplicantPartyId;
- deterministic mock verification service;
- successful verification result mapping;
- calling ApplicantParty.MarkVerified();
- persisting ApplicantParty verification state;
- returning compact verification result DTO;
- API integration test plan with no-mutation checks for request/review state.
```

Endpoint:

```http
POST /api/employee/requests/{requestId}/applicant-party/verification/run
```

Success:

```http
200 OK
```

with:

```text
RunApplicantPartyVerificationResponseDto
```

Why `200 OK` instead of `204 No Content`:

```text
This command returns immediate mock verification feedback for the UI button.

This does not change the existing 204 No Content convention for review commands.
```

---

## 3. Out of Scope

| Out of scope                                         | Owner / destination                                          |
| ---------------------------------------------------- | ------------------------------------------------------------ |
| Real external verification provider                  | future integration slice                                     |
| Random mock result                                   | explicitly not allowed                                       |
| Failed / Unavailable persisted ApplicantParty states | future ApplicantParty domain extension                       |
| Verification history                                 | future audit/history slice                                   |
| Blocking ApproveReview until verification passed     | future review guard slice                                    |
| Changing ApproveReview behavior                      | `SL-EMP-REQ-004` or follow-up guard                          |
| Changing RejectReview behavior                       | `SL-EMP-REQ-005` or follow-up guard                          |
| Standalone ApplicantParty verification page          | not first pass                                               |
| ApplicantParty create/edit auto-verification         | explicitly out of scope                                      |
| Client panel/button UI                               | client sidecar follow-up                                     |
| Dashboard/details read DTO extension                 | follow-up read-model slice or paired client/server extension |
| AgreementProposalExchange creation                   | agreement exchange slices                                    |
| Manual status set in application handler             | domain owns state change                                     |

Important boundary:

```text
This command verifies ApplicantParty data.

It does not decide the request review.

It does not approve the request.

It does not reject the request.

It does not start agreement exchange.
```

---

## 4. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns Employee request dashboard/list read.
  Later should expose compact ApplicantParty verification state if needed.

SL-EMP-REQ-002
  Owns Employee request details read.
  Later should expose detailed ApplicantParty verification state if needed.

SL-EMP-REQ-003
  Owns StartReview.
  Verification may be shown during review but does not start review.

SL-EMP-REQ-004
  Owns ApproveReview.
  This slice does not change approval guard rules.

SL-EMP-REQ-005
  Owns RejectReview.
  This slice does not change rejection behavior.

ApplicantParty domain
  Owns verification status and MarkVerified behavior.

Mock verification service
  Owns deterministic first-pass mock result only.
```

---

## 5. Scenario Flow

```text
Employee opens request dashboard/details/review
        ↓
System shows request and ApplicantParty summary
        ↓
System shows verification state:
  not required / unverified / verified
        ↓
Employee clicks "Run verification"
        ↓
System resolves request by requestId
        ↓
System reads request.ApplicantPartyId
        ↓
System loads ApplicantParty
        ↓
System runs mock verification service
        ↓
Mock result = Passed
        ↓
System calls ApplicantParty.MarkVerified()
        ↓
System persists ApplicantParty verification state
        ↓
System returns verification result
        ↓
Employee sees verified state after response/refetch
```

Scenario flow table:

| Step | Actor / System layer | Behavior                                                 | Status                |
| ---- | -------------------- | -------------------------------------------------------- | --------------------- |
| F01  | Employee             | Opens request dashboard/details/review surface.          | target                |
| F02  | System               | Shows ApplicantParty verification state for the request. | target/follow-up read |
| F03  | Employee             | Starts verification from request context.                | target                |
| F04  | System               | Loads request by requestId.                              | target                |
| F05  | System               | Reads `request.ApplicantPartyId`.                        | target                |
| F06  | System               | Loads ApplicantParty by ApplicantPartyId.                | target                |
| F07  | System               | Runs deterministic mock verification.                    | target                |
| F08  | Domain               | ApplicantParty is marked verified.                       | target                |
| F09  | System               | Request/review state remains unchanged.                  | target                |
| F10  | System/UI            | Verification result becomes visible.                     | target/follow-up read |

---

## 6. Implementation Flow

```text
[HTTP POST]
POST /api/employee/requests/{requestId}/applicant-party/verification/run
        ↓
[CSRF boundary]
validate unsafe request protection
        ↓
[Auth boundary]
require authenticated Employee session
        ↓
[Session context]
read current employee account id from app cookie identity
        ↓
[Route binding]
requestId is positive long
        ↓
[Command handler]
load Employee
verify Employee active/capable if current policy requires it
load request by requestId
apply current temporary employee visibility policy
read request.ApplicantPartyId
load ApplicantParty by ApplicantPartyId
        ↓
[Mock verification service]
VerifyAsync(applicantParty)
        ↓
[Domain]
if mock result passed:
  applicantParty.MarkVerified()
        ↓
[Persistence]
save ApplicantParty verification state
        ↓
[Response]
200 OK RunApplicantPartyVerificationResponseDto
```

Implementation flow table:

| Step | Layer                     | Responsibility                                                |
| ---- | ------------------------- | ------------------------------------------------------------- |
| I01  | Route / Controller        | Exposes Employee verification command endpoint.               |
| I02  | CSRF boundary             | Protects unsafe POST command.                                 |
| I03  | Auth boundary             | Allows Employee only.                                         |
| I04  | Session context           | Resolves current Employee id from claims.                     |
| I05  | Request loading           | Loads request by requestId.                                   |
| I06  | Visibility/context        | Applies current temporary Employee request visibility policy. |
| I07  | ApplicantParty resolution | Reads `request.ApplicantPartyId`.                             |
| I08  | ApplicantParty loading    | Loads ApplicantParty by ApplicantPartyId.                     |
| I09  | Mock service              | Returns deterministic Passed result.                          |
| I10  | Domain                    | Calls `ApplicantParty.MarkVerified()`.                        |
| I11  | Persistence               | Saves ApplicantParty verification state.                      |
| I12  | API response              | Returns compact result DTO.                                   |

Guardrail:

```text
Application layer must not set ApplicantParty.VerificationStatus directly.

Use domain method:

applicantParty.MarkVerified()
```

---

## 7. API Contract

Endpoint:

```http
POST /api/employee/requests/{requestId}/applicant-party/verification/run
```

Controller placement direction:

```text
EmployeeRequestsController

Route base:
  api/employee/requests

Action route:
  {requestId:long:min(1)}/applicant-party/verification/run
```

Auth:

```csharp
[Authorize(Roles = "Employee")]
```

Request body:

```text
none first pass
```

Success:

```http
200 OK
```

Response direction:

```csharp
public sealed record RunApplicantPartyVerificationResponseDto(
    long RequestId,
    long ApplicantPartyId,
    string VerificationStatus,
    string MockResult,
    string? Message);
```

First-pass response:

```json
{
  "requestId": 123,
  "applicantPartyId": 456,
  "verificationStatus": "Verified",
  "mockResult": "Passed",
  "message": "Mock verification passed."
}
```

Failure categories:

```text
401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Employee
  Employee missing/inactive/not allowed by current policy

404 NotFound
  request does not exist
  request is not employee-visible under current policy

422 UnprocessableEntity
  request has no ApplicantPartyId / impossible state
  ApplicantParty cannot be loaded
  ApplicantParty cannot be marked verified
  mock provider returns non-pass result if later configured

500 InternalServerError
  unexpected server failure
```

No request body is accepted first pass.

Client must not send:

```text
applicantPartyId
employeeId
target verification status
mock result
```

---

## 8. Questions / Decisions

| ID                     | Status   | Question                                                  | Decision / current direction                              | Impact                                          |
| ---------------------- | -------- | --------------------------------------------------------- | --------------------------------------------------------- | ----------------------------------------------- |
| `SL-APPL-VER-001-Q001` | accepted | Does ApplicantParty own verification state?               | Yes.                                                      | Command mutates ApplicantParty aggregate.       |
| `SL-APPL-VER-001-Q002` | accepted | Should route use applicantPartyId?                        | No. Use requestId and resolve ApplicantParty server-side. | Prevents arbitrary ApplicantParty verification. |
| `SL-APPL-VER-001-Q003` | accepted | Is mock result random?                                    | No. Deterministic Passed first pass.                      | Stable tests and UX.                            |
| `SL-APPL-VER-001-Q004` | accepted | Does this command approve/reject request?                 | No.                                                       | Review commands unchanged.                      |
| `SL-APPL-VER-001-Q005` | accepted | Does standalone ApplicantParty edit trigger verification? | No.                                                       | Verification is request/review action.          |
| `SL-APPL-VER-001-Q006` | accepted | Add Failed/Unavailable enum states now?                   | No. Current mini slice uses existing Verified state.      | Avoids domain expansion.                        |
| `SL-APPL-VER-001-Q007` | accepted | Should response return result?                            | Yes, 200 with compact result.                             | Button can show result immediately.             |
| `SL-APPL-VER-001-Q008` | accepted | Use command status enum?                                  | Yes, align with current Employee command style.           | Handler/controller switch stays consistent.     |
| `SL-APPL-VER-001-Q009` | future   | Should ApproveReview require Verified?                    | Separate later slice if needed.                           | Do not change approve behavior here.            |
| `SL-APPL-VER-001-Q010` | future   | Should verification history be stored?                    | Future audit/history slice.                               | Not first pass.                                 |

---

## 9. Mock Verification Service

Interface direction:

```csharp
public interface IApplicantPartyMockVerificationService
{
    Task<ApplicantPartyMockVerificationResult> VerifyAsync(
        ApplicantParty applicantParty,
        CancellationToken cancellationToken);
}
```

Result direction:

```csharp
public sealed record ApplicantPartyMockVerificationResult(
    bool IsPassed,
    string Result,
    string? Message);
```

First-pass implementation:

```text
MockApplicantPartyVerificationService
```

Behavior:

```text
Deterministic.

Default:
  IsPassed = true
  Result = "Passed"
  Message = "Mock verification passed."
```

Allowed first-pass validation:

```text
If ApplicantParty lacks required minimum data already enforced by domain CanMarkVerified(),
domain method can return failure.

The mock service should not create random failed results.
```

Rule:

```text
Mock service returns result only.

Mock service does not mutate ApplicantParty.

Application handler calls domain method after successful mock result.
```

---

## 10. Domain / Persistence Requirement

Domain owner:

```text
ApplicantParty
```

Existing/current direction:

```csharp
ApplicantPartyVerificationStatus VerificationStatus
ApplicantParty.MarkVerified()
ApplicantParty.CanMarkVerified()
```

Success state:

```text
ApplicantParty.VerificationStatus = Verified
```

Do not add in this mini slice:

```text
ApplicantPartyVerificationStatus.Failed
ApplicantPartyVerificationStatus.Unavailable
VerificationCheckedAt
VerificationCheckedByEmployeeId
VerificationHistory
VerificationResult entity
ExternalProviderResponse
```

Those can be added later only through a dedicated domain-extension slice.

Persistence:

```text
Persist existing ApplicantParty verification status update.

No request status mutation.

No review state mutation.

No agreement exchange mutation.
```

---

## 11. Application / Handler Direction

Command:

```csharp
public sealed record RunApplicantPartyVerificationFromRequestCommand(
    long EmployeeId,
    long RequestId)
    : IRequest<RunApplicantPartyVerificationFromRequestCommandResult>;
```

Result:

```csharp
public sealed record RunApplicantPartyVerificationFromRequestCommandResult(
    RunApplicantPartyVerificationFromRequestCommandStatus Status,
    RunApplicantPartyVerificationResponse? Response,
    IReadOnlyList<Error> Errors)
{
    public static RunApplicantPartyVerificationFromRequestCommandResult Verified(
        RunApplicantPartyVerificationResponse response) =>
        new(RunApplicantPartyVerificationFromRequestCommandStatus.Verified, response, []);

    public static RunApplicantPartyVerificationFromRequestCommandResult NotFound() =>
        new(RunApplicantPartyVerificationFromRequestCommandStatus.NotFound, null, []);

    public static RunApplicantPartyVerificationFromRequestCommandResult Forbidden() =>
        new(RunApplicantPartyVerificationFromRequestCommandStatus.Forbidden, null, []);

    public static RunApplicantPartyVerificationFromRequestCommandResult Invalid(
        IReadOnlyList<Error> errors) =>
        new(RunApplicantPartyVerificationFromRequestCommandStatus.Invalid, null, errors);
}
```

Status enum:

```csharp
public enum RunApplicantPartyVerificationFromRequestCommandStatus
{
    Verified = 1,
    NotFound = 2,
    Forbidden = 3,
    Invalid = 4
}
```

Response model:

```csharp
public sealed record RunApplicantPartyVerificationResponse(
    long RequestId,
    long ApplicantPartyId,
    string VerificationStatus,
    string MockResult,
    string? Message);
```

Handler direction:

```text
1. Resolve Employee id from session.
2. Load Employee.
3. If Employee missing/inactive/not allowed, return Forbidden.
4. Load request by requestId.
5. If request is missing, return NotFound.
6. Apply current temporary Employee visibility policy:
   any active Employee can review every L1 request first pass.
7. Read request.ApplicantPartyId.
8. Load ApplicantParty by ApplicantPartyId.
9. If ApplicantParty missing, return Invalid or NotFound according to project error mapping.
10. Run IApplicantPartyMockVerificationService.VerifyAsync(applicantParty).
11. If mock result is not passed, return Invalid and do not mutate.
12. Call applicantParty.MarkVerified().
13. If domain returns failure, return Invalid and do not save.
14. SaveChanges.
15. Return Verified result with compact response.
```

---

## 12. Controller Direction

Controller switch:

```csharp
return result.Status switch
{
    RunApplicantPartyVerificationFromRequestCommandStatus.Verified =>
        Ok(ToDto(result.Response!)),

    RunApplicantPartyVerificationFromRequestCommandStatus.NotFound =>
        NotFound(),

    RunApplicantPartyVerificationFromRequestCommandStatus.Forbidden =>
        Forbid(),

    RunApplicantPartyVerificationFromRequestCommandStatus.Invalid =>
        ProblemDetailsFromValidation(result.Errors),

    _ =>
        ProblemDetailsFromInternalServerError(Errors.General.InternalServerError)
};
```

Controller must resolve Employee id from session:

```text
Do not accept EmployeeId in route/query/body.
```

---

## 13. Validation / FluentValidation

Route validation:

```text
requestId must be positive.
```

Accepted implementation:

```text
route constraint {requestId:long:min(1)}
```

No request body validator first pass.

FluentValidation does not own:

```text
- request exists;
- request is employee-visible;
- request has ApplicantPartyId;
- ApplicantParty can be loaded;
- ApplicantParty can be verified;
- ApplicantParty verification lifecycle;
- DB reads;
- domain mutation.
```

Those belong to application/domain.

---

## 14. Security / Protection

Security layers:

```text
1. Auth role:
   endpoint requires Employee.

2. Session identity:
   employee id comes from authenticated session, not request body.

3. Request context:
   command route uses requestId.

4. ApplicantParty resolution:
   server resolves ApplicantParty through request.ApplicantPartyId.

5. Domain guard:
   ApplicantParty domain decides whether MarkVerified is allowed.
```

Important:

```text
UI button visibility is not authorization.

Even if UI hides the verification button, server must enforce:
- Employee role;
- request context;
- employee-visible request;
- linked ApplicantParty resolution;
- ApplicantParty domain preconditions.
```

---

## 15. Behavior Coverage

| Source / draft behavior                                             | Status                     | Covered by this slice                            |
| ------------------------------------------------------------------- | -------------------------- | ------------------------------------------------ |
| Employee can start ApplicantParty verification from request context | covered                    | Employee POST endpoint by requestId.             |
| Client cannot start verification                                    | covered                    | Employee-only auth.                              |
| ApplicantParty id is not accepted from client                       | covered                    | server resolves through request.                 |
| Mock service returns deterministic result                           | covered                    | mock provider returns Passed.                    |
| ApplicantParty becomes Verified                                     | covered                    | `ApplicantParty.MarkVerified()` and persistence. |
| Standalone ApplicantParty edit does not verify                      | supported / out of command | no hooks in ApplicantParty create/edit.          |
| Request status is not changed                                       | covered                    | no request mutation.                             |
| Review state is not changed                                         | covered                    | no review mutation.                              |
| ApproveReview is not gated                                          | out of scope               | later optional slice.                            |
| Dashboard/details show verification state                           | follow-up                  | read-model/client sidecar extension.             |

---

## 16. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior                          | Server/system outcome                        | Test layer                     | Implementation mechanism                           | Escape risk                                | Refactor risk | Planned / actual test                                                                               |
| ------------------------------------------------- | -------------------------------------------- | ------------------------------ | -------------------------------------------------- | ------------------------------------------ | ------------- | --------------------------------------------------------------------------------------------------- |
| Employee starts verification from request context | Employee POST verifies linked ApplicantParty | API integration + DB assertion | employee auth, request fixture, HTTP POST, DB read | Low if ApplicantParty state asserted       | Low           | `RunApplicantPartyVerification_WhenEmployeeAndValidRequest_ReturnsOkAndMarksApplicantPartyVerified` |
| Client cannot verify                              | Client role receives 403                     | API integration                | client auth, HTTP POST                             | Low                                        | Low           | `RunApplicantPartyVerification_WhenClient_ReturnsForbidden`                                         |
| Missing request does not mutate                   | missing request returns 404                  | API integration                | HTTP POST unknown requestId                        | Low                                        | Low           | `RunApplicantPartyVerification_WhenRequestMissing_ReturnsNotFound`                                  |
| Client cannot choose ApplicantParty               | route/body has no applicantPartyId           | API contract/API test          | endpoint contract and bodyless request             | Medium if hidden alternate endpoint exists | Low           | endpoint contract + no standalone endpoint check                                                    |
| ApplicantParty owns state                         | ApplicantParty status becomes Verified       | API integration + DB assertion | DB read after command                              | Low                                        | Low           | success test                                                                                        |
| Request is not approved/rejected                  | request status/review state unchanged        | API integration + DB snapshot  | DB snapshot before/after                           | Low                                        | Low/Medium    | `RunApplicantPartyVerification_DoesNotChangeRequestStatusOrReviewState`                             |
| Standalone ApplicantParty save does not verify    | create/edit flow does not set Verified       | existing/follow-up API test    | ApplicantParty create/edit endpoint                | Medium                                     | Low           | `CreateApplicantParty_DoesNotMarkApplicantPartyVerified`                                            |
| CSRF protects command                             | missing CSRF rejected                        | API integration smoke          | POST without token                                 | Low                                        | Low           | command-family CSRF smoke if needed                                                                 |

### Success test

```text
Given:
- Employee is authenticated.
- Request exists and is employee-visible.
- Request has ApplicantPartyId.
- ApplicantParty exists.
- ApplicantParty.VerificationStatus = Unverified.

When:
- Employee POSTs /api/employee/requests/{requestId}/applicant-party/verification/run.

Expect:
- 200 OK.
- response.RequestId = requestId.
- response.ApplicantPartyId = linked ApplicantParty id.
- response.VerificationStatus = "Verified".
- response.MockResult = "Passed".
- ApplicantParty.VerificationStatus persisted as Verified.
- Request status unchanged.
- Review state unchanged.
```

### No-mutation / failure tests

```text
- unauthenticated -> 401;
- Client role -> 403;
- missing request -> 404;
- missing ApplicantParty -> 422 / Invalid or NotFound depending project mapping;
- ApplicantParty cannot be verified -> 422;
- failed command does not mark ApplicantParty Verified;
- failed command does not change request status;
- failed command does not start review;
- failed command does not approve/reject review;
- failed command does not create AgreementProposalExchange.
```

---

## 17. OpenAPI / Generated Artifacts

Expected OpenAPI addition:

```text
POST /api/employee/requests/{requestId}/applicant-party/verification/run

200 RunApplicantPartyVerificationResponseDto
401
403
404
422
500
```

Generated artifacts workflow:

```text
Run existing repo OpenAPI/type generation commands only when implementing API.

Do not manually edit generated artifacts.
```

---

## 18. Implementation Checklist

```text
[ ] confirm request.ApplicantPartyId is available
[ ] add IApplicantPartyMockVerificationService
[ ] add deterministic MockApplicantPartyVerificationService
[ ] add RunApplicantPartyVerificationResponseDto
[ ] add RunApplicantPartyVerificationFromRequestCommand
[ ] add RunApplicantPartyVerificationFromRequestCommandResult
[ ] add RunApplicantPartyVerificationFromRequestCommandStatus
[ ] add command handler
[ ] add Employee endpoint:
    POST /api/employee/requests/{requestId}/applicant-party/verification/run
[ ] require Employee role
[ ] require CSRF
[ ] do not accept applicantPartyId in body
[ ] do not accept employeeId in body
[ ] resolve Employee id from session
[ ] load Employee
[ ] load request by requestId
[ ] apply current temporary Employee visibility policy
[ ] read request.ApplicantPartyId
[ ] load ApplicantParty by ApplicantPartyId
[ ] run mock verification service
[ ] call applicantParty.MarkVerified()
[ ] persist ApplicantParty verification state
[ ] return 200 with verification result
[ ] add API integration success test
[ ] add auth/role tests
[ ] add missing request test
[ ] add no-mutation tests for request/review state
[ ] add CSRF smoke if needed
[ ] regenerate OpenAPI/types only through repo commands
[ ] do not change ApproveReview
[ ] do not change RejectReview
[ ] do not implement client UI in this slice
```

---

## 19. Guardrail Summary

```text
This is a mock ApplicantParty verification command.

ApplicantParty owns verification state.

Employee request/review context owns the command entry point.

Use requestId route.

Do not accept applicantPartyId from client.

Do not accept employeeId from client.

Server resolves ApplicantParty through request.ApplicantPartyId.

Mock result is deterministic, not random.

First pass result is Passed.

First pass persists existing Verified state only.

Do not add Failed / Unavailable domain states in this mini slice.

Do not add verification history in this mini slice.

Do not trigger verification from ApplicantParty create/edit.

Do not change request status.

Do not change review state.

Do not block ApproveReview in this slice.

Do not create AgreementProposalExchange.

Do not manually set ApplicantParty.VerificationStatus in application layer.

Use ApplicantParty.MarkVerified().

Use command result/status enum style to match current Employee command slices.
```
