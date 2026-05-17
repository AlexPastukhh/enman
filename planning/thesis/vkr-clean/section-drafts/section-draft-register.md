# VKR Section Draft Register

Status: working register / repo-inspection sync  
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
| 1.1 Анализ предметной области обработки клиентских заявок и документооборота | Why does the project need a web application connecting client requests, employee decisions, contracts/documents and notifications? | full-draft-v1 | not-reviewed | Full draft v1 exists at `chapter-1/01-01-problem-domain.full-v1.md`; next: Content/Structure/Style review, then fragment harvest and v2 |
| 1.2 Анализ существующих решений и обоснование собственной разработки | Why is custom web application justified compared to ready-made systems? | full-draft-v1 | not-reviewed | Needs source markers, comparison table cleanup, reviewer pass and fragment harvest |
| 1.3 Формирование требований к разрабатываемой системе | What requirements follow from the domain and comparison? | no-draft | not-reviewed | Next short draft after 1.1/1.2 review or after prediploma package outline |

## 3. Chapter 2

| Section | Project question | Current status | Review status | Notes |
|---|---|---|---|---|
| 2.1 Пользовательские роли и сценарии | Who uses the system and what tasks do they solve? | no-draft | not-reviewed | Use functional specification; now include client, employee and agreement exchange roles |
| 2.2 Жизненный цикл заявки | How does a request move from client input to employee decision? | no-draft | not-reviewed | Needs draw.io lifecycle diagram; repo now supports employee review actions |
| 2.3 Жизненный цикл документа и договора | How does an approved request lead to agreement/document exchange? | no-draft | not-reviewed | New/important: use AgreementProposalExchange baseline; do not describe as full legal signing |
| 2.4 Функциональная спецификация сценариев | Which scenarios define system behavior? | no-draft | not-reviewed | Use functional specification and current implemented slices |
| 2.5 Доменная модель | Which domain objects represent the request/review/agreement process? | no-draft | not-reviewed | Use updated clean-domain-model; needs final repo/domain check |
| 2.6 Структура базы данных | How are accounts, applicants, requests, reviews and agreement exchanges stored? | no-draft | not-reviewed | Use migrations/model snapshot; needs ERD |
| 2.7 Архитектура frontend и backend | Why client-server architecture fits the system? | no-draft | not-reviewed | Use clean architecture and API contract docs |
| 2.8 API-контракт приложения | How does OpenAPI/types/constants reduce frontend/backend drift? | no-draft | not-reviewed | Use API contract material and generated artifacts |
| 2.9 Проектирование пользовательского интерфейса | How do UI screens support client/employee/agreement workflows? | no-draft | not-reviewed | Use clean-ui-description and screenshot plan |

## 4. Chapter 3

| Section | Project question | Current status | Review status | Notes |
|---|---|---|---|---|
| 3.1 Структура программного решения | How is the solution organized in projects/modules? | no-draft | not-reviewed | Requires repo check and solution tree summary |
| 3.2 Реализация backend | How backend implements client, employee review and agreement exchange flows? | no-draft | not-reviewed | Requires repo check; use controllers/handlers/domain/persistence |
| 3.3 Реализация frontend | How frontend implements client, employee and agreement exchange screens? | no-draft | not-reviewed | Requires screenshot-ready UI and route/page summary |
| 3.4 Реализация хранения данных | How data is stored and linked? | no-draft | not-reviewed | Requires DB/schema evidence and ERD |
| 3.5 Реализация API-контракта | How OpenAPI/generated types/constants are generated and checked? | no-draft | not-reviewed | Use Tools, Shared/openapi.json and generated client types |
| 3.6 Проверка пользовательских сценариев | How are main workflows demonstrated? | no-draft | not-reviewed | Use Playwright screenshot plan and demo flow |
| 3.7 Тестирование доменной логики и API | How correctness is verified? | no-draft | not-reviewed | Use updated clean-testing and current tests |
| 3.8 Ограничения и дальнейшее развитие | What is implemented and what remains future work? | no-draft | not-reviewed | Use clean-results-and-future-work; email/deployment remain future unless implemented |

## 5. Fragment Bank Status

| Fragment area | Status | Notes |
|---|---|---|
| Chapter 1 fragments | initialized | First sample fragment exists; harvest 1.1 after reviewer pass |
| Chapter 2 fragments | empty | Add after lifecycle/domain/API full drafts |
| Chapter 3 fragments | empty | Add after implementation drafts and repo-check |
| Reviewer-suggested fragments | empty | Add after first reviewer pass |

## 6. Immediate Queue

```text
1. Review 1.1 full draft v1 with Content Reviewer.
2. Review 1.1 with Structure and Style/Originality reviewers if content review does not require major rewrite first.
3. Consolidate feedback and harvest fragment candidates.
4. Update 1.1 to full draft v2 or defer until PZ v0.7 assembly.
5. Prepare prediploma/PZ v0.7 outline using updated implementation baseline.
6. Prepare Playwright screenshot script/plan for report and slides.
```

## 7. Review Tracking Rule

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
