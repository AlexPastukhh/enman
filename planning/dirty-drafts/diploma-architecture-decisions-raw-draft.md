Ниже грязный черновик по нашим обсуждениям и решениям. Его можно потом растащить в диплом/пояснительную записку/архитектурные заметки.

---

# Черновик архитектурных решений и пояснений

## 1. Общий переход от legacy runtime к L1/L2 runtime

В проекте был старый runtime-контур:

```text
/api/auth/*
/api/ClientRequest/*
AppDbContext
legacy DTO / commands / queries / repositories
legacy integration tests/helpers
```

Он постепенно был вытеснен новым L1/L2 runtime-контуром:

```text
/api/l1/auth/*
/api/l1/applicant-parties*
/api/l1/requests*
/api/employee/requests*
/api/employee/requests/{requestId}/review/start
/api/antiforgery/token
```

Решение:

```text
Старый exposed backend runtime должен быть удалён.
OpenAPI не должен содержать legacy endpoints.
Legacy tests/helpers удаляются, если они проверяют удалённый runtime.
```

При этом важно было не удалить случайно текущие общие вещи:

```text
ProblemDetailsContract
CSRF/security infrastructure
L1DbContext
current L1 tests
current generated OpenAPI/types flow
```

---

## 2. Почему старые DTO ещё мешали после удаления runtime

После удаления старых контроллеров часть старых DTO ещё могла оставаться не потому, что они нужны runtime’у, а потому что старый generator constants всё ещё ссылался на них.

Примеры legacy DTO:

```text
RegisterClientDto
LoginDto
ProvideIndividualClientsDataDto
CreateIndividualRequestDto
old FullNameDto
old AddressDto
```

Проблема:

```text
Эти DTO больше не должны определять runtime contract,
но могут держаться tools/constants generation.
```

Решение:

```text
Нужно развязать tools/constants generation от legacy DTO/routes.
ServerExceptionDto и ServerValidationError, если всё ещё нужны для ProblemDetails,
нужно вынести из старой Data-папки в актуальный Api/Contracts/Common.
```

---

## 3. Client API ownership architecture

Раньше часть business endpoint wrappers лежала в:

```text
shared/api
```

Например:

```text
shared/api/l1ApplicantPartyApi.ts
shared/api/employeeRequestApi.ts
shared/api/l1RequestApi.ts
```

Обсудили, что у такой схемы есть плюсы: централизованный API contract adapter, все endpoint wrappers в одном месте, рядом с `fetchJson`, CSRF и generated OpenAPI types.

Но итоговое решение было сделать архитектуру чище:

```text
shared/api
  только generic API infrastructure

entities/*/api
  entity-specific server calls / read wrappers / DTO aliases

features/*/api
  user-action / command wrappers
```

То есть:

```text
GET /api/l1/applicant-parties
  -> entities/applicant-party/api

POST /api/l1/applicant-parties/individual
  -> features/applicant-party/create-individual/api

GET /api/employee/requests
  -> entities/employee-request/api

POST /api/employee/requests/{id}/review/start
  -> features/employee-request/start-review/api
```

`shared/api` теперь должен содержать только:

```text
fetchJson
ProblemDetails / ApiError
antiforgeryTokenStore
generated/openapi-types
generic helpers
```

---

## 4. Generated OpenAPI DTO aliases

Решение по client DTO types:

```text
Server response DTO shape не нужно руками копировать в client.
Нужно alias'ить generated OpenAPI schemas.
```

Пример:

```ts
import type { components } from "../../../shared/api/generated/openapi-types";

export type EmployeeRequestListResponseDto =
  components["schemas"]["EmployeeRequestListResponseDto"];
```

Локальные client unions можно оставить, если это именно client-controlled filters:

```ts
export type EmployeeRequestStatus = "...";
export type EmployeeDashboardReviewState = "...";
```

Смысл:

```text
generated types защищают от contract drift.
локальные типы остаются только для client-side состояния/фильтров.
```

---

## 5. CSRF / antiforgery

