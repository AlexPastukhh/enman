# SL-APPL-001 — Create Individual ApplicantParty

Status: implemented backend/API/persistence slice  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`  
Slice type: backend / API / persistence slice with dependent UI, request-creation and applicant-extension slices  
Current implementation status: implemented and integration-tested; applicant UI/replacement/versioning/external verification remain separate

## 1. Slice Overview

Observable behavior:

```text
Authenticated L1 client saves individual applicant data.
Accepted data creates a persisted IndividualApplicantParty linked to the current client account.
Rejected data or missing account context returns validation/auth failure and does not create an applicant party.
```

Backend scope implemented by this slice:

```text
protected L1 applicant endpoint
-> authenticated account id from L1 auth context
-> full name + contact email + phone backend command input
-> account existence check
-> individual applicant domain creation
-> initial Unverified + current-active applicant state
-> persistence
-> applicant identity response
```

Current implementation evidence checked for this reconciliation:

```text
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Commands/L1Commands.cs
EnergyManagement.Server/L1/Application/Commands/L1CreateIndividualApplicantPartyHandler.cs
Domain.EnergyManagement/L1/Applicants/ApplicantParty.cs
Domain.EnergyManagement/L1/Applicants/IndividualApplicantParty.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Tests.EnergyManagement/Domain/Applicants/IndividualApplicantPartyTests.cs
```

Out of this backend slice:

```text
- applicant form UI/client sidecar;
- applicant data edit/replacement/versioning;
- enforcing one current active ApplicantParty across versions;
- external applicant verification provider integration;
- entrepreneur/legal-entity applicant types;
- request creation itself, except as a dependent consumer.
```

Related extension / dependent slices:

```text
[DEPENDENT] SL-REQ-001 — Create request from current active ApplicantParty
[UI][DEPENDENT] SL-APPL-UI-001 — Applicant data form UI
[EXTENSION][L1/L2] SL-APPL-002 — Replace current active ApplicantParty version
[EXTENSION][L2] SL-APPL-TYPE-ENT-001 — Entrepreneur applicant type
[EXTENSION][L2] SL-APPL-TYPE-LEGAL-001 — Legal entity applicant type
[PLUGIN][DEPENDENT] SL-VER-001 — Applicant verification through external provider
```

## 2. Sources / Source Behavior Items

Scenario / planning sources:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
planning/api/client-server-contract-principles.md
```

Behavior items covered by this backend slice:

```text
APPL-CMD-SAVE-001 — Save applicant data
APPL-VI-001 — Applicant data by applicant type
```

Scenario facts used by the backend slice:

```text
- Client account context is required.
- Individual applicant data has individual-specific shape.
- Applicant contact email may differ from account email.
- Accepted individual applicant data creates persisted ApplicantParty state.
- Current implementation creates applicant party as Unverified and current active version.
```

Client/UI facts intentionally not implemented here:

```text
- applicant form page/component placement;
- deferred validation timing;
- field-level ProblemDetails display;
- success message/navigation after applicant save.
```

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Authenticated L1 Client                      │
│ submits individual applicant data            │
│ full name + contact email + phone            │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ L1 System                                    │
│ resolves client account id from auth context │
└──────────────┬───────────────────────────────┘
               │ validate data and account existence
               ▼
┌──────────────────────────────────────────────┐
│ Input accepted and account exists?           │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Create Individual     │   │ Auth/validation failure       │
│ ApplicantParty        │   │ no applicant party created    │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           ▼                              ▼
┌──────────────────────┐        ┌──────────────────────────┐
│ Link to account       │        │ Client can correct input │
│ Unverified            │        │ or refresh auth context  │
│ Current active version│        └──────────────────────────┘
└──────────┬───────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ ApplicantParty saved                         │
│ response returns ApplicantPartyId + AccountId│
└──────────────────────────────────────────────┘

