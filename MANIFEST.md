# MANIFEST — Employee Details Client API Placement Full Sync

Status: docs-only archive  
Purpose: replace/synchronize Employee Request Details client sidecar after the client API ownership decision.

## Source input

```text
Uploaded file:
Вставленная ​​уценка(21).md
```

Key extracted decision:

```text
No business wrapper in shared/api.

Read wrapper:
  entities/employee-request/api/getEmployeeRequestDetails.ts

Generated DTO aliases:
  entities/employee-request/api/employeeRequestApiTypes.ts

Future command wrappers:
  features/employee-request/<action>/api

shared/api:
  fetchJson, ProblemDetails/ApiError, CSRF helpers,
  generated OpenAPI types and generic transport helpers only.
```

## Replaced / synchronized files

```text
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/README.md
planning/slices/README.md
planning/slices/slice-implementation-notes-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
```

## Not included

```text
runtime code
tests
generated artifacts
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

This is documentation-only. Generated artifacts belong only in implementation archives that change API shape.
