# SL-AUTH-001.client — Login Client Account

Status: implemented first-stage client feature flow / tests pending  
Parent slice: `planning/slices/SL-AUTH-001-login-client-account.md`  
Source scenario: client authentication / session start  
Slice type: client sidecar / auth login UI  
Current implementation status: implemented route/page/form/model/API mapping/session invalidation/navigation; component/E2E coverage not found in current inspection

## 1. Sidecar Overview

Implemented client behavior:

```text
Guest opens /login
        ↓
Client shows login form
        ↓
Guest enters email/password
        ↓
Client validates visible fields
        ↓
Client submits L1 login command
        ↓
 ┌──────────────────────────┬──────────────────────────┐
 │ API success              │ API/ProblemDetails error  │
 ▼                          ▼
Invalidate session query    Show field/root errors
        ↓
Navigate to /
```

Current implementation evidence:

```text
energymanagement.client/src/app/router/router.tsx
energymanagement.client/src/pages/login/LoginPage.tsx
energymanagement.client/src/features/auth/login/ui/LoginForm.tsx
energymanagement.client/src/features/auth/login/model/useLoginForm.ts
energymanagement.client/src/features/auth/login/model/loginSchema.ts
energymanagement.client/src/features/auth/login/api/loginClientAccount.ts
energymanagement.client/src/entities/session/model/sessionKeys.ts
energymanagement.client/src/shared/api/l1AuthApi.ts
energymanagement.client/src/shared/api/applyApiErrorToForm.ts
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Out of scope / not implemented here:

```text
- full protected-route policy;
- logout UI;
- remember-me/refresh-token behavior;
- lockout/rate limiting UX;
- browser E2E evidence for this exact L1 login flow.
```

## 2. Sources / Source Behavior Items

Planning/source docs:

```text
planning/slices/SL-AUTH-001-login-client-account.md
planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
```

Source behavior items used by this client sidecar:

```text
Source BI TBD — Guest sees login form.
Source BI TBD — Guest enters email and password.
Source BI TBD — Client submits L1 login request.
Source BI TBD — Client shows validation/API errors.
Source BI TBD — Successful login refreshes client session state.
Source BI TBD — Successful login moves user to the home route.
```

## 3. Visual UI / Scenario Flow

```text
┌──────────────────────────────┐
│ Guest                        │
│ opens /login                 │
└──────────────┬───────────────┘
               ▼
┌──────────────────────────────┐
│ Login UI                     │
│ email + password             │
└──────────────┬───────────────┘
               │ submit
               ▼
┌──────────────────────────────┐
│ Client validation accepted?  │
└──────────────┬───────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌─────────────────────────────┐
│ POST login           │   │ Show local field errors      │
│ email + password     │   │ no API call                  │
└──────────┬───────────┘   └─────────────────────────────┘
           ▼
┌──────────────────────────────┐
│ API success?                 │
└──────────────┬───────────────┘
               │
       ┌───────┴────────┐
       │                │
     success            error
       │                │
       ▼                ▼
┌──────────────────────┐   ┌─────────────────────────────┐
│ Invalidate session   │   │ Map ProblemDetails to       │
│ query and navigate / │   │ field/root form errors      │
└──────────────────────┘   └─────────────────────────────┘
```

## 4. UI Slice Flow

| Step | UI behavior | Implementation evidence | Status |
|---|---|---|---|
| F01 | `/login` route exists. | `app/router/router.tsx`, `shared/config/clientRoutes.ts` | implemented |
| F02 | Login page composes header, main content and footer. | `pages/login/LoginPage.tsx` | implemented |
| F03 | Login page renders `LoginForm`. | `LoginPage.tsx` | implemented |
| F04 | Form shows email and password fields. | `features/auth/login/ui/LoginForm.tsx`, `loginConst.ts` | implemented |
| F05 | Client validates email/password. | `features/auth/login/model/loginSchema.ts` | implemented |
| F06 | Submit maps form values to L1 login DTO. | `features/auth/login/api/loginClientAccount.ts` | implemented |
| F07 | Client submits through shared L1 auth API. | `shared/api/l1AuthApi.ts` | implemented |
| F08 | ProblemDetails/API errors are applied to form field/root errors. | `useLoginForm.ts`, `shared/api/applyApiErrorToForm.ts` | implemented |
| F09 | Successful login invalidates session query. | `useLoginForm.ts`, `sessionKeys.ts` | implemented |
| F10 | Successful login navigates home. | `useLoginForm.ts`, `clientRoutes.home` | implemented |

## 5. Visual Client Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ Route                                        │
│ app/router/router.tsx                        │
│ path: clientRoutes.login -> /login           │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Page                                         │
│ pages/login/LoginPage.tsx                    │
│ owns page layout and places LoginForm        │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Feature UI                                   │
│ features/auth/login/ui/LoginForm.tsx         │
│ fields, root error, submit button            │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Feature Model                                │
│ features/auth/login/model/                   │
│ useLoginForm + loginSchema                   │
│ RHF + zod + mutation + session invalidation  │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Feature API Mapper                           │
│ features/auth/login/api/                     │
│ form values -> L1LoginRequest                │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Shared API                                   │
│ shared/api/l1AuthApi.ts                      │
│ POST /api/l1/auth/login via fetchJson        │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Generated Contracts                          │
│ shared/api/generated/openapi-types.ts        │
│ L1LoginRequest + L1CurrentUserResponse       │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Session Cache Boundary                       │
│ entities/session/model/sessionKeys.ts        │
│ invalidate session query after success       │
└──────────────────────────────────────────────┘
```

