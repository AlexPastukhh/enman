# Goal Map Example

Status: current root goal-map example / demonstration-only  
Doc version: v0.2.0  
Scope: demonstrates the current Goal Map shape for the command-system and Tampermonkey helper goal

Owner:

```text
planning/goal-map-principles-workflow-template.md
```

This file demonstrates valid use only. It does not own command semantics, routing, source truth, source-cascade rules, output modes, permission boundaries or the current workstream state.

Current living workstream example:

```text
planning/workstreams/command-system-and-tampermonkey-goal-map.md
```

## Example Map

````markdown
# Command System And Tampermonkey Goal Map

Status: active workstream goal map  
Doc version: v0.1.0  
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

## 0. Current Snapshot

Last updated:
  2026-05-31 / example snapshot

Current goal:
  Build a transferable command system and Tampermonkey prompt helper for long-running chats across projects.

Current focus:
  SL-3 — Goal Map Format And Living Workstream is validating the map format and planning-reference behavior.

Active slice:
  SL-3 — Goal Map Format And Living Workstream
    Status: validating / being improved

Already available:
  - Reusable command examples exist.
  - `планируй` is defined as immediate planning, not “plan later.”
  - Goal Map owner, static example and living workstream exist.
  - Goal Map route exists in the root use-case map.
  - Living map has a Current Snapshot.
  - Roadmap evidence format uses status labels and `<details>` blocks instead of markdown task-list checkboxes.

Latest completed:
  - Added root Goal Map owner/example.
  - Added active living workstream map.
  - Added roadmap/evidence rules so completed work can show visible proof and explanatory details.
  - Added planning-reference rules requiring the agent to consult the active living map for workstream planning.

Next action:
  Choose next slice:
    A. F7-CMD-1B — route reusable command examples from root use-case map.
    B. F7-CMD-2 — add critical-thinking command `крит`.
    C. TM-0 — plan Tampermonkey MVP.

Recommended next action:
  F7-CMD-1B, because it closes the already-open examples/routing chain.

Open decisions:
  - DEC-1: next slice after living map readability/evidence update.
  - DEC-2: Tampermonkey command storage.
  - DEC-3: Generic Action Overview placement.
  - DEC-4: how much use-case map detail Tampermonkey expansion should repeat.
  - DEC-5: final Generic Action / Change Overview format for DONE item details.

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
| Phase 1 — Command foundation | Short commands work safely and consistently across projects. | SL-1 — Reusable Command Semantics Foundation; SL-2 — Root Command Routing | User command can be traced to owner workflow/example/boundary without hidden edit/package/commit permission. | ▶ NOW |
| Phase 2 — Goal navigation | User can open repo state and understand current goal/progress/path. | SL-3 — Goal Map Format And Living Workstream | Living map has snapshot, roadmap, readable scenarios/slices, next action, open decisions and visible evidence. | ▶ NOW |
| Phase 3 — Critical thinking | User can ask for honest evaluation instead of automatic agreement. | SL-4 — Critical Review Command | `крит` command has semantics, aliases, example, route and honest-verdict behavior. | ⏭ NEXT |
| Phase 4 — Generic action outcomes | Meaningful non-file actions become reviewable. | SL-5 — Generic Action Overview | Non-file action can be summarized structurally without weakening file-specific `Итог`. | ⬜ TODO |
| Phase 5 — Guided command input | User can issue short commands without long chat forgetting execution details. | SL-6 — Tampermonkey Guided Prompt Helper | Expansion includes command label, execution reminders, key boundaries and command-specific details. | ⬜ TODO |

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
- That is separate work in SL-2.

</details>

##### ⏭ NEXT — Route reusable command examples from root use-case map

Expected result:
  Relevant command rows reference reusable command example files.

Evidence required:
  - Changed:
    - `planning/planning-use-case-map.md`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Why this is next</summary>

**Why this matters**

- The command examples chain is not fully closed until root routes reference the examples.
- This keeps root use-case map as a router while avoiding embedded long example content.

**How to verify later**

- Open `planning/planning-use-case-map.md`.
- Confirm repeated/continuation command rows point to relevant reusable example files.
- Confirm the action log has an append-only entry for the route update.

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

- It does not prove every future map update will be done correctly.
- That is governed by the owner workflow and future review.

</details>

##### ✅ DONE — Add readable roadmap and proof-oriented DONE details

Result:
  File shows a clear roadmap before detailed scenario/slice sections, and completed work has visible proof with dropdown details.

Visible evidence:
  - Changed:
    - `planning/workstreams/command-system-and-tampermonkey-goal-map.md`
    - `planning/goal-map-principles-workflow-template.md`
    - `planning/planning-use-case-map.md`
  - Visible sections:
    - `## 1. Roadmap / Дорожная карта`
    - `## 4. Work Directions / Slices`
  - Recorded:
    - `planning/documentation-action-log.md`

<details>
<summary>Evidence details</summary>

**What changed**

- Roadmap was placed immediately after Current Snapshot.
- Status labels replaced markdown task-list checkboxes.
- Readable phase/slice/scenario names were added.
- Evidence dropdowns with bold mini-headings were added.
- Planning-reference rules were added so `планируй` uses the living Goal Map inside an active workstream.

**Visible result**

- The living map starts with snapshot, then roadmap, then whole picture and scenario/slice mapping.
- DONE items use `Result`, `Visible evidence` and expandable details.
- NEXT items use `Expected result` and `Evidence required`.

**Why this evidence exists**

- The user needs to understand what is happening in the work without reconstructing context from the chat.
- Completed work should show proof artifacts and verification paths, not only “done”.

