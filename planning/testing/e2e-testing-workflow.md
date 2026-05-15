# E2E Testing Workflow

Status: current E2E testing workflow  
Scope: Playwright, backend/frontend startup, E2E responsibility, locator policy, test data and current coverage

## 1. Purpose

E2E tests verify cross-layer wiring through the real application:

```text
browser
-> React client
-> HTTP API
-> backend/application/domain/persistence/session
-> HTTP response
-> client handling
-> visible route/page/session outcome
```

E2E uses the UI as the public entry point, but it does not own detailed client-visible behavior.

## 2. Responsibility Boundary

E2E should prove:

```text
- a user path runs in a real browser;
- a relevant real HTTP request happens;
- backend response is received;
- persistence/session/server state changes when the scenario requires it;
- the client shows a major observable result such as redirect, authenticated state or visible page result.
```

E2E should not exhaustively prove:

```text
- every field validation branch;
- deferred validation timing;
- exact aria-describedby / aria-errormessage details;
- all disabled/enabled states;
- component variants;
- full form state matrices.
```

Those details belong to client/component tests. API/server validation, persistence and orchestration details belong to server integration/API tests.

## 3. Playwright Startup Strategy

The repository-level command is:

```bash
npm run test:e2e
```

Playwright owns both processes through `webServer`:

```text
backend  -> dotnet run --project EnergyManagement.Server/EnergyManagement.Server.csproj --no-launch-profile -- --urls https://localhost:7250
frontend -> npm --prefix energymanagement.client run dev -- --host 127.0.0.1
```

Backend uses `--no-launch-profile` so ASP.NET SpaProxy does not start a second Vite process.

Readiness targets:

```text
backend  -> https://localhost:7250/swagger/index.html
frontend -> https://127.0.0.1:5173
```

The Playwright browser still opens `https://localhost:5173`. The frontend
readiness URL uses `127.0.0.1` to avoid local IPv6 `localhost` resolution issues
where Node probes `::1` while Vite is listening on IPv4.

## 4. Vite Proxy And CORS Rule

The browser opens:

```text
https://localhost:5173
```

Client API calls must remain relative:

```text
api/... or /api/...
```

Vite proxies `/api` to:

```text
https://localhost:7250
```

That keeps browser requests same-origin from the user's perspective. Do not add CORS workarounds unless the client intentionally starts calling `https://localhost:7250/api/...` directly and that decision is documented.

## 5. Test Location

Current E2E files live under:

```text
tests/e2e/
  auth/
    register.spec.ts
    login.spec.ts
  pages/
    RegisterPage.ts
    LoginPage.ts
  support/
    apiResponse.ts
    locators.ts
    testData.ts
```

The main Playwright config is repository-level:

```text
playwright.config.ts
```

Do not add a competing client-local Playwright config for the same E2E suite.

## 6. Locator Policy

Use user-facing locators:

```text
buttons, links, headings, alerts, dialogs, navigation -> getByRole(role, { name })
form controls -> getByLabel(label)
ordinary visible result -> getByText(text), when role/label is not appropriate
test ids -> only when user-facing semantics do not exist
```

For password inputs, use `getByLabel`, not `getByRole("textbox")`.

Avoid:

```text
- CSS classes;
- internal DOM structure;
- nth-child selectors;
- component implementation details.
```

When exact case-insensitive matching is needed, use an escaped anchored regex helper. Never build a raw regex from UI text without escaping.

## 7. Page Object Responsibility

E2E Page Objects are thin and stateless by default.

They may:

```text
- open a page;
- expose major page landmarks;
- locate primary user-facing controls;
- perform repeated user actions.
```

They should not:

```text
- store scenario input data as instance state;
- assert detailed field errors;
- become component/form test objects;
- hide the test's main expected outcome;
- import broad client internals.
```

Scenario data stays visible in the spec:

```ts
await registerPage.register({
  email,
  password,
  passwordConfirmation: password,
});
```

## 8. Component Object vs Page Object

Detailed helpers for field errors, aria relations, validation timing and disabled-state behavior belong to component/client test objects, not E2E Page Objects.

E2E Page Objects stay focused on the cross-layer user action and the large observable outcome.

## 9. Test Data Strategy

E2E tests use unique data:

```ts
uniqueEmail("register");
uniqueEmail("login");
```

Tests must not depend on:

```text
- previous successful runs;
- fixed emails already in the database;
- test order.
```

For login E2E, the existing user precondition may be created through API setup. The login behavior itself must still be tested through the UI.

## 10. E2E Test Database Strategy

E2E uses the LocalDB test database:

```text
TestEnergyManagement
```

This is the same database target used by integration tests. The shared reset logic lives in:

```text
EnergyManagement.Testing/TestDatabase/TestDatabaseManager.cs
```

The repository E2E scripts reset the test database before Playwright starts:

```bash
dotnet run --project EnergyManagement.Tools -- reset-test-db
```

Playwright then starts the real backend process with:

```text
ConnectionStrings__ManagementDb = TestEnergyManagement LocalDB connection string
```

E2E must not use the developer database from `appsettings.json` by accident. The backend webServer environment in `playwright.config.ts` owns the test connection string for E2E.

Integration tests and E2E share `TestDatabaseManager`, but they should not run concurrently against the same LocalDB database until stronger per-run isolation exists.

## 11. Server Communication Assertion

E2E should explicitly wait for the relevant API response:

```ts
const responsePromise = waitForApiResponse(
  page,
  "POST",
  "/api/auth/login"
);
```

Avoid broad waits such as "any POST" when a route-specific path can be used.

## 12. Current Coverage

Implemented:

| E2E test | Cross-layer purpose | Setup | User path | Server communication checked | Expected result |
|---|---|---|---|---|---|
| Register happy path | Browser registration reaches backend and persists new account | Unique email | `/register`, fill form, submit | `POST /api/auth/registerIndividual` | `2xx`, redirect to `/login`, login page visible |
| Login happy path | Browser login reaches backend and establishes session | Unique account via API setup | `/login`, fill form, submit | `POST /api/auth/login` | `2xx`, redirect to home |

Optional future coverage:

```text
duplicate email coarse failure, only if current UI exposes a stable coarse error surface.
```

## 13. What Not To Test In E2E

Do not expand E2E to cover:

```text
- every validation branch;
- field-level error mapping matrices;
- exact field aria labels;
- component variants;
- planned but unimplemented flows;
- backend contract details already covered by server/API tests.
```
