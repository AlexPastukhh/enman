# Tampermonkey Chat Command Helper

Status: current Tampermonkey helper implementation entrypoint  
Doc version: v0.5.0
Scope: implementation documentation entrypoint for the Enman Chat Command Helper; not the userscript source code

## 0. Source Sync / ROOT-FULL-1 / SRC-CMD-1B

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.6.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/documentation/tampermonkey-command-projection-workflow.md @ Doc version: v0.1.0
    - planning/workstreams/tampermonkey-command-projection-plan.md @ Doc version: v0.5.0
    - planning/planning-use-case-map.md @ Doc version: v1.1.0
    - tools/tampermonkey/IMPLEMENTATION-NOTES.md @ Doc version: v0.2.0
    - tools/tampermonkey/chat-command-palette.user.js @ implementation/helper @version 0.4.0
  Internal dependencies:
    - Purpose
    - MVP Behavior
    - Interpreting Inserted Commands
  Not checked:
    - full root coverage outside ROOT-FULL-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

ROOT-FULL-1 source pass treats this file as Tampermonkey helper implementation orientation.
This section records the files that must be checked when this file is created, updated or used as a source for later work.
It does not make the Tampermonkey userscript, implementation notes or this README a source of truth for command semantics. Reusable projection rules live in `planning/documentation/tampermonkey-command-projection-workflow.md`.

SRC-CMD-1B updates this entrypoint for helper profiles that project source/version maintenance commands and the explicit `положняк` current-state command.

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

The helper does not define command semantics. It only projects existing repository command routes into editable prompt bodies. Reusable helper/projection rules and reusable starter notes are copied through `planning/documentation/`; this folder is the Enman project-local implementation.

## 2. Current Implementation Documentation

Start here:

```text
tools/tampermonkey/README.md
```

Detailed implementation notes live here:

```text
tools/tampermonkey/IMPLEMENTATION-NOTES.md
```

Reusable projection workflow:

```text
planning/documentation/tampermonkey-command-projection-workflow.md
```

Enman-specific planning/application source:

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
  userscript profile update

implemented:
  list-only draggable widget
  prioritized command lists
  click-to-insert documented command bodies
  no auto-send
  source/version maintenance command profiles
  explicit `положняк` current-state command profile
  neutral English command names in labels and inserted bodies

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
- Click `положняк` and verify the body says scope sections are relevant examples, not mandatory headings.
- Click one source/version audit command and verify it is review/audit mode, not edit/package mode.
- Command row click keeps the widget open.
- Prompt is not auto-sent.
- Inserted body includes source_of_truth.
- Inserted body includes route_read_rule.
- Inserted body includes compact key_reminders.
- Inserted body includes `english_name:`.
- Button label shows `<english name> · <command label>`.
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

Every inserted `[ENMAN_COMMAND]` body starts with:

```text
Read this whole command body before answering.
Do not ignore `key_reminders`.
```

These two lines are intentionally short. Detailed conflict/stop and honesty rules stay in `planning/workflow-activation-map.md` and `planning/planning-use-case-map.md`.


English display names:

```text
- Every helper profile has an English display name.
- Button labels use `<english name> · <command label>`.
- Inserted command bodies include `english_name:` immediately after `command:`.
- English names are readability labels only; they do not override `command`, `command_family`, UCM or owner docs.
```


```text
- The helper does not browse the repo or verify freshness.
- The helper does not decide that a command grants edit/archive/commit/push permission.
- The chat must read the route/owner files when behavior is uncertain or not checked in the current chat.
- If the helper body conflicts with the use-case map or owner workflow, the use-case map / owner workflow wins.
```

Archive command distinction:

```text
давай архив
  Default full replacement archive/package mode.
  No patches, no patch files, no partial snippets and no planning-only response.
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

## 10. Available Helper Profile Additions From SRC-CMD-1B

```text
- стейл версии в регистрах
- стейл локальные сорсы
- полная проверка сорсов
- положняк
```

These profiles remain editable prompt projections. They do not browse the repo, run automated scans, edit files, create archives, commit or push.

## 11. Source Delta / Change Log

```text
- SRC-CMD-1B added helper profile documentation for source/version maintenance and current-state commands; bumped this README to Doc version: v0.2.0.
- CASCADE-CMD-PREFLIGHT-0 added the two-line inserted command-body guardrail, clarified `давай архив` as full replacement archive only, and bumped this README to Doc version: v0.3.0.
```

### CMD-TM-EN-NAMES-1

```text
Source Delta:
  - Documented neutral English command names for helper button labels and inserted bodies.
  - Button labels use `<english name> · <command label>`.
  - Inserted bodies include `english_name:`.
```
