# Command System And Tampermonkey Goal Map

Status: active workstream goal map  
Doc version: v0.2.0
Owner format: `planning/goal-map-principles-workflow-template.md`  
Scope: living goal map for transferable command semantics, command examples, goal/process tracking, generic action overview and Tampermonkey prompt-helper work

This is a living map. Update it when the goal, scenario status, slice status, decision points, roadmap evidence or next action changes.

This file does not own command semantics, routing, source truth, output modes or permission boundaries.

Owner / routing files:

```text
planning/goal-map-principles-workflow-template.md
planning/planning-use-case-map.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/documentation/examples/README.md
```

## 0. Source Sync / ROOT-FULL-1

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.6.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ Doc version: v0.1.0
    - planning/planning-use-case-map.md @ Doc version: v0.5.0
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
    - planning/workstreams/tampermonkey-command-projection-plan.md @ Doc version: v0.2.0
    - tools/tampermonkey/README.md @ Doc version: v0.2.0
    - tools/tampermonkey/IMPLEMENTATION-NOTES.md @ Doc version: v0.2.0
    - tools/tampermonkey/chat-command-palette.user.js @ implementation/helper @version 0.2.0
  Internal dependencies:
    - Current Snapshot
    - Roadmap / Дорожная карта
    - Open decisions
  Not checked:
    - full root coverage outside ROOT-FULL-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

ROOT-FULL-1 source pass treats this file as active living workstream state artifact.
This section records the files that must be checked when this file is created, updated or used as a source for later work.
It does not make the Tampermonkey userscript or any example file a source of truth for command semantics.

## 0. Current Snapshot

SRC-CMD-1B note:
  Source/version maintenance commands and the explicit `положняк` current-state command are being added to the Tampermonkey helper palette as prompt projections. This does not make the userscript source of truth and does not replace SL-6 smoke testing.

CMD-CREATE-1 note:
  Command creation workflow and explicit parallel-work start helper profiles are being added as command-system infrastructure. Tampermonkey remains a projection layer; UCM and owner workflow files remain source of truth.

Last updated:
  2026-06-06 / after CMD-CREATE-1 command creation workflow and helper projection update

Current goal:
  Build a transferable command system and Tampermonkey prompt helper for long-running chats across projects.

Current focus:
  SL-6 — Tampermonkey helper smoke testing and command-list validation.

Active slice:
  SL-6 — Tampermonkey Guided Prompt Helper
    Status: ▶ NOW / planning

Already available:
  - Reusable command examples exist.
  - Level 2 / Key points / `Краткое саммари` rules exist.
  - `планируй` is defined as immediate planning, not “plan later.”
  - Goal Map owner, static example and living workstream exist.
  - Goal Map route exists in the root use-case map.
  - Living map has a Current Snapshot.
  - Tampermonkey helper requirement includes execution reminders, key boundaries and command-specific details.
  - Roadmap evidence format now uses status labels and `<details>` blocks instead of markdown task-list checkboxes.
  - Goal Map Brief response output rule exists.
  - Critical review command `крит` exists with response workflow semantics, root route and reusable command example.
  - Plan file-update command `план файл-обновление` exists for file/docs/code/archive update planning.
  - Goal Map status synchronization rule exists.
  - Goal Map sync command `синх карта` exists for synced brief + archive output.
  - Tampermonkey command projection plan exists.
  - Tampermonkey inserted command bodies are documented for MVP-1 and MVP-2.
  - Tampermonkey implementation docs entrypoint exists.
  - Tampermonkey implementation notes file exists.
  - Tampermonkey list-only draggable widget visual behavior is planned.
  - Tampermonkey first userscript skeleton exists.
  - First `синх карта` command insertion smoke test is recorded.
  - Command row click no longer closes the widget.
  - Repo-structure orientation command is documented and available in the helper.
  - Deferred goals and far-future ideas owner file exists.
  - Replacement archive default review transfer is restored to saved diff copied to clipboard.
  - Review-diff-file archive transfer is explicit-only.
  - Root responsibility map now includes Goal Map and Tampermonkey command-helper ownership discovery.
  - Tampermonkey helper has an explicit review-diff-file archive command profile in addition to the default clipboard-diff archive command.
  - Tampermonkey helper has source/version maintenance and `положняк` profiles as prompt projections.
  - Command creation workflow exists for creating/changing commands by rules and UCM row template.
  - Tampermonkey helper has `создай команду` and `начни параллельную работу` profiles as prompt projections.

