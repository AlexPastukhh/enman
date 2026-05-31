# Goal Map Example

Status: current root goal-map example  
Doc version: v0.1.0  
Scope: demonstrates a Goal Map for the command-system and Tampermonkey helper goal

Owner:

```text
planning/goal-map-principles-workflow-template.md
```

This file demonstrates valid use only. It does not own command semantics, routing, source truth, source-cascade rules, output modes or permission boundaries.

## Example Map

```text
## Карта цели

Финальная картина:
  There is a transferable command system and a Tampermonkey command helper.
  A user can give short commands in any long chat, the chat expands them into the right working mode,
  remembers relevant boundaries/details and can show progress toward the goal.

Целевые сценарии:

  SC-1 — Short command is understood
    Поведение:
      User writes commands such as `планируй`, `арх`, `давай архив`, `крит`, `прогресс`.
      Chat understands the intended mode, boundaries and expected output.
    Acceptance criteria:
      - Command has meaning and aliases.
      - Command has does-not-mean boundaries.
      - Command has expected behavior.
      - Dangerous or ambiguous commands have examples or recorded deferred examples.
      - Command does not imply file edit / package / commit permission unless explicitly defined as output mode.
    Status:
      partially implemented.

  SC-2 — Critical thinking can be requested
    Поведение:
      User can ask the chat to evaluate a plan or idea as a hypothesis rather than accept it.
    Acceptance criteria:
      - Chat states strengths.
      - Chat states weaknesses.
      - Chat identifies hidden assumptions and risks.
      - Chat compares alternatives by criteria.
      - Chat gives an honest verdict.
    Status:
      planned.

  SC-3 — User can see the goal map
    Поведение:
      User says `карта процесса`, `карта цели`, `где мы`, `прогресс` or `статус цели`.
      Chat shows final picture, target scenarios, process slices, acceptance criteria, current state, decision points and next action.
    Acceptance criteria:
      - Final picture is clear.
      - Current position is visible.
      - Scenarios and slices show status.
      - Acceptance criteria are checkable.
      - Next action is explicit.
    Status:
      being implemented.

  SC-4 — Generic action outcome can be summarized
    Поведение:
      After meaningful non-file work, user can ask for an action-level overview.
    Acceptance criteria:
      - Generic action overview does not break file-specific `Итог`.
      - Overview states action, status, scope, what changed, why, checked/not checked, owner and next action.
    Status:
      planned.

  SC-5 — Tampermonkey helps expand commands
    Поведение:
      User selects or types a short command in a browser command palette.
      Script shows an expanded prompt with execution reminders, key boundaries and command-specific details,
      allows target/context edits and inserts the prompt into chat input.
    Acceptance criteria:
      - Command palette opens.
      - Commands are searchable by alias.
      - Expansion preview is visible before insert.
      - User can add target/context.
      - Expansion includes execution reminders and key boundaries.
      - No auto-send by default.
      - Script is prompt helper, not source of truth.
    Status:
      planned.

Слайсы процесса:

  SL-1 — Reusable command semantics
    Покрывает сценарии:
      - SC-1
      - SC-2
      - SC-3
      - SC-4
    Артефакты / действия:
      - planning/documentation/reviewable-agent-output-and-commands-workflow.md
      - reusable command examples
    Шаги:
      1. [x] Clarify Level 2 / Key points / Краткое саммари.
      2. [x] Add reusable command execution examples.
      3. [ ] Add critical-thinking command.
      4. [ ] Add Goal Map command semantics.
      5. [ ] Add generic action overview command semantics.
    Acceptance criteria:
      - Commands have aliases, meaning, expected behavior and boundaries.
      - Examples exist for ambiguous commands.
      - Command semantics remain in owner workflows, not examples.
    Проверка:
      - Review workflow sections and examples index.
    Status:
      partially implemented.

  SL-2 — Root use-case routing
    Покрывает сценарии:
      - SC-1
      - SC-3
    Артефакты / действия:
      - planning/planning-use-case-map.md
    Шаги:
      1. [ ] Route reusable command examples from the root map.
      2. [ ] Route Goal Map commands to planning/goal-map-principles-workflow-template.md.
      3. [ ] Keep long examples outside the use-case map.
    Acceptance criteria:
      - Use-case map remains router.
      - Rows link to owner workflow and examples when relevant.
      - No duplicated workflow/example content is embedded in rows.
    Проверка:
      - Diff review of planning/planning-use-case-map.md.
    Status:
      in-progress.

  SL-3 — Goal Map format
    Покрывает сценарии:
      - SC-3
    Артефакты / действия:
      - planning/goal-map-principles-workflow-template.md
      - planning/goal-map-example.md
    Шаги:
      1. [x] Define map as mini-architecture of a goal.
      2. [x] Separate owner file from example file.
      3. [ ] Route commands from use-case map.
      4. [ ] Test the map on this command/Tampermonkey goal.
    Acceptance criteria:
      - Owner file defines principles, format, workflow and update rules.
      - Example file demonstrates target scenarios and process slices.
      - Map includes decision points.
    Проверка:
      - Ask `где мы` and confirm the map shows current state and next action.
    Status:
      in-progress.

  SL-4 — Generic Action Overview
    Покрывает сценарии:
      - SC-4
    Артефакты / действия:
      - future action-overview workflow/template/example
    Шаги:
      1. [ ] Design generic format.
      2. [ ] Keep File Update Overview as specialization.
      3. [ ] Add example and routing.
    Acceptance criteria:
      - Generic overview works for non-file actions.
      - File-specific `Итог` remains clear and unchanged.
    Проверка:
      - Use it after a non-file command decision.
    Status:
      planned.

  SL-5 — Tampermonkey command palette MVP
    Покрывает сценарии:
      - SC-5
    Артефакты / действия:
      - tools/tampermonkey/chat-command-palette.user.js
      - tools/tampermonkey/README.md
      - optional project profile config
    Шаги:
      1. [ ] Define MVP command list.
      2. [ ] Define command expansion schema with execution reminders and key boundaries.
      3. [ ] Build floating button / palette UI.
      4. [ ] Add preview/edit before insert.
      5. [ ] Add reusable default commands.
      6. [ ] Add project-specific profile later if needed.
      7. [ ] Manual browser test.
    Acceptance criteria:
      - User can pick a command.
      - User can edit the expanded prompt.
      - Prompt inserts into chat input.
      - Expansion includes command-specific execution reminders and key boundaries.
      - Script does not auto-send by default.
    Проверка:
      - Manual browser test.
    Status:
      planned.

Текущее состояние:
  Где мы:
    Reusable command examples are done.
    Goal Map owner/example split is being added.
    Use-case map routing is the current active slice.
    Critical-thinking command, generic action overview and Tampermonkey MVP are planned.

  Готово:
    - [x] `арх` vs `давай архив` separated.
    - [x] `планируй` means plan now.
    - [x] Key points have no fixed format.
    - [x] Reusable command execution examples added.

  Не готово:
    - [ ] Use-case map routes to all command examples.
    - [ ] Critical-thinking command.
    - [ ] Generic Action Overview.
    - [ ] Tampermonkey command palette.

Инварианты:
  - Use-case map remains router.
  - Workflows own semantics.
  - Examples demonstrate valid execution only.
  - Tampermonkey is helper, not source of truth.
  - Commands do not imply edit/package/commit permission unless explicitly defined.
  - Slices need acceptance criteria and verification.

Точки выбора:

  DEC-1 — Goal Map placement
    Вопрос:
      Should the map live as one root file first or be split into reusable workflow/template/example immediately?
    Влияет на:
      - SC-3
      - SL-3
    Варианты:
      A. One root owner file plus separate root example.
      B. Reusable workflow/template/example split immediately.
    Критерии выбора:
      - speed of adoption;
      - avoiding premature abstraction;
      - file size;
      - ability to route commands now.
    Текущее решение:
      A chosen for first pass.

  DEC-2 — Tampermonkey source of truth boundary
    Вопрос:
      Should command expansions copy full docs or stay as short prompt helpers?
    Влияет на:
      - SC-5
      - SL-5
    Варианты:
      A. Copy detailed docs into script.
      B. Use concise expansions that remind the chat of command intent, execution reminders and key boundaries.
    Критерии выбора:
      - avoid stale duplicated logic;
      - keep prompts usable;
      - preserve use-case map/workflow ownership;
      - keep enough command detail so long chats do not forget important constraints.
    Текущее решение:
      B preferred for MVP.

Текущий фокус:
  SL-3 / SL-2: add root Goal Map owner/example and route commands from the root use-case map.

Следующее действие:
  Apply the F7-CMD-3A archive and review the diff before commit.
```

## Why This Example Is Valid

```text
- It shows final picture first.
- It defines target scenarios as desired behaviors.
- It breaks work into process slices that cover scenarios.
- Each scenario and slice has acceptance criteria.
- It shows current state and not-ready items.
- It includes decision points with options and criteria.
- It has current focus and next action.
```
