# Testing Responsibility Map

Status: current testing-layer responsibility map  
Doc version: v0.1.0
Scope: routes testing-layer guidance for slice Test / Verification Plans, E2E workflows, test object patterns and transitional screenshot/evidence notes

## 1. Purpose

This file routes testing information to the correct owner inside `planning/testing/`.

It supports, but does not replace:

```text
planning/slices/slice-test-plan-workflow.md
```

Slice drafts own their own `Test / Verification Plan` and `Behavior-to-Test Trace` sections. The testing layer provides reusable principles and layer-specific workflow guidance.

## 2. Core Ownership Rule

```text
planning/slices/slice-test-plan-workflow.md
  owns the slice draft testing algorithm and Behavior-to-Test Trace shape.

planning/testing/testing-responsibility-map.md
  owns testing-layer routing and file selection.

planning/testing/testing-principles.md
  owns test-layer boundaries and general principles.

planning/testing/testing-source-sync-register.md
  owns testing-layer source-sync register rows and future behavior-to-test dependency status.
```

Do not duplicate the full slice test-plan workflow in testing-layer files.

## 3. Routing Table

| Information / task | Read / use | Do not use as |
|---|---|---|
| Slice draft `Test / Verification Plan` shape | `planning/slices/slice-test-plan-workflow.md` | A generic testing README |
| Test-layer boundary decision | `planning/testing/testing-principles.md` | Server-only rules |
| Server/API slice test buckets | `planning/testing/server-slice-test-plan-rules.md` | E2E workflow |
| Testing source-sync status | `planning/testing/testing-source-sync-register.md` | Behavior proof or current coverage claim |
| E2E browser + real API workflow | `planning/testing/e2e-testing-workflow.md` | Component test workflow |
| Page Object / Component Object rules | `planning/testing/test-object-patterns.md` | Slice draft template |
| Legacy Playwright workflow alias | `planning/testing/e2e-playwright-workflow.md` | Current workflow source |
| Historical/planned Playwright cleanup note | `planning/testing/playwright-e2e-cleanup-plan.md` | Current state proof |
| Playwright E2E + screenshot/evidence transitional plan | `planning/testing/playwright-e2e-and-screenshot-plan.md` | Default slice behavior proof |

## 4. Slice Test Plan Read Selector

When drafting a slice `Test / Verification Plan`:

```text
1. Start from planning/slices/slice-test-plan-workflow.md.
2. Use Behavior-to-Test Trace to identify the needed test layer.
3. Read this testing responsibility map.
4. Read the specific testing file selected below.
```

### Server/API behavior

Read:

```text
planning/testing/server-slice-test-plan-rules.md
planning/testing/testing-principles.md
```

Use for:

```text
API integration tests;
server command/read slice buckets;
DB state assertions;
no-mutation proof;
ProblemDetails / validation contract proof.
```

### Client visible behavior

Read:

```text
planning/testing/testing-principles.md
```

Also read, when page/component object patterns matter:

```text
planning/testing/test-object-patterns.md
```

Use for:

```text
visible UI behavior;
form state;
deferred validation;
field/server error mapping;
accessibility locator contract;
disabled/enabled/pending/error/success states.
```

### E2E / cross-layer user path

Read:

```text
planning/testing/e2e-testing-workflow.md
planning/testing/testing-principles.md
```

Also read, when Page Object patterns matter:

```text
planning/testing/test-object-patterns.md
```

Use for:

```text
browser -> client -> HTTP API -> server -> persistence/session -> visible outcome;
critical happy paths;
critical cross-layer stale/auth/access flows.
```

Do not use E2E for every validation matrix branch when API/component tests own detailed behavior.

### Contract/generated support checks

Generated/API contract checks can support behavior proof but do not replace behavior tests.

Use API/generation docs when the slice specifically owns generated contracts or constants.

### Screenshot / evidence generation

Screenshot runner and reproducible screenshot evidence are not ordinary slice behavior proof.

Use screenshot planning only when the task explicitly includes:

```text
VKR/thesis screenshots;
evidence artifact generation;
Playwright screenshot runner;
visual evidence packaging.
```

Do not select screenshot runner as a `Test layer` in the ordinary Behavior-to-Test Trace unless the slice explicitly owns screenshot/evidence behavior.

## 5. Current / Legacy / Transitional Files

| File | Status | Notes |
|---|---|---|
| `testing-principles.md` | current principles | Test-layer boundaries and general selection principles. |
| `server-slice-test-plan-rules.md` | current server-specific rules | Server/API read/query vs command slice test plan buckets. |
| `testing-source-sync-register.md` | skeleton / incomplete / not synchronized | Testing-layer source-sync register for future behavior-to-test rows. |
| `e2e-testing-workflow.md` | current E2E workflow | Current Playwright/E2E workflow. |
| `test-object-patterns.md` | current object pattern rules | Page Object and Component Object rules. |
| `e2e-playwright-workflow.md` | legacy alias | Keep as compatibility pointer to current E2E workflow. |
| `playwright-e2e-cleanup-plan.md` | historical/planned cleanup note | Needs currentness verification before use as actionable plan. |
| `playwright-e2e-and-screenshot-plan.md` | transitional mixed file | Contains E2E support notes and screenshot/evidence notes; screenshot placement review is separate. |

## 6. Source Sync Register Boundary

```text
planning/testing/testing-source-sync-register.md
  owns testing-layer source-sync register rows and future behavior-to-test dependency status.

Current state:
  skeleton / incomplete / not synchronized.
```

Do not use `testing-source-sync-register.md` as synchronized proof for a concrete slice test plan until that test plan or behavior-to-test trace has been reviewed.

## 7. Guardrails

```text
Do not let planning/testing/ replace slice-test-plan-workflow.md for slice drafts.
Do not treat screenshot evidence as ordinary test proof.
Do not use legacy alias files as current workflow sources.
Do not treat historical cleanup plans as current repo state without verification.
Do not duplicate server-specific rules into every slice draft; link or summarize only what is needed.
```
