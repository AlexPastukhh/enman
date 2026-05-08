# Solution map and cleanup plan

Документ фиксирует рабочую картину текущего solution и план наведения порядка перед доработкой проекта под диплом.

## 1. Текущая карта solution

### `Domain.EnergyManagement`

Назначение: доменная модель и бизнес-правила.

Сейчас содержит:

- value objects: `Email`, `Password`, `PhoneNumber`, `FullName`, `Address`, `PassportData`, `SNILS`;
- старую модель предметной области: `Client`, `IndividualClient`, `Manager`, `LegalEntityUser`, `ClientRequest`, `IndividualRequest`, `RequestReview`;
- общие ошибки: `Domain.EnergyManagement/Common/Error.cs`;
- временные/template-файлы: `Class1.cs`, `DocumentManaging/Untitled`.

Проблема: доменная модель пока отражает старый проект, а не целевую дипломную модель. Позже ее нужно привести к L1:

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

Назначение: ASP.NET Core API, EF Core, MediatR handlers, валидация, инфраструктура.

Сейчас содержит:

- API controllers;
- DTO в `Data/Dtos.cs`;
- FluentValidation validators в `Data/Validation.cs`;
- server constants в `Data/SharedConstants.cs`;
- генерацию/описание error constants в `Data/ErrorCodesToJson.cs`;
- закомментированный генератор constants JSON в `Data/ConstantsToJson.cs`;
- закомментированный hosted service `Infrastructure/ConstantWriterService.cs`;
- EF migrations;
- template-файлы ASP.NET: `WeatherForecast.cs`, `WeatherForecastController.cs`.

Проблема: в папке `Data` смешаны DTO, validation, constants, error contracts, db options и utility-код. Нужно разделить по назначению.

### `energymanagement.client`

Назначение: React/Vite frontend.

Сейчас содержит:

- UI views/components/hooks;
- frontend constants в `src/globConstants.ts`;
- прямой импорт JSON из корневой папки `Shared`;
- component tests в `src/Tests`;
- generated/test artifacts: `playwright-report`, `test-results`;
- template assets: `public/vite.svg`, `src/assets/react.svg`.

Проблема: клиент зависит от `../../Shared/*.json`, а эти JSON пока не являются надежно генерируемым контрактом.

### `Tests.EnergyManagement`

Назначение: .NET unit/integration tests.

Решение: оставить один тестовый проект, потому что общие helpers уже есть и это удобно для дипломного проекта.

Целевая структура внутри проекта:

```text
Tests.EnergyManagement/
  Unit/
  Integration/
  TestHelpers/
  Fixtures/
```

### `Shared`

Назначение сейчас: ручные JSON-константы, используемые frontend.

Сейчас содержит:

- `constants.json`;
- `errorcodes.json`;
- `testconstants.json`;
- invalid test constants.

Решение: временно оставить, потому что frontend сейчас от этого зависит. Позже заменить ручное редактирование на генерацию из server/domain source of truth.

### Корневые `tests`

Назначение: Playwright E2E tests.

Проблема: E2E тесты сейчас лежат отдельно от client tests, плюс есть playwright artifacts.

Решение: оставить E2E отдельно от .NET tests и держать в текущей корневой папке `tests`.

```text
tests/
  *.spec.ts
  TestPages/
```

На текущем этапе не переносить E2E в `energymanagement.client/e2e`, чтобы не создавать лишний churn.

### `api`, `_site`, `playwright-report`

Скорее всего generated artifacts:

- `api` - DocFX metadata/yml;
- `_site` - DocFX generated site;
- `playwright-report` - Playwright report.

Решение: не считать исходным кодом. После проверки добавить в `.gitignore` и удалить из репозитория, если они уже закоммичены.

### `examples`

Похоже на старый пример/донор кода.

Решение принято в ходе очистки: удалить вместе с остальными файлами шага 2, так как это не часть текущего дипломного solution.

## 2. Классификация файлов

### Оставить как исходники

```text
Domain.EnergyManagement/
EnergyManagement.Server/
energymanagement.client/src/
Tests.EnergyManagement/
planning/
docs/              если это рукописная документация
uml/               если это исходники диаграмм
Shared/            временно, пока frontend зависит от JSON
```

