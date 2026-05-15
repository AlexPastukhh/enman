# L1-CLIENT-AUTH-SESSION-MIGRATION.client - Early Short Draft

Status: implementation started; cleanup pass completed
Slice type: client sidecar / client migration slice
Scope: existing register, login, current-user/session, logout API helper, and route/provider wiring
Source scenario/UI behavior items: Source BI TBD labels are temporary and must be replaced when authoritative source behavior IDs are identified.
Contract sources: `Shared/openapi.json`, `energymanagement.client/src/shared/api/generated/openapi-types.ts`, `Shared/constants.json`, `Shared/errorcodes.json`

## 1. Visual UI / Scenario Flow

```text
[Visitor]
Opens Register
        |
        v
[Client]
Validates email/password/confirmation
        |
        v
[Client]
Submits L1 register command
        |
        v
[System]
Creates L1 account
        |
        v
[Client]
Navigates to Login

[Visitor]
Opens Login
        |
        v
[Client]
Validates email/password
        |
        v
[Client]
Submits L1 login command
        |
        v
[System]
Creates L1 session cookie
        |
        v
[Client]
Refreshes session and navigates Home
```

## 2. Visual Client Implementation Flow

```text
[app/App.tsx]
Mounts providers and router
        |
        v
[pages/register | pages/login]
Composes layout and feature form
        |
        v
[features/auth/register | features/auth/login]
Owns form schema, DTO mapping, submit behavior
        |
        v
[shared/api/l1AuthApi.ts]
Calls /api/l1/auth/* with generated OpenAPI DTO types
        |
        v
[shared/api/problemDetails.ts]
Normalizes ProblemDetails into field/root form errors
        |
        v
[entities/session]
Reads /api/l1/auth/current-user and maps 401 to unauthenticated state
```

## 3. Questions / Decisions

### Q-CLIENT-AUTH-001 - Where are authoritative source scenario/UI behavior IDs?

Status: open

Current note: this sidecar uses temporary `Source BI TBD` labels. They must be replaced with authoritative scenario/UI behavior IDs when those IDs are available.

### Q-CLIENT-AUTH-002 - When should legacy views/hooks/MutationFns/QueryFns be deleted?

Status: open / follow-up

Current note: migrated route wiring no longer uses old register/login/session modules, but unmigrated legacy files still exist in `src/views`, `src/hooks`, `src/MutationFns`, and `src/QueryFns`. Deletion should happen after a dedicated import scan confirms no remaining build-time or planned compatibility dependency.

### Q-CLIENT-AUTH-003 - Should session mapper fail hard on missing required current-user fields?

Status: open / follow-up

Current note: `mapCurrentUserToSession` keeps conservative defaults for missing optional OpenAPI fields. A later hardening pass should decide whether missing `accountId`, `email`, `role`, `isActive`, or `isAuthenticated` should throw a contract error.

### Q-CLIENT-AUTH-004 - Should logout get visible UI now?

Status: decision: no in this cleanup

Decision: this cleanup keeps only the L1 logout API helper. No visible logout UI is added because this task migrates existing auth/session logic only.

### D-CLIENT-AUTH-001 - L1 register DTO excludes password confirmation

Status: decided

Register form keeps password confirmation as client-only validation state. The L1 API request sends only `email` and `password`.

### D-CLIENT-AUTH-002 - Legacy route constants remain outside migrated flows

Status: decided

`globConstants.ts` and legacy `constants.Routes` may remain for unmigrated code. Migrated auth/session flows use `shared/config/clientRoutes.ts` and `shared/api/l1ApiPaths.ts`.

## 4. Behavior Coverage

Behavior Coverage is not Test Coverage. This table explains how the implementation covers the intended behavior. Verification is listed separately.

| Behavior item | Implementation coverage |
|---|---|
| Source BI TBD: visitor can register with valid credentials and continue to login | `features/auth/register` validates the existing form fields, maps form values to `L1RegisterClientAccountDto`, calls `POST /api/l1/auth/register`, and navigates to `clientRoutes.login` on success. |
| Source BI TBD: registered client can login and establish session state | `features/auth/login` maps form values to `L1LoginRequest`, calls `POST /api/l1/auth/login`, invalidates the session query, and navigates home on success. |
| Source BI TBD: unauthenticated current-user lookup does not become a fatal page error | `entities/session/api/getCurrentSession.ts` treats `401` from `GET /api/l1/auth/current-user` as `null` session. |
| Source BI TBD: server validation errors are shown as field/root errors | `shared/api/problemDetails.ts` reads generated ProblemDetails constants and maps server validation errors through feature field maps. |
| Source BI TBD: root form errors are surfaced at page level | `RegisterForm` and `LoginForm` sync `errors.root?.message` to page-level error state after render with `useEffect`. |

## 5. Client / Component / E2E Verification Plan

| Verification area | Plan |
|---|---|
| Build/typecheck | Run `npm.cmd --prefix energymanagement.client run build`. |
| Component/client tests | Keep role/label-facing register/login validation and submit tests. |
| Session API behavior | Test that current-user `401` maps to unauthenticated session. |
| E2E register | Browser submits the register form and waits for real `POST /api/l1/auth/register`. |
| E2E login | Test setup creates an L1 account, browser submits login, and waits for real `POST /api/l1/auth/login`. |
| API contract | Run `npm.cmd run check:api` to confirm OpenAPI/types are current. |

## 6. Covered Scenario / UI Behavior Items

- Source BI TBD: visitor can register with valid credentials and continue to login.
- Source BI TBD: registered client can login and establish session state.
- Source BI TBD: unauthenticated current-user lookup does not become a fatal page error.
- Source BI TBD: server validation errors are shown as field/root errors.
- Source BI TBD: root form errors are surfaced at page level.

## 7. Next Step

Replace temporary `Source BI TBD` labels with authoritative behavior IDs, then migrate or delete the remaining legacy auth/session files in a focused cleanup once their remaining imports are fully understood.

