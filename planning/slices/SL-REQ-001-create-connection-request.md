# SL-REQ-001 — Create Connection Request From Current Applicant Party

Status: implemented backend/API/persistence slice  
Package: `[L1]`  
Source scenario: `SC-04 Client Request Creation`  
Slice type: backend / API / persistence slice with dependent client/UI/read/security/extension slices  
Current implementation status: implemented backend/API/persistence and integration-tested; request creation UI, My Requests read screens and broad CSRF rollout remain separate

## 1. Slice Overview

Observable behavior:

```text
Authenticated L1 client submits connection request data.
System creates a connection request for the current active individual applicant party of the authenticated account.
Created request enters InReview state.
API returns command success without a required response body.
```

Backend scope implemented by this slice:

```text
protected L1 request endpoint
-> details + object address command input
-> authenticated account id from L1 auth context
-> server-selected current active individual ApplicantParty
-> request details/address validation
-> ConnectionRequest creation
-> InReview status
-> persistence
-> HTTP success with no required response body
```

Current implementation evidence checked for this reconciliation:

```text
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Commands/L1Commands.cs
EnergyManagement.Server/L1/Application/Commands/L1CreateConnectionRequestHandler.cs
Domain.EnergyManagement/L1/Requests/ClientRequest.cs
Domain.EnergyManagement/L1/Requests/ConnectionRequest.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Tests.EnergyManagement/Domain/Requests/ConnectionRequestCreationTests.cs
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
```

Out of this backend slice:

```text
- concrete request creation page/client sidecar;
- My Requests read/list/detail screen;
- request document upload;
- notifications;
- employee review workflow;
- applicant party replacement/version management;
- broad CSRF rollout;
- client retry/token mechanics.
```

Client success handling should follow:

```text
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
```

## 2. Sources / Source Behavior Items

Scenario / planning sources:

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/api/client-server-contract-principles.md
planning/api/api-error-contract.md
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Behavior items covered by this backend slice:

```text
REQ-CMD-CREATE-001 — Create request
REQ-LC-001 — Request creation creates InReview
REQ-VI-001 — Object address value integrity
REQ-UCQ-001 — Request uses saved ApplicantData without mutating it
```

Scenario facts used by the backend slice:

```text
- Client submits request details and object address.
- Authenticated account context is server-side context.
- Client must not submit applicantPartyId/clientAccountId for the create command.
- Server selects the current active individual applicant party.
- Missing current active applicant party rejects the command and writes no request row.
- Accepted request starts InReview.
- Initial command success does not require returned request id/status/body.
```

Client/UI facts intentionally not implemented here:

```text
- request creation form/page;
- success message rendering;
- navigation to My Requests;
- My Requests route/read model;
- field-level ProblemDetails display.
```

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Authenticated L1 Client                      │
│ submits connection request data              │
│ details + object address                     │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ L1 System                                    │
│ resolves client account id from auth context │
└──────────────┬───────────────────────────────┘
               │ find current active individual ApplicantParty
               ▼
┌──────────────────────────────────────────────┐
│ Current active applicant party exists?       │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Validate details and  │   │ Validation ProblemDetails     │
│ object address        │   │ no request is created         │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           ▼                              ▼
┌──────────────────────┐        ┌──────────────────────────┐
│ Create request for    │        │ Client can keep/correct  │
│ selected party        │        │ request form context     │
└──────────┬───────────┘        └──────────────────────────┘
           │
           ▼
┌──────────────────────┐
│ Request enters        │
│ InReview state        │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Command success                              │
│ HTTP success is enough for initial flow      │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Dependent client UI                          │
│ show success message                         │
│ navigate to My Requests read context         │
└──────────────────────────────────────────────┘

Dependent / out-of-scope:
- request creation UI sidecar;
- My Requests read/list/detail slice;
- request document upload;
- notification/navigation details;
- employee review;
- CSRF broad rollout.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Submits request details and object address. | SC-04 / REQ-CMD-CREATE-001 | source behavior |
| F02 | API/Auth boundary | Derives current L1 client account id from authenticated user context. | API/security boundary | backend/API slice |
| F03 | System | Resolves current active individual applicant party for the account. | REQ-UCQ-001 / current implementation | backend/application slice |
| F04 | System | Rejects command if current active applicant party is missing. | validation/no-write behavior | backend/API slice |
| F05 | System | Validates request details and object address. | REQ-VI-001 / validation addendum | backend/domain slice |
| F06 | System | Creates `ConnectionRequest` for the selected applicant party. | REQ-CMD-CREATE-001 | backend/domain slice |
| F07 | System | Sets created request state to `InReview`. | REQ-LC-001 | backend/domain slice |
| F08 | System | Persists request with server-selected `ApplicantPartyId`. | persistence responsibility | backend/persistence slice |
| F09 | API | Returns command success without required response body. | CL-COMMAND-001 / API contract | backend/API slice |
| F10 | Client UI | Shows success message and navigates to My Requests. | CL-COMMAND-001 / UI behavior | dependent client sidecar |
| F11 | Client UI | Shows ProblemDetails validation errors and preserves input where appropriate. | API error contract / client error convention | dependent client sidecar |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ [Authorize]                                  │
│ POST /api/l1/requests                        │
│ Body: details + address                      │
│ No applicantPartyId/clientAccountId in body  │
└──────────────┬───────────────────────────────┘
               │ TryGetCurrentL1AccountId
               │ builds command with server account id
               ▼
