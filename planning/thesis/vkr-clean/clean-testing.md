# Clean Testing

Status: draft / repo-inspection sync  
Scope: testing strategy and verification material for VKR chapter 3

## 1. Testing Goal

Testing confirms that the web application correctly handles business rules, API behavior, persistence, client-visible behavior and cross-layer user flows.

The project separates test responsibility by layer:

```text
Domain unit tests
Server integration/API tests
Client API/model/component tests
E2E Playwright tests
Contract artifact checks
```

## 2. Domain Unit Tests

Domain unit tests verify:

```text
- domain invariants;
- state transitions;
- invalid command no-write behavior;
- account and applicant entity rules;
- request creation rules;
- request review start/approve/reject behavior;
- agreement proposal exchange state transitions;
- agreement proposal versioning and active proposal replacement;
- permission/invariant errors for invalid employee/client actions.
```

Repo-observed examples include tests for accounts, individual applicants, connection request creation/review, employees and agreement proposals.

## 3. Server Integration / API Tests

Server integration/API tests verify:

```text
- application handlers;
- endpoint behavior;
- validation;
- ProblemDetails / ServerError responses;
- persistence effects;
- session/auth behavior;
- role-based access for client and employee APIs;
- API behavior visible to frontend.
```

Repo-observed L1 scenarios include:

```text
- register client account;
- login client account;
- current user;
- logout;
- create individual applicant party;
- list/select applicant parties;
- create connection request;
- list own requests;
- get own request details;
- employee request dashboard;
- employee request details;
- employee start/approve/reject review;
- agreement exchange list/details;
- start agreement exchange;
- send proposal versions;
- accept active agreement proposal;
- final refusal;
- antiforgery behavior.
```

## 4. Client API / Model / Component Tests

Client tests verify detailed UI and frontend behavior:

```text
- labels and accessible controls;
- form state;
- deferred validation timing;
- field errors;
- disabled/enabled states;
- DTO mapping;
- ProblemDetails parsing;
- error-code to message mapping;
- list empty states;
- filters and URL query serialization;
- request details rendering;
- employee dashboard/details rendering;
- agreement exchange list/details widgets;
- review/agreement action availability.
```

Repo-observed client test areas include auth, applicant parties, request creation, my requests, employee requests and agreement exchange components.

## 5. E2E Tests

E2E tests verify critical cross-layer flows:

```text
browser
-> React client
-> HTTP API
-> server/application/domain/persistence/session
-> response
-> visible result
```

Repo-observed Playwright E2E areas:

```text
- registration;
- login;
- applicant party creation;
- applicant party read/select current default;
- connection request creation;
- My Requests list;
- My Request details;
- My Requests filters.
```

Current Playwright infrastructure also makes it possible to create repeatable screenshots for the practice report, PZ and presentation.

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
| Domain unit | Domain invariants and transitions | request approve/reject, agreement proposal exchange transitions, no-write on invalid action | domain test files |
| API integration | Endpoints, validation, persistence and authorization | auth, applicant, request, employee review, agreement exchange | integration test files |
| Contract artifacts | OpenAPI/constants freshness | generate/check API and constants | Tools/tests/scripts |
| Client/component | UI form behavior and state rendering | validation, error mapping, filters, widgets | client test files |
| E2E | Browser-to-server wiring | registration, login, applicant/request happy paths | Playwright tests |
| Screenshot scripts | Repeatable report/presentation visuals | stable UI screenshots for PZ and slides | Playwright screenshot plan |

## 8. Diploma-Safe Formulation

```text
Тестирование строится по слоям. Доменный уровень проверяет бизнес-правила и переходы состояний, серверные интеграционные тесты проверяют API, валидацию, авторизацию и сохранение данных, клиентские тесты проверяют поведение форм и отображение состояний интерфейса, а E2E-тесты подтверждают прохождение критичных сценариев через браузер, frontend, HTTP API и серверную часть. Дополнительно используются проверки контрактных артефактов OpenAPI и generated constants, чтобы уменьшить риск рассинхронизации между backend и frontend.
```