Для cookie-authenticated browser API нужен antiforgery protection.

Решение:

```text
GET /api/antiforgery/token
  возвращает requestToken

unsafe requests:
  POST/PUT/PATCH/DELETE
  должны отправлять X-CSRF-TOKEN
```

Клиент не использует readable XSRF cookie. Token хранится runtime-only.

CSRF failure должен быть отличим от обычной validation error:

```json
{
  "status": 400,
  "code": "security.antiforgery.validation.failed"
}
```

Важно:

```text
Не маппить generic 400 в CSRF.
Не использовать FluentValidation / ServerValidationError для CSRF.
Не делать auto-replay unsafe request после refresh token.
```

Client behavior:

```text
missing/expired CSRF token
  -> fetchJson понимает ProblemDetails.code
  -> refresh token
  -> throws typed AntiforgeryApiError
  -> user/action layer может повторить действие явно
```

---

## 6. Почему отключали MVC ClientError mapping

Появилась проблема: ASP.NET Core `ClientErrorResultFilter` мог раньше преобразовать antiforgery result в generic 400.

Решение:

```text
SuppressMapClientErrors = true
```

Потому что проект сам нормализует ProblemDetails и не хочет, чтобы MVC silently превращал results в generic client error.

Итог:

```text
CSRF result filter может видеть antiforgery-specific marker/result.
Generic 400 не используется как CSRF marker.
```

---

## 7. Server validation

Договорились различать:

```text
CSRF/security failure
  -> 400 ProblemDetails + code security.antiforgery.validation.failed

DTO / query validation
  -> 422 validation ProblemDetails

domain lifecycle/business validation
  -> 422 ProblemDetails / domain error mapping
```

CSRF не должен идти через FluentValidation.

FluentValidation остаётся для DTO/query формы:

```text
invalid status filter
invalid reviewState filter
invalid request body
```

Domain errors остаются для lifecycle:

```text
already started review
request not InReview
approved/rejected cannot start review
```

---

## 8. Employee dashboard read side

Employee dashboard read endpoints:

```text
GET /api/employee/requests
GET /api/employee/requests/{requestId}
```

First-pass visibility decision:

```text
All active Employees can see all review-relevant requests.
No department/region/assignment visibility in this slice.
```

Review state в read model:

```text
NotStarted
StartedByCurrentEmployee
StartedByAnotherEmployee
Approved
Rejected
```

Это read-side derived state. Он нужен UI для отображения, но не должен становиться сложной visibility model на первом этапе.

---

## 9. Start Request Review

Slice:

```text
SL-EMP-REQ-003 — Start Request Review
```

Endpoint:

```http
POST /api/employee/requests/{requestId}/review/start
```

Response:

```text
204 No Content
```

Решение отказаться от response DTO:

```text
Command меняет состояние.
Client уже знает requestId.
Новые reviewStartedAt / reviewState должны прийти из read endpoints после refetch.
```

Flow:

```text
Employee видит request NotStarted
  -> нажимает Start review
  -> POST command
  -> server вызывает domain StartReview(employee)
  -> persists RequestReview
  -> 204
  -> client invalidates/refetches list/details
  -> UI видит StartedByCurrentEmployee
```

StartReview не idempotent:

```text
already started -> 422
approved/rejected/not InReview -> 422
```

---

## 10. Employee identity problem: accountId vs employeeId

Был важный спор.

Если Employee моделируется как отдельный profile:

```text
Employee.Id
Employee.AccountId
```

тогда claim:

```text
NameIdentifier = Account.Id
```

нельзя напрямую трактовать как `Employee.Id`.

Это ломает:

```text
StartReview
StartedByCurrentEmployee
EmployeeRepository.GetById
```

Но было принято другое intended design:

```text
Employee должен наследоваться от Account через TPH.
```

То есть:

```text
Account
  ├─ ClientAccount
  └─ Employee
```

И тогда:

```text
ClaimTypes.NameIdentifier = Account.Id = Employee.Id
```

Решение:

