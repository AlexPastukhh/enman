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
- First userscript MVP uses no preview and no search.
- First userscript MVP uses prioritized command lists with a scrollbar.
- The widget opens and closes by clicking anywhere on its header.
- The widget can be dragged by its header.
- Header click and drag are separated by a small movement threshold.
- First userscript skeleton exists as `tools/tampermonkey/chat-command-palette.user.js`.
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

### UC-1 — User opens the helper widget

Example:

```text
User clicks the ENMAN widget header.
```

Expected helper behavior:

```text
- closed widget is visible as a small header/button;
- click anywhere on the header opens the command list;
- no prompt is inserted on open;
- no auto-send happens.
```

Success condition:

```text
The user can open the helper without changing the ChatGPT composer.
```

### UC-2 — User closes the helper widget

Example:

```text
User clicks the open widget header.
```

Expected helper behavior:

```text
- click anywhere on the header closes the command list;
- current open/closed state changes only on click, not on drag;
- no prompt is inserted.
```

Success condition:

```text
The user can close the helper without side effects.
```

### UC-3 — User drags the helper widget

Example:

```text
User holds the header and moves the pointer.
```

Expected helper behavior:

```text
- pointer down on header starts a possible click-or-drag interaction;
- if pointer movement stays below threshold, treat release as click toggle;
- if pointer movement exceeds threshold, treat interaction as drag;
- drag moves the widget and does not toggle open/closed on release;
- current open/closed state is preserved during drag.
```

Success condition:

```text
The widget can be moved freely without accidental open/close toggles.
```

### UC-4 — User scans prioritized command lists

Example:

```text
User opens the widget and scrolls the command list.
```

Expected helper behavior:

```text
- commands are shown in priority order;
- MVP-1 / high-risk commands appear first;
- MVP-2 helper commands appear after MVP-1;
- command list area scrolls when content exceeds available height;
- no search is present in the first MVP.
```

Success condition:

```text
The user can reach every command through the scrollbar without search.
```

### UC-5 — User inserts a command body

Example:

```text
User clicks `синх карта`.
```

Expected helper behavior:

```text
- command row click inserts the complete documented body into the ChatGPT composer;
- helper focuses the composer after insert;
- helper does not auto-send;
- helper closes after insert;
- user can still edit the inserted body in the ChatGPT input.
```

Success condition:

```text
Clicking a command row creates an editable prompt in the composer and leaves sending under user control.
```

### UC-6 — User inserts a high-risk command

Example:

```text
давай архив
```

Expected helper behavior:

```text
- inserted body includes output-package reminders;
- inserted body reminds that archive source mode is different;
- inserted body reminds to include apply/diff commands in chat;
- inserted body reminds not to commit or push.
```

Success condition:

```text
The chat receives enough compact reminders to avoid unsafe archive/package behavior.
```

### UC-7 — User inserts a simple output modifier

Example:

```text
без кп
```

Expected helper behavior:

```text
- inserted body remains compact;
- inserted body still includes source_of_truth and route_read_rule;
- inserted body says suppress only the named output block;
- underlying task content is not changed by the modifier.
```

Success condition:

```text
Even simple modifiers preserve source-of-truth and boundary reminders without bloating the UI.
```

### UC-8 — Chat composer is not found

Example:

```text
User clicks a command row but the helper cannot find the ChatGPT composer.
```

Expected helper behavior:

```text
- show a small error message in the widget;
- do not auto-send or throw visible JS errors;
- keep the widget usable so the user can retry.
```

Success condition:

```text
Failure to find the composer is safe and recoverable.
```

## 6. What Should Be Possible

First userscript MVP should make this possible:

```text
- show a small ENMAN widget;
- open the widget by clicking anywhere on the header;
- close the widget by clicking anywhere on the header;
- drag the widget by the header;
- avoid accidental toggle while dragging by using a click-vs-drag threshold;
- show prioritized command lists;
- scroll the command list;
- click command row to insert the complete command body;
- focus ChatGPT composer after insert;
- close the widget after insert;
- avoid auto-send;
- support MVP-1 command bodies;
- support MVP-2 command bodies if cheap after the same data model is ready.
```

Explicitly not in first MVP:

```text
- no command preview panel;
- no search input;
- no external profile loading;
- no automatic send;
- no browser/repo source verification;
- no source-of-truth changes.
```

Later should make this possible:

```text
- add search by aliases and labels;
- add preview/edit inside the helper before insert;
- load optional project profile config;
- separate reusable defaults from project commands;
- add keyboard shortcut;
- add last-used command memory;
- add command categories;
- validate profile object shape;
- export/copy selected command body without inserting.
```

## 7. Text UI Visualization

First MVP is list-only:

```text
no preview
no search
prioritized command lists
scrollbar
click row -> insert body into ChatGPT composer
```

### 7.1 Closed widget

```text
┌───────────────┐
│ ENMAN         │
└───────────────┘
```

Behavior:

```text
- click header opens the widget;
- drag header moves the widget;
- closed widget does not show commands.
```

### 7.2 Open widget

