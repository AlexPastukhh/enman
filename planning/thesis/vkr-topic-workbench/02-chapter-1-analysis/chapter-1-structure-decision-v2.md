# Chapter 1 Structure Decision v2

Status: accepted / working decision

## Decision

Use the following Chapter 1 structure for future VKR drafting:

```text
1 Анализ предметной области и обоснование автоматизации

1.1 Характеристика процесса обработки клиентских заявок в сетевой компании
1.2 Договорно-документный этап и обратная связь с клиентом
1.3 Проблематика ручного ведения заявок, документов и уведомлений
1.4 Анализ вариантов автоматизации процесса
1.5 Выбор направления разработки, цели, задачи и требования к web-приложению
```

## Rationale

This structure follows the methodical logic of Chapter 1 as a pre-project investigation rather than a technical design chapter:

```text
process → problem → automation options → selected direction → goals/tasks/requirements
```

The structure also preserves the thesis-specific line:

```text
client → applicant → request → employee decision → contract/document stage → feedback/notification
```

## Why not only “existing solutions”

The section should not be limited to a review of software products. It should compare automation alternatives as possible ways to remove or weaken the discovered problem:

- ready-made EDMS/service desk systems;
- simple email + Excel workflow;
- custom web application.

## What belongs outside Chapter 1

Do not move the following into Chapter 1 as full technical topics:

- DDD/domain isolation details;
- scenario preconditions/postconditions/invariants as full specification;
- API contract design;
- database structure;
- implementation slices;
- tests.

These belong to Chapters 2 and 3. Chapter 1 may only introduce the need for them.

## Visuals expected in Chapter 1

- participants table;
- request handling process flow;
- request → decision → document flow;
- process-before-automation figure;
- problem → requirement table;
- automation options comparison table.

## Impact on workbench

The active Chapter 1 workbench folders are:

```text
01-domain-process/
02-documents-and-feedback/
03-problematic/
04-automation-options/
05-selected-direction-and-requirements/
```

The first-version folders are kept for review and should not be deleted automatically.
