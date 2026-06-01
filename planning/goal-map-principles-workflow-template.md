# Goal Map Principles / Workflow / Template

Status: current root compound starter artifact / goal-map principles-workflow-template  
Doc version: v0.1.0  
Scope: principles, workflow and template for defining, updating and reviewing a goal-level map for long-running work across chats, files, artifacts, decisions and implementation steps

## 1. Purpose

A Goal Map / `Карта цели` is a compact architecture of a goal and its process of achievement.

It shows:

```text
- what should exist or work in the final result;
- which target scenarios / desired behaviors define that result;
- which process slices implement or achieve those behaviors;
- what steps and substeps lead to each slice;
- which acceptance criteria prove each scenario or slice is ready;
- where the current work is now;
- which decision points still need a choice;
- what the next action is.
```

It is meant for long-running work where a chat can otherwise lose track of the result, the current position, the route and the verification criteria.

This file owns the root goal-map principles, workflow and template. It does not replace specialized project/domain/slice workflows, command workflows, source registers, PMR or action logs.

## 1A. Compound Starter Artifact

This file is a compound starter artifact. It intentionally combines:

```text
- principles / invariants;
- workflow / process;
- template / exact output shape.
```

Use this combined form while the concept is early and has one cohesive owner. Keep examples in separate files so this owner file does not become too large.

Split this file later only when the concept stabilizes, becomes too large, or its sections get different owners, review cadence or reuse boundaries.

## 2. Mental Model

Goal Map transfers project-architecture thinking to any long-running goal.

```text
Project architecture:
  scenario -> behavior items -> domain/slice -> implementation -> tests

Goal map:
  target scenario -> required behavior/outcome -> process slice -> action/artifact -> acceptance check
```

The final picture is not a vague wish. It is a set of observable target scenarios.

A process slice is not just a task. It is a work direction: it starts from needed behavior and turns that behavior into our plan.

```text
Slice / work direction:
  needed behavior -> plan -> artifacts/actions/steps -> verification -> visible evidence
```

Acceptance criteria should be attached to scenarios and slices so the chat can check whether the work is done instead of only saying that progress was made.

## 3. Relationship To Existing Response Blocks

```text
Key points
  compressed mirror of the detailed answer.

Краткое саммари
  current-answer conclusion, next actions, goal understood, context and boundaries.

File Update Overview / Итог
  file/change/update-specific overview.

Goal Map / Карта цели
  final picture, target scenarios, process slices, current state, steps, acceptance criteria and decision points for a long-running goal.
```

Do not use Goal Map as a generic summary. Use it when the work has a continuing goal, multiple steps, multiple possible paths or scenario/slice-like behavior to achieve.

## 4. Commands

Canonical command family:

```text
карта процесса
карта цели
план процесса
где мы
прогресс
статус цели
кц
карта цели кратко
goal process
goal map
goal map brief
```

Meaning:

```text
Produce, review or update the Goal Map for the active or named long-running goal.
```

Brief command meaning:

```text
`кц`, `карта цели кратко` and `goal map brief` produce a compact in-answer projection of the living Goal Map. They do not replace or update the durable living map by themselves.
```

The command does not grant permission to edit files, create archives, commit changes or change repository state.

If the active goal is unclear, ask for the goal or mark assumptions explicitly. Do not invent a final picture silently.

If a previous map exists in the conversation or repo, preserve its accepted structure and update statuses, decision points, steps and next action instead of recreating the map from scratch.

## 5. Required Map Content

A useful map should include:

```text
Current Snapshot
  current goal, focus, active slice, latest completed, next action and open decisions.

Roadmap
  phases, work directions/slices, phase verification and evidence-backed progress.

Whole Picture
  compact view of result areas, scenarios and slices.

Target scenarios
  observable behaviors or states that define the final picture.

Process slices / work directions
  verifiable work directions that enable scenarios.

Acceptance criteria
  checks that prove a scenario or slice is ready.

Current state
  where the work is now relative to the final picture.

Visible evidence
  artifacts, changed files, changed sections, route entries and action-log records proving completed work.

Invariants
  constraints that must stay true while moving toward the goal.

Decision points
  places where route, order, implementation or ownership must be chosen.

Current focus
  the scenario/slice/step being worked now.

Next action
  the next concrete action.
```

## 5A. Living Map Planning Reference Rule

When planning inside a long-running workstream that has a living Goal Map, the agent must consult the map before choosing a plan.

The planning answer should use the map to:

```text
- identify the active goal and current focus;
- choose the relevant scenario/slice/work direction;
- preserve accepted decisions and invariants;
- explain how the proposed plan advances the roadmap;
- state whether the map needs to be updated after the work;
- avoid inventing a new plan that ignores current progress.
```

If the user says `планируй`, `следующий шаг`, `что дальше`, `где мы`, `прогресс`, or asks for planning inside the active workstream, use the living map when it exists.

If the map is stale or incomplete, say that explicitly and update it in the same batch when the user asks to change repo files. If no edit/package permission exists, state what map update is needed.

## 5B. Roadmap And Evidence Rules

Living maps should use a roadmap immediately after `Current Snapshot` when the goal has multiple phases or work directions.

Roadmap records should use status labels instead of markdown task-list checkboxes when completed items must remain readable:

```text
✅ DONE     completed and backed by visible evidence
▶ NOW      active work
⏭ NEXT     next planned work
⬜ TODO     planned but not active
⚠ BLOCKED  blocked or needs a decision
```

DONE items must not be only “done”. They should be reviewable records.

A DONE item should include:

```text
Result
  what became possible / what visible outcome exists now

Visible evidence
  added files, renamed paths, changed files, changed sections,
  route entries, examples, action-log entries or commit/diff references

Evidence details dropdowns
  per-evidence explanation: what changed, visible result, why this evidence exists,
  how to verify and what the evidence does not prove

Action / change overview dropdown
  overall summary of what was done, why, goal impact, verification and remaining gaps
```

Do not copy full diffs into the Goal Map unless the diff is small and directly needed for understanding. Prefer short meaningful evidence descriptions and point to files/sections that can be opened for detailed review.

## 5C. Evidence Details Format

Top-level evidence should be a quick proof index:

```text
Added:
  - <file>
Renamed:
  - <old path> -> <new path>
Changed:
  - <file>
Changed sections:
  - <file>#<section or visible block>
Recorded:
  - <action-log entry>
```

Each top-level evidence item may include its own `<details>` block.

Inside evidence `<details>`, section labels must be visually distinct. Use bold mini-headings, not plain label lines:

```markdown
<details>
<summary>Evidence details</summary>

**What changed**

- ...

**Visible result**

- ...

**Why this evidence exists**

- ...

**How to verify**

- ...

**What this evidence does not prove**

- ...

</details>
```

The DONE item itself may include a final `<details>` block for the action/change overview:

```markdown
<details>
<summary>Action / change overview</summary>

**What was done**

- ...

**Why it was done**

- ...

**Goal impact**

- ...

**Verification**

- ...

**Remaining gaps**

- ...

</details>
```

## 5D. In-Answer Goal Map Brief

`Goal Map Brief` / `Карта цели` is a compact response projection of the living Goal Map.

Use it when a user asks for `кц`, `карта цели кратко` or `goal map brief`, or when a Level 2/3 answer is planning, status, continuation or next-step work inside an active long-running workstream.

The brief is not the durable map itself. The living map remains the current-state source.

Before output, check:

```text
- Current Snapshot;
- active/current slice;
- roadmap / slice status rows;
- latest completed work;
- next action;
- open decisions if they affect the current slice.
```

The brief must use this shape:

```text
Goal
  current goal

Current slice
  current slice name and status

Current slice chain
  Why now
  Done
  Now
  Next
  After

Other slices
  compact status table only
```

Rules:

```text
- Current slice is expanded.
- Other slices are status-only.
- Do not use `<details>` / collapsible blocks; they are unstable in chat output.
- Do not copy the full living map into a normal answer.
- If the living map is stale or was not checked, say so.
- If the response completes work or changes next action, state whether the living map needs an update.
```

Example:

```text
planning/documentation/examples/GOAL-MAP-BRIEF-RESPONSE-EXAMPLE.md
```

## 6. Default Format

```text
## 0. Current Snapshot

Current goal:
  <active goal>

Current focus:
  <active scenario/slice/step>

Already available:
  - <visible capability or artifact>

Latest completed:
  - <latest completed work>

Next action:
  <next action>

Open decisions:
  - <decision>

## 1. Roadmap / Дорожная карта

| Phase | Phase goal | Work directions / slices | Phase verification | Status |
|---|---|---|---|---|
| Phase 1 — <name> | <goal> | <slices> | <verification target> | <status> |

### Phase 1 — <name>

Goal:
  <what must become true>

Needed behavior:
  <what should be possible / observable>

Work directions / slices:
  - SL-1 — <readable slice name>

Phase verification:
  <how to prove the phase goal is achieved>

#### Progress

##### ✅ DONE — <work item>

Result:
  <what became possible / what exists>

Visible evidence:
  - Changed:
    - <file>

<details>
<summary>Evidence details</summary>

**What changed**

- ...

**Visible result**

- ...

**Why this evidence exists**

- ...

**How to verify**

- ...

**What this evidence does not prove**

- ...

</details>

##### ▶ NOW — <work item>

Expected result:
  <expected outcome>

Evidence required:
  - Changed:
    - <file>
```