Latest completed:
  - Added root Goal Map owner/example.
  - Added active living workstream map.
  - Added living-map storage rule to `planning/goal-map-principles-workflow-template.md`.
  - Renamed goal-map files to match compound starter artifact naming.
  - Added roadmap/evidence rules so completed work can show visible proof and explanatory details.
  - Routed reusable command examples from root use-case map and added Enman scenario/domain/slice command-routing example.
  - Added Goal Map Brief response output rule.
  - Added critical review command `крит` with route and reusable example.
  - Synchronized Goal Map statuses after SL-4 completion.
  - Clarified file-update planning output as `План файл-обновление`.
  - Added Goal Map Sync command `синх карта` and synchronized stale map statuses.
  - Deferred Generic Action Overview and selected TM-0 command projection planning.
  - Documented Tampermonkey inserted command bodies and route-read rule.
  - Added Tampermonkey implementation docs infrastructure and implementation notes.
  - Planned list-only draggable Tampermonkey widget visual behavior.
  - Added first Tampermonkey userscript skeleton.
  - Recorded first `синх карта` command insertion smoke test.
  - Fixed command row click so it keeps the widget open.
  - Added repo-structure orientation command and deferred-goals owner file.
  - Restored clipboard diff as the default replacement archive review transfer and kept review-diff-file mode explicit-only.
  - Added discovery paths so new chats can find Goal Map maintenance rules and Tampermonkey command-helper ownership.
  - Added explicit Tampermonkey helper profile for `давай архив с review diff file`.
  - Added Tampermonkey helper profiles for source/version maintenance commands and `положняк`.
  - Added command creation workflow and Tampermonkey helper profiles for `создай команду` and `начни параллельную работу`.

Next action:
  Continue SL-6 smoke testing:
    retest the helper command list, including ordinary `давай архив`, explicit `давай архив с review diff file`, source/version audit commands, `положняк`, `создай команду` and `начни параллельную работу`, while keeping userscript profiles as projections rather than source of truth.
Recommended next action:
  Continue Tampermonkey helper smoke testing before adding preview, search, external profile loading or deferred buffer/stack features.

Open decisions:
  - DEC-1: resolved — Generic Action Overview deferred; TM-0 selected.
  - DEC-2: Tampermonkey command storage.
  - DEC-3: Generic Action Overview placement.
  - DEC-4: how much use-case map detail Tampermonkey expansion should repeat.
  - DEC-5: final Generic Action / Change Overview format for DONE item details.
  - DEC-6: final Tampermonkey MVP command list.

Update rule:
  Update this snapshot after every meaningful batch, decision, route change, scenario/slice status change, roadmap evidence change or next-action change.

Planning rule:
  When planning inside this workstream, the agent must consult this map first, use the roadmap/slices to choose the next work direction, and state whether the map needs an update after the planned/applied work.

## 1. Roadmap / Дорожная карта

Roadmap shows how the final goal becomes real through phases and work directions / slices.

Status labels:

```text
✅ DONE     completed and backed by visible evidence
▶ NOW      active work
⏭ NEXT     next planned work
⬜ TODO     planned but not active
⚠ BLOCKED  blocked or needs a decision
```

Roadmap principle:

```text
phase goal -> needed behavior -> work directions / slices -> visible evidence -> verification of phase goal
```

A completed item must remain readable. Do not use markdown task-list checkboxes for roadmap records. Use status labels and expandable evidence details instead.

| Phase | Phase goal | Work directions / slices | Phase verification | Status |
|---|---|---|---|---|
| Phase 1 — Command foundation | Short commands work safely and consistently across projects. | SL-1 — Reusable Command Semantics Foundation; SL-2 — Root Command Routing | User command can be traced to owner workflow/example/boundary without hidden edit/package/commit permission. | ✅ DONE / validating |
| Phase 2 — Goal navigation | User can open repo state and understand current goal/progress/path. | SL-3 — Goal Map Format And Living Workstream | Living map has snapshot, roadmap, readable scenarios/slices, next action, open decisions and visible evidence. | ✅ DONE / validating |
| Phase 3 — Critical thinking | User can ask for honest evaluation instead of automatic agreement. | SL-4 — Critical Review Command | `крит` command has semantics, aliases, example, route and honest-verdict behavior. | ✅ DONE / validating |
| Phase 4 — Generic action outcomes | Meaningful non-file actions become reviewable. | SL-5 — Generic Action Overview | Non-file action can be summarized structurally without weakening file-specific `План файл-обновление`. | ⬜ DEFERRED / not started |
| Phase 5 — Guided command input | User can issue short commands without long chat forgetting execution details. | SL-6 — Tampermonkey Guided Prompt Helper | Expansion includes command label, execution reminders, key boundaries and command-specific details. | ▶ NOW / planning |

### 1.1 Phase 1 — Command foundation

Goal:
  Short commands work safely and consistently across projects.

Needed behavior:
  User writes a short command; chat understands mode, boundaries, expected output and whether permission is granted or not.

Work directions / slices:
  - SL-1 — Reusable Command Semantics Foundation
  - SL-2 — Root Command Routing

Phase verification:
  Phase is verified when:
  - root use-case map routes command rows to owners/examples;
  - command meaning and boundaries are documented;
  - examples exist for ambiguous commands;
  - source mode, output mode and edit/package/commit permissions are separated;
  - another chat can follow the route without guessing.

#### Progress

##### ✅ DONE — Clarify Level 2 / Key points / `Краткое саммари`

Result:
  Response shape rules distinguish key points, detailed answer and fixed summary order.

Visible evidence:
  - Changed:
    - `planning/documentation/reviewable-agent-output-and-commands-workflow.md`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Evidence details</summary>

**What changed**

- Response output rules were updated so Key points are a compressed mirror of the detailed answer, not a replacement.
- `Краткое саммари` owns the fixed traceability order near the end of the response.

