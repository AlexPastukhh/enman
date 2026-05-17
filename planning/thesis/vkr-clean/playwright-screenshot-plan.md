# Playwright Screenshot Plan

Status: draft / repo-inspection sync  
Scope: repeatable UI screenshots for prediploma report, explanatory note and defense presentation

## 1. Purpose

This file defines screenshots that can be generated through Playwright instead of manual browser screenshots.

The goal is not to replace E2E tests. The goal is to create stable visual materials for:

```text
- отчет по преддипломной практике;
- пояснительная записка ВКР;
- презентация;
- доклад/demo script.
```

## 2. Why Playwright Screenshots

Playwright screenshots are useful because they provide:

```text
- repeatable viewport size;
- repeatable test data;
- reproducible UI state;
- easier regeneration after UI changes;
- direct connection between demonstrated screens and implemented application;
- lower risk of outdated manual screenshots.
```

## 3. Existing E2E Infrastructure

Repo-observed root scripts:

```text
npm run test:e2e
npm run test:e2e:headed
npm run test:e2e:ui
```

The current Playwright config starts backend and frontend web servers and resets the test database before E2E tests through the root npm script.

Default viewport:

```text
1280x720
```

This viewport is acceptable for report screenshots and presentation screenshots. For final PZ, additional `1365x768` or `1440x900` screenshots may be generated if needed.

## 4. Proposed Screenshot Folder

Recommended output folder:

```text
planning/thesis/assets/screenshots/
```

Alternative if implementation assets should stay closer to tests:

```text
tests/e2e/screenshots/vkr/
```

Prefer the first path for final report/PZ assets and keep test artifacts separate from documentation assets.

## 5. Minimal Prediploma Screenshot Set

| File | Screen | Purpose |
|---|---|---|
| `01-register-client.png` | Registration page | Shows client entry point |
| `02-login-client.png` | Login page | Shows authentication UI |
| `03-account-applicant.png` | Account page / applicant block | Shows applicant data in client account |
| `04-create-request.png` | Create request form | Shows core client request submission |
| `05-my-requests-list.png` | My Requests list | Shows request list/status view |
| `06-my-request-details.png` | My request details | Shows request details and review result area |
| `07-employee-request-dashboard.png` | Employee dashboard | Shows employee queue and filters |
| `08-employee-request-details-review-actions.png` | Employee request details | Shows start/approve/reject/review actions |
| `09-agreement-exchange-list.png` | Agreement exchange list | Shows document exchange navigation |
| `10-agreement-exchange-details.png` | Agreement exchange details | Shows proposal/document exchange details |
| `11-e2e-test-run.png` | Playwright or test result view | Shows verification evidence if needed |

## 6. Screenshot Scenario Data

Use deterministic test data. Suggested data shape:

```text
client email: vkr.client@example.test
client password: TestPassword123!
applicant: Иванов Иван Иванович
applicant email: applicant@example.test
applicant phone: +79990000000
request details: Заявка на подключение объекта к сети ООО «ЗСК»
address: 656000, Алтайский край, Барнаул, Ленина, 1
employee: seeded employee account / Windows sign-in test helper, if available
agreement document title/ref: Проект договора технологического присоединения №1
```

Do not use real personal data in final screenshots.

## 7. Scenario Order

Recommended screenshot scenario:

```text
1. Open registration page and capture empty/clean state.
2. Register or seed client.
3. Open login page and capture state.
4. Log in as client.
5. Create or seed individual applicant.
6. Capture account/applicant state.
7. Open create request page and capture form.
8. Submit request or seed request.
9. Capture My Requests list.
10. Capture My Request details.
11. Sign in/seed employee session.
12. Capture Employee Request Dashboard.
13. Open employee request details.
14. Start review / approve request.
15. Capture review actions or approved state.
16. Start agreement exchange.
17. Capture agreement list/details.
```

If employee authentication is unstable for screenshots, seed state at API/database level and open pages with a prepared session only after confirming this is acceptable for demonstration.

## 8. Screenshot Naming Rules

Use ordered filenames:

```text
01-register-client.png
02-login-client.png
03-account-applicant.png
04-create-request.png
05-my-requests-list.png
06-my-request-details.png
07-employee-request-dashboard.png
08-employee-request-details-review-actions.png
09-agreement-exchange-list.png
10-agreement-exchange-details.png
```

Avoid names like `screenshot1.png`.

## 9. Captions For Report / PZ

Suggested captions:

```text
Рисунок X — Форма регистрации клиента
Рисунок X — Форма входа в web-приложение
Рисунок X — Страница аккаунта клиента с данными заявителя
Рисунок X — Форма создания клиентской заявки
Рисунок X — Список заявок клиента
Рисунок X — Страница просмотра деталей заявки
Рисунок X — Рабочая область сотрудника со списком заявок
Рисунок X — Страница рассмотрения заявки сотрудником
Рисунок X — Список обменов проектами договорных документов
Рисунок X — Детали обмена проектом договорного документа
```

For source line:

```text
Источник: скриншот разработанного web-приложения.
```

## 10. Boundaries

Do not use screenshots to claim features that are not implemented.

Safe current claims:

```text
- client request submission and read flow can be shown if the scenario passes;
- employee request dashboard/details/review can be shown if seed/sign-in path is stable;
- agreement exchange can be shown as project document exchange;
- email notification should not be shown as implemented unless real sender/outbox is added.
```

## 11. TODO Before Implementation

```text
TODO: decide output folder.
TODO: create a dedicated Playwright spec or script for screenshots.
TODO: choose whether screenshots are produced by test setup or separate documentation setup.
TODO: confirm employee auth/demo setup.
TODO: regenerate screenshots after final UI text and styles stabilize.
```