**How to verify**

- Open the living map.
- Confirm Roadmap is directly after Snapshot.
- Confirm DONE items show Visible evidence and details.
- Confirm NEXT items show Evidence required.
- Confirm slices are described as work directions from needed behavior to plan/actions/verification.

**What this evidence does not prove**

- It does not prove the format is final.
- It should be validated on the next planning/update pass.

</details>

## 2. Whole Picture / Картина целиком

| Result area | What should be possible | Current state | Scenarios | Work directions / slices |
|---|---|---|---|---|
| Reliable command understanding | User writes short commands and the chat understands mode, boundaries, expected output and permissions. | partially implemented | SC-1 — Short Command Works With Boundaries | SL-1 — Reusable Command Semantics Foundation; SL-2 — Root Command Routing |
| Honest critical review | User can ask the chat to evaluate a plan or idea as a hypothesis, not as accepted truth. | planned | SC-2 — Honest Critical Review On Demand | SL-4 — Critical Review Command |
| Goal navigation | User can open this repo file and immediately see the current goal, progress, active slice, next action and decisions. | validating / being improved | SC-3 — Current Goal Map Is Openable And Useful | SL-3 — Goal Map Format And Living Workstream |
| Generic action outcome | User can get a structured outcome for meaningful non-file actions without misusing file-specific `Итог`. | planned | SC-4 — Generic Action Outcome Is Available | SL-5 — Generic Action Overview |
| Guided command prompts | Tampermonkey expands short commands into editable prompts with execution reminders, key boundaries and command-specific details. | planned | SC-5 — Tampermonkey Expands Commands Into Guided Prompts | SL-6 — Tampermonkey Guided Prompt Helper |

## 3. Scenario And Slice Map

| Scenario | Desired behavior | Done when | Supporting slices |
|---|---|---|---|
| SC-1 — Short Command Works With Boundaries | Short command triggers the right working mode without hidden edit/package/commit permission. | aliases, meaning, expected behavior, boundaries and examples exist | SL-1, SL-2 |
| SC-2 — Honest Critical Review On Demand | Chat challenges plans honestly when asked, including risks and alternatives. | `крит` command has semantics, example and route | SL-4 |
| SC-3 — Current Goal Map Is Openable And Useful | Opening this file shows current state and route to the goal. | snapshot + roadmap + readable scenarios/slices stay updated | SL-3 |
| SC-4 — Generic Action Outcome Is Available | Non-file actions can get a structured outcome summary. | generic action overview exists and does not weaken file-specific `Итог` | SL-5 |
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
| SL-1 — Reusable Command Semantics Foundation | Commands have stable meaning across projects. | Update reusable workflow/docs/examples so command semantics are explicit. | partially implemented | Add `крит`; later add generic action overview command. |
| SL-2 — Root Command Routing | The root use-case map routes user commands to owner workflows and examples. | Update root use-case rows and example references without embedding long examples. | in progress | Route reusable command examples and add Enman-specific scenario/domain/slice example. |
| SL-3 — Goal Map Format And Living Workstream | User can open the repo file and understand current goal/progress. | Maintain owner/template/example and this living map with snapshot, roadmap, evidence and next action. | validating / being improved | Validate readability and update structure if needed. |
| SL-4 — Critical Review Command | User can ask the chat not to agree automatically. | Define `крит`, aliases, boundaries, example and root route. | planned | Design command semantics and example. |
| SL-5 — Generic Action Overview | Meaningful non-file actions can be summarized structurally. | Design generic overview while keeping file-specific `Итог` separate. | planned | Design owner/format/example. |
| SL-6 — Tampermonkey Guided Prompt Helper | Short commands become useful guided prompts for long chats. | Build palette + expansion schema with reminders, boundaries and details. | planned | Define MVP command list and expansion schema. |

## 5. Decision Points

| ID | Question | Options | Current decision | Affects |
|---|---|---|---|---|
| DEC-1 | Next slice after living map readability/evidence update? | A: route examples; B: add `крит`; C: plan Tampermonkey MVP | A recommended | SL-2, SL-4, SL-6 |
| DEC-2 | Where should Tampermonkey commands live? | A: inline MVP; B: reusable defaults + project profile; C: external JSON/raw URL | A for MVP, B as later-compatible design | SL-6 |
| DEC-3 | Generic Action Overview placement? | A: separate workflow/template; B: response workflow section | likely A if it becomes a real generic counterpart to File Update Overview | SL-5 |
| DEC-4 | How much of use-case map should Tampermonkey expansion repeat? | A: copy detailed docs; B: concise prompt reminders; C: command ID only | B preferred, but expansions must include execution reminders/key boundaries/details | SL-6 |
| DEC-5 | How much evidence belongs in DONE item details? | A: file list only; B: evidence details per item; C: full diff copies | B preferred | SL-3, all future roadmap records |

## 6. Next Action

Choose the next workstream slice:

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
````

## Why This Example Is Valid

```text
- It starts with Current Snapshot so the current state is immediately visible.
- It places Roadmap directly after the snapshot.
- It uses status labels instead of markdown task-list checkboxes so completed items stay readable.
- It defines scenarios as desired behaviors.
- It defines slices as work directions from needed behavior to plan/actions/verification/evidence.
- It uses Visible evidence for DONE work and Evidence required for NEXT work.
- It uses `<details>` blocks with bold mini-headings for proof details.
- It keeps the example demonstration-only and points to the owner file for the actual rules.
- It includes decision points and next action.
```
