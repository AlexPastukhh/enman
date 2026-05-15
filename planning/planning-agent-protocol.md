# Planning Agent Protocol

Status: current collaboration protocol

## Core Rule

Do not continue implementation planning through a question that may change required behavior.

If ambiguity affects scenario behavior, DATA, UI-visible requirements, validation/security, API contract, user-visible outcome or cross-layer responsibility, stop and ask.

## Scenario / UI / API Question Loop

```text
question about scenario / DATA / UI / validation / API / visible outcome
-> classify as scenario-level, UI-level, API-contract-level, or implementation-only
-> clarify / choose current direction
-> update scenario/API/source docs if changed
-> update behavior items / UI behavior items if required behavior changed
-> update architecture-decision-notes / adr-candidates if architecture decision changed
-> continue implementation planning
```

## API Contract Capture Rule

When planning or implementing API behavior, identify:

```text
- endpoint/method;
- request DTO;
- response DTO;
- OpenAPI structural contract;
- native ProblemDetails shape;
- ServerError / ServerValidationError shape;
- client-facing error codes;
- DTO field names used by validation errors;
- internal/server-only errors excluded from client contract;
- generated constants needed by client;
- generated OpenAPI types needed by client.
```

Use:

```text
planning/api/api-error-contract.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
```

## Error Code Rule

Client-facing error codes are API contract.

The client must not hardcode error code strings.

Server validation errors use API DTO field names.

Client maps DTO field names to form field names when needed.

Do not migrate FluentValidation `ErrorMessage`/`ErrorCode` usage until current validators/helpers and integration tests are inspected.

## A11Y / Test Contract Rule

Accessibility is a component/test contract.

Prefer native semantic HTML.

Use ARIA only when native semantics are insufficient.

Client tests should prefer role/name/label queries where possible.

## Do Not

```text
- Do not create `.client.md` before concrete client work starts.
- Do not continue planning through scenario/API-level ambiguity.
- Do not hardcode client-facing error code strings in client planning.
- Do not use a hosted service as primary way to generate repository constants.
- Do not migrate FluentValidation ErrorCode usage without inspecting current helpers/tests.
- Do not use ARIA as decoration.
- Do not create full numbered ADRs unless explicitly requested.
```
