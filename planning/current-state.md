# Current State

## Current phase

Pre-L1 cleanup completed. Project is being prepared for L1 domain/API refactor.

## Current date

2026-05-08

## Current branch

`my-changes`

## Current goal

Привести старый учебно-экспериментальный ASP.NET Core + React проект к L1-версии дипломного приложения для обработки клиентских заявок и документооборота сетевой компании ООО «ЗСК».

## Current task

Организовать живую planning-документацию по структуре variant 2 и зафиксировать текущее состояние проекта для будущих AI-агентов.

## Current implementation status

### Already present in repository

- ASP.NET Core backend project.
- React/Vite/TypeScript frontend project.
- Domain project.
- .NET test project.
- Playwright E2E folder at repository root.
- Existing domain classes from old model:
  - `Client`;
  - `IndividualClient`;
  - `Manager`;
  - `ClientRequest`;
  - `IndividualRequest`;
  - `RequestReview`.
- Existing server endpoints for registration/login/user and individual request creation.
- Existing EF Core mapping for old entities.
- Planning folder with initial documents:
  - `classes.md`;
  - `general project info.md`;
  - `solution-map-and-cleanup-plan.md`;
  - `usecases.md`.

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
→ client logs in
→ client creates IndividualApplicantParty
→ client submits request
→ employee sees requests
→ employee takes request for review
→ employee approves or rejects
→ system creates simple ContractDraft on approval
→ system sends email notification
```

## Explicitly not in L1

- ИП / ЮЛ applicant types;
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

## Next steps

1. Rename current planning files to stable names:
   - `general project info.md` → `general-project-info.md`;
   - `classes.md` → `domain-model.md`;
   - `usecases.md` → `use-cases.md`.
2. Archive deprecated root `PROJECT_PLANNING.md`.
3. Fix UTF-8 corruption in the end of `solution-map-and-cleanup-plan.md`.
4. Add `current-state.md`, `decisions.md`, `action-log.md`, `risk-log.md`, `agent-rules.md`, `layer-plan.md`, `api-plan.md`, `testing-strategy.md`.
5. Add explicit L1 implementation cut to `domain-model.md`.
6. Start L1 domain refactor:
   - `Client` → `Account` / `ClientAccount`;
   - `IndividualClient` → `IndividualApplicantParty` plus optional profile separation;
   - `Manager` → `EmployeeAccount`;
   - `IndividualRequest` → `ConnectionRequest` or `ClientRequest` subclass;
   - `RequestReview.IsApproved` → `ReviewDecision`.
7. Add unit tests for L1 domain invariants.
8. Configure EF Core TPH mapping for L1 inheritance.
9. Implement L1 API endpoints.
10. Add integration tests and Playwright E2E scenarios.

## Current blockers / known issues

- Integration tests currently require SQL Server/test database.
- Full solution build may be affected by frontend `.esproj` / JavaScript SDK availability in some environments.
- Existing old root `PROJECT_PLANNING.md` conflicts with new layered planning.
- Existing domain model still uses old class names.
- Existing planning file `solution-map-and-cleanup-plan.md` contains corrupted text near the end and should be repaired.
- Existing `general project info.md` contains ChatGPT response wrapper text and should be cleaned.

## Last updated

2026-05-08 — planning variant 2 archive prepared from current GitHub branch state and existing planning documents.
