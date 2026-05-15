# OpenAPI Contract Generation

Status: current target direction  
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

Current server already has the basic Swagger setup direction:

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

Example direction:

```csharp
[ProducesResponseType(typeof(L1RegisterClientAccountResponse), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
```

Protected endpoint direction:

```csharp
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
```

## 5. Public Contract Endpoint Classification

Before generation, classify endpoints:

```text
target L1 contract
legacy/current support
temporary compatibility
internal/not client-facing
```

Current planning direction:

```text
L1 endpoints are target contract for new L1 slices.
Legacy AuthController endpoints remain current/legacy auth support until migration.
```

Do not remove legacy routes from client constants until the client has moved away from them.

## 6. Shared/openapi.json Generation Options

### Option A — running server

```bash
dotnet run --project EnergyManagement.Server/EnergyManagement.Server.csproj
curl -k https://localhost:7250/swagger/v1/swagger.json -o Shared/openapi.json
```

Pros:

```text
simple; matches runtime.
```

Cons:

```text
requires running server; weaker for check mode/CI.
```

### Option B — Swashbuckle CLI

```bash
dotnet tool install Swashbuckle.AspNetCore.Cli
dotnet build EnergyManagement.Server/EnergyManagement.Server.csproj
dotnet swagger tofile --output Shared/openapi.json EnergyManagement.Server/bin/Debug/net8.0/EnergyManagement.Server.dll v1
```

Pros:

```text
better for generation/check pipeline.
```

Cons:

```text
may need startup/config work if app startup requires DB/secrets.
```

Current direction:

```text
Start with Option A if fastest, then move to CLI/tool command when stable.
```

## 7. Generated TypeScript Types

First stage:

```bash
npx openapi-typescript Shared/openapi.json -o energymanagement.client/src/shared/api/generated/openapi-types.ts
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

Possible check strategy:

```bash
npm run generate:api
git diff --exit-code Shared/openapi.json energymanagement.client/src/shared/api/generated/openapi-types.ts
```

or later:

```bash
dotnet run --project EnergyManagement.Tools -- check-contracts
```

## 9. Relationship To CC-API-001

Implementation-ready workflow lives in:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

This file owns API-level principles and generation direction.