```text
Не добавлять GetByAccountIdAsync.
Не добавлять Employee.AccountId.
Не создавать отдельную L1Employees identity table.
Сделать Employee : Account через TPH.
```

---

## 11. Account / Employee TPH model

Target domain model:

```text
Account:
  Id
  Email
  PasswordHash
  Role
  IsActive
  CreatedAt

ClientAccount : Account

Employee : Account:
  WindowsLogin
  FullName
```

Persistence:

```text
L1Accounts
  Id
  Email
  PasswordHash
  Role
  IsActive
  CreatedAt
  AccountType = Client | Employee
  WindowsLogin
  EmployeeFullName_*
```

No separate:

```text
L1Employees
Employee.AccountId
GetByAccountIdAsync
```

Reason:

```text
Employee is not a profile linked to Account.
Employee is an account subtype.
```

---

## 12. Employee authentication model

Employee должен иметь два способа входа:

```text
1. Email/password
2. Windows Integrated Authentication
```

Оба способа должны приводить к одному runtime result:

```text
application cookie
Role = Employee
NameIdentifier = Employee.Id
AuthModel = L1
```

Client:

```text
email/password
  -> Role = Client
```

Employee password login:

```text
email/password
  -> authenticate Account by Email + PasswordHash
  -> if AccountType/Role = Employee
  -> Role = Employee
```

Employee Windows sign-in:

```text
EmployeeWindows external auth
  -> User.Identity.Name = DOMAIN\username
  -> find Employee by WindowsLogin
  -> Role = Employee
```

---

## 13. Windows Auth как provider, не runtime session

Windows Auth не должен быть runtime authorization для каждого employee endpoint.

Решение:

```text
Windows Auth используется только для employee sign-in bootstrap.
После sign-in выдаётся обычная app cookie.
Дальше employee endpoints используют [Authorize(Roles = "Employee")].
```

Почему так лучше:

```text
SPA проще.
CSRF работает одинаково.
Employee endpoints не зависят от Kerberos/NTLM.
Тесты можно писать через fake auth handler.
Windows/Auth инфраструктуру не нужно поднимать для дипломного проекта.
```

---

## 14. Real Windows Auth vs test fake auth

Для диплома не нужно поднимать:

```text
Active Directory
Kerberos
NTLM
domain-joined machine
SPN
browser integrated auth settings
```

Тестируем не Windows infrastructure, а наше application behavior:

```text
external identity name
  -> Employee.WindowsLogin
  -> Employee account
  -> app cookie
  -> employee endpoints доступна
```

Production/intranet:

```text
EmployeeWindows = Negotiate / Windows Auth
```

Tests:

```text
EmployeeWindows = fake authentication handler
```

Это нормальный подход:

```text
External auth provider заменяется test scheme.
Бизнес-поведение проверяется стабильно.
```

---

## 15. Logout behavior

Logout очищает только application cookie.

Он не делает:

```text
logout from Windows
logout from domain
forget OS credentials
stop browser from silently using Windows Auth again
```

Решение:

```text
POST /api/l1/auth/logout
  или future /api/session/logout
  очищает app cookie

Если employee после logout снова открывает windows-signin,
браузер может снова автоматически пройти Windows Auth.
Это ожидаемо.
```

---

## 16. Inactive Employee behavior

Два случая.

### Inactive before sign-in

```text
Employee.IsActive = false
  -> password login запрещён
  -> Windows sign-in запрещён
  -> no app cookie issued
```

### Employee стал inactive после выдачи cookie

```text
old cookie still has Role=Employee
  -> employee command loads Employee domain actor
  -> employee.EnsureCanReview()
  -> fails
```

Решение:

```text
Inactive Employee cannot sign in by either method.
Stale session cannot execute employee command.
```

Read endpoints first-pass могут опираться на роль, но commands должны проверять актуального Employee actor.

---

## 17. ClaimsPrincipalFactory

ClaimsPrincipalFactory нужен как единая точка создания claims для app cookie.

Проблема:

```text
Client password login
Employee password login
Employee Windows sign-in
```

Все создают app cookie. Без factory claims легко разъедутся:

