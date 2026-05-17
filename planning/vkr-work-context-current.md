# VKR Work Context Current

Status: internal VKR context / repo-inspection sync  
Scope: current VKR writing context, repo-grounded baseline and next documentation steps  
Last inspected source: local archive `enman-my-changes (35).zip`

This file is not final diploma text. It is a working entry point for future VKR-related chats and replacement packages.

## 1. VKR Topic

«Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО „ЗСК“».

ООО «ЗСК» используется как условная сетевая компания. Главный фокус ВКР — разработка web-приложения для подачи, обработки и сопровождения клиентских заявок и связанных документов.

Core project line for writing:

```text
client -> applicant -> request -> employee review -> decision -> agreement/document exchange -> client feedback/notification
```

## 2. Current Repo-Grounded Baseline

Current implementation is stronger than the earlier L1 client baseline. The project now contains client, employee and agreement-exchange flows that should be reflected in VKR materials after repo-check.

### 2.1 Client/backend/API baseline

Implemented or repo-observed baseline:

```text
- register client account;
- login client account;
- current user/session bootstrap;
- logout endpoint;
- create individual applicant party;
- list account applicant parties;
- select current/default applicant party;
- read current individual applicant party;
- create connection request;
- list own requests;
- read own request details.
```

### 2.2 Client UI baseline

Repo-observed client UI includes:

```text
- app shell / routing / providers;
- shared fetch / ProblemDetails / form error mapping;
- typed API wrappers and generated OpenAPI types;
- current-user session bootstrap;
- registration UI;
- login UI;
- account page with applicant creation/read behavior;
- request creation page/form;
- My Requests list page with filters;
- own request details page;
- client agreement exchange list page;
- client agreement exchange details page.
```

### 2.3 Employee/backend/UI baseline

Repo-observed employee flow includes:

```text
- employee Windows sign-in endpoint/client feature;
- employee request dashboard API and UI;
- employee request details API and UI;
- start request review;
- approve request review;
- reject request review with feedback;
- employee-side agreement exchange list and details pages.
```

### 2.4 Agreement/document exchange baseline

The implementation now contains an agreement proposal exchange slice:

```text
- AgreementProposalExchange domain model;
- AgreementProposal and AgreementDocumentRef domain objects;
- exchange statuses and proposal versions;
- start agreement exchange from an approved request;
- send employee proposal version;
- send client counter-proposal version;
- client accept active proposal;
- employee final refusal;
- agreement exchange list/details APIs;
- client and employee agreement exchange UI pages/widgets;
- integration and client tests for agreement exchange flows.
```

Diploma-safe formulation:

```text
В текущем программном срезе реализована не финальная юридическая процедура подписания договора, а обмен проектами договорного документа: сотрудник может начать обмен по одобренной заявке, отправить версию документа, клиент может просмотреть предложение, отправить свою версию или принять активное предложение, а сотрудник может продолжить обмен или завершить его отказом.
```

## 3. API Contract Baseline

The project has a client/server contract story:

```text
OpenAPI structural contract
+
generated TypeScript API types
+
generated semantic constants/error artifacts
+
typed client API wrappers
+
contract artifact checks
```

Diploma-safe formulation:

```text
Для согласования серверной и клиентской частей используется контрактный подход: структурное описание API выносится в OpenAPI-артефакт, а стабильные семантические константы ошибок и полей — в отдельные генерируемые JSON-артефакты. Это уменьшает риск рассинхронизации между ASP.NET Core backend и React/TypeScript frontend.
```

## 4. Testing Baseline

Repo-observed testing layers include:

```text
- domain tests for accounts, applicants, requests, request review and agreement proposals;
- server integration tests for L1 auth, applicant parties, client requests, employee requests, agreement exchanges and antiforgery;
- tools tests for OpenAPI and generated constants artifacts;
- client API/model/component tests for forms, lists, filters, details and agreement widgets;
- Playwright E2E tests for registration, login, applicant parties, request creation, my requests, request details and filters.
```

Playwright is also useful for report/presentation screenshots because the repository already has root E2E infrastructure with backend/frontend web servers and a test database reset command.

## 5. Still Planned / Do Not Overclaim

Before writing as fully implemented in final chapter text, recheck repo evidence for:

```text
- real email sending through SMTP/provider/outbox;
- persisted email notification history;
- production deployment;
- electronic signature;
- final legally valid contract signing;
- full audit/history timeline for all status changes;
- screenshot-ready happy path data for employee/agreement pages;
- whether employee authentication is acceptable for demo/prediploma or needs a simplified demo seed/login flow.
```

Current safe position:

```text
- employee request review is repo-observed;
- agreement/document exchange is repo-observed;
- email notification remains planned/designed unless a sender/outbox implementation is added;
- deployment can remain future work unless explicitly implemented.
```

## 6. Where Status Belongs

Detailed implementation status belongs here and in evidence/control files.

Clean VKR text should not become a tracker. In final diploma-oriented files prefer:

```text
- "в системе реализован программный срез";
- "в проектной модели выделяется";
- "в текущей версии реализованы основные операции";
- "email-уведомления рассматриваются как направление дальнейшего развития", если sender/outbox не реализованы;
- "после завершения реализации уточняется" for unstable facts.
```

Avoid long `[IMPLEMENTED] / [DESIGNED] / [PLANNED]` markers inside every paragraph of final text. Use them in working docs only.

## 7. Main VKR Source Files

Use these as the clean layer:

```text
planning/thesis/vkr-clean/vkr-outline.md
planning/thesis/vkr-clean/functional-specification.md
planning/thesis/vkr-clean/use-case-diagrams-plan.md
planning/thesis/vkr-clean/clean-requirements.md
planning/thesis/vkr-clean/clean-architecture.md
planning/thesis/vkr-clean/api-contract-and-client-server-sync.md
planning/thesis/vkr-clean/clean-domain-model.md
planning/thesis/vkr-clean/clean-database-design.md
planning/thesis/vkr-clean/clean-ui-description.md
planning/thesis/vkr-clean/clean-testing.md
planning/thesis/vkr-clean/visuals-and-diagrams-plan.md
planning/thesis/vkr-clean/playwright-screenshot-plan.md
planning/thesis/vkr-clean/planning-to-vkr-extraction-map.md
```

## 8. Current Writing Direction

```text
1. Sync clean VKR materials with current client + employee + agreement-exchange baseline.
2. Keep scenario specification as behavior specification, not only a flat requirement list.
3. Treat agreement exchange as the implemented document/draft exchange slice, not as final legal signing.
4. Keep email sending as planned/designed unless repo evidence confirms sender/outbox implementation.
5. Add API contract and generated artifacts as a separate architecture/implementation subsection.
6. Expand testing to include domain, API, client/component, E2E and artifact checks.
7. Prepare diagrams and Playwright screenshots from the clean plan.
8. Then expand chapters 1-3 into a preddiploma/PZ v0.7.
```
