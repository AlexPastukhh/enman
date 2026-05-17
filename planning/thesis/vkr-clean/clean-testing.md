# Clean Testing

Status: draft  
Scope: testing strategy and verification material for VKR chapter 3

## 1. Testing Goal

Testing confirms that the web application correctly handles business rules, API behavior, persistence, client-visible behavior and cross-layer user flows.

The project separates test responsibility by layer:

```text
Domain unit tests
Server integration/API tests
Client/component tests
E2E tests
Contract artifact checks
```

## 2. Domain Unit Tests

Domain unit tests verify:

```text
- domain invariants;
- state transitions;
- invalid command no-write behavior;
- request creation rules;
- request review decisions such as approve/reject;
- account and applicant entity rules.
```

## 3. Server Integration / API Tests

Server integration/API tests verify:

```text
- application handlers;
- endpoint behavior;
- validation;
- ProblemDetails / ServerError responses;
- persistence effects;
- session/auth behavior;
- API contract behavior visible to frontend.
```

For L1 this includes scenarios such as:

```text
- register client account;
- login client account;
- current user;
- logout;
- create individual applicant party;
- create connection request;
- validation and unauthorized cases.
```

## 4. Client / Component Tests

Client/component tests should verify detailed UI behavior:

```text
- labels and accessible controls;
- form state;
- deferred validation timing;
- field errors;
- disabled/enabled states;
- DTO mapping;
- ProblemDetails parsing;
- error-code to message mapping.
```

## 5. E2E Tests

E2E tests should verify critical cross-layer flows:

```text
browser
-> React client
-> HTTP API
-> server/application/domain/persistence/session
-> response
-> visible result
```

E2E is useful for registration, login, applicant creation and request creation happy paths after corresponding UI is stable.

## 6. Contract Artifact Checks

The project also has contract-related checks:

```text
- OpenAPI artifact generation/check;
- TypeScript API type generation/check;
- generated semantic constants check;
- Tools tests for artifact serialization/writer/checker;
- API tests consuming generated contract artifacts where appropriate.
```

## 7. Testing Table For VKR

| Test group | Purpose | Example checks | Result source |
|---|---|---|---|
| Domain unit | Domain invariants and transitions | approve/reject, no-write on invalid action | domain test files |
| API integration | Endpoints, validation, persistence | register/login/applicant/request | integration test files |
| Contract artifacts | OpenAPI/constants freshness | generate/check API and constants | Tools/tests/scripts |
| Client/component | UI form behavior | validation, error mapping | future/current client tests |
| E2E | Browser-to-server wiring | happy paths | Playwright tests |

## 8. Diploma-Safe Formulation

```text
Тестирование строится по слоям. Доменный уровень проверяет бизнес-правила и переходы состояний, серверные интеграционные тесты проверяют API, валидацию и сохранение данных, клиентские тесты предназначены для проверки поведения форм и отображения ошибок, а E2E-тесты подтверждают прохождение критичных сценариев через браузер, frontend, HTTP API и серверную часть.
```
