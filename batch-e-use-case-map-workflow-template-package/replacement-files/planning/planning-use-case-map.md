# Planning Use Case Map

Status: current root use-case map / action-to-doc-flow router  
Scope: maps user-visible actions and commands to planning docs, workflows, templates, sources and permission boundaries

## 1. Purpose

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

This file is one concrete use-case map instance. Reusable use-case-map maintenance rules are owned by `planning/documentation/use-case-map-workflow.md`; exact reusable map shape is owned by `planning/documentation/USE-CASE-MAP-TEMPLATE.md`.

## 2. Relationship To Root Files

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
```

## 3. Universal Chat Algorithm

For non-trivial planning/repo work, use this path:

```text
1. Decide whether the request starts new work or continues active context.
2. Use planning/workflow-activation-map.md.
3. Output Workflow Preflight when required.
4. Use planning/planning-doc-responsibility-map.md to choose the planning layer.
5. Use layer README / responsibility map for local routing.
6. Select traversal depth.
7. Select read source mode.
8. Select output mode when the user asks for a deliverable such as an archive/package.
9. Read workflows for algorithms.
10. Read templates for output shape.
11. Read sources/evidence needed for this pass.
12. Report sources used this pass and relevant not-checked sources.
13. Separate implicit checks from explicit-permission actions.
14. Produce answer/update/plan/draft/package.
```

## 3A. Principle Section References

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
| Documentation structure / placement / owner boundaries | `planning/documentation/planning-docs-architecture-principles.md#contents`, `#17-responsibility-ownership`, `#24-no-duplication--authority-rule`, `#24a-link-instead-of-copy--docs-dry-rule` | Prevent misplaced duplicated logic. |
| Template/workflow/example governance | `planning/documentation/planning-docs-architecture-principles.md#9-template-vs-workflow`, `#24a-link-instead-of-copy--docs-dry-rule` | Keep templates, workflows and examples within their own responsibilities. |
| Archive/replacement package output | `planning/documentation/planning-docs-architecture-principles.md#23-direct-edits-archives-and-commit-granularity`, plus `planning/replacement-file-generation-guide.md` | Archive output is an output mode and must use replacement files. |
| Source usage / stale-reference / cascade / reviewed-work concerns | `planning/documentation/planning-docs-architecture-principles.md#12a-layer-encapsulation-and-attention-preservation`, `#13-source--version-principle`, `#14-dependency-cascade-principle`, `#15-section-level-sources-principle` | Prevent duplicated source truth and preserve attention by referencing reviewed upstream work instead of reconstructing it. |
| Accepted commands / post-apply preservation | `planning/documentation/planning-docs-architecture-principles.md#24b-accepted-command-and-preservation-guardrails`, plus owner workflow/use-case files | Prevent silent reinterpretation and accidental information loss after replacement packages. |

Do not add source-version numbers to this use-case map yet. Section-level principle versions and use-case source-version metadata are deferred until the source/version model is ready. Use the encapsulation/source-usage principle reference to decide when a pilot source usage register or cascade workflow is needed, not to add ad hoc version fields to use-case rows.

## 4. Active Context Rule

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

If user wording matches an accepted command or use case, follow the owner use-case/workflow definition.

Do not silently substitute another output mode or process because it seems safer, easier or more convenient.

If the accepted mode cannot be completed safely, stop and explain the blocker. Ask for explicit approval before switching modes.

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

Traversal depth answers: how much should the chat read/check now?

| Mode | Meaning | Use when |
|---|---|---|
| Full traversal | Read the full required doc/source chain for the use case. | New chat, first use of a use case, changed scope/source model, high-risk status/current claim, explicit full recheck. |
| Targeted traversal | Read only files needed for the current update/check. | Same use case, narrow update, named target file/source, mechanical link check. |
| Reuse previous traversal | Reuse recent checked context in this chat. | Same active draft/use case, no source/scope change, user says no changes. |
| No traversal | Answer from active context without new reading. | Simple clarification, active draft display, wording update with no evidence change. |

Do not confuse traversal depth with read source mode.

## 6. Read Source Modes

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
| `давай архив`, `собери архив`, `replacement package`, `archive for manual apply` | Archive as output package. | Use `planning/replacement-file-generation-guide.md` and produce a replacement package. |

When the user asks for an archive/package output, the chat must read:

```text
planning/replacement-file-generation-guide.md
```

