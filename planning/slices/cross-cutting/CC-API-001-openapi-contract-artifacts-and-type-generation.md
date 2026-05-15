# CC-API-001 — OpenAPI Contract Artifacts And Type Generation

Status: implementation-ready draft  
Slice type: cross-cutting slice  
Layers: Server API metadata + Shared/openapi.json + client generated TypeScript types + checks  
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

## 2. Why This Is A Cross-Cutting Slice

This is not a business scenario.

It is cross-cutting because many business/client slices depend on API shape, DTOs and response statuses.

### Observable/support behavior

```text
- server exposes OpenAPI JSON;
- Shared/openapi.json is generated and committed;
- client TypeScript types are generated from Shared/openapi.json;
- client API wrappers use generated types;
- checks detect stale OpenAPI/types artifacts.
```

### Implementation path

```text
- improve server OpenAPI metadata;
- classify client-facing endpoints;
- generate Shared/openapi.json;
- generate openapi-types.ts;
- add generation/check scripts;
- update client API wrappers;
- document per-slice API contract usage.
```

### Independent testability/checkability

```text
- artifact generation can run without implementing a business slice;
- check can fail when Shared/openapi.json or generated types are stale;
- client typecheck can catch DTO mismatch;
- server integration/API tests still verify runtime behavior.
```

## 3. Inputs / Sources

| Source | Purpose |
|---|---|
| `planning/api/client-server-contract-principles.md` | Contract split and generated artifact rules |
| `planning/api/openapi-contract-generation.md` | OpenAPI generation and quality checklist |
| `planning/api/api-error-contract.md` | ProblemDetails/error response shape |
| server controllers / endpoint metadata | Runtime contract source |
| server request/response DTOs | OpenAPI schema source |
| `Shared/openapi.json` | Generated structural contract artifact |
| generated `openapi-types.ts` | Client type source |

## 4. Concern-Derived Behavior Items

| ID | Behavior | Concern flow step |
|---|---|---|
| CC-API-SRV-001 | Server exposes client-facing endpoints through OpenAPI. | F01 |
| CC-API-SRV-002 | Client-facing DTOs appear in OpenAPI schemas. | F01 |
| CC-API-SRV-003 | Success and ProblemDetails response statuses are documented. | F02 |
| CC-API-SRV-004 | Client-facing endpoints are classified as target L1, legacy/current, temporary compatibility or internal. | F03 |
| CC-API-ART-001 | `Shared/openapi.json` is generated and committed. | F04 |
| CC-API-ART-002 | Client TypeScript types are generated from `Shared/openapi.json`. | F05 |
| CC-API-CL-001 | Client API wrappers use generated OpenAPI types. | F06 |
| CC-API-CHK-001 | Checks detect stale OpenAPI and generated TypeScript artifacts. | F07 |
| CC-API-NW-001 | OpenAPI does not replace generated semantic error-code constants. | F08 |
| CC-API-NW-002 | No generated artifact is written during normal server startup. | F08 |
| CC-API-TEST-001 | Server integration/API tests still verify runtime contract behavior. | F09 |

## 5. Coverage Overview

| Behavior item | Concern flow | Implementation flow | Test/check coverage | Status |
|---|---|---|---|---|
| CC-API-SRV-001 | F01 | I01 | OpenAPI generation/check | planned |
| CC-API-SRV-002 | F01 | I01 | generated schema review/type generation | planned |
| CC-API-SRV-003 | F02 | I02 | OpenAPI diff + API tests | planned |
| CC-API-SRV-004 | F03 | I03 | contract inventory review | planned |
| CC-API-ART-001 | F04 | I04 | artifact diff/check | planned |
| CC-API-ART-002 | F05 | I05 | type generation + typecheck | planned |
| CC-API-CL-001 | F06 | I06 | client typecheck/tests | planned |
| CC-API-CHK-001 | F07 | I07 | check script/tool | planned |
| CC-API-NW-001 | F08 | I08 | docs + constants slice | accepted |
| CC-API-NW-002 | F08 | I07 | command-only workflow | accepted |
| CC-API-TEST-001 | F09 | I09 | server integration/API tests | planned |

## 6. Structural Contract Concern Flow

### F01 — Server publishes structural API shape

Server OpenAPI must expose client-facing endpoint paths, methods and DTO schemas.

Covers:

```text
CC-API-SRV-001
CC-API-SRV-002
```

