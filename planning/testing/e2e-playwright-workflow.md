# E2E Playwright Workflow

Status: current E2E testing workflow  
Scope: Playwright, backend/frontend startup, E2E scope, locator policy, test data, scenario coverage

## 1. Purpose

E2E tests verify that a user-visible scenario works through the real browser, client application, HTTP API and server-side behavior.

```text
Browser UI
-> React client
-> HTTP API
-> server/application/domain/persistence/session
-> response
-> visible UI result
```

E2E tests are not a replacement for:

```text
- client/component tests;
- server integration/API tests;
- domain unit tests;
- API contract tests.
```

## 2. What E2E Tests Should Prove

E2E tests should prove:

```text
- user path can be executed in a real browser;
- real HTTP request/response happens;
- backend accepts/rejects the action as expected;
- route/session/navigation changes are wired;
- persisted/server-visible result is visible after navigation/reload when relevant;
- critical cross-layer failure/stale/auth states produce the expected visible outcome.
```

E2E tests should not prove every client UI detail.

Detailed form validation, ARIA field relations, disabled-state matrices and client error parser details belong to client/component tests.

## 3. Source Of Truth

Each E2E test should trace to:

```text
scenario text spec
+ scenario DATA spec
+ scenario UI spec, when exists
+ behavior items / UI behavior items
+ parent slice
+ .client.md sidecar, when exists
```

`.client.md` should include:

```text
## End-to-End Tests
```

Table:

| E2E test | Scenario/UI items | Cross-layer purpose | Setup | User path | Server communication checked | Expected result | Status |
|---|---|---|---|---|---|---|---|

## 4. Location And Ownership

E2E is repository-level, not client-unit-level.

Target structure:

```text
playwright.config.ts

tests/e2e/
  auth/
    register.spec.ts
    login.spec.ts
  pages/
    RegisterPage.ts
    LoginPage.ts
  support/
    exactName.ts
    testData.ts
    authHelpers.ts
```

Do not keep Playwright config pointing to `energymanagement.client/src/tests` while E2E specs live in root `tests/`.

## 5. Playwright Web Server Strategy

Playwright should explicitly start both backend and frontend.

Reason:

```text
E2E tests should have one visible command and one config that starts the system under test.
```

Target:

```text
webServer[0] = backend
webServer[1] = frontend
```

Backend should be started without ASP.NET launch profile / SpaProxy.

Reason:

```text
Frontend is started explicitly by Playwright.
Backend SpaProxy should not secretly start another Vite process.
```

## 6. Vite Proxy And CORS

Browser opens:

```text
https://localhost:5173
```

Client sends API requests as relative paths:

```text
/api/...
```

Vite dev server proxies those requests to:

```text
https://localhost:7250/api/...
```

Therefore the browser sees a same-origin request:

```text
https://localhost:5173/api/...
```

CORS is not involved while the browser/client uses relative `/api` paths.

CORS can appear if client code calls backend directly:

```text
https://localhost:7250/api/...
```

Rule:

```text
Browser E2E should open frontend origin.
Client API calls should remain relative /api unless CORS is intentionally configured and documented.
```

## 7. Target Root Playwright Config

Example target config:

```ts
import { defineConfig, devices } from "@playwright/test";

const isCI = !!process.env.CI;

export default defineConfig({
  testDir: "./tests/e2e",
  timeout: 30_000,
  workers: 1,
  forbidOnly: isCI,
  retries: isCI ? 1 : 0,
  reporter: [
    ["list"],
    ["html", { open: "never" }],
  ],
  use: {
    baseURL: "https://localhost:5173",
    headless: true,
    ignoreHTTPSErrors: true,
    viewport: { width: 1280, height: 720 },
    trace: "on-first-retry",
    screenshot: "only-on-failure",
  },
  projects: [
    {
      name: "chromium",
      use: { ...devices["Desktop Chrome"] },
    },
  ],
  webServer: [
    {
      name: "backend",
      command:
        "dotnet run --project EnergyManagement.Server/EnergyManagement.Server.csproj --no-launch-profile -- --urls https://localhost:7250",
      url: "https://localhost:7250/swagger/index.html",
      reuseExistingServer: !isCI,
      timeout: 120_000,
      ignoreHTTPSErrors: true,
      stdout: "pipe",
      stderr: "pipe",
      env: {
        ASPNETCORE_ENVIRONMENT: "Development",
      },
    },
    {
      name: "frontend",
      command: "npm --prefix energymanagement.client run dev",
      url: "https://localhost:5173",
      reuseExistingServer: !isCI,
      timeout: 120_000,
      ignoreHTTPSErrors: true,
      stdout: "pipe",
      stderr: "pipe",
    },
  ],
});
```

