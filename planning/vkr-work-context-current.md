# VKR Work Context Current

Status: internal VKR context  
Scope: current VKR writing context, repo-grounded baseline and next documentation steps

This file is not final diploma text. It is a working entry point for future VKR-related chats and replacement packages.

## 1. VKR Topic

«Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО „ЗСК“».

ООО «ЗСК» используется как условная сетевая компания. Главный фокус ВКР — разработка web-приложения для подачи, обработки и сопровождения клиентских заявок и связанных документов.

## 2. Current Repo-Grounded Baseline

According to current planning navigation and workflow, the L1 backend/API/persistence/session baseline exists for:

```text
- register client account;
- login client account;
- current user;
- logout;
- create individual applicant party;
- create connection request.
```

First-stage L1 client implementation exists for:

```text
- app shell / routing / providers;
- shared typed L1 API wrappers;
- shared fetch / ProblemDetails / form error mapping;
- current-user session bootstrap;
- registration UI;
- login UI;
- Account page applicant create UI.
```

Current remaining L1 client/read work includes:

```text
- logout UI/cache/navigation flow, if needed before protected flows;
- current applicant read after refresh;
- request creation form UI;
- My Requests read/list/detail UI;
- client/component tests for implemented client flows;
- browser E2E happy paths after UI/read flows are stable.
```

## 3. API Contract Baseline

The project now has a stronger client/server contract story:

```text
OpenAPI structural contract
+
generated semantic constants
+
typed client API wrappers
+
contract artifact checks
```

Diploma-safe formulation:

```text
Для согласования серверной и клиентской частей используется контрактный подход: структурное описание API выносится в OpenAPI-артефакт, а стабильные семантические константы ошибок и полей — в отдельные генерируемые JSON-артефакты. Это уменьшает риск рассинхронизации между ASP.NET Core backend и React/TypeScript frontend.
```

## 4. Where Status Belongs

Detailed implementation status belongs here and in evidence/control files.

Clean VKR text should not become a tracker. In final diploma-oriented files prefer:

```text
- "в системе предусматривается";
- "в проектной модели выделяется";
- "для текущего среза описывается";
- "после завершения реализации уточняется".
```

Avoid long `[IMPLEMENTED] / [DESIGNED] / [PLANNED]` markers inside every paragraph.

## 5. Main VKR Source Files

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
planning/thesis/vkr-clean/planning-to-vkr-extraction-map.md
```

## 6. Current Writing Direction

```text
1. Sync clean VKR materials with the L1 backend/client baseline.
2. Keep scenario specification as behavior specification, not only a flat requirement list.
3. Add API contract and generated artifacts as a separate architecture/implementation subsection.
4. Expand testing to include domain, API, client/component, E2E and artifact checks.
5. Prepare diagrams from the clean plan.
6. Only after this expand chapters 1-3 into long diploma prose.
```

## 7. Do Not Claim Without Rechecking

Before writing as implemented in chapter text, recheck repo evidence for:

```text
- request creation UI;
- My Requests list/detail UI;
- logout UI flow;
- current applicant read after refresh;
- employee dashboard/review UI;
- approve/reject API/UI;
- agreement/document creation;
- email sending;
- client/component tests;
- E2E tests for L1 happy paths.
```
