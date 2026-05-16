# Client Planning Index

Status: current client planning navigation  
Scope: client-wide UI/client conventions, cross-cutting client behavior and current L1 client implementation state

## 1. Purpose

This folder contains client-wide planning docs that are broader than a single slice sidecar.

It complements:

```text
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/l1-slice-drafting-guide.md
planning/diagrams/scenario-ui-specs/
planning/slices/*.client.md
```

## 2. Responsibility

This folder owns:

```text
- client-wide UI conventions;
- client-wide accessibility conventions;
- client-wide styling conventions;
- client-wide form validation conventions;
- client-wide error mapping conventions;
- client-wide command success conventions;
- client behavior conventions reused by multiple `.client.md` sidecars.
```

It does not own concrete scenario behavior, concrete slice implementation flow, backend API contracts or domain rules.

Concrete client feature flow/status belongs in the matching `.client.md` slice sidecar.

## 3. Current Files

```text
planning/client/cross-cutting/README.md
planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
```

## 4. Current L1 Client State

Current repo state:

```text
- L1 backend/API/persistence/session flows are implemented for register/login/current-user/logout/applicant/request commands.
- Generated OpenAPI TypeScript support exists in `energymanagement.client/src/shared/api/generated/openapi-types.ts`.
- `energymanagement.client/package.json` has `generate:api-types` using `../Shared/openapi.json`.
- Shared L1 API wrappers exist for auth/current-user/logout and applicant create.
- Shared fetch/ProblemDetails/form-error mapping exists.
- First-stage client feature flows exist for registration, login, current-user session bootstrap and applicant create on Account page.
```

Implemented first-stage client sidecars:

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
```

Current implemented client feature/support summary:

| Area | Client implementation state | Sidecar/status |
|---|---|---|
| App shell/routing/providers | `main.tsx`, `App`, `AppProviders`, router and routes are implemented | support baseline |
| Shared L1 API | typed wrappers for register/login/current-user/logout/applicant create use generated OpenAPI schema types | support baseline |
| Shared fetch/error mapping | `fetchJson`, `ApiError`, ProblemDetails parsing and form error mapping implemented | support baseline |
| Session bootstrap | `SessionProvider`, `useSessionQuery`, `getCurrentSession`, `useSession` implemented | `SL-AUTH-002.client` first-stage implemented |
| Register | `/register` page and form implemented; success navigates to login | `SL-ACC-001.client` first-stage implemented |
| Login | `/login` page and form implemented; success invalidates session and navigates home | `SL-AUTH-001.client` first-stage implemented |
| Applicant create | `/account` renders applicant create form for authenticated session; success shows read-only local state and notification | `SL-APPL-001.client` first-stage implemented / read-current missing |
| Logout | shared API wrapper exists | concrete logout UI/cache/navigation flow not confirmed |
| Request create | generated contract support exists | feature UI not implemented |
| My Requests | no read/list/detail UI confirmed | planned |

Current remaining client work:

```text
1. Logout UI/cache/navigation, if needed before protected flows.
2. Current applicant read after refresh.
3. Request creation form UI.
4. My Requests read/list/detail UI.
5. Client/component tests for implemented feature flows.
6. Browser E2E happy paths after UI/read flows are stable.
7. CSRF/antiforgery handling for unsafe browser commands when that cross-cutting slice is implemented.
```

## 5. Relationship To `planning/slices/*.client.md`

Client-wide conventions live here.

Concrete feature flow and status live in sidecars:

```text
planning/slices/*.client.md
```

A `.client.md` file should state:

```text
- feature UI behavior;
- architecture/folder-based Visual Client Implementation Flow;
- generated OpenAPI types used;
- generated constants/error codes used;
- form value -> API DTO mapping;
- ProblemDetails field/root error mapping;
- command success convention, if used;
- local questions/assumptions;
- behavior coverage;
- client/component/E2E verification plan;
- follow-up slices.
```

## 6. Relationship To Scenario UI Specs

Scenario UI specs describe what the user must see, understand, enter, confirm, correct or be prevented from doing.

Client-wide conventions describe reusable implementation choices for realizing those UI outcomes.

Do not convert a client implementation convention into a domain/API requirement unless a scenario explicitly needs that behavior.

Example:

```text
A scenario may require visible success outcome after command submit.

CL-COMMAND-001 says the client can treat HTTP success as confirmation when no returned entity data is needed.

That does not mean the domain must return requestId/status by default.
```

## 7. Concrete Client Sidecar Rule

Do not create `.client.md` files in advance.

Create/update a `.client.md` only when concrete client work starts or when implemented client logic must be documented/reconciled.

When concrete client work starts, read:

```text
planning/slices/l1-slice-drafting-guide.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/client/cross-cutting/README.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
planning/slices/slice-questions-register.md
planning/slices/slice-implementation-notes-register.md
```

Then use generated OpenAPI types and generated semantic constants rather than inventing client contracts.
