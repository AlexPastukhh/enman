# SL-AUTH-003-logout.client — Logout Client Sidecar

Status: implemented client sidecar  
Parent backend slice: `SL-AUTH-003 — Logout Client Account`  
Slice type: client sidecar / auth command UI  
Scope: logout UI action, current-user/session state update, post-logout navigation to Home, visible logout error feedback  
Contract source: `POST /api/l1/auth/logout`, generated operation `L1Logout`, success `204 No Content`, possible `401`  
Source scenario/UI behavior items: temporary `Source BI TBD` / `UI BI TBD`; authoritative source IDs are not attached yet.

## 1. Visual UI / Scenario Flow

```text
Authenticated L1 client
        ↓
sees Logout action in authenticated header UI
        ↓
activates Logout
        ↓
client calls POST /api/l1/auth/logout
        ↓
204 success or 401 stale-session response
        ↓
client clears local auth/session state
        ↓
client navigates to Home / public home
        ↓
guest/public UI is visible
```

Unexpected failure branch:

```text
logout request fails unexpectedly
        ↓
client shows visible action error feedback
        ↓
client does not navigate as if logout succeeded
        ↓
authenticated UI remains until logout is confirmed or session is otherwise cleared
```

Out of scope:

```text
- backend logout implementation;
- CSRF implementation;
- global notification architecture;
- applicant/request flows;
- dedicated logout browser E2E scenario.
```

## 2. Visual Client Implementation Flow

```text
shared UI header
  Header
  authenticated session exists
        ↓
features/auth/logout
  LogoutButton
  useLogoutAction
        ↓
shared API boundary
  logoutClientAccount()
  POST /api/l1/auth/logout
        ↓
response handling
  204 -> confirmed logout
  401 -> stale/already invalid session
  other -> visible action error
        ↓
session/cache behavior
  set current-user session query to null
  remove known applicant-party current query
        ↓
router behavior
  navigate to clientRoutes.home
```

The client does not depend on a success response body. HTTP success is enough for this command.

## 3. Questions / Decisions

Open / future-review items first:

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `SL-AUTH-Q-007` | open / future security | When should logout require antiforgery token handling? | Defer to `CC-CSRF-001`; do not implement custom CSRF inside this logout client slice. | unsafe command security |
| `SL-AUTH-CLIENT-Q-004` | resolved for current UI | Should logout show visible pending/loading state? | Header button shows a disabled pending label while the command is in flight. | component UX |
| `SL-AUTH-CLIENT-Q-003` | resolved for current UI | How should unexpected logout failure be shown? | Show local action feedback with `role="alert"` and keep current route/auth UI. | feedback/tests |

Accepted / implemented decisions:

| ID | Status | Question | Decision |
|---|---|---|---|
| `SL-AUTH-Q-006` | resolved | Where should client navigate after logout? | Home / public home. |
| `SL-AUTH-Q-008` | resolved for current known caches | Should logout clear only auth state or all user-scoped query caches? | Clear current-user/session state and remove the current applicant-party query. Broader cache policy can evolve when more user-scoped queries exist. |
| `SL-AUTH-CLIENT-Q-001` | resolved | How should client handle `401` from logout? | Treat as already unauthenticated/stale session, clear local state and navigate Home. |
| `SL-AUTH-CLIENT-Q-002` | resolved | Should logout show a success notification? | No required success toast/message; Home + guest/public state is the visible outcome. |

## 4. Client Extension / Change Points

| ID | Type | Area | Current implementation | Future pressure |
|---|---|---|---|---|
| `CP-AUTH-LOGOUT-CACHE-001` | change point | cache invalidation | `sessionQueryKey` is set to `null`; current applicant-party query is removed. | Add a shared user-scoped cache convention when more protected read queries exist. |
| `CP-AUTH-LOGOUT-CSRF-001` | security extension | CSRF | No custom CSRF work in this slice. | Integrate with `CC-CSRF-001`. |
| `CP-AUTH-LOGOUT-FEEDBACK-001` | feedback | action error | Logout failure uses local `role="alert"` feedback. | Move to a shared feedback host only when multiple shell actions need it. |

## 5. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Source behavior / UI behavior | How implementation covers it | Location | Status |
|---|---|---|---|
| `Source BI TBD` — Authenticated L1 client can end session | Authenticated header exposes a Logout action that calls the L1 logout endpoint. | `Header`, `LogoutButton`, `useLogoutAction` | covered with temporary BI |
| `Source BI TBD` — Current-user after logout is Unauthorized | Client clears local current-user/session state after 204 or 401 logout response. | `useLogoutAction` | covered with temporary BI |
| `UI BI TBD` — Guest/public state does not show logout action | Header renders Logout only when session exists. | `Header` | covered |
| `UI BI TBD` — User lands on Home after logout | 204 and 401 branches navigate to `clientRoutes.home`. | `useLogoutAction` | covered |
| `UI BI TBD` — No success message is required | Success path clears/navigates without toast. | `useLogoutAction` | covered |
| `UI BI TBD` — Unexpected logout failure is visible and does not falsely confirm success | Unexpected error sets local alert feedback and does not navigate. | `LogoutButton`, `useLogoutAction` | covered |
| CSRF unsafe command handling | Identified as future cross-cutting security concern. | Questions / Change Points | deferred |

## 6. Client / Component / E2E Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Authenticated user sees Logout | Logout exists only in authenticated UI context. | client/component | implemented |
| Guest state does not show Logout | Public UI avoids authenticated-only logout action. | client/component | implemented |
| Successful logout navigates Home and hides Logout | 204 success clears auth UI and routes Home. | client/component/router/session | implemented |
| Logout `401` navigates Home and hides Logout | Stale session is treated as already unauthenticated. | client/component/router/session | implemented |
| Unexpected logout failure shows alert and stays put | Failure is visible and does not falsely show success. | client/component/feedback | implemented |
| Existing auth E2E list/run | Existing register/login E2E remains the cross-layer baseline. | E2E | run separately |
| Dedicated logout E2E | Login -> logout -> protected UI loss. | E2E | future |
| CSRF logout tests | Token attach/failure/refetch behavior. | security/client/server | deferred |

## 7. Covered Scenario / UI Behavior Items

Temporary because source behavior IDs are not attached yet:

```text
Source BI TBD — Authenticated L1 client can end session.
Source BI TBD — Current-user after logout is Unauthorized.
UI BI TBD — Authenticated user can trigger logout.
UI BI TBD — Guest/public user does not see logout.
UI BI TBD — After logout, user lands on Home / public home.
UI BI TBD — No logout success message is required.
UI BI TBD — Unexpected logout failure is visible and does not falsely show success.
```

## 8. Implementation Status

```text
[x] backend logout endpoint exists
[x] shared logout API wrapper exists
[x] logout UI placement selected: authenticated header actions
[x] logout action implemented
[x] current-user/session query is cleared
[x] known current applicant-party read query is removed
[x] 204 success navigates Home/public home
[x] 401 stale-session response clears local auth/session state
[x] unexpected failure shows feedback and does not show false success
[x] visible pending state exists through disabled button and pending label
[ ] CSRF integration deferred to CC-CSRF-001
[ ] dedicated browser E2E deferred
```
