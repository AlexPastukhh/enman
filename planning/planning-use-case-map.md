# Planning Use Case Map

Status: current root use-case map / action-to-doc-flow router
Doc version: v1.0.0
Scope: maps user-visible actions and commands to planning docs, workflows, templates, sources and permission boundaries

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/documentation/field-kits/root-use-case-map-field-kit.md @ Doc version: v0.1.0
    - planning/documentation/use-case-map-workflow.md @ Doc version: v0.1.0
    - planning/documentation/USE-CASE-MAP-TEMPLATE.md @ version not confirmed
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
  Internal dependencies:
    - none
  Not checked:
    - full reusable use-case map layer audit outside ROOT-SRC-2A
```

This file answers:

```text
User says X.
What should the chat do?
Which docs should it read?
Why those docs?
Where is the obligation to read/use them defined?
What output is expected?
What requires explicit permission?
```

This file is not:

```text
- a full workflow;
- a template;
- a responsibility map;
- a README replacement;
- a replacement for workflow-activation-map.md.
```

It is a root action/use-case trace map.


Command/action routing authority rule:

```text
planning/planning-use-case-map.md
  owns concrete Enman user-visible command/action routing.

planning/workflow-activation-map.md
  helps select workflows and read order.
  It does not own command semantics when this map owns the route.

planning/planning-doc-responsibility-map.md
  chooses planning layer/responsibility owner.
  It does not create source/version dependencies by pointing to owners.
```

Route/read/navigation links are not source/version dependencies by default. They become source dependencies only when the consumer file/section uses the linked file's meaning, rule, output shape, evidence or source truth.

This file is the concrete Enman root use-case map. Reusable setup guidance is owned by `planning/documentation/field-kits/root-use-case-map-field-kit.md`; reusable maintenance rules are owned by `planning/documentation/use-case-map-workflow.md`; exact reusable map shape is owned by `planning/documentation/USE-CASE-MAP-TEMPLATE.md`.

## 2. Relationship To Root Files

```text
Sources:
  Format/process:
    - planning/README.md @ Doc version: v0.3.0
    - planning/workflow-activation-map.md @ Doc version: v0.6.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.4.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Purpose
  Not checked:
    - local source coverage for documentation reusable files outside ROOT-SRC-2A
```

```text
planning/README.md
  Overview / entry point / source-of-truth map.

planning/workflow-activation-map.md
  Workflow activation, Workflow Preflight, implicit vs explicit actions.

planning/planning-doc-responsibility-map.md
  Root layer routing.

planning/planning-use-case-map.md
  Action/use-case trace:
  user command -> docs/workflows/templates/sources/output.

planning/documentation/use-case-map-workflow.md
  Reusable workflow for creating/updating use-case maps.

planning/documentation/USE-CASE-MAP-TEMPLATE.md
  Reusable template for concrete use-case-map shape.


planning/documentation/field-kits/root-use-case-map-field-kit.md
  Reusable setup kit for deriving one project root use-case map and common command clusters.

planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
  Reusable profile-specific setup kit for adding scenario/domain/slice route families to the project root map.

```

## 3. Universal Chat Algorithm

```text
Sources:
  Format/process:
    - planning/workflow-activation-map.md @ Doc version: v0.6.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.4.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.8.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Relationship To Root Files
  Not checked:
    - protocol/role files covered by ROOT-SRC-2B; downstream role-specific workflows not re-audited
```

For non-trivial planning/repo work, use this path:

```text
1. Decide whether the request starts new work or continues active context.
2. Use planning/workflow-activation-map.md.
3. Output Workflow Preflight when required.
3a. In Workflow Preflight, list explicit commands accepted from the user/helper prompt.
3b. In Workflow Preflight, list implicit command/task modes that follow from the request itself.
3c. Put `key_reminders` only under `Command/task considerations`.
3d. If commands, owner docs, source state or permissions conflict, stop and tell the user before proceeding.
4. Use planning/planning-doc-responsibility-map.md to choose the planning layer.
5. Use layer README / responsibility map for local routing.
5a. Use project root profiles when status/shared visibility/source usage is relevant.
6. Select traversal depth.
7. Select read source mode.
8. Select output mode when the user asks for a deliverable such as an archive/package.
9. Read workflows for algorithms.
10. Read templates for output shape.
10a. If the task is inside an active long-running workstream, read the relevant living Goal Map before choosing the next step.
10b. If the task comes from or changes the Tampermonkey helper, treat inserted command bodies as route hints and verify behavior against this root map plus owner workflows.
11. Read sources/evidence needed for this pass.
12. Report sources used this pass and relevant not-checked sources.
13. Separate implicit checks from explicit-permission actions.
14. Produce answer/update/plan/draft/package.
```

## 3A. Principle Section References

```text
Sources:
  Format/process:
    - planning/documentation/planning-docs-architecture-principles.md @ Doc version: v0.1.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.8.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Universal Chat Algorithm
  Not checked:
    - principles file local source audit outside ROOT-SRC-2A
```

Some use cases require not only reading a workflow, but also re-checking a specific architecture principle.

Use this rule instead of copying principle text into this file:

```text
- principles own global documentation theory;
- this use-case map owns action routing;
- when a use case depends on a principle, link to the principle section;
- do not copy the principle logic into the use-case row;
- on repeated/targeted traversal, re-read only the linked principle sections when the full principles file was recently checked and scope did not change.
```

Common principle references:

| Use case / concern | Read on first use or changed scope | Why |
|---|---|---|
| Documentation structure / placement / owner boundaries | `planning/documentation/planning-docs-architecture-principles.md#contents`, `#1a-principles-responsibility-boundary`, `#17-responsibility-ownership`, `#24-no-duplication--authority-rule`, `#24a-link-instead-of-copy--docs-dry-rule`, `#24c-responsibility-zone-review-guardrail` | Prevent misplaced duplicated logic and classify existing content by owner zone before moving it. |
| Template/workflow/example governance | `planning/documentation/planning-docs-architecture-principles.md#9-template-vs-workflow`, `#24a-link-instead-of-copy--docs-dry-rule` | Keep templates, workflows and examples within their own responsibilities. |
| New documentation concept / compound starter artifact / filename-version migration | `planning/documentation/planning-docs-architecture-principles.md#9a-compound-starter-artifact-principle`, `#13a-filename-version-migration-policy`, `#18a-decomposable-file-architecture` | Allow one strict principles+workflow+template owner file for a new cohesive concept, keep the example separate, and avoid ad hoc file renames/version migrations. |
| Archive/replacement package output | `planning/documentation/planning-docs-architecture-principles.md#23-direct-edits-archives-and-commit-granularity`, plus `planning/replacement-file-generation-guide.md` | Archive output is an output mode and must use replacement files. |
| Source usage / stale-reference / cascade / reviewed-work concerns | `planning/documentation/planning-docs-architecture-principles.md#12a-layer-encapsulation-and-attention-preservation`, `#13-source--version-principle`, `#14-dependency-cascade-principle`, `#15-section-level-sources-principle` | Prevent duplicated source truth and preserve attention by referencing reviewed upstream work instead of reconstructing it. |
| Route/read/navigation vs source dependency | `planning/source-cascade-sync-workflow.md`, `planning/root-source-sync-register.md` | Route/read/navigation links are valid discovery/routing aids but do not create source/version cascade by default. Use source-impact classification when uncertain. |
| Accepted commands / post-apply preservation | `planning/documentation/planning-docs-architecture-principles.md#24b-accepted-command-and-preservation-guardrails`, plus owner workflow/use-case files | Prevent silent reinterpretation and accidental information loss after replacement packages. |
| Use-case map setup / common command extraction | `planning/documentation/field-kits/root-use-case-map-field-kit.md`, `planning/documentation/use-case-map-workflow.md`, `planning/documentation/USE-CASE-MAP-TEMPLATE.md` | Keep one concrete root UCM per project while reusing setup/workflow/template logic. |

Do not add ad hoc source-version fields to use-case rows. Use accepted `Doc version:` headers, local `Sources:` blocks and source-sync registers where the source-cascade workflow requires them. Filename-version migration is a separate planned migration policy; do not rename files or remove `Doc version:` headers from this map as a side effect of an unrelated use-case update.

## 4. Active Context Rule

```text
Sources:
  Format/process:
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Universal Chat Algorithm
  Not checked:
    - response workflow local source pass outside ROOT-SRC-2A
```

Short commands apply to the active context unless the user names another target.

Active context can be:

