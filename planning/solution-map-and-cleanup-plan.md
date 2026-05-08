# Solution Map and Cleanup Plan

## Назначение

Документ фиксирует рабочую картину текущего solution и план наведения порядка перед доработкой проекта под диплом.

## 1. Текущая карта solution

### `Domain.EnergyManagement`

Назначение: доменная модель и бизнес-правила.

Сейчас содержит старую модель предметной области:

- `Client`;
- `IndividualClient`;
- `Manager`;
- `LegalEntityUser`;
- `ClientRequest`;
- `IndividualRequest`;
- `RequestReview`;
- value objects: `Email`, `Password`, `PhoneNumber`, `FullName`, `Address`, `PassportData`, `SNILS`;
- общие ошибки.

Целево привести к L1:

```text
Account
ClientAccount
EmployeeAccount
ApplicantParty
IndividualApplicantParty
ClientRequest
ConnectionRequest
MeteringDeviceRequest
RequestStatus
RequestReview
ReviewDecision
ContractDraft
EmailNotification
```

### `EnergyManagement.Server`

Назначение: ASP.NET Core API, EF Core, MediatR handlers, validation, infrastructure.

Сейчас содержит:

- API controllers;
- DTO;
- validation;
- constants/routes/contracts;
- EF Core context and migrations;
- commands/queries;
- infrastructure.

Проблема: старые папки смешивают DTO, validation, constants, error contracts, persistence and utility code.

Целевая организация:

```text
EnergyManagement.Server/
  Api/
    Controllers/
    Routes/
    Contracts/
      Auth/
      ApplicantParties/
      Requests/
      Common/
  Application/
    Commands/
    Queries/
    Validation/
  Infrastructure/
    Persistence/
    Email/
    ContractGeneration/
  Configuration/
```

### `energymanagement.client`

Назначение: React/Vite frontend.

Содержит:

- views/components/hooks;
- frontend constants;
- component tests;
- old generated/test artifacts already removed/ignored.

Целево:

- move server API/DTO typing to OpenAPI/orval generated client;
- keep frontend-only constants in frontend;
- do not use manual server/shared JSON as source of truth.

### `Tests.EnergyManagement`

Назначение: .NET unit/integration tests.

Оставить один тестовый проект.

Recommended structure:

```text
Tests.EnergyManagement/
  Unit/
    Domain/
    Application/
  Integration/
    Auth/
    Requests/
  TestHelpers/
  Fixtures/
```

### Root `tests`

Назначение: Playwright E2E tests.

Решение: оставить E2E отдельно в текущей корневой папке.

```text
tests/
  *.spec.ts
  TestPages/
```

### `Shared`

Сейчас: ручные JSON-константы для frontend.

Целево:

- DTO/API → OpenAPI/orval;
- error codes/enums/validation metadata → generator/tool;
- frontend-only constants → frontend.

## 2. Что считать исходниками

```text
Domain.EnergyManagement/
EnergyManagement.Server/
energymanagement.client/src/
Tests.EnergyManagement/
planning/
docs/
uml/
Shared/    temporary until contract generation is solved
tests/     Playwright E2E
```

## 3. Что не считать исходниками

Generated/template artifacts:

```text
_site/
api/
playwright-report/
test-results/
coverage/
bin/
obj/
*.csproj.user
```

## 4. Cleanup done

- `.gitignore` updated.
- Generated artifacts removed/ignored.
- ASP.NET/Vite template files removed.
- `examples/` removed.
- E2E tests kept in root `tests`.
- .NET tests kept in one project.
- Planning folder created.

## 5. Constants strategy

### Do not develop old `SharedConst`

Decision: do not keep old `SharedConst` as compatibility facade.

Instead split by responsibility:

- server HTTP routes → `EnergyManagement.Server/Api/Routes`;
- ProblemDetails constants → `EnergyManagement.Server/Api/Contracts/Common`;
- JSON field names for FluentValidation → `EnergyManagement.Server/Api/Contracts/*`;
- DTO/API client for frontend → OpenAPI/orval later;
- error codes/enums/validation metadata → separate generator/tool later.

### `JsonField.Of<T>()`

Purpose:

- server-side validation contract only;
- reads `[JsonPropertyName]`;
- returns JSON field name for validation response.

Not a domain concept and not frontend source of truth.

## 6. OpenAPI generation plan

Potential tools:

- `openapi-typescript` — types only;
- `orval` — types + client/hooks;
- `NSwag` — .NET-friendly generation.

Likely best candidate for React Query project: `orval`.

## 7. Domain L1 refactor plan

Planned migration:

```text
Client -> Account / ClientAccount
IndividualClient -> IndividualApplicantParty
Manager -> EmployeeAccount
IndividualRequest -> ConnectionRequest or ClientRequest subclass
RequestReview.IsApproved -> ReviewDecision
```

## 8. Next cleanup actions

1. Rename planning files to stable kebab-case names.
2. Archive old root `PROJECT_PLANNING.md`.
3. Fix corrupted UTF-8 text in old planning file.
4. Add `current-state.md`, `decisions.md`, `action-log.md`, `risk-log.md`, `agent-rules.md`.
5. Add explicit L1 implementation cut.
6. Start L1 domain refactor with unit tests.

## 9. Notes for AI agents

- Do not start large domain/API/frontend refactor in one pass.
- Always check current git status before changes.
- Update planning after changes.
- If old code uses old names, do not rename everything blindly.
- Prefer small vertical slices with tests.