```text
один controller положит NameIdentifier = accountId
другой — employeeId
один забудет AuthModel
один напишет role "employee", другой "Employee"
```

Factory фиксирует единые правила:

```text
NameIdentifier = Account.Id
Role = Account.Role
AuthModel = L1
Email = Account.Email
auth_source = password/windows optional
windows_name = only for Windows sign-in optional
```

Она не проверяет пароль, не делает Windows Auth и не создаёт cookie сама. Она только собирает `ClaimsPrincipal`, который потом передаётся в `SignInAsync`.

---

## 18. Current-user/session endpoint

Решение:

```text
Не важно, как называется endpoint сейчас.
Можно оставить /api/l1/auth/current-user.
```

Главное, чтобы он мог представить оба типа сессии:

```text
Client session
Employee session
```

Future cleanup:

```text
/api/session/current
```

но это не часть auth slice.

---

## 19. PasswordHash placement

Так как Employee тоже может входить по email/password, `PasswordHash` остаётся на уровне `Account`.

Решение:

```text
Account owns Email / PasswordHash,
because ClientAccount and Employee can both use application password login.
```

WindowsLogin — это дополнительный binding только для Employee:

```text
Employee.WindowsLogin = DOMAIN\username
```

---

## 20. Employee Windows sign-in endpoint

Proposed endpoint:

```http
GET /api/employee/auth/windows-signin
```

Почему GET:

```text
Windows/Negotiate often challenge-oriented.
Endpoint не выполняет business command.
Он только устанавливает app session после external auth.
```

Но важно:

```text
Он issue app cookie.
Он не должен мутировать бизнес-состояние.
Он не должен обходить CSRF для business commands.
```

---

## 21. Client behavior after login/sign-in

После любого session context change нужно обновить CSRF token.

Flows:

```text
password login success
  -> refetch current session
  -> refresh antiforgery token

employee windows sign-in success
  -> refetch current session
  -> refresh antiforgery token

logout
  -> clear/refetch session
  -> clear/refetch antiforgery token
```

---

## 22. Employee auth client architecture

Не возвращаем business wrappers в `shared/api`.

Target:

```text
features/auth/login/api
  password login

features/auth/employee-windows-signin/api
  signInEmployeeWithWindows

entities/session/api
  getCurrentSession

shared/api
  fetchJson / ProblemDetails / CSRF / generated types
```

---

## 23. Testing strategy for Employee auth

Server integration tests:

```text
Client account can login by email/password -> Role=Client
Employee account can login by email/password -> Role=Employee
Employee password login can call GET /api/employee/requests -> 200
Client password login cannot call employee endpoint -> 403

Windows identity not mapped -> 403
Windows identity mapped to inactive employee -> 403
Windows identity mapped to active employee -> 200 + Set-Cookie

After Windows sign-in:
  employee endpoint -> 200
  StartReview + CSRF -> 204
  StartedByEmployeeId = Employee.Id

Logout clears employee app cookie
After logout employee endpoint -> 401
```

No real Windows/AD dependency.

---

## 24. OpenAPI / generated artifacts policy

После каждого изменения exposed API нужно:

```text
regenerate Shared/openapi.json
regenerate generated openapi-types.ts
run check:api
```

Generated artifacts нельзя править руками.

Если `check:api` падает из-за intended generated diff до commit/stage — это не логическая ошибка, но нужно честно отметить.

---

## 25. Migrations / database

Была проблема:

```text
There is already an object named 'L1Accounts'
```

Причина:

```text
БД уже содержит таблицы, но EF migration history не соответствует текущему состоянию.
```

Для test/local DB можно было сделать:

```powershell
dotnet ef database drop --context L1DbContext --force
dotnet ef database update --context L1DbContext
```

Предупреждения EF про optional dependent owned types:

```text
ProposalComment
FinalRefusalReason
RejectionFeedback
```

Это не blocker, но означает, что owned optional objects with all-null columns могут не materialize’иться при query.

---

## 26. Test database