**Visible result**

- The reusable response workflow contains the updated Level 2 / Key points / summary rules.
- The action log contains the related documentation update entry.

**Why this evidence exists**

- We identified that long chats need predictable output structure.
- Key points must help navigation without losing the detailed answer.

**How to verify**

- Open `planning/documentation/reviewable-agent-output-and-commands-workflow.md`.
- Check the sections about Level 2, Key points and `Краткое саммари`.
- Open `planning/documentation-action-log.md` and find the corresponding append-only record.

**What this evidence does not prove**

- It does not prove all command examples are routed from the root use-case map.
- That remains separate work in SL-2.

</details>

##### ✅ DONE — Clarify `планируй` as immediate planning

Result:
  `планируй` means “build a concrete plan now”, not “say planning is needed later”.

Visible evidence:
  - Changed:
    - `planning/documentation/reviewable-agent-output-and-commands-workflow.md`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Evidence details</summary>

**What changed**

- Planning command behavior was clarified as immediate planning.
- The assistant should enter the task context and produce a concrete plan of action rather than saying that planning will be needed later.

**Visible result**

- The response workflow documents the command behavior for planning.
- The action log records the reusable command/workflow update.

**Why this evidence exists**

- The user explicitly clarified that “планируй” means the assistant is already doing planning now.
- This prevents deferral-style answers.

**How to verify**

- Open `planning/documentation/reviewable-agent-output-and-commands-workflow.md`.
- Check the command semantics for planning.
- Confirm `planning/documentation-action-log.md` contains the related update record.

**What this evidence does not prove**

- It does not prove the root use-case map has every planning-related route needed for all future commands.
- Current batch improves Goal Map planning reference rules separately.

</details>

##### ✅ DONE — Add reusable command examples

Result:
  Reusable examples exist as separate artifacts.

Visible evidence:
  - Added/changed:
    - `planning/documentation/examples/LEVEL-2-KEY-POINTS-SUMMARY-EXAMPLE.md`
    - `planning/documentation/examples/PLAN-COMMAND-VALID-EXECUTION-EXAMPLE.md`
    - `planning/documentation/examples/ARCHIVE-SOURCE-VS-OUTPUT-PACKAGE-EXAMPLE.md`
    - `planning/documentation/examples/README.md`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Evidence details</summary>

**What changed**

- Reusable examples were added for response-shape, planning-command and archive-source-vs-output-package behavior.
- The examples index was updated so examples are discoverable.

**Visible result**

- Example files exist outside the root use-case map.
- `planning/documentation/examples/README.md` indexes the reusable examples.

**Why this evidence exists**

- Examples should demonstrate valid execution without bloating the root use-case map.
- Owner workflows keep semantics; examples show valid application.

**How to verify**

- Open the example files listed above.
- Open `planning/documentation/examples/README.md` and confirm they are indexed.
- Open `planning/documentation-action-log.md` and confirm the update is recorded.

**What this evidence does not prove**

- It does not prove the root use-case map already routes to every reusable example.
- That is still a next action for SL-2.

</details>

##### ✅ DONE — Route Goal Map commands

Result:
  Goal Map command family has owner, example and living workstream state.

Visible evidence:
  - Added/renamed:
    - `planning/goal-process-map.md` -> `planning/goal-map-principles-workflow-template.md`
    - `planning/goal-process-map-example.md` -> `planning/goal-map-example.md`
    - `planning/workstreams/command-system-and-tampermonkey-goal-process.md` -> `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
  - Changed:
    - `planning/planning-use-case-map.md`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Evidence details</summary>

**What changed**

- Goal Map owner, example and living workstream file names were synchronized with the compound starter artifact model.
- Root command routing was updated to the new owner/example paths.

**Visible result**

- Goal Map commands are owned by `planning/goal-map-principles-workflow-template.md`.
- The example path is `planning/goal-map-example.md`.
- The living map path is `planning/workstreams/command-system-and-tampermonkey-goal-map.md`.

**Why this evidence exists**

- The user wanted a repo-backed map that can be opened at any time to understand current goal and progress.
- The name `goal-process-map` no longer reflected the owner file responsibility.

**How to verify**

- Open `planning/planning-use-case-map.md` and check the Goal Map command owner block.
- Open `planning/goal-map-principles-workflow-template.md` and check the compound starter wording.
- Open `planning/workstreams/command-system-and-tampermonkey-goal-map.md` and check `## 0. Current Snapshot`.
- Open `planning/documentation-action-log.md` and find `2026-05-31 - Renamed and synchronized Goal Map files`.

**What this evidence does not prove**

- It does not prove the roadmap is intuitive enough.
- It does not prove reusable command examples are routed.
- Those remain active/next work.

</details>

##### ✅ DONE — Improve Goal Map readability and evidence records

Result:
  Living Goal Map becomes easy to read from the top: snapshot, roadmap, readable scenario/slice names, proof-oriented DONE items and clear next action.

Visible evidence:
  - Changed:
    - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
    - `planning/goal-map-principles-workflow-template.md`
    - `planning/planning-use-case-map.md`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Implementation details</summary>