### Скорее всего удалить или игнорировать

```text
_site/
api/
playwright-report/
energymanagement.client/playwright-report/
energymanagement.client/test-results/
EnergyManagement.Server/EnergyManagement.Server.csproj.user
```

### Template-мусор, проверить и удалить

```text
Domain.EnergyManagement/Class1.cs
Domain.EnergyManagement/DocumentManaging/Untitled
EnergyManagement.Server/WeatherForecast.cs
EnergyManagement.Server/Controllers/WeatherForecastController.cs
energymanagement.client/public/vite.svg
energymanagement.client/src/assets/react.svg
```

### Не удалять без отдельной проверки

```text
EnergyManagement.Server/Migrations/
Shared/
examples/
docs/
uml/
```

## 3. Стратегия констант

Нужно разделить разные типы констант.

### Domain constants

Источник истины: `Domain.EnergyManagement`.

Примеры:

- error codes;
- request statuses;
- request types;
- validation limits;
- business constraints.

### API contract

Источник истины: `EnergyManagement.Server`.

Примеры:

- DTO request/response shapes;
- HTTP endpoints;
- ProblemDetails format.

Рекомендуемый способ передачи на frontend: OpenAPI generation.

### Frontend constants

Источник истины: frontend.

Примеры:

- client routes: `/`, `/login`, `/register`;
- UI labels;
- query keys;
- form display text.

Эти значения не должны лежать в server/shared JSON.

### Generated shared constants

Источник истины: server/domain, результат используется клиентом.

Примеры:

- error codes;
- enum values;
- validation metadata, если нужна клиентская валидация в том же формате;
- имена полей server validation contract.

Целевая схема:

```text
Domain/Server source of truth
        -> generation step
energymanagement.client/src/generated/server-constants.ts
energymanagement.client/src/generated/server-contracts.ts
```

## 4. Сервер -> клиент контракты

### DTO и API endpoints

Лучше генерировать из OpenAPI, а не вручную через JSON.

Целевая схема:

```text
ASP.NET Core Swagger/OpenAPI
        -> openapi-typescript / orval / NSwag
energymanagement.client/src/generated/api.ts
```

Плюсы:

- меньше ручного дублирования DTO;
- frontend получает TypeScript-типы;
- легче ловить расхождение API и клиента;
- проще масштабировать L1 -> L2.

### Error codes и справочники

OpenAPI плохо описывает весь набор возможных доменных error codes, поэтому их можно генерировать отдельно.

Целевая схема:

```text
Domain.EnergyManagement/Common/Error.cs
        -> contract generator
energymanagement.client/src/generated/error-codes.ts
```

### Routes

На первом этапе можно оставить `SharedConst.AppRoutes`, но целево:

- server routes должны жить рядом с controllers/minimal APIs;
- frontend должен использовать generated API client;
- ручные route strings в `Shared/constants.json` должны исчезнуть.

## 5. Целевая организация server folders

Вместо текущего большого `EnergyManagement.Server/Data` постепенно перейти к:

```text
EnergyManagement.Server/
  Api/
    Controllers/
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
  Constants/
```

На первом шаге можно не переносить все физически. Важно сначала зафиксировать назначение:

- DTO -> `Api/Contracts`;
- validators -> `Application/Validation`;
- db context/repositories -> `Infrastructure/Persistence`;
- generated contract helpers -> `Contracts` или отдельный tool;
- local server-only settings -> `Configuration`.

## 6. Тестовая стратегия

### .NET tests

Оставить один проект `Tests.EnergyManagement`.

Рекомендуемая структура:

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

### Frontend tests

Компонентные тесты оставить в frontend:

```text
energymanagement.client/src/Tests/
```

Позже можно переименовать в более стандартное:

```text
energymanagement.client/src/__tests__/
```

### E2E tests

Держать отдельно в текущей корневой папке:

```text
tests/
  *.spec.ts
  TestPages/
```

Главное: не держать E2E одновременно в нескольких местах.

## 7. Порядок очистки

### Шаг 1. Зафиксировать карту

Создан этот документ.

### Шаг 2. Проверить и удалить мусор

Статус: выполнено.

