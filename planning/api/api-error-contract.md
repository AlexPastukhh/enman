# API Error Contract

Status: current API error contract direction / server validation boundary added  
Scope: ProblemDetails, ServerError / ServerValidationError, client-facing error codes, DTO field names, security errors, FluentValidation mapping

## 1. Default Error Envelope

Server returns native ASP.NET `ProblemDetails`.

Client-facing errors are placed into a shared `errors` extension.

## 2. ServerError / ServerValidationError

`ServerError` / `ServerValidationError` is an API-facing DTO when returned through `ProblemDetails`.

Expected fields:

```text
FieldName
ErrorCode
```

These field names must be available through generated shared constants when client parsing depends on them.

## 3. Error Code Policy

Error codes are stable human-readable symbolic identifiers, not UI messages.

Client maps error codes to messages/behavior locally.

## 4. Field Name Policy

Server validation errors use API DTO field names, not React form field names.

Client sidecar maps DTO field names to form fields when needed.

## 5. FluentValidation Boundary

FluentValidation should be used for request DTO/query validation when a server API input has:

```text
- required fields;
- discriminator/branch rules;
- mutually exclusive fields;
- nested DTO presence;
- allowed query values;
- simple API DTO shape checks.
```

FluentValidation failures should be mapped to `ProblemDetails` with status `422` and the shared `errors` extension.

For current code, legacy manual validators are mapped through `ProblemDetailsFromValidation(IEnumerable<ValidationFailure>)`. That mapper currently needs inspection before any ErrorMessage/ErrorCode migration.

Do not blindly assume `ValidationFailure.ErrorCode` is already used as the stable client-facing code.

Use:

```text
planning/api/fluentvalidation-error-code-policy-note.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 6. Application / Domain Validation Boundary

Application/domain validation remains separate from FluentValidation.

Application/domain validation owns:

```text
- ownership;
- account/entity existence;
- selected entity belongs to current account;
- business state transitions;
- domain value object invariants;
- no-write/atomicity;
- persistence consistency.
```

These failures may still return `422 ProblemDetails` when they are client-correctable validation/business errors, but they are not DTO-shape validation failures.

## 7. Client-Facing vs Internal Errors

Client-facing errors:

```text
- intentionally returned through ProblemDetails.errors;
- exist in generated errorcodes artifact when client depends on them;
- are listed in parent slice API contract;
- have client handling documented in `.client.md` when client-visible behavior exists.
```

Internal/server-only errors:

```text
- are not part of client contract;
- are logged/server-side only;
- should become generic client errors if exposed.
```

## 8. OpenAPI Error Contract

OpenAPI should expose/document response schemas and statuses:

```text
ProblemDetails
ProblemDetails + errors extension shape when client depends on it
401 Unauthorized ProblemDetails
403 Forbidden ProblemDetails
422 Validation ProblemDetails
500 ProblemDetails when relevant
```

Runtime may use native ASP.NET `ProblemDetails`, but the documented/OpenAPI-visible shape must be usable by generated TypeScript types and client parser.

## 9. Client-Facing Error Code Constants

OpenAPI documents shape, not the full semantic list of domain/client-facing error codes.

Stable client-facing error codes live in generated constants artifact:

```text
Shared/errorcodes.json
```

## 10. Antiforgery Failure

Antiforgery validation failure is a client-facing security/API error when a browser unsafe request fails token validation.

Current direction:

```text
ProblemDetails
+ errors extension
+ ErrorCode = security.antiforgery.validation.failed
```

Normalization rule:

```text
Always-run result filter checks antiforgery failure marker/result,
not generic HTTP 400.
```

Reason:

```text
ordinary DTO validation and other bad requests must not be mislabeled as CSRF/antiforgery failure.
```

Primary slice:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## 11. Constants Testing Link

Use:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

for generated artifact checks, literal contract tests and generator tests.
