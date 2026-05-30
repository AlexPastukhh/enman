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

**Change - New:** `<path>`  
**R:** <file responsibility>  
**Что:** <what is added or planned>  
**Почему:** <why it is added or planned>

**Change - Updated:** `<path>`  
**R:** <file responsibility>  
**Что:** <what changes or is planned to change>  
**Почему:** <why it changes or is planned>

### Change group: <logical group>

**Change - Updated:** `<path>`  
**R:** <file responsibility>  
**Что:** <what changes or is planned to change>  
**Почему:** <why it changes or is planned>

### Not changed

**Not changed:** `<path>`  
**R:** <file responsibility>  
**Почему:** <why it is intentionally not changed>

### Not created

**Not created:** `<path>`  
**R:** <future responsibility if relevant>  
**Почему:** <why it is intentionally not created>

### Проверка

- <what was checked or should be checked>
- <important limits / not checked items, if any>

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

Change - New
  New file/artifact being added or planned.

Change - Updated
  Existing file/artifact being changed or planned.

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

Следующее действие
  The next action after this answer.
```

## Rules

```text
- Use normal Markdown in chat output, not an outer code fence around the final `Итог`.
- Keep `Итог` as the last block when it is present.
- Do not use `Итог` as a generic conclusion for answers without file/change/update context.
- Use `Краткое саммари` before `Итог` when a contextual summary is useful.
```
