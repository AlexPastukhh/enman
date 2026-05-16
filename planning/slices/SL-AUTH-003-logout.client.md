# SL-AUTH-003.client — Logout Client Sidecar

Status: implementation-ready client draft / UI not confirmed implemented  
Parent backend slice: `SL-AUTH-003 — Logout Client Account`  
Slice type: client sidecar / auth command UI  
Scope: logout UI/action, session/current-user state update, post-logout navigation to Home, logout error feedback  
Contract source: `POST /api/l1/auth/logout`, generated operation `L1Logout`, success `204 No Content`, possible `401`  
Source behavior items: `Source BI TBD` from `SL-AUTH-003`; dedicated scenario/UI behavior IDs are not attached yet.

This file promotes the early short draft into a full client sidecar draft.

It does not claim logout UI is implemented.

## 1. Sidecar Overview

Target client behavior:

```text
Authenticated L1 client chooses Logout
        ↓
Client calls POST /api/l1/auth/logout
        ↓
204 success or 401 stale-session response
        ↓
Client clears auth/session state
        ↓
Client navigates to Home / public home
        ↓
Authenticated UI is no longer shown
```

Unexpected failure behavior:

```text
network/server/unexpected failure
        ↓
show action/global feedback
        ↓
do not falsely show successful logout
```

Out of scope:

```text
- backend logout implementation;
- CSRF implementation;
- broad auth hardening;
- registration/login form changes;
- applicant/request flows;
- browser E2E until auth UI/session flow is stable;
- full feedback-message infrastructure implementation.
```

## 2. Sources / Source Behavior Items

Sources:

```text
planning/slices/SL-AUTH-003-logout.md
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Temporary behavior items:

```text
Source BI TBD — Authenticated L1 client can end session.
Source BI TBD — Current-user after logout is Unauthorized.
UI BI TBD — Authenticated user can trigger logout.
UI BI TBD — After logout, user is shown as guest/public user.
UI BI TBD — After logout, user lands on Home / public home.
UI BI TBD — No logout success message is required.
UI BI TBD — Unexpected logout failure is visible and does not falsely show success.
```

Source behavior item IDs are not fully attached yet.

Before implementation finalization, attach real scenario/UI behavior IDs or keep the gap explicit.

## 3. Visual UI / Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Authenticated L1 client                      │
│ wants to end the current session             │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Client chooses Logout                        │
│ source behavior: end authenticated session   │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ System attempts to end session               │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
 session ended      session already invalid
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ User becomes guest    │   │ User is treated as guest      │
│ authenticated UI ends │   │ stale client auth is cleared  │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           └──────────────┬───────────────┘
                          ▼
┌──────────────────────────────────────────────┐
│ User lands on Home / public home             │
│ No logout success message is required        │
└──────────────────────────────────────────────┘

Failure branch:

┌──────────────────────────────────────────────┐
│ Logout cannot be confirmed                   │
│ network / server / unexpected failure        │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ User sees logout error feedback              │
│ Client does not falsely show success outcome │
└──────────────────────────────────────────────┘
```

Not scenario-confirmed yet:

```text
- exact UI placement: header, account menu, profile page, etc.;
- exact error copy;
- whether the error is local action feedback or global feedback surface.
```

## 4. UI Slice Flow

| Step | UI behavior | Source / decision | Status |
|---|---|---|---|
| F01 | Authenticated UI exposes a logout action. | UI BI TBD | target |
| F02 | User activates logout. | UI BI TBD | target |
| F03 | Client calls logout API. | generated `L1Logout` contract | target |
| F04 | 204 success clears auth/session state. | backend contract + auth sidecar direction | target |
| F05 | 401 stale-session response also clears auth/session state. | accepted direction | target |
| F06 | User lands on Home / public home. | `SL-AUTH-Q-006` | accepted direction |
| F07 | No logout success message is required. | `SL-AUTH-CLIENT-Q-002` | accepted direction |
| F08 | Unexpected failure shows action/global feedback and avoids false success. | CL-FEEDBACK-001 | target |

## 5. Visual Client Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ UI placement                                 │
│ authenticated shell / header / account menu  │
│ Logout action                                │
└──────────────┬───────────────────────────────┘
               │ user activates action
               ▼
┌──────────────────────────────────────────────┐
│ Feature command layer                        │
│ logout action / mutation boundary            │
│ optional visible pending state               │
└──────────────┬───────────────────────────────┘
               │ uses shared L1 auth API boundary
               ▼
┌──────────────────────────────────────────────┐
│ Shared API / generated contract              │
│ shared/api/l1AuthApi.ts                      │
│ POST /api/l1/auth/logout                     │
│ operation: L1Logout                          │
│ no request body / no success body required   │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┼─────────────────────┐
       │       │                     │
     204      401                 other error
       │       │                     │
       ▼       ▼                     ▼
