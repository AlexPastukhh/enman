# SL-ACC-001 — Register Client Account

Status: implemented backend/API/persistence slice  
Package: `[L1]`  
Source scenario: `SC-01 Guest Registration`  
Slice type: backend / API / persistence slice with dependent auth/client UI extension slices  
Current implementation status: implemented and integration-tested; registration UI/password-confirmation UI remains separate; DB uniqueness constraint is not introduced by this slice

## 1. Slice Overview

Observable behavior:

```text
Guest registers a client account.
Accepted backend registration creates a persisted L1 ClientAccount identity.
Rejected backend registration returns validation ProblemDetails and does not create a second/invalid account.
```

Backend scope implemented by this slice:

```text
public L1 registration endpoint
-> email/password backend command input
-> email/password validation and duplicate email precheck
-> password hash boundary
-> active ClientAccount domain state
-> persistence
-> account identity response
```

Current implementation evidence checked for this reconciliation:

```text
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Commands/L1Commands.cs
EnergyManagement.Server/L1/Application/Commands/L1RegisterClientAccountHandler.cs
Domain.EnergyManagement/L1/Accounts/Account.cs
Domain.EnergyManagement/L1/Accounts/ClientAccount.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Tests.EnergyManagement/Domain/Accounts/ClientAccountTests.cs
```

Out of this backend slice:

```text
- registration page/UI implementation;
- password confirmation field handling in the client UI;
- automatic sign-in after registration;
- email confirmation / PendingActivation lifecycle;
- password recovery;
- rate limiting / lockout / auth hardening;
- DB uniqueness constraint hardening.
```

Related extension / dependent slices:

```text
[UI][DEPENDENT] SL-AUTH-UI-001 — Login / registration / recovery UI
[AUTH][DEPENDENT] L1 login/current-user/logout client integration
[EXTENSION][L2] SL-AUTH-EMAIL-001 — Email confirmation / PendingActivation flow
[EXTENSION][L2] SL-AUTH-RECOVERY-001 — Password recovery flow
[EXTENSION][L2] SL-AUTH-HARDENING-001 — Rate limiting / lockout / auth hardening
```

## 2. Sources / Source Behavior Items

Scenario / planning sources:

```text
planning/diagrams/scenario-text-specs/SC-01-guest-registration.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
planning/api/client-server-contract-principles.md
```

Behavior items covered by this backend slice:

```text
ACC-CMD-REGISTER-001 — Register account
```

Scenario facts used by the backend slice:

```text
- Guest provides registration data.
- Invalid registration data must not create an account.
- Duplicate email must not create a second account through the backend command.
- Accepted registration creates an L1 client account identity.
- Current L1 core creates the account as Active.
```

Client/UI facts intentionally not implemented here:

```text
- registration screen layout;
- password confirmation field;
- visible client-side validation timing;
- post-registration navigation or auto-login.
```

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Guest                                        │
│ enters registration data                     │
│ UI may include password confirmation         │
└──────────────┬───────────────────────────────┘
               │ submits backend command
               │ current API body: email + password
               ▼
┌──────────────────────────────────────────────┐
│ L1 System                                    │
│ checks email/password command data           │
│ checks duplicate email                       │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
   accepted          rejected
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Create active         │   │ Validation ProblemDetails     │
│ ClientAccount         │   │ no account is created         │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           ▼                              ▼
┌──────────────────────┐        ┌──────────────────────────┐
│ Persist account       │        │ Guest/client can correct │
│ email + password hash │        │ submitted data           │
└──────────┬───────────┘        └──────────────────────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Registration success                         │
│ API returns AccountId + Email                │
└──────────────────────────────────────────────┘

