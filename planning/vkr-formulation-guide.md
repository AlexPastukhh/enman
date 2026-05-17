# VKR Formulation Guide

Status: current internal writing guide  
Scope: clean VKR materials and presentation wording

## 1. Purpose

This file defines wording principles for clean VKR materials and defense materials.

Use it when editing:

```text
planning/thesis/vkr-clean/
planning/thesis/presentation/
```

This file is an internal writing guide. It is not final diploma text.

The goal is to make materials:

```text
- specific to the Enman / ООО «ЗСК» project;
- academically clear;
- technically precise;
- grounded in repository evidence and project sources;
- free from internal planning wording;
- separated by implementation status.
```

## 2. Core Topic Anchor

Current VKR topic:

```text
«Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО „ЗСК“».
```

Project essence:

```text
Web application on ASP.NET Core + React/TypeScript.

Clients register, sign in, create an individual applicant party and submit requests.

An employee sees submitted requests, takes them into manual review, approves or rejects them.

After approval, the system supports creating a contract/document draft and notifying the client by email.
```

When writing VKR text, prefer project-specific sentences over generic claims.

## 3. Source Priority

Use sources in this order:

```text
1. Current repository implementation and tests.
2. Current planning scenario specs and DATA specs.
3. User-provided topic and project description.
4. Uploaded defense examples only as structural references.
5. External literature only for theory/technology background.
```

Do not claim functionality is implemented unless implementation or tests confirm it.

## 4. Status Language

Use these working markers in clean drafts:

```text
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

Meaning:

```text
[IMPLEMENTED]
  Confirmed by source code, tests or working UI.

[DESIGNED]
  Described in planning materials as target design, but not yet confirmed by implementation.

[PLANNED]
  Intended for later implementation or full version.

[DEFERRED]
  Explicitly outside the current scope.

[QUESTION]
  Requires clarification from user, supervisor, code or methodology.
```

Final VKR text may remove markers after status is stable.

## 5. Main Style Rules

### 5.1 Be project-specific

Avoid generic statements that could fit any software project.

Bad:

```text
В современном мире информационные технологии играют важную роль в деятельности предприятий.
```

Better:

```text
В сетевой компании обработка клиентской заявки связана с несколькими последовательными действиями: приемом данных клиента, проверкой заявки сотрудником, принятием решения и подготовкой договорного документа.
```

### 5.2 Avoid promotional language

Bad:

```text
Разработанное приложение является мощным инструментом для оптимизации всех процессов компании.
```

Better:

```text
Разрабатываемое приложение снижает зависимость процесса обработки заявок от разрозненного ручного учета и позволяет хранить сведения о клиентах, заявителях и заявках в единой системе.
```

### 5.3 Do not overclaim implementation

Bad:

```text
В системе реализован полный документооборот с договорными предложениями и email-уведомлениями.
```

Better:

```text
[IMPLEMENTED] В базовом срезе реализованы регистрация клиента, создание заявителя-физического лица и подача заявки.
[DESIGNED] Полная версия предусматривает рассмотрение заявки сотрудником, подготовку проекта договора и уведомление клиента.
```

### 5.4 Separate design from code

Bad:

```text
Заявка имеет статусы InReview, Approved и Rejected, и все переходы уже реализованы.
```

Better:

```text
[DESIGNED] Проектный жизненный цикл заявки включает состояния InReview, Approved и Rejected.
[IMPLEMENTED] В текущем L1-срезе реализовано создание заявки с базовым статусом, используемым при сохранении заявки.
```

### 5.5 Avoid internal planning wording in clean files

Bad:

```text
Coverage item REQ-LC-002 должен быть покрыт domain draft.
```

Better:

```text
Для заявки предусмотрен переход из состояния рассмотрения в состояние одобрения. Такой переход выполняется только после положительного решения сотрудника.
```

### 5.6 Avoid vague architecture wording

Bad:

```text
Система построена на современных технологиях и имеет удобную архитектуру.
```

Better:

```text
Система построена как web-приложение с разделением клиентской и серверной частей. Серверная часть реализуется на ASP.NET Core, клиентская часть — на React и TypeScript. Для хранения данных используется реляционная база данных, доступ к которой организован через Entity Framework Core.
```

### 5.7 Do not invent sources or facts

Bad:

```text
В ООО «ЗСК» ежедневно обрабатывается большое количество заявок.
```

Better:

```text
В рамках работы ООО «ЗСК» рассматривается как условная сетевая компания, для которой характерен процесс приема и обработки клиентских заявок.
```

Use exact numbers only when they are known.

## 6. Frequent Bad Patterns And Replacements

| Bad pattern | Why bad | Replacement approach |
|---|---|---|
| “в современном мире” | generic opening | start from ООО «ЗСК» process |
| “повышает эффективность” | vague benefit | explain what operation becomes easier |
| “мощный инструмент” | promotional | say what data/actions system supports |
| “полностью автоматизирует” | overclaim | specify implemented/designed scope |
| “пользователь может удобно” | subjective | describe concrete UI action |
| “система должна” everywhere | requirement-only style | vary: “предусмотрено”, “реализовано”, “проектируется” |
| raw scenario IDs in main text | internal traceability noise | convert to natural scenario names |
| direct planning wording | not diploma style | rewrite as engineering result |

## 7. Examples For The Current Project

### 7.1 Problem statement

Bad:

```text
На предприятии есть проблема с документооборотом, которую необходимо решить с помощью автоматизации.
```

Better:

```text
При ручной обработке клиентских заявок возникает риск потери сведений о заявителе, неоднозначного определения текущего состояния заявки и задержек при подготовке договорных документов. Для сетевой компании это особенно важно, так как заявка клиента проходит несколько этапов и требует участия как клиента, так и сотрудника.
```

### 7.2 Goal

Bad:

```text
Целью является создание удобного web-приложения для автоматизации деятельности компании.
```

Better:

```text
Целью работы является разработка web-приложения, обеспечивающего регистрацию клиента, создание заявителя, подачу клиентской заявки, ее рассмотрение сотрудником и сопровождение договорного документа на последующих этапах обработки.
```

### 7.3 Requirements

Bad:

```text
Система должна иметь регистрацию, заявки, договоры и уведомления.
```

Better:

```text
К функциональным требованиям относятся регистрация и вход клиента, создание заявителя-физического лица, подача заявки, просмотр заявок сотрудником, фиксация решения по заявке, подготовка проекта договора и уведомление клиента о результате обработки.
```

### 7.4 Implemented slice

Bad:

```text
В системе реализован весь процесс обработки заявок.
```

Better:

```text
В текущем базовом срезе реализована серверная часть для регистрации клиента, создания заявителя-физического лица и создания заявки. Эти функции образуют начальный участок бизнес-процесса, на который в дальнейшем может быть добавлено рассмотрение заявки сотрудником и договорный документооборот.
```

### 7.5 Database description

Bad:

```text
База данных хранит всю необходимую информацию.
```

Better:

```text
В реализованном L1-срезе база данных хранит сведения об аккаунтах клиентов, заявителях и клиентских заявках. Для заявителя сохраняются контактные данные и ФИО, для заявки — связь с заявителем, тип заявки, статус, текстовое описание и адрес объекта.
```

### 7.6 UI description

Bad:

```text
Интерфейс удобен и интуитивно понятен.
```

Better:

```text
Пользовательский интерфейс должен поддерживать последовательный сценарий работы клиента: регистрацию, вход, заполнение данных заявителя, создание заявки и просмотр ее состояния. Для сотрудника предусматриваются страницы просмотра очереди заявок, детального просмотра заявки и принятия решения.
```

### 7.7 Testing

Bad:

```text
Проведено тестирование, все работает.
```

Better:

```text
Для проверки базового среза используются интеграционные тесты, которые выполняют регистрацию клиента, создание заявителя и создание заявки через API. Отдельно проверяются ошибки, например повторная регистрация с тем же email, отсутствие заявителя и пустое описание заявки.
```

## 8. Rewriting Workflow

Use this workflow for any new VKR section:

```text
1. Collect source facts.
2. Mark each fact as implemented, designed, planned, deferred or question.
3. Remove internal wording and raw planning mechanics.
4. Write a first clean version.
5. Replace generic phrases with project-specific details.
6. Check that every implementation claim is supported.
7. Add source/literature references later where needed.
8. Move unresolved issues to questions, not into final claims.
```

## 9. Reusable Writing Instruction

Use this instruction when asking for a new clean VKR section:

```text
Write a clean VKR section for the Enman / ООО «ЗСК» project.

