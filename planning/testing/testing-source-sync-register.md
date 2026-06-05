# Testing Source Sync Register

Status: skeleton / incomplete / not synchronized
Doc version: v0.1.0
Scope: source dependency register for testing workflows, testing rules, slice test plans and future behavior-to-test source-sync rows

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.9.0
    - planning/testing/testing-responsibility-map.md @ Doc version: v0.1.0
    - planning/slices/slice-test-plan-workflow.md @ version not confirmed
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.6.0
    - planning/slices/slice-source-sync-register.md @ Doc version: v0.1.0
  Internal dependencies:
    - none
  Not checked:
    - concrete slice test plans were not audited in SLICE-TEST-SRC-0A
    - testing workflow/rules owner versions are not seeded in this step
    - no behavior-to-test rows are synchronized yet
```

This register is the testing-layer source dependency index.

It exists before slice/testing refactor so future Behavior-to-Test Trace and test-plan updates have a stable place to record source relationships, version labels and sync status.

It does not replace `planning/slices/slice-test-plan-workflow.md` or testing-layer owner files.

## 2. Current Boundary

```text
State:
  skeleton / incomplete / not synchronized

Allowed in this state:
  - define row shape;
  - name testing-layer owner/register dependencies;
  - mark concrete test-plan coverage as not checked;
  - support future one-slice-at-a-time refactor.

Not allowed in this state:
  - claim testing coverage for current slice drafts;
  - infer test dependencies from slice filenames alone;
  - treat generated/API/E2E/screenshot references as tested behavior proof without review;
  - rewrite test plans in this skeleton step.
```

Testing rows become authoritative only after the relevant slice test plan, behavior-to-test trace or testing workflow/rule section is reviewed.

## 3. Testing Source Dependency Classification

```text
slice test-plan source:
  `planning/slices/slice-test-plan-workflow.md` and the local slice draft Test / Verification Plan section.

testing-layer source:
  testing responsibility map, testing principles, server/API rules, E2E workflow or object-pattern docs.

behavior source:
  scenario behavior items, cross-cutting behavior sources or domain behavior consumed by the test trace.

contract source:
  API contract, generated type/constant or DTO/validation source used by test expectations.

implementation/evidence source:
  code/test/generated artifact or prior evidence checked by status reconciliation.
```

Do not use screenshot/evidence workflow as ordinary behavior proof unless the slice explicitly owns screenshot/evidence behavior.

## 4. Register Row Shape

Use this shape when future batches add real rows:

| Test consumer / section | Consumer status/version | Source file / section | Source version/status used | Source role | Sync status | Review result | Notes |
|---|---|---|---|---|---|---|---|
| `<slice file>#Test / Verification Plan` | `<Doc version or not declared>` | `<source path>#<section>` | `<Doc version / status>` | `slice-test-plan / testing-layer / behavior / contract / implementation-evidence` | `candidate / local-sources-added / derived / synchronized / needs-review` | `<ok / needs review / blocked>` | `<short note>` |

Do not add a row as `synchronized` unless the relevant test plan/local Sources block or a documented file-level testing audit was compared with this register.

## 5. Current Skeleton Rows

| Consumer / scope | Source or source group | Source role | Current status | Next action | Notes |
|---|---|---|---|---|---|
| `planning/testing/testing-responsibility-map.md` | `planning/testing/testing-source-sync-register.md` | responsibility route | skeleton created | Route testing source-sync/register questions here | Responsibility pointer only; no test coverage claim. |
| `planning/source-cascade-sync-workflow.md` | this register | source-cascade process | skeleton created | Use before broad testing/slice refactor | Workflow owns register-state rules. |
| future slice `Test / Verification Plan` sections | testing workflow/rules + behavior sources | candidate future rows | not checked | Fill one slice at a time | Must come from reviewed slice/test source pass. |
| future testing owner docs | testing rules/workflows/principles | candidate future rows | not checked | Version-seed and audit in later step | No owner workflow/template version seed in SLICE-TEST-SRC-0A. |

## 6. Not Checked / Deferred

```text
- No concrete slice draft Test / Verification Plan was edited.
- No Behavior-to-Test Trace was audited.
- No testing workflow/rule/principles version seed was performed in this step.
- No test implementation or generated artifacts were checked.
- No synchronized testing coverage is claimed.
```

## 7. Change Log

```text
- SLICE-TEST-SRC-0A created this skeleton register at Doc version: v0.1.0 before broad slice/testing refactor.
- This skeleton exists so later batches can seed testing owner versions, audit test-plan source usage and then synchronize rows one slice at a time.
```
