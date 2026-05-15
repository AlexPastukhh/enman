# Current Planning Workflow

Status: current workflow  
Scope: current implementation baseline, planning gates and next work direction

## 1. Current Point

```text
OpenAPI structural contract + generated semantic constants are first-stage/baseline support.

L1 backend/API/persistence flow is implemented for the current core backend slices:
- register client account;
- login client account;
- current user;
- logout;
- create individual applicant party;
- create connection request.

The next work should consume these implemented backend contracts through draft-driven client sidecar planning, not reimplement backend/API infrastructure.
```

## 2. Current Contract Artifact Baseline

Repo-grounded baseline:

| Area | Current evidence | Status | Current rule |
|---|---|---|---|
| OpenAPI artifact | `EnergyManagement.Tools generate-openapi`, `Shared/openapi.json`, `energymanagement.client/src/shared/api/generated/openapi-types.ts`, root `generate/check:api` scripts | first-stage implemented | Use and check existing artifacts; do not redo infrastructure unless explicitly in scope |
| Generated constants | `EnergyManagement.Tools generate-client-constants`, `Shared/constants.json`, `Shared/errorcodes.json`, Tools checker/tests | implemented baseline | Use generated semantic constants; add new constants through the existing generator/check flow |
| L1 endpoint metadata | `EnergyManagement.Server/L1/Controllers/L1Controller.cs` exposes L1 request/response DTOs and ProblemDetails statuses | first-stage implemented | Keep metadata updated when API changes |
| L1 generated TS types | `energymanagement.client/src/shared/api/generated/openapi-types.ts` includes L1 auth/applicant/request paths and DTOs | support baseline available | Client slices should consume generated types when concrete client work starts |
| Client API wrappers | Thin handwritten wrappers are still the expected first-stage strategy, but migration is per client slice | planned per slice | Use generated OpenAPI types when concrete client work starts |
| E2E auth baseline | Root Playwright config and `tests/e2e/auth` register/login coverage | implemented baseline | Protect current legacy-auth E2E unless L1 auth consolidation is explicitly in scope |

## 3. Current L1 Backend Baseline

| Backend area | Current evidence | Status | Client/UI status | Next planning action |
|---|---|---|---|---|
| Register client account | `POST /api/l1/auth/register`; `L1RegisterClientAccountDto(email,password)`; `L1RegisterClientAccountResponse(AccountId,Email)`; integration tests for creation/duplicate email | implemented backend/API/persistence | Registration page, password confirmation UI and auto-login decision are not completed client flows | Use `SL-ACC-001`; plan registration/auth client sidecar when concrete client work starts |
| Login client account | `POST /api/l1/auth/login`; command handler validates credentials/activation, signs L1 cookie, returns current-user shape; tests cover success and safe invalid failures | implemented backend/API/session | Login form/session state integration is not completed client flow | Use `SL-AUTH-001`; start auth/session client baseline before protected UI flows |
| Current user | `GET /api/l1/auth/current-user`; L1 marker/current account lookup; tests cover unauth, non-existing account, legacy-shaped cookie rejection and valid marker | implemented backend/API/session query | Client bootstrapping/route guard/current user cache is not completed client flow | Use `SL-AUTH-002`; plan client auth bootstrap with login/logout |
| Logout | `POST /api/l1/auth/logout`; clears cookie and returns 204; test confirms current-user becomes 401 | implemented backend/API/session | Logout UI/session invalidation is not completed client flow | Use `SL-AUTH-003`; treat as unsafe browser command for future CSRF planning |
| Create individual applicant party | protected `POST /api/l1/applicant-parties/individual`; account id derived from L1 cookie; response ids; integration tests | implemented backend/API/persistence | Applicant form UI and field-level ProblemDetails mapping are not completed client flows | Use `SL-APPL-001`; plan after auth/session baseline |
| Create connection request | protected `POST /api/l1/requests`; DTO `details + address`; server-selected current active applicant; no required response body; integration/domain tests | implemented backend/API/persistence | Request creation UI, My Requests read context and E2E browser flow are not completed client flows | Use `SL-REQ-001`; plan after applicant data UI |

## 4. Contract Artifact Gate For New Work

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

## 5. OpenAPI Gate

For any API endpoint used by client:

```text
- endpoint/method is documented;
- request DTO is documented;
- response DTO or absence of required body is documented;
- success status is documented;
- ProblemDetails statuses are documented;
- endpoint contract status is explicit;
- generated client type is available when client consumes it.
```

Do not treat missing client wrapper migration as missing OpenAPI infrastructure.

Do not treat generated TypeScript types as implemented feature UI.

## 6. Constants Gate

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

## 7. Cross-Cutting / Helper Slice Workflow

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

## 8. CSRF Gate

When planning browser unsafe API requests or auth/session flow:

```text
1. Read scenario-browser-security-addendum.md.
2. Read CC-CSRF-001-antiforgery-behavior-items.md.
3. Read CC-CSRF-001-antiforgery-token-session-context.md.
4. Link parent business slice API/security section to CC-CSRF-001.
5. Do not implement antiforgery mechanics separately inside business slices.
```

Current known status remains docs/planning/future hardening unless repo evidence later shows antiforgery implementation.

## 9. Testing Workflow Gate

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

For L1 applicant/request browser E2E, wait until the corresponding client feature UI/read flow exists.

## 10. Current Client Work Order

Recommended order for the next concrete client work:

```text
1. Auth/session client baseline:
   - login;
   - current-user bootstrapping;
   - logout;
   - session state/route guard direction.

2. Applicant Data UI:
   - individual applicant form;
   - ProblemDetails field/global mapping;
   - success handling.

3. Request Creation UI:
   - current active applicant context display;
   - request details/address form;
   - command success convention;
   - ProblemDetails display.

4. My Requests read/list/detail:
   - target read model;
   - route/navigation target for request creation success.

5. Browser E2E happy paths after client and read flows exist.
```

## 11. Implementation Flow Detail Rule

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
