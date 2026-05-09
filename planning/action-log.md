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

## 2026-05-08 - Integration test lifecycle centralized

### Done

- Moved integration test lifecycle ownership to `IntegrationTestFixture`.
- Removed the old `TestDatabaseConnection` helper.
- Updated `WebAppFactory` to receive the test connection string from the fixture and use it for both configuration override and `AppDbContext`.
- Updated the integration test collection to share the fixture and prevent parallel execution against one test database.
- Added per-scenario database cleanup for client request tests that create users and requests.

### Checks

- `rg "TestDatabaseConnection|ConnectionStrings__Test" Tests.EnergyManagement EnergyManagement.Server` found no remaining references.
- First `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=normal"` passed compilation but failed 7 client request validation cases because previous request data leaked between tests.
- After adding cleanup to the affected scenarios, full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=normal"` passed 88/88.

### Diploma note

- Topic: integration testing lifecycle and database isolation.
- Why it matters: the test suite now centralizes test database setup and prevents shared state from affecting request-processing scenarios.
- Possible text use: testing chapter, reproducibility, integration testing limitations and mitigation.

## 2026-05-08 - Static test user removed

### Done

- Renamed `IntegrationTestFixure` to `IntegrationTestFixture`.
- Added immutable `TestIndividualActor` for fixture-created users.
- Seeded a read-only baseline client in the integration fixture.
- Removed mutable `TestIndividual` state objects.
- Moved request DTO comparison to stateless `RequestAssertions`.
- Updated auth and client request integration tests to use fixture actors or per-scenario users.

### Checks

- `rg "IntegrationTestFixure|TestIndividual.cs|AuthTestsIndividual|RequestsTestIndividual|SetRegisteredIndividual|UpdateRegisteredIndividual|TestIndividual =" Tests.EnergyManagement` found no remaining references.
- First full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=normal"` failed at compile time because `RequestAssertions` missed the DTO namespace import.
- After fixing the import, full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=normal"` passed 88/88.

### Diploma note

- Topic: integration test independence.
- Why it matters: tests no longer depend on a static mutable user or on previous test execution order; baseline data is prepared by the fixture and exposed through immutable actor objects.
- Possible text use: testing chapter, reliability of automated tests, test data management.

## 2026-05-08 - Fixture reset model corrected

### Done

- Removed manual fixture reset calls from individual tests.
- Kept database cleanup and baseline client seed in collection fixture initialization.
- Stored the shared immutable client actor in test class fields through constructors.
- Removed the technical `ClearDatabaseBeforeTests` test.
- Kept per-scenario users local to tests that mutate user state.

### Checks

- `rg "ResetDatabaseAsync|ClearDatabaseBeforeTests|_fixure" Tests.EnergyManagement/Integration Tests.EnergyManagement/TestHelpers` found no remaining references.
- Full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=normal"` passed 87/87.

### Diploma note

- Topic: fixture-based integration test setup.
- Why it matters: baseline data is created once for the integration test collection, while state-changing scenarios use local test data, reducing order coupling without repeatedly rebuilding the whole database state.
- Possible text use: testing chapter, integration test setup and data isolation.

## 2026-05-08 - Test project cleanup

### Done

- Removed empty/dummy test files and commented legacy constants test files.
- Removed `Xunit.Extensions.Ordering` from the test project.
- Removed `Order` attributes from active integration tests.
- Cleaned unused `using` directives in active tests/helpers.
- Simplified `DatabaseHelpers` by removing no-op `try/catch` wrappers.
- Updated local test-user cleanup to use `try/finally` in mutable integration scenarios.
- Fixed `HttpResponseAssertions.ShouldBeSuccess` failure wording so it reports expected success instead of an expected `500`.
- Fixed nullable handling in `IntegrationTestHelper.GetProblemDetailsAsync`.

### Checks