## 6. Client Implementation Flow

| Layer | Responsibility | Current implementation | Status |
|---|---|---|---|
| Route | Expose login route. | `/login` route in router. | implemented |
| Page | Compose standard layout and feature form. | Header/main/Footer + `LoginForm`. | implemented |
| Feature UI | Render form fields, root error and submit state. | `LoginForm.tsx`. | implemented |
| Feature model | Form validation, mutation, API error mapping, session invalidation and navigation. | `useLoginForm.ts`. | implemented |
| Feature validation | Email/password validation. | `loginSchema.ts`. | implemented |
| Feature API mapper | Convert form values to generated login DTO. | `loginClientAccount.ts`. | implemented |
| Shared API | Send request using generated schema types. | `l1AuthApi.loginClientAccount`. | implemented |
| Session cache | Refresh current session after login. | `queryClient.invalidateQueries({ queryKey: sessionQueryKey })`. | implemented |

## 7. Client API / Generated Contract

| Client API function | Endpoint | Generated OpenAPI type(s) used | Response used? | Error constants used | Status |
|---|---|---|---|---|---|
| `features/auth/login/api/loginClientAccount(values)` | `POST /api/l1/auth/login` | `L1LoginRequest`, `L1CurrentUserResponse` | Response is returned from shared API; feature success invalidates current-user session query rather than directly storing response. | generated auth field names and error codes through schema/error messages | implemented |

## 8. Questions / Decisions

### Q-AUTH-LOGIN-CLIENT-001 — Should login navigate home or account?

Question status: future review  
Question: Should successful login navigate to `/` or `/account`?  
Assumption / current direction: Current implementation navigates to `clientRoutes.home`.  
Impact: Affects auth UX and E2E expectations.  
Shared register: `planning/slices/slice-questions-register.md / SL-AUTH-LOGIN-CLIENT-Q-001`

### D-AUTH-LOGIN-CLIENT-001 — Session refresh through invalidation

Question status: accepted direction  
Question: Should login store current-user response directly or invalidate the session query?  
Assumption / current direction: Current implementation invalidates `sessionQueryKey` after successful login.  
Impact: Keeps login and current-user bootstrap aligned through the session entity.  
Shared register: `planning/slices/slice-questions-register.md / SL-AUTH-LOGIN-CLIENT-D-001`

### Q-AUTH-LOGIN-CLIENT-002 — Lockout/rate limiting UX

Question status: future review  
Question: Should failed login show lockout/rate-limit states?  
Assumption / current direction: Not implemented in current client/backend flow; future auth/security hardening.  
Impact: Could add new error states and tests.  
Shared register: `planning/slices/slice-questions-register.md / SL-AUTH-Q-003`

## 9. Behavior Coverage

| Scenario behavior item | How sidecar covers it | Draft/file location | Status |
|---|---|---|---|
| Source BI TBD — Guest opens login | `/login` route renders login page. | UI Slice Flow | covered |
| Source BI TBD — Login form fields visible | LoginForm renders email/password fields. | UI Slice Flow / Feature UI | covered |
| Source BI TBD — Client validates login input | Zod schema validates email/password. | Client Implementation Flow | covered |
| Source BI TBD — Client submits L1 login command | API mapper sends generated DTO. | Client API / Generated Contract | covered |
| Source BI TBD — Server validation visible | ProblemDetails maps to form field/root errors. | Client Implementation Flow | covered |
| Source BI TBD — Successful login updates session | Session query is invalidated after success. | Client Implementation Flow / Decisions | covered |
| Source BI TBD — Successful login navigates | Current implementation navigates home. | UI Slice Flow / Questions | covered |

## 10. Client / Component / E2E Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Login page renders form | `/login` route/page composition. | component/router | planned/gap |
| Login form renders accessible fields | Email/password labels and errors. | component | planned/gap |
| Login validation displays errors | Client validation. | component/model | planned/gap |
| Submit maps login DTO | API body is email/password. | feature/API mapper | planned/gap |
| ProblemDetails maps to field/root errors | Server error UI. | feature/shared error handling | planned/gap |
| Success invalidates session query | Session state refresh. | feature/model | planned/gap |
| Success navigates home | Current success outcome. | component/integration | planned/gap |
| Browser login happy path | UI -> API -> session -> route. | E2E | planned after test scope decision |

## 11. Dependent / Follow-up Slices

```text
SL-AUTH-002-current-user.client.md
SL-AUTH-003-logout.client.md, when concrete logout UI starts
protected route guard/client authorization planning
```

## 12. Implementation Checklist

```text
[x] /login route exists
[x] LoginPage exists
[x] LoginForm exists
[x] email/password fields exist
[x] client validation exists
[x] ProblemDetails mapping exists
[x] shared login API function exists
[x] session query invalidation exists
[x] success navigates home
[ ] component tests found/confirmed
[ ] E2E found/confirmed
[ ] protected route policy finalized
```
