# API Contract Planning Index

Status: current API contract planning index / generated workflow and client API placement synchronized  
Scope: client/server API contract, OpenAPI structural contract, generated semantic constants, API errors, server request validation policy, generated artifact workflow

## 1. Core Split

```text
OpenAPI
= structural API contract.

Generated constants JSON
= semantic API/client constants.

FluentValidation
= server request DTO/query validation.

Application/domain validation
= business invariants.

Shared client API infrastructure
= transport/generated infrastructure:
  fetchJson, ProblemDetails parsing, ApiError, antiforgery helpers,
  generated OpenAPI types, generic request/query helpers.

Entity client API
= read-side business endpoint wrappers:
  entity read endpoint path, generated DTO aliases, read DTO mapping,
  fetchJson GET/read call, entity operation names.

Feature client API
= command/user-action endpoint wrappers:
  command endpoint path, request/response aliases, unsafe fetchJson call,
  command request building.
```

## 2. Generated Artifact Workflow

After backend/API contract changes, run from repo root:

```powershell
cd C:\enman\enman

dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

Expected changed generated files when API shape changed:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

If server/API implementation changes API shape, include generated artifacts in the same handoff.

Docs-only archives must not include generated artifacts.

## 3. Client API Placement Rule

Generated types are shared infrastructure:

```text
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Business-specific aliases and wrappers live with their owner:

```text
entities/<entity>/api
  read endpoint wrappers and read DTO aliases.

features/<business-action>/api
  command/mutation endpoint wrappers and command DTO/result aliases.
```

Transport remains shared:

```text
shared/api/fetchJson
shared/api/ProblemDetails / ApiError
shared/api/antiforgery helpers
```

Existing `shared/api/<businessArea>Api.ts` files are transitional compatibility, not the target for new drafts.