Проверить, что следующие файлы/папки нигде не используются:

```text
Class1.cs
Untitled
WeatherForecast.cs
WeatherForecastController.cs
public/vite.svg
src/assets/react.svg
examples/
api/
_site/
playwright-report/
test-results/
```

### Шаг 3. Обновить `.gitignore`

Добавить generated artifacts:

```text
_site/
api/
playwright-report/
**/playwright-report/
**/test-results/
*.csproj.user
```

`api/` добавлять в `.gitignore` только если решено, что DocFX yml не хранится в репозитории.

### Шаг 4. Удалить очевидный template/generated мусор

Удалять только после проверки и без трогания пользовательских изменений.

### Шаг 5. Разделить constants

Решение обновлено: не делать compatibility facade для `SharedConst`.

Причина: текущая реализация не является стабильным публичным контрактом, проект все равно приводится к L1 как первой рабочей версии. Поэтому нет смысла сохранять старый `SharedConst` как фасад и тянуть лишний слой.

Что сделать:

- разобрать `SharedConst` на логические классы;
- заменить места использования напрямую;
- удалить старый `SharedConst`, когда все ссылки будут заменены;
- перестать считать `Shared/constants.json` ручным source of truth;
- сделать список того, что реально нужно frontend.

Целево:

- сгенерировать error codes из domain;
- сгенерировать API types/client из OpenAPI;
- перенести frontend-only constants в frontend.

### Шаг 6. Подготовить OpenAPI generation

Выбрать инструмент:

- `openapi-typescript` - только типы;
- `orval` - типы + API client/hooks;
- `NSwag` - C#/.NET-friendly generation.

Для текущего React Query проекта вероятный лучший кандидат: `orval`.

### Шаг 7. Доменная L1-перестройка

После уборки структуры перейти к предметной модели:

```text
Client -> Account / ClientAccount
IndividualClient -> IndividualApplicantParty
Manager -> EmployeeAccount
IndividualRequest -> ConnectionRequest или ClientRequest с RequestType
IsApproved -> ReviewDecision
```

## 8. Ближайшие решения

Перед очисткой нужно принять 3 решения:

1. Храним ли `api/` и `_site/` в git как документацию или считаем generated artifacts.
2. Где держим E2E: принято решение оставить в текущей корневой папке `tests`.
3. Какой generator для API contracts берем первым: `openapi-typescript`, `orval` или `NSwag`.

Предварительная рекомендация:

```text
api/ и _site/ не хранить в git
E2E держать в текущей папке tests
для frontend API client использовать orval
```

## 9. Журнал решений и выполненных действий

Этот раздел нужен, чтобы новый AI agent без истории чата мог быстро понять текущее состояние проекта.

### Принятые решения

1. E2E tests остаются в текущей корневой папке `tests`.
2. `.NET` тесты остаются в одном проекте `Tests.EnergyManagement`, потому что там уже есть общие helpers.
3. `api/` и `_site/` считаются generated DocFX artifacts и не должны храниться как исходники.
4. `playwright-report/`, `test-results/` и аналогичные папки считаются generated artifacts.
5. `examples/` не является частью текущего дипломного solution и удаляется.
6. `SharedConst` не развивать и не сохранять через фасад совместимости.
7. `JsonField.Of<T>()` нужен только на сервере для FluentValidation: получить JSON-имя проблемного поля и вернуть его клиенту в validation response.
8. DTO и API endpoints для frontend в перспективе генерировать из OpenAPI.
9. Error codes, enum values и validation metadata, которые не являются DTO, генерировать отдельным CLI/tool.
10. Все важные решения, действия и планы должны отражаться в `planning`, чтобы проект можно было передать новому агенту без контекста чата.

### Что уже сделано

1. Создан файл `planning/solution-map-and-cleanup-plan.md`.
2. Обновлен `.gitignore`:

```text
_site/
api/
playwright-report/
**/playwright-report/
test-results/
**/test-results/
*.csproj.user
.dotnet-home/
```

3. Удалены template/generated/лишние файлы и папки:

