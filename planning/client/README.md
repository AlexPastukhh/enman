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
- client-wide feedback/message conventions;
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
planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
```

## 4. Current L1 Client State

Current repo/planning state:

```text
- L1 backend/API/persistence/session flows are implemented for register/login/current-user/logout/applicant/request commands.
- Generated OpenAPI TypeScript support exists.
- Shared L1 API wrappers exist for auth/current-user/logout and applicant create.
- Shared fetch/ProblemDetails/form-error mapping exists.
- First-stage client feature flows exist for registration, login, current-user session bootstrap and applicant create on Account page.
- Logout `.client.md` is now implementation-ready planning, but concrete UI/cache/navigation implementation is not confirmed.
- Request creation UI is future work and must consume SC-04 UI/behavior sources.
```

Implemented first-stage client sidecars:

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
```

Implementation-ready/planned client sidecars:

```text
planning/slices/SL-AUTH-003-logout.client.md
planning/slices/SL-APPL-002-read-current-individual-applicant-party.client.md
future planning/slices/SL-REQ-001-create-connection-request.client.md
```

## 5. Current Remaining Client Work

```text
1. Logout UI/cache/navigation.
2. Current applicant read after refresh.
3. Request creation form UI with applicant fields/prefill/clear behavior from SC-04.
4. My Requests read/list/detail UI.
5. Client/component tests for implemented feature flows.
6. Browser E2E happy paths after UI/read flows are stable.
7. CSRF/antiforgery handling for unsafe browser commands when that cross-cutting slice is implemented.
```

## 6. Relationship To `planning/slices/*.client.md`

Client-wide conventions live here.

Concrete feature flow and status live in sidecars:

```text
planning/slices/*.client.md
```

A `.client.md` file should state:

```text
- feature UI behavior from scenario/UI sources;
- architecture/folder-based Visual Client Implementation Flow;
- generated OpenAPI types used;
- generated constants/error codes used;
- form value -> API DTO mapping;
- ProblemDetails field/root error mapping;
- feedback/message convention, if used;
- command success convention, if used;
- local questions/assumptions;
- behavior coverage with source behavior IDs;
- client/component/E2E verification plan;
- follow-up slices.
```

## 7. Relationship To Scenario UI Specs

Scenario UI specs describe what the user must see, understand, enter, confirm, correct or be prevented from doing.

Client-wide conventions describe reusable implementation choices for realizing those UI outcomes.

Do not convert a client implementation convention into a domain/API requirement unless a scenario explicitly needs that behavior.

Use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

before drafting a `.client.md` Behavior Coverage table.

## 8. Concrete Client Sidecar Rule

Do not create `.client.md` files in advance.

Create/update a `.client.md` only when concrete client work starts or when implemented client logic must be documented/reconciled.

When concrete client work starts, read:

```text
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
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
