# Current Planning Workflow

Status: current workflow

## 1. Current Point

```text
Testing workflow and E2E Playwright workflow are being introduced before changing Playwright code.
```

## 2. Testing Workflow Gate

When planning or implementing a slice, classify test coverage by layer:

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

Do not push detailed client-visible UI behavior into E2E when component/client tests are the correct layer.

## 3. E2E Gate

When a slice needs E2E coverage, read:

```text
planning/testing/e2e-playwright-workflow.md
planning/testing/test-object-patterns.md
```

E2E must prove cross-layer flow:

```text
browser -> client -> HTTP API -> server/application/domain/persistence/session -> visible outcome
```

E2E should not exhaustively prove:

```text
field validation matrix
ARIA field relations
disabled-state matrix
client error parser details
```

## 4. Playwright Infrastructure Gate

Before changing Playwright config/tests, read:

```text
planning/testing/playwright-e2e-cleanup-plan.md
```

Current target:

```text
- root playwright.config.ts;
- testDir: ./tests/e2e;
- explicit webServer backend + frontend;
- headless by default;
- root npm scripts;
- role/label-first locators;
- unique test data;
- E2E Page Objects thin and mostly stateless.
```

## 5. Cross-Cutting Slice Workflow

When work is not a business scenario slice but has observable support behavior, implementation flow and tests, use a cross-cutting/helper slice.

```text
cross-cutting/helper behavior
-> coverage table
-> implementation flow
-> involved classes/methods only where useful
-> test plan
-> consumer rule for business slices
-> ADR impact
```

## 6. Constants Gate

When a slice introduces client-facing constants/error codes:

```text
1. Check CC-CONST-001.
2. Classify code:
   - ordinary validation;
   - important domain;
   - critical behavioral.
3. Update parent slice API error table.
4. Regenerate Shared/*.json.
5. Add/adjust API integration test:
   - generated JSON expectation for ordinary codes;
   - literal expectation for critical behavioral codes.
6. If client work exists, map ErrorCode -> UI behavior in `.client.md`.
7. Run generate-client-constants --check.
```

## 7. Implementation Flow Detail Rule

Implementation flow must not become a full code listing.

Include class/method/code details only when they explain:

```text
- contract boundary;
- non-obvious behavior;
- behavior that was discussed/questioned;
- important trade-off;
- extension/change point;
- error handling;
- testability;
- no-write/no-side-effect guarantee;
- generated artifact shape;
- API/client boundary.
```

Routine implementation details stay high-level.

If details dominate the flow, extract them into a sibling `.impl.md`.

Do not create `.impl.md` in advance.

## 8. Current Next Step

```text
After this docs update, implement Playwright cleanup according to planning/testing/playwright-e2e-cleanup-plan.md.
```