- `rg "Xunit.Extensions.Ordering|\\[Order|TestConstantsData|ConstantsTests|IndividualClientMethodsTests|Assert.True\\(true\\)|Chech|ResetDatabaseAsync|ClearDatabaseBeforeTests" Tests.EnergyManagement` found no remaining references.
- `rg "catch \\(Exception\\)|catch\\(Exception\\)|FIXED|getSeeds|SetTestOutputHelper" Tests.EnergyManagement -g "*.cs"` found no remaining references.
- Full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=minimal"` passed 85/85.

### Diploma note

- Topic: automated test maintainability.
- Why it matters: the test suite now contains fewer non-functional artifacts, no explicit order dependency, clearer HTTP diagnostics, and safer cleanup for state-changing integration tests.
- Possible text use: testing chapter, quality assurance process.

## 2026-05-08 - API validation contract coverage

### Done

- Moved expected API validation errors from `TestData` to focused `ServerValidationErrors`.
- Removed unused parsing/exception helpers from `IntegrationTestHelper`.
- Added missing integration validation cases for password special-character rules, wrong login password, request address required fields and address length limits.
- Fixed auth and client request validation assertions so missing expected errors fail the test instead of only being logged.
- Fixed server validation field mapping for address fields and `requestDetails`.
- Changed failed login password handling to return the public `password.is.wrong` validation contract for the `password` field.

### Checks

- First full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=normal"` failed 4/94 and exposed contract mismatches:
  - `requestDetails` was returned as `RequestDetails`;
  - wrong login password returned an empty parsed validation error because the controller returned raw domain error data.
- After fixes, full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=normal"` passed 94/94.

### Diploma note

- Topic: API contract validation and integration testing.
- Why it matters: integration tests now verify not only invalid input rejection, but also stable external error codes and field names returned by the backend.
- Possible text use: testing chapter, API contract reliability, validation behavior.

## 2026-05-09 - Validation assertion helper

### Done

- Renamed expected API validation error helper to `ExpectedValidationErrors`.
- Replaced repeated integration-test `All/Contains` checks with `IntegrationTestHelper.ShouldHaveValidationErrorsEquivalentTo`.
- Removed obsolete integration-test error collection logging helper.
- Updated the missing postal code case to expect the full current API contract: `postalCode.is.required` and `postalCode.is.invalid`.

### Checks

- `rg "ExpectedValidaitonErrors|ServerValidationErrors|allErrorsContained|expectedErrors\\.All|errors\\.All|LogTwoErrorCollections" Tests.EnergyManagement/Integration Tests.EnergyManagement/TestHelpers` found no remaining references.
- Full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=normal"` passed 94/94.

### Diploma note

- Topic: test assertion reliability.
- Why it matters: validation tests now compare the complete expected and actual error collections, so extra or missing API validation errors are detected.
- Possible text use: testing chapter, API contract testing methodology.

## 2026-05-09 - Validation error contract simplified

### Done

- Removed route path prefixing from `ServerValidationError.ErrorCode`.
- Simplified `ServerValidationError.Create` to accept only `fieldName` and `errorCode`.
- Updated validation problem-details creation to return field/error pairs without endpoint path coupling.
- Flattened auth expected validation errors in `ExpectedValidationErrors`; kept request/address errors grouped by request contract area.
- Removed nullable initialization warnings from `ServerValidationError`.

### Checks

- Full `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --logger "console;verbosity=minimal"` passed 94/94.

### Diploma note

- Topic: API validation error contract.
- Why it matters: validation responses now separate endpoint routing from validation semantics, making client-side handling and automated testing simpler.
- Possible text use: API design, validation contract, testing chapter.

## 2026-05-09 - Frontend validation tests stabilized

### Done

