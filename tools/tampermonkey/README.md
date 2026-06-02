# Tampermonkey Chat Command Helper

Status: current Tampermonkey helper implementation entrypoint  
Doc version: v0.1.0  
Scope: implementation documentation entrypoint for the Enman Chat Command Helper; not the userscript source code

## 1. Purpose

The Tampermonkey Chat Command Helper should help the user insert structured command prompts into ChatGPT.

It should make short commands safer in long chats by adding compact reminders about:

```text
- command family;
- source of truth;
- route-read rule;
- key boundaries;
- user target/context.
```

The helper does not define command semantics. It only projects existing repository command routes into editable prompt bodies.

## 2. Current Implementation Documentation

Start here:

```text
tools/tampermonkey/README.md
```

Detailed implementation notes live here:

```text
tools/tampermonkey/IMPLEMENTATION-NOTES.md
```

Planning source:

```text
planning/workstreams/tampermonkey-command-projection-plan.md
```

Command source of truth:

```text
planning/planning-use-case-map.md
```

## 3. MVP Behavior

MVP helper behavior:

```text
1. User opens command palette.
2. User selects command profile.
3. Helper renders a preview of the command body.
4. User edits target/context.
5. Helper inserts the prompt into the ChatGPT input.
6. Helper does not auto-send by default.
```

## 4. Hard Boundaries

```text
- The helper does not browse the repo.
- The helper does not verify source freshness.
- The helper does not edit repository files.
- The helper does not create commits or push.
- The helper does not become source of truth.
- The helper only inserts a better prompt for the chat to execute.
```

## 5. Implementation Status

```text
current:
  first userscript skeleton

implemented:
  list-only draggable widget
  prioritized command lists
  click-to-insert documented command bodies
  no auto-send

next:
  manual browser test
  improve composer detection if needed

later:
  search
  preview/edit inside helper
  optional profile loading
```

## 6. Installing The First Skeleton

Use Tampermonkey's dashboard to create a new userscript and paste the contents of:

```text
tools/tampermonkey/chat-command-palette.user.js
```

The script uses a standard userscript header with `@match` entries for ChatGPT pages and `@run-at document-idle`.

## 7. Manual Test Checklist Placeholder

```text
- ENMAN widget appears on ChatGPT page.
- Header click opens the widget.
- Header click closes the widget.
- Header drag moves the widget without toggling.
- Command list scrolls.
- MVP-1 command can be selected.
- Command row click inserts compact command body into ChatGPT composer.
- Command row click keeps the widget open.
- Prompt is not auto-sent.
- Inserted body includes source_of_truth.
- Inserted body includes route_read_rule.
- Inserted body includes compact key_reminders.
- Composer-not-found error is safe and visible.
```


## 8. Interpreting Inserted Commands

Every inserted command body is an editable prompt projection, not command authority.

Interpretation order:

```text
1. planning/planning-use-case-map.md
2. owner workflow/template files linked from that route
3. reusable examples linked from the route/example index
4. planning/workstreams/tampermonkey-command-projection-plan.md
5. tools/tampermonkey/IMPLEMENTATION-NOTES.md
6. tools/tampermonkey/chat-command-palette.user.js
```

Rules:

```text
- The helper does not browse the repo or verify freshness.
- The helper does not decide that a command grants edit/archive/commit/push permission.
- The chat must read the route/owner files when behavior is uncertain or not checked in the current chat.
- If the helper body conflicts with the use-case map or owner workflow, the use-case map / owner workflow wins.
```

Archive command distinction:

```text
давай архив
  Default replacement archive/package mode.
  Post-apply review saves a local .diff and copies it to clipboard.

давай архив с review diff file
  Explicit-only review-diff-file mode.
  Apply command may create/push only _ai-review-diffs/last-archive.diff.
```

## 9. Repo Structure Helper Command

The helper includes a repo-orientation command:

```text
вспомни структуру репо
```

The command body points the chat to:

```text
planning/planning-use-case-map.md
planning/repo-structure-memory.md
```

Use it before planning/editing when the chat may have lost track of root areas, documentation layers, workstream files or app/tooling/example areas.
