# FluentValidation Error Code Policy Note

Status: deferred cross-cutting inspection note  
Scope: FluentValidation failures, ServerValidationError, stable error codes

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