┌──────────────────────────────────────────────┐
│ Application Handler                          │
│ L1CreateConnectionRequestHandler             │
└──────────────┬───────────────────────────────┘
               │ load current active individual
               │ applicant party for account
               ▼
┌──────────────────────────────────────────────┐
│ ApplicantParty found?                        │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Domain               │   │ API error mapping             │
│ Address.Create       │   │ validation ProblemDetails     │
│ ConnectionRequest    │   │ no request write              │
│ .Create              │   └──────────────────────────────┘
└──────────┬───────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Persistence                                  │
│ L1ClientRequests row                         │
│ selected ApplicantPartyId + InReview status  │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ returns 200 OK with no required body         │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Client convention boundary                   │
│ CL-COMMAND-001                               │
│ success -> message -> My Requests            │
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current implementation note |
|---|---|---|---|
| I01 | API Controller | Expose protected create request endpoint. | `[Authorize] POST /api/l1/requests`. |
| I02 | API/Auth boundary | Derive current client account id from L1 auth claims. | Client must not submit account identity. |
| I03 | API DTO | Receive create-request body. | `L1CreateConnectionRequestDto(details, address)`; no `applicantPartyId` or `clientAccountId`. |
| I04 | Application Handler | Resolve current active individual applicant party for account. | Uses repository current-active individual lookup. |
| I05 | Application Handler | Reject missing applicant context. | Returns `ApplicantPartyIsRequired` validation failure; no request row is created. |
| I06 | Domain/value objects | Validate object address and request details. | `Address.Create(...)`; `ConnectionRequest.Create(...)`. |
| I07 | Domain | Create connection request in `InReview`. | `ClientRequest` base initializes `Status = RequestStatus.InReview`. |
| I08 | Persistence | Store request with selected applicant party id. | `_clientRequests.Add(...)` + `L1DbContext.SaveChangesAsync(...)`. |
| I09 | API response | Return HTTP success without required response body. | Controller maps `UnitResult` success to `Ok()`. |
| I10 | Dependent client | Show success message and navigate to My Requests. | Not part of this backend archive; follows CL-COMMAND-001 when client work starts. |
| I11 | Dependent client | Parse ProblemDetails and preserve/correct input on failure. | Not part of this backend archive. |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/requests` | POST | `L1CreateConnectionRequestDto` with `details`, `address` | none required | 200, 401, 403, 422, 500 | target L1 | yes |

Contract rule:

```text
The request body must not include applicantPartyId or clientAccountId.
ClientAccountId comes from authenticated L1 context.
ApplicantPartyId is selected by the server from the current active individual applicant party.
Successful command completion does not require requestId/status/body for the initial UI flow.
```

Generated contract coverage should show:

```text
- POST /api/l1/requests request body has details + address;
- request body does not have applicantPartyId/clientAccountId;
- 200 success has no required response body;
- ProblemDetails statuses are documented for auth/validation/server failures.
```

Security note:

```text
This slice is an unsafe browser command consumer of the future/broader antiforgery concern, but this documentation archive does not implement CSRF.
Parent client/security work should use CC-CSRF-001 when concrete unsafe browser request handling is started.
```

## 8. Questions / Decisions

Open questions and unresolved future review items:

| ID | Area | Question | Current direction | Status |
|---|---|---|---|---|
| SL-REQ-Q-001 | Read model | What exact My Requests list/detail response does the client need after navigation? | Separate read/list/detail slice. | open |
| SL-REQ-Q-002 | Applicant versions | How are older applicant party versions made inactive when replacement/edit flow exists? | Keep current active lookup now; replacement/versioning later. | future review |
| SL-REQ-Q-003 | Client UI | What concrete page/form implements request creation? | Create/update client sidecar only when concrete client work starts. | open |
| SL-REQ-Q-004 | Security | When should unsafe browser commands enforce CSRF? | Use CC-CSRF-001 in concrete security/client work; do not implement here. | open |
| SL-REQ-Q-005 | Verification | Must applicant party be verified before request creation? | Current backend does not require verification; verification is a separate slice/policy. | future review |

Accepted decisions:

```text
Decision:
Request creation command treats HTTP success as enough for the initial command confirmation.

Reason:
The next expected user step is a read-context screen; the initial client flow does not require requestId/status/body from the command.

Consequence:
Client UI can show a success message and navigate to My Requests without depending on a response DTO.
```

```text
Decision:
Request creation uses the current active individual applicant party for the authenticated account.

Reason:
The client must not choose or spoof ApplicantPartyId in the command body.

Consequence:
Missing current active applicant party returns validation ProblemDetails and no request row is created.
```

```text
Decision:
A created connection request starts in InReview.

