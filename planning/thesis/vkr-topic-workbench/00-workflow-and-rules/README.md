# Правила и workflow рабочей базы тем ВКР

Статус: актуально / block-based topic-to-section workflow / research и visual bridge

Эта папка содержит правила работы с материалами ВКР до и во время написания section-драфтов.

## Основная идея

Topic-драфты и section-драфты развиваются параллельно. Topic-драфт хранит смысловые блоки, вопросы, research/repo/visual notes и планы раскрытия. Section-драфт постепенно собирает текст по этим блокам.

```text
topic draft
↔ вопросы / research / repo-check / visual bridge
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

## Основные файлы

| Файл | Назначение |
|---|---|
| `workflow.md` | общий порядок работы с темами, блоками section draft, research, visual и текстом |
| `topic-card-template.md` | шаблон topic-файла на русском языке |
| `topic-draft-format-protocol.md` | формат драфта темы перед/во время подготовки текста |
| `research-bridge-protocol.md` | как связывать topic-драфт с research без буквального копирования |
| `visual-bridge-protocol.md` | как превращать визуальные требования в рисунки/таблицы/скриншоты |
| `research-question-bank.md` | банк типовых вопросов к research и к проекту |
| `writing-from-topics-protocol.md` | как превращать темы в текст ПЗ |
| `visual-evidence-protocol.md` | старый/совместимый протокол для визуальных материалов |
| `mock-and-extension-point-wording-protocol.md` | как писать про mock/demo/stub-возможности без завышения реализации |
| `previous-topic-example-usage-rule.md` | как использовать уже принятые темы как примеры |
| `global-assumptions.md` | общие допущения и ограничения формулировок |
| `archive-merge-safety-protocol.md` | правила безопасных архивов с replacement files |
| `navigation-sync-note.md` | обязательное обновление навигации при изменении структуры |
| `author-materials/raw-author-message-log.md` | сырые сообщения автора без обработки |
| `examples/` | примеры удачного формата тем и визуальных ТЗ |

## Внешние navigation files

| Файл | Назначение |
|---|---|
| `../../VKR-WORKFLOW-SOURCE-OF-TRUTH.md` | главный source of truth |
| `../../NEW-CHAT-ONBOARDING.md` | памятка для новых чатов |
| `../TOPIC-TO-SECTION-BLOCK-WORKFLOW.md` | как topic-блоки превращаются в section-блоки |
| `../../chat-action-algorithms/` | обязательные действия чатов в типовых ситуациях |

## Правило языка

Смысловые материалы ВКР пишутся на русском языке. Английские имена допускаются только для технологий, стандартов, классов, файлов и терминов кода, если они нужны для точности.

## Правило active/legacy

Не писать новые драфты в legacy/support папки. Использовать их только для harvest. Cleanup делать отдельным архивом.

## Правило навигации

Если добавляется папка, новый тип артефакта или меняется active/legacy статус, одновременно обновить README/index/source-of-truth.
