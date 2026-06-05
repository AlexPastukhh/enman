# Slice Source Sync Register

Status: skeleton / incomplete / not synchronized
Doc version: v0.1.0
Scope: source dependency register for slice-layer drafts, slice workflows/templates and future per-slice source-sync rows

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.9.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.1.0
    - planning/slices/slice-responsibility-map.md @ Doc version: v0.1.0
    - planning/slices/SLICE-INDEX.md @ Doc version: v0.1.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planning/root-source-sync-register.md @ Doc version: v1.6.0
  Internal dependencies:
    - none
  Not checked:
    - concrete slice drafts were not audited in SLICE-TEST-SRC-0A
    - slice workflow/template owner versions are not seeded in this step
    - testing source-sync register rows are skeleton-only
```

This register is the slice-layer source dependency index.

It exists before broad slice refactor so future per-slice work has a stable place to record source relationships, version labels and local/global sync status.

It does not replace local section-level `Sources:` blocks inside slice drafts.

## 2. Current Boundary

```text
State:
  skeleton / incomplete / not synchronized

Allowed in this state:
  - name the row shape;
  - name known layer-level owner/register dependencies;
  - name candidate future row groups;
  - mark concrete slice draft coverage as not checked.

Not allowed in this state:
  - claim synchronized source coverage for current slice drafts;
  - infer per-slice dependencies from paths or index rows alone;
  - treat scenario/domain/testing links as reviewed until the slice file/local Sources are checked;
  - update every slice draft in one broad pass.
```

Local section `Sources:` blocks become authoritative for a slice only after they are added to that slice draft and reviewed. This register should then mirror the local source blocks and their current status/version labels.

## 3. Source Dependency Classification

Use this register for dependencies that affect slice draft meaning, source review or future cascade checks.

```text
format/process source:
  workflow, template, principles, responsibility map or section-source template shaping the slice.

scenario/content source:
  scenario text, DATA, behavior items, artifact map or UI/source artifact that provides slice behavior meaning.

domain source:
  aggregate/value-object/domain mapping source that provides command/read behavior or state transition meaning.

testing source:
  slice test plan workflow, testing responsibility map or testing rules used by Behavior-to-Test Trace.

internal dependency:
  one section of the same slice draft that another section depends on.

route/read/navigation link:
  a discovery or route link. It is not a source/version dependency by default.
```

If uncertain, classify with `сорс-импакт` before adding a dependency row.

## 4. Register Row Shape

Use this shape when future batches add real rows:

| Consumer file / section | Consumer status/version | Source file / section | Source version/status used | Source role | Sync status | Review result | Notes |
|---|---|---|---|---|---|---|---|
| `<slice file>#<section>` | `<Doc version or not declared>` | `<source path>#<section>` | `<Doc version / status>` | `format-process / scenario-content / domain / testing / internal / register-index` | `candidate / local-sources-added / derived / synchronized / needs-review` | `<ok / needs review / blocked>` | `<short note>` |

Do not add a row as `synchronized` unless the local slice section `Sources:` block or a documented file-level source audit was compared with this register.

## 5. Current Skeleton Rows

| Consumer / scope | Source or source group | Source role | Current status | Next action | Notes |
|---|---|---|---|---|---|
| `planning/slices/README.md` | `planning/slices/slice-source-sync-register.md` | register-index / navigation | skeleton created | Keep as slice source-sync entrypoint | Navigation only; no per-slice coverage claim. |
| `planning/slices/slice-responsibility-map.md` | `planning/slices/slice-source-sync-register.md` | responsibility route | skeleton created | Route source-sync/register questions here | Responsibility pointers are not source/version deps by default. |
| `planning/slices/SLICE-INDEX.md` | `planning/slices/slice-source-sync-register.md` | catalog/register discovery | skeleton created | Link register without marking slice rows checked | Index rows are not source evidence. |
| `planning/source-cascade-sync-workflow.md` | this register | source-cascade process | skeleton created | Use before broad slice refactor | Workflow owns register-state rules. |
| future concrete slice drafts | scenario/domain/testing/source files | candidate future rows | not checked | Audit one slice at a time | Fill from local section `Sources:` blocks after per-slice review. |

## 6. Not Checked / Deferred

```text
- No concrete `SL-*` slice draft was edited in SLICE-TEST-SRC-0A.
- No current slice draft local section `Sources:` blocks were added.
- No per-slice source chain was audited.
- No server/client template/workflow owner version seed was performed in this step.
- No testing source rows were synchronized.
- No synchronized slice coverage is claimed.
```

## 7. Change Log

```text
- SLICE-TEST-SRC-0A created this skeleton register at Doc version: v0.1.0 before broad slice refactor.
- This skeleton exists so the next work can seed owner workflow/template versions, build a local slice-refactor Goal Map and then refactor one slice at a time.
```
