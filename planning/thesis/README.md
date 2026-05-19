# VKR Thesis Workspace

Статус: главный вход в материалы ВКР / навигация v2

Эта папка содержит рабочую систему подготовки ВКР по теме:

> Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО «ЗСК».

## 1. Главный source of truth

Сначала читать:

```text
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/NEW-CHAT-ONBOARDING.md
```

Эти файлы объясняют, где лежат topic-драфты, section-драфты, research, визуальные материалы, repo/evidence facts и правила для новых чатов.

## 2. Основные зоны

| Зона | Путь | Назначение |
|---|---|---|
| Topic workbench | `vkr-topic-workbench/` | Смысловые topic-драфты, вопросы, research bridge, visual bridge, планы раскрытия |
| Clean VKR materials | `vkr-clean/` | Чистые материалы, section-драфты, evidence, тексты ближе к ПЗ |
| Research | `vkr-topic-workbench/00-research-materials/` | Research-отчёты и карты источников |
| Chat action algorithms | `chat-action-algorithms/` | Обязательные действия чатов в типовых ситуациях |
| Preddiploma derivatives | `preddiploma/` | Отчёт по практике и производные материалы |
| Presentation | `presentation/` | Слайды, доклад, демо-сценарий |

## 3. Где хранятся драфты

Topic-драфты:

```text
planning/thesis/vkr-topic-workbench/**/*.topic.md
```

Section-драфты:

```text
planning/thesis/vkr-clean/section-drafts/
```

Слабые/хаотичные главы, созданные до workflow:

```text
planning/thesis/vkr-clean/legacy-chaotic-drafts/
```

## 4. Актуальная формула работы

```text
topic draft
↔ вопросы / research / repo-check / visual bridge
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

Topic draft не заменяет section draft. Topic draft задаёт смысловые блоки, вопросы и план раскрытия. Section draft постепенно собирает текст под эти блоки.

## 5. Обязательное правило навигации

Если меняется структура папок, появляется новый тип артефакта, добавляется active/legacy статус или переносится драфт, нужно одновременно обновить навигацию:

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/vkr-topic-workbench/README.md
соответствующий chapter README
соответствующий topic-index.md, если меняются темы
planning/thesis/vkr-clean/README.md, если меняется clean layer
```

Не добавлять новые папки без README или явного упоминания в навигации.
