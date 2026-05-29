# File Update Overview Template

Status: current reusable output template  
Scope: exact structure for the final File Update Overview block used at the end of non-trivial file, documentation or code update answers

Use with:

```text
planning/documentation/file-update-overview-workflow.md
```

Do not use this block as the whole answer when the task requires a Level 2 or Level 3 reviewable answer. Put it at the end.

```text
Итог:
  Статус:
    <plan / archive created / diff checked / can commit / pushed / blocked>

  Change group: <logical group>
    Change - New: <path>
      R: <file responsibility>
      Что: <what is added>
      Почему: <why it is added>

    Change - Updated: <path>
      R: <file responsibility>
      Что: <what changes>
      Почему: <why it changes>

  Change group: <logical group>
    Change - Updated: <path>
      R: <file responsibility>
      Что: <what changes>
      Почему: <why it changes>

  Not changed:
    <path>
      R: <file responsibility>
      Почему: <why it is intentionally not changed>

  Not created:
    <path>
      R: <future responsibility if relevant>
      Почему: <why it is intentionally not created>

  Проверка:
    - <what was checked or should be checked>
    - <important limits / not checked items, if any>

  Следующее действие:
    <next concrete action>
```

## Field Notes

```text
Статус
  Current state of the update.

Change group
  Logical system area, not a New/Updated bucket.

Change - New
  New file/artifact being added.

Change - Updated
  Existing file/artifact being changed.

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
