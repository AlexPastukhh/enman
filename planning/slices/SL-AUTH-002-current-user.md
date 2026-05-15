# SL-AUTH-002 — Current User

Status: implemented backend/API/session query slice  
Package: `[L1]`  
Source scenario: auth/session continuity and protected-client-flow planning  
Slice type: backend / API / session query slice with dependent client auth bootstrap sidecar  
Current implementation status: implemented and integration-tested; client bootstrapping/route guard remains future client work

## 1. Slice Overview

Observable behavior:

```text
Authenticated L1 client asks for current user.
System accepts only a valid L1-marked cookie session.
System returns current-user data for an existing account.
Missing/invalid/non-L1/legacy-shaped session returns Unauthorized.
```

Backend scope implemented by this slice:

```text
protected current-user endpoint
-> cookie auth required
-> L1 auth marker claim check
-> account id claim parse
-> account repository lookup
-> current-user response
-> Unauthorized for missing/invalid/legacy-shaped session
```

Current implementation evidence checked for this reconciliation:

```text
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Queries/L1GetCurrentUserHandler.cs
EnergyManagement.Server/L1/Application/Security/L1AuthClaimTypes.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Out of this backend slice:

```text
- client current-user query/cache implementation;
- route guard behavior;
- app bootstrap loading/error UI;
- current-user refetch strategy after login/logout;
- token refresh mechanics;
- global auth provider/client state design.
```

## 2. Sources / Source Behavior Items

Scenario / planning sources:

```text
planning/api/client-server-contract-principles.md
planning/api/api-error-contract.md
planning/slices/slice-questions-register.md
planning/slices/slice-implementation-notes-register.md
```

Behavior items covered by this backend slice:

```text
Source BI TBD — Authenticated L1 client can retrieve current user.
Source BI TBD — Missing/invalid session is rejected.
Source BI TBD — Legacy-shaped non-L1 cookie is rejected for L1 endpoints.
```

Source behavior item IDs are not fully attached yet.

Temporary Source BI TBD labels are used for this documentation pass.

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Browser / Client                             │
│ sends current-user request with cookies      │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ L1 System                                    │
│ requires authenticated cookie                │
│ requires L1 auth marker claim                │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
 valid L1 session   missing/legacy/invalid
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Load account by id    │   │ Unauthorized                  │
│ from session claim    │   │ no current user returned      │
└──────────┬───────────┘   └──────────────────────────────┘
           │
           ▼
┌──────────────────────────────────────────────┐
│ Account exists?                              │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Return current-user   │   │ Unauthorized                  │
│ response              │   │ stale/non-existing account    │
└──────────────────────┘   └──────────────────────────────┘
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Requests current L1 user. | Source BI TBD | source behavior |
| F02 | API/Auth boundary | Requires authenticated cookie. | auth/session boundary | backend/API slice |
| F03 | API/Auth boundary | Requires L1 marker claim. | current implementation | backend/API slice |
| F04 | API/Auth boundary | Parses current account id from claim. | current implementation | backend/API slice |
| F05 | Application query | Loads account by id. | current implementation | backend/application slice |
| F06 | API | Returns current-user response when account exists. | API contract | backend/API slice |
| F07 | API | Returns Unauthorized for missing/invalid/non-L1/stale account session. | auth/session safety | backend/API slice |
| F08 | Client | Uses current user for auth state/route guard. | dependent client sidecar | out of current backend slice |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ API Controller                               │
│ [Authorize] GET /api/l1/auth/current-user    │
└──────────────┬───────────────────────────────┘
               │ TryGetCurrentL1AccountId
               │ checks L1 marker + NameIdentifier
               ▼
┌──────────────────────────────────────────────┐
│ L1 account id available?                     │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Query Handler         │   │ API Controller                │
│ L1GetCurrentUser      │   │ 401 Unauthorized              │
└──────────┬───────────┘   └──────────────────────────────┘
           │ repository lookup
           ▼
┌──────────────────────────────────────────────┐
│ Account found?                               │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ API response          │   │ API Controller                │
│ 200 CurrentUser       │   │ 401 Unauthorized              │
└──────────────────────┘   └──────────────────────────────┘
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current implementation note |
|---|---|---|---|
| I01 | API Controller | Expose protected current-user endpoint. | `[Authorize] GET /api/l1/auth/current-user`. |
| I02 | API/Auth boundary | Check L1 marker and account id claim. | `TryGetCurrentL1AccountId(...)`. |
| I03 | API Controller | Reject missing/non-L1 marker. | Returns `Unauthorized()`. |
| I04 | Query Handler | Load account by current account id. | `L1GetCurrentUserHandler`. |
| I05 | API Controller | Reject missing account. | Query failure maps to `Unauthorized()`. |
| I06 | API response | Return `L1CurrentUserResponse`. | Account id, email, role, active, authenticated. |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO / Body | Statuses | ProblemDetails statuses | Auth/session behavior | Contract status | OpenAPI exposed? | Generated TS types |
|---|---|---|---|---|---|---|---|---|
| `/api/l1/auth/current-user` | GET | none | `L1CurrentUserResponse` | 200, 401, 500 | 401, 500 | requires valid L1 cookie marker/account claim | target L1 | yes | `operations["L1GetCurrentUser"]`, `components["schemas"]["L1CurrentUserResponse"]` |

## 8. Questions / Decisions

Open questions and future review items:

| ID | Area | Question status | Question | Assumption / current direction | Shared register |
|---|---|---|---|---|---|
| SL-AUTH-Q-004 | Client bootstrap | open | Should client call current-user on app bootstrap, protected route entry, or both? | Draft assumes this belongs to auth/session `.client.md`. | `slice-questions-register.md` |
| SL-AUTH-Q-005 | Error UX | open | How should client distinguish unauthenticated from server failure during current-user bootstrap? | Draft assumes Unauthorized clears/keeps guest state; 500/global failure needs separate UI handling. | `slice-questions-register.md` |

Accepted decisions:

```text
Decision:
L1 current-user rejects legacy-shaped cookies that do not carry the L1 auth marker.

