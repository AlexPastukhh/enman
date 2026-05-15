# API Error Mapping Boundary

Status: target implementation direction  
Scope: Domain/Application Error -> ServerError -> ProblemDetails mapping

## Target Boundary

```text
Domain/Application Error
        ↓
IApiErrorMapper
        ↓
ServerError / ServerValidationError
        ↓
IApiProblemDetailsFactory
        ↓
ProblemDetails HTTP response
```

## Why

Controllers should not decide:

```text
- whether a domain/application error is client-facing;
- which ErrorCode is returned;
- whether error is field/root;
- which DTO FieldName is used;
- which HTTP status is used.
```

Those decisions belong to the API boundary layer.

## Suggested Components

```text
IApiErrorMapper
IApiProblemDetailsFactory
ClientFacingErrorCatalog
ApiResultMapper, optional
```

## OpenAPI Relationship

OpenAPI documents:

```text
ProblemDetails response schema
status codes
error response shape
```

Generated constants document:

```text
stable semantic error code values
ProblemDetails extension names
ServerError field names
```

Do not put user-facing message text into API error contract.