Reason:
A newly submitted client request enters the employee review queue.
```

```text
Decision:
Request creation does not mutate saved ApplicantParty data.

Reason:
The request stores selected ApplicantPartyId and request-local details/address; applicant data editing/replacement is a separate concern.
```

## 9. Behavior Coverage

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| REQ-CMD-CREATE-001 — Create request | API/application/domain flow creates request from submitted details/address and server-selected applicant party. | Visual Scenario Flow / Scenario Slice Flow / Implementation Flow | covered |
| REQ-LC-001 — Request creation creates InReview | Domain request creation initializes status to `InReview`; integration test checks persisted status. | Scenario Slice Flow / Implementation Flow / Test Plan | covered |
| REQ-VI-001 — Object address value integrity | Address value object is created before request persistence. | Implementation Flow / Test Plan | covered |
| REQ-UCQ-001 — Request uses saved/current applicant context without client spoofing | DTO omits applicantPartyId/clientAccountId; application resolves applicant party server-side. | Visual Implementation Flow / API Contract | covered |
| Missing applicant context no-write | Missing current active applicant party returns validation ProblemDetails and request count remains unchanged. | Visual Scenario Flow / Visual Implementation Flow / Test Plan | covered |
| Client success outcome | HTTP success is enough for success message and My Requests navigation. | Visual Scenario Flow / API Contract / CL-COMMAND-001 | partially covered; dependent client sidecar |
| My Requests read context | Read/list/detail destination is identified but delegated to read slice. | Questions / Dependent Slices | open |
| CSRF unsafe request protection | Identified as cross-cutting future/concrete client-security work. | API Contract security note / Questions | deferred; not implemented here |

## 10. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `L1LoginCookie_CreateConnectionRequest_Succeeds` | Authenticated L1 flow can create applicant then request; request is persisted in InReview. | API + Auth/session + Application + Persistence | implemented |
| `CreateConnectionRequest_StoresServerSelectedCurrentActiveApplicantPartyReference` | Server selects current active applicant party; persisted request uses selected ApplicantPartyId and stores details/address. | API + Application + Persistence | implemented |
| `L1Flow_PropagatesEfGeneratedIdsAcrossAggregates` | Account, applicant and request generated ids propagate across aggregate references. | API + Persistence | implemented |
| `CreateConnectionRequest_WithoutCurrentActiveApplicantParty_ReturnsValidationProblem` | Missing applicant party returns validation ProblemDetails and no request row is created. | Application + API error mapping + Persistence | implemented |
| `CreateConnectionRequest_WithEmptyDetails_ReturnsValidationProblem` | Empty request details fail validation. | API + Domain/Application | implemented |
| `Create_creates_in_review_request` | Domain creates connection request with selected applicant id and InReview status. | Domain unit | implemented |
| `Create_fails_without_details` | Domain rejects missing details. | Domain unit | implemented |
| `Create_fails_without_object_address` | Domain rejects missing object address. | Domain unit | implemented |
| `Create_guards_transient_applicant_party` | Domain guards against non-persisted applicant reference. | Domain unit | implemented |
| OpenAPI/generated type check | Endpoint schema/statuses stay aligned with generated contract artifacts. | Tooling/API contract | available through existing API check workflow |
| Request creation client/component tests | Form behavior, ProblemDetails display, DTO mapping, success message/navigation. | Client/component | dependent sidecar; not created here |
| E2E request creation happy path | Browser -> client -> API -> persistence -> success outcome/navigation. | Browser + client + server | future when client/UI/read flow exists |
| CSRF tests | Antiforgery token fetch/attach/failure behavior. | Security/client/server | deferred to CC-CSRF/concrete client-security work |

## 11. Dependent / Follow-up Slices

```text
[UI][DEPENDENT] Request creation form client sidecar
[READ][DEPENDENT] My Requests read/list/detail slice
[EXTENSION] Request documents
[EXTENSION] Notifications/navigation details
[EMPLOYEE] Employee review workflow
[SECURITY][CROSS-CUTTING] CC-CSRF-001 unsafe browser command protection
[APPLICANT][EXTENSION] Applicant party replacement/version management
[POLICY][FUTURE] Applicant verification requirement before request creation, if adopted
```

## 12. Implementation Checklist

```text
[x] POST /api/l1/requests exists
[x] endpoint is protected by authorization
[x] request DTO contains details + address
[x] request body omits applicantPartyId/clientAccountId
[x] client account id comes from L1 auth context
[x] current active individual applicant party is selected server-side
[x] missing applicant party returns validation ProblemDetails
[x] missing applicant party creates no request row
[x] address/details are validated before persistence
[x] ConnectionRequest is created in InReview state
[x] request is persisted with selected ApplicantPartyId
[x] success has no required response body
[x] integration tests cover success, no-write missing applicant and empty details validation
[x] domain tests cover InReview creation and failure branches
[ ] request creation UI/client sidecar
[ ] My Requests read/list/detail slice
[ ] E2E browser flow after client/read implementation exists
[ ] CSRF broad unsafe command rollout
[ ] applicant replacement/versioning slice
[ ] applicant verification policy, if later adopted
```