Тестовая БД используется integration tests. Если миграции поменялись, локальную test DB нужно привести к актуальному состоянию.

Для чистой локальной проверки допустимо:

```text
drop database
apply migrations
run tests
```

Но это не production migration strategy.

---

## 27. ProblemDetails philosophy

Проект старается не полагаться на framework default errors.

Решение:

```text
Ошибки должны быть явно нормализованы в ProblemDetails.
CSRF имеет свой code.
Auth failures могут иметь свои code.
Validation/domain failures не смешиваются с security failures.
```

---

## 28. Почему StartReview response = 204

Для command endpoints:

```text
POST start-review
```

не нужно возвращать DTO, потому command не является read source.

Правильный UX:

```text
command success -> invalidate/refetch read queries
```

Это сохраняет разделение:

```text
command endpoint changes state
read endpoints return current state
```

---

## 29. Что остаётся future/deferred

После cleanup и auth slice остаются отдельные будущие темы:

```text
Approve Request Review
Reject Request Review
Agreement proposal exchange
Employee permissions/department/assignment visibility
neutral /api/session/current naming
auth API ownership split on client
rename/move shared VOs out of old namespace if needed
real deployment config for Windows Auth
```

---

## 30. Формулировка для диплома про auth

Черновик:

```text
В системе предусмотрены разные способы аутентификации для внешних заявителей и внутренних сотрудников.

Внешние заявители используют аутентификацию по email и паролю.

Сотрудники могут использовать обычный вход по email и паролю, а также вход через Windows Integrated Authentication в корпоративном контуре. Windows-аутентификация используется как внешний поставщик идентичности: приложение получает доменное имя пользователя, сопоставляет его с Employee-аккаунтом и создаёт обычную cookie-сессию приложения.

После успешной аутентификации все пользователи работают через единую application cookie. Дальнейшая авторизация выполняется по ролям приложения, например Client или Employee. Это позволяет бизнес-endpoints не зависеть от конкретного способа входа.

В автоматизированных тестах реальная доменная инфраструктура не разворачивается. Вместо неё используется тестовая authentication scheme, эмулирующая результат Windows-аутентификации. Это позволяет проверять бизнес-поведение без зависимости от Kerberos, NTLM и Active Directory.
```

---

## 31. Формулировка для диплома про CSRF

Черновик:

```text
Так как приложение использует cookie-based authentication, unsafe browser requests защищаются antiforgery token mechanism.

Клиент получает request token через специальный endpoint и передаёт его в заголовке X-CSRF-TOKEN для unsafe методов. Сервер проверяет token до выполнения бизнес-логики.

Ошибки antiforgery validation нормализуются в ProblemDetails с отдельным кодом security.antiforgery.validation.failed. Это позволяет клиенту отличать security/session failure от обычных ошибок DTO validation или доменной валидации.

Клиент может обновить token после такой ошибки, но не должен автоматически повторять unsafe command без явного действия пользователя.
```

---

## 32. Формулировка для диплома про L1/L2 domain evolution

Черновик:

```text
В процессе разработки старая модель была отделена от нового L1/L2 контура. Legacy runtime endpoints и tests были удалены после появления новых L1 endpoints и employee request endpoints.

Новая модель разделяет account/auth identity, applicant party data, connection request lifecycle и employee review behavior. Employee является subtype Account, что позволяет использовать единый account identity и role-based authorization.

Command endpoints изменяют состояние агрегатов, а read endpoints предоставляют актуальное представление для клиента. Поэтому, например, StartReview возвращает 204 No Content, а UI обновляет состояние через повторное чтение list/details endpoints.
```

---

## 33. Короткий список ключевых решений

