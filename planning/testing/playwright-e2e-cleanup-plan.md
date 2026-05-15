# Playwright E2E Cleanup Plan

Status: implementation prompt source / planned cleanup  
Scope: current Playwright config, E2E files, Page Objects, test data, scripts

## 1. Current Repo Findings

Current Playwright config:

```text
energymanagement.client/playwright.config.ts
```

Current config issues:

```text
- config is client-local;
- testDir points to ./src/tests;
- real E2E tests are in root tests/;
- headless is false by default;
- no webServer;
- no root test:e2e script.
```

Current E2E files:

```text
tests/registerTest.spec.ts
tests/TestPages/TestBase.ts
tests/TestPages/RegisterPage.ts
tests/TestPages/LoginPage.ts
```

Current E2E issues:

```text
- happy-path E2E asserts field-error details that belong to client/component tests;
- fixed email makes tests state-dependent;
- getByRoleFullName uses raw unescaped regex;
- SUTFormField uses role textbox for password fields;
- SUTFormField.HasErrorsAsync returns isVisible reference, not isVisible();
- RegisterPage.FillForm is not async and does not await field fills;
- Page Objects store scenario data by default.
```

## 2. Target File Layout

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

## 3. Root Package Scripts

Add to root `package.json`:

```json
{
  "scripts": {
    "test:e2e": "playwright test",
    "test:e2e:headed": "playwright test --headed",
    "test:e2e:ui": "playwright test --ui"
  }
}
```

## 4. Root Playwright Config

Create root `playwright.config.ts` using:

```text
- testDir: ./tests/e2e;
- baseURL: https://localhost:5173;
- headless: true by default;
- ignoreHTTPSErrors: true;
- workers: 1 initially;
- webServer array with backend + frontend.
```

Backend:

```text
dotnet run --project EnergyManagement.Server/EnergyManagement.Server.csproj --no-launch-profile -- --urls https://localhost:7250
```

Frontend:

```text
npm --prefix energymanagement.client run dev
```

## 5. E2E Test Refactor

Register E2E should prove:

```text
home/register path
-> fill unique data
-> real registration API response ok
-> redirect to login
```

Login E2E should prove:

```text
precondition existing account
-> login through UI
-> real login API response ok
-> authenticated route/home outcome
```

Do not assert:

```text
- no email field errors;
- no password field errors;
- no password confirmation field errors;
- every aria alert state.
```

Those belong to client/component tests.

## 6. Data Refactor

Replace fixed data:

```text
email@gmail.com
```

with unique data:

```ts
export function uniqueEmail(prefix = "e2e") {
  return `${prefix}.${Date.now()}.${Math.random().toString(36).slice(2)}@example.com`;
}
```

## 7. Locator Refactor

Use:

```text
getByLabel for fields;
getByRole for buttons/links/headings/alerts/dialogs;
exact: true for exact case-sensitive name;
exactName() escaped regex for exact case-insensitive name.
```

Remove or rewrite `getByRoleFullName`.

## 8. Page Object Refactor

Target Page Object shape:

```ts
export class RegisterPage {
  constructor(private readonly page: Page) {}

  async open() {
    await this.page.goto("/register");
  }

  async register(data: RegisterData) {
    await this.page.getByLabel("Email").fill(data.email);
    await this.page.getByLabel("Password").fill(data.password);
    await this.page.getByLabel("Password Confirmation").fill(data.passwordConfirmation);
    await this.page.getByRole("button", { name: "Register", exact: true }).click();
  }
}
```

## 9. Stop Conditions

Stop and ask before implementation if:

```text
- backend cannot start without SpaProxy through --no-launch-profile;
- swagger URL is not available as backend ready URL;
- Vite proxy is not used by client API calls;
- registration/login route names are uncertain;
- DB state makes login E2E impossible without setup helper.
```

## 10. Success Criteria

```text
- npm run test:e2e starts backend + frontend and runs tests;
- tests/e2e is the only Playwright testDir;
- E2E tests no longer assert detailed form-error behavior in happy paths;
- API response is observed for register/login flows;
- fixed email is removed;
- existing async bugs are fixed;
- role/label-first locators are used;
- Playwright report/test-results remain ignored.
```