**What changed**

- Add or refine roadmap as the main readable path after Current Snapshot.
- Use status labels instead of task-list checkboxes.
- Give scenarios and slices readable names while preserving IDs.
- Add evidence details dropdowns with bold mini-headings.
- Require planning passes to consult the active living Goal Map when work belongs to this workstream.

**Why this is needed**

- The file has useful data but should become more intuitive and representative.
- The user wants to understand what is happening in the work without reconstructing context from the chat.

**How to verify**

- Open the living map and confirm that Current Snapshot is followed by Roadmap.
- Check that DONE items include Visible evidence and dropdown details.
- Check owner file rules for roadmap/evidence/planning consultation.
- Check use-case map planning route includes the Goal Map reference obligation.

</details>

##### ✅ DONE — Route reusable command examples from root use-case map

Result:
  Root use-case map now exposes the reusable command example traversal chain, and Enman scenario/domain/slice command routing has a project-specific demonstration.

Visible evidence:
  - Changed:
    - `planning/planning-use-case-map.md`
    - `planning/documentation/examples/README.md`
    - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
  - Added:
    - `planning/documentation/examples/project-specific/enman/SCENARIO-DOMAIN-SLICE-COMMAND-ROUTING-EXAMPLE.md`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Evidence details</summary>

**What changed**

- Added `## 9A. Reusable Command Example References` to the root use-case map.
- Linked answer-shape, planning, archive source/output, Goal Map and Enman scenario/domain/slice route families to current examples.
- Added an Enman-specific scenario/domain/slice command-routing demonstration.
- Indexed the new project-specific example.
- Kept examples demonstration-only and outside root-map rows.

**Visible result**

- `planning/planning-use-case-map.md` now points command routes to reusable examples without copying example bodies.
- `planning/documentation/examples/README.md` lists the new Enman-specific command-routing example.
- The new example file demonstrates scenario, domain, slice/server/client and testing route traversal.

**Why this evidence exists**

- Existing command examples were useful but not visible enough from the root route chain.
- The root map should remain the concrete router, while examples demonstrate valid execution.

**How to verify**

- Open `planning/planning-use-case-map.md` and find `## 9A. Reusable Command Example References`.
- Open `planning/documentation/examples/README.md` and confirm the new Enman-specific example row is indexed.
- Open the new example file and confirm it is marked demonstration-only.
- Confirm no workflow/template semantics were rewritten.

**What this evidence does not prove**

- It does not implement `крит`.
- It does not implement Generic Action Overview.
- It does not implement Tampermonkey.

</details>

### 1.2 Phase 2 — Goal navigation

Goal:
  User can open repo state and understand current goal, progress, active slice, roadmap and next action.

Needed behavior:
  Opening `planning/workstreams/command-system-and-tampermonkey-goal-map.md` is enough to continue the work without reading the whole chat.

Work direction / slice:
  - SL-3 — Goal Map Format And Living Workstream

Phase verification:
  Phase is verified when:
  - Current Snapshot is present and current;
  - roadmap shows the full path;
  - scenarios and slices have readable names;
  - slices are explained as work directions;
  - each DONE item has visible evidence and expandable details;
  - next action and open decisions are visible;
  - planning commands consult this map when the work belongs to this workstream.

#### Progress

##### ✅ DONE — Add living workstream map

Result:
  Current command-system/Tampermonkey goal has a mutable repo file.

Visible evidence:
  - Added/renamed:
    - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Evidence details</summary>

**What changed**

- Created the active living map for this command-system/Tampermonkey workstream.
- The map stores current goal, slices, scenarios, decisions and next action.

**Visible result**

- The repo contains `planning/workstreams/command-system-and-tampermonkey-goal-map.md`.

**Why this evidence exists**

- Goal state should survive between chats.
- A future assistant should not need to infer the whole goal from scattered chat messages.

**How to verify**

- Open `planning/workstreams/command-system-and-tampermonkey-goal-map.md`.
- Check the title and owner/routing file block.

**What this evidence does not prove**

- It does not prove the map is fully readable or complete.
- Readability is being improved in the active batch.

</details>

##### ✅ DONE — Add Current Snapshot

Result:
  Top of the file shows current goal, focus, active slice, latest completed work, next action and open decisions.

Visible evidence:
  - Changed:
    - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
  - Visible section:
    - `## 0. Current Snapshot`

<details>
<summary>Evidence details</summary>

**What changed**

- Added a `Current Snapshot` section at the top of the living map.

**Visible result**

- Opening the file immediately shows:
  - Current goal;
  - Current focus;
  - Active slice;
  - Latest completed;
  - Next action;
  - Recommended next action;
  - Open decisions;
  - Update rule.

**Why this evidence exists**

- The user needs to open one file at any moment and understand where the work stands.

**How to verify**

- Open `planning/workstreams/command-system-and-tampermonkey-goal-map.md`.
- Confirm `## 0. Current Snapshot` exists near the top.

**What this evidence does not prove**

- It does not prove the roadmap below the snapshot is readable enough.
- That is the current active improvement.

</details>

