# Current Planning Workflow

Status: current workflow

## 1. Current Point

```text
Client/server contract artifacts are the next blocker before missing client slices:
OpenAPI structural contract + generated semantic constants.
```

## 2. Contract Artifact Gate

Before implementing missing client slices:

```text
1. Read planning/api/client-server-contract-principles.md.
2. Read CC-API-001.
3. Read CC-CONST-001.
4. Classify current endpoints as target L1 / legacy-current / compatibility / internal.
5. Generate/check Shared/openapi.json.
6. Generate/check client OpenAPI TypeScript types.
7. Generate/check Shared/constants.json and Shared/errorcodes.json.
8. Update client API wrappers to use generated types and constants.
```

## 3. OpenAPI Gate

For any API endpoint used by client:

```text
- endpoint/method is documented;
- request DTO is documented;
- response DTO is documented;
- success status is documented;
- ProblemDetails statuses are documented;
- endpoint contract status is explicit;
- generated client type is available when client consumes it.
```

## 4. Constants Gate

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

## 5. Cross-Cutting / Helper Slice Workflow

Cross-cutting/helper slices follow the same planning shape as business slices:

```text
source requirements
-> behavior items
-> concern slice flow
-> implementation flow
-> tests/checks
-> coverage/questions/ADR impact
```

## 6. CSRF Gate

When planning browser unsafe API requests or auth/session flow:

```text
1. Read scenario-browser-security-addendum.md.
2. Read CC-CSRF-001-antiforgery-behavior-items.md.
3. Read CC-CSRF-001-antiforgery-token-session-context.md.
4. Link parent business slice API/security section to CC-CSRF-001.
5. Do not implement antiforgery mechanics separately inside business slices.
```

## 7. Testing Workflow Gate

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

## 8. Implementation Flow Detail Rule

Implementation flow must not become a full code listing.

Include class/method/code details only when they explain:

```text
- contract boundary;
- non-obvious behavior;
- behavior that was discussed/questioned;
- important trade-off;
- extension/change point;
- error handling;
- testability/checkability;
- no-write/no-side-effect guarantee;
- generated artifact shape;
- API/client boundary.
```

Routine implementation details stay high-level.

If details dominate the flow, extract them into a sibling `.impl.md`.

Do not create `.impl.md` in advance.