Replacement package output must contain complete replacement/add files under `replacement-files/<repo-relative-path>`, plus `MANIFEST.md` and `APPLY.md`. Do not emit patch scripts or diff-only packages unless the user explicitly asks for patch proposal mode.

## 7. Source Model

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

## 8. Source Delta

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

Answer depth is governed by:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

This use-case map may mark expected response level or output shape in the `Expected output` column, but it does not own Level 1/2/3 trigger logic.

File Update Overview process and shape are owned by:

```text
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
```

Use-case rows may reference File Update Overview as expected output, but must not duplicate its workflow or template logic.

The canonical prior-discussion recheck command is:

```text
обс
```

`обс` means context/discussion recheck. Its semantics are owned by `reviewable-agent-output-and-commands-workflow.md`.

Response block commands such as `кп`, `саммари`, `итог` and `отличия драфта` are routed in this file, but their behavior is owned by `reviewable-agent-output-and-commands-workflow.md`.

## 9. Repeated / Continuation Commands

| User says | If active context exists | If no active context exists | Traversal depth | Read source mode | Expected output |
|---|---|---|---|---|---|
| `драфт`, `давай драфт`, `покажи драфт` | Show/update active draft. | Ask target or start draft workflow if target is obvious. | Reuse / targeted. | Conversation / canvas / sources if changed. | Latest draft version or canvas update. |
| `обнови`, `обнови драфт`, `актуализируй` | Apply latest discussion deltas to active draft/answer/plan. | Ask target unless obvious. | Reuse / targeted. | Conversation / canvas; source check only if needed. | Updated version or “already current”. |
| `уточни` | Same scope, more precise wording/boundary. | Clarify target if ambiguous. | No traversal / reuse / targeted. | Conversation; sources only if precision depends on evidence. | Clarified answer/section. |
| `расширь` | Add depth/examples/edge cases without silently changing scope. | Ask what to expand if ambiguous. | Reuse / targeted. | Conversation plus named/new sources if needed. | Expanded section/answer with scope note. |
| `кп`, `key points` | Force a `Key points first` block for the active answer. | Add key points if the requested answer exists or ask what answer to summarize. | No traversal / reuse. | Conversation; sources only if key points depend on evidence. | `Key points first` block plus normal answer structure. |
| `без кп`, `без key points` | Suppress `Key points first` for the active answer. | Apply to the current answer if target is obvious. | No traversal / reuse. | Conversation. | Answer without key-points preview. |
| `саммари` | Add contextual `Краткое саммари`. | Summarize current answer/context or ask target. | No traversal / reuse; targeted if evidence changed. | Conversation plus sources only if needed. | `Краткое саммари`, not file-change `Итог`. |
| `без саммари` | Suppress `Краткое саммари`. | Apply to current answer if target is obvious. | No traversal / reuse. | Conversation. | Answer without contextual summary. |
| `итог` | Add or update file/change-oriented `Итог` when update context exists. | If no file/change/update context exists, explain that `Итог` is not applicable and offer `Краткое саммари`. | Reuse / targeted. | Conversation plus target files/diff/archive when needed. | File Update Overview / `Итог`; during planning it is the rolling nearest-batch plan. |
| `без итога` | Suppress `Итог`. | Apply to current answer if target is obvious. | No traversal / reuse. | Conversation. | Answer without File Update Overview. |
| `полный конец` | Add `Краткое саммари` and `Итог` when `Итог` is applicable. | Add both ending blocks if target is obvious. | Reuse / targeted. | Conversation plus target files/diff/archive when needed. | Context summary followed by final file/change overview. |
| `отличия драфта`, `draft diff` | Show how the active draft changed from the previous version. | Ask for the draft target if no active draft is clear. | Reuse / targeted. | Conversation / canvas / draft sources if changed. | `Отличия от предыдущего драфта` plus updated draft when requested. |
| `обс`, `перепроверь обсуждение`, `context recheck` | Re-check relevant prior discussion, accepted decisions and constraints before answering. | Re-check available prior discussion or say what context is unavailable. | Reuse / targeted; full only if task requires it. | Conversation plus sources only if needed. | Context/discussion recheck result; combine with Level 1/2/3 by task breadth. |
| `перепроверь`, `recheck` | Recheck active answer/draft/source coverage. | Recheck last answer or ask target. | Targeted / full depending risk. | Conversation plus relevant sources. | Findings and corrections/no-change result. |
| `учти файл X` | Incorporate explicit new source into active work. | Use X for the requested new answer. | Targeted. | Uploaded/named file. | Updated answer/draft with Source Delta. |
| `без изм`, `б изм`, `no ch` | Reuse recent context and avoid broad re-audit. | Weak without prior context; ask what state is unchanged if needed. | Reuse / targeted. | Previous context plus targeted reads. | Answer/update with minimal checks. |
| `арх`, `из арх`, `из архива`, `use archive` | Use latest/current archive as read source when checks are needed. | Use latest uploaded archive in current conversation, or ask for archive in a new chat. | Does not decide depth. | Archive snapshot. | Read-only answer/check based on archive. |
| `б из арх`, `без изм, арх`, `изм нет, арх` | No changes + archive source mode. | Use if latest archive exists; otherwise ask for archive. | Reuse / targeted. | Latest uploaded archive. | No broad audit; targeted archive checks only. |
| `давай архив`, `собери архив`, `replacement package`, `archive for manual apply` | Produce archive/package for the active approved plan/scope. | Build archive plan from obvious target or ask only blocking target questions. | Targeted/full by package scope. | GitHub/archive/conversation by context. | Replacement package ZIP with `MANIFEST.md`, `APPLY.md`, `replacement-files/`. |
| `проверь` after replacement archive/package application | Verify the active archive application result. | Ask for status/diff or target archive if not available. | Targeted. | Applied repo state / user-provided diff. | Post-apply verification: expected files changed, no unexpected files in commit scope, diff matches package intent, no unrelated sections/register entries/routing rules were removed; if pasted diff shows mojibake, request/copy full suspect file contents before judging file corruption. |