```text
1. Удалить exposed legacy runtime.
2. Не держать business endpoint wrappers в shared/api.
3. shared/api = generic infrastructure only.
4. Entity reads -> entities/*/api.
5. Commands/actions -> features/*/api.
6. Server DTO types на client — через generated OpenAPI aliases.
7. CSRF через /api/antiforgery/token + X-CSRF-TOKEN.
8. CSRF failure = 400 ProblemDetails + code.
9. No unsafe auto-replay after CSRF refresh.
10. Employee dashboard first-pass visibility = all active Employees see review-relevant requests.
11. StartReview = POST + CSRF + Role Employee + 204 No Content.
12. StartReview не idempotent.
13. Employee : Account через TPH.
14. Account.Id == Employee.Id.
15. No Employee.AccountId.
16. No L1Employees identity table.
17. Employee может войти по email/password.
18. Employee может войти через Windows Auth.
19. Windows Auth = sign-in provider, не runtime scheme.
20. Runtime session = application cookie.
21. Logout очищает только app cookie.
22. Inactive Employee не может sign in и не может выполнять commands через stale session.
23. ClaimsPrincipalFactory нужен для единых claims.
24. Tests используют fake EmployeeWindows scheme.
25. Real AD/Kerberos/NTLM не нужны для automated tests.
```

---

## 34. TestServer и Windows Negotiate Auth

После добавления Windows Auth возникла массовая ошибка integration tests:

```text
System.NotSupportedException:
Negotiate authentication requires a server that supports IConnectionItemsFeature like Kestrel.
```

Симптом:

```text
GET /api/antiforgery/token -> 500
GET protected endpoint without auth -> 500 instead of 401
many unrelated tests fail before reaching controllers
```

Причина была не в БД, не в миграциях и не в конкретных business endpoints.

Проблема:

```text
Production app registers real Negotiate handler.

Program.cs:
  AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(...)
    .AddNegotiate()

Integration tests run through ASP.NET Core TestServer.

Real NegotiateHandler cannot run inside TestServer,
because TestServer does not provide IConnectionItemsFeature.
```

Важно:

```text
Negotiate can affect requests even when endpoint is not Windows-specific.

Request pipeline:
  UseAuthentication()
    -> real NegotiateHandler tries to process request
    -> TestServer unsupported feature
    -> 500 before controller/authorization
```

Поэтому падали даже endpoints, которые не связаны с Windows Auth:

```text
/api/antiforgery/token
/api/l1/auth/current-user
/api/l1/auth/register
/api/l1/applicant-parties
```

## 35. Почему старый fake auth был недостаточен

До Windows Auth integration tests работали двумя способами.

Обычные tests:

```text
Base WebAppFactory
  -> real cookie authentication
  -> no cookie means unauthenticated
  -> [Authorize] returns 401
```

Tests with forced authenticated user:

```text
AuthenticatedInstanceWithClaims(...)
  -> replace IAuthenticationSchemeProvider
  -> TestAuthenticationSchemeProvider
  -> MockAuthenticationHandler
  -> returns claims from MockClaimSeed
```

То есть fake auth уже был.

Но он применялся только к tests, которые явно вызывали:

```text
AuthenticatedInstanceWithClaims(...)
```

Обычные tests продолжали использовать real authentication scheme provider. После `AddNegotiate()` это значило:

```text
ordinary integration test
  -> real scheme provider
  -> real Negotiate handler available
  -> TestServer tries to run Negotiate
  -> 500
```

То есть проблема была не в отсутствии fake handler вообще, а в том, что **Windows/Negotiate scheme должна быть подменена в test host для всех integration tests**, а не только в специальных authenticated tests.

## 36. Test auth providers после фикса

В `WebAppFactory` есть два разных test scheme provider’а.

### Full fake provider

```csharp
public class TestAuthenticationSchemeProvider : AuthenticationSchemeProvider
{
    public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
    {
        var schemeName = string.IsNullOrWhiteSpace(name)
            ? CookieAuthenticationDefaults.AuthenticationScheme
            : name;

        AuthenticationScheme mockScheme = new(
            schemeName,
            schemeName,
            typeof(MockAuthenticationHandler));

        return Task.FromResult<AuthenticationScheme?>(mockScheme);
    }
}
```

Назначение:

```text
AuthenticatedInstanceWithClaims(...)
```

Он мокает любую scheme, включая cookie. Поэтому он подходит только для тестов, где мы специально хотим считать пользователя authenticated.

