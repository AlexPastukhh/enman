# OpenAPI Contract Generation

Status: current first-stage implemented workflow / generated artifact check workflow clarified  
Scope: OpenAPI as structural API contract, `Shared/openapi.json`, generated TypeScript DTO/types and local generated-artifact verification

## 1. Purpose

OpenAPI is the structural client/server contract.

It should cover:

```text
- endpoint path;
- HTTP method;
- route/query/body parameters;
- request DTO shape;
- response DTO shape;
- status codes;
- ProblemDetails / error response schemas;
- operation id where practical.
```

## 2. Split From Constants

```text
OpenAPI = shape/type/endpoint/status contract.
Shared constants JSON = semantic constants and error codes.
Client local code = UI messages and behavior mapping.
```

## 3. Current Server Prerequisites

Current server has Swagger/OpenAPI infrastructure and explicit L1 response metadata.

OpenAPI work now means:

```text
- keep metadata quality high when endpoints change;
- regenerate/check committed artifacts;
- use generated TypeScript types from client code;
- do not reimplement the tooling baseline unless hardening is explicitly in scope.
```

## 4. OpenAPI Quality Checklist

For client-facing endpoints:

```text
- public request DTOs;
- public response DTOs;
- explicit route/method;
- explicit success response type;
- explicit ProblemDetails response types;
- 401/403 metadata for protected endpoints;
- 422 metadata for validation responses;
- stable operation id if practical;
- no accidental internal DTO exposure.
```

## 5. Public Contract Endpoint Classification

Client slices must know whether an endpoint is:

```text
- target L1 contract;
- legacy/current compatibility;
- temporary compatibility;
- internal / not for client consumption.
```

Do not remove legacy route constants until the client has moved away from them.

## 6. Shared/openapi.json Generation

Write/update artifact:

```bash
npm run generate:openapi
```

Equivalent command:

```bash
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
```

Check mode:

```bash
npm run check:openapi
```

Equivalent command:

```bash
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
```

Normal server startup must not write generated OpenAPI artifacts.

## 7. Generated TypeScript Types

Generate TypeScript DTO/types from `Shared/openapi.json`:

```bash
npm run generate:api-types
```

Output:

```text
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

First-stage client strategy:

```text
Generate TypeScript DTO/types from OpenAPI.
Keep thin handwritten client API wrapper functions.
```

Reason:

```text
- avoids DTO drift;
- preserves frontend architecture boundaries;
- avoids risky full generated client migration.
```

## 8. Generated Artifact Verification

Current check strategy:

```bash
npm run check:api
```

`check:api` does three things:

```text
1. check OpenAPI artifact against server metadata;
2. regenerate TypeScript API types;
3. fail if generated artifacts differ from the git index.
```

Because the final step is `git diff --exit-code`, generated artifacts should be staged before using `check:api` as a no-extra-drift check in local archive/commit workflow.

Recommended workflow after API source changes:

```powershell
npm run generate:openapi
npm run generate:api-types

git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm run check:api
```

Detailed workflow:

```text
planning/api/generated-artifact-check-workflow.md
```

## 9. Relationship To CC-API-001

Current implementation/status reconciliation lives in:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

This file owns API-level principles and generation direction.

CC-API-001 owns cross-cutting implementation status, remaining hardening questions and consumer rules.
