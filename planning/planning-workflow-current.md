# Current Planning Workflow

Status: current workflow

## 1. Current Point

```text
Client/server contract artifact infrastructure is now the current first-stage baseline.

OpenAPI structural contract + generated semantic constants are available through explicit generation/check commands.
The next work should consume those artifacts through draft-driven L1/client slice planning, not reimplement the artifact infrastructure.
```

## 2. Current Contract Artifact Baseline

Repo-grounded baseline:

| Area | Current evidence | Status | Current rule |
|---|---|---|---|
| OpenAPI artifact | `EnergyManagement.Tools generate-openapi`, `Shared/openapi.json`, `energymanagement.client/src/shared/api/generated/openapi-types.ts`, root `generate/check:api` scripts | first-stage implemented | Use and check existing artifacts; do not redo infrastructure unless explicitly in scope |
| Generated constants | `EnergyManagement.Tools generate-client-constants`, `Shared/constants.json`, `Shared/errorcodes.json`, Tools checker/tests | implemented baseline | Use generated semantic constants; add new constants through the existing generator/check flow |
| L1 endpoint metadata | `EnergyManagement.Server/L1/Controllers/L1Controller.cs` exposes L1 request/response DTOs and ProblemDetails statuses | first-stage implemented | Keep metadata updated when API changes |
| Client API wrappers | Thin handwritten wrappers are still the expected first-stage strategy, but migration is per client slice | planned per slice | Use generated OpenAPI types when concrete client work starts |
| E2E auth baseline | Root Playwright config and `tests/e2e/auth` register/login coverage | implemented baseline | Protect current legacy-auth E2E unless L1 auth consolidation is explicitly in scope |

## 3. Contract Artifact Gate For New Work

Before implementing or updating a client/server slice:

```text
1. Read planning/api/client-server-contract-principles.md.
2. Read CC-API-001.
3. Read CC-CONST-001.
4. Classify endpoint usage as target L1 / legacy-current / temporary compatibility / internal.
5. Run or rely on existing OpenAPI and constants artifact checks.
6. Use generated OpenAPI TypeScript types for request/response DTOs.
7. Use generated constants for semantic error/field/extension names.
8. Do not recreate OpenAPI/constants tooling unless the task explicitly asks for tooling hardening.
```

Current commands:

```bash
npm run generate:api
npm run check:api

dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
```

## 4. OpenAPI Gate

For any API endpoint used by client:

```text
- endpoint/method is documented;
- request DTO is documented;
- response DTO is documented;
- success status is documented;
- ProblemDetails statuses are documented;
- endpoint contract status is explicit;
- generated client type is available when client consumes it.
```

Do not treat missing client wrapper migration as missing OpenAPI infrastructure.

## 5. Constants Gate

When a slice introduces client-facing constants/error codes:

```text
1. Check CC-CONST-001.
2. Classify code:
   - ordinary validation;
   - important domain;
   - critical behavioral.
3. Update parent slice API error table.
4. Add/update source C# constants through the existing source of truth.
5. Regenerate Shared/constants.json and Shared/errorcodes.json.
6. Add/adjust API integration test or Tools test as appropriate.
7. If client work exists, map ErrorCode -> UI behavior in `.client.md`.
8. Run generate-client-constants --check.
```

## 6. Cross-Cutting / Helper Slice Workflow

Cross-cutting/helper slices follow the same planning shape as business slices:

```text
source requirements
-> behavior items
-> concern slice flow
-> implementation flow
-> tests/checks
-> coverage/questions/ADR impact
```

If current repo evidence shows a cross-cutting slice is now implemented, update its status table rather than leaving all coverage as planned.

## 7. CSRF Gate

When planning browser unsafe API requests or auth/session flow:

```text
1. Read scenario-browser-security-addendum.md.
2. Read CC-CSRF-001-antiforgery-behavior-items.md.
3. Read CC-CSRF-001-antiforgery-token-session-context.md.
4. Link parent business slice API/security section to CC-CSRF-001.
5. Do not implement antiforgery mechanics separately inside business slices.
```

Current known status remains docs/planning unless repo evidence later shows antiforgery implementation.

## 8. Testing Workflow Gate

When planning or implementing a slice, classify test coverage by layer:

```text
Domain unit tests
Server integration/API tests
Client/component tests
End-to-end tests
```

Use:

```text
planning/testing/testing-principles.md
planning/testing/e2e-testing-workflow.md
```

Do not migrate the existing auth E2E from legacy AuthController endpoints to L1 unless auth consolidation is explicitly in scope.

## 9. Implementation Flow Detail Rule

Implementation flow must not become a full code listing.

Include class/method/code details only when they explain:

```text
- contract boundary;
- non-obvious behavior;
- behavior that was discussed/questioned;
- important trade-off;
- extension/change point;
- error handling;
- testability/checkability;
- no-write/no-side-effect guarantee;
- generated artifact shape;
- API/client boundary.
```

Routine implementation details stay high-level.

If details dominate the flow, extract them into a sibling `.impl.md`.

Do not create `.impl.md` in advance.
