# Planning Agent Protocol

Status: current collaboration protocol

## 1. Core Rule

Do not continue implementation planning through a question that may change required behavior, API contract, security requirement, client-facing constants, testing responsibility, E2E scope, or cross-layer responsibility.

## 2. Client / Server Contract Rule

Before planning missing client slices, check:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

The agent must not let client code guess:

```text
routes
request DTOs
response DTOs
status/error shapes
ProblemDetails extension names
error code strings
```

Use generated OpenAPI types for structure and generated constants for semantics.

## 3. OpenAPI / Constants Split Rule

```text
OpenAPI = structural contract:
  endpoints, methods, DTOs, response schemas, statuses.

Generated constants JSON = semantic contract:
  error codes, ProblemDetails extension names, ServerError field names,
  temporary legacy route constants.
```

Do not collapse these into one artifact.

## 4. Generated Artifact Rule

Generated artifacts are produced by explicit commands/checks.

Do not write generated client artifacts during normal server startup.

## 5. Endpoint Classification Rule

Before generating client API types/wrappers for a slice, classify endpoint as:

```text
target L1
legacy/current support
temporary compatibility
internal/not client-facing
```

If current endpoint status is unclear, ask or document assumption before implementation.

## 6. Cross-Cutting / Helper Slice Rule

When work is cross-cutting or helper-like, do not bury it only in workflow docs or shared notes.

Create or update a cross-cutting/helper slice if the work has:

```text
- observable/support behavior;
- concrete implementation flow;
- independent tests/checks;
- multiple consumers;
- contract/helper/tooling/security responsibility.
```

Use:

```text
planning/slices/cross-cutting/
```

## 7. Same Format Rule

Cross-cutting/helper slices must follow the same planning shape as business slices:

```text
source requirements
-> behavior items
-> concern slice flow
-> implementation flow
-> tests/checks
-> coverage/questions/ADR impact
```

## 8. CSRF / Antiforgery Rule

When planning browser unsafe API request security, use:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## 9. Testing Responsibility Rule

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

## 10. Implementation Flow Detail Filter

Slice flow may include involved classes, methods and short code snippets.

Do so only when they clarify behavior, boundary, trade-off, testability/checkability, no-write/no-side-effect guarantees, generated artifact shape or API/client contract.

Routine code mechanics should be described high-level.

If class/method details make the flow noisy, suggest a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 11. Do Not

```text
- Do not implement client slices by manually guessing API contract.
- Do not use OpenAPI as replacement for stable semantic error-code constants.
- Do not use generated constants JSON as long-term replacement for OpenAPI structural route/DTO contract.
- Do not write generated artifacts during normal server startup.
- Do not implement cross-cutting security from loose notes only.
- Do not skip behavior items for technical concerns when traceability is needed.
- Do not label antiforgery failure by generic HTTP 400.
- Do not blindly replay unsafe requests after token refresh.
```
