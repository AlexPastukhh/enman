# Slice Planning Index

Status: current slice-planning navigation index  
Scope: business slices, client sidecars, cross-cutting/helper slices, source-flow/behavior register, examples, shared registers, test planning, API contract artifacts, client architecture, extension/change points and implementation notes

## 1. Purpose

This folder documents how to derive implementation slices from scenarios/concerns and how to plan implementation one slice at a time.

It is also the entry point for slice-wide shared registers.

## 2. Draft-Driven Discovery

All slice families use draft-driven discovery:

```text
planning/slices/draft-driven-discovery-principles.md
```

Practical workflow:

```text
planning/slices/l1-slice-drafting-guide.md
```

## 3. Scenario Flow / Behavior Source Rule

Before writing Scenario Flow, Visual Scenario Flow, Behavior Coverage or Covered Behavior Items, read:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

That register points to actual source artifacts:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-behavior-items/
```

Do not use `slice-questions-register.md`, `slice-extension-points-register.md` or `slice-implementation-notes-register.md` as the source for scenario flow or behavior items.

They are for questions, extension pressure and implementation notes.

## 4. Current Backend Slice Files

Current implemented L1 backend/API/persistence/session slice docs:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-AUTH-001-login-client-account.md
planning/slices/SL-AUTH-002-current-user.md
planning/slices/SL-AUTH-003-logout.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
```

Implementation-ready/planned read slice docs:

```text
planning/slices/SL-APPL-002-read-current-individual-applicant-party.md
```

## 5. Current Client Sidecar Files

Implemented/first-stage or implementation-ready client sidecars currently documented:

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-AUTH-003-logout.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-read-current-individual-applicant-party.client.md
```

Status notes:

```text
SL-AUTH-003.client = implementation-ready client draft; UI not confirmed implemented.
SL-APPL-002.client = planned / blocked by backend read endpoint.
```

Do not create new `.client.md` files before concrete client work starts or implemented client logic must be reconciled.

## 6. Slice Support Files

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/shared/README.md
planning/slices/cross-cutting/README.md
planning/slices/examples/README.md
```

## 7. Slice Registers

| Register | Responsibility |
|---|---|
| `slice-scenario-flow-behavior-register.md` | maps slices/client sidecars to scenario flow, DATA, UI scenario and behavior item source files |
| `slice-questions-register.md` | shared overview of currently relevant local questions, including questions from implemented slices |
| `slice-extension-points-register.md` | extension points, change pressure, anti-coupling decisions and extension/change questions |
| `slice-implementation-notes-register.md` | future implementation/client/testing notes not yet assigned or already promoted |

Implemented slices can still have open/future/assumption questions.

Those questions must remain in local `Questions / Decisions` sections and be mirrored to the shared registers when relevant.

## 8. Examples

Use:

```text
planning/slices/examples/
```

Current examples:

```text
planning/slices/examples/L1-APPLICANT-PARTY-READ-CURRENT-early-short-draft-example.md
planning/slices/examples/L1-APPLICANT-PARTY-READ-CURRENT-client-early-short-draft-example.md
planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
```

Example files are examples only and are not current implementation evidence unless copied into an active slice and reconciled with current repo state.

## 9. Cross-Cutting / Helper Slices

Use:

```text
planning/slices/cross-cutting/
```

Current cross-cutting slices:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Client-wide cross-cutting conventions live in:

```text
planning/client/cross-cutting/
```

## 10. API Contract Support

API contract docs live in:

```text
planning/api/
```

Use them before client slice implementation.

Generated OpenAPI TypeScript types are support artifacts, not completed feature UI.

## 11. Testing Support

Testing workflow lives in:

```text
planning/testing/
```

Browser E2E should wait until concrete client/read UI exists and the cross-layer behavior is stable.

## 12. Parent Business Slice Files

Parent business slice files own:

```text
- vertical behavior;
- Visual Scenario Flow sourced from scenario artifacts;
- Scenario Slice Flow sourced from scenario artifacts;
- behavior item coverage summary using source behavior item IDs;
- Visual Implementation Flow;
- API contract;
- local Questions / Decisions;
- links/back-references to shared registers when questions are mirrored;
- extension/change/pressure decisions;
- server/integration tests;
- link to `.client.md` sidecar when client work starts.
```

## 13. Client Sidecar Files

A `.client.md` file is created only when concrete client work starts or implemented client logic must be documented and reconciled.

It owns detailed client implementation planning and client/component tests.

It must consume `[UI-SCENARIO]` sources and scenario behavior items through `slice-scenario-flow-behavior-register.md`.

## 14. Recommended Remaining Client Order

```text
Logout UI/cache/navigation
-> Current applicant read after refresh
-> Request Creation UI
-> My Requests read/list/detail
-> Browser E2E happy paths
```
