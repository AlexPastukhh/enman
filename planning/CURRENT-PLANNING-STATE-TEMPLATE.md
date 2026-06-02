# Current Planning State Template

Status: current root output template / explicit current-state command template
Doc version: v0.2.0
Scope: exact output shape for `положняк` / current planning-state responses; not a Goal Map replacement and not an automatic Level 2 block

## 0. Source Sync / SRC-CMD-1A / SRC-CMD-1C

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.7.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.6.0
    - planning/root-source-sync-register.md @ Doc version: v1.3.0
    - planning/workstreams/command-system-and-tampermonkey-goal-map.md @ Doc version: v0.1.0
  Internal dependencies:
    - Purpose
    - Scope Rule
    - Template
  Not checked:
    - full root coverage outside SRC-CMD-1A scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

SRC-CMD-1A treats this file as the owner template for explicit current planning-state output.
This file does not own source/version audit semantics, Goal Map workflow or command routing.

SRC-CMD-1C refreshes this file's current source labels after root register/helper profile coverage. It does not change the `положняк` output shape.

## 1. Purpose

Use this template when the user explicitly asks for the current operational state, for example:

```text
положняк
текущий положняк
стейт
текущий стейт
покажи состояние
покажи текущий план
current planning state
```

The output should be compact, emoji-coded and scoped to the current task.

## 2. Scope Rule

The section names shown in examples are examples, not mandatory headings.

When using this template:

```text
- include only areas relevant to the current task/scope;
- omit areas that are not involved in the current plan/status question;
- do not mention Domain/Root/Slices/Tampermonkey just because an example includes them;
- if an omitted area may be wrongly assumed covered, mention it only in `Do not claim`;
- prefer compact status lines over broad repo-wide summaries.
```

Examples:

```text
If the task is source/version coverage, sections may include Root, Domain and Slices.
If the task is Tampermonkey-only, sections may include Tampermonkey, current workstream and next checks.
If the task is command/output docs, sections may include Command system, Output workflow and Examples/templates.
```

## 3. Status Legend

```text
✅ done / covered / synchronized enough for the stated scope
🟨 partial / needs review / active but not fully covered
⬜ not started / not created
▶ NOW / current focus
⏭ next / planned after current step
❌ missing / stale / must not be claimed
```

Use words with the emoji. Do not rely on emoji alone.

## 4. Template

```text
## Текущий положняк

<Area 1>:
  <emoji status> <short state>.
  <1-3 lines of relevant detail.>

<Area 2>:
  <emoji status> <short state>.
  <1-3 lines of relevant detail.>

Current focus:
  ▶ NOW — <current work / slice / decision>

Next choices:
  A. <next possible path>
  B. <next possible path>

Do not claim:
  - <only claims relevant to this scope>
```

## 5. Output Rules

```text
- This command is explicit-only. Do not automatically include it in every Level 2 answer.
- This command is not the same as `кц` / Goal Map Brief.
- `кц` answers where the long-running goal is going.
- `положняк` answers what the current operational planning/coverage state is.
- Do not update files, create archives, commit or push from this command alone.
- If current state depends on repo evidence and the relevant files were not checked, say what is unchecked.
```

## 6. Source Delta / Change Log

```text
- SRC-CMD-1A created this template for the explicit current-state command `положняк`.
- Domain/Root/Slices sections in the example are illustrative only and must not be treated as mandatory headings.
```

## Source Delta / Change Log

```text
- SRC-CMD-1C refreshed current source labels to source-cascade v0.7.0, planning-use-case-map v0.6.0 and root-source-sync-register v1.3.0; bumped this template to Doc version: v0.2.0 without changing output shape.
```
