# Client / Server Contract Principles

Status: current API/client contract principles  
Scope: OpenAPI, generated constants, generated artifacts, legacy route transition, client API layer

## 1. Main Decision

Client/server contract is split into two generated artifact channels.

```text
OpenAPI = structural API contract.
Generated constants JSON = semantic constants not represented well by OpenAPI.
```

## 2. OpenAPI Owns Structural Contract

OpenAPI owns:

```text
- endpoint path;
- HTTP method;
- route/query/body parameters;
- request DTO shape;
- response DTO shape;
- status codes;
- ProblemDetails response shape;
- operation id, where practical;
- nullable fields and enums when represented in DTOs.
```

OpenAPI is the source for generated TypeScript API DTO/response types.

## 3. Generated Constants Own Semantic Contract

Generated constants JSON owns:

```text
- stable client-facing error codes;
- ProblemDetails extension names;
- ServerError / ServerValidationError field names;
- temporary legacy route constants until OpenAPI migration removes the need;
- other symbolic semantic constants that client must not hardcode.
```

Generated constants JSON does not own DTO shape when OpenAPI covers it.

## 4. No Server Startup Writes

Generated artifacts must not be written during normal server startup.

Do not use hosted service / runtime file writer as the primary generation path.

Use explicit commands.

## 5. Generated Artifacts Are Committed

Generated artifacts are repository contract artifacts.

Expected artifacts:

```text
Shared/openapi.json
Shared/constants.json
Shared/errorcodes.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Changes to generated artifacts should be visible in diffs.

## 6. Current Public Contract Boundary

Current planning assumption:

```text
L1 endpoints are the target public contract for new L1 slices.
Legacy AuthController endpoints remain current/legacy auth support until login/getUser/registration migration is completed.
```

Before generating client types for a slice, the parent slice/API planning must identify whether it uses:

```text
target L1 endpoint
legacy/current endpoint
temporary compatibility endpoint
```

## 7. Legacy Route Constants

Route constants in `Shared/constants.json` are temporary during migration.

Current direction:

```text
- keep legacy route constants only while current client still needs them;
- move endpoint path/route usage toward OpenAPI-generated contract;
- avoid adding new route constants when OpenAPI can cover them.
```

## 8. Client API Layer Rule

Client API functions should use generated OpenAPI types for request/response DTOs.

First migration stage:

```text
OpenAPI generates TypeScript DTO/types.
Thin client API wrapper functions remain handwritten.
```

This keeps client architecture boundaries while avoiding DTO drift.

Full generated client can be reconsidered later.

## 9. Contract Change Rule

A contract change must update all relevant artifacts:

```text
server endpoint/DTO/status metadata
Shared/openapi.json
generated TypeScript types
server source constants if semantic constants change
Shared/constants.json / Shared/errorcodes.json
client API wrappers / parser / message maps
tests/checks
slice/client docs
```

## 10. Current Work Order

Before missing client slices:

```text
1. Add/update API contract docs.
2. Improve OpenAPI metadata.
3. Generate Shared/openapi.json.
4. Generate client OpenAPI types.
5. Implement explicit client constants generator/checker.
6. Update client API layer to use generated types.
7. Then implement missing client slices.
```
