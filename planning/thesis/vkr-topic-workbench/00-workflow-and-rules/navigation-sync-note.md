# Navigation Sync Protocol

Статус: актуально / обязательное правило

## 1. Главное правило

Если меняется структура материалов ВКР, навигация обновляется в том же архиве/изменении.

Структурным изменением считается:

```text
- новая папка;
- новый тип файла;
- перенос topic-драфта;
- появление active/support/legacy статуса;
- добавление workflow-протокола;
- добавление chapter-level workbench;
- изменение пути хранения section drafts;
- изменение правил перехода от topic draft к section draft.
```

## 2. Минимум файлов для проверки

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/NEW-CHAT-ONBOARDING.md
planning/thesis/vkr-topic-workbench/README.md
planning/thesis/vkr-topic-workbench/00-workflow-and-rules/README.md
соответствующий chapter README
соответствующий topic-index.md, если меняются темы
planning/thesis/vkr-clean/README.md, если меняется clean layer
planning/thesis/vkr-clean/vkr-materials-index.md, если меняется clean navigation
```

## 3. Active/support/legacy

Нельзя оставлять папки с похожими названиями без объяснения. Если есть дубли, README должен явно сказать:

```text
active — куда пишем новое;
support — что читаем как источник;
legacy — что не используем для новых драфтов и harvest later.
```

## 4. Нельзя

```text
- создать папку без README;
- добавить workflow-файл без ссылки из README;
- заменить active path без source of truth update;
- удалить legacy без отдельного cleanup archive;
- генерировать section draft, если непонятно, какой topic-драфт является смысловой базой.
```
