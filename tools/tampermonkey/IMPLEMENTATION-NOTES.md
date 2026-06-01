# Tampermonkey Implementation Notes

Status: current implementation notes / compound planning artifact  
Doc version: v0.1.0  
Scope: working notes for Tampermonkey helper implementation details, decisions, use cases, UI sketches, external sources and repo reading map

## 1. Purpose

This file is the implementation notes workspace for the Tampermonkey Chat Command Helper.

It intentionally collects several kinds of implementation detail in one place first:

```text
- accepted decisions;
- implementation principles;
- use cases;
- what should be possible to do;
- text visualization / UI sketches;
- command body behavior;
- external sources to check;
- repo docs to read and why;
- open questions;
- future split candidates.
```

This is a controlled compound notes file. It can later be split into smaller files after the implementation shape stabilizes.

## 2. Responsibility Split

```text
planning/workstreams/tampermonkey-command-projection-plan.md
  Owns command projection planning: source-of-truth order, envelope shape, MVP groups and inserted bodies.

tools/tampermonkey/README.md
  Owns implementation entrypoint and quick orientation.

tools/tampermonkey/IMPLEMENTATION-NOTES.md
  Owns current implementation notes, decisions, sketches, use cases and source-reading map.

tools/tampermonkey/chat-command-palette.user.js
  Future userscript implementation.
```

## 3. Accepted Decisions So Far

```text
- Generic Action Overview is deferred.
- `План файл-обновление` already covers file/docs/code/archive update planning.
- SL-6 / TM-0 is the active workstream focus.
- Tampermonkey profiles are projections, not source of truth.
- The source-of-truth route starts from `planning/planning-use-case-map.md`.
- Inserted command bodies stay compact.
- Full route fields stay in profile metadata, not in the inserted body.
- Every inserted body must include `source_of_truth`.
- Every inserted body must include `route_read_rule`.
- The route-read rule must tell the chat to read route/owners if not read in this chat.
- The route-read rule must tell the chat to reread source of truth if behavior, boundaries or key points are not remembered.
- MVP storage can be inline userscript profile data first.
- Later design should allow reusable defaults plus optional project profile config.
- No auto-send by default.
```

## 4. Implementation Principles

### 4.1 Source-of-truth principle

The userscript is never the source of truth.

Source-of-truth order:

```text
1. planning/planning-use-case-map.md
2. Owner workflow/template files linked from the route
3. Reusable examples linked from the route/example index
4. planning/workstreams/tampermonkey-command-projection-plan.md
5. tools/tampermonkey/IMPLEMENTATION-NOTES.md
6. userscript code
```

### 4.2 Projection principle

The helper projects repository command routes into prompt bodies. It must not invent command semantics.

### 4.3 Human-edit principle

The user must be able to preview and edit the generated prompt before insertion.

### 4.4 No-auto-send principle

The helper inserts text into the ChatGPT input. It does not send automatically by default.

### 4.5 Compact-body principle

Prompt body should contain compact reminders only:

```text
- command;
- command_family;
- source_of_truth;
- route_read_rule;
- key_reminders;
- user_target.
```

Do not paste full route metadata into the body:

```text
- active_context behavior;
- no_active_context behavior;
- traversal_depth;
- read_source_mode;
- expected_output;
- long use-case map row text.
```

### 4.6 Route-read principle

Every inserted body includes the route-read rule.

Canonical rule:

```text
route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.
```

## 5. Core Use Cases

### UC-1 — User inserts a high-risk command

Example:

```text
давай архив
```

Expected helper behavior:

```text
- user opens palette;
- selects `давай архив`;
- helper shows preview body;
- user fills target;
- helper inserts body into ChatGPT input;
- user reviews and sends manually.
```

Success condition:

```text
The chat receives enough reminders to avoid confusing archive source mode with output package mode and to include apply/diff commands.
```

### UC-2 — User inserts a map-sync command

Example:

```text
синх карта
```

Expected helper behavior:

```text
- helper inserts compact body;
- body reminds chat to inspect living map first;
- body reminds chat to output synced target-state brief;
- body reminds chat to create map-sync archive and apply/diff commands;
- body prevents starting the next functional slice.
```

### UC-3 — User inserts a planning command

Example:

```text
планируй
```

Expected helper behavior:

```text
- body reminds chat that planning means planning now;
- body reminds chat to consult living Goal Map when active workstream exists;
- body does not grant edit/archive permission.
```

### UC-4 — User inserts a simple output modifier

Example:

```text
без кп
```

Expected helper behavior:

```text
- helper still includes route_read_rule;
- body stays short;
- body says suppress only that block and do not change task content.
```

## 6. What Should Be Possible

MVP should make this possible:

