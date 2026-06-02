# Deferred Goals And Ideas

Status: current deferred backlog / far-future idea owner  
Doc version: v0.1.0  
Scope: parked goals, deferred features, far-future implementation ideas and postponed decision points across planning/workstream docs

This file keeps deferred ideas visible without bloating active implementation notes or current workstream plans.

## 0. Source Sync / ROOT-FULL-1

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.5.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/workstreams/command-system-and-tampermonkey-goal-map.md @ Doc version: v0.1.0
    - planning/workstreams/tampermonkey-command-projection-plan.md @ Doc version: v0.1.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.5.0
  Internal dependencies:
    - Deferred Items
    - Current Active-Work Boundary
    - How To Promote A Deferred Item
  Not checked:
    - full root coverage outside ROOT-FULL-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

ROOT-FULL-1 source pass treats this file as active backlog / deferred goal owner.
This section records the files that must be checked when this file is created, updated or used as a source for later work.
It does not make the Tampermonkey userscript or any example file a source of truth for command semantics.

## 1. Purpose

Use this file for ideas that should be remembered but not implemented now:

```text
- deferred features;
- far-future helper behavior;
- postponed command UX;
- parked implementation options;
- future work that must not distract the current MVP.
```

A deferred item here is not permission to implement it. It needs a later explicit plan and route through the relevant owner/workstream.

## 2. Status Labels

```text
DEFERRED
  Intentionally parked. Do not implement in the current active slice.

FAR FUTURE
  Worth remembering, but not a near-term candidate.

REVISIT AFTER
  Reconsider only after a named condition is met.

BLOCKED BY DECISION
  Needs a decision before planning.

SUPERSEDED
  Kept for history; not expected to be implemented.
```

## 3. Deferred Items

### DG-1 — Generic Action Overview / `Итог действия`

Status:
  DEFERRED

Owner / likely future route:

```text
planning/planning-use-case-map.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
future generic action overview owner/template if created
```

Why deferred:

```text
`План файл-обновление` already covers file/docs/code/archive update planning and review. Generic Action Overview should not weaken or duplicate that file-specific workflow.
```

Revisit after:

```text
- There is a clear meaningful non-file action category that needs structured review.
- The format can stay separate from `План файл-обновление`.
- The route can be added without making every response heavier.
```

Current boundary:

```text
Do not implement Generic Action Overview while the current work is Tampermonkey smoke testing and command-helper stabilization.
```

### DG-2 — Tampermonkey helper-owned compose buffer

Status:
  FAR FUTURE

Problem:

```text
When command bodies are inserted directly into the main ChatGPT composer, the composer can contain a mixture of:
- selected command blocks;
- user-written text;
- repeated command blocks;
- malformed blocks after manual edits;
- accidental garbage text.
```

Far-future direction:

```text
The helper may eventually provide its own compose area inside the widget.
The user would type the message/target there instead of typing directly into the main ChatGPT input.
The helper would assemble the final clean prompt only when the user chooses to insert it.
```

Possible behavior:

```text
1. User opens the helper.
2. User selects one or more commands.
3. Widget shows a selected-command stack.
4. User types free-form message/target in a helper-owned text area.
5. User can remove selected commands before insertion.
6. Helper validates or cleans command blocks before final insertion.
7. Helper inserts one clean assembled prompt into the main ChatGPT composer.
8. User still sends manually.
```

Current boundary:

```text
Do not implement in the list-only MVP or smoke-test fix loop.
```

### DG-3 — Selected-command stack before insertion

Status:
  FAR FUTURE

Concept:

```text
Selected commands:
  1. синх карта              [remove]
  2. давай архив             [remove]
  3. план файл-обновление    [remove]

User text:
  [free-form target/message typed inside helper]

Actions:
  [Insert assembled prompt] [Clear commands] [Cancel]
```

Why this may help:

```text
- makes multi-command selection visible before insertion;
- prevents hidden command accumulation in the main composer;
- lets the user delete commands without manually editing long command bodies;
- keeps the main composer clean until the final assembled prompt is ready.
```

Current boundary:

```text
Do not add selected-command stack until command insertion, widget behavior, composer detection and multiline formatting are stable.
```

### DG-4 — Cleanup / repair of malformed command blocks

Status:
  FAR FUTURE

Possible cleanup targets:

```text
- duplicate command blocks;
- empty selected commands;
- malformed command blocks caused by manual edits;
- incomplete `[ENMAN_COMMAND]` / `[/ENMAN_COMMAND]` pairs;
- garbage text between command blocks when the user asks to clean it;
- command blocks that conflict and require user confirmation.
```

Boundary:

```text
Cleanup must not change command semantics silently.
If the helper cannot confidently clean or repair the selected command set, it should ask the user to remove or confirm the problematic command before insertion.
```

Current boundary:

```text
Do not implement cleanup/repair before selected-command stack and helper-owned compose buffer are designed.
```

### FUT-SRC-VERIFY-1 — Register stale source/version usage command

Status:
  ⬜ deferred / future command

Goal:
  Add a command that checks source-sync registers for stale source paths, stale Doc version labels and register rows that still claim a version/status no longer present in the source file.

Expected command aliases:

```text
стейл версии в регистрах
проверь регистры на стейл версии
register stale version scan
```

Boundary:
  This is not implemented in ROOT-FULL-1. It is parked here so future command work can add it without mixing it into the Goal Map/Tampermonkey source pass.

### FUT-SRC-VERIFY-2 — Local Sources stale usage command

Status:
  ⬜ deferred / future command

Goal:
  Add a command that scans local `Sources:` blocks in active files and drafts for stale source versions, stale source paths, stale source relationships and missing source-register updates.

Expected command aliases:

```text
стейл локальные сорсы
проверь локальные сорсы
local sources stale scan
```

Boundary:
  This must check local file sections, not only registers. It should especially cover active drafts where local section `Sources:` blocks are authoritative.

### FUT-SRC-VERIFY-3 — Full source/version consistency audit command

Status:
  ⬜ deferred / future command

Goal:
  Add a full audit command that combines:
  - register stale version scan;
  - local Sources stale usage scan;
  - active-file Doc version presence check;
  - missing register impact check for created/updated active files.

Expected command aliases:

```text
полная проверка сорсов
полная source/version проверка
full source/version audit
```

Boundary:
  This is a future maintenance command. It should not claim full root/domain/slice coverage until it has checked both registers and local file content.

## 4. Current Active-Work Boundary

Current near-term work remains:

```text
- keep the list-only Tampermonkey widget stable;
- finish smoke testing;
- fix composer detection or multiline insertion only if tests show failure;
- do not add preview/search/external profile loading/far-future buffer behavior yet.
```

## 5. How To Promote A Deferred Item

Before promoting a deferred item:

```text
1. Check the active living Goal Map.
2. Confirm the item is now the selected slice/step.
3. Add or update the use-case route if user-facing command behavior changes.
4. Add an owner workflow/template if the behavior is bigger than implementation notes.
5. Produce `План файл-обновление` before editing files.
6. Keep command semantics out of the userscript unless rooted in the use-case map.
```
