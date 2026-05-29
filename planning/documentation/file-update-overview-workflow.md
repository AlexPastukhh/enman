# File Update Overview Workflow

Status: current documentation-layer workflow  
Scope: when and how to produce the final structured File Update Overview / `Итог` block for non-trivial file, documentation or code update answers

## 1. Purpose

This workflow owns the process for producing a `File Update Overview` / `Итог` block.

A File Update Overview is a final summary block that shows:

```text
- which files are new;
- which files are updated;
- which relevant files are intentionally not changed;
- which possible files are intentionally not created;
- each file's responsibility;
- what changes;
- why the change is needed;
- what was checked;
- what the next action is.
```

It is meant to help a person or another chat understand the system impact of an update without re-reading the whole answer or diff.

## 2. Relationship To Response Levels

This workflow does not define Level 1, Level 2 or Level 3 answer rules.

Answer levels and response-level commands are owned by:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

Use this workflow when the main answer is a non-trivial update plan, archive/package response, diff review, applied-update check or multi-file synchronization/refactor summary and needs a structured final file/change summary.

The File Update Overview does not replace the main answer or `Краткое саммари`.

Use:

```text
main reviewable answer
+
Краткое саммари, when useful
+
File Update Overview / Итог at the end, when file/change/update context applies
```

## 3. When To Use

Use a File Update Overview when an answer plans, creates, reviews or verifies meaningful changes to files or file-like artifacts.

Typical cases:

```text
- documentation update plan;
- replacement archive/package summary;
- diff review before commit;
- post-apply verification;
- multi-file sync plan;
- code/file refactor plan;
- workflow/template/output-shape update;
- adding new documentation files;
- explaining why some expected files are intentionally not changed or not created.
```

It is especially useful when the change spans multiple responsibilities or when future chats need to continue from the answer.

## 3A. Planned And Actual Modes

A File Update Overview can describe two states of the same file/change work.

```text
Planned mode
  Use while the update is still being planned and no artifact/diff has been produced yet.
  The overview is the rolling nearest-batch plan and should be updated as decisions change.

Actual mode
  Use after an archive, script, diff, local application, commit or remote verification exists.
  The overview summarizes the actual artifact/diff/application/commit state.
```

Do not create separate overview formats for planned and actual mode. Use the same logical groups, responsibility fields, what/why fields, checks and next action.

Use `Статус` in the template to show the current state, for example:

```text
planned
archive created
diff checked
can commit
pushed
blocked
```


## 4. When Not To Use

Do not use the full File Update Overview for:

```text
- casual explanations;
- tiny one-command answers;
- simple yes/no checks;
- single-file typo fixes with obvious scope;
- answers that do not plan, change, review or package files.
```

A short sentence may be enough for narrow tasks.

Do not use File Update Overview as a generic conclusion. Use `Краткое саммари` for contextual/human summary when there is no file/change/update context.

## 5. Logical Grouping Rule

Group files by logical role in the system, not by `New` versus `Updated`.

Use:

```text
Change group: <logical group>
```

Examples:

```text
Change group: Response/output governance
Change group: Documentation navigation
Change group: Responsibility routing
Change group: Example coverage
Change group: Action log
```

Inside each group, list the files with their change type.

## 6. File Line Types

Use these file line types:

```text
Change - New:
  A new repository file or artifact will be added, or is planned to be added when `Статус` is `planned`.

Change - Updated:
  An existing repository file or artifact will be changed, or is planned to be changed when `Статус` is `planned`.

Not changed:
  A relevant existing file is intentionally left unchanged.

Not created:
  A plausible new file is intentionally not created.
```

Do not group files under separate top-level `New` and `Updated` sections by default. Preserve the logical system structure first.

## 7. Responsibility Field Rule

Each file entry must include `R`.

`R` means the file responsibility relevant to this change.

Preferred shape:

```text
Change - Updated: path/to/file.md
  R: owns ...
```

Keep `R` short. It should help the reviewer understand why the file belongs in this change.

Do not use `R` to restate the whole workflow or duplicate owner rules.

## 8. What / Why Rule

Each changed file entry should include:

```text
Что:
  what changes in this file.

Почему:
  why this file needs the change.
```

For `Not changed` and `Not created`, include `Почему` and optionally `R`.

This makes the overview show both the diff intent and the boundary of the update.

## 9. Checks And Next Action

End the overview with:

```text
Проверка:
  - checks performed or planned.

Следующее действие:
  next concrete action.
```

For archive/package work, `Следующее действие` should usually say whether to apply, paste diff, review, commit or continue with a follow-up script/archive.

## 10. Source-Of-Truth Boundaries

File Update Overview is a summary format.

It does not own:

```text
- response level selection;
- response block placement;
- response command semantics;
- documentation update mechanics;
- replacement archive mechanics;
- use-case routing;
- permission boundaries;
- file ownership rules.
```

Those remain in their owner files.

Use-case maps may reference File Update Overview as an expected output shape, but they must not copy this workflow or template logic.

Examples may demonstrate File Update Overview output, but they must not become the source of truth for the format.

## 11. Quality Checklist

Before finalizing an overview, check:

```text
- `Статус` makes clear whether the overview is planned, archive-created, diff-checked, applied, pushed or blocked;
- every changed/planned file has a clear responsibility;
- new files and updated files are placed in logical groups;
- relevant excluded files are listed when their exclusion matters;
- the overview does not replace the main reviewable answer;
- `Что` and `Почему` are specific enough to review;
- checks and next action are concrete;
- owner workflow/template/use-case logic is linked or named, not duplicated.
```

## 12. Do Not

```text
- Do not use File Update Overview as the whole answer for non-trivial work.
- Do not hide source or verification limits inside the overview.
- Do not duplicate complete workflow logic in the overview.
- Do not list only changed files when intentionally excluded files are important to understand scope.
- Do not create a separate standalone overview format when this template is sufficient.
```