- Fixed generic form field registration so text inputs call the React Hook Form register function with the field name.
- Updated registration component tests to assert the submitted DTO and to verify that invalid local form data does not call the register mutation.
- Fixed component test helpers for password inputs, empty alert elements, and error-code-to-message comparison.
- Added unit coverage for frontend problem-details parsing and server validation error mapping.
- Updated frontend session provider usage and related imports so the client project compiles.
- Restored the register page view to render the registration form and page-level error message.

### Checks

- `npm.cmd run build` in `energymanagement.client` passed; Vite emitted only the large chunk warning.
- `npm.cmd test -- --run` in `energymanagement.client` passed 21/21 tests.

### Diploma note

- Topic: frontend validation and API error contract handling.
- Why it matters: registration form tests now cover successful submission, client-side validation blocking, and mapping of backend validation errors to form fields.
- Possible text use: testing chapter, frontend implementation, validation contract.

## 2026-05-09 - Auth component tests split by behavior

### Done

- Moved React Query/router render setup for component tests into a shared helper.
- Added a login test component helper and login component test scenarios.
- Split auth component tests into separate checks for initial render, valid-field validation UX, invalid-field validation UX, successful submit, and blocked invalid submit.
- Replaced boolean exact-error helper assertions with explicit visible-message collection comparison in tests.
- Wrapped debounce waiting in `act` so component validation tests run without React update warnings.
- Added mixed valid/invalid auth form UX scenarios that compare per-field expected errors and `null` values.

### Checks

- `npm.cmd test -- --run src/Tests/ComponentTest/Auth.test.tsx` in `energymanagement.client` passed 49/49 auth component tests; debounce UX tests still emit React `act` warnings.
- `npm.cmd run build` in `energymanagement.client` passed; Vite emitted only the large chunk warning.

### Diploma note

- Topic: frontend component testing methodology.
- Why it matters: auth form tests now separate validation user experience from submit behavior, making failures easier to diagnose.
- Possible text use: testing chapter, frontend validation testing.

## 2026-05-09 - Auth debounce UX tests made deterministic

### Done

- Reworked auth component debounce UX tests to use Vitest fake timers.
- Added explicit checks for three debounce states: immediately after input, just before the debounce delay, and after the debounce delay.
- Covered valid, mixed valid/invalid, and fully invalid field combinations for register and login validation UX.
- Added a form-field test helper for direct `change` events in fake-timer scenarios, while keeping `userEvent` for submit-flow tests.
- Allowed route render test setup to receive `userEvent.setup` options for future timer-sensitive tests.

### Checks

- `npm.cmd test -- --run src/Tests/ComponentTest/Auth.test.tsx` in `energymanagement.client` passed 52/52 auth component tests.

### Diploma note

- Topic: deterministic frontend validation testing.
- Why it matters: debounce-based form validation is verified without real-time polling, reducing flaky timing behavior while still checking visible user-facing validation states.
- Possible text use: testing chapter, frontend validation testing methodology.

## 2026-05-09 - Frontend test polling helpers removed and Playwright inspected

### Done

- Removed old real-time polling debounce helpers from component test field/page helpers.
- Updated valid login submit tests to pass form debounce validation through fake timers before clicking the disabled-until-valid submit button.
- Inspected Playwright-related files:
  - root `playwright.config.ts`;
  - client `energymanagement.client/playwright.config.ts`;
  - root `tests/registerTest.spec.ts`;
  - root `tests/TestPages/*`.

### Checks

- `rg "TryGetErrorMessageDebounced|GetErrorMessageDebounced|HasErrorDebounced|ExpectNoErrorDuringDebouncedValidationAsync|WaitForDebouncedValidationAsync|getErrorMessagesDebounced|getVisibleErrorMessagesDebounced|hasAnyErrorsDebounced" -n` found no remaining references.
- `npm.cmd test -- --run src/Tests/ComponentTest/Auth.test.tsx` in `energymanagement.client` passed 52/52 auth component tests.
- `npm.cmd run build` in `energymanagement.client` passed; Vite emitted only the existing large chunk warning.
- `npx.cmd playwright test --list` from repository root was blocked by npm cache/registry access.
- `.\energymanagement.client\node_modules\.bin\playwright.cmd test --config=playwright.config.ts --list` from repository root failed because root `playwright.config.ts` could not resolve `@playwright/test` from root `node_modules`.

