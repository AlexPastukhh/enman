# Протокол безопасного применения архивов

Статус: актуально

## 1. Проблема

Архив может содержать много файлов:

```text
часть файлов новые;
часть файлов заменяет существующие;
замена может быть правильной по новой теме;
но случайно потерять старые guardrails, навигацию или детали.
```

Поэтому для больших архивов используется двухшаговый safe-merge workflow.

## 2. Шаг 1 — safe merge archive

Если архив заменяет существующие файлы, он обязан сохранить исходные версии внутри себя:

```text
_archive-review/<archive-slug>/
  ORIGINALS-INDEX.md
  MERGE-RISK-REPORT.md
  original-files/
    <repo-relative original file copies>
  raw-author-message-log.md
  derived-decisions.md
```

Если архив заменяет:

```text
planning/thesis/vkr-topic-workbench/00-workflow-and-rules/workflow.md
```

то исходная копия должна быть сохранена как:

```text
_archive-review/<archive-slug>/original-files/planning/thesis/vkr-topic-workbench/00-workflow-and-rules/workflow.md
```

## 3. Шаг 2 — merge correction archive

После применения архива нужно проверить:

```text
новые файлы нормальны или нет;
replacement files не потеряли важные guardrails;
какие sections из originals надо вернуть;
какие docs стали беднее;
что надо исправить небольшим correction-архивом.
```

Correction archive не должен тащить всё заново. Он исправляет только проблемные файлы.

## 4. Перед созданием архива

Перед созданием архива в чате нужно озвучить:

```text
какие author messages будут сохранены;
какие файлы будут добавлены;
какие файлы будут заменены;
где будут сохранены originals;
какие файлы high-risk;
как проверять после применения.
```

## 5. Запрет на скрытое удаление

Не удалять старые папки и файлы внутри большого mixed archive. Cleanup — отдельный явный шаг.

## 6. Исключение

Add-only архив может не сохранять originals, если он добавляет только новые пути и не заменяет существующие файлы.
