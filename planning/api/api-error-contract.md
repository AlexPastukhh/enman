# API Error Contract

Status: current API error contract direction  
Scope: ProblemDetails, ServerError / ServerValidationError, client-facing error codes, DTO field names, security errors

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

## 5. Client-Facing vs Internal Errors

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

## 6. OpenAPI Error Contract

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

## 7. Client-Facing Error Code Constants

OpenAPI documents shape, not the full semantic list of domain/client-facing error codes.

Stable client-facing error codes live in generated constants artifact:

```text
Shared/errorcodes.json
```

## 8. Antiforgery Failure

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

## 9. Constants Testing Link

Use:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

for generated artifact checks, literal contract tests and generator tests.
