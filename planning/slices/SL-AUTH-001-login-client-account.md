# SL-AUTH-001 — Login Client Account

Status: implemented backend/API/session slice  
Package: `[L1]`  
Source scenario: `SC-02 Login / client auth flow` or current auth consolidation planning  
Slice type: backend / API / session slice with dependent client auth sidecar  
Current implementation status: implemented and integration-tested; concrete client login/session flow remains future client work

## 1. Slice Overview

Observable behavior:

```text
Client submits login credentials.
System validates the credentials against an active L1 ClientAccount.
On success, system creates an L1-marked cookie session and returns authenticated current-user data.
On failure, system returns safe validation ProblemDetails without creating a session.
```

Backend scope implemented by this slice:

```text
public L1 login endpoint
-> email/password command input
-> safe invalid-credential handling
-> active-account check
-> password hash verification
-> L1 cookie marker claim
-> current-user response body
```

Current implementation evidence checked for this reconciliation:

```text
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Commands/L1Commands.cs
EnergyManagement.Server/L1/Application/Commands/L1LoginClientAccountHandler.cs
EnergyManagement.Server/L1/Application/Security/L1AuthClaimTypes.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Out of this backend slice:

```text
- login page/UI implementation;
- client session store/cache;
- route guards;
- refresh/current-user bootstrapping strategy;
- remember-me/persistent session choice;
- password recovery;
- lockout/rate limiting/auth hardening;
- CSRF token lifecycle for unsafe requests.
```

## 2. Sources / Source Behavior Items

Scenario / planning sources:

```text
planning/api/client-server-contract-principles.md
planning/api/api-error-contract.md
planning/testing/testing-principles.md
planning/slices/slice-questions-register.md
planning/slices/slice-implementation-notes-register.md
```

Behavior items covered by this backend slice:

```text
Source BI TBD — Client submits login credentials.
Source BI TBD — System authenticates active L1 client account.
Source BI TBD — System creates L1 session on successful login.
Source BI TBD — Invalid credentials return safe validation failure.
```

Source behavior item IDs are not fully attached yet.

Temporary Source BI TBD labels are used for this documentation pass.

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Client                                       │
│ submits email + password                     │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ L1 System                                    │
│ validates email, finds ClientAccount,        │
│ checks account is active and password matches│
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
    success          failure
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Create L1 cookie      │   │ Validation ProblemDetails     │
│ session marker        │   │ no session is created         │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           ▼                              ▼
┌──────────────────────┐        ┌──────────────────────────┐
│ Return current-user   │        │ Client can show a safe    │
│ response              │        │ credential error           │
└──────────────────────┘        └──────────────────────────┘

Dependent / out-of-scope:
- login page/client sidecar;
- auth state store;
- route guards;
- persistent session/remember-me;
- rate limiting/lockout;
- password recovery.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Submits email and password. | Source BI TBD | source behavior |
| F02 | System | Parses/validates email value enough to find account. | auth validation | backend/application slice |
| F03 | System | Looks up account by email. | current implementation | backend/application slice |
| F04 | System | Rejects non-client, missing, inactive or invalid-password attempts. | auth safety | backend/application slice |
| F05 | System | Returns the same safe validation failure for unknown email and invalid password. | integration test | backend/API slice |
| F06 | System | Creates L1-marked cookie session on success. | current implementation | backend/API/session slice |
| F07 | API | Returns current-user response: account id, email, role, active/authenticated flags. | API contract | backend/API slice |
| F08 | Client UI | Stores/uses authenticated state and navigates. | dependent client sidecar | out of current backend slice |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ POST /api/l1/auth/login                      │
│ L1LoginRequest: email + password             │
└──────────────┬───────────────────────────────┘
               │ send command
               ▼
┌──────────────────────────────────────────────┐
│ Application Handler                          │
│ L1LoginClientAccountHandler                  │
└──────────────┬───────────────────────────────┘
               │ Email.Create + account lookup
               │ EnsureActivated + password verify
               ▼
┌──────────────────────────────────────────────┐
│ Credentials accepted?                        │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ API Controller        │   │ API error mapping             │
│ SignInL1AccountAsync  │   │ validation ProblemDetails     │
│ adds L1 marker claim  │   │ no cookie session             │
└──────────┬───────────┘   └──────────────────────────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ API response                                 │
│ 200 OK: L1CurrentUserResponse                │
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current implementation note |
|---|---|---|---|
| I01 | API Controller | Expose public L1 login endpoint. | `POST /api/l1/auth/login`. |
| I02 | API DTO | Receive login body. | `L1LoginRequest(email,password)`. |
| I03 | Application Handler | Validate email and load account by email. | Invalid email/missing account returns safe invalid credentials. |
| I04 | Application Handler | Require `ClientAccount` and active account. | `EnsureActivated()` failure returns validation errors. |
| I05 | Security boundary | Verify password hash. | `L1PasswordHasher.VerifyPassword(...)`. |
| I06 | API Controller | Sign in with cookie auth on success. | Adds `NameIdentifier`, `Email`, `Role`, `L1AuthClaimTypes.AuthModel`. |
| I07 | API response | Return authenticated current-user shape. | `L1CurrentUserResponse(..., IsAuthenticated: true)`. |
| I08 | API error mapping | Return validation ProblemDetails on failure. | Unknown email and invalid password share safe failure body. |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO / Body | Statuses | ProblemDetails statuses | Auth/session behavior | Contract status | OpenAPI exposed? | Generated TS types |
|---|---|---|---|---|---|---|---|---|
| `/api/l1/auth/login` | POST | `L1LoginRequest` with `email`, `password` | `L1CurrentUserResponse` | 200, 422, 500 | 422, 500 | creates non-persistent L1 cookie session on success | target L1 | yes | `operations["L1LoginClientAccount"]`, `components["schemas"]["L1LoginRequest"]`, `L1CurrentUserResponse` |

## 8. Questions / Decisions

Open questions and future review items:

| ID | Area | Question status | Question | Assumption / current direction | Shared register |
|---|---|---|---|---|---|
| SL-AUTH-Q-001 | Client auth baseline | open | What concrete client state/store/query shape owns L1 current-user after login? | Draft assumes this belongs to the first auth/session `.client.md` sidecar. | `slice-questions-register.md` |
| SL-AUTH-Q-002 | Registration/auth UX | open | Should registration automatically login or require explicit login? | Current backend register does not issue session; draft assumes explicit auth/client decision later. | `slice-questions-register.md` |
| SL-AUTH-Q-003 | Auth hardening | future review | When should lockout/rate limiting be introduced? | Not part of current implemented backend slice. | `slice-extension-points-register.md` if adopted |

Accepted decisions:

```text
Decision:
Successful L1 login creates an L1-marked cookie session and returns current-user data.

