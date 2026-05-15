# SL-ACC-001 — Register Client Account — Full Backend Slice Example

Status: example / valid full backend slice draft  
Example type: full parent backend/API/persistence slice  
Source scenario: `SC-01 Guest Registration`  
Not authoritative implementation evidence: check active slice and repo before implementation

## 1. Slice Overview

Observable behavior:

```text
Guest registers a client account.
Accepted registration creates persisted client account identity.
```

Why this is a real slice:

```text
- separate observable behavior;
- creates account identity required by later L1 slices;
- clear success/failure outcomes;
- independently testable through API/persistence integration tests;
- does not require ApplicantParty, Request, UI, password recovery or email confirmation.
```

Out of scope:

```text
- registration UI;
- auto-login/session issuance after registration;
- email confirmation / PendingActivation;
- password recovery;
- applicant data collection.
```

## 2. Sources / Source Behavior Items

Sources:

```text
planning/diagrams/scenario-text-specs/SC-01-guest-registration.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
```

Behavior items:

```text
ACC-CMD-REGISTER-001 — Register account
```

Scenario facts used:

```text
- Guest enters email, password and password confirmation.
- Client-side validation can show correction feedback.
- Guest submits registration.
- System checks whether registration data is accepted.
- Invalid registration data does not create an account.
- Accepted registration creates an account.
- Current core creates activated account.
```

## 3. Visual Scenario Flow

```text
┌──────────────────┐
│      Guest       │
└────────┬─────────┘
         │ opens registration screen
         │ enters email + password + confirmation
         ▼
┌──────────────────────────────────────────────┐
│ Client UI                                    │
│ Visible validation/correction feedback       │
└────────┬─────────────────────────────────────┘
         │ submits registration
         ▼
┌──────────────────────────────────────────────┐
│ System                                       │
│ Checks whether registration data is accepted │
└────────┬─────────────────────────────────────┘
         │
   ┌─────┴─────┐
   │           │
accepted    rejected
   │           │
   ▼           ▼
┌──────────────────────────────┐   ┌─────────────────────────────┐
│ Create client account         │   │ No account is created        │
│ Current core: Active          │   │ Validation/business errors   │
└──────────────┬───────────────┘   └──────────────┬──────────────┘
               │                                  │
               ▼                                  ▼
┌──────────────────────────────┐       ┌──────────────────────────┐
│ Registration success visible  │       │ Guest corrects input     │
└──────────────────────────────┘       └──────────────────────────┘

Out of this backend slice:
- concrete registration screen;
- auto-login decision;
- email confirmation / PendingActivation extension;
- password recovery.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source | Scope status |
|---|---|---|---|---|
| F01 | Guest | Provides email, password and password confirmation. | SC-01 / SC-01-DATA-01 | source behavior |
| F02 | Client UI | Shows visible validation feedback and allows correction. | SC-01 / validation addendum | dependent UI |
| F03 | Guest | Submits registration. | SC-01 | source behavior |
| F04 | System | Validates registration data server-side. | validation addendum | backend slice |
| F05 | System | Rejects invalid input without creating account. | SC-01 / ACC-CMD-REGISTER-001 | backend slice |
| F06 | System | Creates client account for accepted input. | SC-01 / ACC-CMD-REGISTER-001 | backend slice |
| F07 | System | Marks account `Active` in current L1 core. | SC-01 | backend slice |
| F08 | System/API | Returns registration success or validation ProblemDetails. | API error contract | backend/API slice |
| F09 | Client UI | Shows registration success state. | SC-01 | dependent UI |
| F10 | Future extension | Handles PendingActivation / email confirmation if introduced. | SC-01 future branch | extension slice |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ POST /api/l1/auth/register                   │
│ Body: email + password + passwordConfirm     │
└──────────────┬───────────────────────────────┘
               │ map/validate request shape
               ▼
┌──────────────────────────────────────────────┐
│ Application Handler                          │
│ Register client account command              │
└──────────────┬───────────────────────────────┘
               │
               │ check duplicate email / command validity
               ▼
┌──────────────────────────────────────────────┐
│ Registration accepted?                       │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Domain / Account     │   │ API error mapping             │
│ ClientAccount.Register│  │ ProblemDetails                │
│ Activation = Active  │   │ no account write              │
└──────────┬───────────┘   └──────────────────────────────┘
           │
           ▼
┌──────────────────────┐
│ Security boundary    │
│ hash password        │
│ no raw password state│
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Persistence          │
│ store account row    │
└──────────┬───────────┘
           ▼
┌──────────────────────────────────────────────┐
│ API response                                 │
│ success with account identity or no body     │
│ depending on active contract                 │
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Notes |
|---|---|---|---|
| I01 | API | Receive registration DTO. | Email, password, password confirmation. |
| I02 | API/Application | Validate required fields and confirmation match. | Server-side validation is authoritative. |
| I03 | Application | Check duplicate email / account uniqueness policy. | Prefer application precheck plus DB constraint eventually if adopted. |
| I04 | Security boundary | Hash password. | Raw password must not become persisted domain state. |
| I05 | Domain | Register client account. | Creates account identity and current-core active state. |
| I06 | Persistence | Store account. | Invalid registration must not write account state. |
| I07 | API error mapping | Return validation ProblemDetails for invalid input. | Follow API error contract and constants policy. |
| I08 | API response | Return registration success. | Exact response body belongs to active API contract. |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/auth/register` | POST | registration data | account identity or explicitly empty command success, depending on active contract | 200/201/204, 400/422, 500 | target L1 | yes |