```text
Domain.EnergyManagement/Class1.cs
Domain.EnergyManagement/DocumentManaging/Untitled
EnergyManagement.Server/WeatherForecast.cs
EnergyManagement.Server/Controllers/WeatherForecastController.cs
energymanagement.client/public/vite.svg
energymanagement.client/src/assets/react.svg
api/
_site/
playwright-report/
energymanagement.client/playwright-report/
energymanagement.client/test-results/
examples/
```

4. Из `energymanagement.client/index.html` убрана ссылка на удаленный `/vite.svg`.
5. Проверено, что корневые E2E tests остались на месте:

```text
tests/registerTest.spec.ts
tests/TestPages/LoginPage.ts
tests/TestPages/RegisterPage.ts
tests/TestPages/TestBase.ts
```

### Что может вызвать проблемы после очистки

1. Если кто-то открывал локальную документацию из `_site`, ее нужно будет заново генерировать через DocFX.
2. Если кто-то ожидал наличие DocFX metadata в `api/`, его тоже нужно будет генерировать заново.
3. Если frontend или docs ссылались на `/vite.svg`, ссылка уже удалена из `index.html`, но другие скрытые ссылки стоит проверять поиском.
4. Удаление `examples/` означает, что старый donor code больше недоступен в рабочем дереве.
5. Полная `dotnet build EnergyManagement.sln` в sandbox может падать из-за `energymanagement.client.esproj` и отсутствия `Microsoft.VisualStudio.JavaScript.Sdk`, это не обязательно связано с кодом проекта.
6. `EnergyManagement.Server/AppDbContext.cs` был изменен до очистки и не трогался в рамках этих действий.
7. `npm.cmd run build` запускается, но сейчас падает на существующих TypeScript errors в клиенте. Ошибки не связаны с удалением `vite.svg`/`react.svg`.

## 10. План разборки `SharedConst`

### Текущее состояние

Файл:

```text
EnergyManagement.Server/Data/SharedConstants.cs
```

Сейчас содержит разные ответственности:

- HTTP routes;
- ProblemDetails/validation response constants;
- JSON field names для FluentValidation;
- временный/лишний logging в nested classes;
- лишние using и закомментированный код.

### Как разбить

#### HTTP routes

Вынести в:

```text
EnergyManagement.Server/Api/Routes/AuthRoutes.cs
EnergyManagement.Server/Api/Routes/ClientRequestRoutes.cs
```

Назначение:

- использовать в attributes контроллеров;
- использовать в integration tests;
- позже заменить frontend route usage на OpenAPI/orval generated client.

Пример:

```csharp
namespace EnergyManagement.Server.Api.Routes;

public static class AuthRoutes
{
    public const string Controller = "api/auth";
    public const string RegisterIndividual = "registerIndividual";
    public const string Login = "login";
    public const string GetUser = "getUser";

    public const string RegisterIndividualPath = $"{Controller}/{RegisterIndividual}";
    public const string LoginPath = $"{Controller}/{Login}";
    public const string GetUserPath = $"{Controller}/{GetUser}";
}
```

#### ProblemDetails contract

Вынести в:

```text
EnergyManagement.Server/Api/Contracts/Common/ProblemDetailsContract.cs
```

Назначение:

- единый contract для server validation response;
- значения можно будет генерировать для frontend CLI tool.

Пример:

```csharp
namespace EnergyManagement.Server.Api.Contracts.Common;

public static class ProblemDetailsContract
{
    public const int ValidationStatusCode = 422;
    public const string ErrorsExtension = "errors";
    public const string ExceptionExtension = "exception";
}
```

#### JSON field names для FluentValidation

Вынести в:

```text
EnergyManagement.Server/Api/Contracts/Auth/AuthFieldNames.cs
EnergyManagement.Server/Api/Contracts/Requests/RequestFieldNames.cs
```

Назначение:

- только server-side validation;
- validators записывают имя проблемного JSON-поля;
- клиент получает это имя в response и привязывает ошибку к форме.

Важно:

- это не domain constants;
- это не frontend UI constants;
- это не нужно делать фасадом для старого `SharedConst`;
- это можно будет генерировать для frontend, если клиенту понадобится типизированная карта field names.

Пример:

