# SL-ACC-001.client — Register Client Account

Status: implemented first-stage client feature flow / tests pending  
Parent slice: `planning/slices/SL-ACC-001-register-client-account.md`  
Source scenario: guest/client registration  
Slice type: client sidecar / auth registration UI  
Current implementation status: implemented route/page/form/model/API mapping; component/E2E coverage not found in current inspection

## 1. Sidecar Overview

Implemented client behavior:

```text
Guest opens /register
        ↓
Client shows registration form
        ↓
Guest enters email, password and password confirmation
        ↓
Client validates visible form fields
        ↓
Client submits email/password to the L1 backend
        ↓
 ┌──────────────────────────┬──────────────────────────┐
 │ API success              │ API/ProblemDetails error  │
 ▼                          ▼
Navigate to /login          Show field/root errors
```

Current implementation evidence:

```text
energymanagement.client/src/app/router/router.tsx
energymanagement.client/src/pages/register/RegisterPage.tsx
energymanagement.client/src/features/auth/register/ui/RegisterForm.tsx
energymanagement.client/src/features/auth/register/model/useRegisterForm.ts
energymanagement.client/src/features/auth/register/model/registerSchema.ts
energymanagement.client/src/features/auth/register/api/registerClientAccount.ts
energymanagement.client/src/shared/api/l1AuthApi.ts
energymanagement.client/src/shared/api/applyApiErrorToForm.ts
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Out of scope / not implemented here:

```text
- auto-login after registration;
- email activation / PendingActivation;
- registration success page;
- applicant data collection during registration;
- browser E2E evidence for this exact L1 flow.
```

## 2. Sources / Source Behavior Items

Planning/source docs:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
```

Source behavior items used by this client sidecar:

```text
Source BI TBD — Guest sees registration form.
Source BI TBD — Guest enters email, password and password confirmation.
Source BI TBD — Client validates password confirmation before submit.
Source BI TBD — Client submits backend DTO with email/password only.
Source BI TBD — Client shows server validation errors as field/root errors.
Source BI TBD — Successful registration moves user to the login screen.
```

Note: source behavior IDs should be attached when the registration scenario behavior register is next reconciled.

## 3. Visual UI / Scenario Flow

```text
┌──────────────────────────────┐
│ Guest                        │
│ opens /register              │
└──────────────┬───────────────┘
               ▼
┌──────────────────────────────┐
│ Registration UI              │
│ email                         │
│ password                      │
│ password confirmation         │
│ login link                    │
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
│ POST registration    │   │ Show local field errors      │
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
│ Navigate to /login   │   │ Map ProblemDetails to       │
│                      │   │ field/root form errors      │
└──────────────────────┘   └─────────────────────────────┘
```

## 4. UI Slice Flow

| Step | UI behavior | Implementation evidence | Status |
|---|---|---|---|
| F01 | `/register` route exists. | `app/router/router.tsx`, `shared/config/clientRoutes.ts` | implemented |
| F02 | Register page composes header, main content and footer. | `pages/register/RegisterPage.tsx` | implemented |
| F03 | Register page renders `RegisterForm`. | `RegisterPage.tsx` | implemented |
| F04 | Form shows email, password and password confirmation. | `features/auth/register/ui/RegisterForm.tsx`, `registerConst.ts` | implemented |
| F05 | Client validates email/password/password confirmation. | `features/auth/register/model/registerSchema.ts` | implemented |
| F06 | Password confirmation is checked client-side. | `registerSchema.ts` refine rule | implemented |
| F07 | Submit maps form values to backend DTO with email/password only. | `features/auth/register/api/registerClientAccount.ts` | implemented |
| F08 | Client submits through shared L1 auth API. | `shared/api/l1AuthApi.ts` | implemented |
| F09 | ProblemDetails/API errors are applied to form field/root errors. | `useRegisterForm.ts`, `shared/api/applyApiErrorToForm.ts` | implemented |
| F10 | Successful registration navigates to login. | `useRegisterForm.ts` | implemented |