Topic:
«Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО „ЗСК“».

Style:
academic but plain Russian;
specific to this project;
no generic introductions;
no promotional wording;
no internal planning wording;
no unsupported implementation claims.

Facts:
- stack: ASP.NET Core, React/TypeScript, EF Core, SQL Server;
- implemented base slice: client registration, sign-in, individual applicant party creation, request creation;
- designed full workflow: employee review, approve/reject decision, contract/document draft, email notification;
- organization: ООО «ЗСК» as a conditional/example network company.

Output:
Use status markers [IMPLEMENTED], [DESIGNED], [PLANNED], [DEFERRED], [QUESTION] where needed.
Separate implemented code from designed target behavior.
Prefer concrete project details over general phrases.
```

## 10. Review Checklist

Before accepting a clean VKR section, check:

```text
- Does it mention the specific project and ООО «ЗСК»?
- Does it avoid generic “modern world” opening?
- Does it avoid promotional language?
- Does it separate implemented and designed functionality?
- Does it avoid raw internal IDs unless evidence-map needs them?
- Does it avoid claiming full automation if only a slice is implemented?
- Does it describe actual data, roles, statuses, API, database or UI?
- Are unresolved assumptions marked as questions?
- Is it suitable for a diploma reader who does not know planning files?
```

## 11. What Not To Put Into Final VKR Text

Do not include:

```text
- instructions for generating materials;
- internal chat/task wording;
- raw replacement archive workflow;
- prompts;
- internal coverage mechanics;
- statements about how the material was produced;
- unverified claims about implementation completeness.
```

Convert internal material into normal engineering text.

## 12. Good Final Direction

A good VKR paragraph usually has this structure:

```text
1. Concrete process or problem.
2. Why it matters for the project.
3. What the system does or is designed to do.
4. What is implemented or planned.
```

Example:

```text
Клиентская заявка в сетевой компании связана не только с вводом данных клиента, но и с последующим рассмотрением сотрудником. Поэтому в системе выделены роли клиента и сотрудника. Клиент отвечает за регистрацию, создание заявителя и подачу заявки, а сотрудник — за проверку заявки и фиксацию решения. В текущем базовом срезе реализована клиентская часть процесса, а дальнейшее развитие предусматривает добавление рабочего места сотрудника и сопровождение договорного документа.
```
