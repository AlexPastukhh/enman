# Testing Strategy

## Цель

Сделать тестовую стратегию понятной для диплома и удобной для разработки.

## Levels

| Level | Tooling | What to test |
|---|---|---|
| Domain unit tests | xUnit + FluentAssertions | Доменные модели и бизнес-правила |
| Backend integration tests | xUnit + WebApplicationFactory | API сценарии, EF Core, auth flow |
| Frontend component tests | Vitest + Testing Library | React формы, валидация, компоненты |
| E2E tests | Playwright TypeScript | Полный пользовательский сценарий в браузере |

## L1 unit tests

### Account

- create client account;
- create employee account;
- cannot create account with invalid email;
- cannot create inactive account login state, if implemented.

### ApplicantParty

- create individual applicant party;
- cannot create without full name;
- cannot create without email/phone.

### ClientRequest

- create connection request;
- create metering device request;
- cannot create request without details;
- cannot create request with too long details;
- initial status is `Submitted`.

### RequestReview / transitions

- `TakeForReview` changes status to `InReview`;
- cannot approve request not in `InReview`;
- approve creates `Approved` review;
- reject creates `Rejected` review;
- cannot approve already rejected request.

### ContractDraft

- simple contract draft can be created for approved request;
- contract draft can be marked sent.

### EmailNotification

- email notification can be created;
- can be marked sent;
- can be marked failed in L2.

## L1 integration tests

- `POST /api/auth/register`;
- `POST /api/auth/login`;
- `GET /api/auth/me`;
- `POST /api/applicant-parties/individual`;
- `POST /api/requests`;
- `GET /api/requests/my`;
- `GET /api/employee/requests`;
- `POST /api/employee/requests/{id}/take`;
- `POST /api/employee/requests/{id}/approve`;
- `POST /api/employee/requests/{id}/reject`.

## L1 frontend component tests

- registration form validation;
- login form validation;
- applicant party form;
- request creation form;
- request list;
- employee request card actions.

## L1 Playwright E2E

Use Playwright with TypeScript.

Recommended specs:

```text
tests/layer1-auth.spec.ts
tests/layer1-request-submission.spec.ts
tests/layer1-employee-review.spec.ts
```

### Scenario 1

```text
client registers
→ client logs in
→ client creates individual applicant
→ client submits request
→ client sees request in list
```

### Scenario 2

```text
employee logs in
→ employee opens requests
→ employee takes request
→ employee approves request
→ contract draft/email flow is triggered
```

### Scenario 3

```text
employee rejects request
→ client sees rejected status
```

## Current test notes

- .NET tests remain in one project: `Tests.EnergyManagement`.
- Recommended internal structure:
  - `Unit/`;
  - `Integration/`;
  - `TestHelpers/`;
  - `Fixtures/`.
- E2E tests remain in root `tests/`.
- Component tests remain in frontend project.
- Integration tests use LocalDB by default:
  - `Data Source=(localdb)\MSSQLLocalDB`;
  - database: `TestEnergyManagement`;
- The integration fixture owns the current test connection string.
- Shared baseline users should be seeded once by the collection fixture and exposed as immutable fixture actors, not mutable static test objects.
- Integration tests should not rely on explicit test ordering.
- Test host replaces `ConnectionStrings:ManagementDb` with the test connection string so services use the normal application configuration key against the test database.
- Dapper queries should use `ConnectionStringNames.ManagementDb`; do not hardcode connection string names in handlers.
- Test configuration overrides should use `ConnectionStringNames.ManagementDbConfigurationKey`.
- Do not add a separate DB-name switch for tests.
- Integration tests that mutate user/request state should use unique per-scenario test data and clean it up in the same test when possible.
- API validation integration tests should verify the external contract: HTTP status, error code, field name, and that invalid requests do not create domain data.
- Keep shared expected API validation errors in `TestHelpers/ExpectedValidationErrors.cs`; keep raw valid/invalid input values in `TestHelpers/TestData.cs`.
- For validation rules, cover each public API rule at least once at the integration level when the rule affects the response contract. Domain unit tests can cover lower-level value-object combinations in more detail.
- Frontend validation tests should distinguish client-side invalid-form behavior from server-side validation responses: invalid local form data must not call mutation functions; backend problem-details handling should be covered by focused unit tests.
- Frontend form component helpers should assert visible validation messages, while test data may keep canonical error codes and map them through shared client constants.

## Known test risks

- Integration tests require SQL Server LocalDB/test database.
- Full solution build may depend on frontend `.esproj` support in local environment.
- E2E tests require both backend and frontend to be running.
- Hidden data/order coupling is the main integration-test risk; keep collection baseline seed explicit and avoid static mutable test actors.