## 5. Visual Client Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ Route                                        │
│ app/router/router.tsx                        │
│ path: clientRoutes.register -> /register     │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Page                                         │
│ pages/register/RegisterPage.tsx              │
│ owns page layout and places RegisterForm     │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Feature UI                                   │
│ features/auth/register/ui/RegisterForm.tsx   │
│ fields, root error, submit button, login link│
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Feature Model                                │
│ features/auth/register/model/                │
│ useRegisterForm + registerSchema             │
│ RHF + zod + mutation + navigation            │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Feature API Mapper                           │
│ features/auth/register/api/                  │
│ form values -> L1RegisterClientAccountDto    │
│ drops passwordConfirmation                   │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Shared API                                   │
│ shared/api/l1AuthApi.ts                      │
│ POST /api/l1/auth/register via fetchJson     │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Generated Contracts                          │
│ shared/api/generated/openapi-types.ts        │
│ L1RegisterClientAccountDto/Response          │
└──────────────────────────────────────────────┘
```

## 6. Client Implementation Flow

| Layer | Responsibility | Current implementation | Status |
|---|---|---|---|
| Route | Expose registration route. | `/register` route in router. | implemented |
| Page | Compose standard layout and feature form. | Header/main/Footer + `RegisterForm`. | implemented |
| Feature UI | Render form fields, root error, submit state and login link. | `RegisterForm.tsx`. | implemented |
| Feature model | Form validation, mutation, API error mapping and navigation. | `useRegisterForm.ts`. | implemented |
| Feature validation | Email/password/confirmation validation. | `registerSchema.ts`. | implemented |
| Feature API mapper | Convert form values to backend DTO. | `registerClientAccount.ts`. | implemented |
| Shared API | Send request using generated schema type. | `l1AuthApi.registerClientAccount`. | implemented |
| Shared errors | Convert ProblemDetails to RHF field/root errors. | `applyApiErrorToForm`. | implemented |

## 7. Client API / Generated Contract

| Client API function | Endpoint | Generated OpenAPI type(s) used | Response used? | Error constants used | Status |
|---|---|---|---|---|---|
| `features/auth/register/api/registerClientAccount(values)` | `POST /api/l1/auth/register` | `L1RegisterClientAccountDto`, `L1RegisterClientAccountResponse` | Feature ignores response body; success navigates to `/login`. | generated auth field names and error codes through schema/error messages | implemented |

Contract notes:

```text
- Client form includes `passwordConfirmation`, but backend DTO does not.
- Feature API mapper sends only `email` and `password`.
- Password confirmation is a client-side validation behavior.
```

## 8. Questions / Decisions

### Q-ACC-CLIENT-001 — Should registration auto-login?

Question status: open  
Question: Should successful registration create a session automatically?  
Assumption / current direction: Current implementation navigates to `/login`; auto-login is not implemented.  
Impact: Affects auth/session invalidation, registration success UX and E2E flow.  
Shared register: `planning/slices/slice-questions-register.md / SL-ACC-CLIENT-Q-001`

### D-ACC-CLIENT-001 — Password confirmation is client-side only

Question status: accepted direction  
Question: Does backend registration receive password confirmation?  
Assumption / current direction: Current implementation sends backend DTO `email/password` only; password confirmation is client validation only.  
Impact: Keeps client API mapping aligned with current backend contract.  
Shared register: `planning/slices/slice-questions-register.md / SL-ACC-D-001`

### D-ACC-CLIENT-002 — Registration success target is login

Question status: accepted direction  
Question: Where does current client navigate after successful registration?  
Assumption / current direction: Current implementation navigates to `/login`.  
Impact: Future auto-login or success page work must deliberately change this behavior.  
Shared register: `planning/slices/slice-questions-register.md / SL-ACC-CLIENT-D-002`

## 9. Behavior Coverage

| Scenario behavior item | How sidecar covers it | Draft/file location | Status |
|---|---|---|---|
| Source BI TBD — Guest opens registration | `/register` route renders registration page. | UI Slice Flow | covered |
| Source BI TBD — Registration form fields visible | RegisterForm renders email/password/password confirmation. | UI Slice Flow / Feature UI | covered |
| Source BI TBD — Password confirmation validated | Zod refine compares password and confirmation. | Client Implementation Flow | covered |
| Source BI TBD — Backend receives email/password only | Feature API mapper sends generated DTO without confirmation. | Client API / Generated Contract | covered |
| Source BI TBD — Server validation visible | ProblemDetails maps to form field/root errors. | Client Implementation Flow | covered |
| Source BI TBD — Registration success continues to login | Mutation success navigates `/login`. | UI Slice Flow / Questions | covered |
| Auto-login after registration | Not implemented; open UX question. | Questions / Follow-up | not covered |

## 10. Client / Component / E2E Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Register page renders form | `/register` route/page composition. | component/router | planned/gap |
| Register form renders accessible fields | Email/password/confirmation labels and errors. | component | planned/gap |
| Password mismatch validation | Client confirmation rule. | component/model | planned/gap |
| Submit maps DTO without confirmation | API mapper sends `email/password` only. | feature/API mapper | planned/gap |
| ProblemDetails maps to field/root errors | Server validation UI. | feature/shared error handling | planned/gap |
| Success navigates to login | Registration success outcome. | component/integration | planned/gap |
| Browser registration happy path | UI -> API -> success navigation. | E2E | planned after test scope decision |

## 11. Dependent / Follow-up Slices

```text
SL-AUTH-001-login-client-account.client.md
future registration auto-login decision
future PendingActivation/email confirmation flow
```

## 12. Implementation Checklist

```text
[x] /register route exists
[x] RegisterPage exists
[x] RegisterForm exists
[x] email/password/password confirmation fields exist
[x] client validation exists
[x] password confirmation is not sent to backend
[x] ProblemDetails mapping exists
[x] success navigates to login
[ ] component tests found/confirmed
[ ] E2E found/confirmed
[ ] auto-login decision resolved
```
