# Slice Implementation Principles

Status: current common implementation principles  
Scope: general implementation rules near slice planning

## 1. Same Format Principle

Business, cross-cutting and helper slices use the same planning discipline:

```text
source requirements
-> behavior items
-> slice/concern flow
-> implementation flow
-> tests/checks
-> coverage/questions/ADR impact
```

Cross-cutting/helper slices are not allowed to skip behavior/flow just because the concern is technical.

## 2. Draft-Driven Discovery Principle

Use:

```text
planning/slices/draft-driven-discovery-principles.md
```

Draft-driven discovery applies to all slice implementation work:

```text
domain
business slices
client sidecars
cross-cutting/helper slices
testing/support slices
documentation/status reconciliation drafts
```

A draft is allowed to reveal questions and gaps.

Do not push implementation through unresolved behavior/API/client/testing questions.

## 3. Client / Server Contract Principle

Before implementing missing client slices:

```text
OpenAPI structural contract must be clear.
Generated semantic constants must be clear.
Client API wrappers should use generated OpenAPI types.
Client error parsing/mapping should use generated constants.
```

Use:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 4. OpenAPI And Constants Split

```text
OpenAPI = structural contract:
  endpoints, methods, DTOs, response schemas, status codes.

Generated shared constants JSON = semantic constants:
  client-facing error codes, ProblemDetails extension names,
  ServerError field names, temporary route constants if needed.
```

Do not use one artifact to replace the other.

## 5. Testing Responsibility

Use:

```text
planning/testing/testing-principles.md
```

for test layer boundaries.

Short version:

```text
Client/component tests
= detailed client-visible UI behavior.

Server integration/API tests
= API/server contract, validation, persistence and application orchestration.

E2E tests
= cross-layer browser-client-server-persistence/session wiring and final visible outcome.
```

Do not make E2E duplicate the full client/component test matrix.

## 6. Cross-Cutting And Helper Slices

Cross-cutting/helper slices are allowed.

They are not business scenario slices, but they must still have:

```text
- observable/support behavior;
- concern-derived behavior items;
- concern slice flow;
- implementation flow;
- test/check plan;
- consumers / used-by slices;
- coverage table;
- local questions;
- ADR impact when relevant.
```

Use:

```text
planning/slices/cross-cutting/
```

## 7. Implementation Flow Detail Filter

Implementation flow is behavior-first.

Detailed class/method/code explanations are included only when they clarify behavior, boundary, trade-off, error handling, testability/checkability, no-write/no-side-effect guarantees, generated artifact shape or API/client contract.

Routine code mechanics should be described high-level.

If flow becomes too noisy, extract detailed class/method reference into a sibling `.impl.md`.

Do not create `.impl.md` files in advance.

## 8. Constants Generation / Testing

Primary source:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Core rules:

```text
- Generate Shared/constants.json and Shared/errorcodes.json by explicit Tools command.
- `--check` compares generated output with committed files and does not write.
- Client must not hardcode error code strings.
- API integration tests should read generated artifact for ordinary codes.
- Critical behavioral codes get literal integration contract tests.
- Route constants are temporary during OpenAPI migration.
```

## 9. OpenAPI Contract Artifacts

Primary source:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

Core rules:

```text
- OpenAPI owns structural contract;
- Shared/openapi.json is generated and committed;
- client OpenAPI TypeScript types are generated from Shared/openapi.json;
- thin handwritten client API wrappers use generated types first;
- generated artifacts are checked for staleness;
- no generated artifacts are written during normal server startup.
```

## 10. Antiforgery / CSRF

Primary source:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```