Dependent / out-of-scope:
- concrete registration UI;
- password confirmation validation;
- auto-login/session issuance;
- PendingActivation/email confirmation;
- auth hardening.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Guest | Provides registration data. | SC-01 | source behavior |
| F02 | Client UI | May collect password confirmation and show correction feedback. | SC-01 / validation addendum | dependent UI; not backend |
| F03 | Client/API caller | Submits current backend registration body: email + password. | current implementation / API contract | backend slice |
| F04 | System | Validates email value and password hashability. | validation addendum / domain value objects | backend slice |
| F05 | System | Rejects duplicate email through application precheck. | ACC-CMD-REGISTER-001 | backend slice |
| F06 | System | Creates `ClientAccount` with role/type Client and Active state. | ACC-CMD-REGISTER-001 | backend/domain slice |
| F07 | System | Persists account with password hash; raw password is not persisted as domain state. | security boundary | backend/persistence slice |
| F08 | API | Returns account identity: `AccountId`, `Email`. | API contract | backend/API slice |
| F09 | Client UI | Shows success/errors and chooses navigation/auto-login behavior. | SC-01 UI behavior | dependent client sidecar |
| F10 | Future extension | Handles PendingActivation/email confirmation if introduced. | future L2 branch | extension slice |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ POST /api/l1/auth/register                   │
│ L1RegisterClientAccountDto                   │
│ Body: email + password                       │
└──────────────┬───────────────────────────────┘
               │ create command
               ▼
┌──────────────────────────────────────────────┐
│ Application Handler                          │
│ L1RegisterClientAccountHandler               │
└──────────────┬───────────────────────────────┘
               │ validate email
               │ check duplicate email
               │ hash password
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
│ Domain               │   │ API error mapping             │
│ ClientAccount.Create │   │ validation ProblemDetails     │
│ Active + Client role │   │ no account write              │
└──────────┬───────────┘   └──────────────────────────────┘
           │
           ▼
┌──────────────────────┐
│ Persistence          │
│ L1Accounts row       │
└──────────┬───────────┘
           ▼
┌──────────────────────────────────────────────┐
│ API response                                 │
│ 200 OK: AccountId + Email                    │
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current implementation note |
|---|---|---|---|
| I01 | API Controller | Expose public L1 registration endpoint. | `POST /api/l1/auth/register`. |
| I02 | API DTO | Receive backend registration body. | `L1RegisterClientAccountDto(email, password)`; no `passwordConfirm` field in current backend contract. |
| I03 | Application Handler | Validate email and duplicate email. | `Email.Create(...)`; `_accounts.ExistsByEmailAsync(...)`. |
| I04 | Security boundary | Hash password before domain persistence. | `L1PasswordHasher.HashPassword(...)`; raw password is not persisted as account state. |
| I05 | Domain | Create active client account. | `ClientAccount.Create(...)` / `ClientAccount.Register(...)`; account role is Client and active state is true. |
| I06 | Persistence | Store account. | `_accounts.Add(...)` + `L1DbContext.SaveChangesAsync(...)`. |
| I07 | API error mapping | Return validation ProblemDetails for domain/application failures. | Duplicate/invalid input goes through validation problem mapping. |
| I08 | API response | Return registration identity. | `L1RegisterClientAccountResponse(AccountId, Email)` with HTTP 200. |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/auth/register` | POST | `L1RegisterClientAccountDto` with `email`, `password` | `L1RegisterClientAccountResponse` with `AccountId`, `Email` | 200, 422, 500 | target L1 | yes |

Contract notes:

```text
- Current backend request body does not include password confirmation.
- Password confirmation belongs to dependent client/UI validation unless a future backend contract change explicitly adds it.
- Endpoint is public; it does not require an authenticated L1 cookie.
- Duplicate/invalid input returns validation ProblemDetails.
- Generated OpenAPI types and semantic constants must stay aligned when this contract changes.
```

## 8. Questions / Decisions

Open questions and unresolved future review items:

| ID | Area | Question | Current direction | Status |
|---|---|---|---|---|
| SL-ACC-Q-001 | DB hardening | Should duplicate email also be enforced by a DB unique constraint? | Do not add in this documentation archive; application precheck is current implemented behavior. | future review |
| SL-ACC-Q-002 | Activation lifecycle | Should PendingActivation/email confirmation be introduced? | Separate L2 extension slice. | deferred |
| SL-ACC-Q-003 | Auth UX | Should registration automatically sign the user in? | Separate auth/client flow decision. | open for client/auth planning |

Accepted decisions:

```text
Decision:
Current L1 registration creates an Active ClientAccount.

Reason:
L1 backend flow needs an account identity that can be used by protected applicant/request commands without implementing email confirmation first.

Consequence:
PendingActivation/email confirmation remains a future extension slice.
```

```text
Decision:
Current backend registration accepts email + password only.