```text
- active draft;
- active answer;
- active plan;
- active section;
- active canvas document;
- active use case;
- active source snapshot;
- active repo/archive read mode.
```

If the user says `драфт`, `давай драфт`, `покажи драфт`, `обнови` or `обнови драфт` while an active draft exists:

```text
- treat it as active draft continuation;
- apply latest discussion deltas;
- update canvas if draft is in canvas;
- otherwise output latest draft version;
- do not start a new draft unless user says "новый драфт" or names another target.
```

If no active context exists, ask for the missing target unless the task is obvious from the current message.

## 4A. Accepted Command / No Reinvention Rule

```text
Sources:
  Format/process:
    - planning/documentation/planning-docs-architecture-principles.md#24b-accepted-command-and-preservation-guardrails @ version not confirmed
    - planning/replacement-file-generation-guide.md @ Doc version: v0.1.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Active Context Rule
  Not checked:
    - archive/output workflow pass outside ROOT-SRC-2A
```

If user wording matches an accepted command or use case, follow the owner use-case/workflow definition.

Do not silently substitute another output mode or process because it seems safer, easier or more convenient.

If the accepted mode cannot be completed safely, stop and explain the blocker. Ask for explicit approval before switching modes.

Command body boundary:

```text
- Pasted or helper-inserted `[ENMAN_COMMAND]` bodies are accepted input context.
- They are not stronger than this root map or linked owner docs.
- If the command body conflicts with this root map or owner docs, stop and tell the user.
- `key_reminders` from a command body should be surfaced under Workflow Preflight `Command/task considerations` when preflight applies.
- Do not duplicate `key_reminders` under `Explicit commands accepted`.
- Do not say a command, route, source, file, archive, sync or update was checked/done unless it was actually checked/done.
```

Examples:

```text
- `давай архив` means replacement archive/package.
- It does not mean patch script, diff-only output or partial snippets.
- `проверь` after archive application means post-apply verification, including preservation/no-loss checks.
```

Relevant principles:

```text
planning/documentation/planning-docs-architecture-principles.md#24b-accepted-command-and-preservation-guardrails
```

## 5. Traversal Depth

```text
Sources:
  Format/process:
    - planning/workflow-activation-map.md @ Doc version: v0.6.0
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
  Internal dependencies:
    - Universal Chat Algorithm
  Not checked:
    - none
```

Traversal depth answers: how much should the chat read/check now?

| Mode | Meaning | Use when |
|---|---|---|
| Full traversal | Read the full required doc/source chain for the use case. | New chat, first use of a use case, changed scope/source model, high-risk status/current claim, explicit full recheck. |
| Targeted traversal | Read only files needed for the current update/check. | Same use case, narrow update, named target file/source, mechanical link check. |
| Reuse previous traversal | Reuse recent checked context in this chat. | Same active draft/use case, no source/scope change, user says no changes. |
| No traversal | Answer from active context without new reading. | Simple clarification, active draft display, wording update with no evidence change. |

Do not confuse traversal depth with read source mode.

## 6. Read Source Modes

```text
Sources:
  Format/process:
    - planning/workflow-activation-map.md @ Doc version: v0.6.0
    - planning/replacement-file-generation-guide.md @ Doc version: v0.1.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
  Internal dependencies:
    - Traversal Depth
  Not checked:
    - implementation evidence not checked unless a route requires it
```

Read source mode answers: where should the chat read from if reading is needed?

| Mode | Meaning | Limits |
|---|---|---|
| Current conversation | Use user corrections, discussion deltas and prior assistant context. | Not enough for current repo truth. |
| Canvas | Use/update the active canvas draft when a draft lives there. | Canvas content may still need source checks if claims changed. |
| Archive | Use uploaded archive snapshot for read-only checks. | Does not prove remote state if archive freshness is uncertain. |
| GitHub / repo | Read current remote branch files. | Still respect no broad audit / targeted check rules. |
| Uploaded explicit file | Use the file the user attached or named for this pass. | Does not automatically become a default source. |

Archive source mode:

```text
- means a fresh archive/source snapshot has been provided or selected for reading;
- does not request archive/package generation by itself;
- does not decide how much to check;
- only decides where to read from if checks are needed;
- can combine with full / targeted / reuse traversal;
- if user says `арх` without attaching a new archive, use the latest uploaded archive in the current conversation.
```

GitHub/repo source mode is still required when:

```text
- a GitHub write needs the exact target file content and SHA;
- the answer claims remote/current branch cleanliness and archive freshness is not enough;
- current implementation/status evidence must be checked from the current branch.
```

## 6A. Output Modes

```text
Sources:
  Format/process:
    - planning/replacement-file-generation-guide.md @ Doc version: v0.1.0
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Read Source Modes
  Not checked:
    - archive/output workflow local source pass outside ROOT-SRC-2A
```

Read source mode and output mode are different.

```text
Read source mode
  = where the chat reads from.

Output mode
  = what the chat produces.
```

Archive wording can mean two different things:

| User wording | Meaning | Required action |
|---|---|---|
| `арх`, `из архива`, `use archive` | Archive as read source. | Use archive snapshot for read-only checks when needed. |
| `давай архив`, `собери архив`, `replacement package`, `archive for manual apply` | Archive as output package. | Use `planning/replacement-file-generation-guide.md` and produce a full replacement package; no patches or planning-only answer. |

When the user asks for an archive/package output, the chat must read:

```text
planning/replacement-file-generation-guide.md
```

If the user explicitly asks for repo-stored review diff transfer, the chat must also read:

```text
planning/documentation/review-diff-file-workflow.md
```

Replacement package output for `давай архив` must contain complete replacement/add files under `replacement-files/<repo-relative-path>`, plus `MANIFEST.md` and `APPLY.md`. It is full replacement archive mode: no patches, no patch files, no diff-only packages, no snippet-only packages and no planning-only answer. Default post-apply review transfer saves the scoped diff to a local file and copies it to clipboard. Review-diff-file mode is explicit-only.

## 7. Source Model

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.8.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Read Source Modes
  Not checked:
    - slice source-sync register not created yet
```

Default/expected sources and sources used in this pass are different.

```text
Default/expected sources
  belong in templates, source blocks or section definitions.

Sources used this pass
  belong in the chat answer/update report.

Additional sources named by user
  are used this pass;
  do not automatically become default sources.
```

Promote a source to default only through explicit docs/template/workflow update.

Source dependency/link commands are routed by this map, but dependency classification and local/register update rules are owned by:

```text
planning/source-cascade-sync-workflow.md
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/source-usage-cascade-profile.md
```

When the target file is in a layer/root scope with a register, also read the relevant register before deciding whether a local `Sources:` block or register row must be updated:

```text
planning/root-source-sync-register.md when root planning/workflow/router files are involved
planning/domain/domain-source-sync-register.md when domain aggregate/value-object files are involved
planning/slices/slice-source-sync-register.md once created when slice files are involved
```

## 8. Source Delta

```text
Sources:
  Format/process:
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.8.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Source Model
  Not checked:
    - response workflow local source pass outside ROOT-SRC-2A
```

Use Source Delta when an answer/draft changes because of:

```text
- user correction;
- new source;
- recheck;
- self-correction;
- source coverage change;
- active draft update.
```

Recommended shape:

```text
Source Delta

Previous basis:
- ...

Newly used this pass:
- ...

User-provided sources/constraints:
- ...

Not rechecked:
- ...

Impact:
- ...

Changed conclusion?
- yes/no

Changed draft text?
- yes/no

Should any source become default later?
- no / maybe / yes, needs docs update
```

Use a shorter version when the update is simple.

## 8A. Response Level And File Update Overview References

```text
Sources:
  Format/process:
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Output Modes
    - Source Delta
  Not checked:
    - Goal Map/output workflow source passes outside ROOT-SRC-2A