```csharp
namespace EnergyManagement.Server.Api.Contracts.Auth;

public static class AuthFieldNames
{
    public static class Register
    {
        public static string Email => JsonField.Of<RegisterClientDto>(x => x.Email);
        public static string Password => JsonField.Of<RegisterClientDto>(x => x.Password);
        public static string PasswordConfirmation => JsonField.Of<RegisterClientDto>(x => x.PasswordConfirmation);
    }

    public static class Login
    {
        public static string Email => JsonField.Of<LoginDto>(x => x.Email);
        public static string Password => JsonField.Of<LoginDto>(x => x.Password);
    }
}
```

#### `JsonField`

Вынести в:

```text
EnergyManagement.Server/Api/Contracts/Common/JsonField.cs
```

Добавить namespace. Сейчас helper лежит без namespace, это нужно исправить.

Назначение:

- читать `[JsonPropertyName]`;
- возвращать реальное JSON-имя поля;
- использовать только в server contract/validation layer.

### Что обновить при разборке

Server:

```text
EnergyManagement.Server/Controllers/AuthController.cs
EnergyManagement.Server/Controllers/ClientRequestController.cs
EnergyManagement.Server/Controllers/ProjectController.cs
EnergyManagement.Server/Data/Validation.cs
EnergyManagement.Server/Data/Dtos.cs
EnergyManagement.Server/Data/ConstantsToJson.cs
EnergyManagement.Server/Data/ErrorCodesToJson.cs
EnergyManagement.Server/Program.cs
```

Tests:

```text
Tests.EnergyManagement/Integration/AuthTests.cs
Tests.EnergyManagement/Integration/ClientRequestsTests.cs
Tests.EnergyManagement/TestHelpers/TestData.cs
Tests.EnergyManagement/TestHelpers/IntegrationTestHelper.cs
Tests.EnergyManagement/TestHelpers/HttpResponseAssertions.cs
```

Frontend later:

```text
energymanagement.client/src/globConstants.ts
energymanagement.client/src/Utils/FormSchemas.ts
energymanagement.client/src/Utils/ServerValidationErrUtils.ts
energymanagement.client/src/Utils/handleErrorResponse.ts
```

### Важный порядок

1. Создать новые классы routes/problem details/field names/json field.
2. Обновить server controllers и validators.
3. Обновить .NET tests.
4. Удалить `SharedConst`.
5. Отдельно решить, что делать с `Shared/constants.json` и frontend `globConstants.ts`.
6. После этого проектировать OpenAPI/orval и CLI generator.

## 11. Протокол работы с planning

### Главный принцип

`planning` должен быть handoff-документацией для AI agent без контекста чата.

Новый агент должен прочитать:

```text
planning/solution-map-and-cleanup-plan.md
planning/general project info.md
planning/classes.md
planning/usecases.md
```

и понять:

- предметную область;
- текущую структуру solution;
- принятые решения;
- что уже сделано;
- какие действия опасны;
- какой следующий шаг.

### Как вести planning

1. После каждого архитектурного решения обновлять `planning/solution-map-and-cleanup-plan.md`.
2. После каждой чистки/переноса/удаления добавлять запись в "Журнал решений и выполненных действий".
3. Если действие может сломать build/tests/frontend, фиксировать это в "Что может вызвать проблемы".
4. Не хранить много старых версий planning-файлов без необходимости.
5. Если нужно сохранить старую идею, не создавать копии файла, а добавить короткий раздел "История решений" или "Отмененные решения".
6. Не тратить время и токены на перетаскивание старых planning-файлов между папками, пока нет явной пользы.
7. Текущий файл считать живым планом и handoff-документом.

### Нужно ли хранить старые версии планов

Решение: пока не заводить папку старых версий.

Причина:

- старые версии будут мешать новому агенту;
- агенту придется читать больше текста;
- можно случайно выполнить устаревший план;
- git уже хранит историю изменений.

Если понадобится сохранить важное старое решение, лучше добавить краткую запись:

```text
Отмененное решение: ...
Почему отменено: ...
```

в текущий planning-файл.

## 12. Handoff prompt для нового AI agent

Этот prompt можно дать новому агенту в начале работы.

