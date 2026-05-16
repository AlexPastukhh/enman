# VKR Section Draft Register

Status: working register / reviewer workflow synchronized  
Scope: tracks short/full draft progress, reviewer state and fragment harvesting for VKR subsections

## 1. Status Legend

Draft statuses:

```text
no-draft
short-discussed
short-approved
full-draft-v1
full-draft-v2
full-draft-v3
chapter-ready
merged
superseded
```

Review statuses:

```text
not-reviewed
content-reviewed
structure-reviewed
style-reviewed
review-consolidated
```

Check statuses:

```text
fragments-harvested
source-checked
repo-checked
visuals-inserted
```

Use multiple statuses in notes when needed.

## 2. Chapter 1

| Section | Project question | Current status | Review status | Notes |
|---|---|---|---|---|
| 1.1 Анализ предметной области обработки клиентских заявок и документооборота | Why does the project need a request-centered system? | no-draft | not-reviewed | Next: write short draft in chat |
| 1.2 Анализ существующих решений и обоснование собственной разработки | Why is custom web application justified compared to ready-made systems? | full-draft-v1 | not-reviewed | Needs source markers, comparison table cleanup, reviewer pass and fragment harvest |
| 1.3 Формирование требований к разрабатываемой системе | What requirements follow from the domain and comparison? | no-draft | not-reviewed | Depends on 1.1 and 1.2 |

## 3. Chapter 2

| Section | Project question | Current status | Review status | Notes |
|---|---|---|---|---|
| 2.1 Пользовательские роли и сценарии | Who uses the system and what tasks do they solve? | no-draft | not-reviewed | Use functional specification |
| 2.2 Бизнес-процесс обработки заявки | How does a request move from client input to employee decision? | no-draft | not-reviewed | Needs draw.io diagram later |
| 2.3 Архитектура web-приложения | Why client-server architecture fits the system? | no-draft | not-reviewed | Use clean architecture and API contract docs |
| 2.4 API-контракт frontend/backend | How does OpenAPI/constants reduce frontend/backend drift? | no-draft | not-reviewed | Use API contract material |
| 2.5 Доменная модель и данные | Which objects represent the domain? | no-draft | not-reviewed | Needs repo/domain check |
| 2.6 Проектирование интерфейса | How do UI screens support scenarios? | no-draft | not-reviewed | Needs screenshots later |

## 4. Chapter 3

| Section | Project question | Current status | Review status | Notes |
|---|---|---|---|---|
| 3.1 Реализация серверной части | How backend implements the selected design? | no-draft | not-reviewed | Requires repo check |
| 3.2 Реализация клиентской части | How frontend implements user workflows? | no-draft | not-reviewed | Requires repo check and screenshots |
| 3.3 Работа с данными | How data is stored and linked? | no-draft | not-reviewed | Requires DB/schema evidence |
| 3.4 Тестирование | How correctness is verified? | no-draft | not-reviewed | Use testing materials and current tests |

## 5. Fragment Bank Status

| Fragment area | Status | Notes |
|---|---|---|
| Chapter 1 fragments | initialized | First sample fragment exists |
| Chapter 2 fragments | empty | Add after full drafts |
| Chapter 3 fragments | empty | Add after implementation drafts |
| Reviewer-suggested fragments | empty | Add after first reviewer pass |

## 6. Review Tracking Rule

When a full draft is reviewed, update the relevant row with:

```text
content-reviewed
structure-reviewed
style-reviewed
review-consolidated
fragments-harvested
```

as applicable.

Reviewer outputs are not final text. Only mark `chapter-ready` after:

```text
- reviewer feedback has been considered;
- source/repo/visual checks are complete enough for the subsection;
- TODO markers are either resolved or intentionally preserved for a later pass.
```