```

Answer depth is governed by:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

This use-case map may mark expected response level or output shape in the `Expected output` column, but it does not own Level 1/2/3 trigger logic.

File Update Overview / `План файл-обновление` process and shape are owned by:

```text
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
```

Use-case rows may reference File Update Overview / `План файл-обновление` as expected output, but must not duplicate its workflow or template logic.

The canonical prior-discussion recheck command is:

```text
обс
```

`обс` means context/discussion recheck. Its semantics are owned by `reviewable-agent-output-and-commands-workflow.md`.

Repo Structure / `вспомни структуру репо` command behavior is owned by:

```text
planning/repo-structure-memory.md
```

The command reconstructs known repository root areas, documentation/source-of-truth layers, active workstream files and next files to read. It does not grant edit, archive/package, commit or push permission.


Explicit Review Diff File / repo-stored archive review transfer behavior is owned by:

```text
planning/documentation/review-diff-file-workflow.md
```

Default archive output uses a saved diff copied to clipboard. Review-diff-file mode is explicit-only; when used, the apply command may push only `_ai-review-diffs/last-archive.diff`. Do not create or commit `_ai-review-diffs/last-archive-summary.md` by default. Real archive files must not be committed or pushed before diff review.


Response block and response modifier commands such as `кп`, `саммари`, `кц`, `карта цели кратко`, `goal map brief`, `крит`, `критически`, `critical review`, `план файл-обновление`, legacy `итог` and `отличия драфта` are routed in this file, but their behavior is owned by `reviewable-agent-output-and-commands-workflow.md`.

Goal Map maintenance commands such as `синх карта` are routed in this file, but their behavior is owned by `planning/goal-map-principles-workflow-template.md`.

Critical review / `крит` command behavior is owned by:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

`крит` is a modifier command. When combined with another use-case command, keep the underlying route and apply critical-review answer mode. The command does not grant edit, archive/package, commit or push permission.

Goal Map / `Карта цели` commands are owned by:

```text
planning/goal-map-principles-workflow-template.md
```

Examples:

```text
planning/goal-map-example.md
planning/documentation/examples/GOAL-MAP-BRIEF-RESPONSE-EXAMPLE.md
```

Use-case rows may reference Goal Map or Goal Map Brief as expected output, but they do not own the full map format, brief format or workflow.

Current planning-state / `положняк` output is owned by:

```text
planning/CURRENT-PLANNING-STATE-TEMPLATE.md
```

The `положняк` command is explicit-only. Do not implicitly include it in Level 2 answers, Goal Map Brief or normal planning responses. Its example sections are illustrative; output only scope-relevant areas.

Goal Map planning/reference behavior:
  When planning inside an active long-running workstream, the chat should read or reuse the relevant living Goal Map before choosing the next slice/step. For the active command-system/Tampermonkey workstream, use:
    planning/workstreams/command-system-and-tampermonkey-goal-map.md

  The Goal Map should be kept meaningful and representative: update roadmap, readable scenario/slice names, statuses, evidence-backed DONE records, current focus, next action and open decisions when a meaningful batch/decision changes the workstream.

Tampermonkey command-helper discovery behavior:
  Tampermonkey inserted command bodies are not command authority. They are editable route hints that should lead the chat back to this root use-case map and linked owner workflows/examples.

  When a task mentions Tampermonkey, command helper, command palette, inserted command bodies or helper command profiles, read:
    planning/workstreams/tampermonkey-command-projection-plan.md
    tools/tampermonkey/README.md
    tools/tampermonkey/IMPLEMENTATION-NOTES.md, when implementation behavior or UI details matter

  If the userscript or projection plan conflicts with this root map or an owner workflow, this root map / owner workflow wins.

## 9. Repeated / Continuation Commands

```text
Sources:
  Format/process:
    - planning/documentation/field-kits/root-use-case-map-field-kit.md @ Doc version: v0.1.0
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.8.0
    - planning/replacement-file-generation-guide.md @ Doc version: v0.1.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Active Context Rule
    - Output Modes
    - Source Model
  Not checked:
    - command example files remain example-only and not command authority
