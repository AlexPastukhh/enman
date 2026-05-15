# Client / Server Contract Principles

Status: current API/client contract principles  
Scope: OpenAPI, generated constants, generated artifacts, legacy route transition, client API layer

## 1. Main Decision

Client/server contract is split into two generated artifact channels.

```text
OpenAPI = structural API contract.
Generated constants JSON = semantic constants not represented well by OpenAPI.
```

Current repo status:

```text
OpenAPI/types generation is first-stage implemented.
Generated semantic constants are implemented baseline.
Client wrapper migration remains per concrete client slice.
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

Current artifacts:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

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

Current artifacts:

```text
Shared/constants.json
Shared/errorcodes.json
```

## 4. No Server Startup Writes

Generated artifacts must not be written during normal server startup.

Do not use hosted service / runtime file writer as the primary generation path.

Use explicit commands.

Current commands include:

```bash
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
npm run generate:api
npm run check:api
```

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

Before generating or consuming client types for a slice, the parent slice/API planning must identify whether it uses:

```text
target L1 endpoint
legacy/current endpoint
temporary compatibility endpoint
internal/not client-facing endpoint
```

Current target L1 endpoints visible in the generated OpenAPI artifact:

```text
POST /api/l1/auth/register
POST /api/l1/applicant-parties/individual
POST /api/l1/requests
```

## 7. Legacy Route Constants

Route constants in `Shared/constants.json` are temporary during migration.

Current direction:

```text
- keep legacy route constants only while current client still needs them;
- move endpoint path/route usage toward OpenAPI-generated contract;
- avoid adding new route constants when OpenAPI can cover them.
```

Do not remove legacy route constants as part of documentation-only reconciliation.

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

For new client/server slice work:

```text
1. Read current API contract docs and cross-cutting slice status.
2. Do not redo OpenAPI/constants infrastructure unless tooling hardening is explicitly in scope.
3. Classify endpoint status.
4. Update server endpoint/DTO/status metadata if the contract changes.
5. Regenerate/check OpenAPI and generated TypeScript types.
6. Regenerate/check semantic constants if new client-facing semantic constants are introduced.
7. Update thin client API wrappers to use generated types for the concrete slice.
8. Add/update server/client/E2E tests according to the testing responsibility boundary.
9. Update parent slice and `.client.md` docs only when concrete client work starts.
```

## 11. Remaining Future Review Items

| ID | Area | Question / review item | Current direction | Status |
|---|---|---|---|---|
| CSR-FR-001 | Full generated client | Should generated types evolve into a generated client? | Types-only first | future review |
| CSR-FR-002 | CI gate | Should `npm run check:api` and constants check be mandatory in CI? | Keep commands checkable; revisit with CI hardening | future review |
| CSR-FR-003 | Legacy auth migration | When should legacy AuthController client flows move to L1 endpoints? | Only under explicit L1 auth consolidation scope | open question |
| CSR-FR-004 | Route constants retirement | When can legacy route constants be removed from `Shared/constants.json`? | After client route usage is backed by OpenAPI/typed wrappers | future review |
