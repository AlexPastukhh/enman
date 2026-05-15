# Client Planning Index

Status: current client planning navigation  
Scope: client-wide UI/client conventions, cross-cutting client behavior and current L1 client work status

## 1. Purpose

This folder contains client-wide planning docs that are broader than a single slice sidecar.

It complements:

```text
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/l1-slice-drafting-guide.md
planning/diagrams/scenario-ui-specs/
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
- L1 backend/API/persistence is implemented for register/login/current-user/logout/applicant/request commands.
- Generated OpenAPI TypeScript support exists in `energymanagement.client/src/shared/api/generated/openapi-types.ts`.
- `energymanagement.client/package.json` has `generate:api-types` using `../Shared/openapi.json`.
- Generated types/support are not the same as completed L1 feature UI.
- Concrete L1 client feature flows and `.client.md` sidecars are still future/concrete-client-work tasks.
```

Current missing client feature flows:

```text
1. Registration UI / auth client flow.
2. Login/current-user/logout client integration.
3. Applicant data form UI.
4. Request creation form UI.
5. My Requests read/list/detail UI.
6. Client-side ProblemDetails/error mapping for these flows.
7. E2E browser flows for applicant/request creation after client/read UI exists.
```

Recommended client work order:

```text
auth/session baseline
-> applicant data UI
-> request creation UI
-> My Requests read/list/detail
-> browser E2E happy paths
```

## 5. Relationship To `planning/slices/shared`

`planning/client/cross-cutting/` owns client-wide conventions.

`planning/slices/shared/` may keep slice-near reusable support notes, especially when directly referenced by slice planning.

If a rule applies to all client sidecars, prefer `planning/client/cross-cutting/`.

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

Create/update a `.client.md` only when concrete client work starts.

When concrete client work starts, read:

```text
planning/slices/l1-slice-drafting-guide.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/client/cross-cutting/README.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
```

Then use generated OpenAPI types and generated semantic constants rather than inventing client contracts.
