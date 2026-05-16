# SL-AUTH-003 — Logout Client Account

Status: implemented backend/API/session slice  
Package: `[L1]`  
Source scenario: auth/session termination and protected-client-flow planning  
Slice type: backend / API / session command slice with dependent client auth sidecar  
Current implementation status: implemented and integration-tested; concrete client logout/session invalidation flow is implemented in `SL-AUTH-003-logout.client.md`

## 1. Slice Overview

Observable behavior:

```text
Authenticated L1 client requests logout.
System clears the cookie authentication session.
After logout, current-user no longer returns authenticated user data.
```

Backend scope implemented by this slice:

```text
protected L1 logout endpoint
-> cookie auth required
-> cookie sign-out
-> 204 No Content response
-> current-user becomes Unauthorized after logout
```

Current implementation evidence checked for this reconciliation:

```text
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Out of this backend slice:

```text
- CSRF token clearing/refetch mechanics;
- broad antiforgery implementation.
```

## 2. Sources / Source Behavior Items

Scenario / planning sources:

```text
planning/api/client-server-contract-principles.md
planning/api/api-error-contract.md
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
planning/slices/slice-questions-register.md
planning/slices/slice-implementation-notes-register.md
```

Behavior items covered by this backend slice:

```text
Source BI TBD — Authenticated L1 client can end session.
Source BI TBD — Current-user after logout is Unauthorized.
```

Source behavior item IDs are not fully attached yet.

Temporary Source BI TBD labels are used for this documentation pass.

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Authenticated L1 Client                      │
│ requests logout                              │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ L1 System                                    │
│ requires authenticated session               │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
 authenticated       not authenticated
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Clear cookie session  │   │ Unauthorized                  │
└──────────┬───────────┘   └──────────────────────────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ 204 No Content                               │
│ subsequent current-user is Unauthorized      │
└──────────────────────────────────────────────┘

Dependent / out-of-scope:
- CSRF token lifecycle.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Requests logout. | Source BI TBD | source behavior |
| F02 | API/Auth boundary | Requires authenticated cookie. | auth/session boundary | backend/API slice |
| F03 | System | Clears cookie authentication session. | current implementation | backend/API/session slice |
| F04 | API | Returns 204 No Content. | API contract | backend/API slice |
| F05 | Client/API caller | Current-user after logout is Unauthorized. | integration test | backend observable behavior |
| F06 | Client UI | Clears client auth state and navigates. | `SL-AUTH-003-logout.client.md` | implemented client sidecar |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ [Authorize] POST /api/l1/auth/logout         │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ ASP.NET Core Authentication                  │
│ HttpContext.SignOutAsync(cookie scheme)      │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ API response                                 │
│ 204 No Content                               │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Follow-up check                              │
│ GET current-user returns 401                 │
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current implementation note |
|---|---|---|---|
| I01 | API Controller | Expose protected logout endpoint. | `[Authorize] POST /api/l1/auth/logout`. |
| I02 | Authentication boundary | Clear cookie authentication session. | `HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)`. |
| I03 | API response | Return 204. | `NoContent()`. |
| I04 | Client follow-up | Client clears local auth state and navigates. | Implemented in `SL-AUTH-003-logout.client.md`. |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO / Body | Statuses | ProblemDetails statuses | Auth/session behavior | Contract status | OpenAPI exposed? | Generated TS types |
|---|---|---|---|---|---|---|---|---|
| `/api/l1/auth/logout` | POST | none | none | 204, 401 | 401 | requires cookie auth; clears cookie session | target L1 | yes | `operations["L1Logout"]` |

Security note:

```text
Logout is an unsafe browser command. Current slice documents backend behavior only.
Concrete client/security work should follow CC-CSRF-001 for antiforgery token lifecycle and unsafe request protection.
```

## 8. Questions / Decisions

Open questions and future review items:

| ID | Area | Question status | Question | Assumption / current direction | Shared register |
|---|---|---|---|---|---|
| SL-AUTH-Q-006 | Client logout UX | resolved | Where should the client navigate after logout? | Client navigates to Home / public home. | `slice-questions-register.md` |
| SL-AUTH-Q-007 | CSRF/security | open | When should logout require antiforgery token handling? | Draft assumes CC-CSRF-001 governs concrete unsafe-browser command handling. | `slice-questions-register.md`; possibly `slice-extension-points-register.md` |
| SL-AUTH-Q-008 | Client cache | resolved for current known caches | Should logout clear only auth state or also invalidate all user-scoped query caches? | Client clears session query and removes current applicant-party query; broader cache policy remains future convention work. | `slice-implementation-notes-register.md` |

Accepted decisions:

```text
Decision:
Logout clears the cookie authentication session and returns 204 No Content.

Decision:
After logout, current-user returns Unauthorized.
```

## 9. Behavior Coverage

| Scenario behavior item | How slice covers it | Draft/file location | Status |
|---|---|---|---|
| Source BI TBD — Authenticated L1 client can end session | Protected logout endpoint signs out cookie session. | Scenario Flow / Implementation Flow | covered |
| Source BI TBD — Current-user after logout is Unauthorized | Integration test calls current-user after logout and expects 401. | Test Plan | covered |
| Client post-logout UX | Header logout action clears session state, removes known applicant-party query, navigates Home and handles unexpected failure with visible feedback. | `SL-AUTH-003-logout.client.md` | covered |
| CSRF logout protection | Identified as unsafe command concern. | API Contract / Questions | deferred to CC-CSRF/client-security work |

## 10. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `L1Logout_AfterLogin_RemovesCurrentUserSession` | Logout returns 204 and current-user becomes 401. | API + session | implemented |
| Generated OpenAPI type check | Logout path/status remains generated. | Tooling/API contract | available through API check workflow |
| Client logout component tests | Button/menu action, pending/error state, cache clearing and navigation. | Client/component | implemented |
| Logout browser E2E | User can login, logout and loses protected access. | Browser + client + server | future dedicated E2E |
| CSRF logout tests | Unsafe logout request requires token when CSRF is implemented. | Security/client/server | deferred |

## 11. Dependent / Follow-up Slices

```text
[CLIENT][DEPENDENT] L1 auth/session client baseline
[CLIENT][DEPENDENT] Logout UI/action
[SECURITY][CROSS-CUTTING] CC-CSRF-001 unsafe browser command protection
[TESTING][FUTURE] auth browser E2E after client auth UI exists
```

## 12. Implementation Checklist

```text
[x] POST /api/l1/auth/logout exists
[x] endpoint is protected by authorization
[x] cookie sign-out is called
[x] response is 204 No Content
[x] current-user after logout returns Unauthorized
[x] integration test covers logout behavior
[x] client logout UI/session state sidecar
[x] cache invalidation/navigation decision
[ ] CSRF token handling for unsafe browser commands
[ ] browser E2E after client auth UI exists
```
