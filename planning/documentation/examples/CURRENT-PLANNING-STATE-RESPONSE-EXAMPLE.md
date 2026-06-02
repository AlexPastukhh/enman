# Current Planning State Response Example

Status: current response/output command example / demonstration-only
Doc version: v0.1.0
Scope: demonstrates the `положняк` current planning-state output format; does not own command routing or source/version audit rules

## 0. Source Sync / SRC-CMD-1A

```text
Sources:
  Format/process:
    - planning/CURRENT-PLANNING-STATE-TEMPLATE.md @ Doc version: v0.1.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.6.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.5.0
    - planning/root-source-sync-register.md @ Doc version: v1.1.0
    - planning/workstreams/command-system-and-tampermonkey-goal-map.md @ Doc version: v0.1.0
  Internal dependencies:
    - Example Output
    - Rules Demonstrated
  Not checked:
    - full root coverage outside SRC-CMD-1A scope
```

This example demonstrates output shape only. It does not own Goal Map workflow, source/version audit process, command routing, permission boundaries or current repo state.

## 1. User Command Examples

```text
положняк
текущий положняк
стейт
покажи состояние
покажи текущий план
```

## 2. Scope Note

The Domain/Root/Slices sections below are examples because this demonstration uses a source/version coverage planning scenario.

For other tasks, replace these areas with scope-relevant sections. Do not always print Domain/Root/Slices when they are not involved in the current question.

## 3. Example Output

```text
## Текущий положняк

Domain aggregates:
  ✅ covered/synchronized enough.
  4 active aggregate drafts have local Sources blocks.
  Domain register covers active aggregates.

Domain value objects:
  ✅ covered/synchronized enough.
  Active VO drafts have local Sources blocks.
  Domain register covers active value objects.

Root:
  🟨 partial.
  Root core groups covered.
  ROOT-FULL-1 covers Goal Map / Tampermonkey active scope.
  Full root coverage still deferred.

Slices:
  ⬜ not started.
  planning/slices/slice-source-sync-register.md does not exist yet.

Current workstream:
  ▶ NOW — SL-6 Tampermonkey helper smoke testing.

Next choices:
  A. Continue SL-6 smoke testing.
  B. Start source/version maintenance command implementation.
  C. Plan slice-source-sync-register.

Do not claim:
  - full root coverage;
  - full domain-folder coverage;
  - slice coverage;
  - userscript as source of truth.
```

## 4. Rules Demonstrated

```text
- Output is explicit-only for `положняк` / current-state commands.
- Sections are scope-relevant examples, not mandatory headings.
- Emoji labels are paired with words.
- The block is compact and operational.
- It does not replace Goal Map Brief.
- It does not grant permission to update files or create archives.
```
