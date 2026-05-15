# L1-CLIENT-AUTH-SESSION-MIGRATION.client — Early Short Draft

Status: implementation started
Slice type: client sidecar / client migration slice
Scope: existing register, login, current-user/session, logout API helper, and route/provider wiring
Source scenario/UI behavior items: Source BI TBD; currently derived from existing register/login UI behavior and L1 auth/session contract
Contract sources: `Shared/openapi.json`, `energymanagement.client/src/shared/api/generated/openapi-types.ts`, `Shared/constants.json`, `Shared/errorcodes.json`

## 1. Visual UI / Scenario Flow

```text
[Visitor]
Opens Register
        ↓
[Client]
Validates email/password/confirmation
        ↓
[Client]
Submits L1 register command
        ↓
[System]
Creates L1 account
        ↓
[Client]
Navigates to Login

[Visitor]
Opens Login
        ↓
[Client]
Validates email/password
        ↓
[Client]
Submits L1 login command
        ↓
[System]
Creates L1 session cookie
        ↓
[Client]
Refreshes session and navigates home
```

## 2. Visual Client Implementation Flow

```text
[app/App.tsx]
Mounts providers and router
        ↓
[pages/register | pages/login]
Composes layout and feature form
        ↓
[features/auth/register | features/auth/login]
Owns form schema, field mapping, submit behavior
        ↓
[shared/api/l1AuthApi.ts]
Calls /api/l1/auth/* with generated OpenAPI DTO types
        ↓
[shared/api/problemDetails.ts]
Normalizes ProblemDetails into field/root form errors
        ↓
[entities/session]
Reads /api/l1/auth/current-user and maps 401 to unauthenticated state
```

## 3. Questions / Decisions

- Legacy `globConstants.ts` remains temporarily for unmigrated code, but migrated auth/session flows no longer use legacy `constants.Routes`.
- Register form still has password confirmation as a client-only field. The L1 register DTO receives only `email` and `password`.
- Login uses the returned current-user response only as the mutation result and invalidates the session query after success.
- Logout has an API helper but no new visible UI in this step.

## 4. Behavior Coverage

- Register preserves the existing visible fields and deferred client validation.
- Register submits to `POST /api/l1/auth/register` and navigates to login on success.
- Login preserves the existing visible fields and validation.
- Login submits to `POST /api/l1/auth/login`, refreshes session state, and navigates home on success.
- Session reads `GET /api/l1/auth/current-user` and treats `401` as no authenticated session.
- Server `ProblemDetails` can map validation errors to field errors or root form errors.

## 5. Client / Component / E2E Verification Plan

- Component tests cover register/login visible validation and submit behavior through role/label-facing helpers.
- Session/API tests cover unauthenticated current-user handling.
- E2E register/login tests wait for real `/api/l1/auth/register` and `/api/l1/auth/login` responses.
- Build/typecheck verifies generated OpenAPI types are used at the API boundary.

## 6. Covered Scenario / UI Behavior Items

- Source BI TBD: visitor can register with valid credentials and continue to login.
- Source BI TBD: registered client can login and establish session state.
- Source BI TBD: unauthenticated current-user lookup does not become a fatal page error.

## 7. Next Step

Add typed client API wrappers and UI flow for L1 applicant party and connection request only after those screens are intentionally started.

