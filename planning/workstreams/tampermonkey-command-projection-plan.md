# Tampermonkey Command Projection Plan

Status: current workstream planning file  
Doc version: v0.1.0  
Scope: planning rules for projecting repository command routes into editable Tampermonkey prompt-helper prompts; not userscript implementation

## 1. Purpose

This file plans how the Tampermonkey prompt helper should turn short commands into editable prompts that preserve command route, boundaries and key reminders.

The helper exists because a short user command can lose important execution details in a long chat. The helper should make those details visible before the message is inserted.

## 2. Source Of Truth

Tampermonkey command profiles are projections. They are not source of truth.

Source of truth order:

```text
1. planning/planning-use-case-map.md
2. Owner workflow/template files linked from the use-case row
3. Reusable examples linked from the use-case map or examples index
4. This planning file
5. Userscript implementation
```

If this file or the userscript conflicts with the use-case map or owner workflow, the use-case map / owner workflow wins.

The helper must not invent new command semantics. It can only surface compact reminders that are already grounded in the route chain.

## 3. Prompt Envelope Shape

Use this editable envelope shape:

```text
[ENMAN_COMMAND]

command:
  <selected user alias>

command_family:
  <aliases from planning-use-case-map.md>

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - <compact reminders only>

user_target:
  <free text user target/context>

[/ENMAN_COMMAND]
```

The envelope should be inserted into chat input, not auto-sent by default.

## 4. MVP Command Groups

### MVP-1 — high-risk / high-value commands

| Command profile | Aliases | Why first |
|---|---|---|
| `replacement_archive.create` | `давай архив`, `собери архив`, `replacement package`, `archive for manual apply` | Easy to forget package boundaries, apply/diff commands and commit review |
| `replacement_archive.review_diff_file` | `давай архив с review diff file`, `давай архив с repo diff`, `archive with review diff file` | Explicit-only alternative when clipboard diff transfer is not desired/practical |
| `archive_source.use` | `арх`, `из архива`, `use archive` | Prevents confusion between read-source archive and output package |
| `goal_map.sync` | `синх карта`, `синхронизируй карту`, `синх карта архив` | Combined command: synced brief + archive + apply/diff commands |
| `file_update.plan` | `план файл-обновление`, `спланируй файл-обновление`, `спланируй архив`, `file update plan`, `archive plan` | Keeps file/docs/code/archive planning tied to `План файл-обновление` |
| `critical_review.apply` | `крит`, `критически оцени`, `critical review` | Ensures honest critique without becoming hostile or editing files |
| `plan.now` | `планируй`, `распланируй`, `plan` | Must mean concrete planning now; use Goal Map when active workstream exists |
| `goal_map.brief` | `кц`, `карта цели кратко`, `goal map brief` | Requires current slice expanded and other slices status-only |
| `context_recheck.apply` | `обс`, `перепроверь обсуждение`, `context recheck` | Forces prior decisions/context recheck before answering |

### MVP-2 — useful command helpers

```text
карта цели / где мы / прогресс
кп / key points
саммари
давай драфт
обнови / актуализируй
уточни
расширь
отличия драфта
без кп
без саммари
без план файл-обновления
вспомни структуру репо / структура репо / слои репо
```

### Reserved / deferred

```text
Generic Action Overview / Итог действия
```

Generic Action Overview is deferred. Do not add it to the MVP command list until its owner workflow and command route exist.

## 5. MVP-1 Key Reminder Profiles

### replacement_archive.create

```text
key_reminders:
  - Output-package mode, not archive read-source mode.
  - Use active approved scope; ask only blocking questions.
  - Follow replacement archive workflow and current package layout rules.
  - Include ready-to-run apply/diff commands directly in chat.
  - Save full diff to file and copy it with ReadAllText(... UTF8) + Set-Clipboard.
  - Ask user to paste diff before commit.
  - Never commit/push without diff review.
```

### replacement_archive.review_diff_file

```text
key_reminders:
  - Explicit-only output-package mode, not default `давай архив`.
  - Use only when user asks for repo-stored review diff transfer or approves switch from clipboard diff.
  - Follow review-diff-file workflow.
  - Apply command may create/commit/push only `_ai-review-diffs/last-archive.diff`.
  - Do not create `_ai-review-diffs/last-archive-summary.md` by default.
  - Real archive files remain local until diff review approval.
```

### archive_source.use

```text
key_reminders:
  - Read-source mode, not output package mode.
  - Use uploaded/latest archive as a source snapshot.
  - Do not create a replacement archive unless separately requested.
  - State source snapshot limits when freshness matters.
```