##### ✅ DONE — Add readable roadmap and proof-oriented DONE details

Result:
  File shows a clear roadmap before detailed scenario/slice sections, and completed work has visible proof with dropdown details.

Visible evidence:
  - Changed:
    - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
  - Visible sections:
    - `## 1. Roadmap / Дорожная карта`
    - `## 4. Work Directions / Slices`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Implementation details</summary>

**What changed**

- Put Roadmap immediately after Current Snapshot.
- Use readable phase/slice/scenario names.
- Use status labels instead of markdown task-list checkboxes.
- Add evidence dropdowns with bold mini-headings.
- Put visible file/section changes into evidence details when useful.

**Why this is needed**

- The map should be both a current-state file and an orientation map for future planning.
- A user should understand the work without reading the whole chat.

**How to verify**

- Open the living map.
- Confirm Roadmap is directly after Snapshot.
- Confirm DONE items show Visible evidence and details.
- Confirm slices are described as work directions from needed behavior to plan/actions/verification.

</details>

## 2. Whole Picture / Картина целиком

| Result area | What should be possible | Current state | Scenarios | Work directions / slices |
|---|---|---|---|---|
| Reliable command understanding | User writes short commands and the chat understands mode, boundaries, expected output and permissions. | done / validating for current command set | SC-1 — Short Command Works With Boundaries | SL-1 — Reusable Command Semantics Foundation; SL-2 — Root Command Routing |
| Honest critical review | User can ask the chat to evaluate a plan or idea as a hypothesis, not as accepted truth. | done / validating | SC-2 — Honest Critical Review On Demand | SL-4 — Critical Review Command |
| Goal navigation | User can open this repo file and immediately see the current goal, progress, active slice, next action and decisions. | done / validating | SC-3 — Current Goal Map Is Openable And Useful | SL-3 — Goal Map Format And Living Workstream |
| Generic action outcome | User can get a structured outcome for meaningful non-file actions without misusing file-specific `План файл-обновление`. | deferred / not started | SC-4 — Generic Action Outcome Is Available | SL-5 — Generic Action Overview |
| Guided command prompts | Tampermonkey expands short commands into editable prompts with execution reminders, key boundaries and command-specific details. | ▶ NOW / planning | SC-5 — Tampermonkey Expands Commands Into Guided Prompts | SL-6 — Tampermonkey Guided Prompt Helper |

## 3. Scenario And Slice Map

| Scenario | Desired behavior | Done when | Supporting slices |
|---|---|---|---|
| SC-1 — Short Command Works With Boundaries | Short command triggers the right working mode without hidden edit/package/commit permission. | aliases, meaning, expected behavior, boundaries and examples exist | SL-1, SL-2 |
| SC-2 — Honest Critical Review On Demand | Chat challenges plans honestly when asked, including risks and alternatives. | `крит` command has semantics, example and route | SL-4 |
| SC-3 — Current Goal Map Is Openable And Useful | Opening this file shows current state and route to the goal. | snapshot + roadmap + readable scenarios/slices stay updated | SL-3 |
| SC-4 — Generic Action Outcome Is Available | Non-file actions can get a structured outcome summary. | generic action overview exists and does not weaken file-specific `План файл-обновление` | SL-5 |
| SC-5 — Tampermonkey Expands Commands Into Guided Prompts | Script expands command labels into prompts that preserve execution details. | palette, preview/edit, insert, no auto-send, reminders/boundaries/details | SL-6 |

## 4. Work Directions / Slices

Slice = work direction.

A slice starts from needed behavior:
  what should become possible / observable.

A slice turns that behavior into our plan:
  artifacts, actions, steps, checks and visible evidence.

Each slice must show:
  - readable name;
  - supported scenario / needed behavior;
  - work direction;
  - concrete actions or artifacts;
  - visible evidence for completed work;
  - verification target;
  - current status;
  - next step.

| Slice | Needed behavior it enables | Plan / work direction | Status | Next action |
|---|---|---|---|---|
| SL-1 — Reusable Command Semantics Foundation | Commands have stable meaning across projects. | Update reusable workflow/docs/examples so command semantics are explicit. | ✅ DONE / validating | Future command families are tracked by their own slices. |
| SL-2 — Root Command Routing | The root use-case map routes user commands to owner workflows and examples. | Update root use-case rows and example references without embedding long examples. | ✅ DONE / validating | Keep future command examples linked from root map when they become current. |
| SL-3 — Goal Map Format And Living Workstream | User can open the repo file and understand current goal/progress. | Maintain owner/template/example and this living map with snapshot, roadmap, evidence and next action. | ✅ DONE / validating | Keep the map synced after meaningful batches and decisions. |
| SL-4 — Critical Review Command | User can ask the chat not to agree automatically. | Define `крит`, aliases, boundaries, example and root route. | ✅ DONE / validating | Validate through real critical reviews. |
| SL-5 — Generic Action Overview | Meaningful non-file actions can be summarized structurally. | Design generic overview while keeping file-specific `План файл-обновление` separate. | ⬜ DEFERRED / not started | Revisit only after Tampermonkey command projection planning. |
| SL-6 — Tampermonkey Guided Prompt Helper | Short commands become useful guided prompts for long chats. | Build palette + expansion schema with reminders, boundaries and details. | ▶ NOW / planning | Use `planning/workstreams/tampermonkey-command-projection-plan.md` to define MVP command profiles and envelope. |