```

Reusable command setup owner:

```text
planning/documentation/field-kits/root-use-case-map-field-kit.md
```

This root map remains the concrete Enman route table.

| User says | If active context exists | If no active context exists | Traversal depth | Read source mode | Expected output |
|---|---|---|---|---|---|
| `драфт`, `давай драфт`, `покажи драфт` | Show/update active draft. | Ask target or start draft workflow if target is obvious. | Reuse / targeted. | Conversation / canvas / sources if changed. | Latest draft version or canvas update. |
| `обнови`, `обнови драфт`, `актуализируй` | Apply latest discussion deltas to active draft/answer/plan. | Ask target unless obvious. | Reuse / targeted. | Conversation / canvas; source check only if needed. | Updated version or “already current”. |
| `уточни` | Same scope, more precise wording/boundary. | Clarify target if ambiguous. | No traversal / reuse / targeted. | Conversation; sources only if precision depends on evidence. | Clarified answer/section. |
| `расширь` | Add depth/examples/edge cases without silently changing scope. | Ask what to expand if ambiguous. | Reuse / targeted. | Conversation plus named/new sources if needed. | Expanded section/answer with scope note. |
| `кп`, `key points` | Force a `Key points first` block for the active answer. | Add key points if the requested answer exists or ask what answer to summarize. | No traversal / reuse. | Conversation; sources only if key points depend on evidence. | `Key points first` block plus normal answer structure. |
| `без кп`, `без key points` | Suppress `Key points first` for the active answer. | Apply to the current answer if target is obvious. | No traversal / reuse. | Conversation. | Answer without key-points preview. |
| `саммари` | Add contextual `Краткое саммари`. | Summarize current answer/context or ask target. | No traversal / reuse; targeted if evidence changed. | Conversation plus sources only if needed. | `Краткое саммари`, not file-change `План файл-обновление`. |
| `планируй`, `распланируй`, `plan` | Produce a concrete plan now. If the request belongs to an active long-running workstream, use the relevant living Goal Map to choose the next slice/step and preserve current-state continuity. | Ask for target/scope if no active context exists. | Reuse / targeted; full if goal/scope changed. | Conversation plus living Goal Map and relevant repo docs when planning depends on project state. | Concrete plan with current Goal Map reference, chosen slice/step, batch boundary, expected evidence and next action. |
| `план файл-обновление`, `спланируй файл-обновление`, `спланируй обновление файлов`, `спланируй архив`, `план архива`, `file update plan`, `archive plan` | Produce a concrete file/docs/code/archive update plan and end with `План файл-обновление` in planned mode. | Ask target/scope if the file/update target is unclear. | Reuse / targeted; full if target files or source requirements are unclear. | Conversation plus target repo files/GitHub/archive sources as needed for file/update planning. | Main plan plus `План файл-обновление`: status `planned`, change groups, boundaries, delivery-safety checks and next action. Does not edit files or create archive unless separately requested. |
| `создай команду`, `создай новую команду`, `добавь команду`, `спланируй команду`, `new command`, `create command`, `add command` | Plan or create a command route by rules/template, including command family, owner semantics, UCM row, example coverage, Goal Map/workstream impact and Tampermonkey decision gate. | Use active command-system/Tampermonkey workstream only if the target command is clear; otherwise ask what command must be created or changed. | Targeted/full by command scope; full if owner semantics or permission boundary are unclear. | `planning/documentation/command-creation-workflow.md`, this root map, use-case map workflow/template, reviewable command workflow, examples index and Tampermonkey projection plan when helper support is in scope. | Command creation plan or replacement archive when separately requested. Does not edit files, update Tampermonkey, create archive, commit or push unless the user explicitly includes that action in scope. |
| `карта процесса`, `карта цели`, `план процесса`, `где мы`, `прогресс`, `статус цели`, `goal process`, `goal map` | Show/update the Goal Map for the active or named long-running goal. | Ask for the goal if no active/named goal exists. | Reuse / targeted; full if goal/scope changed. | Conversation plus relevant docs/sources when the map depends on repo state. | Goal Map / Карта цели: final picture, target scenarios, process slices, acceptance criteria, current state, decision points, current focus and next action. |
| `кц`, `карта цели кратко`, `goal map brief`, `выведи краткую карту цели` | Show compact Goal Map Brief for the active or named long-running goal. | Ask for the goal if no active/named goal exists. | Reuse / targeted; full only if the living map is missing/stale or scope changed. | Conversation plus relevant living Goal Map; repo sources only when current map state depends on repo evidence. | Goal Map Brief after `Краткое саммари`: current goal, current slice, Done/Now/Next/After for the current slice, and compact status table for other slices. |
| `положняк`, `текущий положняк`, `стейт`, `текущий стейт`, `покажи состояние`, `покажи текущий план`, `current planning state` | Show compact current operational planning/coverage state for the active or named scope. | Ask for scope if there is no active/named state. | Reuse / targeted; full only if current state depends on unchecked repo/register evidence. | Conversation plus relevant living Goal Map/register/source files when the state depends on repo evidence. | `Текущий положняк`: emoji-coded scope-relevant state block using `planning/CURRENT-PLANNING-STATE-TEMPLATE.md`; not implicit in Level 2 or `кц`; not a file-update command. |
| `синх карта`, `синхронизируй карту`, `синх карта архив`, `карта синх архив`, `синхронизируй карту и дай архив`, `sync goal map`, `sync map archive` | Check the relevant living Goal Map for current-state consistency, prepare a narrow map-sync replacement archive, and show the target synced brief in the same response. | Ask for the goal/map only if no active/named living map is clear. | Targeted/full by map inconsistency; always review the living map before output. | Relevant living Goal Map plus owner route/example/action-log files touched by the sync. | Synced target-state Goal Map Brief, archive link, ready-to-run apply/diff commands, then `План файл-обновление`; does not start the next functional slice. |
| `параллельный агент`, `работай параллельно`, `parallel workspace`, `parallel work`, `создай parallel workspace`, `начни параллельную работу`, `старт параллельной работы`, `создай параллельный воркфлоу`, `create parallel workflow`, `start parallel workflow` | Use or plan a staging-only parallel workspace for one agent/workstream when shared canonical docs may conflict with other chats. | Ask for scope if no active workstream or target files are clear; do not create placeholder workspace. | Targeted/full by workspace scope. | Conversation plus current canonical target files and `planning/documentation/parallel-work/` workflow/template docs. | Parallel workspace plan or package when separately requested; does not edit shared canonical docs directly and does not create aggregate sync until a sync-candidate workspace exists. |
| `синх параллельной работы`, `синх parallel work`, `parallel sync`, `aggregate sync`, `синк параллельных воркфлоу` | Plan or execute an aggregate sync from one or more sync-candidate parallel workspaces into canonical docs. | Ask which workspace(s) to sync if unclear. | Targeted/full by included workspaces and target canonical files. | Included workspace files plus current canonical target files and `planning/documentation/parallel-work/parallel-sync-workflow.md`. | Aggregate sync plan / file update plan / archive when separately requested; main action log is appended only after real changed files are known. |
| `крит`, `критически`, `критически оцени`, `проверь критически`, `оцени честно`, `не соглашайся автоматически`, `за и против`, `critical review` | Apply critical-review answer mode to the current target or underlying use-case route. | Ask what to review if the target is unclear. | Reuse / targeted; full if a fair verdict needs source checks. | Conversation plus the relevant sources for the underlying task when evidence matters. | Critical review: target, verdict, strong points, weak points/risks, hidden assumptions, alternatives/adjustments, confidence/checks. |
| `без саммари` | Suppress `Краткое саммари`. | Apply to current answer if target is obvious. | No traversal / reuse. | Conversation. | Answer without contextual summary. |
| `итог`, `покажи план файл-обновление` | Add or update file/change-oriented `План файл-обновление` when file/update context exists; `итог` is legacy shorthand only in this context. | If no file/change/update context exists, explain that `План файл-обновление` is not applicable and offer `Краткое саммари`. | Reuse / targeted. | Conversation plus target files/diff/archive when needed. | File Update Overview / `План файл-обновление`; during planning it is the rolling nearest-batch plan. |
| `без план файл-обновления`, `без итога` | Suppress `План файл-обновление`. | Apply to current answer if target is obvious. | No traversal / reuse. | Conversation. | Answer without File Update Overview / `План файл-обновление`. |
| `полный конец` | Add `Краткое саммари` and `План файл-обновление` when file/change/update overview is applicable. | Add both ending blocks if target is obvious. | Reuse / targeted. | Conversation plus target files/diff/archive when needed. | Context summary followed by final file/change overview. |
| `отличия драфта`, `draft diff` | Show how the active draft changed from the previous version. | Ask for the draft target if no active draft is clear. | Reuse / targeted. | Conversation / canvas / draft sources if changed. | `Отличия от предыдущего драфта` plus updated draft when requested. |
| `обс`, `перепроверь обсуждение`, `context recheck` | Re-check relevant prior discussion, accepted decisions and constraints before answering. | Re-check available prior discussion or say what context is unavailable. | Reuse / targeted; full only if task requires it. | Conversation plus sources only if needed. | Context/discussion recheck result; combine with Level 1/2/3 by task breadth. |
| `вспомни структуру репо`, `структура репо`, `вспомни репо`, `слои репо`, `где что лежит`, `repo structure`, `repo layers` | Reconstruct repo structure and source-of-truth layers for the active task before planning/editing. | Provide current known repo structure or ask for repo/archive/tree if unavailable. | Targeted; full when structure is stale/unknown or task touches unknown layers. | GitHub/repo tree/archive plus root planning docs and active workstream docs as needed. | Repo Structure Brief: root areas, documentation layers, source-of-truth chain, active workstream files, code/tooling/example areas, known vs uncertain, files to read next. |
| `проверь review diff file`, `проверь diff file`, `review diff file` | Read the standard repo-stored review diff for an explicitly requested review-diff-file archive flow and review it before real files are committed. | Ask for repo/branch/path if not clear; do not ask for pasted full diff when repo access is available. | Targeted. | GitHub/repo path `_ai-review-diffs/last-archive.diff`. | Diff review verdict; if OK, scoped real commit/push commands for intended files only. |
| `перепроверь`, `recheck` | Recheck active answer/draft/source coverage. | Recheck last answer or ask target. | Targeted / full depending risk. | Conversation plus relevant sources. | Findings and corrections/no-change result. |
| `сорс`, `укажи сорс`, `добавь source`, `добавь зависимость`, `ссылка на другой файл`, `этот файл зависит от X`, `эта секция зависит от X`, `source dependency`, `dependency link`, `source link` | Classify and declare a file/section dependency on another source file. | Ask for target file/section or source file only if missing. | Targeted; full if source model/register scope changed. | Conversation plus target file, referenced source file, source-cascade workflow/template/profile and relevant root/domain/slice register. | Source dependency decision: target, referenced source, dependency type, version/status label, local `Sources:` need, register impact, not-checked items and next action. No edit/archive without explicit permission. |
| `сорс-импакт`, `классифицируй зависимость`, `source impact`, `dependency classification` | Classify whether a relation creates source/version cascade or is only navigation/read/route linkage. | Ask for target/referenced file or relation only if missing. | Targeted; full only if dependency model or register scope is unclear. | Conversation plus target relation, `planning/source-cascade-sync-workflow.md`, relevant local `Sources:` block/register row when provided. | Source impact decision: relation found, dependency class, cascade decision, why, local Sources impact, register impact, not-checked items and next action. Review/classification mode only; no edit/archive/commit unless separately requested. |
| `стейл версии в регистрах`, `проверь регистры на стейл версии`, `register stale version scan` | Check source-sync registers for stale source paths, stale Doc version labels and rows that claim versions/statuses no longer present in source files. | Ask for register/layer scope if no active scope is clear. | Targeted; full only for explicitly requested layer/all scope. | Relevant source-sync register(s), current referenced source files and `planning/source-cascade-sync-workflow.md`. | Register stale version scan: checked registers, synchronized rows, needs-review rows, stale rows, expected fixes and not-checked scope. Does not edit files or create archive without separate request. |
| `стейл локальные сорсы`, `проверь локальные сорсы`, `local sources stale scan` | Check local `Sources:` blocks and file-level Source Sync sections for stale versions, paths, source relationships and missing register impact. | Ask for target files/layer scope if no active scope is clear. | Targeted; full only for explicitly requested layer/all scope. | Target active files/drafts, their local `Sources:` blocks, referenced source files, relevant registers and `planning/source-cascade-sync-workflow.md`. | Local Sources stale scan: checked files, findings by file/section, stale local sources, register impact and next cleanup plan. Does not edit files or create archive without separate request. |
| `полная проверка сорсов`, `полная source/version проверка`, `full source/version audit` | Run combined source/version consistency review for a bounded scope. | Ask for scope if not clear; do not default to full repo. | Full only for explicit root/domain/slice/all scope; otherwise targeted. | Relevant registers, local `Sources:` blocks, active file headers, referenced source files and source-cascade workflow. | Full source/version audit: scope, coverage state for scope-relevant areas, synchronized/needs-review/stale findings, recommended fixes and do-not-claim limits. Does not edit files or create archive without separate request. |
| `учти файл X` | Incorporate explicit new source into active work. | Use X for the requested new answer. | Targeted. | Uploaded/named file. | Updated answer/draft with Source Delta. |
| `без изм`, `б изм`, `no ch` | Reuse recent context and avoid broad re-audit. | Weak without prior context; ask what state is unchanged if needed. | Reuse / targeted. | Previous context plus targeted reads. | Answer/update with minimal checks. |
| `арх`, `из арх`, `из архива`, `use archive` | Treat the provided/latest archive as the current source snapshot for reads/checks. | Use latest uploaded archive in current conversation, or ask for archive in a new chat. | Does not decide depth. | Archive snapshot. | Read-only answer/check based on archive; do not generate an output package unless separately requested. |
| `б из арх`, `без изм, арх`, `изм нет, арх` | No changes + archive source mode. | Use if latest archive exists; otherwise ask for archive. | Reuse / targeted. | Latest uploaded archive. | No broad audit; targeted archive checks only. |
| `давай архив`, `собери архив`, `replacement package`, `archive for manual apply` | Produce full replacement archive/package for the active approved plan/scope. | Build archive plan from obvious target or ask only blocking target questions. | Targeted/full by package scope. | GitHub/archive/conversation by context. | Full replacement package ZIP with `MANIFEST.md`, `APPLY.md`, `replacement-files/`; no patches, patch files, snippet-only package or planning-only answer; post-apply diff saved to file and copied to clipboard without printing full diff. |
| `давай архив с review diff file`, `давай архив с repo diff`, `archive with review diff file` | Produce archive/package using explicit repo-stored review-diff-file transfer instead of the default clipboard diff. | Build archive plan from obvious target or ask only blocking target questions. | Targeted/full by package scope. | GitHub/archive/conversation by context. | Replacement package ZIP; apply command creates and pushes only `_ai-review-diffs/last-archive.diff`; real files remain local until review approval. |
| `проверь` after replacement archive/package application | Verify the active archive application result. | Ask for status/diff or target archive if not available. | Targeted. | Applied repo state / user-provided diff. | Post-apply verification: expected files changed, no unexpected files in commit scope, diff matches package intent, no unrelated sections/register entries/routing rules were removed; if pasted diff shows mojibake, request/copy full suspect file contents before judging file corruption. |

## 9A. Reusable Command Example References

```text
Sources:
  Format/process:
    - planning/documentation/example-coverage-workflow.md @ Doc version: v0.1.0
    - planning/documentation/field-kits/root-use-case-map-field-kit.md @ Doc version: v0.1.0
  Content:
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Repeated / Continuation Commands
  Not checked:
    - full examples index source pass outside ROOT-SRC-2A
