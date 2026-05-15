# OpenAPI Contract Generation

Status: current implemented workflow
Scope: OpenAPI as structural API contract, Shared/openapi.json and generated TypeScript DTO/types

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

Current server already has:

```text
AddEndpointsApiExplorer()
AddSwaggerGen()
Development Swagger middleware
Swashbuckle.AspNetCore package
```

OpenAPI work should improve metadata quality, not merely enable Swagger UI.

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

Current classification:

```text
target L1 contract:
  POST /api/l1/auth/register
  POST /api/l1/applicant-parties/individual
  POST /api/l1/requests

legacy/current auth support:
  AuthController endpoints under /api/auth

temporary compatibility:
  existing route constants in Shared/constants.json while the client migration is incomplete
```

Do not remove legacy routes from client constants until the client has moved away from them.

## 6. Shared/openapi.json Generation

Current command:

```bash
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
```

Check mode:

```bash
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
```

The tool:

```text
- starts the backend with --no-launch-profile;
- does not start frontend/Vite;
- uses the shared TestEnergyManagement LocalDB connection string;
- fetches /swagger/v1/swagger.json over development HTTPS;
- parses and re-serializes deterministic indented JSON;
- writes Shared/openapi.json only in write mode;
- compares without writing in --check mode.
```

Normal server startup must not write generated OpenAPI artifacts.

Swashbuckle CLI remains a possible future alternative:

```bash
dotnet swagger tofile --output Shared/openapi.json EnergyManagement.Server/bin/Debug/net8.0/EnergyManagement.Server.dll v1
```

## 7. Generated TypeScript Types

First stage:

```bash
npm --prefix energymanagement.client run generate:api-types
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

## 8. Check Mode / Generated Artifact Verification

Target checks:

```text
Shared/openapi.json is up to date with server metadata.
generated openapi-types.ts is up to date with Shared/openapi.json.
```

Current check strategy:

```bash
npm run check:api
```

Root scripts:

```text
npm run generate:openapi
npm run check:openapi
npm run generate:api-types
npm run generate:api
npm run check:api
```

## 9. Relationship To CC-API-001

Implementation-ready workflow lives in:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

This file owns API-level principles and generation direction.
