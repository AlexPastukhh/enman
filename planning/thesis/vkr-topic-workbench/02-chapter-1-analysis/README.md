# 02-chapter-1-analysis

Status: refined / chapter-1 topic workbench

Purpose:
store and organize semantic topics for Chapter 1 of the VKR. Chapter 1 is not an implementation chapter. It must support pre-project investigation, problem discovery, analysis of automation options, and transition to the selected development direction.

Methodical orientation:
Chapter 1 should answer why the system is needed: what process caused the automation problem, what the problem is, what alternatives exist, why the selected direction is justified, and what requirements follow from the analysis.

## Recommended Chapter 1 structure for VKR text

```text
1 Анализ предметной области и обоснование автоматизации

1.1 Характеристика процесса обработки клиентских заявок в сетевой компании
1.2 Договорно-документный этап и обратная связь с клиентом
1.3 Проблематика ручного ведения заявок, документов и уведомлений
1.4 Анализ вариантов автоматизации процесса
1.5 Выбор направления разработки, цели, задачи и требования к web-приложению
```

## Active topic folders

```text
01-domain-process/
02-documents-and-feedback/
03-problematic/
04-automation-options/
05-selected-direction-and-requirements/
```

## Legacy initial folders

The first workbench version used more generic folder names:

```text
01-domain-analysis/
02-documents-and-contracts/
03-manual-process-problems/
04-existing-solutions/
05-requirements/
```

Do not delete them through a mixed archive. Treat the active folders listed above as the preferred Chapter 1 workbench structure. The legacy folders can be reviewed and cleaned up later through a separate cleanup step.

## Rules

- Keep this folder aligned with its `topic-index.md`.
- Do not write final VKR text here.
- Store what must be disclosed, what evidence is needed and what questions remain.
- Use project/domain terms, not internal implementation labels such as L1/L2.
- Keep technical details such as DDD, API contract, database and testing primarily for Chapters 2 and 3.
- If a topic becomes large, it may become a subfolder inside the relevant VKR point.

## Chapter 1 flow

```text
process
→ document/feedback context
→ problematics
→ automation options
→ selected direction, goal, tasks, high-level requirements
```

## Common outputs from this folder

```text
topic cards
material harvest notes
visual evidence plan
future Chapter 1 short/full draft inputs
preddiploma report inputs
presentation problem/goal slides
```
