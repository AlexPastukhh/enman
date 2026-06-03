# Plan File Update Command Example

Status: current reusable response-command and output example / demonstration-only
Doc version: v0.1.0
Scope: demonstrates `план файл-обновление` / file-update planning command output with the planned-mode File Update Overview

## 1. Purpose

This example demonstrates how a file/docs/code/archive update planning command should end with `План файл-обновление` in planned mode.

Owner files:

```text
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/planning-use-case-map.md
```

This example is demonstration-only. It does not own command semantics, routing, source truth, output mode, permission boundaries or file ownership rules.

## 2. User Command Examples

```text
план файл-обновление
спланируй файл-обновление
спланируй обновление файлов
спланируй архив
план архива
file update plan
archive plan
```

## 3. Meaning

The command asks for a concrete file/docs/code/archive update plan.

It does not grant permission to:

```text
- edit files;
- create an archive/package;
- run scripts;
- commit or push.
```

If the user later says `давай архив`, that is a separate output-package request.

## 4. Example Input

User:

```text
план файл-обновление для documentation-layer example taxonomy / field-kit example fit / deferred plan detail recovery
```

## 5. Example Output Shape

Main answer:

```text
- target and reason;
- owner/source files to read;
- planned file groups;
- boundaries / not changed;
- checks and risks;
- next action.
```

Then end with:

## План файл-обновление

Статус: `planned / no files changed`.

Что:
  Уточнить documentation-layer example taxonomy and example-fit setup rule; добавить deferred команду для восстановления деталей плана, когда Goal Map ещё не обновлён.

Почему:
  Reusable docs examples должны различать fully reusable examples и project-type/profile-specific reusable examples. Literal current-project-only examples должны жить в project-local/root area. При создании root UCM через field kit чат должен показать candidate profile examples пользователю и спросить fit, а не решать молча. Отдельно нужно не забыть deferred command для восстановления “рассыпанных” деталей плана.

Files:
  `planning/documentation/planning-docs-architecture-principles.md`, `planning/documentation/examples/README.md`, `planning/documentation/field-kits/root-use-case-map-field-kit.md`, `planning/documentation/use-case-map-workflow.md`, `planning/documentation/example-coverage-workflow.md`, `planning/planning-use-case-map.md`, `planning/deferred-goals-and-ideas.md`, `planning/root-source-sync-register.md`, `planning/documentation-action-log.md`.

Checks:
  No examples-as-authority; no silent profile-fit decision; no new active command; deferred recovery prompt command parked only; no slice/testing register work yet.

Next action:
  Include this in next `CASCADE-ROUTE-1B` archive scope.

## 6. Rules Demonstrated

```text
- The command produces a plan, not an edit.
- The final block is `План файл-обновление`, not generic `Итог`.
- The block can be used before files are changed: `planned / no files changed`.
- The block exposes files, reasons, checks and next action.
- Archive/package generation remains a separate explicit command.
```