## 10. Primary Use Case Table

| User says | Task type | Active context? | Traversal depth | Read source mode | Activated workflows | Required reads | Source of obligation | Expected output | Permission boundary |
|---|---|---:|---|---|---|---|---|---|---|
| “новый чат”, “вкатись”, “разберись с planning docs”, “first pass”, no reliable active context | New Chat Onboarding / first planning pass | New or uncertain | Full first time; targeted/reuse after checked context exists | GitHub/repo by default; archive only if user requests archive source mode or provides an archive | workflow activation; planning agent protocol; role identification when specialized work is likely; reviewable output | `planning/README.md`, `planning/workflow-activation-map.md`, `planning/planning-use-case-map.md`, `planning/planning-agent-protocol.md`, `planning/planning-doc-responsibility-map.md`; `planning/agent-roles-and-required-actions.md` when role matters | README + use-case map + planning agent protocol | Short onboarding preflight, selected next use case, checked/not-checked sources, next action | No edits/packages unless explicitly requested |
| “посмотри / распланируй / проверь docs” | Non-trivial planning/docs work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | workflow activation; reviewable output | `planning/README.md`, `workflow-activation-map.md`, `planning-doc-responsibility-map.md` | README + activation map | Level 2 reviewable answer/plan | Edits require approval |
| “обнови docs” | Documentation update | Optional | Targeted/full by scope | GitHub for writes; archive for read-only if requested | docs update plan/workflow; local-global sync | documentation README/map/workflows | activation map + docs workflows | Level 2 update plan or applied update + File Update Overview when files are planned/changed/reviewed | GitHub writes require approval |
| “добавь template / workflow / command / draft format / пример” | Documentation governance update | Optional | Full first time; targeted later | GitHub/archive/conversation by context | docs update workflow; example coverage decision when template/output shape changes; reviewable output | documentation README/map/workflows; `planning/documentation/example-coverage-workflow.md` when template/output/command/draft example coverage matters; relevant owner workflow/template; principle section references in §3A | docs architecture principles + use-case map + example coverage workflow | Level 2 update plan or replacement package + File Update Overview when files are planned/changed/reviewed; example coverage decision if applicable | Edits/packages require approval |
| “source usage”, “source/version”, “каскад зависимостей”, “stale downstream”, “версии сорсов”, “инкапсуляция слоёв” | Source usage / cascade governance or pilot work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | source usage cascade governance plan; principles §12A/13/14/15; PMR-002 until full workflow exists | `planning/documentation/source-usage-cascade-governance-plan.md`; `planning/documentation/source-usage-pilots/README.md`; pilot register if user asks to work on a pilot; relevant source/domain/slice docs only for real pilot fill | layer encapsulation principle + source usage governance plan | Level 2 governance plan, pilot skeleton, pilot fill plan or replacement package + File Update Overview when files are planned/changed/reviewed | Do not pretend full cascade workflow exists; do not add ad hoc version fields |
| “давай архив”, “собери архив”, “replacement package”, “archive for manual apply” | Replacement archive/package generation | Optional | Targeted/full by package scope | GitHub/archive/conversation by context | replacement file generation guide; docs update workflow when docs change; reviewable output | `planning/replacement-file-generation-guide.md`, target files, docs update workflow when docs are changed | replacement guide + workflow activation | Level 2 archive/package response + File Update Overview; ZIP package with `MANIFEST.md`, `APPLY.md`, `replacement-files/<repo-relative-path>` complete files | Direct repo edits not allowed unless separately approved |
| “файл большой”, “дай ps1”, “скрипт для большого файла”, “архив неудобен” | Large/shared file update delivery choice | Optional | Targeted/full by file risk | Fresh archive/current full file preferred; script only if complete replacement unsafe | docs update workflow; replacement guide; local targeted script mode if fallback needed | `planning/documentation/documentation-update-workflow.md`, `planning/replacement-file-generation-guide.md`, target file | delivery safety check + replacement guide | Level 2 delivery plan: prefer fresh full repo/archive + safe complete replacement; one-file targeted script only as fallback | Do not choose script only because file is large; scripts require explicit mode and no auto-commit |
| “поправь ссылки во многих файлах” | Mechanical link/path/name sync | Optional | Targeted/broad search | GitHub or archive | docs update workflow; local-global sync | target files/search source | docs update workflow output mode rules | Level 2 link-sync plan + File Update Overview; one bundled commit when tool-supported | Bundled/bulk mode should be used when approved; if unavailable, stop and disclose before per-file writes |
| “задрафти slice” | Slice draft work | Usually new | Full first time | Sources required by slice workflow | slice workflow chain; testing selector when needed | slice README/map/workflow/principles/test workflow/template; `planning/testing/testing-responsibility-map.md` when test-layer-specific guidance is needed | activation map + slice README/map | Slice draft/plan | File writes require approval |
| “задрафти server slice” | Server slice draft | New/active | Full first time; targeted update | Scenario/domain/API/testing sources | slice + server drafting workflows; testing selector when needed | server workflow/template/principles; `planning/slices/slice-test-plan-workflow.md`; `planning/testing/testing-responsibility-map.md`; `planning/testing/server-slice-test-plan-rules.md` when server/API test guidance is needed | slice README/map | Server/backend/API draft | File writes require approval |
| “задрафти client sidecar” | Client slice draft | New/active | Full first time; targeted update | UI/scenario/client/testing sources | slice + client drafting workflows; testing selector when needed | client workflow/template/principles/CSS/a11y/form docs; `planning/slices/slice-test-plan-workflow.md`; `planning/testing/testing-responsibility-map.md`; `planning/testing/test-object-patterns.md` when client/E2E object patterns matter | slice README/map | `.client.md` draft | File writes require approval |
| “сделай cross-cutting umbrella” | Cross-cutting coordination doc | New/active | Full first time | Cross-cutting behavior and side draft sources | slice workflow + cross-cutting template | cross-cutting README/template | slice README/map | Umbrella coordination draft | File writes require approval |
| “проверь current/implemented status” | Status reconciliation | Optional | Targeted/full by claim | Current branch evidence unless archive explicitly accepted | status reconciliation | status workflow + code/tests/generated artifacts | activation map status chain | Level 2 status findings/sync plan | Current-state claims need evidence |
| “сделай сценарий / DATA / UI scenario / behavior items” | Scenario source work | Optional | Full first time; targeted update later | Scenario/source files or archive | scenario responsibility map; artifact map; scenario drafting workflow | `planning/diagrams/README.md`, `scenario-responsibility-map.md`, `scenario-artifact-map.md`, `scenario-drafting-workflow.md`, `planning/scenario-specification-principles.md`, relevant text/DATA/UI/behavior/clarification files | activation map + use-case map + scenario responsibility map | Scenario text / DATA / UI scenario / behavior item draft or update plan | Edits require approval |
| “сделай диаграммы / обнови draw.io / подготовь diagram prompt / проверь диаграммы” | Diagramming work | Optional | Full first time; targeted update later | Diagramming docs plus scenario/domain/testing/thesis sources as needed | diagramming responsibility map; diagram source consistency; diagram prompt/draw.io workflows | `planning/diagramming/README.md`, `planning/diagramming/diagramming-responsibility-map.md`, `planning/diagrams/scenario-diagram-consistency-report.md`, `planning/diagrams/diagram-prompt-generation-workflow.md` or `planning/diagrams/drawio-diagram-generation-workflow.md`; source layers required by requested pages | activation map + use-case map + diagramming responsibility map | Diagram source audit / diagram prompt / batch plan / draw.io artifact plan or package | File/artifact writes require approval |
| “разбери domain draft / domain discovery / aggregate draft” | Domain work | Optional | Full first time; targeted later | Domain docs/scenario sources/current code if needed | domain responsibility/discovery/aggregate/value-object workflows by scope | `planning/domain/README.md`, `planning/domain/domain-responsibility-map.md`, `planning/domain/scenario-to-aggregate-map.md`; old `planning/tables/domain-drafts/` only as historical/cross-check source | root responsibility map + domain responsibility map | Domain discovery map / aggregate draft / value-object draft / review plan | Edits require approval |
| “разбери тестирование” | Testing layer work | Optional | Targeted/full by scope | Testing docs/current tests if needed | testing responsibility map + selected testing workflows | `planning/testing/README.md`, `planning/testing/testing-responsibility-map.md`, `planning/testing/testing-principles.md`, specific testing docs by selector | root responsibility map + testing responsibility map | Testing audit/plan | Edits require approval |
| “обнови API/OpenAPI rules” | API planning work | Optional | Targeted/full by scope | API docs/generated artifacts if status claims | API/docs workflows | `planning/api/README.md` | root responsibility map | API docs plan/update | Generated changes need explicit scope |
| “сделай VKR текст” | VKR/thesis wording | Optional | Targeted | Clean docs + evidence as needed | reviewable output | `planning/vkr-clean-reference.md` | planning README | Clean thesis wording | Do not use internal labels |