```

These references keep reusable command examples visible from the root route chain without copying example bodies into the root map.

Examples demonstrate valid execution only. They do not own command semantics, routing, source truth, output mode or permission boundary.

| Route family | User wording / rows | Example | When to read | Notes |
|---|---|---|---|---|
| Answer shape / response blocks | `кп`, `key points`, `саммари`, `полный конец`, Level 2 response shape | `planning/documentation/examples/LEVEL-2-KEY-POINTS-SUMMARY-EXAMPLE.md` | Read when Key points / `Краткое саммари` / Level 2 response-shape behavior is non-trivial or disputed. | Owner semantics stay in `planning/documentation/reviewable-agent-output-and-commands-workflow.md`. |
| Planning command | `планируй`, `спланируй`, `давай план`, `распланируй`, `plan` | `planning/documentation/examples/PLAN-COMMAND-VALID-EXECUTION-EXAMPLE.md` | Read when a plan must show how `планируй` means concrete planning now and does not grant edit/package permission. | Combine with living Goal Map when the plan belongs to an active long-running workstream. |
| Archive source vs output package | `арх`, `из архива`, `use archive`, `давай архив`, `собери архив`, `replacement package` | `planning/documentation/examples/ARCHIVE-SOURCE-VS-OUTPUT-PACKAGE-EXAMPLE.md` | Read when archive wording may be confused between read-source mode and package-output mode. | Owner output rules stay in `planning/replacement-file-generation-guide.md` and this root map. |
| Goal Map / progress navigation | `карта процесса`, `карта цели`, `план процесса`, `где мы`, `прогресс`, `статус цели`, `goal map` | `planning/goal-map-example.md` | Read when the chat needs to show or update the Goal Map shape, not just report a short status. | Owner rules stay in `planning/goal-map-principles-workflow-template.md`; active state lives in the relevant workstream map. |
| Goal Map Brief / compact progress projection | `кц`, `карта цели кратко`, `goal map brief`, `выведи краткую карту цели` | `planning/documentation/examples/GOAL-MAP-BRIEF-RESPONSE-EXAMPLE.md` | Read when the chat needs the compact in-answer Goal Map projection rather than the full living map. | Owner rules stay in `planning/goal-map-principles-workflow-template.md`; response placement stays in `planning/documentation/reviewable-agent-output-and-commands-workflow.md`. |
| Goal Map Sync command | `синх карта`, `синхронизируй карту`, `синх карта архив`, `карта синх архив`, `sync goal map` | `planning/documentation/examples/GOAL-MAP-SYNC-COMMAND-EXAMPLE.md` | Read when the chat needs to check living-map consistency, produce a map-sync archive and show the target synced brief in one response. | Owner rules stay in `planning/goal-map-principles-workflow-template.md`; this command includes archive output but not commit/push permission or next-slice implementation. |
| Critical Review command | `крит`, `критически`, `критически оцени`, `проверь критически`, `не соглашайся автоматически`, `critical review` | `planning/documentation/examples/CRITICAL-REVIEW-COMMAND-EXAMPLE.md` | Read when the chat needs to apply honest critical evaluation to a plan, decision, draft, answer or route. | Owner rules stay in `planning/documentation/reviewable-agent-output-and-commands-workflow.md`; this is a response modifier, not edit/package permission. |
| Plan file update command | `план файл-обновление`, `спланируй файл-обновление`, `спланируй обновление файлов`, `спланируй архив`, `план архива` | `planning/documentation/examples/PLAN-FILE-UPDATE-COMMAND-EXAMPLE.md` | Read when the chat needs to plan file/docs/code/archive updates and end with `План файл-обновление` in planned mode. | Owner shape stays in `planning/documentation/file-update-overview-workflow.md` and `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md`; this command does not grant edit/archive permission. |
| Enman scenario/domain/slice route families | `сделай сценарий`, `DATA`, `разбери domain draft`, `задрафти slice`, `server slice`, `client sidecar`, `разбери тестирование` | `planning/documentation/examples/project-specific/enman/SCENARIO-DOMAIN-SLICE-COMMAND-ROUTING-EXAMPLE.md` | Read when updating or reviewing Enman scenario/domain/slice command routes. | Profile-specific reusable demonstration for Enman/scenario-driven planning-app route families; route logic remains in this root map and reusable setup guidance remains in the profile field kit. Example fit must be user/project accepted before wiring similar examples into another concrete root map. |

## 10. Primary Use Case Table

```text
Sources:
  Format/process:
    - planning/workflow-activation-map.md @ Doc version: v0.6.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.4.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.8.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Repeated / Continuation Commands
    - Source Model
  Not checked:
    - non-root layer route rows not fully re-audited in ROOT-SRC-2A