### goal_map.sync

```text
key_reminders:
  - Inspect relevant living Goal Map first.
  - Produce synced target-state Goal Map Brief.
  - Produce narrow map-sync archive in the same response.
  - Include apply/diff commands in chat.
  - Do not start the next functional slice.
```

### file_update.plan

```text
key_reminders:
  - Plan file/docs/code/archive update only.
  - End with `План файл-обновление` in planned mode.
  - Include target files, responsibilities, `Что`, `Почему`, boundaries, checks and next action.
  - Does not edit files or create archive unless separately requested.
```

### critical_review.apply

```text
key_reminders:
  - Treat target as a hypothesis.
  - Give honest verdict, strong points, weak points, risks, assumptions and alternatives.
  - Do not disagree just to disagree.
  - Does not grant edit/archive/commit permission.
```

### plan.now

```text
key_reminders:
  - Produce a concrete plan now.
  - If active long-running workstream exists, consult living Goal Map.
  - Include chosen slice/step, boundary, evidence and next action.
  - Does not edit files or create archive unless separately requested.
```

### goal_map.brief

```text
key_reminders:
  - Use the relevant living Goal Map.
  - Current slice expanded; other slices status-only.
  - Use detailed slice statuses, not roadmap phase statuses.
  - If map is stale, say so and identify sync need.
```

### context_recheck.apply

```text
key_reminders:
  - Re-check prior discussion, accepted decisions and constraints.
  - Do not answer from memory when route/decision may have changed.
  - State what was checked and what remains unavailable.
```

## 5A. Inserted Command Body Rule

Tampermonkey stores profile metadata separately from the inserted prompt body.

Keep these outside the inserted body unless the chat explicitly needs them:

```text
- active_context behavior;
- no_active_context behavior;
- traversal_depth;
- read_source_mode;
- expected_output;
- owner file lists;
- long use-case map row text.
```

Every inserted command body must stay compact and must include:

```text
- selected command;
- short command family;
- source_of_truth;
- route_read_rule;
- key_reminders;
- user_target.
```

The `route_read_rule` is mandatory for every command body, including simple modifiers and suppressors.

Canonical rule:

```text
route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.
```

For simple commands with no dedicated owner/example file, keep the same meaning and use `if needed` in the source-of-truth line.

## 5B. Inserted Command Bodies

These bodies are the first Tampermonkey projection candidates.

They are not source of truth. They are compact prompts that tell the chat where to read the real route and what not to forget.

### MVP-1 — high-risk / high-value commands

#### replacement_archive.create / `давай архив`

```text
[ENMAN_COMMAND]

command:
  давай архив

command_family:
  `давай архив` / `собери архив` / `replacement package`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Output-package mode, not archive read-source mode.
  - Use active approved scope; ask only blocking questions.
  - Do not put apply commands only inside the archive.
  - Give apply/diff commands in chat.
  - Save full diff to file and copy it to clipboard.
  - Ask user to paste diff before commit.
  - Do not commit or push.

user_target:
  <what archive/package should include>

[/ENMAN_COMMAND]
```

#### replacement_archive.review_diff_file / `давай архив с review diff file`

```text
[ENMAN_COMMAND]

command:
  давай архив с review diff file

command_family:
  `давай архив с review diff file` / `давай архив с repo diff` / `archive with review diff file`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read `planning/replacement-file-generation-guide.md` and `planning/documentation/review-diff-file-workflow.md`.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Explicit-only output-package mode, not default `давай архив`.
  - Use only when repo-stored review diff transfer is requested/approved.
  - Apply command may create/commit/push only `_ai-review-diffs/last-archive.diff`.
  - Do not create `_ai-review-diffs/last-archive-summary.md` by default.
  - Real archive files remain local until diff review approval.

user_target:
  <what archive/package should include>

[/ENMAN_COMMAND]
```

#### archive_source.use / `арх`

```text
[ENMAN_COMMAND]

command:
  арх

command_family:
  `арх` / `из архива` / `use archive`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Read-source mode, not output-package mode.
  - Use provided/latest archive as source snapshot.
  - Do not create replacement archive unless separately requested.
  - State archive freshness/source limits when relevant.

user_target:
  <what should be checked from archive>

[/ENMAN_COMMAND]
```

#### goal_map.sync / `синх карта`

