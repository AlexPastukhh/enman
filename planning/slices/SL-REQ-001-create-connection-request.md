# SL-REQ-001 — Create Connection Request From Current Applicant Party

Status: implemented backend/API/persistence slice  
Package: `[L1]`  
Source scenario: `SC-04 Client Request Creation`  
Slice type: backend / API / persistence slice with dependent Client/UI/read/extension slices  
Current implementation status: implemented backend/API/persistence; client/UI/read slices remain separate

## 1. Slice Overview

Observable behavior:

```text
Client submits connection request data.
System creates a connection request for the current active applicant party of the authenticated account.
Created request enters InReview state.
```

Backend scope implemented by this slice:

```text
submit request data
-> server selects current active applicant party
-> create request
-> request enters InReview
-> API returns success
```

Out of this backend slice:

```text
- concrete request creation page;
- My Requests read/list/detail screen;
- document upload;
- notifications;
- employee review;
- applicant party replacement/version management;
- broad CSRF rollout.
```

Client success handling should follow:

```text
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
```

## 2. Sources / Source Behavior Items

Sources:

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/api/client-server-contract-principles.md
planning/api/api-error-contract.md
```

Behavior items used by this slice:

```text
REQ-CMD-CREATE-001 — Create request
REQ-LC-001 — Request creation creates InReview
REQ-VI-001 — Object address value integrity
REQ-UCQ-001 — Request uses saved ApplicantData without mutating it
```

Current implementation narrows applicant context to the current active individual applicant party.

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Client                                       │
│ submits connection request data              │
│ details + address                            │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ System                                       │
│ authenticated L1 client account is known     │
└──────────────┬───────────────────────────────┘
               │ resolve current active
               │ individual ApplicantParty
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
│ Create request        │   │ Validation error              │
│ for selected party    │   │ no request is created         │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           ▼                              ▼
┌──────────────────────┐        ┌──────────────────────────┐
│ Request enters        │        │ Client keeps/corrects     │
│ InReview state        │        │ request form context      │
└──────────┬───────────┘        └──────────────────────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Command success                              │
│ HTTP success is enough for this command flow │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Dependent client UI                          │
│ show success message                         │
│ navigate to My Requests                      │
└──────────────────────────────────────────────┘

Dependent / out-of-scope slices:
- request creation UI sidecar;
- My Requests read/list/detail slice;
- request document upload;
- notification/navigation;
- employee review.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Submits request details and object address. | SC-04 / REQ-CMD-CREATE-001 | source behavior |
| F02 | System | Uses authenticated L1 client account context. | API/security boundary | backend slice |
| F03 | System | Resolves current active individual applicant party for the account. | REQ-UCQ-001 / current implementation direction | backend slice |
| F04 | System | Rejects command if current active applicant party is missing. | validation/no-write behavior | backend slice |
| F05 | System | Validates request details and address. | REQ-VI-001 / validation addendum | backend/domain slice |
| F06 | System | Creates `ConnectionRequest` for selected applicant party. | REQ-CMD-CREATE-001 | backend/domain slice |
| F07 | System | Sets created request state to `InReview`. | REQ-LC-001 | backend/domain slice |
| F08 | System | Persists request with server-selected `ApplicantPartyId`. | persistence responsibility | backend slice |
| F09 | API | Returns command success without required response body. | CL-COMMAND-001 / API contract | backend/API slice |
| F10 | Client UI | Shows success message and navigates to My Requests. | UI convention / dependent sidecar | dependent client slice |
| F11 | Client UI | Shows ProblemDetails validation errors and preserves input where appropriate. | API error contract / client error convention | dependent client slice |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ POST /api/l1/requests                        │
│ Body: details + address                      │
│ No applicantPartyId/clientAccountId in body  │
└──────────────┬───────────────────────────────┘
               │ derive ClientAccountId
               │ from authenticated user context
               ▼
┌──────────────────────────────────────────────┐
│ Application Handler                          │
│ Create connection request command            │
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
│ Domain               │   │ Application/API error         │
│ Address.Create       │   │ validation ProblemDetails     │
│ ConnectionRequest    │   │ no write                      │
│ .Create              │   └──────────────────────────────┘
└──────────┬───────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Persistence                                  │
│ store request with server-selected           │
│ ApplicantPartyId and InReview status         │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ returns HTTP success without required body   │
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

| Step | Layer | Responsibility | Notes |
|---|---|---|---|
| I01 | API Controller | Accept `POST /api/l1/requests`. | Request body contains `details` and `address`. |
| I02 | API/Auth boundary | Derive current client account id from authenticated user context. | Client must not submit account identity. |
| I03 | Application Handler | Resolve current active individual applicant party for account. | Current implementation uses server-selected applicant party. |
| I04 | Application Handler | Return validation ProblemDetails if applicant party is missing. | No request row is created. |
| I05 | Domain | Create address/value objects and request details. | Invalid details/address fail before persistence. |
| I06 | Domain | Create `ConnectionRequest` in `InReview` state. | Covers `REQ-LC-001`. |
| I07 | Persistence | Store request with selected `ApplicantPartyId`. | Applicant party is selected server-side. |
| I08 | API Controller | Return success without required response body. | Client command confirmation uses HTTP success. |
| I09 | Client sidecar | Show success message and navigate to My Requests. | Dependent slice, not implemented here. |
| I10 | Client sidecar | Parse ProblemDetails and show errors. | Dependent slice, not implemented here. |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/requests` | POST | `L1CreateConnectionRequestDto` with `details` and `address` | none required | 200, 401, 403, 422, 500 | target L1 | yes |

