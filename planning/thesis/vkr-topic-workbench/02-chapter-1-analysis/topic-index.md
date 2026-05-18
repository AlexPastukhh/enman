# Topic Index — Chapter 1 Analysis

Status: refined / chapter-level index

## Chapter purpose

Chapter 1 discloses the pre-project analysis: the business process, the problem requiring automation, possible automation alternatives, the selected development direction and high-level requirements.

## Active VKR point folders

| Folder | VKR point meaning | Main output | Status |
|---|---|---|---|
| `01-domain-process/` | 1.1 Характеристика процесса обработки клиентских заявок | process description, participants table, process flow | active |
| `02-documents-and-feedback/` | 1.2 Договорно-документный этап и обратная связь | document stage explanation, feedback link, storage boundary note | active |
| `03-problematic/` | 1.3 Проблематика ручного ведения заявок, документов и уведомлений | problem matrix, process-before-automation figure | active |
| `04-automation-options/` | 1.4 Анализ вариантов автоматизации процесса | alternatives comparison, justification of own web application | active |
| `05-selected-direction-and-requirements/` | 1.5 Выбор направления разработки, цели, задачи и требования | selected direction, goal/tasks, high-level requirements | active |

## Legacy initial folders

The following folders came from the first workbench version and should be treated as superseded naming until a dedicated cleanup/merge pass:

```text
01-domain-analysis/
02-documents-and-contracts/
03-manual-process-problems/
04-existing-solutions/
05-requirements/
```

Do not remove them through a normal replacement archive. Use a separate cleanup archive only after checking that no useful material remains there.

## Internal topic granularity rule

A VKR point folder may be covered by one strong topic draft if the point is cohesive. Do not create many topic files only for artificial granularity.

Recommended granularity:

| VKR point | Recommended number of topic files | Reason |
|---|---:|---|
| 1.1 Process | 1 | process can be explained as one coherent flow |
| 1.2 Documents/feedback | 2 | subject flow and storage boundary are different concerns |
| 1.3 Problematics | 1 | problem table can cover the point compactly |
| 1.4 Automation options | 3-4 | alternatives, criteria and own development justification differ |
| 1.5 Direction/goal/tasks/requirements | 3-4 | selected direction, tasks and requirements need separate handling |

## Synchronization rule

When a point folder or topic changes, update:

1. this chapter-level index;
2. the local child `topic-index.md`;
3. relevant visual evidence notes;
4. author raw message log only if the user introduced a meaningful new decision.