## 11. Detailed Trace: New Chat Onboarding

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
7. For documentation-layer work, read documentation README/map/workflows as required by planning-agent-protocol.md.
8. Select traversal depth:
   - full for first use / new chat / changed scope;
   - targeted or reuse only after recent checked context exists.
9. Select read source mode:
   - GitHub/repo for current remote docs;
   - archive only if user explicitly wants archive source mode or provides archive;
   - conversation only for user corrections/discussion deltas.
10. Select output mode:
   - answer/plan by default;
   - replacement archive only when user says “давай архив” / package output;
   - direct edits only when explicitly approved.
11. Use accepted command / no reinvention rule.
12. Use Docs DRY: link to owner files, do not copy owner logic.
13. Produce compact reviewable answer:
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

User says:

```text
добавь template / workflow / command / draft format / пример
```

Steps:

```text
1. Use workflow-activation-map.md and disclose activated workflows when non-trivial.
2. Use documentation responsibility routing to find the owner file.
3. Re-read the relevant principle sections from §3A instead of copying their logic here.
4. Decide whether the change creates or changes a template, output shape, command behavior or draft format.
5. If there is a template, use planning/documentation/example-coverage-workflow.md and decide that a working example is needed by default unless the owner file is routing-only or the example would duplicate another example.
6. If an example is not added, record the reason in the relevant index or plan.
7. Examples may link to owner use cases/workflows/templates, but must not duplicate routing, source-mode, output-mode or permission logic.
8. Keep source/output/permission logic in concrete use-case maps and owner workflows.
9. Use planning/documentation/use-case-map-workflow.md and planning/documentation/USE-CASE-MAP-TEMPLATE.md when creating or restructuring use-case maps.
10. Use replacement archive/package output only when the user asks for an archive/package or after approval.
```

Deferred:

```text
Section-level principle versions and source-version metadata for use-case references are not introduced yet. Track that as deferred source/version governance work rather than adding ad hoc version fields to every row.
```

## 14. Detailed Trace: Replacement Archive / Package Output

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
6. Generate complete replacement/add files under replacement-files/<repo-relative-path>.
7. Include MANIFEST.md and APPLY.md.
8. Include PowerShell apply commands using temporary extraction and Copy-Item -Destination.
9. Include exact git add and commit commands for the intended changed files.
10. Do not include patch scripts, diff-only files or partial snippets.
11. If complete replacement files cannot be produced safely, stop and say so instead of switching to patch mode.
12. Include post-apply verification commands and preservation/no-loss checks in the final chat response and APPLY.md.
13. Include full diff capture commands that save diff to a file through `git --no-pager diff --no-color --output` and copy UTF-8 text from that file to clipboard.
14. If copied diff shows mojibake or suspicious broken Cyrillic, provide suspect-file content copy commands from `planning/replacement-file-generation-guide.md#7b-diff-capture-and-clipboard-commands`.
```