## 5. Current State Overview

| Area | State | Notes |
|---|---|---|
| Reusable command examples | done | Level 2, plan command and archive source/output examples were added. |
| Level 2 / Key points / `Краткое саммари` | done | Key points are non-fixed compressed mirrors of the detailed answer; `Краткое саммари` owns fixed traceability order. |
| Planning command | done | `планируй` means produce a concrete plan now, not “plan later.” |
| Goal Map owner/example | done / validating | Root owner and root example are added; this file is the active living map. |
| Use-case routing | done / validating | Goal Map and reusable command examples are routed from the root map; keep future command examples linked when they become current. |
| Roadmap/evidence readability | done / validating | DONE items include visible evidence plus expandable details; status synchronization rule now prevents stale `NOW` markers. |
| Goal Map sync command | done / validating | `синх карта` checks the living map, outputs target-state brief, archive and apply/diff commands. |
| Critical-thinking command | done / validating | `крит` has response workflow semantics, root route and reusable command example. |
| Generic Action Overview | deferred | File/docs/code/archive updates are covered by `План файл-обновление`; revisit generic non-file overview later. |
| Tampermonkey MVP | now / planning | Need command projection profiles, envelope shape, preview/edit and insert behavior. |

## 6. Detailed Scenario Definitions

### SC-1 — Short Command Works With Boundaries

Desired behavior:
  User writes commands like `планируй`, `арх`, `давай архив`, `крит`, `прогресс`; chat understands mode, boundaries and output.

Acceptance:
  - Command has aliases.
  - Command has meaning.
  - Command has expected behavior.
  - Command has does-not-mean boundary.
  - Ambiguous or dangerous commands have examples or explicit deferred reason.
  - Command does not imply edit/package/commit permission unless explicitly defined.

Status:
  done / validating for current command set; future command families are tracked by their own slices.

### SC-2 — Honest Critical Review On Demand

Desired behavior:
  User asks for honest evaluation; chat treats the user option as a hypothesis.

Acceptance:
  - Chat states strengths.
  - Chat states weaknesses.
  - Chat identifies hidden assumptions and risks.
  - Chat compares alternatives.
  - Chat gives honest verdict.

Status:
  done / validating.

### SC-3 — Current Goal Map Is Openable And Useful

Desired behavior:
  User opens this file or asks `карта цели`, `где мы`, `прогресс`; chat shows current goal, current focus, slices, decisions and next action.

Acceptance:
  - Current Snapshot is present and updated.
  - Roadmap appears immediately after the snapshot.
  - Whole Picture section gives the result landscape.
  - Scenario/slice statuses are readable.
  - DONE items contain visible evidence and details.
  - Next action is explicit.
  - Open decisions are visible.
  - Current Snapshot, roadmap phase statuses and detailed slice statuses do not contradict each other.

Status:
  done / validating.

### SC-4 — Generic Action Outcome Is Available

Desired behavior:
  After meaningful non-file work, user can ask for an action-level overview.

Acceptance:
  - Generic action overview does not break file-specific `План файл-обновление`.
  - Overview states action, status, scope, what changed, why, checked/not checked, owner and next action.

Status:
  deferred / not started.

### SC-5 — Tampermonkey Expands Commands Into Guided Prompts

Desired behavior:
  User selects or types a short command in a browser command palette. Script shows an expanded editable prompt with execution reminders, key boundaries and command-specific details, then inserts it into chat.

Acceptance:
  - Command palette opens.
  - Commands are searchable by alias.
  - Expansion preview is visible before insert.
  - User can add target/context.
  - Expansion includes execution reminders and key boundaries.
  - No auto-send by default.
  - Script is prompt helper, not source of truth.

Status:
  ▶ NOW / planning.

## 7. Detailed Slice Definitions

### SL-1 — Reusable Command Semantics Foundation

Covers:
  - SC-1
  - SC-2
  - SC-3
  - SC-4

Work direction:
  Make reusable command behavior explicit through owner workflows, examples and boundaries.

Artifacts / actions:
  - `planning/documentation/reviewable-agent-output-and-commands-workflow.md`
  - `planning/documentation/examples/README.md`
  - reusable command examples

Steps:
  - ✅ DONE — Clarify Level 2 / Key points / `Краткое саммари`.
  - ✅ DONE — Clarify `планируй` as immediate planning.
  - ✅ DONE — Add reusable command examples.
  - ✅ DONE — Add critical-thinking command `крит`.
  - ↪ FOLLOW-UP — Generic Action Overview is tracked by SL-5, not as current SL-1 work.

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
  done / validating.

### SL-2 — Root Command Routing

Covers:
  - SC-1
  - SC-3

Work direction:
  Keep root use-case map as the concrete route table from user-visible commands to owners, examples and output expectations.

Artifacts / actions:
  - `planning/planning-use-case-map.md`
  - Enman-specific command example under project-specific examples