```text
Ты работаешь в репозитории C:\enman\enman над дипломным проектом EnergyManagement.

Сначала прочитай planning:
- planning/solution-map-and-cleanup-plan.md
- planning/general project info.md
- planning/classes.md
- planning/usecases.md

Считай planning главным источником текущих решений. Если находишь расхождение между кодом и planning, сначала явно отметь это и предложи безопасный шаг.

Текущая цель проекта: привести старый хаотичный ASP.NET Core + React проект к L1 дипломной версии приложения для обработки клиентских заявок в сетевой компании ООО "ЗСК".

Важные правила:
- все существенные решения, выполненные действия, риски и следующий план фиксируй в planning/solution-map-and-cleanup-plan.md;
- не создавай старые копии planning без необходимости, текущий planning-файл является живым handoff-документом;
- не удаляй пользовательские изменения;
- перед удалением файлов проверь, что они не используются;
- E2E tests остаются в текущей корневой папке tests;
- .NET tests остаются в одном проекте Tests.EnergyManagement;
- generated artifacts вроде _site, api, playwright-report, test-results не считать исходниками;
- SharedConst не развивать и не делать фасадом совместимости, его нужно разобрать на логические классы и заменить прямыми ссылками;
- JsonField.Of нужен только для server-side FluentValidation field names: сервер возвращает клиенту имя проблемного JSON-поля;
- DTO/API для frontend в будущем генерировать через OpenAPI/orval;
- error codes/enums/validation metadata, не являющиеся DTO, генерировать отдельным CLI/tool.

Перед началом работы выполни короткую инвентаризацию текущего git status и назови, какие изменения уже есть, чтобы не перетереть чужую работу.

После выполнения задачи:
- обнови planning;
- кратко перечисли измененные файлы;
- укажи, какие проверки запускались и что не удалось проверить.
```
## 13. ������ ������� � ����������� ��������

### 2026-05-08: ������ `SharedConst`

���������:

- `SharedConst` ������ ��� ���������� compatibility facade.
- HTTP routes �������� � `EnergyManagement.Server/Api/Routes/AuthRoutes.cs` � `EnergyManagement.Server/Api/Routes/ClientRequestRoutes.cs`.
- ProblemDetails validation/exception extension names �������� � `EnergyManagement.Server/Api/Contracts/Common/ProblemDetailsContract.cs`.
- `JsonField` ��������� �� global namespace � `EnergyManagement.Server/Api/Contracts/Common/JsonField.cs`.
- Server-side FluentValidation field names �������� � `EnergyManagement.Server/Api/Contracts/Auth/AuthFieldNames.cs` � `EnergyManagement.Server/Api/Contracts/Requests/RequestFieldNames.cs`.
- Server controllers, validators, .NET integration tests � test helpers ���������� � `SharedConst` �� ����� ������ ������.

�������:

- `JsonField.Of` �������� ������ helper ��� server-side validation contract: ������ ���������� ������� JSON-��� ����������� ����.
- ����� ������ �� �������� DTO � �� ������ ����������� frontend source of truth. ��� frontend �����: DTO/API ����� OpenAPI/orval; error codes/enums/validation metadata ����� ��������� CLI/tool.

��������:

- `rg "SharedConst|SharedConstants" -n EnergyManagement.Server Tests.EnergyManagement` �� ����� ���������� ������.
- `dotnet build Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-restore` �������� �������.
- `dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj --no-restore` ������������� ������ � �������� �����, �� integration tests ������ �� ����������� � SQL Server: `Named Pipes Provider, error: 40 - Could not open a connection to SQL Server`. ��� ������������� ������, �� �������������� ��� ��������� ������� ������.

�����:

- `EnergyManagement.Server/Data/ConstantsToJson.cs` �������� ������������������ ������ ����������� � ������ ��������� � ������������ �� ����� ������. ���������� ��������� ���: ������� ��� �������� ���� ���� ��������� CLI/tool ����� ������� ��������� ��������� non-DTO metadata.
- ������ integration-test ������ ������� ���������� SQL Server/test database.

��������� ���������� ���:

- ��������� ���������� ����� `EnergyManagement.Server/Data`: �������� DTO/API contracts, validation, persistence options � ������ ����������, �� ����� �������� ������ �� ���� ������� ���.