The map can be shortened for small goals, but it must preserve the core relationships: final picture -> scenarios -> slices/work directions -> acceptance checks -> roadmap evidence -> current state -> next action.

## 7. Workflow

When building or updating a Goal Map:

```text
1. Identify the active or named goal.
2. Check whether a living Goal Map exists for this workstream.
3. Read the Current Snapshot, Roadmap, Decision Points and Next Action.
4. Define or preserve the final picture in observable terms.
5. Split the final picture into target scenarios / desired behaviors.
6. Add acceptance criteria for each scenario.
7. Derive slices as work directions that turn needed behavior into plan/actions/checks/evidence.
8. For each slice, list artifacts/actions, steps, checks, visible evidence and status.
9. Add or update roadmap phases and phase verification targets.
10. Add invariants that must not be violated.
11. Add decision points where route/order/implementation/ownership is not obvious.
12. Mark current focus and next action.
13. On later updates, preserve accepted structure and update the map instead of restarting.
```

## 8. Update Rules

```text
When user says `где мы` / `прогресс`:
  show current state, completed items, missing items, current focus and next action from the living Goal Map when one exists.

When user says `кц` / `карта цели кратко` / `goal map brief`:
  produce the compact Goal Map Brief in the current answer, using the living Goal Map when one exists.

When user says `карта процесса` / `карта цели`:
  produce or refresh the full Goal Map.

When user says `обнови карту процесса` / `обнови карту цели`:
  update statuses, roadmap evidence, steps, decisions and next action using the latest accepted work.

When user says `крит, карта процесса`:
  evaluate final picture, scenarios, slices, acceptance criteria, evidence and decision points honestly.

When user says `планируй` inside an active goal:
  use the map to choose the next slice/work direction and explain the batch boundary.
```

Do not hide uncertainty. If a scenario, slice, status, evidence item or acceptance criterion is inferred rather than established, mark it as inferred.

## 9. Decision Points

Decision points are not ordinary open questions. They are moments where several valid routes may exist and the chat must choose or defer based on criteria.

Minimal shape:

```text
DEC-1 — <decision name>
  Вопрос:
    <what must be decided>
  Влияет на:
    - <scenario/slice/step>
  Варианты:
    A. <option>
    B. <option>
  Критерии выбора:
    - <criterion>
  Текущее решение:
    chosen / deferred / needs critical review
```

Use decision points when the user is deciding which step is better, where to store an artifact, how broad the next batch should be, whether to split a workflow/template/example, or which implementation route to take.

## 10. Do Not

```text
- Do not treat a Goal Map as a generic conclusion.
- Do not replace detailed answers, file update overviews or source checks with the map.
- Do not invent a final picture when the goal is unclear.
- Do not silently drop accepted scenarios, slices, invariants, evidence or decision points during updates.
- Do not treat map commands as permission to edit files, create archives, commit or push.
- Do not use a flat todo list when scenario/slice structure is needed for verification.
- Do not let the map become a full duplicate of the action log or full diffs.
```

## 11. Living Goal Maps

Static examples and active living maps are different.

```text
Static example
  Demonstrates a valid shape.
  Does not change during ordinary work.

Living goal map
  Tracks a real active goal/workstream.
  Can be updated as statuses, slices, decisions, roadmap evidence and next actions change.
```

Living maps may be stored under:

```text
planning/workstreams/
```

Current active command-system/Tampermonkey workstream map:

```text
planning/workstreams/command-system-and-tampermonkey-goal-map.md
```

A living map must be current-state-readable. It should start with `Current Snapshot` so a user or another chat can open the file and immediately see the current goal, focus, active slice, latest completed action, next action and open decisions.

When updating a living map, preserve accepted final picture, scenarios, slices, invariants and decision points unless the user explicitly changes them. Update roadmap evidence, statuses, steps, current focus and next action instead of recreating the map from scratch.

## 12. Related Example

Example:

```text
planning/goal-map-example.md
```

The example demonstrates the command-system and Tampermonkey helper goal. It is demonstration-only and does not own this workflow.