┌──────────────────────┐ ┌──────────────────────┐ ┌──────────────────────────┐
│ Confirmed logout      │ │ Stale/invalid session│ │ Unexpected failure       │
│ success               │ │ treat as guest       │ │ map to feedback message  │
└──────────┬───────────┘ └──────────┬───────────┘ └────────────┬─────────────┘
           │                        │                          │
           └────────────┬───────────┘                          │
                        ▼                                      ▼
┌──────────────────────────────────────────────┐   ┌──────────────────────────┐
│ Session/query state                          │   │ Feedback concern          │
│ remove/invalidate current-user session       │   │ action/global error       │
│ invalidate known user-scoped queries         │   │ no false success state    │
└──────────────┬───────────────────────────────┘   └──────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Router/navigation                            │
│ navigate to Home / public home               │
└──────────────────────────────────────────────┘
```

## 6. Client Implementation Flow

| Layer | Responsibility | Current direction | Status |
|---|---|---|---|
| UI placement | Provide logout action in authenticated UI. | header/shell/account menu to be decided. | open placement |
| Feature command | Own logout mutation/pending/error boundary. | local feature or auth feature boundary. | target |
| Shared API | Call generated logout endpoint through existing auth API wrapper. | use `l1AuthApi.logoutClientAccount`. | support exists |
| Session/cache | Clear/invalidate current-user session and known user-scoped queries. | minimum current-user/session invalidation. | assumption |
| Feedback | Show unexpected failure; no required success message. | use CL-FEEDBACK-001. | target |
| Router | Navigate Home/public home after success/stale-session. | `clientRoutes.home`. | accepted direction |
| CSRF | Do not implement custom CSRF in logout feature. | consume future CC-CSRF-001. | deferred |

## 7. Client API / Generated Contract

| Client API function | Endpoint | Generated OpenAPI type(s) used | Response body required? | Error handling | Status |
|---|---|---|---|---|---|
| `logoutClientAccount()` | `POST /api/l1/auth/logout` | `operations["L1Logout"]` | no | 204 success, 401 stale/already unauthenticated, unexpected error feedback | shared API support exists; UI integration target |

Implementation notes:

```text
POST /api/l1/auth/logout returns 204 No Content on success.
Client implementation treats HTTP success as enough and does not require a response body.
This is a contract/implementation rule, not Behavior Coverage.
Logout action should use the shared L1 auth API boundary rather than inventing route/DTO details locally.
```

## 8. Questions / Decisions

Open / assumption / future-review items first:

| ID | Question status | Question | Assumption / current direction | Impact | Shared register |
|---|---|---|---|---|---|
| `SL-AUTH-Q-008` | assumption | Should logout clear only auth state or all user-scoped query caches? | Minimum: remove/invalidate current-user/session query. Preferred: also invalidate known user-scoped queries. | stale user data prevention | `slice-questions-register.md` |
| `SL-AUTH-CLIENT-Q-003` | assumption | How should unexpected logout failure be shown? | Use shared feedback direction: action/global error; do not show success. | feedback concern / tests | `slice-questions-register.md`; `CL-FEEDBACK-001` |
| `SL-AUTH-CLIENT-Q-004` | open | Should logout show a visible pending/loading state? | Only if chosen UI placement has a natural visible pending state. | component behavior/tests | local + register if placement affects shell |
| `SL-AUTH-Q-007` | open / future security | When should logout require antiforgery token handling? | Do not implement CSRF in this logout client slice now. Record dependency on `CC-CSRF-001`. | unsafe command security | `slice-questions-register.md` |

Accepted directions:

| ID | Question status | Question | Current direction | Impact |
|---|---|---|---|---|
| `SL-AUTH-Q-006` | accepted direction | Where should client navigate after logout? | Navigate to Home / public home. | router, tests, visible UX |
| `SL-AUTH-CLIENT-Q-001` | accepted direction | How should client handle `401` from logout? | Treat as already unauthenticated/stale session; clear local auth/session state and navigate Home. | stale session recovery |
| `SL-AUTH-CLIENT-Q-002` | accepted direction | Should logout show a success notification? | No required success message/toast. Home + guest/public state is the visible outcome. | UI copy/tests |

Accepted backend facts:

```text
- Logout endpoint is protected.
- Success returns 204 No Content.
- Backend clears cookie session.
- current-user after logout returns Unauthorized.
```

## 9. Client Extension / Change Points

| ID | Type | Area | Current direction | Register sync | Status |
|---|---|---|---|---|---|
| `CP-AUTH-LOGOUT-CACHE-001` | change point | cache invalidation | Convention-first: clear current-user/session; invalidate known user-scoped queries where available. | `slice-questions-register` / `SL-AUTH-Q-008` | assumption |
| `CP-AUTH-LOGOUT-CSRF-001` | security extension | CSRF | Anti-coupling only now; do not build custom CSRF inside logout feature. Use future shared CSRF support. | `SL-AUTH-Q-007`, `CC-CSRF-001` | deferred |
| `CP-AUTH-LOGOUT-NAV-001` | UX decision | post-logout route | Home / public home. | `SL-AUTH-Q-006` | accepted direction |
| `CP-AUTH-LOGOUT-FEEDBACK-001` | cross-cutting client concern | feedback messages | Logout consumes shared feedback direction for unexpected failure; no required success message. | `CL-FEEDBACK-001` | assumption |

## 10. Behavior Coverage

| Source behavior / UI behavior | How draft covers it | Draft location | Status |
|---|---|---|---|
| `Source BI TBD` — Authenticated L1 client can end session | Logout action lets authenticated user request session termination. | Visual UI / Scenario Flow | covered with temporary BI |
| `Source BI TBD` — Current-user after logout is Unauthorized | Client moves to guest/public state after confirmed logout or stale-session logout response. | Visual UI / Scenario Flow | covered with temporary BI |
| `UI BI TBD` — User can trigger logout from authenticated UI | Authenticated shell/header/account area exposes logout action. | Visual UI Flow / Client Implementation Flow | covered as draft direction |
| `UI BI TBD` — User is no longer shown authenticated/protected UI after logout | Client moves to guest/public state and navigates Home. | Visual UI / Scenario Flow | covered as draft direction |
| `UI BI TBD` — Logout success does not require success message | Home + guest state is enough visible outcome. | Questions / Decisions | covered as accepted direction |
| `UI BI TBD` — Logout failure is visible and does not falsely confirm success | Unexpected failure shows action/global feedback and avoids successful logout outcome. | Visual UI / Scenario Flow / Questions | covered as draft direction |
| CSRF unsafe command handling | Identified as future cross-cutting security concern, not current behavior coverage. | Client Extension / Change Points | deferred |

## 11. Client / Component / E2E Verification Plan

UX-level checks only; internal wrapper calls, response-body absence, duplicate-submit helpers and cache implementation mechanics are not primary behavior tests.

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Authenticated user can see and use logout action | Logout is available in authenticated UI context. | client/component | planned |
| Guest/public state does not show authenticated logout action | Logged-out user is not offered authenticated-only logout UI. | client/component | planned |
| Successful logout moves user to Home / public state | After successful logout, user lands on Home and authenticated UI is gone. | client/component/router/session | planned |
| Successful logout removes protected/authenticated UI from view | User is no longer shown as authenticated after logout. | client/component/session | planned |
| Stale-session logout response still moves user to guest/public state | If server responds `401`, client treats session as already invalid, shows public Home and does not keep stale authenticated UI. | client/component/router/session | planned |
| Unexpected logout failure shows visible feedback | Network/5xx/unexpected failure shows action/global error feedback and does not falsely show logout success. | client/component/feedback | planned |
| Optional: visible pending state appears while logout is in progress | Only if chosen UI exposes loading/disabled/progress state. | client/component | optional |
| Future E2E: user can login, logout and lose protected access | Full browser-client-server proof after auth UI/session flow is stable. | E2E | future |
| Future CSRF behavior | Token attach/failure/refetch behavior after CSRF implementation. | security/client/server | deferred |

## 12. Covered Scenario / UI Behavior Items

Temporary because source behavior IDs are not attached yet:

```text
Source BI TBD — Authenticated L1 client can end session.
Source BI TBD — Current-user after logout is Unauthorized.
UI BI TBD — Authenticated user can trigger logout.
UI BI TBD — After logout, user is shown as guest/public user.
UI BI TBD — After logout, user lands on Home / public home.
UI BI TBD — No logout success message is required.
UI BI TBD — Unexpected logout failure is visible and does not falsely show success.
```

Before implementation handoff, replace these with real scenario/UI behavior IDs if available, or keep them explicitly marked as temporary.

## 13. Dependent / Follow-up Slices

```text
CL-FEEDBACK-001 — Client Feedback Messages
CC-CSRF-001 — Antiforgery token/session context
future protected route / auth shell sidecar if logout placement depends on shell architecture
future auth E2E after client auth/session UI stabilizes
```

## 14. Implementation Checklist

Working defaults:

```text
[x] backend logout endpoint exists
[x] shared logout API wrapper exists
[ ] exact logout UI placement selected
[ ] logout action/mutation implemented
[ ] current-user/session query cleared or invalidated
[ ] known user-scoped queries invalidated if applicable
[ ] 204 success navigates Home/public home
[ ] 401 stale-session response clears local auth/session state
[ ] unexpected failure shows feedback and does not show false success
[ ] optional visible pending state decided
[ ] CSRF integration deferred to CC-CSRF-001
```
