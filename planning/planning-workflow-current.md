# Current Planning Workflow

Status: current workflow  
Scope: current implementation baseline, planning gates and next work direction

## 1. Current Point

```text
OpenAPI structural contract + generated semantic constants are first-stage/baseline support.

L1 backend/API/persistence/session flow is implemented for the current core backend slices:
- register client account;
- login client account;
- current user;
- logout;
- create individual applicant party;
- create connection request.

First-stage L1 client flows are implemented for:
- app shell/routing/providers;
- shared typed L1 API wrappers;
- shared fetch/ProblemDetails/form error mapping;
- current-user session bootstrap;
- register client account UI;
- login client account UI;
- create individual applicant party UI on Account page.

The next work should consume implemented backend contracts and implemented first-stage client baseline, not re-plan them as missing.
```

## 2. Current Contract Artifact Baseline

Repo-grounded baseline:

| Area | Current evidence | Status | Current rule |
|---|---|---|---|
| OpenAPI artifact | `EnergyManagement.Tools generate-openapi`, `Shared/openapi.json`, `energymanagement.client/src/shared/api/generated/openapi-types.ts`, root `generate/check:api` scripts | first-stage implemented | Use and check existing artifacts; do not redo infrastructure unless explicitly in scope |
| Generated constants | `EnergyManagement.Tools generate-client-constants`, `Shared/constants.json`, `Shared/errorcodes.json`, Tools checker/tests | implemented baseline | Use generated semantic constants; add new constants through the existing generator/check flow |
| L1 endpoint metadata | `EnergyManagement.Server/L1/Controllers/L1Controller.cs` exposes L1 request/response DTOs and ProblemDetails statuses | first-stage implemented | Keep metadata updated when API changes |
| L1 generated TS types | `energymanagement.client/src/shared/api/generated/openapi-types.ts` includes L1 auth/applicant/request paths and DTOs | support baseline available | Client slices should consume generated types |
| Client API wrappers | Thin handwritten wrappers use generated OpenAPI schema types and typed path constants for L1 auth/applicant support | first-stage implemented for current L1 auth/applicant endpoints | Extend per client slice when new endpoint is consumed |
| E2E auth baseline | Root Playwright config and `tests/e2e/auth` register/login coverage | implemented legacy-auth baseline | Protect current legacy-auth E2E unless L1 auth consolidation is explicitly in scope |

## 3. Current L1 Backend / Client Baseline

| Area | Backend/API state | Client/UI state | Next planning action |
|---|---|---|---|
| Register client account | implemented endpoint, DTO `email/password`, response ids/email, persistence and tests | first-stage `/register` UI implemented with password confirmation, API submit, ProblemDetails mapping and success -> `/login` | Document/use `SL-ACC-001.client`; decide auto-login only if UX scope asks |
| Login client account | implemented endpoint, credential validation, L1 cookie sign-in, current-user response and tests | first-stage `/login` UI implemented with validation, API submit, session query invalidation and success -> home | Document/use `SL-AUTH-001.client`; route target remains future-review if UX changes |
| Current user | implemented protected current-user endpoint/query/session validation and tests | first-stage session bootstrap implemented through `SessionProvider`, current-user query and `useSession`; protected route policy pending | Document/use `SL-AUTH-002.client`; plan route guard/global failure policy separately |
| Logout | implemented protected logout endpoint/session clearing and tests | shared logout API wrapper exists; concrete logout UI/cache/navigation flow not confirmed | Do not mark logout client UI implemented; create `.client.md` only when UI starts |
| Create individual applicant party | implemented protected applicant create endpoint, server-derived account id, persistence and tests | first-stage Account page applicant create UI implemented; local read-only success state, Edit action and notification; read-current after refresh missing | Document/use `SL-APPL-001.client`; plan current applicant read next |
| Create connection request | implemented protected request create endpoint, server-selected applicant, no required response body and tests | request creation UI and My Requests read context not implemented | Plan request `.client.md` only after current applicant/read direction is clear |

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

Do not treat generated TypeScript types as implemented feature UI.

Do not treat implemented feature UI as a reason to stop checking the generated contract.

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

## 7. Client Sidecar Gate

When documenting or implementing a client sidecar:

```text
1. Read the parent backend slice.
2. Read generated contract/API docs.
3. Read client-wide conventions.
4. Inspect current client code before assigning status.
5. Use architecture/folder-based Visual Client Implementation Flow.
6. Separate current implementation from future/read/route-guard/test gaps.
7. Keep Behavior Coverage separate from Test / Verification Plan.
8. Mark component/E2E tests as planned/gap unless repo evidence shows them.
9. Sync relevant questions to slice-questions-register.md.
10. Sync future implementation notes to slice-implementation-notes-register.md.
```

## 8. Optional Result / Maybe Gate

When planning or refactoring server-side repository/query APIs, read:

```text
planning/slices/shared/maybe-for-optional-results.md
```

Use `Maybe<T>` when absence of the resulting object is a normal possible outcome.

Especially for repositories:

```text
GetByIdAsync(id) -> Maybe<Entity>
GetByEmailAsync(email) -> Maybe<Account>
GetCurrentActive...Async(...) -> Maybe<T>
Find...Async(...) -> Maybe<T>
```

Application handlers unwrap `Maybe<T>` and map absence to the correct use-case result:

```text
invalid credentials
not found
validation/domain error
successful empty state
```

Do not make repositories decide API/use-case meaning.

Current repo status:

```text
Current L1 repositories still use nullable returns such as `Task<Account?>` and `Task<ApplicantParty?>`.
The Maybe convention is a target for new/refactored repository APIs; do not claim it is already implemented until code changes prove it.
```

## 9. Cross-Cutting / Helper Slice Workflow

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

## 10. CSRF Gate

When planning browser unsafe API requests or auth/session flow:

```text
1. Read scenario-browser-security-addendum.md.
2. Read CC-CSRF-001-antiforgery-behavior-items.md.
3. Read CC-CSRF-001-antiforgery-token-session-context.md.
4. Link parent business slice API/security section to CC-CSRF-001.
5. Do not implement antiforgery mechanics separately inside business slices.
```

Current known status remains docs/planning/future hardening unless repo evidence later shows antiforgery implementation.

## 11. Testing Workflow Gate

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

## 12. Current Remaining Client Work Order

Recommended order for remaining concrete client work:

```text
1. Logout UI/cache/navigation, if needed for the auth baseline.
2. Current applicant read after refresh:
   - current applicant read endpoint/read model;
   - Account page load state;
   - verification status direction.
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

## 13. Implementation Flow Detail Rule

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