```

Scenario/domain/slice use-case setup note:

```text
Use planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md when adding or changing scenario/domain/slice route rows in this root map.
Do not treat that field kit as an activated workflow for ordinary scenario/slice/domain work.
```

| User says | Task type | Active context? | Traversal depth | Read source mode | Activated workflows | Required reads | Source of obligation | Expected output | Permission boundary |
|---|---|---:|---|---|---|---|---|---|---|
| “новый чат”, “вкатись”, “разберись с planning docs”, “first pass”, no reliable active context | New Chat Onboarding / first planning pass | New or uncertain | Full first time; targeted/reuse after checked context exists | GitHub/repo by default; archive only if user requests archive source mode or provides an archive | workflow activation; planning agent protocol; role identification when specialized work is likely; reviewable output | `planning/README.md`, `planning/workflow-activation-map.md`, `planning/planning-use-case-map.md`, `planning/planning-agent-protocol.md`, `planning/planning-doc-responsibility-map.md`; `planning/agent-roles-and-required-actions.md` when role matters; relevant living Goal Map and Tampermonkey projection docs when active workstream/helper context is in scope | README + use-case map + planning agent protocol | Short onboarding preflight, selected next use case, checked/not-checked sources, Goal Map staleness note when applicable, next action | No edits/packages unless explicitly requested |
| “посмотри / распланируй / проверь docs” | Non-trivial planning/docs work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | workflow activation; reviewable output | `planning/README.md`, `workflow-activation-map.md`, `planning-doc-responsibility-map.md` | README + activation map | Level 2 reviewable answer/plan | Edits require approval |
| “обнови docs” | Documentation update | Optional | Targeted/full by scope | GitHub for writes; archive for read-only if requested | docs update plan/workflow; local-global sync | documentation README/map/workflows; `planning/shared-visibility-map.md` when shared visibility is involved | activation map + docs workflows | Level 2 update plan or applied update + File Update Overview / `План файл-обновление` when files are planned/changed/reviewed | GitHub writes require approval |
| “создай planning file”, “новая концепция”, “введи концепцию”, “concept kit”, “principles/workflow/template в одном”, “пример отдельно” | Documentation file creation / new concept artifact setup | Optional | Full first time; targeted later | GitHub/archive/conversation by context | docs update workflow; documentation responsibility routing; example coverage decision; source-cascade workflow when the new file becomes a source | `planning/documentation/planning-docs-architecture-principles.md#9a-compound-starter-artifact-principle`, `planning/documentation/planning-docs-architecture-principles.md#13a-filename-version-migration-policy`, `planning/documentation/documentation-responsibility-map.md`, `planning/documentation/example-coverage-workflow.md`; relevant owner workflow/template if one already exists | docs architecture principles + documentation responsibility map + example coverage workflow | Placement decision and update plan or replacement package; one compound starter artifact plus a separate example when appropriate; split-later conditions recorded | Edits/packages require approval; do not perform filename-version migration or broad file splits without a separate approved batch |
| “добавь template / workflow / command / draft format / пример” | Documentation governance update | Optional | Full first time; targeted later | GitHub/archive/conversation by context | docs update workflow; example coverage decision when template/output shape changes; reviewable output | documentation README/map/workflows; `planning/documentation/field-kits/root-use-case-map-field-kit.md` when command/use-case routes change; `planning/documentation/example-coverage-workflow.md` when template/output/command/draft example coverage matters; relevant owner workflow/template; principle section references in §3A, including compound starter artifact policy when a new concept combines principles/workflow/template | docs architecture principles + use-case map + example coverage workflow | Level 2 update plan or replacement package + File Update Overview / `План файл-обновление` when files are planned/changed/reviewed; example coverage decision if applicable; compound starter artifact + separate example decision when introducing a new cohesive concept | Edits/packages require approval |
| “ревью зон ответственности”, “что куда относится”, “generic vs project-specific”, “portability review”, “док слой реюзабл” | Documentation responsibility-zone / portability review | Optional | Full first time; targeted later | GitHub/archive/conversation by context; `арх` means source snapshot only | responsibility-zone review workflow; docs architecture principles; documentation responsibility map | `planning/documentation/documentation-responsibility-zone-review-workflow.md`, `planning/documentation/PORTABLE-STARTER-KIT.md` when copying/adapting reusable docs into a new project, `planning/documentation-migration/documentation-layer-portability-migration-plan.md`, `planning/documentation/planning-docs-architecture-principles.md`, `planning/documentation/documentation-responsibility-map.md`; target docs under review | responsibility-zone review workflow + portability migration plan | Level 2 classification table: reusable core, specialized profile, adapter mapping, example candidate, correct owner/action + File Update Overview if file changes are planned | Do not review scenario/slice/domain/API content itself unless separately requested; edits/packages require approval |
| “ссылка на другой файл”, “зависимость от файла”, “этот файл зависит от X”, “эта секция зависит от X”, “укажи сорс”, “добавь source”, “source dependency”, “dependency link”, “source link” | Source dependency declaration / local `Sources:` + register impact check | Optional | Targeted; full if target/source/register scope is unclear or source model changed | GitHub/archive/conversation by context | source cascade sync workflow; source section template; Enman source usage profile; relevant root/domain/slice register; file-type workflow/template for the target file | `planning/source-cascade-sync-workflow.md`, especially explicit link/dependency declaration rule; `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/source-usage-cascade-profile.md`; target file/section; referenced source file; `planning/root-source-sync-register.md` when root files are involved; `planning/domain/domain-source-sync-register.md` when domain files are involved; `planning/slices/slice-source-sync-register.md` once created when slice files are involved; target file-type workflow/template/responsibility map when local section shape is needed | source cascade sync workflow + local Sources template + relevant layer/root register | Level 2 source-dependency decision: target file/section, referenced source, dependency type, source version/status label, local `Sources:` need, register impact, not-checked sources and next action; replacement package only when separately requested | Do not edit files/registers without explicit permission; do not invent source versions; do not claim register synchronization until local `Sources:` blocks or file-level audit prove it |
| “сорс-импакт”, “классифицируй зависимость”, “source impact”, “dependency classification” | Source-impact classification / cascade trigger review | Optional | Targeted; full only if relation or dependency model is unclear | GitHub/archive/conversation by context | source cascade sync workflow; source section template/profile; relevant local `Sources:` block or register row when provided | `planning/source-cascade-sync-workflow.md`, especially Dependency Strength / Cascade Trigger and Source Impact Classification Output; target/referenced files or relation being classified; relevant register only when register impact is in scope | source cascade sync workflow | Level 2 Source impact decision: relation found, dependency class, cascade decision, why, local Sources impact, register impact, not-checked items and next action | Review/classification only; do not edit files/registers, create archives, commit or push unless separately requested |
| “source usage”, “source/version”, “каскад зависимостей”, “stale downstream”, “версии сорсов”, “инкапсуляция слоёв”, “локальные Sources blocks” | Source usage / cascade governance, local section source drafting or pilot work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | source cascade sync workflow; source usage field kit; Enman source usage profile; aggregate/server slice section source templates when draft sections are involved; principles §12A/13/14/15; PMR-002 until layer registers/full workflow exist | `planning/source-cascade-sync-workflow.md`, `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`, `planning/source-usage-cascade-profile.md`; `planning/domain/domain-source-sync-register.md` when domain/aggregate source dependencies or downstream sync impact are involved; `planning/documentation/field-kits/source-usage-cascade-field-kit.md` when setting up/reviewing source-usage model; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md` when adding/reviewing aggregate section `Sources:` blocks; `planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md` when adding/reviewing server slice section `Sources:` blocks; `planning/source-usage-pilots/README.md`; pilot register if user asks to work on a pilot; relevant source/domain/slice docs only for real pilot fill | layer encapsulation principle + source cascade sync workflow + source usage field kit + Enman source usage profile | Level 2 governance plan, local section source plan, pilot skeleton, pilot fill plan or replacement package + File Update Overview / `План файл-обновление` when files are planned/changed/reviewed | Do not pretend layer registers/full cascade workflow exist before local `Sources:` blocks prove the shape; do not add ad hoc version fields outside the doc-version convention |
| “давай архив”, “собери архив”, “replacement package”, “archive for manual apply” | Replacement archive/package generation | Optional | Targeted/full by package scope | GitHub/archive/conversation by context | replacement file generation guide; docs update workflow when docs change; reviewable output | `planning/replacement-file-generation-guide.md`, target files, docs update workflow when docs are changed; `planning/documentation/review-diff-file-workflow.md` only for explicit review-diff-file mode | replacement guide + workflow activation | Full replacement archive response only: ZIP package with `MANIFEST.md`, `APPLY.md`, `replacement-files/<repo-relative-path>` complete files; apply/diff commands in chat; saved diff copied to clipboard | Direct repo edits not allowed unless separately approved; no patches, no patch files, no snippet-only package, no planning-only answer; if complete current file contents cannot be obtained, stop and ask for a fresh archive or full target-file copies |
| “файл большой”, “дай ps1”, “скрипт для большого файла”, “архив неудобен” | Large/shared file update delivery choice | Optional | Targeted/full by file risk | Fresh archive/current full file preferred; script only if complete replacement unsafe | docs update workflow; replacement guide; local targeted script mode if fallback needed | `planning/documentation/documentation-update-workflow.md`, `planning/replacement-file-generation-guide.md`, target file | delivery safety check + replacement guide | Level 2 delivery plan: prefer fresh full repo/archive + safe complete replacement; one-file targeted script only as fallback | Do not choose script only because file is large; scripts require explicit mode and no auto-commit |
| “поправь ссылки во многих файлах” | Mechanical link/path/name sync | Optional | Targeted/broad search | GitHub or archive | docs update workflow; local-global sync | target files/search source | docs update workflow output mode rules | Level 2 link-sync plan + File Update Overview / `План файл-обновление`; one bundled commit when tool-supported | Bundled/bulk mode should be used when approved; if unavailable, stop and disclose before per-file writes |
| “задрафти slice” | Slice draft work | Usually new | Full first time | Sources required by slice workflow | slice workflow chain; source cascade sync workflow when local section sources/doc versions are involved; testing selector when needed | slice README/map/workflow/principles/test workflow/template; `planning/source-cascade-sync-workflow.md` and `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` when adding/reviewing local `Sources:` blocks; `planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md` when the slice is server/backend/API; `planning/testing/testing-responsibility-map.md` when test-layer-specific guidance is needed | activation map + slice README/map + source cascade workflow when local section sources are requested | Slice draft/plan with local `Sources:` blocks when requested/in scope | File writes require approval |
| “задрафти server slice” | Server slice draft | New/active | Full first time; targeted update | Scenario/domain/API/testing sources | slice + server drafting workflows; source cascade sync workflow + server slice section source template when local section sources/doc versions are involved; testing selector when needed | server workflow/template/principles; `planning/source-cascade-sync-workflow.md`, `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`, `planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md` when adding/reviewing local `Sources:` blocks; `planning/slices/slice-test-plan-workflow.md`; `planning/testing/testing-responsibility-map.md`; `planning/testing/server-slice-test-plan-rules.md` when server/API test guidance is needed | slice README/map + server workflow/template + source cascade workflow when local section sources are requested | Server/backend/API draft with local `Sources:` blocks when requested/in scope | File writes require approval |
| “задрафти client sidecar” | Client slice draft | New/active | Full first time; targeted update | UI/scenario/client/testing sources | slice + client drafting workflows; testing selector when needed | client workflow/template/principles/CSS/a11y/form docs; `planning/slices/slice-test-plan-workflow.md`; `planning/testing/testing-responsibility-map.md`; `planning/testing/test-object-patterns.md` when client/E2E object patterns matter | slice README/map | `.client.md` draft | File writes require approval |
| “сделай cross-cutting umbrella” | Cross-cutting coordination doc | New/active | Full first time | Cross-cutting behavior and side draft sources | slice workflow + cross-cutting template | cross-cutting README/template | slice README/map | Umbrella coordination draft | File writes require approval |
| “проверь current/implemented status” | Status reconciliation | Optional | Targeted/full by claim | Current branch evidence unless archive explicitly accepted | status reconciliation | `planning/status-evidence-profile.md`; status workflow + code/tests/generated artifacts | activation map status chain | Level 2 status findings/sync plan | Current-state claims need evidence |
| “сделай сценарий / DATA / UI scenario / behavior items” | Scenario source work | Optional | Full first time; targeted update later | Scenario/source files or archive | scenario responsibility map; artifact map; scenario drafting workflow | `planning/diagrams/README.md`, `scenario-responsibility-map.md`, `scenario-artifact-map.md`, `scenario-drafting-workflow.md`, `planning/scenario-specification-principles.md`, relevant text/DATA/UI/behavior/clarification files | activation map + use-case map + scenario responsibility map | Scenario text / DATA / UI scenario / behavior item draft or update plan | Edits require approval |
| “сделай диаграммы / обнови draw.io / подготовь diagram prompt / проверь диаграммы” | Diagramming work | Optional | Full first time; targeted update later | Diagramming docs plus scenario/domain/testing/thesis sources as needed | diagramming responsibility map; diagram source consistency; diagram prompt/draw.io workflows | `planning/diagramming/README.md`, `planning/diagramming/diagramming-responsibility-map.md`, `planning/diagrams/scenario-diagram-consistency-report.md`, `planning/diagrams/diagram-prompt-generation-workflow.md` or `planning/diagrams/drawio-diagram-generation-workflow.md`; source layers required by requested pages | activation map + use-case map + diagramming responsibility map | Diagram source audit / diagram prompt / batch plan / draw.io artifact plan or package | File/artifact writes require approval |
| “разбери domain draft / domain discovery / aggregate draft” | Domain work | Optional | Full first time; targeted later | Domain docs/scenario sources/current code if needed | domain responsibility/discovery/aggregate/value-object workflows by scope; source cascade sync workflow + aggregate section source template when local section sources/doc versions are involved | `planning/domain/README.md`, `planning/domain/domain-responsibility-map.md`, `planning/domain/scenario-to-aggregate-map.md`; `planning/domain/domain-source-sync-register.md` when reviewing existing aggregate source dependencies or preparing downstream slice refactor; `planning/source-cascade-sync-workflow.md`, `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`, `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md` when adding/reviewing aggregate local `Sources:` blocks; old `planning/tables/domain-drafts/` only as historical/cross-check source | root responsibility map + domain responsibility map + domain source-sync register when existing dependencies/downstream impact are in scope + source cascade workflow when local section sources are requested | Domain discovery map / aggregate draft / value-object draft / review plan with local `Sources:` blocks when requested/in scope | Edits require approval |
| “разбери тестирование” | Testing layer work | Optional | Targeted/full by scope | Testing docs/current tests if needed | testing responsibility map + selected testing workflows | `planning/testing/README.md`, `planning/testing/testing-responsibility-map.md`, `planning/testing/testing-principles.md`, specific testing docs by selector | root responsibility map + testing responsibility map | Testing audit/plan | Edits require approval |
| “обнови API/OpenAPI rules” | API planning work | Optional | Targeted/full by scope | API docs/generated artifacts if status claims | API/docs workflows | `planning/api/README.md` | root responsibility map | API docs plan/update | Generated changes need explicit scope |
| “сделай VKR текст” | VKR/thesis wording | Optional | Targeted | Clean docs + evidence as needed | reviewable output | `planning/vkr-clean-reference.md` | planning README | Clean thesis wording | Do not use internal labels |

## 11. Detailed Trace: New Chat Onboarding

```text
Sources:
  Format/process:
    - planning/README.md @ Doc version: v0.3.0
    - planning/workflow-activation-map.md @ Doc version: v0.6.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.4.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Primary Use Case Table
  Not checked:
    - protocol/role files covered by ROOT-SRC-2B; downstream role-specific workflows not re-audited
