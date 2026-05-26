# Slice Draft Authoring Workflow

Status: current root workflow for creating, reviewing and refactoring slice drafts  
Scope: process for slice draft work before side-specific server/client/cross-cutting workflows

## 1. Purpose

This workflow describes how to create, review or refactor a slice draft from scenario/source behavior.

```text
Workflow = how to work.
Principles = rules and meaning.
Template = output shape.
Responsibility map = where information belongs.
```

This file must not duplicate the detailed rules in:

```text
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-test-plan-workflow.md
planning/slices/server-implementation-principles.md
planning/slices/client-implementation-principles.md
```

## 2. When To Use

Use this workflow for:

```text
- creating a new slice draft;
- reviewing an existing slice draft;
- refactoring a draft to the current slice model;
- preparing a server/backend/API slice plan;
- preparing a client sidecar plan;
- preparing a cross-cutting/helper slice plan.
```

Do not use this workflow as a replacement for implemented-slice synchronization. If implementation already exists and the draft may be stale, switch to:

```text
planning/slices/implemented-slice-sync-workflow.md
```

## 3. Required Reads

Start with:

```text
planning/slices/README.md
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-test-plan-workflow.md
```

Then use the relevant side-specific workflow/template when slice type is known:

```text
Server/backend/API:
  planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
  planning/slices/server/SERVER-SLICE-TEMPLATE.md
  planning/slices/server-implementation-principles.md

Client sidecar:
  planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
  planning/slices/client/CLIENT-SLICE-TEMPLATE.md
  planning/slices/client-implementation-principles.md
  planning/slices/client-css-architecture-rules.md
  planning/slices/client-form-validation-implementation-principles.md
  planning/slices/client-a11y-implementation-principles.md
  planning/slices/client-ui-style-workflow.md

Cross-cutting/paired concern:
  planning/slices/cross-cutting/README.md
  planning/slices/cross-cutting/CROSS-CUTTING-UMBRELLA-TEMPLATE.md
```

## 4. Workflow Steps

```text
1. Confirm the work belongs to the slice layer.
2. Identify slice type: server / client / cross-cutting / implemented sync.
3. Collect scenario/source files.
4. Collect DATA / behavior items / cross-cutting behavior sources.
5. Collect domain/API/testing/client/server implementation sources if relevant.
6. Mark source status: current / provisional / pending source sync / blocked.
7. Draft Scenario Scope / Slice Boundary.
8. List behavior not implemented by this slice and assign owner/destination.
9. Identify cross-cutting concerns and delegated owners.
10. Run Future-Change Check without expanding current scope.
11. Draft implementation responsibilities and boundaries.
12. Draft Implementation Flow by responsibility owner.
13. Draft Behavior Coverage.
14. Draft Behavior-to-Test Trace using slice-test-plan-workflow.md.
15. Check local/global sync needs: registers, index, responsibility map, README.
16. If implementation already exists, switch to implemented-slice-sync-workflow.md.
17. Produce reviewable output with assumptions, not-checked items and next actions.
```

## 5. Source Collection Checklist

A slice draft should identify the sources that shape it:

```text
- scenario text source;
- UI scenario source, if client-visible behavior is involved;
- DATA source, if user-entered/seen/selected data matters;
- behavior item source;
- cross-cutting behavior source, if concern behavior applies;
- domain source/draft, if domain model or invariants are central;
- API/generated contract source, if endpoint/DTO/generated artifacts change;
- testing/source workflow, if verification model is non-trivial;
- current code/tests, only when syncing an implemented slice.
```

Do not invent behavior locally inside a slice draft when source files exist.

## 6. Section-Level Sources

For high-risk or separately reviewed sections, include section-level sources.

Use this shape when needed:

```text
Sources:
  Format:
    - <workflow/template/principles file>
  Content:
    - <scenario/domain/API/slice source file or earlier section>
  Internal dependencies:
    - <draft sections this section depends on>
  Not checked:
    - <explicitly unchecked evidence/source>
```

Section-level sources are especially useful for:

```text
Scenario Sources / Source Sync;
Scenario Scope / Slice Boundary;
Domain Methods / Domain Behavior Contract;
Implementation Flow;
API / Client Contract;
Behavior Coverage;
Test / Verification Plan.
```

Do not add heavy source blocks to every small section when they do not help review.

## 7. Output Checklist

Before calling a slice draft ready for review, check:

```text
[ ] Scope is behavior-based, not file-list-based.
[ ] Scenario/source status is explicit.
[ ] Out-of-scope behavior has owner/destination.
[ ] Cross-cutting concern applicability is stated when relevant.
[ ] Future-change check does not expand current scope.
[ ] Implementation responsibility is not mixed across controller/validator/application/domain/client/UI.
[ ] Behavior Coverage is separate from Test / Verification Plan.
[ ] Required assertions live inside Behavior-to-Test Trace.
[ ] Local/global sync was checked.
[ ] New shared questions/notes were routed to the correct register.
[ ] No source/version claims are invented.
[ ] Runtime implementation status is not claimed unless current code/tests were checked.
```

## 8. Local / Global Sync Check

After drafting or refactoring, decide whether to update:

```text
planning/slices/SLICE-INDEX.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/slice-responsibility-map.md
planning/slices/README.md
```

Local draft details stay in the draft. Shared facts that affect future work should be mirrored into the correct register/index/map.

## 9. Implemented Slice Exception

If the slice has current implementation/code/tests, do not only rewrite the draft to match the newest template.

Use:

```text
planning/slices/implemented-slice-sync-workflow.md
```

Check source files, draft state, current implementation, current tests and generated artifacts before updating status or implementation claims.
