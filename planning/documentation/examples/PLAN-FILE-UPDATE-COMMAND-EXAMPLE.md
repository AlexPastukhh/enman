# Plan File Update Command Example

Status: current reusable response-command and output example / demonstration-only
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
спланируй архив для переименования `Итог` в `План файл-обновление`
```

## 5. Example Output Shape

Main answer:

```text
- target and reason;
- source/owner files to read;
- planned file groups;
- boundaries / not changed;
- checks and risks;
- next action.
```

Then end with:

## План файл-обновление

**Статус:** planned

### Change group: Response/output governance

| Change | File | R | Что | Почему |
|---|---|---|---|---|
| Updated | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | owns response placement/commands | Add `план файл-обновление` command aliases and placement | The file-update planning block needs a clear file-specific name |
| Updated | `planning/documentation/file-update-overview-workflow.md` | owns file-update overview workflow | Rename user-facing block from legacy `Итог` to `План файл-обновление` | Prevents conflict with future generic action/update summaries |
| Updated | `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md` | owns exact block shape | Change heading to `## План файл-обновление` | Keeps output shape reusable and explicit |

### Boundaries

| Type | File / artifact | R | Почему |
|---|---|---|---|
| Not changed | Generic Action Overview files | future non-file action overview | This command restores file-update planning first; generic action summary is separate work |
| Not created | Archive/package | output artifact | Planning command does not create an archive unless separately requested |

### Проверка

| Check | Result |
|---|---|
| Delivery safety classified | yes; complete replacement files are expected to be safe |
| Large/shared files | no large unsafe replacement identified in this example |
| Fresh full archive needed | no; current repo files are enough for planning |
| Preferred delivery | no artifact for planning; replacement archive only after `давай архив` |

### Следующее действие

Approve the plan or ask for changes. Say `давай архив` only after the planned file/update scope is accepted.

## 6. Rules Demonstrated

```text
- The command produces a plan, not an edit.
- The final block is `План файл-обновление`, not generic `Итог`.
- The block uses `Статус: planned`.
- The block exposes files, responsibilities, what/why, boundaries, checks and next action.
- Archive/package generation remains a separate explicit command.
```
