# Planning Agent Protocol

Status: current collaboration protocol

## 1. Core Rule

Do not continue implementation planning through a question that may change required behavior, API contract, client-facing constants, testing responsibility, E2E scope, or cross-layer responsibility.

## 2. Testing Responsibility Rule

When planning slice/client/API work, classify tests as:

```text
Domain unit tests
Server integration/API tests
Client/component tests
End-to-end tests
```

Use:

```text
planning/testing/testing-principles.md
```

If a behavior is detailed client-visible UI behavior, prefer client/component tests.

If a behavior proves browser-client-server-persistence/session wiring, use E2E.

## 3. E2E Planning Rule

Before proposing or changing Playwright/E2E code, read:

```text
planning/testing/e2e-playwright-workflow.md
planning/testing/test-object-patterns.md
planning/testing/playwright-e2e-cleanup-plan.md
```

The agent must identify:

```text
- what cross-layer behavior E2E proves;
- what UI details should remain in component/client tests;
- what server/API behavior should remain in integration tests;
- how backend/frontend are started;
- what test data isolation strategy is used;
- what locators are used;
- whether Page Object is too stateful or too UI-detail-heavy.
```

## 4. Playwright WebServer Rule

E2E Playwright should explicitly start backend and frontend through `webServer`.

Do not rely on manual startup as the primary workflow.

Browser should open the frontend origin, and client API calls should use relative `/api` paths through Vite proxy unless CORS is intentionally configured and documented.

## 5. Locator Rule

Use role/label-first locators.

```text
getByRole(role, { name }) for buttons, links, headings, alerts, dialogs, navigation.
getByLabel(label) for form controls, especially password inputs.
getByText(text) for non-interactive visible results when appropriate.
getByTestId only when no meaningful user-facing semantic target exists.
```

Playwright exact matching rule:

```text
- Playwright string name matching is substring/case-insensitive by default.
- Use { exact: true } for exact case-sensitive matching.
- Use escaped anchored regex for exact case-insensitive matching.
- Do not build raw regex from UI text without escaping.
```

## 6. Test Object Rule

E2E Page Objects:

```text
- hide locator mechanics and repeated user actions;
- remain thin;
- receive scenario input data in action methods by default;
- do not own detailed field validation / ARIA / UI-state assertions.
```

Component Objects:

```text
- may expose detailed UI state helpers;
- may store render/user/setup context;
- can own field error/deferred validation/ARIA state helpers.
```

## 7. Cross-Cutting / Helper Slice Rule

When a user identifies work as cross-cutting or helper-like, do not bury it only in workflow docs.

Create or update a cross-cutting/helper slice if the work has:

```text
- observable/support behavior;
- concrete implementation flow;
- independent tests;
- multiple consumers;
- contract/helper/tooling responsibility.
```

Use:

```text
planning/slices/cross-cutting/
```

## 8. Constants Capture Rule

When planning or implementing client-facing constants, identify:

```text
- source C# constant;
- generated JSON artifact path;
- whether client imports it;
- ordinary validation / important domain / critical behavioral classification;
- whether integration tests should read generated JSON;
- whether a literal integration contract test is needed;
- whether Shared/*.json must be regenerated;
- whether --check should be run;
- whether client parser/message/behavior tests must change.
```

Use:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 9. Implementation Flow Detail Filter

Slice flow may include involved classes, methods and short code snippets.

Do so only when they clarify behavior, boundary, trade-off, testability, no-write/no-side-effect guarantees, generated artifact shape or API/client contract.

Routine code mechanics should be described high-level.

If class/method details make the flow noisy, suggest a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 10. Do Not

```text
- Do not use hosted service generation as primary constants generation path.
- Do not make --check modify files.
- Do not literal-test every validation code by default.
- Do not add regex convention tests or golden-file tests unless explicitly decided.
- Do not migrate FluentValidation ErrorMessage/ErrorCode usage without inspecting helpers/tests.
- Do not turn implementation flow into full code listing.
- Do not make E2E duplicate all component/client UI behavior.
- Do not rely on manual backend/frontend startup as the primary E2E workflow.
- Do not build raw regex locators from UI text without escaping.
```