Required behavior:

```text
- public client-facing endpoints appear in OpenAPI;
- request/response DTO schemas are visible;
- internal details are not accidentally exposed as public contract.
```

### F02 — Response statuses and ProblemDetails are visible

OpenAPI must describe success and error response statuses.

Covers:

```text
CC-API-SRV-003
```

Required behavior:

```text
- success response DTO/status is documented;
- validation/error ProblemDetails status is documented;
- protected endpoints document 401/403 ProblemDetails.
```

### F03 — Endpoint contract status is explicit

Endpoints must be classified before client generation.

Covers:

```text
CC-API-SRV-004
```

Current direction:

```text
L1 endpoints = target contract for new L1 slices.
Legacy AuthController endpoints = current/legacy auth support until migrated.
```

Required behavior:

```text
client slice must know whether it calls target, legacy/current, temporary compatibility or internal endpoint.
```

### F04 — OpenAPI artifact is generated and committed

`Shared/openapi.json` is generated from server metadata and committed.

Covers:

```text
CC-API-ART-001
```

Required behavior:

```text
contract change appears as a generated artifact diff.
```

### F05 — Client types are generated from OpenAPI

Client TypeScript types are generated from `Shared/openapi.json`.

Covers:

```text
CC-API-ART-002
```

Required behavior:

```text
client DTO/request/response types do not drift from server DTOs.
```

### F06 — Client API wrappers use generated types

Client API layer uses generated OpenAPI types.

Covers:

```text
CC-API-CL-001
```

Current first-stage direction:

```text
generated DTO/types + thin handwritten API wrappers.
```

### F07 — Stale artifacts are detected

Checks detect stale `Shared/openapi.json` and generated TypeScript types.

Covers:

```text
CC-API-CHK-001
```

Required behavior:

```text
generation/check command or script fails when committed generated artifacts are outdated.
```

### F08 — Structural and semantic contracts stay separate

OpenAPI does not replace generated semantic constants.

Covers:

```text
CC-API-NW-001
CC-API-NW-002
```

Required behavior:

```text
OpenAPI owns shape/types/statuses.
Generated constants own stable error codes and field/extension names.
No generated artifact is written during normal server startup.
```

### F09 — Runtime behavior is still tested

Generated OpenAPI/types do not replace server/API tests.

Covers:

```text
CC-API-TEST-001
```

Required behavior:

```text
server integration/API tests verify actual runtime responses, validation, ProblemDetails and persistence effects.
```

## 7. Implementation Flow

### I01 — Improve server OpenAPI metadata

Concern flow:

```text
F01
F02
```

Implementation direction:

```text
add or refine response metadata for client-facing endpoints;
ensure request/response DTOs are public and schema-friendly;
avoid accidental internal DTO exposure.
```

Typical metadata:

```csharp
[ProducesResponseType(typeof(SomeResponse), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
```

Protected endpoint metadata:

```csharp
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
```

### I02 — Define ProblemDetails schema exposure

Concern flow:

```text
F02
```

Implementation direction:

```text
document/expose native ProblemDetails as error response shape;
if client depends on errors extension, document expected extension shape.
```

### I03 — Classify endpoint public contract status

Concern flow:

```text
F03
```

Implementation direction:

```text
create API contract inventory before client generation.
```

Classification:

```text
target L1 contract
legacy/current support
temporary compatibility
internal/not client-facing
```

Open point:

```text
verify exact L1 endpoint list in current branch before generation.
```

### I04 — Generate Shared/openapi.json

Concern flow:

```text
F04
```

Option A:

```bash
dotnet run --project EnergyManagement.Server/EnergyManagement.Server.csproj
curl -k https://localhost:7250/swagger/v1/swagger.json -o Shared/openapi.json
```

Option B later:

```bash
dotnet swagger tofile --output Shared/openapi.json EnergyManagement.Server/bin/Debug/net8.0/EnergyManagement.Server.dll v1
```

Current direction:

```text
start with the fastest reliable option; move to CLI/tooling when stable.
```

### I05 — Generate client TypeScript types

Concern flow:

```text
F05
```

First-stage direction:

