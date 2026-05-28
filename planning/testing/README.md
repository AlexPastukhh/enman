# Testing Planning Index

Status: current testing planning index  
Scope: testing-layer navigation, slice test-plan support, test-layer boundaries, server/E2E/test-object workflows and transitional screenshot/evidence notes

## 1. Purpose

This folder centralizes testing workflow decisions that apply across slices.

Use it to avoid mixing responsibilities between:

```text
domain unit tests
server integration/API tests
client/component tests
end-to-end tests
screenshot/evidence tooling
```

This layer supports slice drafting, but does not replace:

```text
planning/slices/slice-test-plan-workflow.md
```

Slice drafts own concrete `Test / Verification Plan` and `Behavior-to-Test Trace` content.

## 2. Files

| File | Responsibility |
|---|---|
| `testing-responsibility-map.md` | Testing-layer routing and read selector. |
| `testing-principles.md` | Test-layer boundaries and general principles. |
| `server-slice-test-plan-rules.md` | Server/API slice test-plan buckets and server-specific test planning rules. |
| `e2e-testing-workflow.md` | Current Playwright/E2E workflow. |
| `test-object-patterns.md` | Page Object and Component Object patterns. |
| `e2e-playwright-workflow.md` | Legacy alias pointing to the current E2E workflow. |
| `playwright-e2e-cleanup-plan.md` | Historical/planned cleanup note; verify currentness before use. |
| `playwright-e2e-and-screenshot-plan.md` | Transitional mixed Playwright E2E + screenshot/evidence plan; screenshot placement review is separate. |

## 3. Read Order For Slice Test / Verification Plan

When drafting the testing section of a slice:

```text
1. planning/slices/slice-test-plan-workflow.md
2. planning/testing/testing-responsibility-map.md
3. planning/testing/testing-principles.md
4. Specific testing file selected by testing-responsibility-map.md
```

Use the testing layer only as supporting guidance after the slice draft workflow has defined the Behavior-to-Test Trace shape.

## 4. Current Testing Direction

```text
E2E tests verify cross-layer browser -> client -> HTTP API -> server -> persistence/session -> visible outcome wiring.

E2E tests use UI as the public entry point,
but they do not exhaustively test client-visible UI behavior.

Detailed UI behavior belongs to client/component tests.
Detailed server/API behavior belongs to server integration/API tests.
```

For backend/server slice drafts, use:

```text
planning/testing/server-slice-test-plan-rules.md
```

Current rule split:

```text
Read/query slices:
  primary proof = API/read integration tests;
  no unit tests by default;
  unit tests only for reusable helper logic with non-trivial branching.

State-changing command slices:
  primary proof = API/integration tests with direct DB state assertions;
  no repository/handler mocks as primary behavior proof.
```

## 5. Current Playwright / E2E Direction

```text
- Playwright config should be repository-level.
- E2E tests should live in root-level tests/e2e.
- Playwright should explicitly start backend and frontend through webServer.
- Browser should open the frontend origin.
- Client API calls should use relative /api paths.
- Vite proxy forwards /api to backend.
```

Current E2E workflow:

```text
planning/testing/e2e-testing-workflow.md
```

## 6. Screenshot / Evidence Note

Screenshot runner and reproducible screenshot evidence are related to Playwright tooling, but they are not ordinary slice behavior proof.

Use screenshot planning only when the task explicitly includes VKR/thesis screenshots, evidence artifacts or screenshot runner work.

Screenshot/evidence placement is tracked as a future review item in:

```text
planning/planning-maintenance-register.md
```

## 7. Related Docs

```text
planning/slices/slice-test-plan-workflow.md
planning/slices/slice-draft-authoring-workflow.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
planning/slices/server-implementation-principles.md
planning/api/api-error-contract.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```