Dependent / out-of-scope:
- applicant form UI;
- replacement/versioning;
- external verification;
- non-individual applicant types.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Starts applicant data save as authenticated L1 client. | SC-10 / protected client action | backend/API slice |
| F02 | API/Auth boundary | Derives current L1 account id from authenticated L1 cookie/claims. | API/security boundary | backend/API slice |
| F03 | Client/API caller | Submits individual applicant data: full name, contact email, phone number. | APPL-CMD-SAVE-001 / APPL-VI-001 | source behavior |
| F04 | System | Validates individual applicant data shape through value objects/domain factory. | validation addendum | backend/domain slice |
| F05 | Application | Checks that the current account id resolves to a `ClientAccount`. | current implementation | backend/application slice |
| F06 | System | Rejects invalid data or missing account without applicant write. | no-write behavior | backend/API slice |
| F07 | Domain | Creates `IndividualApplicantParty` linked to `ClientAccountId`. | APPL-CMD-SAVE-001 | backend/domain slice |
| F08 | Domain | Initializes applicant as Individual, Unverified and current active version. | APPL-VI-001 / current implementation | backend/domain slice |
| F09 | Persistence | Stores applicant party row with account reference and individual fields. | persistence responsibility | backend/persistence slice |
| F10 | API | Returns applicant identity: `ApplicantPartyId`, `ClientAccountId`. | API contract | backend/API slice |
| F11 | Client UI | Shows success/errors and decides navigation. | UI convention | dependent client sidecar |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ [Authorize]                                  │
│ POST /api/l1/applicant-parties/individual    │
│ Body: fullName + email + phoneNumber         │
└──────────────┬───────────────────────────────┘
               │ TryGetCurrentL1AccountId
               │ builds command with server account id
               ▼
┌──────────────────────────────────────────────┐
│ Application Handler                          │
│ L1CreateIndividualApplicantPartyHandler      │
└──────────────┬───────────────────────────────┘
               │ validate FullName/Email/Phone
               │ load account by id
               ▼
┌──────────────────────────────────────────────┐
│ Account exists and input accepted?           │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Domain               │   │ API error mapping             │
│ IndividualApplicant  │   │ validation ProblemDetails     │
│ Party.Create         │   │ no applicant write            │
└──────────┬───────────┘   └──────────────────────────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Persistence                                  │
│ L1ApplicantParties row linked to account     │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ API response                                 │
│ 200 OK: ApplicantPartyId + ClientAccountId   │
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current implementation note |
|---|---|---|---|
| I01 | API Controller | Expose protected applicant creation endpoint. | `[Authorize] POST /api/l1/applicant-parties/individual`. |
| I02 | API/Auth boundary | Derive account id from L1 auth claims. | Client does not submit arbitrary account ownership. |
| I03 | API DTO | Receive individual applicant body. | `L1CreateIndividualApplicantPartyDto(fullName, email, phoneNumber)`. |
| I04 | Application Handler | Validate full name, email and phone value objects. | Aggregates all value-object errors before returning validation failure. |
| I05 | Application Handler | Load account and ensure it is a `ClientAccount`. | Missing/non-client account returns validation ProblemDetails in current implementation. |
| I06 | Domain | Create `IndividualApplicantParty`. | Factory links to account id and initializes Individual/Unverified/current-active state. |
| I07 | Persistence | Store applicant party. | `_applicantParties.Add(...)` + `L1DbContext.SaveChangesAsync(...)`. |
| I08 | API response | Return applicant identity. | `L1CreateIndividualApplicantPartyResponse(ApplicantPartyId, ClientAccountId)`. |
| I09 | Dependent client | Display form success/errors. | Not part of this backend archive; sidecar starts only with concrete client work. |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/applicant-parties/individual` | POST | `L1CreateIndividualApplicantPartyDto` with `fullName`, `email`, `phoneNumber` | `L1CreateIndividualApplicantPartyResponse` with `ApplicantPartyId`, `ClientAccountId` | 200, 401, 403, 422, 500 | target L1 | yes |

Contract notes:

```text
- Endpoint is protected by L1 authentication.
- Client does not submit `clientAccountId`.
- ClientAccountId is resolved by the server from auth context and passed into the command.
- Applicant contact email may differ from account email.
- Missing account or invalid applicant input returns validation ProblemDetails in the current implementation.
- Generated OpenAPI types and semantic constants must stay aligned when this contract changes.
```

## 8. Questions / Decisions

Open questions and unresolved future review items:

| ID | Area | Question | Current direction | Status |
|---|---|---|---|---|
| SL-APPL-Q-001 | Replacement/versioning | Is “current active applicant party” unique per account or per applicant type? | Separate replacement/versioning slice. | future review |
| SL-APPL-Q-002 | Missing account semantics | Should a missing account under authenticated claim return validation, unauthorized, forbidden or not found? | Keep current validation ProblemDetails unless API/security policy changes. | future review |
| SL-APPL-Q-003 | Applicant verification | When is external verification required before request creation/review? | Separate verification/provider slice; not required by current save command. | deferred |
| SL-APPL-Q-004 | Non-individual applicants | When do entrepreneur/legal-entity applicant shapes enter L1? | Separate applicant-type extension slices. | deferred |

Accepted decisions:

```text
Decision:
ApplicantParty creation uses server-derived ClientAccountId.

Reason:
The client must not choose or spoof account ownership in the request body.

Consequence:
The API DTO contains applicant fields only; account id comes from authenticated L1 context.
```

```text
Decision:
Current created IndividualApplicantParty starts Unverified and current active.