```text
[ENMAN_COMMAND]

command:
  синх карта

command_family:
  `синх карта` / `синхронизируй карту` / `синх карта архив`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Inspect the living Goal Map first.
  - Output Goal Map Brief in synced target state.
  - Create narrow map-sync archive in the same response.
  - Include apply/diff commands in chat.
  - Do not start the next functional slice.
  - End with `План файл-обновление`.

user_target:
  <goal/map target or current active workstream>

[/ENMAN_COMMAND]
```

#### file_update.plan / `план файл-обновление`

```text
[ENMAN_COMMAND]

command:
  план файл-обновление

command_family:
  `план файл-обновление` / `спланируй обновление файлов` / `спланируй архив`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Plan file/docs/code/archive update only.
  - End with `План файл-обновление` in planned mode.
  - Include files, responsibilities, `Что`, `Почему`, boundaries, checks and next action.
  - Do not edit files.
  - Do not create archive unless separately requested.

user_target:
  <what update/archive should be planned>

[/ENMAN_COMMAND]
```

#### critical_review.apply / `крит`

```text
[ENMAN_COMMAND]

command:
  крит

command_family:
  `крит` / `критически оцени` / `critical review`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Treat target as hypothesis, not accepted truth.
  - Give honest verdict.
  - Include strong points, weak points, risks, assumptions and alternatives.
  - Do not disagree just to disagree.
  - Do not edit files, create archives, commit or push.

user_target:
  <what should be critically reviewed>

[/ENMAN_COMMAND]
```

#### plan.now / `планируй`

```text
[ENMAN_COMMAND]

command:
  планируй

command_family:
  `планируй` / `распланируй` / `plan`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Plan now, do not defer.
  - Use living Goal Map when active workstream exists.
  - Choose concrete next slice/step.
  - State scope, boundary, expected evidence and next action.
  - Do not edit files or create archive unless separately requested.

user_target:
  <what should be planned>

[/ENMAN_COMMAND]
```

#### goal_map.brief / `кц`

```text
[ENMAN_COMMAND]

command:
  кц

command_family:
  `кц` / `карта цели кратко` / `goal map brief`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Output compact Goal Map Brief.
  - Current slice expanded.
  - Other slices status-only.
  - Use detailed slice statuses, not roadmap phase statuses.
  - If map is stale, say so.

user_target:
  <goal/map target or current active workstream>

[/ENMAN_COMMAND]
```

#### context_recheck.apply / `обс`

```text
[ENMAN_COMMAND]

command:
  обс

command_family:
  `обс` / `перепроверь обсуждение` / `context recheck`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Re-check relevant prior discussion.
  - Preserve accepted decisions and constraints.
  - State what was checked and what remains unavailable.
  - Combine with the underlying task route.

user_target:
  <what discussion/context should be rechecked>

[/ENMAN_COMMAND]
```

### MVP-2 — useful command helpers

#### goal_map.full / `карта цели`

```text
[ENMAN_COMMAND]

command:
  карта цели

command_family:
  `карта цели` / `где мы` / `прогресс`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Full Goal Map, not compact brief.
  - Show current goal, current state, slices, decisions and next action.
  - Say whether the map needs sync.

user_target:
  <goal/map target>

[/ENMAN_COMMAND]
```

#### output.key_points / `кп`

```text
[ENMAN_COMMAND]

command:
  кп

command_family:
  `кп` / `key points`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Add `Key points first`.
  - Key points mirror the main answer.
  - Do not replace the detailed answer.
  - Do not force fixed labels.

user_target:
  <answer/context>

[/ENMAN_COMMAND]
```

#### output.summary / `саммари`

```text
[ENMAN_COMMAND]

command:
  саммари

command_family:
  `саммари`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Add `Краткое саммари`.
  - Use fixed summary order.
  - This is not `План файл-обновление`.

user_target:
  <answer/context>

[/ENMAN_COMMAND]
```

#### draft.show / `давай драфт`

```text
[ENMAN_COMMAND]

command:
  давай драфт

command_family:
  `драфт` / `давай драфт` / `покажи драфт`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Show/update active draft if clear.
  - Ask target if no active draft is clear.
  - Do not silently broaden scope.

user_target:
  <draft target or current active draft>

[/ENMAN_COMMAND]
```

#### active.update / `обнови`

```text
[ENMAN_COMMAND]

command:
  обнови

command_family:
  `обнови` / `обнови драфт` / `актуализируй`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Apply latest discussion deltas.
  - Target active draft/answer/plan.
  - Ask target unless obvious.
  - Say “already current” if nothing changed.

user_target:
  <what should be updated>

[/ENMAN_COMMAND]
```

#### active.clarify / `уточни`

