# Command System And Tampermonkey Goal Process

Status: active workstream goal-process map  
Doc version: v0.1.0  
Owner format: `planning/goal-process-map.md`  
Scope: living process map for transferable command semantics, command examples, goal/process tracking, generic action overview and Tampermonkey prompt-helper work

This is a living map. Update it when the goal, scenario status, slice status, decision points or next action changes.

This file does not own command semantics, routing, source truth, output modes or permission boundaries.

Owner / routing files:

```text
planning/goal-process-map.md
planning/planning-use-case-map.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/documentation/examples/README.md
```

## 1. Final Picture

There is a transferable command system and Tampermonkey prompt helper that let a user work in long chats across projects without losing command meaning, goal state, source boundaries, acceptance criteria or next steps.

In the final state:

```text
- short commands are easy to type;
- each command has clear meaning, aliases, boundaries and expected behavior;
- root use-case maps route commands to owner workflows and examples;
- long-running goals can be shown as Goal Process Maps;
- the chat can critically evaluate user proposals instead of agreeing automatically;
- meaningful non-file actions can be summarized without misusing file-specific `Итог`;
- Tampermonkey can expand short commands into editable prompt-like instructions;
- project-specific command profiles can extend reusable command defaults without becoming source of truth.
```

## 2. Current State Overview

| Area | State | Notes |
|---|---|---|
| Reusable command examples | done | Level 2, plan command and archive source/output examples were added. |
| Level 2 / Key points / `Краткое саммари` | done | Key points are non-fixed compressed mirrors of the detailed answer; `Краткое саммари` owns fixed traceability order. |
| Planning command | done | `планируй` means produce a concrete plan now, not “plan later.” |
| Goal Process Map owner/example | done / validating | Root owner and root example are added; this file is the active living map. |
| Use-case routing | in progress | Goal Process Map is routed; reusable command examples still need routing from the root map. |
| Critical-thinking command | planned | Need `крит` semantics, example and route. |
| Generic Action Overview | planned | Need generic non-file overview without weakening file-specific `Итог`. |
| Tampermonkey MVP | planned | Need command palette, expansion model, preview/edit and insert behavior. |

## 3. Target Scenarios

| ID | Scenario | Desired behavior | Acceptance summary | Status |
|---|---|---|---|---|
| SC-1 | Short command is understood | User writes commands like `планируй`, `арх`, `давай архив`, `крит`, `прогресс`; chat understands mode, boundaries and output. | aliases, meaning, does-not-mean boundary, expected behavior, examples or deferred reasons | partially implemented |
| SC-2 | Critical thinking can be requested | User asks for honest evaluation; chat treats the user option as a hypothesis. | strengths, weaknesses, hidden assumptions, risks, alternatives, honest verdict | planned |
| SC-3 | Process map visible | User asks `карта процесса`, `где мы`, `прогресс`; chat shows final picture, slices, current state and next action. | final picture, current state, scenario/slice statuses, decision points, next action | first pass implemented / validating |
| SC-4 | Generic action overview works | After meaningful non-file work, user can request a structured action outcome. | action, status, scope, what changed, why, checked/not checked, owner, next action | planned |
| SC-5 | Tampermonkey helper works | User selects/types short command; script shows expanded editable prompt and inserts it into chat. | palette, alias search, preview/edit, insert, no auto-send, helper-not-source-of-truth | planned |

## 4. Process Slice Matrix

| Slice | Covers | Status | Current / next action |
|---|---|---|---|
| SL-1 Reusable command semantics | SC-1, SC-2, SC-3, SC-4 | partially implemented | Add `крит`; later add generic action overview command. |
| SL-2 Root use-case routing | SC-1, SC-3 | in progress | Route reusable command examples and add Enman-specific scenario/domain/slice command example. |
| SL-3 Goal Process Map | SC-3 | first pass implemented / validating | Validate this living map and update owner rules if needed. |
| SL-4 Critical-thinking command | SC-2 | planned | Define command, aliases, behavior, boundaries and example. |
| SL-5 Generic Action Overview | SC-4 | planned | Design owner/format/example while preserving File Update Overview. |
| SL-6 Tampermonkey MVP | SC-5 | planned | Define MVP command list and expansion schema. |

## 5. Slice Details

### SL-1 — Reusable command semantics

Covers:
- SC-1
- SC-2
- SC-3
- SC-4

Artifacts / actions:
- `planning/documentation/reviewable-agent-output-and-commands-workflow.md`
- `planning/documentation/examples/README.md`
- reusable command examples

Steps:
- [x] Clarify Level 2 / Key points / `Краткое саммари`.
- [x] Clarify `планируй` as immediate planning.
- [x] Add reusable command examples.
- [ ] Add critical-thinking command `крит`.
- [ ] Add generic action overview command.

Acceptance criteria:
- Commands have aliases, meaning, expected behavior and boundaries.
- Ambiguous commands have examples or explicit deferred reason.
- Command semantics stay in owner workflows, not examples.
- Commands do not imply edit/package/commit permission unless explicitly defined.

Verification:
- Review workflow command sections.
- Check examples index.
- Use commands in real chat.

Status:
- partially implemented.

### SL-2 — Root use-case routing

Covers:
- SC-1
- SC-3

Artifacts / actions:
- `planning/planning-use-case-map.md`
- Enman-specific command example under project-specific examples