Decision:
Missing/stale account behind a cookie claim returns Unauthorized.
```

## 9. Behavior Coverage

| Scenario behavior item | How slice covers it | Draft/file location | Status |
|---|---|---|---|
| Source BI TBD — Authenticated L1 client can retrieve current user | Endpoint checks L1 session and returns current-user response. | Scenario Flow / API Contract | covered |
| Source BI TBD — Missing/invalid session is rejected | Missing marker or bad claim returns Unauthorized. | Visual Scenario Flow / Test Plan | covered |
| Source BI TBD — Legacy-shaped cookie is rejected | L1 marker is required. | Implementation Flow / Test Plan | covered |
| Client auth bootstrap | Current-user endpoint is available for future client sidecar. | Dependent Slices | partially covered; client sidecar needed |

## 10. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `L1RegisterLoginAndCurrentUser_UsesL1AccountIdentity` | Login session current-user matches authenticated account. | API + session + persistence | implemented |
| `L1CurrentUser_WithoutAuth_ReturnsUnauthorized` | Missing auth is rejected. | API/auth boundary | implemented |
| `L1CurrentUser_WithNonExistingAccountClaim_ReturnsUnauthorized` | Stale/non-existing account claim is rejected. | API + query | implemented |
| `LegacyShapedCookie_WithExistingL1AccountId_IsRejected` | Legacy-shaped cookie without L1 marker is rejected. | API/security boundary | implemented |
| `L1Cookie_WithMarkerAndExistingAccount_Succeeds` | Valid L1 marker/account succeeds. | API/security boundary | implemented |
| Client auth bootstrap tests | App/route uses current-user response correctly. | Client/component | future client sidecar |

## 11. Dependent / Follow-up Slices

```text
[CLIENT][DEPENDENT] L1 auth/session client baseline
[CLIENT][DEPENDENT] Route guards/current-user cache
[SECURITY][FUTURE] broader cookie/session/CSRF hardening
```

## 12. Implementation Checklist

```text
[x] GET /api/l1/auth/current-user exists
[x] endpoint is protected by authorization
[x] L1 marker claim is required
[x] missing marker returns Unauthorized
[x] non-existing account claim returns Unauthorized
[x] valid L1 marker/account returns current-user response
[x] integration tests cover success/failure/session marker cases
[ ] client current-user bootstrap/route guard sidecar
[ ] current-user browser/client E2E after UI exists
```