```bash
npx openapi-typescript Shared/openapi.json -o energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Do not immediately require a full generated client.

### I06 — Update client API wrappers

Concern flow:

```text
F06
```

Implementation direction:

```text
client API functions remain thin handwritten wrappers;
request/response DTO types come from generated OpenAPI types.
```

Example target location:

```text
energymanagement.client/src/shared/api/generated/openapi-types.ts
energymanagement.client/src/shared/api/httpClient.ts
energymanagement.client/src/shared/api/problemDetails.ts
```

Feature-specific wrappers can live under client architecture target location chosen for the slice.

### I07 — Add generation/check scripts

Concern flow:

```text
F07
F08
```

Possible scripts:

```bash
npm run generate:api
npm run check:api
```

or later Tools command:

```bash
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project EnergyManagement.Tools -- check-contracts
```

Rule:

```text
normal server startup does not write generated artifacts.
```

### I08 — Keep constants split intact

Concern flow:

```text
F08
```

Implementation direction:

```text
do not move stable domain/client-facing error codes into OpenAPI as the only source;
do not add new route constants when OpenAPI can cover routes;
keep legacy route constants only while migration requires them.
```

### I09 — Tests/checks

Concern flow:

```text
F09
```

Minimum checks before client slices:

```text
dotnet build
dotnet test
generate-client-constants --check
generate/check Shared/openapi.json
generate/check openapi-types.ts
npm test / client typecheck
```

## 8. Target Artifacts / Components

| Artifact / component | Responsibility |
|---|---|
| `Shared/openapi.json` | Committed structural API contract artifact. |
| `openapi-types.ts` | Generated client TypeScript DTO/response types. |
| server endpoint response metadata | Makes contract useful and precise. |
| `planning/api/client-server-contract-principles.md` | Contract split rules. |
| `planning/api/openapi-contract-generation.md` | OpenAPI generation direction. |
| client API wrappers | Use generated types without fully generated client migration. |
| check scripts/tool command | Detect stale generated artifacts. |

## 9. Test / Check Plan

| Check | Purpose | Status |
|---|---|---|
| OpenAPI generation succeeds | Contract artifact can be produced | planned |
| `Shared/openapi.json` diff check | Stale artifact detection | planned |
| TypeScript type generation succeeds | Client type source can be produced | planned |
| generated `openapi-types.ts` diff/typecheck | Stale/generated type detection | planned |
| server integration/API tests | Runtime behavior still matches API contract | existing/expand |
| client typecheck/tests | Client wrappers use generated types correctly | planned |

## 10. Consumer Rule For Business/Client Slices

When a slice exposes or consumes API:

```text
1. Identify endpoint contract status:
   target L1 / legacy-current / compatibility / internal.
2. Document endpoint/method/request DTO/response DTO/statuses in parent slice.
3. Ensure OpenAPI metadata can expose that shape.
4. If client uses it, use generated OpenAPI types in client API wrapper.
5. Keep semantic error codes in generated constants, not in handwritten client strings.
6. Add/update tests/checks.
```

Parent slice API table:

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|

`.client.md` API table:

| Client API function | Endpoint | Generated type(s) used | Error constants used | Status |
|---|---|---|---|---|

## 11. Local Questions

| ID | Question | Assumption / current direction | Status |
|---|---|---|---|
| Q-CC-API-001 | Exact public L1 endpoint list? | L1 endpoints are target for new slices; verify current branch before generation. | open |
| Q-CC-API-002 | Legacy AuthController migration timing? | keep as current/legacy auth support until migrated. | open |
| Q-CC-API-003 | First OpenAPI generation method? | start with running-server generation if fastest, move to CLI/tool later. | accepted direction |
| Q-CC-API-004 | Full generated client or types only? | types-only first + thin handwritten wrappers. | accepted |
| Q-CC-API-005 | OperationId policy? | use stable operation ids where practical. | open |
| Q-CC-API-006 | Where should generated types live? | `energymanagement.client/src/shared/api/generated/openapi-types.ts`. | accepted direction |
| Q-CC-API-007 | How to check stale artifacts? | generation + git diff check first; Tools command later if useful. | open |

## 12. ADR Impact

Decision notes / ADR candidates:

```text
- OpenAPI is source for structural API/DTO contract.
- Generated constants JSON is source for semantic constants.
- Generated artifacts are explicit-command artifacts, not server-startup side effects.
- Route constants in constants.json are temporary/legacy during OpenAPI transition.
- L1 endpoints are target contract for new slices; legacy endpoints remain current support until migration.
- OpenAPI quality requires explicit response metadata and ProblemDetails statuses where practical.
```

No full numbered ADR is created by this slice draft.