```

User says:

```text
новый чат / вкатись / разберись с planning docs / first pass / restored context / no reliable active context
```

This is a use-case route, not a new global workflow. After onboarding selects the concrete task, switch to the specific use-case row and traversal depth.

Steps:

```text
1. Read planning/README.md.
2. Read planning/workflow-activation-map.md.
3. Read planning/planning-use-case-map.md.
4. Read planning/planning-agent-protocol.md.
5. Read planning/agent-roles-and-required-actions.md when a specialized role may be needed.
6. Read planning/planning-doc-responsibility-map.md to choose the layer.
7. If the task touches an active long-running workstream, Goal Map command or next-step choice, read the relevant living Goal Map and `planning/goal-map-principles-workflow-template.md`.
8. If the task touches Tampermonkey helper commands or inserted command bodies, read `planning/workstreams/tampermonkey-command-projection-plan.md` and `tools/tampermonkey/README.md` after the root route is known.
9. For documentation-layer work, read documentation README/map/workflows as required by planning-agent-protocol.md.
10. Select traversal depth:
   - full for first use / new chat / changed scope;
   - targeted or reuse only after recent checked context exists.
11. Select read source mode:
   - GitHub/repo for current remote docs;
   - archive only if user explicitly wants archive source mode or provides archive;
   - conversation only for user corrections/discussion deltas.
12. Select output mode:
   - answer/plan by default;
   - replacement archive only when user says “давай архив” / package output;
   - direct edits only when explicitly approved.
13. Use accepted command / no reinvention rule.
14. Use Docs DRY: link to owner files, do not copy owner logic.
15. Produce compact reviewable answer:
   - mode/source;
   - selected use case;
   - checked sources;
   - not checked / limits;
   - next action.
```

Do not:

```text
- perform broad audit unless user asks or first-pass scope requires it;
- ask the user to name workflow files;
- treat planning docs as implementation proof;
- change files without explicit permission;
- create archive/package unless requested;
- use stale L1/L2 terminology as user-facing routing;
- copy owner logic into the onboarding row.
```

## 12. Detailed Trace: Active Draft Continuation

```text
Sources:
  Format/process:
    - planning/documentation/reviewable-agent-output-and-commands-workflow.md @ Doc version: v0.1.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Active Context Rule
    - Source Delta
  Not checked:
    - response workflow source pass outside ROOT-SRC-2A