Notes:

```text
- workers: 1 is the safe default until test data isolation is stronger.
- headless: true is normal mode.
- headed/debug should be separate commands.
- exact backend ready URL may be changed if swagger endpoint is not stable for E2E startup.
```

## 8. Package Scripts

Root `package.json` should expose E2E commands:

```json
{
  "scripts": {
    "test:e2e": "playwright test",
    "test:e2e:headed": "playwright test --headed",
    "test:e2e:ui": "playwright test --ui"
  }
}
```

Avoid platform-specific debug scripts until needed.

## 9. E2E Test Data Strategy

Each E2E test must own its test data.

Rules:

```text
- use unique emails/identifiers per test;
- avoid hardcoded reusable user data unless DB reset/seed is guaranteed;
- do not depend on previous test order;
- prefer setup helpers for preconditions;
- test the target behavior through UI;
- use API/database setup only for non-target preconditions.
```

Example:

```ts
export function uniqueEmail(prefix = "e2e") {
  return `${prefix}.${Date.now()}.${Math.random().toString(36).slice(2)}@example.com`;
}
```

## 10. Authentication Strategy

Do not force every E2E test to login through UI.

Rules:

```text
- login flow itself is tested once through UI;
- other authenticated scenarios may use auth/session setup helpers;
- session setup must still represent real authenticated app state.
```

## 11. Network / Server Communication Assertion

E2E should often prove server communication happened.

Example:

```ts
const responsePromise = page.waitForResponse((response) =>
  response.url().includes("/api/") &&
  response.request().method() === "POST"
);

await registerPage.register({
  email,
  password,
  passwordConfirmation: password,
});

const response = await responsePromise;
expect(response.ok()).toBeTruthy();
await expect(page).toHaveURL(/\/login$/i);
```

Do not assert every field error in E2E happy path.

## 12. Minimal First E2E Set

Initial E2E set:

```text
E2E-AUTH-001 — Register happy path
E2E-AUTH-002 — Login happy path
```

Later after client/slice planning:

```text
E2E-REQ-001 — Create connection request happy path
E2E-REQ-002 — Client sees own request details
E2E-REVIEW-001 — Employee approves request
E2E-REVIEW-002 — Employee rejects request with/without feedback warning
```

## 13. Playwright Locator Policy

Use user-facing locators.

Preferred:

```text
1. getByRole(role, { name })
   For buttons, links, headings, alerts, dialogs, tables, navigation.

2. getByLabel(label)
   For form controls, especially password inputs.

3. getByText(text)
   For non-interactive visible results/status when role/label is not the right target.

4. getByTestId
   Only if no meaningful user-facing semantic target exists.
```

Native semantic HTML first:

```text
button -> <button>
link/navigation -> <a> / router Link
input -> <input> + <label>
textarea -> <textarea> + <label>
select -> <select> + <label>
```

Explicit ARIA only when native semantics are not enough:

```text
alert / status / dialog / aria-describedby / aria-invalid / aria-current / icon-only accessible name
```

## 14. Playwright Exact Matching Rule

Playwright and React Testing Library have different matching defaults.

In Playwright:

```text
string name matching defaults to case-insensitive substring matching.
{ exact: true } gives case-sensitive whole-string matching.
exact is ignored when the value is a regular expression.
```

Use:

```ts
page.getByRole("link", { name: "Register", exact: true });
```

or escaped anchored regex when case-insensitive exact match is needed:

```ts
export function escapeRegExp(value: string) {
  return value.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
}

export function exactName(value: string) {
  return new RegExp(`^${escapeRegExp(value)}$`, "i");
}

page.getByRole("link", { name: exactName("Register") });
```

Do not build raw regex from UI text without escaping.

## 15. Current Repo Cleanup Rules

Existing E2E tests should be simplified toward this shape:

```text
- move to tests/e2e;
- use root playwright.config.ts;
- use webServer backend + frontend;
- remove detailed field-error assertions from happy-path E2E;
- use unique test data;
- use getByLabel for form controls;
- replace raw getByRoleFullName regex helper;
- fix async no-await bugs.
```
