# File Update Overview Template

Status: current reusable output template
Doc version: v0.1.0
Scope: exact Markdown structure for the final File Update Overview / `План файл-обновление` block used at the end of non-trivial file, documentation, code or archive update planning/review answers

Use with:

```text
planning/documentation/file-update-overview-workflow.md
```

Do not use this block as the whole answer when the task requires a Level 2 or Level 3 reviewable answer. Put it at the end, after `Краткое саммари` when that summary is used.

## Template

```text
Sources:
  Format/process:
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
  Internal dependencies:
    - Template
  Not checked:
    - downstream examples using this template outside ROOT-SRC-3A
```

```markdown
## План файл-обновление

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
Sources:
  Format/process:
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
  Internal dependencies:
    - Template
  Not checked:
    - downstream examples using this template outside ROOT-SRC-3A
```

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
Sources:
  Format/process:
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
  Internal dependencies:
    - Template
  Not checked:
    - downstream examples using this template outside ROOT-SRC-3A
```

```text
- Use normal Markdown in chat output, not an outer code fence around the final `План файл-обновление`.
- Prefer grouped Markdown tables for multi-file or multi-group overviews.
- Do not use one giant table for everything by default; preserve logical groups.
- Keep table cells short. Put detailed reasoning in the main answer.
- Include delivery-safety rows in `Проверка` for file-update planning.
- Keep `План файл-обновление` as the last file/change/update block when it is present.
- Do not use `План файл-обновление` or legacy `Итог` as a generic conclusion for answers without file/change/update context.
- Use `Краткое саммари` before `План файл-обновление` when a contextual summary is useful.
```


## Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
  Internal dependencies:
    - Purpose
  Not checked:
    - Goal Map/Tampermonkey and slice source passes outside ROOT-SRC-3A
```

```text
- ROOT-SRC-3A added Doc version and local section-level Sources blocks to this output/archive workflow file.
- ROOT-SRC-3A did not change output semantics or grant edit/commit permission.
```
