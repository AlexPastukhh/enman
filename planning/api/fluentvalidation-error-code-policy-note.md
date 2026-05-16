# FluentValidation Error Code Policy Note

Status: deferred cross-cutting inspection note / linked to server validation principles  
Scope: FluentValidation failures, ServerValidationError, stable error codes

## Current Repo Evidence

Current implementation shows:

```text
- FluentValidation packages are referenced by the server project;
- legacy validators are registered manually in Program.cs;
- legacy AuthController injects IValidator<T> and calls ValidateAsync manually;
- L1Controller does not currently inject IValidator<L1...Dto>;
- L1 validation mostly happens in handlers/domain value-object factories;
- ProjectController maps FluentValidation ValidationFailure using f.PropertyName and f.ErrorMessage.
```

So FluentValidation is real project infrastructure, but L1 request DTO validators are not a consistent implemented layer yet.

## Current Rule

Do not migrate FluentValidation `ErrorMessage` / `ErrorCode` usage blindly.

Before changing implementation:

```text
1. inspect existing validators;
2. inspect validation helper methods;
3. inspect integration tests for ServerValidationError;
4. determine whether helpers currently put stable code into ErrorMessage or ErrorCode;
5. migrate only with tests.
```

## Request Validation Planning Rule

For new or changed L1 server API inputs, use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

Plan FluentValidation for request DTO/query shape rules, but keep application/domain validation in handlers/domain.

## Target Direction, If Confirmed

```text
validator/helper sets stable code using WithErrorCode(...)
API error mapper reads failure.ErrorCode
ServerValidationError.ErrorCode receives stable code
client maps generated error code to UI message/behavior
```

## Relationship To Constants Testing

Constants generator/testing can be implemented before this migration.

The migration is a separate hardening step.

Do not block business-slice request validation planning merely because the ErrorCode hardening is not finished. Mark stable error-code mapping as future hardening when needed.
