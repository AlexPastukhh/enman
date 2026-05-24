# Status Reconciliation Workflow

Status: current status-reconciliation workflow  
Scope: keeping planning docs aligned with current implementation and generated artifacts

## 1. Purpose

Status reconciliation updates docs when implementation moved faster than planning notes.

It answers:

```text
what is implemented?
what is only first-stage implemented?
what is partially implemented?
what remains planned?
what is deferred?
what should be a future review item rather than a defect?
```

For broad or multi-file status/navigation documentation changes, prepare a Documentation Update Plan first:

```text
planning/documentation/documentation-update-plan-workflow.md
```

## 2. When To Run

Run status reconciliation when:

```text
- a commit implemented a planned cross-cutting slice;
- generated artifacts appeared;
- tests or E2E baseline changed;
- client/server contract changed;
- a slice became partially implemented;
- an old doc still says “planned” for implemented behavior;
- a prompt for another chat depends on current status.
```

## 3. Evidence Order

Prefer current repo evidence:

```text
1. code / generated artifacts / tests;
2. current planning docs;
3. recent user discussion;
4. older archives;
5. memory.
```

If evidence conflicts, say so and update docs with explicit status/assumption.

## 4. Status Table Template

Use this table when helpful:

| Area | Current evidence | Status | Docs to update | Next action |
|---|---|---|---|---|

Example statuses:

```text
CC-API-001 | generate-openapi + Shared/openapi.json + openapi-types.ts | first-stage implemented | CC-API-001, README, workflow | hardening/future questions
CC-CONST-001 | generator/checker/artifacts/tests | implemented baseline | CC-CONST-001 | client usage/API test gaps
E2E auth | root Playwright + register/login tests | implemented baseline | e2e-testing-workflow | future L1 migration
```

## 5. First-Stage Implemented

Use `first-stage implemented` when:

```text
- the core workflow works;
- generated artifacts/checks exist;
- first consumers or metadata exist;
- known hardening work remains.
```

Do not call this merely `planned`.

Do not call it fully final if important hardening remains.

## 6. Future Review Items

Use future review items when:

```text
- current implementation works;
- concern may matter under CI/scale/consolidation;
- changing now would distract from current next step;
- question should not be forgotten.
```

Future review items are not current defects unless they block the current gate.

## 7. Prompt Impact

When docs are reconciled, update prompts for other chats:

```text
- remove instructions to implement already implemented infrastructure;
- add “do not redo” rules;
- mark current baseline and remaining gaps;
- list blockers accurately;
- protect working E2E/auth/client/server contract behavior from accidental rewrite.
```

## 8. Do Not

```text
- Do not overclaim planned code as implemented.
- Do not leave old planned wording after implementation is confirmed.
- Do not bury status updates in unrelated docs.
- Do not update only central README while detailed slice doc remains stale.
- Do not skip the Documentation Update Plan for broad status/navigation changes.
```
