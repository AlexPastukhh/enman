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
8. Read workflows for algorithms.
9. Read templates for output shape.
10. Read sources/evidence needed for this pass.
11. Report sources used this pass and relevant not-checked sources.
12. Separate implicit checks from explicit-permission actions.
13. Produce answer/update/plan/draft.
```

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

## 9. Repeated / Continuation Commands

| User says | If active context exists | If no active context exists | Traversal depth | Read source mode | Expected output |
|---|---|---|---|---|---|
| `драфт`, `давай драфт`, `покажи драфт` | Show/update active draft. | Ask target or start draft workflow if target is obvious. | Reuse / targeted. | Conversation / canvas / sources if changed. | Latest draft version or canvas update. |
| `обнови`, `обнови драфт`, `актуализируй` | Apply latest discussion deltas to active draft/answer/plan. | Ask target unless obvious. | Reuse / targeted. | Conversation / canvas; source check only if needed. | Updated version or “already current”. |
| `уточни` | Same scope, more precise wording/boundary. | Clarify target if ambiguous. | No traversal / reuse / targeted. | Conversation; sources only if precision depends on evidence. | Clarified answer/section. |
| `расширь` | Add depth/examples/edge cases without silently changing scope. | Ask what to expand if ambiguous. | Reuse / targeted. | Conversation plus named/new sources if needed. | Expanded section/answer with scope note. |
| `перепроверь`, `recheck` | Recheck active answer/draft/source coverage. | Recheck last answer or ask target. | Targeted / full depending risk. | Conversation plus relevant sources. | Findings and corrections/no-change result. |
| `учти файл X` | Incorporate explicit new source into active work. | Use X for the requested new answer. | Targeted. | Uploaded/named file. | Updated answer/draft with Source Delta. |
| `без изм`, `б изм`, `no ch` | Reuse recent context and avoid broad re-audit. | Weak without prior context; ask what state is unchanged if needed. | Reuse / targeted. | Previous context plus targeted reads. | Answer/update with minimal checks. |
| `арх`, `из арх`, `из архива`, `use archive` | Use latest/current archive as read source when checks are needed. | Use latest uploaded archive in current conversation, or ask for archive in a new chat. | Does not decide depth. | Archive snapshot. | Read-only answer/check based on archive. |
| `б из арх`, `без изм, арх`, `изм нет, арх` | No changes + archive source mode. | Use if latest archive exists; otherwise ask for archive. | Reuse / targeted. | Latest uploaded archive. | No broad audit; targeted archive checks only. |

## 10. Primary Use Case Table

| User says | Task type | Active context? | Traversal depth | Read source mode | Activated workflows | Required reads | Source of obligation | Expected output | Permission boundary |
|---|---|---:|---|---|---|---|---|---|---|
| “посмотри / распланируй / проверь docs” | Non-trivial planning/docs work | Optional | Full first time; targeted later | GitHub/archive/conversation by context | workflow activation; reviewable output | `planning/README.md`, `workflow-activation-map.md`, `planning-doc-responsibility-map.md` | README + activation map | Reviewable answer/plan | Edits require approval |
| “обнови docs” | Documentation update | Optional | Targeted/full by scope | GitHub for writes; archive for read-only if requested | docs update plan/workflow; local-global sync | documentation README/map/workflows | activation map + docs workflows | Plan or applied update | GitHub writes require approval |
| “поправь ссылки во многих файлах” | Mechanical link/path/name sync | Optional | Targeted/broad search | GitHub or archive | docs update workflow; local-global sync | target files/search source | docs update workflow output mode rules | Link-sync plan/commit | Bundled commit needs approval |
| “задрафти slice” | Slice draft work | Usually new | Full first time | Sources required by slice workflow | slice workflow chain | slice README/map/workflow/principles/test workflow/template | activation map + slice README/map | Slice draft/plan | File writes require approval |
| “задрафти server slice” | Server slice draft | New/active | Full first time; targeted update | Scenario/domain/API/testing sources | slice + server drafting workflows | server workflow/template/principles | slice README/map | Server/backend/API draft | File writes require approval |
| “задрафти client sidecar” | Client slice draft | New/active | Full first time; targeted update | UI/scenario/client sources | slice + client drafting workflows | client workflow/template/principles/CSS/a11y/form docs | slice README/map | `.client.md` draft | File writes require approval |
| “сделай cross-cutting umbrella” | Cross-cutting coordination doc | New/active | Full first time | Cross-cutting behavior and side draft sources | slice workflow + cross-cutting template | cross-cutting README/template | slice README/map | Umbrella coordination draft | File writes require approval |
| “проверь current/implemented status” | Status reconciliation | Optional | Targeted/full by claim | Current branch evidence unless archive explicitly accepted | status reconciliation | status workflow + code/tests/generated artifacts | activation map status chain | Status findings/sync plan | Current-state claims need evidence |
| “сделай сценарий / DATA / behavior items” | Scenario source work | Optional | Full first time | Scenario/source files | scenario drafting workflow | diagrams README + scenario source files | activation map scenario chain | Scenario draft/update plan | Edits require approval |
| “разбери domain draft” | Domain work | Optional | Targeted/full by scope | Domain docs/current code if needed | future/domain routing if present | `planning/tables/README.md` now | root responsibility map | Domain review/plan | Edits require approval |
| “разбери тестирование” | Testing layer work | Optional | Targeted/full by scope | Testing docs/current tests if needed | testing workflows if present | `planning/testing/README.md` | root responsibility map | Testing audit/plan | Edits require approval |
| “обнови API/OpenAPI rules” | API planning work | Optional | Targeted/full by scope | API docs/generated artifacts if status claims | API/docs workflows | `planning/api/README.md` | root responsibility map | API docs plan/update | Generated changes need explicit scope |
| “сделай VKR текст” | VKR/thesis wording | Optional | Targeted | Clean docs + evidence as needed | reviewable output | `planning/vkr-clean-reference.md` | planning README | Clean thesis wording | Do not use internal labels |

## 11. Detailed Trace: Active Draft Continuation

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

Do not switch to an older draft unless the user names it.

## 12. Detailed Trace: New Slice Draft

User says:

```text
задрафти slice
```

Path:

```text
planning/workflow-activation-map.md
planning/planning-doc-responsibility-map.md
planning/slices/README.md
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-test-plan-workflow.md
side-specific workflow/template
```

Expected behavior:

```text
- classify slice type;
- collect scenario/source/behavior/domain/API/testing sources;
- use workflow for algorithm;
- use template for output shape;
- report checked/not checked sources;
- do not invent behavior locally when source files exist.
```

## 13. Detailed Trace: Update / Clarify / Expand

Update types:

```text
- user correction;
- clarification;
- expansion;
- self-correction;
- recheck;
- new source introduced;
- latest-version request.
```

Algorithm:

```text
1. Identify target: active answer, draft, canvas, section, plan or named old item.
2. Identify update type.
3. Decide traversal depth.
4. Decide read source mode.
5. Identify Source Delta.
6. Apply update without silently broadening scope.
7. Preserve useful previous caveats/sources/risks unless superseded.
8. Report changed / already current / blocked.
```

New sources used in one pass do not automatically become default sources.

## 14. Detailed Trace: No Changes + Archive

User says:

```text
без изм, арх
б из арх
изм нет, арх
no ch, archive
```

Meaning:

```text
unchanged state + archive read source
```

Behavior:

```text
- skip broad re-audit;
- reuse previous traversal/context;
- read archive only for needed targeted checks;
- use the latest uploaded archive in the current conversation if no new archive is attached;
- do not use GitHub unless write/SHA or remote-cleanliness claim requires it.
```

Limits:

```text
- a new chat cannot know an old archive unless it is uploaded again;
- archive evidence is not remote/current proof if freshness is uncertain;
- response commands do not grant edit permission.
```

## 15. Permission Boundaries

Response commands do not permit:

```text
- GitHub writes;
- deletes;
- renames;
- commits;
- PRs;
- generated artifact changes;
- implementation changes.
```

Explicit approval is required for:

```text
- create/update/delete files;
- bulk/bundled commits;
- archive package creation;
- code/generated changes;
- implementation changes.
```

## 16. Known Gaps

Known cleanup/future work:

```text
- server/client/cross-cutting drafting workflow split is deferred;
- testing workflow split is deferred;
- source/version cascade sync workflow is future;
- response output templates vs response command workflow split is deferred.
```

Do not hide these gaps in chat memory. Track durable follow-ups in:

```text
planning/planning-maintenance-register.md
```