```text
- open command palette;
- search command by alias;
- select command;
- see generated body preview;
- edit user_target;
- insert body into ChatGPT input;
- avoid auto-send;
- support MVP-1 command bodies;
- support MVP-2 helper bodies if cheap after MVP-1 data model is ready.
```

Later should make this possible:

```text
- load optional project profile config;
- separate reusable defaults from project commands;
- add keyboard shortcut;
- add last-used command memory;
- add command categories;
- validate profile object shape;
- export/copy selected command body without inserting.
```

## 7. Text UI Visualization

### 7.1 Palette closed

```text
[ENMAN]
```

A small floating button is visible near the input area.

### 7.2 Palette open

```text
┌──────────────────────────────────────────────┐
│ Enman commands                               │
│ Search: [арх___________________________]     │
├──────────────────────────────────────────────┤
│ давай архив          output package          │
│ арх                  archive source          │
│ синх карта           map sync + archive      │
│ план файл-обновление file/update planning    │
│ крит                 critical review         │
│ планируй             plan now                │
│ кц                   goal map brief          │
│ обс                  context recheck         │
└──────────────────────────────────────────────┘
```

### 7.3 Preview before insert

```text
┌──────────────────────────────────────────────┐
│ Preview: давай архив                         │
├──────────────────────────────────────────────┤
│ [ENMAN_COMMAND]                              │
│ command:                                     │
│   давай архив                                │
│ ...                                          │
│ user_target:                                 │
│   [_______________________________]          │
├──────────────────────────────────────────────┤
│ [Insert into chat] [Cancel]                  │
└──────────────────────────────────────────────┘
```

### 7.4 Insert result

The helper inserts the final body into the ChatGPT input. The user can still edit it before sending.

## 8. Command Profile Data Shape Draft

Userscript profile data can start like this:

```text
{
  id: "replacement_archive.create",
  group: "MVP-1",
  label: "давай архив",
  aliases: ["давай архив", "собери архив", "replacement package"],
  category: "archive",
  bodyTemplate: "... compact inserted body ..."
}
```

Keep route fields available as metadata if useful for implementation/debugging, but do not paste them into the inserted body by default.

Possible metadata fields:

```text
route: {
  sourceFile: "planning/planning-use-case-map.md",
  activeContext: "...",
  noActiveContext: "...",
  traversalDepth: "...",
  readSourceMode: "...",
  expectedOutput: "..."
}
```

## 9. Docs To Read And Why

| Doc | Read when | Why |
|---|---|---|
| `planning/planning-use-case-map.md` | Always before changing command profile semantics | Source of truth for command routes |
| `planning/workstreams/tampermonkey-command-projection-plan.md` | When changing profile/envelope/bodies | Owns projection plan and inserted bodies |
| `planning/goal-map-principles-workflow-template.md` | When changing `кц`, `карта цели`, `синх карта` behavior | Owns Goal Map workflow |
| `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | When changing `кп`, `саммари`, `крит`, response modifiers | Owns response behavior |
| `planning/documentation/file-update-overview-workflow.md` | When changing `план файл-обновление` | Owns file/update overview behavior |
| `planning/replacement-file-generation-guide.md` | When changing `давай архив` package behavior | Owns replacement package guidance |
| `planning/documentation/examples/README.md` | When linking command examples | Example index |
| `tools/tampermonkey/README.md` | When implementing or using the helper | Implementation entrypoint |
| `tools/tampermonkey/IMPLEMENTATION-NOTES.md` | During implementation planning | Current implementation notes and decisions |

## 10. External Sources To Check Later

Before writing production-like userscript behavior, check current external sources for:

```text
- Tampermonkey userscript metadata block syntax;
- ChatGPT DOM/input behavior for current UI;
- safe ways to insert text into the ChatGPT composer;
- clipboard and keyboard shortcut constraints;
- whether the selected browser supports required DOM APIs;
- Tampermonkey storage APIs if profiles move out of inline data.
```

Record checked external sources here before implementing behavior that depends on current browser or ChatGPT UI details.

## 11. Open Questions

```text
- Should MVP have one floating button or keyboard shortcut only?
- Should MVP store profiles inline only, or leave a disabled project-profile hook?
- Should command search match aliases, labels and categories?
- Should preview be a modal, side panel or inline popover?
- Should inserted prompt include empty lines exactly as documented?
- Should helper remember last user_target text?
- How should implementation detect the active ChatGPT input reliably?
```

## 12. Future Split Candidates

When this file grows, split into:

```text
tools/tampermonkey/IMPLEMENTATION-DECISIONS.md
tools/tampermonkey/USE-CASES.md
tools/tampermonkey/UI-SKETCHES.md
tools/tampermonkey/COMMAND-PROFILES.md
tools/tampermonkey/EXTERNAL-SOURCES.md
tools/tampermonkey/TEST-CHECKLIST.md
```

Do not split before the implementation shape stabilizes.