Reason:
Saving applicant data and verifying applicant data are separate concerns in current L1.

Consequence:
External/provider verification and replacement/version management remain separate slices.
```

```text
Decision:
Applicant contact email may differ from account email.

Reason:
The implemented DTO/domain stores applicant contact email separately from account identity email.
```

## 9. Behavior Coverage

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| APPL-CMD-SAVE-001 — Save applicant data | API/application/domain flow creates applicant party from accepted individual input. | Scenario Slice Flow / Implementation Flow | covered |
| APPL-VI-001 — Applicant data by applicant type | Individual full-name/contact data shape is validated and persisted. | Scenario Slice Flow / API Contract / Implementation Flow | covered |
| ApplicantParty linked to account | Server derives account id and persists `ClientAccountId`; client does not submit ownership id. | Visual Implementation Flow / API Contract | covered |
| ApplicantParty starts Unverified/current active | Domain base and individual factory initialize the state. | Scenario Slice Flow / Implementation Flow / Decisions | covered |
| Missing account no-write | Handler returns validation failure before applicant persistence. | Scenario Slice Flow / Visual Implementation Flow / Test Plan | covered |
| Applicant replacement/versioning | Explicitly separate future slice. | Slice Overview / Questions / Dependent Slices | deferred |
| Applicant UI behavior | Explicitly separate dependent client sidecar. | Visual Scenario Flow / Implementation Flow | delegated |

## 10. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `L1LoginCookie_CreateIndividualApplicantParty_Succeeds` | Authenticated L1 cookie can call applicant endpoint successfully. | API + Auth/session + Application | implemented |
| `CreateIndividualApplicantParty_StoresGeneratedAccountReference` | Applicant row stores generated id, account reference and individual fields. | API + Application + Persistence | implemented |
| `CreateIndividualApplicantParty_ForMissingAccount_ReturnsValidationProblem` | Missing account context returns validation ProblemDetails. | Application + API error mapping | implemented |
| `L1ProtectedEndpoint_WithoutAuth_ReturnsUnauthorized` | Protected applicant endpoint rejects unauthenticated requests. | API/Auth | implemented |
| `LegacyShapedCookie_WithExistingL1AccountId_IsRejected` | L1 endpoints require L1 auth marker, not legacy-shaped cookie only. | API/Auth | implemented |
| `Create_creates_unverified_current_active_applicant` | Domain applicant starts Individual, Unverified and current active. | Domain unit | implemented |
| `Create_fails_for_invalid_applicant_data` | Domain rejects missing/invalid required applicant data. | Domain unit | implemented |
| `MarkVerified_succeeds_when_minimum_data_present` | Verification state can be changed when minimum data exists. | Domain unit | implemented support behavior |
| `MarkInactiveVersion_marks_current_flag_false` | Version marker can be changed for future replacement flow. | Domain unit | implemented support behavior |
| OpenAPI/generated type check | Endpoint schema/statuses stay aligned with generated contract artifacts. | Tooling/API contract | available through existing API check workflow |
| Applicant form client tests | Field validation, field errors, success outcome and DTO mapping. | Client/component | dependent sidecar; not created here |

## 11. Dependent / Follow-up Slices

```text
[DEPENDENT] SL-REQ-001 — Create request from current active ApplicantParty
[UI][DEPENDENT] SL-APPL-UI-001 — Applicant data form UI
[EXTENSION][L1/L2] SL-APPL-002 — Replace current active ApplicantParty version
[EXTENSION][L2] SL-APPL-TYPE-ENT-001 — Entrepreneur applicant type
[EXTENSION][L2] SL-APPL-TYPE-LEGAL-001 — Legal entity applicant type
[PLUGIN][DEPENDENT] SL-VER-001 — Applicant verification through external provider
```

## 12. Implementation Checklist

```text
[x] POST /api/l1/applicant-parties/individual exists
[x] endpoint is protected by authorization
[x] account id is derived from L1 auth context
[x] request DTO contains fullName + email + phoneNumber
[x] client does not submit clientAccountId
[x] application validates full name/email/phone
[x] missing account returns validation ProblemDetails
[x] IndividualApplicantParty is created as Individual
[x] applicant starts Unverified
[x] applicant starts current active version
[x] applicant row is persisted with ClientAccountId
[x] response returns ApplicantPartyId + ClientAccountId
[x] integration tests cover success and missing account validation
[x] domain tests cover applicant initial state and validation
[ ] applicant form UI/client sidecar
[ ] applicant replacement/current-active uniqueness policy
[ ] external verification provider slice
[ ] entrepreneur/legal-entity applicant types
```
