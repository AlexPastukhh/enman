# Current Planning Workflow

Status: current workflow

## 1. Current Point

```text
CSRF/antiforgery is being promoted from shared note to cross-cutting security slice.
```

## 2. Cross-Cutting / Helper Slice Workflow

Cross-cutting/helper slices follow the same planning shape as business slices:

```text
source requirements
-> behavior items
-> concern slice flow
-> implementation flow
-> tests
-> coverage/questions/ADR impact
```

Business slice source type is usually scenario-derived.

Cross-cutting/helper source types may be:

```text
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

## 3. CSRF Gate

When planning browser unsafe API requests or auth/session flow:

```text
1. Read scenario-browser-security-addendum.md.
2. Read CC-CSRF-001-antiforgery-behavior-items.md.
3. Read CC-CSRF-001-antiforgery-token-session-context.md.
4. Link parent business slice API/security section to CC-CSRF-001.
5. Do not implement antiforgery mechanics separately inside business slices.
```

## 4. Testing Workflow Gate

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

## 5. E2E Gate

When a slice needs E2E coverage, read:

```text
planning/testing/e2e-playwright-workflow.md
planning/testing/test-object-patterns.md
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
5. Add/adjust API integration test.
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
