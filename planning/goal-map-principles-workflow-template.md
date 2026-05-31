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

A process slice is not just a task. It is a verifiable part of the goal that enables one or more target scenarios.

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
goal process
goal map
```

Meaning:

```text
Produce, review or update the Goal Map for the active or named long-running goal.
```

The command does not grant permission to edit files, create archives, commit changes or change repository state.

If the active goal is unclear, ask for the goal or mark assumptions explicitly. Do not invent a final picture silently.

If a previous map exists in the conversation or repo, preserve its accepted structure and update statuses, decision points, steps and next action instead of recreating the map from scratch.

## 5. Required Map Content

A useful map should include:

```text
Final picture
  clear end state / desired capability.

Target scenarios
  observable behaviors or states that define the final picture.

Process slices
  verifiable parts of the work that enable scenarios.

Acceptance criteria
  checks that prove a scenario or slice is ready.

Current state
  where the work is now relative to the final picture.

Invariants
  constraints that must stay true while moving toward the goal.

Decision points
  places where route, order, implementation or ownership must be chosen.

Current focus
  the scenario/slice/step being worked now.

Next action
  the next concrete action.
```

## 6. Default Format

```text
## Карта цели

Финальная картина:
  <what should exist / work / be possible at the end>

Целевые сценарии:
  SC-1 — <scenario name>
    Поведение:
      <observable behavior or target state>
    Acceptance criteria:
      - <checkable criterion>
    Status:
      not-started / planned / in-progress / done / verified

Слайсы процесса:
  SL-1 — <slice name>
    Покрывает сценарии:
      - SC-...
    Артефакты / действия:
      - <file / script / command / decision / artifact / action>
    Шаги:
      1. [ ] <step>
      2. [ ] <step>
    Acceptance criteria:
      - <slice readiness check>
    Проверка:
      - <manual check / diff check / example run / test / review>
    Status:
      not-started / planned / in-progress / done / verified

Текущее состояние:
  Где мы:
    <current position relative to scenarios and slices>

  Готово:
    - [x] <completed scenario/slice/step>

  Не готово:
    - [ ] <missing scenario/slice/step>

Инварианты:
  - <constraint that must remain true>

Точки выбора:
  DEC-1 — <decision name>
    Вопрос:
      <what must be decided>
    Влияет на:
      - SC-...
      - SL-...
    Варианты:
      A. <option>
      B. <option>
    Критерии выбора:
      - <criterion>
    Текущее решение:
      chosen / deferred / needs critical review

Текущий фокус:
  <active scenario/slice/step>

Следующее действие:
  <one concrete next action>
```

The map can be shortened for small goals, but it must preserve the core relationships: final picture -> scenarios -> slices -> acceptance checks -> current state -> next action.

## 7. Workflow

When building a Goal Map:

```text
1. Identify the active or named goal.
2. Define the final picture in observable terms.
3. Split the final picture into target scenarios / desired behaviors.
4. Add acceptance criteria for each scenario.
5. Derive process slices that can be completed and verified separately.
6. For each slice, list artifacts/actions, steps, checks and status.
7. Add invariants that must not be violated.
8. Add decision points where route/order/implementation/ownership is not obvious.
9. Mark current focus and next action.
10. On later updates, preserve accepted structure and update the map instead of restarting.
```

## 8. Update Rules

```text
When user says `где мы` / `прогресс`:
  show current state, completed items, missing items, current focus and next action.

When user says `карта процесса` / `карта цели`:
  produce or refresh the full Goal Map.

When user says `обнови карту процесса` / `обнови карту цели`:
  update statuses, steps, decisions and next action using the latest accepted work.

When user says `крит, карта процесса`:
  evaluate final picture, scenarios, slices, acceptance criteria and decision points honestly.

When user says `планируй` inside an active goal:
  use the map to choose the next slice/step and explain the batch boundary.
```

Do not hide uncertainty. If a scenario, slice, status or acceptance criterion is inferred rather than established, mark it as inferred.

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
- Do not silently drop accepted scenarios, slices, invariants or decision points during updates.
- Do not treat map commands as permission to edit files, create archives, commit or push.
- Do not use a flat todo list when scenario/slice structure is needed for verification.
```

## 11. Living Goal Maps

Static examples and active living maps are different.

```text
Static example
  Demonstrates a valid shape.
  Does not change during ordinary work.

Living goal map
  Tracks a real active goal/workstream.
  Can be updated as statuses, slices, decisions and next actions change.
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

When updating a living map, preserve accepted final picture, scenarios, slices, invariants and decision points unless the user explicitly changes them. Update statuses, steps, current focus and next action instead of recreating the map from scratch.

## 12. Related Example

Example:

```text
planning/goal-map-example.md
```

The example demonstrates the command-system and Tampermonkey helper goal. It is demonstration-only and does not own this workflow.