Steps:
- [x] Route Goal Process Map commands.
- [ ] Route reusable command examples from root use-case map.
- [ ] Add Enman-specific scenario/domain/slice command example.
- [ ] Keep long examples outside the root use-case map.

Acceptance criteria:
- Use-case map remains router.
- Rows link to owner workflow and example files when relevant.
- No duplicated long workflow/example content is embedded in rows.
- Source mode and output mode remain separated.

Verification:
- Diff review of `planning/planning-use-case-map.md`.
- Check that examples remain demonstration-only.

Status:
- in progress.

### SL-3 — Goal Process Map

Covers:
- SC-3

Artifacts / actions:
- `planning/goal-process-map.md`
- `planning/goal-process-map-example.md`
- `planning/workstreams/command-system-and-tampermonkey-goal-process.md`

Steps:
- [x] Define the map as mini-architecture of a goal.
- [x] Separate owner file from static example file.
- [x] Route map commands from root use-case map.
- [x] Create active living workstream map in hybrid format.
- [ ] Validate hybrid format in real work.
- [ ] Add refinements if the format is hard to update.

Acceptance criteria:
- Owner file defines principles, format, workflow and update rules.
- Static example demonstrates target scenarios and process slices.
- Living map shows final picture, current state, scenario/slice status, decision points and next action.
- User can ask `где мы` and get a useful current-state answer from the living map.

Verification:
- Use this file for the next few command-system/Tampermonkey decisions.
- Confirm that updates are easy and do not require rewriting the whole map.

Status:
- first pass implemented / validating.

### SL-4 — Critical-thinking command

Covers:
- SC-2

Artifacts / actions:
- response workflow command section
- critical-thinking example
- root use-case route

Steps:
- [ ] Define canonical command `крит`.
- [ ] Define aliases: `критически`, `за и против`, `адвокат дьявола`, `оцени честно`, `не соглашайся автоматически`.
- [ ] Define expected behavior and does-not-mean boundary.
- [ ] Add reusable valid example.
- [ ] Route command from use-case map.

Acceptance criteria:
- Chat evaluates user proposal as hypothesis, not accepted truth.
- Chat states strengths, weaknesses, hidden assumptions, risks and alternatives.
- Chat gives honest verdict.
- Chat does not edit files, create archives or reopen accepted decisions unless asked.

Verification:
- Ask `крит` on a real plan and check whether the answer challenges weak assumptions.

Status:
- planned.

### SL-5 — Generic Action Overview / `Итог действия`

Covers:
- SC-4

Artifacts / actions:
- future action overview owner file or workflow section
- generic action overview example
- root use-case route
- relationship note to File Update Overview

Steps:
- [ ] Design generic action overview format.
- [ ] Keep File Update Overview / file-specific `Итог` as specialization.
- [ ] Add reusable example.
- [ ] Route command/use-case rows.
- [ ] Verify on a non-file decision or critical review.

Acceptance criteria:
- Generic overview works for meaningful non-file actions.
- File-specific `Итог` remains clear and unchanged.
- Clear use / do-not-use rules exist.
- Output helps a later chat continue.

Verification:
- Use after a non-file command-system decision.

Status:
- planned.

### SL-6 — Tampermonkey command palette MVP

Covers:
- SC-5

Artifacts / actions:
- `tools/tampermonkey/chat-command-palette.user.js`
- `tools/tampermonkey/README.md`
- optional project profile config

Steps:
- [ ] Define MVP command list.
- [ ] Define command expansion schema.
- [ ] Decide MVP storage: inline userscript vs config/profile.
- [ ] Build floating button / command palette UI.
- [ ] Add preview/edit before insert.
- [ ] Insert expanded prompt into ChatGPT input.
- [ ] Manual browser test.
- [ ] Decide whether project profile config is needed after MVP.

Acceptance criteria:
- User can pick a command.
- User can edit expanded prompt.
- Prompt inserts into chat input.
- No auto-send by default.
- Script does not become source of truth.
- Reusable defaults can later be extended by project-specific profiles.

Verification:
- Manual Tampermonkey test in browser.

Status:
- planned.

## 6. Decision Points

| ID | Question | Options | Current decision | Affects |
|---|---|---|---|---|
| DEC-1 | Next slice after this living map? | A: route examples; B: add `крит`; C: plan Tampermonkey MVP | deferred | SL-2, SL-4, SL-6 |
| DEC-2 | Where should Tampermonkey commands live? | A: inline MVP; B: reusable defaults + project profile; C: external JSON/raw URL | A for MVP, B as later-compatible design | SL-6 |
| DEC-3 | Generic Action Overview placement? | A: separate workflow/template; B: response workflow section | likely A if it becomes a real generic counterpart to File Update Overview | SL-5 |
| DEC-4 | How much of use-case map should Tampermonkey expansion repeat? | A: copy detailed docs; B: concise prompt reminders; C: command ID only | B preferred | SL-6 |

## 7. Current Focus

SL-3 validation:
- create and use a living hybrid Goal Process Map in repo;
- make sure it is easier to update than a long chat-only map.

Secondary focus:
- choose next slice after this file lands.

## 8. Next Action

After this workstream map is committed, choose one:

```text
A. F7-CMD-1B:
   route reusable command examples from `planning/planning-use-case-map.md`
   and add the Enman-specific scenario/domain/slice command example.

B. F7-CMD-2:
   add the critical-thinking command `крит`.

C. TM-0:
   plan Tampermonkey command palette MVP.
```

Recommended next action:
  F7-CMD-1B if we want to close already-open command example routing.
