# Client Planning Index

Status: current client planning navigation / My Requests sidecars synchronized

## 1. Purpose

This folder contains client-wide planning docs that are broader than a single slice sidecar.

Concrete client feature flow/status belongs in the matching `.client.md` slice sidecar.

## 2. Current Files

```text
planning/client/cross-cutting/README.md
planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
```

## 3. Current L1 Client State

Current repo/planning state:

```text
- Generated OpenAPI TypeScript support exists.
- Shared fetch/ProblemDetails/form-error mapping exists.
- First-stage client feature flows exist for registration, login, current-user session bootstrap, applicant create and My Requests list.
- My Requests details and filters are planned/implementation-ready sidecars.
- Request creation UI remains future work.
```

Implemented first-stage client sidecars:

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/l1/L1-MY-REQUESTS-READ-LIST.client.md
```

Implementation-ready/planned client sidecars:

```text
planning/slices/SL-AUTH-003-logout.client.md
planning/slices/SL-APPL-002-read-current-individual-applicant-party.client.md
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
future planning/slices/SL-REQ-001-create-connection-request.client.md
```

## 4. My Requests Client Architecture Direction

```text
My Requests list page owns URL query params.
Filter feature owns filter controls and parse/serialize helpers.
Entity query accepts MyRequestsFilters.
Shared API maps supported filters to query string.
Details route owns requestId route param.
```

Status is the first filter. Future request type/date/search filters should extend the filter model without moving URL ownership out of the page.

## 5. Relationship To Scenario UI Specs

Scenario UI specs describe what the user must see, understand, enter, confirm, correct or be prevented from doing.

Client-wide conventions describe reusable implementation choices for realizing those UI outcomes.

Use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

before drafting a `.client.md` Behavior Coverage table.

## 6. Concrete Client Sidecar Rule

Do not create `.client.md` files in advance.

Create/update a `.client.md` only when concrete client work starts or when implemented client logic must be documented/reconciled.

When concrete client work starts, read:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
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
