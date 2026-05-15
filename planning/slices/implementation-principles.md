# Slice Implementation Principles

Status: current common implementation principles  
Scope: general implementation rules near slice planning

## Responsibility

API boundary rules belong in:

```text
planning/api/
```

Client-wide conventions belong in:

```text
planning/client/
```

## API Contract Ownership

Parent slice owns what server promises.

Client sidecar owns how client uses that promise.

Use:

```text
planning/api/api-error-contract.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
```

## OpenAPI And Constants Split

```text
OpenAPI = structural contract:
  endpoints, methods, DTOs, response schemas, status codes.

Generated shared constants JSON = semantic constants:
  client-facing error codes, ProblemDetails extension names,
  ServerError field names, temporary route/field constants if needed.
```

Client message text is presentation and belongs to client.

## Native ProblemDetails

Native ASP.NET `ProblemDetails` remains the default API error envelope.

Client-facing errors are placed into a shared `errors` extension.

`ServerError` / `ServerValidationError` is API-facing DTO when returned through `ProblemDetails`.

## Error Code Policy

Error codes are stable human-readable symbolic identifiers.

They are not user-facing messages.

Client must import generated/shared error code constants instead of hardcoding code strings.

## DTO Field Name Policy

Server validation errors use API DTO field names.

Client maps API DTO field names to form field names when needed.

## Client Constants Generation

Generate shared client constants using an explicit command, not a hosted service at application startup.

## FluentValidation Deferred Question

Do not migrate FluentValidation `ErrorMessage`/`ErrorCode` usage blindly.

Use:

```text
planning/api/fluentvalidation-error-code-policy-note.md
```

before changing validation error-code mapping.

## Tests

API contract tests should verify:

```text
- ProblemDetails status;
- errors extension exists;
- each client-facing error has FieldName/ErrorCode as expected;
- FieldName is API DTO field name;
- ErrorCode is stable generated/shared code;
- internal errors are not exposed as client-facing codes.
```