Contract rule:

```text
The request body must not include applicantPartyId or clientAccountId.
ClientAccountId comes from auth context.
ApplicantPartyId is selected by the server from the current active individual applicant party.
```

Generated contract coverage:

```text
Shared/openapi.json and generated openapi-types.ts must show:
- POST /api/l1/requests request body has details + address;
- POST /api/l1/requests request body does not have applicantPartyId;
- command success has no required response body.
```

## 8. Questions / Decisions

Open questions:

| ID | Area | Question | Current direction | Status |
|---|---|---|---|---|
| SL-REQ-Q-001 | Read model | What exact My Requests list/detail response does the client need after navigation? | Separate read slice | open |
| SL-REQ-Q-002 | Applicant party versions | How are older applicant party versions made inactive when replacement/edit flow is implemented? | Keep current active lookup now; version management later | future |
| SL-REQ-Q-003 | UI | What concrete page/form implements request creation? | Dependent client sidecar when client work starts | open |
| SL-REQ-Q-004 | Security | When should unsafe browser commands enforce CSRF? | CC-CSRF-001 before broad client unsafe requests | open |

Accepted decisions:

```text
Decision:
Create request command treats HTTP success as confirmation and does not require response body for the initial command flow.

Reason:
The client does not need created entity data to continue this command flow.

Consequence:
Client UI can show a success message and navigate to My Requests.
The request read/list contract belongs to a separate My Requests/read slice.
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
A created request starts in InReview.

Reason:
A created request immediately enters the employee review queue.
```

## 9. Behavior Coverage

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| REQ-CMD-CREATE-001 — Create request | API/application/domain flow creates request from submitted details/address and server-selected applicant party. | Visual Scenario Flow / Scenario Slice Flow / Implementation Flow | covered |
| REQ-LC-001 — Request creation creates InReview | Domain creates `ConnectionRequest` in `InReview` state. | Scenario Slice Flow / Implementation Flow | covered |
| REQ-VI-001 — Object address value integrity | Domain/value-object step validates address before persistence. | Implementation Flow | covered |
| REQ-UCQ-001 — Request uses saved/current applicant context without client spoofing | Application resolves current active applicant party server-side; DTO does not expose applicantPartyId. | Visual Implementation Flow / API Contract | covered |
| Missing applicant context no-write | Missing current active applicant party returns validation ProblemDetails and creates no request. | Visual Scenario Flow / Visual Implementation Flow / Implementation Flow | covered |
| Client success outcome | HTTP success can drive success message and My Requests navigation. | Visual Scenario Flow / CL-COMMAND-001 reference | partially covered; client sidecar needed |
| My Requests read context | Read/list destination is identified but delegated to read slice. | Questions / Dependent slices | open |

## 10. Test / Verification Plan

Server integration coverage:

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Create request stores server-selected current active applicant party id | Client cannot spoof applicant party and server chooses current active party. | API + Application + Persistence | implemented |
| Created request starts InReview | Request lifecycle state after creation. | Domain + Persistence | implemented |
| Details and address are persisted | Request data is stored. | API + Persistence | implemented |
| Missing current active applicant party returns validation ProblemDetails | Failure branch and no-write behavior. | Application + API error mapping | implemented |
| Empty details returns validation ProblemDetails | Domain/application validation. | API + Domain/Application | implemented |
| Request DTO does not expose applicantPartyId | Contract safety. | API/OpenAPI | implemented |

Domain unit coverage:

```text
- ConnectionRequest.Create creates InReview request;
- create fails without details;
- create fails without object address;
- transient applicant party is guarded.
```

Client/UI coverage:

```text
Deferred until the request creation UI sidecar starts.
```

## 11. Dependent / Follow-up Slices

```text
[UI][DEPENDENT] Request creation form sidecar
[READ][DEPENDENT] My Requests read/list/detail slice
[EXTENSION] Request documents
[EXTENSION] Notifications/navigation
[EMPLOYEE] Employee review workflow
[SECURITY][CROSS-CUTTING] CC-CSRF-001 unsafe browser command protection
[APPLICANT][EXTENSION] Applicant party replacement/version management
```

## 12. Implementation Checklist

```text
[x] POST /api/l1/requests accepts details + address
[x] request body omits applicantPartyId/clientAccountId
[x] client account id comes from auth context
[x] current active individual applicant party is selected server-side
[x] missing applicant party returns validation ProblemDetails
[x] request starts InReview
[x] request persisted with selected ApplicantPartyId
[x] success has no required response body
[ ] request creation UI sidecar
[ ] My Requests read/list/detail slice
[ ] CSRF broad unsafe command rollout
```