Его нельзя включать глобально, иначе обычные tests вроде:

```text
WithoutAuth_ReturnsUnauthorized
```

перестанут быть честными.

### Employee Windows-only fake provider

```csharp
public class TestEmployeeWindowsAuthenticationSchemeProvider
    : AuthenticationSchemeProvider
{
    public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
    {
        if (name == EmployeeAuthSchemes.EmployeeWindows)
        {
            AuthenticationScheme mockScheme = new(
                EmployeeAuthSchemes.EmployeeWindows,
                EmployeeAuthSchemes.EmployeeWindows,
                typeof(MockAuthenticationHandler));

            return Task.FromResult<AuthenticationScheme?>(mockScheme);
        }

        return base.GetSchemeAsync(name);
    }
}
```

Назначение:

```text
Mock only EmployeeWindows / Negotiate scheme.

Leave cookie auth real.
```

После фикса именно этот provider используется в base `ConfigureTestServices` для всех integration tests.

Результат:

```text
Cookie auth remains real:
  no cookie -> 401
  login cookie -> authenticated
  wrong role -> 403

EmployeeWindows / Negotiate becomes fake:
  real NegotiateHandler does not run inside TestServer
  Windows-auth tests can provide fake claims
```

## 37. MockAuthenticationHandler и MockClaimSeed

Fake handler:

```csharp
public class MockAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly MockClaimSeed _seed;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = _seed.GetSeeds();

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
```

`MockClaimSeed` просто хранит claims:

```csharp
public class MockClaimSeed
{
    private readonly IEnumerable<Claim> _seed;

    public IEnumerable<Claim> GetSeeds() => _seed;
}
```

Для обычной base factory регистрируется пустой `MockClaimSeed`, чтобы fake Windows handler мог быть сконструирован, если схема будет запрошена.

Это не делает обычные tests authenticated, потому cookie scheme не подменяется.

Для Windows-specific tests используется:

```text
EmployeeWindowsIdentity(claims)
```

и тогда `MockClaimSeed` содержит Windows claims, например:

```text
Name = TESTDOMAIN\employee
```

## 38. Важное уточнение про override

Новый override `GetRequestHandlerSchemesAsync()` в итоге не понадобился.

Изначальная гипотеза была:

```text
Negotiate может попадать в request-handler pipeline через GetRequestHandlerSchemesAsync,
значит надо фильтровать его там.
```

Но по фактическому коду и результату `193 passed` видно:

```text
достаточно было globally replace IAuthenticationSchemeProvider in test host
на TestEmployeeWindowsAuthenticationSchemeProvider,
который override’ит GetSchemeAsync для EmployeeWindows.
```

То есть итоговое решение:

```text
Production:
  real AddNegotiate remains.

Tests:
  base WebAppFactory uses TestEmployeeWindowsAuthenticationSchemeProvider.
  EmployeeWindows/Negotiate resolves to MockAuthenticationHandler.
  Cookie remains real.
```

## 39. Почему это архитектурно корректно

Windows Auth — внешний provider.

В production:

```text
EmployeeWindows = real Negotiate / Windows Integrated Authentication
```

В tests:

```text
EmployeeWindows = fake authentication handler
```

Business/application behavior, которое тестируется:

```text
external identity name
  -> map to Employee.WindowsLogin
  -> issue application cookie
  -> employee endpoints use Role=Employee
```

Не тестируется:

```text
Kerberos
NTLM
Active Directory
browser integrated auth configuration
Kestrel/IIS Windows authentication internals
```

Это нормально для integration tests приложения.

## 40. Короткий вывод по проблеме

```text
Mass 500 after drop/update was not database-related.

Real NegotiateHandler was running in ASP.NET TestServer.

TestServer cannot support Negotiate internals.

Fix:
  mock only EmployeeWindows/Negotiate scheme globally in test host,
  keep cookie auth real.

Result:
  normal auth tests return 401/403/200 as before,
  Windows auth tests use fake Windows identity,
  193 tests pass.
```
