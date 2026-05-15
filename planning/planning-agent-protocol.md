# Planning Agent Protocol

Status: current collaboration protocol

## 1. Core Rule

Do not continue implementation planning through a question that may change required behavior, API contract, security requirement, client-facing constants, testing responsibility, E2E scope, or cross-layer responsibility.

## 2. Cross-Cutting / Helper Slice Rule

When work is cross-cutting or helper-like, do not bury it only in workflow docs or shared notes.

Create or update a cross-cutting/helper slice if the work has:

```text
- observable/support behavior;
- concrete implementation flow;
- independent tests;
- multiple consumers;
- contract/helper/tooling/security responsibility.
```

Use:

```text
planning/slices/cross-cutting/
```

## 3. Same Format Rule

Cross-cutting/helper slices must follow the same planning shape as business slices:

```text
source requirements
-> behavior items
-> concern slice flow
-> implementation flow
-> tests
-> coverage/questions/ADR impact
```

They must not skip behavior items or flow just because the concern is technical.

## 4. Concern-Derived Behavior Items Rule

When behavior items are not scenario-derived, label their source type clearly:

```text
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

Every behavior item must be reflected in the Concern Slice Flow before Implementation Flow.

## 5. CSRF / Antiforgery Rule

When planning browser unsafe API request security:

```text
1. Use scenario-browser-security-addendum.md as requirements source.
2. Use CC-CSRF-001-antiforgery-behavior-items.md as behavior item source.
3. Use CC-CSRF-001-antiforgery-token-session-context.md as implementation-ready slice.
4. Normalize antiforgery failures through project API error contract.
5. If using always-run result filter, check antiforgery failure marker/result, not generic HTTP 400.
6. Do not let client blindly replay unsafe commands after token refresh.
```

## 6. Testing Responsibility Rule

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

## 7. Constants Capture Rule

When planning or implementing client-facing constants, use:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 8. Implementation Flow Detail Filter

Slice flow may include involved classes, methods and short code snippets.

Do so only when they clarify behavior, boundary, trade-off, testability, no-write/no-side-effect guarantees, generated artifact shape or API/client contract.

Routine code mechanics should be described high-level.

If class/method details make the flow noisy, suggest a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 9. Do Not

```text
- Do not implement cross-cutting security from loose notes only.
- Do not skip behavior items for technical concerns when we need traceability.
- Do not label antiforgery failure by generic HTTP 400.
- Do not blindly replay unsafe requests after token refresh.
- Do not use hosted service generation as primary constants generation path.
- Do not make --check modify files.
- Do not literal-test every validation code by default.
- Do not make E2E duplicate all component/client UI behavior.
```
