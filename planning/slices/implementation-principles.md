# Slice Implementation Principles

Status: current common slice implementation principles  
Scope: general implementation rules near slice planning; not a template and not the full server implementation architecture owner

## 1. Responsibility

This file owns common implementation principles that apply across slice planning.

It does not own:

```text
slice draft section-authoring rules;
server/backend-specific implementation architecture;
client-specific UI/CSS/form/a11y drafting rules;
copyable slice draft structure;
step-by-step drafting workflow.
```

Use these files for those responsibilities:

```text
Slice draft authoring principles:
  planning/slices/slice-draft-authoring-principles.md

Server/backend implementation principles:
  planning/slices/server/server-implementation-principles.md

Server drafting workflow:
  planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md

Client drafting workflow:
  planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md

Slice test/verification workflow:
  planning/slices/slice-test-plan-workflow.md
```

## 2. Same Format Principle

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

## 3. Draft-Driven Discovery Principle

Use:

```text
planning/slices/draft-driven-discovery-principles.md
```

Draft-driven discovery applies to slice-layer implementation work:

```text
business slices
server/backend/API slices
client sidecars
cross-cutting/helper slices
testing/support planning inside slice work
implemented-slice sync when code/tests already exist
```

Domain drafting, scenario drafting and documentation/status reconciliation have separate layer owners and should not be governed by this file.

A draft is allowed to reveal questions and gaps.

Do not push implementation through unresolved behavior/API/client/testing questions.

## 4. Client / Server Contract Principle

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

## 5. OpenAPI And Constants Split

```text
OpenAPI = structural contract:
  endpoints, methods, DTOs, response schemas, status codes.

Generated shared constants JSON = semantic constants:
  client-facing error codes, ProblemDetails extension names,
  ServerError field names, temporary route constants if needed.
```

Do not use one artifact to replace the other.

## 6. Optional Result / Maybe Principle

Use:

```text
planning/slices/shared/maybe-for-optional-results.md
```

Core rule:

```text
If the absence of a resulting object is a normal possible outcome, model that outcome with `Maybe<T>`.
```

This especially applies to repository/query methods such as:

```text
GetByIdAsync
GetByEmailAsync
GetCurrentActive...Async
Find...Async
```

Repository methods should not decide whether absence means:

```text
not found
invalid credentials
validation error
unauthorized
forbidden
```

They should return `Maybe<T>` when the row/object may normally be missing.

Application handlers unwrap `Maybe<T>` and map absence to the correct use-case result:

```text
Maybe.None -> InvalidCredentials
Maybe.None -> NotFound
Maybe.None -> validation/domain error
Maybe.None -> no optional related data
```

Use `bool` only for true existence checks where no object is needed.

Use `Result<T, Error>` or `UnitResult<Error>` when the operation can fail with a meaningful domain/application error.

Do not use nullable repository return types for new repository APIs where absence is a normal result.

Current implementation note:

```text
Some current L1 repositories still return nullable entities such as `Task<Account?>`.
The Maybe convention is the target for new/refactored repository APIs.
Do not claim the nullable-to-Maybe refactor is already implemented unless repo evidence shows it.
```

## 7. Testing Responsibility

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

Slice-specific Behavior-to-Test Trace rules live in:

```text
planning/slices/slice-test-plan-workflow.md
```

## 8. Cross-Cutting And Helper Slices

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

## 9. Implementation Flow Detail Filter

Implementation flow is behavior-first.

Detailed class/method/code explanations are included only when they clarify behavior, boundary, trade-off, error handling, testability/checkability, no-write/no-side-effect guarantees, generated artifact shape or API/client contract.

Routine code mechanics should be described high-level.

If flow becomes too noisy, extract detailed class/method reference into a sibling `.impl.md`.

Do not create `.impl.md` files in advance.

For semantic first-pass names and implementation drift rules, use:

```text
planning/slices/slice-draft-authoring-principles.md
```

For server-specific controller/application/domain/validator/persistence boundaries, use:

```text
planning/slices/server/server-implementation-principles.md
```

## 10. Constants Generation / Testing

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

## 11. OpenAPI Contract Artifacts

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

## 12. Antiforgery / CSRF

Primary source:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```