### Notes

- Current Playwright tests require separately running frontend and backend services; no `webServer` is configured.
- Root Playwright dependencies are declared but root `node_modules` is not currently available.
- The client Playwright config points to `./src/tests`, while active frontend tests are under `src/Tests`; this looks like a stale config.
- Root E2E page helpers contain likely runtime bugs: `Locator.isVisible` is read as a property instead of called as `isVisible()`, and register form fill does not await async field fills.

### Diploma note

- Topic: E2E testing risk and test infrastructure.
- Why it matters: browser E2E tests currently exist but are not yet reliable as an automated verification layer because their configuration and page helper abstractions need cleanup.
- Possible text use: testing chapter, limitations/future work.

## 2026-05-09 - Initial L1 domain subset introduced in parallel

### Done

- Added a parallel L1 domain namespace `Domain.EnergyManagement.L1` without deleting or replacing old domain classes.
- Added the initial L1 account subset:
  - `Account`;
  - `ClientAccount`;
  - `AccountRole`.
- Added the initial L1 applicant subset:
  - `ApplicantParty`;
  - `IndividualApplicantParty`;
  - `ApplicantPartyType`.
- Added the initial L1 request subset:
  - `ClientRequest`;
  - `ConnectionRequest`;
  - `RequestStatus`.
- Added parallel unit tests in `L1DomainTests` while keeping all old unit tests.
- Kept EF mapping, API handlers and integration tests on the old model for now.
- Removed future L1 classes that do not yet have an equivalent tested implementation in the old project:
  - `EmployeeAccount`;
  - `MeteringDeviceRequest`;
  - `RequestReview`;
  - `ReviewDecision`;
  - `ContractDraft`;
  - `EmailNotification`.

### Checks

- `dotnet build Domain.EnergyManagement/Domain.EnergyManagement.csproj --no-restore` passed.
- `dotnet build Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-restore -p:BuildProjectReferences=false` compiled `Tests.EnergyManagement.dll` but returned exit code 1 because the referenced client `.esproj` could not resolve `Microsoft.VisualStudio.JavaScript.Sdk` in this environment.
- `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-build --filter "FullyQualifiedName~L1DomainTests"` passed 12/12.
- `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-build --filter "FullyQualifiedName~Tests.EnergyManagement.Unit"` passed 73/73.

### Diploma note

- Topic: domain model refactoring and separation of responsibilities.
- Why it matters: the initial L1 subset separates authentication accounts from applicant/legal party data while keeping the implemented scope aligned with behavior already present in the project.
- Possible text use: domain design chapter, architecture rationale, testing chapter.

## 2026-05-10 - L1 aggregate boundary rules defined

### Done

- Recorded current implemented L1 aggregate roots:
  - `ClientAccount`;
  - `IndividualApplicantParty`;
  - `ConnectionRequest`.
- Documented aggregate root creation rules and child entity ownership rules.
- Recorded future aggregate candidates and child entities separately from the current implemented L1 subset.
- Accepted `long` IDs as the L1 inter-aggregate reference strategy.
- Documented EF navigation rules, primitive collection restrictions and cross-aggregate invariant handling.
- Updated the current task board so the next safe step is aligning the parallel L1 domain code with these rules while keeping EF/API on the old model.

### Checks

- No tests were run because this was a planning-only change.

### Diploma note

- Topic: domain-driven aggregate boundary design.
- Why it matters: account, applicant and request lifecycles now have explicit ownership boundaries, reducing coupling and avoiding large aggregate graphs during the migration from the old model.
- Possible text use: domain design chapter, architecture rationale, maintainability and risk mitigation.
