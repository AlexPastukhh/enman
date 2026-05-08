# Planning

`planning/` is the living project memory for AI agents and the developer.

Directory goals:

- preserve context between sessions;
- reduce repeated rediscovery;
- avoid reopening accepted architecture debates;
- record decisions, completed actions, risks and blockers;
- help agents make small safe changes without losing the larger project direction.

## Read Order

Always read before work:

1. `current-state.md`
2. `agent-rules.md`

Read by task type:

| Task type | Read |
|---|---|
| Domain / classes | `domain-model.md`, `decisions.md`, `layer-plan.md` |
| API / controllers / DTO | `api-plan.md`, `use-cases.md`, `decisions.md` |
| Tests | `testing-strategy.md`, `current-state.md` |
| Architecture / refactoring | `layer-plan.md`, `decisions.md`, `risk-log.md`, `solution-map-and-cleanup-plan.md` |
| Diploma documentation | `general-project-info.md`, `use-cases.md`, `layer-plan.md` |
| Cleanup / solution structure | `solution-map-and-cleanup-plan.md`, `action-log.md`, `risk-log.md` |

## Source Of Truth

If code and planning disagree, the agent must:

1. explicitly report the mismatch;
2. suggest safe options;
3. avoid silent architecture changes;
4. update planning after an accepted decision.

## Task Navigation Protocol

`current-state.md` must contain a small active task board. It is the HATEOAS-like navigation surface for the next agent turn: every completed step should expose the next safe actions.

Task statuses:

- `done` - implemented or intentionally completed.
- `active` - the task currently being worked on.
- `next` - recommended next task.
- `blocked` - cannot continue without external state or a decision.
- `later` - valid task, but not part of the current safe cut.

After every meaningful change, the agent must update:

- `Current task` if the focus changed;
- `Task board` statuses if any task moved;
- `Current implementation status` if project state changed;
- `Current blockers / known issues` if verification revealed a blocker;
- `action-log.md` with factual completed work.

Final agent response should always include a compact navigation block:

- worked task;
- resulting state: `done`, `partial`, `blocked`, or `needs decision`;
- checks/tests run with exact command and result summary;
- what remains unchecked;
- recommended next 1-3 actions based on `current-state.md`;
- question to the developer when a task state or next step is ambiguous.

The goal is that every final answer gives the developer something concrete to continue from.

## Test Reporting Protocol

When the agent runs tests or checks, the final answer must include enough output for the developer to understand the result without seeing the terminal.

For every command, report:

- exact command;
- result: passed, failed, or blocked;
- totals when available, for example `80 passed / 9 failed / 89 total`;
- important failure names and short failure reasons;
- whether the failure is related to the current change or an existing/environment issue.

If a command fails before tests run, report the tool/environment error separately from test failures.

## Diploma Notes Protocol

When a change is useful for the diploma text, add a short `### Diploma note` block to the related `action-log.md` entry.

Use this only for decisions or work that can support the written diploma, for example:

- architecture decisions;
- scope reduction and MVP/layering decisions;
- testing strategy or test infrastructure;
- configuration and deployment portability;
- maintainability improvements;
- risk mitigation;
- traceability between requirements, implementation and verification inside the software project.

Do not write diploma notes about:

- AI agents;
- assistant workflow;
- prompt/process organization;
- internal planning mechanics unless they directly describe the diploma system itself.

Keep the note short:

- `Topic` - where it may fit in the diploma;
- `Why it matters` - 1-2 sentences;
- `Possible text use` - architecture, testing, implementation, risk analysis, future work, etc.

Do not add diploma notes for routine formatting, tiny typo fixes, purely mechanical edits, or AI-assisted development process.

## Update Rule

After every meaningful change:

- update `current-state.md`;
- append an entry to `action-log.md`;
- update `decisions.md` if a new decision was accepted;
- update `risk-log.md` if a new risk appeared;
- update the affected profile file: `domain-model.md`, `api-plan.md`, `testing-strategy.md` or `layer-plan.md`.

## Meaningful Change

Treat these as meaningful changes:

- class added or renamed;
- endpoint changed;
- EF mapping strategy changed;
- feature layer added or removed;
- test strategy changed;
- files deleted or moved;
- contract generation approach changed;
- risk or planning/code conflict discovered.

## Old Plans

Old active plans must not remain beside current planning. They should be:

- moved to `archive/`;
- marked deprecated;
- briefly explained.

Git already stores history, so do not create many duplicate planning-file copies.