```

User says:

```text
давай драфт / драфт / покажи драфт / обнови / обнови драфт
```

If active draft exists:

```text
1. Identify active draft.
2. Identify changes since the last version.
3. Decide traversal depth.
4. Decide read source mode.
5. Apply updates.
6. Update canvas or output latest draft.
7. Report Source Delta if sources changed.
```

If no active draft exists:

```text
- clarify target; or
- start a new draft workflow only if the target is obvious.
```

## 13. Detailed Trace: Template / Workflow / Command / Example Governance

```text
Sources:
  Format/process:
    - planning/documentation/planning-docs-architecture-principles.md @ Doc version: v0.1.0
    - planning/documentation/example-coverage-workflow.md @ Doc version: v0.1.0
    - planning/documentation/use-case-map-workflow.md @ Doc version: v0.1.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Principle Section References
    - Reusable Command Example References
  Not checked:
    - documentation-layer workflow source pass outside ROOT-SRC-2A
```

User says:

```text
добавь template / workflow / command / draft format / пример
```

Steps:

```text
1. Use workflow-activation-map.md and disclose activated workflows when non-trivial.
2. Use documentation responsibility routing to find the owner file.
3. Re-read the relevant principle sections from §3A instead of copying their logic here.
4. Decide whether the change creates or changes a template, output shape, command behavior, draft format or new cohesive documentation concept.
5. If the new concept needs principles + workflow + template but is still early/cohesive, prefer one explicitly named compound starter artifact plus a separate example instead of three files.
6. If there is a template or compound starter artifact, use planning/documentation/example-coverage-workflow.md and decide that a working example is needed by default unless the owner file is routing-only or the example would duplicate another example.
7. If an example is not added, record the reason in the relevant index or plan.
8. Examples may link to owner use cases/workflows/templates, but must not duplicate routing, source-mode, output-mode or permission logic.
9. Keep source/output/permission logic in concrete use-case maps and owner workflows.
10. Use planning/documentation/use-case-map-workflow.md and planning/documentation/USE-CASE-MAP-TEMPLATE.md when creating or restructuring use-case maps.
11. Do not perform filename-version migration, file renames or broad splits inside an unrelated command/template/example update.
12. Use replacement archive/package output only when the user asks for an archive/package or after approval.
```

Deferred:

```text
Use-case rows do not carry ad hoc source-version fields. Use accepted `Doc version:` headers, source-sync registers and local `Sources:` blocks where the source-cascade workflow requires them. Filename-version migration is tracked as a separate policy and must not be performed as an incidental rename.
```

## 14. Detailed Trace: Replacement Archive / Package Output

```text
Sources:
  Format/process:
    - planning/replacement-file-generation-guide.md @ Doc version: v0.1.0
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Internal dependencies:
    - Output Modes
    - Accepted Command / No Reinvention Rule
  Not checked:
    - archive/output workflow local source pass outside ROOT-SRC-2A
```

User says:

```text
давай архив / собери архив / создай replacement package / archive for manual apply
```

Steps:

```text
1. Treat this as output mode, not archive read-source mode.
2. Use workflow-activation-map.md to disclose activated workflows when non-trivial.
3. Read planning/replacement-file-generation-guide.md.
4. Read documentation-update-workflow.md if the package updates planning docs.
5. Read current target files from GitHub/repo or the accepted archive source.
6. If complete current file contents are required and GitHub/repo access is truncated, incomplete or otherwise unsafe, stop and ask the user for a fresh archive or full target-file copies.
7. Generate complete replacement/add files under replacement-files/<repo-relative-path>; this is full replacement archive mode, not patch/snippet mode.
8. Include MANIFEST.md and APPLY.md.
9. Include PowerShell apply commands using temporary extraction and Copy-Item -Destination.
10. Include exact git add and commit commands for the intended changed files only after diff review.
11. Do not include patch scripts, patch files, diff-only files, partial snippets or a planning-only response.
12. If complete replacement files cannot be produced safely, stop and say so instead of switching to patch mode.
13. Include post-apply verification commands and preservation/no-loss checks in the final chat response and APPLY.md.
14. Include default diff capture commands that save diff to a file through `git --no-pager diff --no-color --output`, copy UTF-8 text from that file to clipboard with `ReadAllText + Set-Clipboard`, and do not print the full diff to the terminal.
15. Use review-diff-file mode only when explicitly requested; it may push only `_ai-review-diffs/last-archive.diff`.
16. If copied diff shows mojibake or suspicious broken Cyrillic, provide suspect-file content copy commands from `planning/replacement-file-generation-guide.md#7b-diff-capture-and-clipboard-commands`.
```

## 15. Detailed Trace: Source Dependency / Link Declaration

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.8.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planned planning/slices/slice-source-sync-register.md @ not created
  Internal dependencies:
    - Source Model
    - Primary Use Case Table
  Not checked:
    - slice source-sync register not created yet
```

User says:

```text
сорс / укажи сорс / добавь source / добавь зависимость / ссылка на другой файл / этот файл зависит от X / эта секция зависит от X / source dependency / dependency link / source link
```

Steps:

```text
1. Identify the target file and, when possible, the exact target section.
2. Identify the referenced source file or internal section.
3. Read planning/source-cascade-sync-workflow.md, planning/SOURCE-SECTION-SOURCES-TEMPLATE.md and planning/source-usage-cascade-profile.md.
4. Read the target file and referenced source file.
5. Read the relevant layer/root register:
   - planning/root-source-sync-register.md for root planning/workflow/router files;
   - planning/domain/domain-source-sync-register.md for domain aggregate/value-object files;
   - planning/slices/slice-source-sync-register.md once created for slice files.
6. Read the target file-type workflow/template/responsibility map when local section shape or ownership is unclear.
7. Classify the dependency as format/process, content, internal, register-index, implementation/evidence or simple navigational link.
8. Decide whether the target needs a local section-level `Sources:` block, a file-level dependency audit, a register row/status update or only a non-source navigation link.
9. Use declared Doc version/status labels; if a source has no Doc version, mark version/status explicitly instead of inventing one.
10. Report register impact and not-checked sources.
11. Do not edit files or create a package unless the user separately approves update/archive output.
```

Expected output:

```text
- target file / section;
- referenced source;
- dependency classification;
- version/status label to use;
- local `Sources:` block or file-level audit decision;
- root/domain/slice register impact;
- not checked / limits;
- next action or `План файл-обновление` when planning file changes.
```



Source/version maintenance commands are active route commands in this map:

```text
- `стейл версии в регистрах` / register stale version scan;
- `стейл локальные сорсы` / local Sources stale scan;
- `полная source/version проверка` / full source/version audit.
```

They are review/audit commands by default. They do not edit files, create archives, commit or push unless the user separately requests an update/package flow. Scripted repository-wide automation remains future work.

## 16. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.8.0
    - planning/root-source-sync-register.md @ Doc version: v2.0.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
    - planning/workflow-activation-map.md @ Doc version: v0.6.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.4.0
  Internal dependencies:
    - Relationship To Root Files
    - Primary Use Case Table
    - Detailed Trace: Source Dependency / Link Declaration
  Not checked:
    - full response/output/Goal Map/Tampermonkey workflow source passes outside ROOT-SRC-2A
```

```text
- ROOT-SRC-2A added local section-level Sources blocks to the root use-case map and bumped it to Doc version: v0.2.0.
- ROOT-SRC-2A preserved existing command routes and did not claim full root-folder coverage.
- ROOT-SRC-2B refreshed protocol/role source status and bumped this map to Doc version: v0.3.0 without changing command routes.
- ROOT-SRC-3A refreshed output/archive source status and bumped this file to Doc version: v0.4.0 without changing routing/navigation semantics.
- SRC-CMD-1A added active routes for source/version maintenance commands and explicit `положняк` current-state output, and bumped this map to Doc version: v0.5.0.
- SRC-CMD-1C refreshed current root register/source-cascade labels after the Tampermonkey helper profile pass, bumped this map to Doc version: v0.6.0 and did not change command semantics.
- CASCADE-SRC-1A added the narrow `сорс-импакт` route for source-impact classification, clarified review/classification-only output and bumped this map to Doc version: v0.7.0 without adding command-intake/action-type expansion.
- CASCADE-ROUTE-1B clarified UCM route ownership, kept WAM as activation/read-order helper, added profile-specific example-fit routing notes and bumped this map to Doc version: v0.8.0 without adding new active command families.
```
