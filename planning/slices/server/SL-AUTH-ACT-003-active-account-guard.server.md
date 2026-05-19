# SL-AUTH-ACT-003.server — Active Account Guard For Login And Protected Use Cases

Status: draft / ready for implementation planning  
Logical slice: `CC-AUTH-ACT-001`  
Package: server/auth/session/security  
Slice type: server/backend/API slice  
Primary purpose: ensure pending Client accounts cannot use application flows before activation.

Depends on:

```text
planning/slices/cross-cutting/CC-AUTH-ACT-001-email-activation-flow.md
planning/slices/server/SL-AUTH-ACT-001-register-pending-and-send-activation-email.server.md
planning/slices/server/SL-AUTH-ACT-002-activate-client-account.server.md
planning/slices/SL-AUTH-001-login-client-account.md
planning/slices/SL-AUTH-002-current-user.md
```

## 0. Scenario Sources

Business scenario:

```text
User may register but must activate account before protected pages/commands.
```

Cross-cutting behavior:

```text
Protected client/employee functionality requires activated account.
Current implementation direction calls Account.EnsureActivated at app/auth boundary.
```

Data source:

```text
Current-user/session response exposes active state.
```

Behavior items:

```text
AUTH-ACT-GUARD-001 — Login for pending Client account is rejected with activation-required problem.
AUTH-ACT-GUARD-002 — No auth cookie is issued for pending login.
AUTH-ACT-GUARD-003 — Protected client commands reject inactive account.
AUTH-ACT-GUARD-004 — Current-user does not silently treat pending account as usable.
AUTH-ACT-GUARD-005 — Active account login/protected flow still works.
```

Concern umbrella:

```text
CC-AUTH-ACT-001
```

Stable source behavior item IDs are pending scenario/source registry.

## 1. Slice Overview

Minimal first-pass UX decision:

```text
Reject login for non-active Client account.
Do not create limited inactive session.
```

Target behavior:

```text
POST /api/auth/login
      ↓
credentials valid?
      ↓
account active?
  ├─ no → 403 activation-required ProblemDetails, no cookie
  └─ yes → issue normal session cookie
```

Protected command behavior:

```text
Authenticated account context
      ↓
load account / check active marker
      ↓
if inactive: reject before business command mutation
```

## 2. Scope

In scope:

```text
- Login rejects PendingActivation accounts.
- Login does not issue cookie for inactive accounts.
- Current-user/session behavior remains safe.
- Protected client commands/read pages rely on active account guard.
- ProblemDetails code is stable enough for client UI to show activation-required message.
```

## 3. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Creating pending account/token | `SL-AUTH-ACT-001.server` |
| Activation endpoint | `SL-AUTH-ACT-002.server` |
| Limited inactive session | Future UX/security slice |
| AccountActivated authorization policy/claim optimization | Future security slice |
| Employee Windows activation | Future employee auth slice |
| Client pages/UI | `SL-AUTH-ACT-001.client` |

## 4. Scenario Flow

Pending login:

```text
Guest enters valid credentials
      ↓
System finds account
      ↓
System verifies password
      ↓
Account state is PendingActivation
      ↓
System returns activation-required ProblemDetails
      ↓
System does not issue session
```

Active login:

```text
Guest enters valid credentials
      ↓
System finds active account
      ↓
System verifies password
      ↓
System issues session cookie
```

Protected command:

```text
Authenticated command starts
      ↓
System resolves current account
      ↓
Account active?
  ├─ no → reject before mutation
  └─ yes → continue use case
```

## 5. Implementation Flow

Login handler/controller direction:

```text
LoginClientAccountHandler
  -> load account by email
  -> verify password
  -> account.EnsureActivated()
  -> on failure return activation-required
  -> create principal + sign-in only after activation check
```

Current-user direction:

```text
GET /api/auth/current-user
  -> if session references missing/inactive account:
       return 401 or 403 activation-required depending chosen session policy
  -> first pass should usually be 401 because pending accounts do not get cookies.
```

Protected use cases:

```text
application/auth boundary
  -> load current account
  -> account.EnsureActivated()
  -> reject before command body mutation
```

## 6. API Contract

Login endpoint:

```text
POST /api/auth/login
```

Pending account failure:

```text
403 Forbidden
application/problem+json
code: auth.account.activation_required
title: Account activation required
detail: Activate your account using the email link before signing in.
```