```text
┌──────────────────────────────────────────────┐
│ ENMAN commands                         [⇕]   │
├──────────────────────────────────────────────┤
│ MVP-1 / high-risk                            │
│  1. давай архив              output package  │
│  2. арх                      archive source  │
│  3. синх карта               map sync        │
│  4. план файл-обновление     file plan       │
│  5. крит                     critical review │
│  6. планируй                 plan now        │
│  7. кц                       goal brief      │
│  8. обс                      context recheck │
│                                              │
│ MVP-2 / helpers                              │
│  9. карта цели               full map        │
│ 10. кп                       key points      │
│ 11. саммари                  summary         │
│ 12. давай драфт              draft           │
│ 13. обнови                   update          │
│ 14. уточни                   clarify         │
│ 15. расширь                  expand          │
│ 16. отличия драфта           draft diff      │
│ 17. без кп                   suppress KP     │
│ 18. без саммари              suppress sum    │
│ 19. без план файл-обновления suppress FU     │
└──────────────────────────────────────────────┘
```

Behavior:

```text
- click header closes the widget;
- drag header moves the widget;
- command list scrolls if it does not fit;
- command rows insert bodies directly;
- no preview panel in first MVP;
- no search input in first MVP.
```

### 7.3 Click-vs-drag rule

Header is both toggle control and drag handle.

```text
pointer down on header:
  store starting pointer position

pointer move:
  if movement > drag threshold:
    enter dragging mode
    move widget
    suppress click toggle

pointer up:
  if dragging:
    finish drag
    do not toggle open/closed
  else:
    toggle open/closed
```

Suggested threshold:

```text
5px or 6px
```

### 7.4 Scroll behavior

```text
helper_panel:
  position: fixed
  max-height: min(70vh, 720px)
  overflow: hidden

header:
  fixed inside widget
  used for click toggle and drag

command_list_area:
  overflow-y: auto
  max-height: calc(panel height - header height)
```

Acceptance:

```text
- long command list does not exceed viewport;
- all commands remain reachable with scrollbar;
- header stays available for close/drag;
- no command row is hidden behind a footer.
```

### 7.5 Insert behavior

```text
click command row:
  render complete documented command body
  insert body into ChatGPT composer
  focus composer
  close widget
  do not auto-send
```

Acceptance:

```text
The user can edit the inserted prompt in ChatGPT before sending.
```

### 7.6 Error state

```text
┌──────────────────────────────────────────────┐
│ ENMAN commands                         [⇕]   │
├──────────────────────────────────────────────┤
│ Could not find ChatGPT input.                │
│ Click into the composer and try again.       │
└──────────────────────────────────────────────┘
```

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
- Should first skeleton use only a floating draggable widget, with keyboard shortcut deferred?
- Should MVP store profiles inline only, or leave a disabled project-profile hook?
- What exact click-vs-drag threshold should be used: 5px or 6px?
- Should widget position persist only for the page session or through Tampermonkey storage?
- Should inserted prompt include empty lines exactly as documented?
- Should helper remember last user_target text?
- How should implementation detect the active ChatGPT input reliably?
```

## 12. First Userscript Skeleton Notes

File:

```text
tools/tampermonkey/chat-command-palette.user.js
```

Implemented in the first skeleton:

```text
- standard userscript metadata header;
- ChatGPT page match rules;
- inline command profile data;
- ENMAN floating widget;
- header click-to-open / click-to-close;
- header drag-to-move with movement threshold;
- prioritized MVP-1 / MVP-2 command lists;
- scrollable command list area;
- command row click-to-insert;
- no auto-send;
- safe composer-not-found message.
```

Explicitly not implemented yet:

```text
- search;
- preview/edit inside the helper;
- external profile loading;
- persistent widget position;
- keyboard shortcut;
- source freshness checks.
```

Implementation caveat:

```text
Composer detection is a first-skeleton heuristic. If ChatGPT changes DOM structure or the heuristic chooses the wrong editable element, update `findComposer()` and record the fix here.
```

External sources checked for this skeleton:

```text
- Tampermonkey documentation: userscript header metadata, `@match`, `@grant`, `@run-at`.
- MDN: `dispatchEvent()` behavior for manually dispatched input/change events.
```


## 13. Manual Test Log

### 2026-06-02 — `синх карта` command insertion smoke test

Observed input:

```text
[ENMAN_COMMAND]
command:
  синх карта
...
[/ENMAN_COMMAND]
```

Result:

```text
partial pass
```

What this proves:

```text
- A `goal_map.sync` / `синх карта` command body reached the chat.
- The body contains the expected compact sections:
  - command
  - command_family
  - source_of_truth
  - route_read_rule
  - key_reminders
  - user_target
- The route-read rule is present.
- The body does not include full use-case route metadata such as active_context, traversal_depth or expected_output.
```

What this does not prove:

```text
- It does not prove all 19 command rows insert correctly.
- It does not prove header click open/close works.
- It does not prove header drag behavior works.
- It does not prove command list scrolling works.
- It does not prove no-auto-send behavior in every browser state.
- It does not prove composer detection works after ChatGPT DOM changes.
- It does not prove multiline formatting is preserved in every insertion path.
```

Follow-up manual tests:

```text
1. Test header click open/close.
2. Test header drag without accidental toggle.
3. Test command list scroll.
4. Test one MVP-1 command besides `синх карта`.
5. Test one MVP-2 command.
6. Test insertion into an empty composer.
7. Test insertion when composer already has text.
8. Verify no auto-send.
9. If line breaks collapse in the composer, fix `setComposerText()` / insertion strategy and document the browser behavior here.
```

## 14. Future Split Candidates

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
