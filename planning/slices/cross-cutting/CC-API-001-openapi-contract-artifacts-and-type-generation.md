# CC-API-001 — OpenAPI Contract Artifacts And Type Generation

Status: first-stage implemented / explicit API-shape generation and archive workflow clarified / hardening planned  
Slice type: cross-cutting slice  
Layers: Server API metadata + `Shared/openapi.json` + client generated TypeScript types + checks  
Depends on: ASP.NET Core API controllers, Swashbuckle/OpenAPI, client API layer  
Used by: all client slices that call server API

## 1. Purpose

Provide a stable structural API contract artifact and generated client TypeScript types so client slices do not manually guess routes, request DTOs, response DTOs or status/error response shapes.

This slice covers:

```text
server OpenAPI metadata
-> Shared/openapi.json
-> generated client TypeScript types
-> thin handwritten client API wrappers using generated types
-> generated artifact checks
```

## 2. Source Of Truth Rule

Never hand-edit generated artifacts.

Correct source chain:

```text
backend API source/metadata
        ↓
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
        ↓
Shared/openapi.json
        ↓
npm.cmd --prefix energymanagement.client run generate:api-types
        ↓
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Implementation archives that change API contract must include generated artifacts produced by these commands.

Documentation-only archives must not include generated artifacts.

## 3. Current Implementation Status

| Area | Status |
|---|---|
| Tools command `generate-openapi` | first-stage implemented |
| `--check` no-write mode | implemented |
| `Shared/openapi.json` committed artifact | implemented |
| generated `openapi-types.ts` | implemented |
| generated artifact drift checks | implemented |
| client wrappers using generated types | planned/migrated per slice |
| CI hardening | future review |

## 4. Concern-Derived Behavior Items

| ID | Behavior |
|---|---|
| `CC-API-SRV-001` | Server exposes client-facing endpoints through OpenAPI. |
| `CC-API-SRV-002` | Client-facing DTOs appear in OpenAPI schemas. |
| `CC-API-SRV-003` | Success and ProblemDetails response statuses are documented. |
| `CC-API-SRV-004` | Client-facing endpoints are classified as target, legacy/current, temporary compatibility or internal. |
| `CC-API-ART-001` | `Shared/openapi.json` is generated and committed/included in API-changing handoffs. |
| `CC-API-ART-002` | Client TypeScript types are generated from `Shared/openapi.json`. |
| `CC-API-CL-001` | Client API wrappers use generated OpenAPI types. |
| `CC-API-CHK-001` | Checks detect stale OpenAPI and generated TypeScript artifacts. |
| `CC-API-NW-001` | OpenAPI does not replace generated semantic error-code constants. |
| `CC-API-NW-002` | No generated artifact is written during normal server startup. |
| `CC-API-TEST-001` | Server integration/API tests still verify runtime contract behavior. |

## 5. Structural Contract Concern Flow

```text
F01 Server publishes structural API shape
F02 Response statuses and ProblemDetails are visible
F03 Endpoint contract status is explicit
F04 OpenAPI artifact is generated and included in handoff
F05 Client types are generated from OpenAPI
F06 Client API wrappers use generated types
F07 Stale artifacts are detected
F08 Structural and semantic contracts stay separate
F09 Runtime behavior is still tested
```

## 6. Generated Artifact Workflow

After API source changes, run from repository root:

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

Why `check:api` can fail while this is correct:

```text
check:api ends with git diff --exit-code Shared/openapi.json energymanagement.client/src/shared/api/generated/openapi-types.ts.
That means generated files must already be part of the expected state.
If they are only modified in the working tree, the check intentionally fails.
```

Detailed workflow:

```text
planning/api/generated-artifact-check-workflow.md
```

## 7. Implementation Flow

```text
I01 Maintain server OpenAPI metadata
I02 Generate Shared/openapi.json from server metadata
I03 Generate client TypeScript types from Shared/openapi.json
I04 Use generated types in thin handwritten client API wrappers
I05 Include generated artifacts in API-changing implementation handoff
I06 Run generated artifact/build/test checks
I07 Keep runtime server/API tests for actual behavior
```

## 8. Client Consumer Rule

Client slices should use:

```text
generated OpenAPI types
        +
thin shared API wrappers
        +
entity/feature API functions
```

Do not manually create duplicate DTO types when generated types exist.

Do not move all client API work into generated client code unless a separate generated-client migration is approved.

## 9. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `CC-API-Q-001` | accepted | Should generated artifacts be hand-edited? | No. Use repo generation commands only. | Prevents fake/stale contract artifacts. |
| `CC-API-Q-002` | accepted | Why can `check:api` fail after correct generation? | The final `git diff --exit-code` is a drift check; generated files must be in expected state, not just unstaged working-tree changes. | Local workflow clarity. |
| `CC-API-Q-003` | accepted | Should implementation archives include generated files when API shape changes? | Yes: include `Shared/openapi.json` and generated `openapi-types.ts`. | Prevents broken handoffs. |
| `CC-API-Q-004` | future review | Should CI enforce check:api? | Future hardening. | CI policy. |
| `CC-API-Q-005` | accepted | Do generated types replace shared API wrappers? | No. First stage keeps thin handwritten wrappers using generated types. | Client architecture stability. |

## 10. Verification Plan

```text
- dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json writes OpenAPI artifact.
- npm.cmd --prefix energymanagement.client run generate:api-types writes generated TypeScript types.
- npm --prefix .\energymanagement.client run build verifies generated types compile with client.
- npm --prefix .\energymanagement.client run test -- --run verifies client tests.
- server integration/API tests still verify runtime behavior.
- if check:api is run, generated artifacts must already be part of the expected state for git diff --exit-code to pass.
```
