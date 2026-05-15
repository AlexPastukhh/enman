# CL-ERROR-HANDLING-001 — Client / Server Error Handling

Status: current client-wide convention  
Type: client cross-cutting behavior

## Purpose

Define client-side conventions for handling API errors.

API boundary contract lives in:

```text
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
```

## Client Responsibility

```text
- parse native ProblemDetails through shared parser;
- locate shared errors extension using generated constants;
- read FieldName and ErrorCode using generated constants;
- map API DTO field names to client form fields when needed;
- show field errors near fields;
- show root/domain/stale/access errors in form/page/action areas;
- map error codes to UI messages locally;
- preserve user input where appropriate;
- refetch/invalidate read context for stale-state errors when required.
```

## Source Of Truth

| Contract item | Source |
|---|---|
| DTO shapes | OpenAPI |
| ProblemDetails/ServerError shape | OpenAPI + `planning/api/api-error-contract.md` |
| errors extension name | generated shared constants JSON |
| FieldName/ErrorCode keys | generated shared constants JSON |
| error code values | generated shared errorcodes JSON |
| user-facing message text | client message map/presentation |

## Tests

Sidecars should include tests for concrete error handling used by the slice:

```text
- field error maps to the correct field;
- root/domain error is shown in action/form area;
- stale-state error triggers expected refetch/invalidation;
- unknown/internal error shows generic message;
- user input is preserved where expected.
```