Decision:
Unknown email and invalid password use the same safe validation failure behavior.
```

## 9. Behavior Coverage

| Scenario behavior item | How slice covers it | Draft/file location | Status |
|---|---|---|---|
| Source BI TBD — Client submits login credentials | API receives email/password. | Scenario Flow / API Contract | covered |
| Source BI TBD — System authenticates active L1 account | Handler validates email, account type, activation and password. | Implementation Flow | covered |
| Source BI TBD — Successful login creates session | Controller signs in with L1 marker claim. | Visual Implementation Flow / Implementation Flow | covered |
| Source BI TBD — Invalid credentials fail safely | Handler returns same safe failure for unknown email and invalid password. | Scenario Flow / Test Plan | covered |
| Client auth state update | Response provides current-user shape for future client sidecar. | API Contract / Dependent Slices | partially covered; client sidecar needed |

## 10. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `L1RegisterLoginAndCurrentUser_UsesL1AccountIdentity` | Login returns current-user and current-user endpoint matches session identity. | API + session + persistence | implemented |
| `L1Login_WithInvalidPassword_ReturnsValidationProblem` | Invalid password returns validation ProblemDetails. | API + application | implemented |
| `L1Login_WithUnknownEmail_ReturnsSameSafeFailureAsInvalidPassword` | Unknown email and invalid password are indistinguishable to caller. | API + security behavior | implemented |
| Generated OpenAPI type check | Login path/request/response remain generated. | Tooling/API contract | available through API check workflow |
| Client login component tests | Form validation, pending/error states, success session state. | Client/component | future client sidecar |
| Login browser E2E | Browser login creates usable session. | Browser + client + server | future after client auth UI exists |

## 11. Dependent / Follow-up Slices

```text
[CLIENT][DEPENDENT] L1 auth/session client baseline
[CLIENT][DEPENDENT] Login page/form sidecar
[CLIENT][DEPENDENT] Route guard/current-user cache strategy
[SECURITY][FUTURE] rate limiting / lockout / auth hardening
[SECURITY][FUTURE] CSRF token lifecycle for unsafe requests after login
```

## 12. Implementation Checklist

```text
[x] POST /api/l1/auth/login exists
[x] request DTO has email + password
[x] email/account/password validation implemented
[x] active account check implemented
[x] successful login signs L1 cookie session
[x] L1 marker claim is added
[x] response returns current-user shape
[x] invalid credentials return validation ProblemDetails
[x] integration tests cover success and safe failures
[ ] concrete login client sidecar
[ ] client auth state/route guard implementation
[ ] L1 login browser E2E after UI exists
[ ] auth hardening/rate limiting
```