```text
[ENMAN_COMMAND]

command:
  уточни

command_family:
  `уточни`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Same scope.
  - More precise wording/boundary.
  - Do not expand or change target silently.

user_target:
  <what should be clarified>

[/ENMAN_COMMAND]
```

#### active.expand / `расширь`

```text
[ENMAN_COMMAND]

command:
  расширь

command_family:
  `расширь`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Add depth/examples/edge cases.
  - Do not silently change scope.
  - Mention scope note if expansion could be ambiguous.

user_target:
  <what should be expanded>

[/ENMAN_COMMAND]
```

#### draft.diff / `отличия драфта`

```text
[ENMAN_COMMAND]

command:
  отличия драфта

command_family:
  `отличия драфта` / `draft diff`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Compare active draft with previous version.
  - Prefer draft diff over key points for draft updates.
  - Ask target if no active draft is clear.

user_target:
  <draft target>

[/ENMAN_COMMAND]
```

#### output.suppress_key_points / `без кп`

```text
[ENMAN_COMMAND]

command:
  без кп

command_family:
  `без кп` / `без key points`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Suppress only `Key points first`.
  - Do not change task content.

user_target:
  <answer/context>

[/ENMAN_COMMAND]
```

#### output.suppress_summary / `без саммари`

```text
[ENMAN_COMMAND]

command:
  без саммари

command_family:
  `без саммари`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Suppress only `Краткое саммари`.
  - Do not change task content.

user_target:
  <answer/context>

[/ENMAN_COMMAND]
```

#### output.suppress_file_update_overview / `без план файл-обновления`

```text
[ENMAN_COMMAND]

command:
  без план файл-обновления

command_family:
  `без план файл-обновления` / `без итога`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Suppress only `План файл-обновление`.
  - Do not suppress `Краткое саммари` unless separately requested.
  - Do not change task content.

user_target:
  <answer/context>

[/ENMAN_COMMAND]
```

#### repo_structure.recall / `вспомни структуру репо`

```text
[ENMAN_COMMAND]

command:
  вспомни структуру репо

command_family:
  `вспомни структуру репо` / `структура репо` / `слои репо` / `где что лежит`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read `planning/repo-structure-memory.md` and linked files for this command route if needed.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - Reconstruct repo root areas and documentation/code/tooling layers before planning or editing.
  - Distinguish known structure from uncertain or unverified structure.
  - Identify source-of-truth chain and files to read next.
  - Do not edit files or create archive unless separately requested.
  - Do not invent missing root files; ask for repo tree/archive if needed.

user_target:
  <what task needs repo-structure orientation>

[/ENMAN_COMMAND]
```

## 6. Storage Decision

MVP storage:

```text
inline command profile data inside the userscript
```

Later-compatible direction:

```text
reusable defaults + optional project profile config
```

Reason:

```text
- inline is fastest for MVP;
- the prompt helper must work without external fetches;
- project profile compatibility should remain possible once the MVP proves useful.
```

## 7. UI Behavior

MVP UI should support:

```text
- floating button or keyboard-opened command palette;
- searchable command aliases;
- command preview before insert;
- editable user target/context field;
- insert into ChatGPT input;
- no auto-send by default.
```

## 8. Boundaries

```text
- The helper does not browse the repo.
- The helper does not verify freshness.
- The helper does not edit repository files.
- The helper does not commit or push.
- The helper does not become source of truth.
- The helper only inserts a better prompt for the chat to execute.
```

## 9. Implementation Documentation Infrastructure

Implementation docs:

```text
tools/tampermonkey/README.md
tools/tampermonkey/IMPLEMENTATION-NOTES.md
```

Responsibility split:

```text
planning/workstreams/tampermonkey-command-projection-plan.md
  Owns projection planning and documented inserted command bodies.

tools/tampermonkey/README.md
  Owns implementation entrypoint and quick orientation.

tools/tampermonkey/IMPLEMENTATION-NOTES.md
  Owns working implementation notes, accepted decisions, use cases, text UI sketches, external source checks and doc-reading map.
```

`IMPLEMENTATION-NOTES.md` is intentionally a compound notes file first. It can later be split into smaller implementation files after the helper shape stabilizes.


## 10. Next Implementation Step

After this planning file lands:

```text
1. Use the documented command bodies as the first profile body source.
2. Create implementation infrastructure docs under `tools/tampermonkey/`.
3. Convert MVP-1 profiles into a minimal userscript data structure.
4. Build command palette / preview / insert behavior.
5. Manual browser test.
```