No cookie:

```text
Set-Cookie auth session must not be present for activation-required response.
```

Current-user:

```text
GET /api/auth/current-user
```

Current-user recommended first pass:

```text
401 Unauthorized for no cookie.
403 activation-required only if an inactive session can exist from older/stale cookies.
```

OpenAPI impact:

```text
ProblemDetails status documentation may change.
No DTO shape change required unless current-user active-state semantics are documented more explicitly.
```

## 7. Validation / FluentValidation

Not primarily FluentValidation.

Authentication/application validation:

```text
missing credentials
invalid credentials
account inactive/pending
stale account/session
```

Error mapping:

```text
invalid credentials -> existing login error
pending account -> activation-required problem
stale/missing session -> unauthorized
```

## 8. Domain Rules

```text
- Account.EnsureActivated fails for PendingActivation.
- Protected use cases must call active-account guard before mutating.
- Business aggregates assume active actor context after guard.
```

Do not place activation checks inside:

```text
ApplicantParty.Create
ConnectionRequest.Create
AgreementProposalExchange.StartByEmployee
```

## 9. Application / Handler Direction

Recommended shared service:

```text
ICurrentAccountGuard
  GetActivatedClientAccount(...)
  GetActivatedEmployeeAccount(... if needed later)
```

First pass may use existing account repository in handlers if a shared guard is too broad, but must avoid copy/paste drift.

## 10. Security / Protection

```text
- Do not issue auth cookie before activation.
- Do not convert pending account into Active during login.
- Do not allow protected command partial mutation before active check.
- Do not expose unrelated resource data in activation-required responses.
```

## 11. Behavior Coverage

| Behavior item | Scenario/source meaning | Covered by this slice? | Notes |
|---|---|---:|---|
| AUTH-ACT-GUARD-001 | Pending account login rejected | yes | central first-pass UX |
| AUTH-ACT-GUARD-002 | No cookie for pending login | yes | security |
| AUTH-ACT-GUARD-003 | Protected commands reject inactive account | yes | no mutation |
| AUTH-ACT-GUARD-004 | Current-user safe | yes | stale/session guard |
| AUTH-ACT-GUARD-005 | Active account still works | yes | regression proof |

## 12. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| AUTH-ACT-GUARD-001 | Pending account login returns activation-required problem | API integration | seed pending account, POST login | Low | Low | `Login_PendingAccount_ReturnsActivationRequired` |
| AUTH-ACT-GUARD-002 | Pending login response has no auth cookie | API integration | inspect Set-Cookie/current-user | Low | Low | `Login_PendingAccount_DoesNotIssueCookie` |
| AUTH-ACT-GUARD-003 | Pending account cannot create ApplicantParty/request | API integration + no-mutation DB assertion | auth fixture or stale cookie setup | Medium: fixture must represent realistic inactive account | Medium | `ProtectedCommand_PendingAccount_ReturnsForbiddenAndDoesNotMutate` |
| AUTH-ACT-GUARD-004 | Stale inactive session does not become usable | API integration | seed inactive account with cookie/claims | Medium | Medium | `CurrentUser_InactiveAccountSession_ReturnsUnauthorizedOrActivationRequired` |
| AUTH-ACT-GUARD-005 | Active account login and protected flow still works | API integration | register/activate or seed active | Low | Low | existing login/protected tests updated |

## 13. OpenAPI / Generated Artifacts

Likely no DTO shape change.

If controller attributes add documented 403 activation-required:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix .\energymanagement.client run generate:api-types
```

## 14. Implementation Checklist

```text
[ ] Add/confirm Account.EnsureActivated behavior for PendingActivation.
[ ] Update login flow to check activation before SignInAsync.
[ ] Add activation-required ProblemDetails code.
[ ] Confirm no cookie is issued on pending login.
[ ] Add stale/inactive current-user behavior.
[ ] Add active-account guard before protected client commands.
[ ] Add integration tests and no-mutation checks.
[ ] Regenerate OpenAPI/types if documented statuses change.
```

This pass did not perform implementation verification.

## 15. Guardrail Summary

```text
- Reject pending login.
- No limited session.
- No cookie for inactive account.
- Protected commands check active before mutation.
- Business aggregates do not own activation checks.
```