Steps:
  - ✅ DONE — Route Goal Map commands.
  - ✅ DONE — Route reusable command examples from root use-case map.
  - ✅ DONE — Add Enman-specific scenario/domain/slice command example.
  - ✅ DONE — Keep long examples outside the root use-case map.

Acceptance criteria:
  - Use-case map remains router.
  - Rows link to owner workflow and example files when relevant.
  - No duplicated long workflow/example content is embedded in rows.
  - Source mode and output mode remain separated.
  - Planning/replanning commands consult the active Goal Map when the work belongs to an active workstream.

Verification:
  - Diff review of `planning/planning-use-case-map.md`.
  - Check that examples remain demonstration-only.
  - Check that planning use cases reference the Goal Map obligation.

Status:
  done / validating.

### SL-3 — Goal Map Format And Living Workstream

Covers:
  - SC-3

Work direction:
  Maintain a living, current-state-readable map that lets the user and future chats understand the goal, route, evidence, decisions and next action.

Artifacts / actions:
  - `planning/goal-map-principles-workflow-template.md`
  - `planning/goal-map-example.md`
  - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`

Steps:
  - ✅ DONE — Define the map as mini-architecture of a goal.
  - ✅ DONE — Separate owner file from static example file.
  - ✅ DONE — Route map commands from root use-case map.
  - ✅ DONE — Create active living workstream map in hybrid format.
  - ✅ DONE — Improve roadmap/evidence readability and planning reference rules.
  - ✅ DONE — Validate on planning/update pass and add status synchronization cleanup.
  - ✅ DONE — Add `синх карта` command and synchronized target-state brief/archive rule.

Visible evidence:
  - Changed:
    - `planning/goal-map-principles-workflow-template.md`
    - `planning/planning-use-case-map.md`
    - `planning/documentation/examples/README.md`
    - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
  - Added:
    - `planning/documentation/examples/GOAL-MAP-SYNC-COMMAND-EXAMPLE.md`
  - Recorded:
    - `planning/documentation-action-log.md`

Acceptance criteria:
  - Owner file defines principles, format, workflow and update rules.
  - Static example demonstrates target scenarios and process slices.
  - Living map shows snapshot, roadmap, whole picture, scenario/slice status, decision points and next action.
  - DONE items have visible evidence and expandable details.
  - User can ask `где мы` and get a useful current-state answer from the living map.
  - Planning inside this workstream consults the living map and updates it when the work changes state.

Verification:
  - Use this file for the next few command-system/Tampermonkey decisions.
  - Confirm that updates are easy and do not require rewriting the whole map.
  - Confirm another chat can open the map and continue without reconstructing chat history.

Status:
  done / validating.

### SL-4 — Critical Review Command

Covers:
  - SC-2

Work direction:
  Add command behavior for honest critique instead of automatic agreement.

Artifacts / actions:
  - `planning/documentation/reviewable-agent-output-and-commands-workflow.md`
  - `planning/planning-use-case-map.md`
  - `planning/documentation/examples/CRITICAL-REVIEW-COMMAND-EXAMPLE.md`
  - `planning/documentation/examples/README.md`
  - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
  - `planning/documentation-action-log.md`

Steps:
  - ✅ DONE — Define canonical command `крит`.
  - ✅ DONE — Define aliases: `критически`, `критически оцени`, `проверь критически`, `оцени честно`, `не соглашайся автоматически`, `за и против`, `critical review`.
  - ✅ DONE — Define expected behavior and does-not-mean boundary.
  - ✅ DONE — Add reusable valid example.
  - ✅ DONE — Route command from use-case map.
  - ✅ DONE — Update living Goal Map state and evidence.

Visible evidence:
  - Changed:
    - `planning/documentation/reviewable-agent-output-and-commands-workflow.md`
    - `planning/planning-use-case-map.md`
    - `planning/documentation/examples/README.md`
    - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
  - Added:
    - `planning/documentation/examples/CRITICAL-REVIEW-COMMAND-EXAMPLE.md`
  - Recorded:
    - `planning/documentation-action-log.md`

Acceptance criteria:
  - Chat evaluates user proposal as hypothesis, not accepted truth.
  - Chat states strengths, weaknesses, hidden assumptions, risks and alternatives.
  - Chat gives honest verdict.
  - Chat does not disagree just to disagree.
  - Chat does not edit files, create archives or reopen accepted decisions unless asked.

Verification:
  - Ask `крит` on a real plan and check whether the answer challenges weak assumptions.
  - Check that `planning/planning-use-case-map.md` routes `крит` as a response modifier command.
  - Check that the reusable example demonstrates expected behavior and boundaries.

Status:
  done / validating.

### SL-5 — Generic Action Overview / `Итог действия`

Covers:
  - SC-4

Work direction:
  Create a generic action/change overview that can summarize meaningful non-file actions without replacing File Update Overview.

Artifacts / actions:
  - future action overview owner file or workflow section
  - generic action overview example
  - root use-case route
  - relationship note to File Update Overview

Steps:
  - ⬜ TODO — Design generic action overview format.
  - ⬜ TODO — Keep File Update Overview / file-specific `План файл-обновление` as specialization.
  - ⬜ TODO — Add reusable example.
  - ⬜ TODO — Route command/use-case rows.
  - ⬜ TODO — Verify on a non-file decision or critical review.

Acceptance criteria:
  - Generic overview works for meaningful non-file actions.
  - File-specific `План файл-обновление` remains clear and unchanged.
  - Clear use / do-not-use rules exist.
  - Output helps a later chat continue.

Verification:
  - Use after a non-file command-system decision.

Status:
  next candidate / not started.

### SL-6 — Tampermonkey Guided Prompt Helper

Covers:
  - SC-5

Work direction:
  Build a prompt helper that expands short commands into editable prompts with execution reminders, key boundaries and command-specific details.

Artifacts / actions:
  - `planning/workstreams/tampermonkey-command-projection-plan.md`
  - `tools/tampermonkey/README.md`
  - `tools/tampermonkey/IMPLEMENTATION-NOTES.md`
  - `tools/tampermonkey/chat-command-palette.user.js`
  - optional project profile config

Steps:
  - ✅ DONE — Define MVP command list.
  - ✅ DONE — Define command expansion schema with execution reminders and key boundaries.
  - ✅ DONE — Decide MVP storage: inline userscript vs config/profile.
  - ✅ DONE — Document inserted command bodies for MVP-1 and MVP-2.
  - ✅ DONE — Create implementation docs entrypoint and implementation notes.
  - ✅ DONE — Plan list-only draggable widget visual behavior.
  - ✅ DONE — Convert documented command bodies into script profile data.
  - ✅ DONE — Build list-only draggable command widget UI.
  - ⬜ DEFERRED — Add preview/edit before insert.
  - ✅ DONE — Insert expanded prompt into ChatGPT input.
  - ▶ NOW — Manual browser test / smoke testing.
  - ⬜ TODO — Decide whether project profile config is needed after MVP.

Acceptance criteria:
  - User can pick a command.
  - User can edit expanded prompt.
  - Prompt inserts into chat input.
  - No auto-send by default.
  - Expansion includes command-specific execution reminders and key boundaries.
  - Script does not become source of truth.
  - Reusable defaults can later be extended by project-specific profiles.

Verification:
  - Manual Tampermonkey test in browser.

Status:
  ▶ NOW / planning.

## 8. Decision Points

| ID | Question | Options | Current decision | Affects |
|---|---|---|---|---|
| DEC-1 | Next slice after FU1/GMS1 map sync? | A: F7-CMD-4 Generic Action Overview; B: TM-0 Tampermonkey MVP planning | B selected; Generic deferred | SL-5, SL-6 |
| DEC-2 | Where should Tampermonkey commands live? | A: inline MVP; B: reusable defaults + project profile; C: external JSON/raw URL | A for MVP, B as later-compatible design | SL-6 |
| DEC-3 | Generic Action Overview placement? | A: separate workflow/template; B: response workflow section | likely A if it becomes a real generic counterpart to File Update Overview | SL-5 |
| DEC-4 | How much of use-case map should Tampermonkey expansion repeat? | A: copy detailed docs; B: concise prompt reminders; C: command ID only | B preferred, but expansions must include execution reminders/key boundaries/details | SL-6 |
| DEC-5 | How much evidence belongs in DONE item details? | A: file list only; B: evidence details per item; C: full diff copies | B preferred | SL-3, all future roadmap records |
| DEC-6 | Which commands should Tampermonkey MVP include first? | A: high-risk commands only; B: all use-case map commands; C: phased MVP-1/MVP-2 | C preferred | SL-6 |

## 9. Current Focus

SL-6 — Tampermonkey helper smoke testing and command-list validation:
  - Goal Map maintenance is now discoverable from root onboarding, workflow activation and responsibility routing;
  - Tampermonkey command bodies are documented as prompt projections, not command authority;
  - `planning/planning-use-case-map.md` and linked owner workflows remain source of truth;
  - ordinary `давай архив` stays on saved-diff-to-clipboard review transfer;
  - `давай архив с review diff file` is available as an explicit-only helper command profile;
  - current near-term work is helper smoke testing and small command-list cleanup;
  - keep Generic Action Overview deferred.

Current active slice:
  - SL-6 — Tampermonkey Guided Prompt Helper

## 10. Next Action

Continue helper smoke testing after the discovery-sync update:

```text
1. Confirm ordinary `давай архив` still inserts clipboard-diff default reminders.
2. Confirm explicit `давай архив с review diff file` appears separately and inserts repo-stored review-diff reminders.
3. Confirm `кц`, `синх карта` and `вспомни структуру репо` still point to route/owner files.
4. Continue scroll / MVP-1 / MVP-2 / empty-composer / existing-text / no-auto-send tests.
5. Do not implement helper-owned compose buffer or selected-command stack yet.
```

Recommended next action:
  Finish smoke testing before adding preview, search, external profile loading or deferred buffer/stack features.

## Source Delta / Change Log

```text
- SRC-CMD-1B added Tampermonkey helper profiles for source/version maintenance commands and the explicit `положняк` current-state command; bumped this living map to Doc version: v0.2.0.
```
