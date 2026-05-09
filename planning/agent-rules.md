# Agent Rules

## Before starting work

1. Read `planning/current-state.md`.
2. Read `planning/agent-rules.md`.
3. Read task-specific files:
   - domain task → `domain-model.md`, `decisions.md`, `layer-plan.md`;
   - API task → `api-plan.md`, `use-cases.md`, `decisions.md`;
   - tests → `testing-strategy.md`, `current-state.md`;
   - architecture/cleanup → `solution-map-and-cleanup-plan.md`, `decisions.md`, `risk-log.md`.
4. Check current git status.
5. Identify files that may be touched.
6. If code and planning conflict, report conflict before changing code.

## During work

1. Prefer small, safe changes.
2. Do not mix layers.
3. Do not implement L2/L3 features in L1 tasks.
4. Do not rename concepts without updating planning.
5. Do not delete files without checking references.
6. Do not introduce compatibility facades unless explicitly approved.
7. Do not silently change accepted decisions.

## After work

1. Update `current-state.md`.
2. Append to `action-log.md`.
3. Update `decisions.md` if a decision was made.
4. Update `risk-log.md` if a new risk appeared.
5. Update `domain-model.md`, `api-plan.md`, `testing-strategy.md` or `layer-plan.md` if affected.
6. Report:
   - changed files;
   - tests/checks run;
   - what was not checked;
   - risks or follow-up tasks.

## Final response navigation

Every final response must include enough navigation for the next turn.

Report:

1. Worked task.
2. Task state: `done`, `partial`, `blocked`, or `needs decision`.
3. Checks/tests run.
4. What remains unchecked.
5. Recommended next 1-3 actions from `current-state.md`.
6. A direct question to the developer if the next state or next action is ambiguous.

The response should act like HATEOAS in REST: after reading it, the developer should know which safe actions are available next.

## Test reporting

When tests/checks are run, report the useful terminal result in the final response.

Include:

1. Exact command.
2. Result: passed, failed, or blocked.
3. Totals when available, for example `80 passed / 9 failed / 89 total`.
4. Names and short reasons for important failures.
5. Whether the failure appears related to the current change or to existing/environment state.

Do not only say "tests failed" or "tests passed"; include the actionable result.

## Diploma notes

If completed work is useful for the diploma text, add a short `### Diploma note` block to the related `action-log.md` entry.

Add notes for:

1. Architecture decisions.
2. Testing strategy or infrastructure.
3. Configuration/deployment portability.
4. Maintainability or refactoring rationale.
5. Risk mitigation and scope control.

Each note should include:

- `Topic`;
- `Why it matters`;
- `Possible text use`.

Never add diploma notes about AI agents, assistant workflow, prompts, or internal planning mechanics. The diploma topic is the software system, not AI-assisted development.

Skip diploma notes for small mechanical edits.

## Hard constraints

- Do not merge `Account` and `ApplicantParty`.
- Do not model ФЛ/ИП/ЮЛ as `Account` subclasses.
- During L1 refactor, do not create aggregate roots from other aggregate roots.
- During L1 refactor, do not add one-side collection navigations for inter-aggregate relationships unless explicitly justified.
- During L1 refactor, use scalar `long` IDs for inter-aggregate references unless a later explicit decision changes this.
- Do not model aggregate relationships as primitive ID collections such as `List<long> ApplicantPartyIds`.
- Do not add typed ID value objects yet.
- Do not add electronic signature.
- Do not add real government integrations.
- Do not add SMS before email works.
- Do not implement anonymous requests before L3.
- Do not move rate limiting/account lockout into L1.
- Do not implement documents/PDF/mock verification in L1.
- Do not treat generated artifacts as source code.
- Do not leave old active planning files that conflict with current planning.

## Handoff prompt for a new AI agent

```text
You are working on the EnergyManagement diploma project.

Read first:
- planning/current-state.md
- planning/agent-rules.md

Then read task-specific files:
- domain work: planning/domain-model.md, planning/decisions.md, planning/layer-plan.md
- API work: planning/api-plan.md, planning/use-cases.md
- tests: planning/testing-strategy.md
- cleanup/architecture: planning/solution-map-and-cleanup-plan.md, planning/risk-log.md

Planning is the source of truth for current project direction.
If code and planning conflict, report the conflict before changing code.

After completing a task:
- update planning/current-state.md;
- append planning/action-log.md;
- update planning/decisions.md if needed;
- update planning/risk-log.md if needed.
```