Contract notes:

```text
- request contains registration data only;
- response body policy must be explicit in the active slice;
- ProblemDetails is used for validation/business errors;
- generated OpenAPI types and generated constants must reflect final contract.
```

## 8. Questions / Decisions

Open questions first:

| ID | Area | Question | Current direction | Status |
|---|---|---|---|---|
| SL-ACC-Q-001 | Registration UX | Should registration automatically sign the user in? | Keep separate until auth/client flow decides | open |
| SL-ACC-Q-002 | Uniqueness | Should duplicate email be enforced by application precheck, DB unique constraint, or both? | Prefer both eventually; current slice can prove validation behavior | future review |
| SL-ACC-Q-003 | Activation | Should PendingActivation be introduced? | Separate extension slice | deferred |

Accepted decisions:

```text
Decision:
Current L1 registration creates active ClientAccount.

Reason:
L1 goal is to unblock protected client flows without implementing email confirmation.

Consequence:
Email confirmation / PendingActivation becomes an extension slice.
```

```text
Decision:
Invalid registration input must not create an account.

Reason:
Registration is a command with no-write failure guarantee.
```

## 9. Behavior Coverage

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| ACC-CMD-REGISTER-001 — Register account | Backend command receives registration input and creates account when accepted. | Scenario Slice Flow / Implementation Flow | covered |
| SC-01 — invalid registration does not create account | Failure branch returns ProblemDetails/no write. | Visual Scenario Flow / Visual Implementation Flow / Implementation Flow | covered |
| SC-01 — accepted registration creates account | Domain/account step creates client account. | Scenario Slice Flow / Implementation Flow | covered |
| SC-01 — current core account is Active | Domain step sets current-core Active state. | Scenario Slice Flow / Decisions | covered |
| SC-01 — visible registration success state | API returns success; UI behavior is dependent sidecar. | Scenario Slice Flow | partially covered |
| SC-01 — future activation required | Explicitly out of scope/deferred. | Slice Overview / Questions | deferred |

## 10. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Register account success integration test | Accepted input creates account. | API + Application + Persistence | planned/current depending on active slice |
| Duplicate email validation test | Duplicate registration returns validation ProblemDetails. | API + Application | planned/current depending on active slice |
| Invalid email/no password/mismatch tests | Invalid input does not create account. | API + Application/Domain | planned |
| No write on failed registration | Failed command leaves no account row. | Persistence | planned |
| Active account state test | Created account starts active in current core. | Domain/Application/Persistence | planned/current depending on active slice |
| OpenAPI/contract check | Register endpoint schema/statuses match generated contract. | Tooling/API | planned |
| UI registration tests | User sees field errors/success outcome. | Client sidecar | dependent |

## 11. Dependent / Follow-up Slices

```text
[UI][DEPENDENT] SL-AUTH-UI-001 — Login / registration / recovery UI
[EXTENSION][L2] SL-AUTH-EMAIL-001 — Email confirmation / PendingActivation flow
[EXTENSION][L2] SL-AUTH-RECOVERY-001 — Password recovery flow
[EXTENSION][L2] SL-AUTH-HARDENING-001 — Rate limiting / lockout / auth hardening
[AUTH/FRAMEWORK][CROSS-CUTTING] AUTH-GUARD-001 — Active account guard for protected actions
```

## 12. Implementation Checklist

```text
[ ] Register client account through API
[ ] Persist account identity
[ ] Account starts active in L1 core
[ ] Duplicate email returns validation ProblemDetails
[ ] Decide DB unique constraint policy
[ ] Plan dependent auth UI slice
[ ] Plan L2 email confirmation slice
```
