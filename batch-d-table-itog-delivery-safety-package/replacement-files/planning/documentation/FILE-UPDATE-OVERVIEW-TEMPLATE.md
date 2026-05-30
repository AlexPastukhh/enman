# File Update Overview Template

Status: current reusable output template  
Scope: exact Markdown structure for the final File Update Overview / `Итог` block used at the end of non-trivial file, documentation or code update answers

Use with:

```text
planning/documentation/file-update-overview-workflow.md
```

Do not use this block as the whole answer when the task requires a Level 2 or Level 3 reviewable answer. Put it at the end, after `Краткое саммари` when that summary is used.

## Template

```markdown
## Итог

**Статус:** <planned / archive created / diff checked / can commit / pushed / blocked>

### Change group: <logical group>

| Change | File | R | Что | Почему |
|---|---|---|---|---|
| New | `<path>` | <file responsibility> | <what is added or planned> | <why it is added or planned> |
| Updated | `<path>` | <file responsibility> | <what changes or is planned to change> | <why it changes or is planned> |

### Change group: <logical group>

| Change | File | R | Что | Почему |
|---|---|---|---|---|
| Updated | `<path>` | <file responsibility> | <what changes or is planned to change> | <why it changes or is planned> |

### Boundaries

| Type | File / artifact | R | Почему |
|---|---|---|---|
| Not changed | `<path>` | <file responsibility> | <why it is intentionally not changed> |
| Not created | `<path>` | <future responsibility if relevant> | <why it is intentionally not created> |

### Проверка

| Check | Result |
|---|---|
| Delivery safety classified | <yes/no> |
| Large/shared files | <yes/no + names if relevant> |
| Fresh full archive needed | <yes/no + reason> |
| Preferred delivery | <replacement archive / direct edit / targeted script fallback / no artifact> |
| <other check> | <result> |

### Следующее действие

<next concrete action>
```

## Field Notes

```text
Статус
  Current state of the update.
  Use `planned` while the overview is a rolling nearest-batch plan.
  Use actual states such as `archive created`, `diff checked`, `can commit`, `pushed` or `blocked` after artifacts/diffs/application exist.

Change group
  Logical system area, not a New/Updated bucket.

Change
  Table value for the file entry. Use `New` or `Updated` in change-group tables.

Type
  Table value for boundary entries. Use `Not changed` or `Not created`.

Not changed
  Existing relevant file intentionally excluded.

Not created
  Plausible new file intentionally not created.

R
  Short responsibility statement for the file.

Что
  Specific change in this file.

Почему
  Reason this file is included or excluded.

Проверка
  Checks performed, planned or still needed.
  For file-update planning, include delivery safety rows:
  delivery safety classified, large/shared files, fresh full archive needed and preferred delivery.

Следующее действие
  The next action after this answer.
```

## Rules

```text
- Use normal Markdown in chat output, not an outer code fence around the final `Итог`.
- Prefer grouped Markdown tables for multi-file or multi-group overviews.
- Do not use one giant table for everything by default; preserve logical groups.
- Keep table cells short. Put detailed reasoning in the main answer.
- Include delivery-safety rows in `Проверка` for file-update planning.
- Keep `Итог` as the last block when it is present.
- Do not use `Итог` as a generic conclusion for answers without file/change/update context.
- Use `Краткое саммари` before `Итог` when a contextual summary is useful.
```
