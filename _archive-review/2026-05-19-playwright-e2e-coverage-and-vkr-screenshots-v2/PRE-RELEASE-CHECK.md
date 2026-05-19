# Pre-release check — Playwright E2E coverage and VKR screenshots v2

## Scope

Adds Playwright E2E support for Employee review and Agreement Exchange flows, plus a dedicated VKR screenshot runner.

## Runtime/product behavior

No production backend/client behavior is intentionally changed.

Changed areas:
- `tests/e2e/**`
- `EnergyManagement.Tools` test database seed command
- root `package.json` script
- `planning/testing/playwright-e2e-and-screenshot-plan.md`

## Route cleanup in E2E

Existing Playwright helpers/specs were updated from old `/api/l1/*` routes to current routes:
- `/api/auth/*`
- `/api/applicant-parties`
- `/api/requests`

`tests/e2e/support/l1ClientSetup.ts` is kept as a backward-compatible re-export wrapper and no longer contains `/api/l1`.

## Added test tooling

Added:
- `dotnet run --project EnergyManagement.Tools -- seed-e2e-demo-data`

This command seeds deterministic E2E demo data:
- Employee: `e2e.employee@example.com / ValidPassword111!`
- Client: `e2e.client@example.com / ValidPassword111!`
- Review request: `9004`
- Agreement request: `9005`

It does not add migrations or change production runtime behavior.

## Added Playwright specs

- `tests/e2e/employee/employee-request-review.spec.ts`
- `tests/e2e/agreement-exchange/agreement-exchange-flow.spec.ts`
- `tests/e2e/vkr-screenshots/vkr-client-screenshots.spec.ts`

## Added screenshot script

Root `package.json`:
- `screenshots:vkr`

## Checks I could do here

Static archive checks:
- No `/api/l1` remains under `tests/e2e/**/*.ts`.
- No deleted file is required for archive application.
- Wrapper folder is not present in zip root.
- Original changed files are backed up under `_archive-review/.../original-files`.

I could not run `.NET`, Playwright, or npm tests in this environment.

## Required local checks after applying

```powershell
dotnet build .\EnergyManagement.sln
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

npm.cmd run test:e2e -- --project=chromium

npm.cmd run screenshots:vkr
```

Focused checks:

```powershell
dotnet run --project .\EnergyManagement.Tools -- reset-test-db
dotnet run --project .\EnergyManagement.Tools -- seed-e2e-demo-data

npx playwright test tests/e2e/employee/employee-request-review.spec.ts --project=chromium
npx playwright test tests/e2e/agreement-exchange/agreement-exchange-flow.spec.ts --project=chromium
npx playwright test tests/e2e/vkr-screenshots/vkr-client-screenshots.spec.ts --project=chromium
```

Expected screenshot output:

```text
planning/thesis/assets/screenshots/
```


## v2 audit fixes

- Added missing `EnergyManagement.Testing.TestDatabase` using to `SeedE2eDemoDataCommand.cs`.
- Tagged VKR screenshot spec with `@screenshots`.
- Updated normal E2E scripts to exclude screenshot tests with `--grep-invert @screenshots`.
- Updated `screenshots:vkr` script to run only screenshot tests with `--grep @screenshots`.
