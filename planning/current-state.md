# Current State

## Current phase

Pre-L1 cleanup is mostly completed. The project is being prepared for L1 domain/API refactor.

## Current date

2026-05-08

## Current branch

`my-changes`

## Current goal

Bring the old educational/experimental ASP.NET Core + React project to an L1 diploma MVP for processing client requests and simple document workflow for the network company ООО "ZSK".

## Current task

Fix current integration test infrastructure before L1 refactor: test database connection now uses LocalDB, remaining issue is test data/order isolation.

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
- Integration test database connection defaults to LocalDB `TestEnergyManagement` and can be overridden with `ConnectionStrings__Test`.
- Test host replaces the central `ConnectionStringNames.ManagementDbConfigurationKey`; `DbNameOptions` was removed as unnecessary indirection.
- Significant architecture/testing/configuration decisions should add short `Diploma note` blocks to `action-log.md`; these notes must not mention AI agents or assistant workflow.
- Old shared constants were split into focused server API route/contract classes under `EnergyManagement.Server/Api`.
- `IProblemDetailsService` is registered for API authentication/authorization problem responses.

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
| active | Stabilize integration test infrastructure | LocalDB connection is fixed; full test run still has data/order coupling failures. |
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
- Full integration test run currently fails after DB connection succeeds because some tests share data/order state.

## Last updated

2026-05-08 - diploma note protocol clarified to exclude AI-agent/workflow content.