Reason:
The implemented `L1RegisterClientAccountDto` contains `email` and `password`; password confirmation is a UI/client validation concern unless the API contract changes later.

Consequence:
Backend slice docs must not claim a current `passwordConfirm` API field.
```

```text
Decision:
Duplicate email is rejected by application precheck in the current implementation.

Reason:
This is the implemented backend behavior and is covered by integration tests.

Consequence:
DB uniqueness can be reviewed later, but is not part of this documentation/status archive.
```

## 9. Behavior Coverage

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| ACC-CMD-REGISTER-001 — Register account | Backend command receives current registration input and creates account identity when accepted. | Scenario Slice Flow / Implementation Flow | covered |
| Invalid registration does not create account | Rejected branch returns validation ProblemDetails before persistence. | Visual Scenario Flow / Visual Implementation Flow / Implementation Flow | covered |
| Duplicate email does not create a second valid account | Application precheck rejects duplicate email. | Scenario Slice Flow / Implementation Flow / Test Plan | covered |
| Accepted registration creates an active client account | Domain account creation sets role Client and active state. | Scenario Slice Flow / Implementation Flow / Decisions | covered |
| Registration success can be visible to user | API returns account identity; visible UI success handling is delegated. | Visual Scenario Flow / API Contract | partially covered; dependent client/UI |
| Password confirmation behavior | Recorded as UI/client responsibility, not current backend API field. | Scenario Slice Flow / API Contract / Decisions | delegated |
| PendingActivation/email confirmation | Explicitly out of current backend L1 slice. | Slice Overview / Questions / Dependent Slices | deferred |

## 10. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `RegisterClientAccount_CreatesL1Account` | Account row is created with generated id, email, password hash, Client role/type and active state. | API + Application + Persistence | implemented |
| `RegisterClientAccount_WithDuplicateEmail_ReturnsValidationProblem` | Duplicate registration returns validation ProblemDetails. | API + Application | implemented |
| `L1RegisterLoginAndCurrentUser_UsesL1AccountIdentity` | Registered account identity can be used by L1 login/current-user flow. | API + Auth/session support | implemented |
| `Register_creates_active_account` | Domain account starts active and has Client role. | Domain unit | implemented |
| `Register_rejects_missing_email` | Domain rejects missing email. | Domain unit | implemented |
| `EnsureActivated_succeeds_for_active_account` | Active account guard succeeds for active account. | Domain unit | implemented |
| `EnsureActivated_fails_for_non_active_account` | Guard fails for inactive account. | Domain unit | implemented |
| OpenAPI/generated type check | Endpoint schema/statuses stay aligned with generated contract artifacts. | Tooling/API contract | available through existing API check workflow |
| Registration UI tests | Password confirmation, field display, user-visible errors/success. | Client/component | dependent sidecar; not created here |
| DB uniqueness constraint test | Database-level duplicate protection. | Persistence hardening | future review if constraint is introduced |

## 11. Dependent / Follow-up Slices

```text
[UI][DEPENDENT] SL-AUTH-UI-001 — Login / registration / recovery UI
[AUTH][DEPENDENT] L1 login/current-user/logout client integration
[EXTENSION][L2] SL-AUTH-EMAIL-001 — Email confirmation / PendingActivation flow
[EXTENSION][L2] SL-AUTH-RECOVERY-001 — Password recovery flow
[EXTENSION][L2] SL-AUTH-HARDENING-001 — Rate limiting / lockout / auth hardening
[HARDENING][FUTURE] DB uniqueness constraint for account email, if adopted
```

## 12. Implementation Checklist

```text
[x] POST /api/l1/auth/register exists
[x] request DTO contains email + password
[x] duplicate email is rejected by application precheck
[x] password is hashed before persistence
[x] ClientAccount is created with Client role/type
[x] account starts active in current L1 core
[x] account identity is persisted
[x] response returns AccountId + Email
[x] validation errors return ProblemDetails
[x] integration tests cover success and duplicate email validation
[x] domain tests cover active account creation and activation guard
[ ] registration UI/client sidecar
[ ] password confirmation UI/client behavior
[ ] auto-login decision
[ ] PendingActivation/email confirmation extension
[ ] DB uniqueness constraint hardening, if later adopted
```
