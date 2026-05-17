# Testing Planning Index

Status: current testing planning index / server slice DB-state test-plan rules synchronized  
Scope: testing responsibilities, server slice test-plan separation, E2E/Playwright workflow, test object patterns, Playwright cleanup plan

## 1. Purpose

This folder centralizes testing workflow decisions that apply across slices.

Use it to avoid mixing responsibilities between:

```text
domain unit tests
server integration/API tests
client/component tests
end-to-end tests
```

## 2. Files

```text
planning/testing/testing-principles.md
planning/testing/server-slice-test-plan-rules.md
planning/testing/e2e-testing-workflow.md
planning/testing/test-object-patterns.md
planning/testing/playwright-e2e-cleanup-plan.md
```

## 3. Current Testing Direction

```text
E2E tests verify cross-layer browser -> client -> HTTP API -> server -> persistence/session -> visible outcome wiring.

E2E tests use UI as the public entry point,
but they do not exhaustively test client-visible UI behavior.

Detailed UI behavior belongs to client/component tests.
Detailed server/API behavior belongs to server integration/API tests.
```

For backend/server slice drafts that change persisted state, use:

```text
planning/testing/server-slice-test-plan-rules.md
```

Primary proof for L1 backend command behavior should be API/integration tests with direct DB state assertions, not mocks.

## 4. Current Playwright Direction

```text
- Playwright config should be repository-level.
- E2E tests should live in root-level tests/e2e.
- Playwright should explicitly start backend and frontend through webServer.
- Browser should open the frontend origin.
- Client API calls should use relative /api paths.
- Vite proxy forwards /api to backend.
```

## 5. Read Order

```text
1. planning/testing/README.md
2. planning/testing/testing-principles.md
3. planning/testing/server-slice-test-plan-rules.md
4. planning/testing/e2e-testing-workflow.md
5. planning/testing/test-object-patterns.md
6. planning/testing/playwright-e2e-cleanup-plan.md
```

## 6. Related Docs

```text
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/api/api-error-contract.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```
