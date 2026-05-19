# VKR Topic Workbench

Статус: active / topic workbench / block-based topic-to-section workflow

Эта папка — рабочая база смысловых тем ВКР. Здесь хранится не финальный текст ПЗ, а темы, вопросы, research bridge, visual bridge, repo/evidence questions и планы раскрытия.

## 1. Главные правила

```text
Topic draft не заменяет section draft.
Topic draft питает section draft.
Section draft собирается по блокам, которые соответствуют смысловым блокам topic-драфта.
```

Основной workflow:

```text
topic draft
↔ вопросы / research / repo-check / visual bridge
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

Подробно:

```text
planning/thesis/vkr-topic-workbench/TOPIC-TO-SECTION-BLOCK-WORKFLOW.md
```

## 2. Главные входы

| Файл | Назначение |
|---|---|
| `../VKR-WORKFLOW-SOURCE-OF-TRUTH.md` | главный source of truth по всему workflow |
| `../NEW-CHAT-ONBOARDING.md` | памятка для новых чатов |
| `TOPIC-TO-SECTION-BLOCK-WORKFLOW.md` | как topic-блоки превращаются в section blocks |
| `00-workflow-and-rules/README.md` | правила и протоколы workbench |

## 3. Основные папки

| Folder | Meaning |
|---|---|
| `00-research-materials/` | Research reports, source IDs and chapter research maps |
| `00-workflow-and-rules/` | Rules, templates, protocols and author-message capture |
| `01-introduction/` | Introductory VKR content: relevance, goal, tasks, significance |
| `02-chapter-1-analysis/` | Analysis chapter topics: domain, problems, alternatives, requirements |
| `03-chapter-2-design/` | Design chapter topics: roles, scenarios, lifecycles, domain model, storage, architecture, API, UI |
| `04-chapter-3-implementation/` | Active compact Chapter 3 implementation topic drafts |
| `04-chapter-3-implementation-and-testing/` | Support/detailed source for Chapter 3; do not write new drafts here unless explicitly decided |
| `05-conclusion/` | Results, task completion and future development |
| `06-preddiploma-derivatives/` | Practice report, presentation and speech as derivatives of VKR materials |

## 4. Где topic-драфты

```text
planning/thesis/vkr-topic-workbench/**/*.topic.md
```

## 5. Где section-драфты

```text
planning/thesis/vkr-clean/section-drafts/
```

## 6. Active / support / legacy

Не писать новые драфты в support/legacy папки.

```text
active — основная папка для новых topic-драфтов;
support — источник для harvest;
legacy — старый материал, не source of truth.
```

Legacy/support не удалять смешанным архивом. Cleanup делать отдельным архивом после harvest.

## 7. Chapter 1 active folders

```text
02-chapter-1-analysis/01-domain-process/
02-chapter-1-analysis/02-documents-and-feedback/
02-chapter-1-analysis/03-problematic/
02-chapter-1-analysis/04-automation-options/
02-chapter-1-analysis/05-selected-direction-and-requirements/
```

Старые папки главы 1 с другими названиями считать legacy/support.

## 8. Chapter 3 active/support

Active:

```text
04-chapter-3-implementation/
```

Support/detailed source:

```text
04-chapter-3-implementation-and-testing/
```

## 9. Навигация

Если добавляется папка, topic-драфт, visual-brief, research-bridge, section workflow file или меняется active/legacy статус, обновить README/source-of-truth/topic-index в том же изменении.
