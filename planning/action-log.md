# Action Log

This file records completed actions. Keep entries short and factual.

## 2026-05-08 — Pre-L1 cleanup

### Done

- Created initial planning documents.
- Updated `.gitignore`.
- Removed generated artifacts:
  - `_site/`;
  - `api/`;
  - `playwright-report/`;
  - `test-results/`;
  - client Playwright reports/test-results.
- Removed template/donor files:
  - ASP.NET WeatherForecast files;
  - Vite/React template assets;
  - `examples/`.
- Kept root Playwright E2E tests.
- Kept .NET tests in `Tests.EnergyManagement`.
- Started separating old shared constants into more focused route/contract classes.

### Notes

- Cleanup is a pre-L1 step.
- Existing domain model still uses old names and has not yet been refactored to target L1.
- Integration tests may require SQL Server.

## 2026-05-08 — Planning variant 2 archive prepared

### Done

- Created structured planning folder variant:
  - `README.md`;
  - `current-state.md`;
  - `general-project-info.md`;
  - `layer-plan.md`;
  - `domain-model.md`;
  - `use-cases.md`;
  - `api-plan.md`;
  - `testing-strategy.md`;
  - `decisions.md`;
  - `action-log.md`;
  - `risk-log.md`;
  - `agent-rules.md`;
  - `solution-map-and-cleanup-plan.md`;
  - `archive/old-project-planning.md`.

### Purpose

Make planning usable as living handoff documentation for future AI agents.

## 2026-05-08 - API constants split verification

### Done

- Verified focused API route/contract classes under `EnergyManagement.Server/Api`.
- Registered ASP.NET Core `ProblemDetails` services for API auth redirects.
- Removed duplicate `EnergyManagement.Server.Data` using from `Program.cs`.
- Confirmed `UnauthorizedUserCantCreateConnectionRequest` passes.

### Notes

- Full integration test run still depends on an available SQL Server/test database.

## 2026-05-08 - Planning navigation protocol

### Done

- Rewrote `planning/README.md` as a clean readable planning entrypoint.
- Added task navigation protocol and final response navigation rules.
- Added final response navigation requirements to `agent-rules.md`.
- Reworked `current-state.md` into a task-board format with explicit statuses.

### Notes

- The next safe task is the L1 domain refactor, starting with `Client` to `Account` / `ClientAccount`.

## 2026-05-08 - Old root planning archived

### Done

- Moved root `PROJECT_PLANNING.md` to `planning/archive/PROJECT_PLANNING.md`.
- Updated `planning/archive/old-project-planning.md` to point to the archived original.
- Verified active planning files do not contain mojibake marker sequences outside the archived old root plan.
- Marked old root planning risk as mitigated.

### Notes

- The archived original remains historical context only and is not a source of truth.

### Diploma note

- Topic: scope control and source-of-truth management.
- Why it matters: deprecated plans were moved out of the active workflow to reduce the risk of implementing outdated requirements.
- Possible text use: risk analysis, requirements management.

## 2026-05-08 - Test database connection moved to LocalDB

### Done

- Added shared `TestDatabaseConnection` helper for integration tests.
- Replaced repeated hardcoded `DESKTOP-V6S02NC` test connection strings.
- Set default test database to LocalDB `TestEnergyManagement`.
- Added `ConnectionStrings__Test` environment variable override for future PC moves.
- Updated `WebAppFactory` to override application configuration for test database access.

### Checks

- `dotnet build Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-restore` passed.
- `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-build --filter "FullyQualifiedName~ClearDatabaseBeforeTests"` passed.
- Full `dotnet test --no-build` reached the database and passed 80/89 tests.

### Notes

- Remaining full-run failures are test data/order isolation problems, not connection failures.

### Diploma note

- Topic: integration testing infrastructure.
- Why it matters: the test database was made portable across developer machines through LocalDB defaults and environment variable override.
- Possible text use: testing chapter, deployment/local setup, reproducibility.

## 2026-05-08 - Test reporting and WebAppFactory configuration clarified

### Done

- Added explicit test reporting protocol to `README.md` and `agent-rules.md`.
- Updated `WebAppFactory` so test configuration replaces only `ConnectionStrings:ManagementDb` with the test database string.
- Removed `DbNameOptions`; Dapper code now reads the normal `ManagementDb` connection string directly.
- Removed obsolete `DbName`/`TestDbName` config and e2e `DbName__Name` override.
- Added `ConnectionStringNames` so code does not hardcode the `ManagementDb` key in handlers or test factory.

### Checks

- `dotnet build Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-restore` passed with 26 warnings.
- `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-build --filter "FullyQualifiedName~ClearDatabaseBeforeTests"` passed 2/2.
- Full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-build` failed 9/89; failures are data/order isolation symptoms after DB connection succeeds.
- After adding `ConnectionStringNames`, `dotnet build Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-restore` passed with 26 warnings and `ClearDatabaseBeforeTests` passed 2/2.

### Diploma note

- Topic: configuration maintainability.
- Why it matters: application code and tests now use the same named connection-string contract instead of a separate test-only DB-name switch.
- Possible text use: implementation chapter, maintainability, test environment design.

## 2026-05-08 - Diploma note scope clarified

### Done

- Updated diploma note protocol to exclude AI-agent and assistant-workflow topics.
- Removed the process-oriented diploma note from the planning navigation entry.

### Notes

- Diploma notes should describe the developed software system, its architecture, tests, configuration, scope control and risks.
- Internal planning and AI-assisted workflow may remain useful for development, but should not be presented as diploma content.
