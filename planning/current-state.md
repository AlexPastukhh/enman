# Current State

## Current phase

Pre-L1 cleanup is mostly completed. The project is being prepared for L1 domain/API refactor.

## Current date

2026-05-09

## Current branch

`my-changes`

## Current goal

Bring the old educational/experimental ASP.NET Core + React project to an L1 diploma MVP for processing client requests and simple document workflow for the network company ООО "ZSK".

## Current task

Backend and frontend validation contract coverage are stabilized. Next safe task: start the L1 domain refactor from `Client` to `Account` / `ClientAccount`.

## Current implementation status

### Already present in repository

- ASP.NET Core backend project.
- React/Vite/TypeScript frontend project.
- Domain project.
- .NET test project.
- Playwright E2E folder at repository root.
- Existing old domain classes:
  - `Client`;
  - `IndividualClient`;
  - `Manager`;
  - `ClientRequest`;
  - `IndividualRequest`;
  - `RequestReview`.
- Existing old server endpoints for registration/login/user and individual request creation.
- Existing EF Core mapping for old entities.
- Structured planning folder exists.

### Pre-L1 cleanup already done

- `.gitignore` updated for generated artifacts.
- Generated folders removed/ignored:
  - `_site/`;
  - `api/`;
  - `playwright-report/`;
  - `test-results/`;
  - client Playwright reports/test-results.
- Template files removed:
  - ASP.NET `WeatherForecast`;
  - Vite/React template assets.
- `examples/` removed as non-project donor/example code.
- E2E tests remain in root `tests/`.
- .NET tests remain in `Tests.EnergyManagement`.
- Integration test database connection is owned by `IntegrationTestFixture` and currently targets LocalDB `TestEnergyManagement`.
- Integration tests use immutable fixture actors for baseline users instead of static mutable test users; the baseline user is seeded once during collection fixture initialization.
- Test host receives the fixture connection string and replaces the central `ConnectionStringNames.ManagementDbConfigurationKey`; `DbNameOptions` was removed as unnecessary indirection.
- Significant architecture/testing/configuration decisions should add short `Diploma note` blocks to `action-log.md`; these notes must not mention AI agents or assistant workflow.
- Old shared constants were split into focused server API route/contract classes under `EnergyManagement.Server/Api`.
- `IProblemDetailsService` is registered for API authentication/authorization problem responses.
- API validation integration tests now cover the current auth and individual request validation contract, including error codes and field names.
- Frontend registration component tests and validation-error unit tests pass against the current client-side validation and backend problem-details contract.
- Frontend production build passes after session provider/import cleanup and registration view restoration.

## Current L1 implementation cut

L1 must implement only:

- `Account`;
- `ClientAccount`;
- `EmployeeAccount`;
- `ApplicantParty`;
- `IndividualApplicantParty`;
- `ClientRequest`;
- `ConnectionRequest`;
- `MeteringDeviceRequest`;
- `RequestStatus`;
- `RequestReview`;
- `ReviewDecision`;
- `ContractDraft`;
- `EmailNotification`.

L1 business flow:

```text
Guest registers
-> client logs in
-> client creates IndividualApplicantParty
-> client submits request
-> employee sees requests
-> employee takes request for review
-> employee approves or rejects
-> system creates simple ContractDraft on approval
-> system sends email notification
```

## Explicitly not in L1

- entrepreneur / legal entity applicant types;
- document uploads;
- PDF generation;
- contract versioning;
- mock verification;
- request clarification;
- extended search and filters;
- email confirmation;
- password recovery;
- rate limiting;
- account lockout;
- Windows Negotiate;
- anonymous requests;
- SMS;
- internal messages;
- real government integrations;
- electronic signature.

## Task board

| Status | Task | Notes |
|---|---|---|
| done | Rename planning files to stable names | `general-project-info.md`, `domain-model.md`, `use-cases.md` exist. |
| done | Archive deprecated root `PROJECT_PLANNING.md` | Original file moved to `planning/archive/PROJECT_PLANNING.md`; root copy removed from active planning. |
| done | Verify UTF-8/mojibake state in planning files | Active planning files were checked with `rg`; mojibake markers were not found outside the archived old root plan. |
| done | Add living planning documents | `current-state.md`, `decisions.md`, `action-log.md`, `risk-log.md`, `agent-rules.md`, `layer-plan.md`, `api-plan.md`, `testing-strategy.md` exist. |
| done | Add explicit L1 implementation cut to planning | L1 cut is present here; verify `domain-model.md` before domain refactor. |
| partial | Split old shared constants | Focused API route/contract classes exist under `EnergyManagement.Server/Api`; old commented constants infrastructure still exists. |
| done | Stabilize integration test infrastructure | Shared fixture owns test lifecycle/connection string and immutable baseline actors; full `.NET` test run passed 85/85 after cleanup on 2026-05-08. |
| done | Cover current API validation contract | Expected validation errors moved to `ExpectedValidationErrors`; auth/request validation cases pass in full `.NET` test run 94/94 on 2026-05-09. |
| done | Stabilize frontend registration validation tests | Vitest registration component tests and validation-error unit tests pass 21/21; frontend build passes on 2026-05-09. |
| done | Improve planning navigation protocol | README, agent rules and current state now guide next safe actions. |
| done | Add diploma note protocol | Significant work can now leave short notes for future diploma text in `action-log.md`. |
| next | Start L1 domain refactor | Begin with `Client` to `Account` / `ClientAccount`. |
| later | Add unit tests for L1 domain invariants | Do after first L1 domain classes are introduced. |
| later | Configure EF Core TPH mapping for L1 inheritance | Depends on L1 domain model. |
| later | Implement L1 API endpoints | Depends on domain and EF mapping. |
| later | Add integration tests and Playwright E2E scenarios | Depends on L1 API behavior and test database strategy. |

## Current blockers / known issues

- Integration tests currently require SQL Server LocalDB/test database.
- Full solution build may be affected by frontend `.esproj` / JavaScript SDK availability in some environments.
- Existing domain model still uses old class names.
- Existing `general-project-info.md` may contain ChatGPT response wrapper text and should be reviewed.
- Integration tests rely on collection fixture baseline seed plus local per-scenario data for state-changing cases; new integration tests should avoid hidden shared state and static mutable actors.

## Last updated

2026-05-09 - Frontend registration validation tests and build stabilized; Vitest passed 21/21 and frontend build passed.
