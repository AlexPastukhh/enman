# L1-CLIENT-AUTH-SESSION-MIGRATION.client - Early Short Draft

Status: implementation started; legacy auth/session leftovers removed
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
[pages/register | pages/login | pages/account]
Composes layout and feature form or temporary account shell
        |
        v
[features/auth/register | features/auth/login]
Owns form schema, DTO mapping, submit behavior, root command error display
        |
        v
[shared/ui/form]
Provides reusable form primitives and shared form styling
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

Status: decided

Decision: the unreachable legacy auth/session chain was deleted after import scan confirmed active app/tests no longer use it.

```text
Deleted:
- src/views/**
- src/hooks/**
- src/MutationFns/**
- src/QueryFns/**
- src/Utils/**
- src/globConstants.ts
- src/Tests/UnitTests/handleServerErrors.test.ts
- src/Components/General/TextWrapper.tsx
```

The active app route wiring now uses only `app`, `pages`, `features`, `entities`, and `shared` for auth/session behavior.

### Q-CLIENT-AUTH-003 - Should session mapper fail hard on missing required current-user fields?

Status: decided

Decision: `mapCurrentUserToSession` now fails fast if authenticated current-user responses omit `accountId`, `email`, `role`, `isActive`, or `isAuthenticated`. A `401` current-user response still maps to unauthenticated session.

### Q-CLIENT-AUTH-004 - Should logout get visible UI now?

Status: decision: no in this cleanup

Decision: this cleanup keeps only the L1 logout API helper. No visible logout UI is added because this task migrates existing auth/session logic only.

### D-CLIENT-AUTH-001 - L1 register DTO excludes password confirmation

Status: decided

Register form keeps password confirmation as client-only validation state. The L1 API request sends only `email` and `password`.

### D-CLIENT-AUTH-002 - Legacy route constants remain outside migrated flows

Status: decided

`globConstants.ts` and legacy `constants.Routes` may remain for unmigrated code. Migrated auth/session flows use `shared/config/clientRoutes.ts` and `shared/api/l1ApiPaths.ts`.

### D-CLIENT-AUTH-003 - Root command errors are feature-owned

Status: decided

Register and login command root errors are rendered inside `RegisterForm` and `LoginForm`. Pages compose layout only and no longer receive `setRootError` from auth features.

### D-CLIENT-AUTH-004 - L1 API path constants are OpenAPI-key typed

Status: decided

`shared/api/l1ApiPaths.ts` keeps explicit path strings but constrains them with `satisfies Record<string, keyof paths>` from generated OpenAPI types. This reduces drift without introducing a full generated client.

### D-CLIENT-AUTH-005 - Shared form primitives moved to shared UI

Status: decided

Reusable form primitives live under `shared/ui/form`. Shared form CSS lives next to those primitives. Register-specific form placement styling lives in `features/auth/register/ui/registerForm.css`.

### D-CLIENT-AUTH-006 - globConstants removed

Status: decided

`globConstants.ts` was deleted after the remaining consumers were identified as part of the unreachable legacy chain. Current client routes live in `shared/config/clientRoutes.ts`, generated constants live in `shared/constants/generatedConstants.ts`, API paths live in `shared/api/l1ApiPaths.ts`, and session query keys live in `entities/session/model/sessionKeys.ts`.

### D-CLIENT-AUTH-007 - AccountPage remains a temporary L1 shell

Status: decided

`pages/account/AccountPage.tsx` remains a simple L1 account shell that reads the migrated session entity and shows basic session output. Applicant party UI belongs to a future L1 applicant party client sidecar and is not implemented here.

### Q-CLIENT-AUTH-005 - Should layout components move out of `Components/Layout`?

Status: open / follow-up

Current note: active pages still import `Components/Layout/Header`, `Footer`, and related layout pieces. They are retained for now and should be moved to `shared/ui/layout` in a separate layout cleanup.

## 4. Behavior Coverage

Behavior Coverage is not Test Coverage. This table explains how the implementation covers the intended behavior. Verification is listed separately.

| Behavior item | Implementation coverage |
|---|---|
| Source BI TBD: visitor can register with valid credentials and continue to login | `features/auth/register` validates the existing form fields, maps form values to `L1RegisterClientAccountDto`, calls `POST /api/l1/auth/register`, and navigates to `clientRoutes.login` on success. |
| Source BI TBD: registered client can login and establish session state | `features/auth/login` maps form values to `L1LoginRequest`, calls `POST /api/l1/auth/login`, invalidates the session query, and navigates home on success. |
| Source BI TBD: unauthenticated current-user lookup does not become a fatal page error | `entities/session/api/getCurrentSession.ts` treats `401` from `GET /api/l1/auth/current-user` as `null` session. |
| Source BI TBD: server validation errors are shown as field/root errors | `shared/api/problemDetails.ts` reads generated ProblemDetails constants and maps server validation errors through feature field maps. |
| Source BI TBD: root form errors are visible without page coupling | `RegisterForm` and `LoginForm` render `errors.root?.message` inside the feature form with `role="alert"`. |
| Source BI TBD: reusable form UI follows shared placement | `shared/ui/form` owns reusable form primitives and shared form CSS; auth features import form primitives from the shared layer. |
| Source BI TBD: L1 current-user contract issues are not silently hidden | `mapCurrentUserToSession` throws when authenticated current-user responses miss required fields. |
| Source BI TBD: migrated auth/session route wiring is isolated from legacy client code | Legacy `views`, `hooks`, `MutationFns`, `QueryFns`, `Utils`, and `globConstants.ts` were removed after import scan. Active route wiring uses `app/pages/features/entities/shared`. |
| Source BI TBD: account route is present but does not start future applicant work | `AccountPage` stays a temporary session-backed shell and does not implement applicant party UI. |

## 5. Client / Component / E2E Verification Plan

| Verification area | Plan |
|---|---|
| Build/typecheck | Run `npm.cmd --prefix energymanagement.client run build`. |
| Component/client tests | Keep role/label-facing register/login validation and submit tests. |
| Session API behavior | Test that current-user `401` maps to unauthenticated session. |
| Session contract safety | Test that missing required current-user fields throw a contract error. |
| E2E register | Browser submits the register form and waits for real `POST /api/l1/auth/register`. |
| E2E login | Test setup creates an L1 account, browser submits login, and waits for real `POST /api/l1/auth/login`. |
| API contract | Run `npm.cmd run check:api` to confirm OpenAPI/types are current. |
| Legacy cleanup | Run import scans for legacy names and verify build/tests/E2E after deletion. |

## 6. Covered Scenario / UI Behavior Items

- Source BI TBD: visitor can register with valid credentials and continue to login.
- Source BI TBD: registered client can login and establish session state.
- Source BI TBD: unauthenticated current-user lookup does not become a fatal page error.
- Source BI TBD: server validation errors are shown as field/root errors.
- Source BI TBD: root form errors are visible without page coupling.
- Source BI TBD: reusable form UI follows shared placement.
- Source BI TBD: L1 current-user contract issues are not silently hidden.
- Source BI TBD: migrated auth/session route wiring is isolated from legacy client code.
- Source BI TBD: account route is present but does not start future applicant work.

## 7. Next Step

Replace temporary `Source BI TBD` labels with authoritative behavior IDs, then move retained layout components from `Components/Layout` to `shared/ui/layout` in a separate cleanup. Future applicant party UI should get its own L1 client sidecar.
