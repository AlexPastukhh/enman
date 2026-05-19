# VKR Workflow Source of Truth

Статус: актуально / block-based topic-to-section workflow v2

Этот файл является главным источником правды по workflow подготовки ВКР.

## 1. Главная идея

ВКР собирается не прямым копированием planning-файлов и не одной большой генерацией текста. Работа идёт через смысловые темы и блоки будущих section-драфтов.

```text
topic draft
↔ вопросы / research / repo-check / visual bridge
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

Topic draft — рабочая смысловая база темы. Section draft — постепенно собираемый текст ПЗ.

## 2. Где что хранится

| Тип материала | Путь | Роль |
|---|---|---|
| Topic-драфты | `planning/thesis/vkr-topic-workbench/**/*.topic.md` | Смысловые темы, вопросы, research/visual/repo notes, план раскрытия |
| Topic-to-section workflow | `planning/thesis/vkr-topic-workbench/TOPIC-TO-SECTION-BLOCK-WORKFLOW.md` | Как topic-блоки превращаются в блоки section-драфта |
| Section-драфты | `planning/thesis/vkr-clean/section-drafts/` | Уже текстовые черновики подразделов ВКР |
| Research | `planning/thesis/vkr-topic-workbench/00-research-materials/` | Исследовательские отчёты и source IDs |
| Clean materials | `planning/thesis/vkr-clean/` | Чистые материалы ВКР, evidence, section drafts, reviewer workflow |
| Chat action algorithms | `planning/thesis/chat-action-algorithms/` | Обязательные действия чатов в повторяющихся ситуациях |
| Chaotic legacy drafts | `planning/thesis/vkr-clean/legacy-chaotic-drafts/` | Слабые/хаотичные главы для анализа, не clean text |

## 3. Topic draft

Topic draft хранит:

```text
- карту размещения темы в структуре ВКР;
- роль темы;
- смысловые блоки темы;
- вопросы для конкретизации;
- research questions;
- repo/evidence questions;
- visual bridge;
- границы утверждений;
- план раскрытия будущих section blocks;
- черновые тезисы, которые могут стать текстом.
```

Topic draft не обязан быть идеальным перед началом section draft. Если один блок темы уже понятен, можно начинать соответствующий блок section draft.

## 4. Section draft

Section draft хранится в:

```text
planning/thesis/vkr-clean/section-drafts/
```

Section draft должен состоять из блоков, которые соответствуют смысловым блокам topic-драфта.

Для каждого блока section draft желательно иметь:

```text
1. вопрос, который блок закрывает;
2. ответы/уточнения;
3. research bridge, если нужен внешний источник;
4. repo-check, если блок говорит о реализации;
5. visual bridge, если нужен рисунок/таблица/скриншот;
6. план раскрытия;
7. постепенно собранный текст.
```

## 5. Research bridge

Research не вставляется буквально.

```text
вопрос по теме
→ ответ из research
→ переработка под проект ООО «ЗСК»
→ место вставки в будущий section block
```

Минимальный research bridge может быть внутри topic-драфта. Если research большой, можно создать рядом отдельный файл `*.research-bridge.md`.

## 6. Visual bridge

Визуальный материал не должен быть просто картинкой. Для каждого рисунка/таблицы/скриншота фиксировать:

```text
- где стоит в тексте;
- что читатель должен увидеть;
- для чего нужен;
- какие блоки/стрелки/поля;
- что нельзя показывать;
- подпись;
- пояснение после рисунка;
- где использовать: ПЗ / приложение / презентация / глава 3.
```

Если визуалов много, можно создать `*.figures-brief.md` или папку `visual-briefs/` рядом с темами.

## 7. Repo/evidence check

Repo-check обязателен для реализации и особенно для главы 3. Нельзя писать “реализовано” без проверки кода, тестов, UI, скриншотов или актуальных slice drafts.

Глава 3 должна строиться через связку:

```text
сценарии
→ вопросы
→ решения
→ домен
→ ADR / архитектурные решения
→ slice drafts
→ код / тесты / скриншоты
→ чистый текст главы 3
```

## 8. Active / support / legacy

Не удалять legacy/support материалы смешанным архивом. Правило:

```text
legacy/support не удалять;
не писать туда новые драфты;
использовать только для harvest;
cleanup делать отдельным архивом;
при изменении структуры обязательно обновлять README/navigation.
```

Активная компактная папка главы 3:

```text
planning/thesis/vkr-topic-workbench/04-chapter-3-implementation/
```

Подробная support-папка главы 3:

```text
planning/thesis/vkr-topic-workbench/04-chapter-3-implementation-and-testing/
```

## 9. Неактивный fragment bank

Fragment bank не является частью активного workflow. Вместо него используются section draft blocks: сначала обсуждается блок, задаются вопросы, строится план раскрытия, затем текст постепенно собирается прямо в section draft.

Старый `section-drafts/fragment-bank.md` не удаляется этим архивом, но считается deprecated/support, пока не будет отдельного cleanup.

## 10. Запреты для финальной ПЗ

Не писать в ВКР про:

```text
ИИ;
ChatGPT;
промпты;
чаты;
agent workflow;
archive-generation kitchen;
raw author logs.
```

Не использовать L1/L2 как язык ВКР. Использовать предметные формулировки: клиентский поток, рассмотрение заявки сотрудником, договорный обмен, программный срез, этап реализации.

## 11. Overclaim guardrails

Всегда разделять:

```text
что реализовано;
что проектируется;
что имитируется;
что является точкой расширения;
что относится к дальнейшему развитию.
```

Особенно осторожно писать про:

```text
mock-проверку данных;
email-уведомления;
файловое хранилище;
электронную подпись;
СЭД/ЭДО;
автоматическую генерацию договора;
внешние интеграции.
```

## 12. Обязательная навигационная синхронизация

Любое изменение структуры требует обновления навигации. Новый чат должен уметь открыть `planning/thesis/README.md`, затем этот файл, затем понять:

```text
где topic-драфты;
где section-драфты;
где research;
где visual briefs;
где repo/evidence facts;
какие папки active;
какие support/legacy;
как из темы получается текст ПЗ.
```
