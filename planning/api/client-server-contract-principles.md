# Client / Server Contract Principles

Status: current / client API placement synchronized

## 1. Core Rule

Client code must not guess server contract.

Use:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
Shared/constants.json
Shared/errorcodes.json
```

OpenAPI is the structural contract. Generated constants are the semantic contract.

## 2. Client API Placement Rule

Generated OpenAPI types are shared infrastructure.

Business-specific client API wrappers live with their owner:

```text
entities/*/api
  read endpoint wrappers and read DTO aliases/mapping.

features/*/api
  command/mutation endpoint wrappers and command DTO/result aliases/mapping.

shared/api
  fetchJson, ProblemDetails/ApiError, CSRF helpers,
  generated OpenAPI types and generic transport helpers.
```

Existing business wrappers under `shared/api` are transitional compatibility and should be moved only when a concrete slice touches that area.

## 3. Placement Examples

ApplicantParty read:

```text
entities/applicant-party/api/listAccountApplicantParties.ts
  owns GET /api/l1/applicant-parties and generated DTO aliases.

entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
  owns query key and read hook.
```

Request creation command:

```text
features/request/create-connection-request/api/createConnectionRequest.ts
  owns POST /api/l1/requests and generated request/response aliases.
```

Employee request reads:

```text
entities/employee-request/api/listEmployeeDashboardRequests.ts
entities/employee-request/api/getEmployeeRequestDetails.ts
```

Employee review commands:

```text
features/employee-request/start-review/api/startRequestReview.ts
features/employee-request/approve-review/api/approveRequestReview.ts
features/employee-request/reject-review/api/rejectRequestReview.ts
```

## 4. ProblemDetails And Validation

Server validation errors use API DTO field names, not React form field names.

Client sidecars map DTO field names to form fields when needed.

Request-level FluentValidation should be separated from application/domain validation in slice drafts.
