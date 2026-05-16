# API Contract Planning Index

Status: current API contract planning index / server validation principles added  
Scope: client/server API contract, OpenAPI structural contract, generated semantic constants, API errors, server request validation policy

## 1. Purpose

This folder owns API boundary contract rules.

It separates:

```text
OpenAPI structural contract
generated semantic constants
API error contract
FluentValidation/API error-code migration notes
server request validation responsibility
```

Cross-cutting implementation/status slices live under:

```text
planning/slices/cross-cutting/
```

## 2. Files

```text
planning/api/client-server-contract-principles.md
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
planning/api/fluentvalidation-error-code-policy-note.md
```

Related cross-cutting validation consumer rule:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 3. Core Split

```text
OpenAPI
= structural API contract:
  endpoint paths, HTTP methods, request DTOs, response DTOs,
  status codes, ProblemDetails response schemas, operation ids.

Generated constants JSON
= semantic API/client constants:
  stable client-facing error codes, ProblemDetails extension names,
  ServerError / ServerValidationError field names,
  temporary legacy route constants while OpenAPI migration is incomplete.

FluentValidation
= server request DTO/query validation:
  required fields, branch/discriminator rules, mutually exclusive fields,
  allowed query values, basic API DTO shape.

Application/domain validation
= business invariants:
  ownership, account/entity existence, selected entity belongs to account,
  domain value objects, state transitions, no-write/atomicity.

Client local code
= presentation and behavior:
  ErrorCode -> UI message, stale/refetch behavior, DTO field -> form field mapping.
```

## 4. Primary Cross-Cutting Slices

| Slice | Responsibility | Current status |
|---|---|---|
| `planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md` | OpenAPI artifact and generated TypeScript type workflow | first-stage implemented; wrapper migration/hardening remains |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Generated semantic constants and constants testing workflow | implemented baseline; client-consumer usage remains per slice |
| `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` | Antiforgery token/session context support and API security error normalization | implementation-ready draft |
| `planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | FluentValidation server request DTO/query validation principles and business-slice consumer rules | implementation-ready principles; L1 adoption planned |

## 5. Current Baseline Before Client Slices

The API artifact baseline exists and should be used by new client/server work:

```text
1. Structural API contract is generated through Shared/openapi.json.
2. Client TypeScript DTO/types are generated from Shared/openapi.json.
3. Semantic constants are generated through Shared/constants.json and Shared/errorcodes.json.
4. Artifact generation is explicit command/tooling work, not server startup side effects.
5. Root scripts provide generate/check API workflow.
6. Request-level FluentValidation for L1 is a planned validation layer, not a fully implemented baseline.
```

Current commands:

```bash
npm run generate:api
npm run check:api
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
```

## 6. Current Direction For New Client/Server Work

Before implementing missing client/server slices:

```text
1. Do not redo OpenAPI/constants infrastructure.
2. Classify endpoint status: target L1 / legacy-current / temporary compatibility / internal.
3. Use generated OpenAPI types for DTO/request/response structure.
4. Use generated constants for semantic error/field/extension names.
5. Keep thin handwritten client API wrappers unless a later ADR/slice explicitly changes that.
6. Record any per-slice contract gaps in the parent slice or `.client.md` once concrete client work starts.
7. For server input changes, plan FluentValidation request DTO/query validation before application/domain handler logic.
```

## 7. Remaining API Future Review Items

| ID | Area | Question / review item | Current direction | Status |
|---|---|---|---|---|
| API-FR-001 | Client wrappers | How quickly should existing wrappers migrate to generated OpenAPI types? | Per concrete client slice; do not mass-migrate without scope | future review |
| API-FR-002 | Endpoint inventory | Should a formal endpoint classification inventory be generated or maintained? | Keep local classification in slice/API docs first | future review |
| API-FR-003 | OpenAPI hardening | Should `check:api` become a CI-required gate? | Use current command locally; revisit under CI hardening | future review |
| API-FR-004 | Full generated client | Should the project move beyond generated types to a generated client? | Not in first stage; handwritten wrappers remain | future review |
| API-FR-005 | L1 validation mechanism | Should L1 standardize manual validators or a pipeline/filter? | Use CC-VALIDATION-001; decide when first L1 validator is implemented | open |
