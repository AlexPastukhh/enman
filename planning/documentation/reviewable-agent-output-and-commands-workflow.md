# Reviewable Agent Output And Commands Workflow

Status: current response-quality and response-command workflow
Doc version: v0.1.0
Scope: how chats/agents should structure non-trivial answers and interpret response-level user commands so a person or another chat can review, verify and continue the work

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

This file defines how an agent should format important answers and interpret response-level user commands.

It does not define how to write scenario specs, domain drafts, slice drafts or documentation update plans. Those are owned by their specialized workflow/template docs.

This file owns the response-level rule:

```text
For non-trivial work, answers should be reviewable by a person or by another context-aware chat.
```

A reviewable answer should make clear:

```text
- what task was answered;
- what was in scope and out of scope;
- which files/sources were checked;
- which relevant files/sources were not checked;
- which sources are valid for this task;
- what result or recommendation was produced;
- which assumptions, questions and risks remain;
- how the result can be verified;
- what should happen next.
```

Response-level commands such as `level 2`, `recheck`, `clarify`, `keep prev`, `no ch`, `без изм`, `use archive`, `арх`, `б из арх`, `давай драфт`, `обнови`, `обс`, `кп`, `саммари`, `кц`, `карта цели кратко`, `goal map brief`, `крит`, `критически`, `critical review`, `создай команду`, `итог` and `отличия драфта` change how the answer should be produced, checked or continued. They do not grant permission to edit files or change repository state.

For action/use-case traces, active context, traversal depth and read source mode, use:

```text
planning/planning-use-case-map.md
```

For creating or changing command routes, use:

```text
planning/documentation/command-creation-workflow.md
```

The command-creation workflow does not grant edit/archive/commit permission by itself; it routes and plans command changes unless the user separately requests an update/archive.

## 2. Core Rule

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

Use the smallest response format that remains reviewable.

Do not turn every casual answer into a heavy report.

Response levels are primarily about task complexity, breadth and external reviewability. They are not quality levels.

The assistant must still do the reasoning, context recall, source check, web verification, safety check or accepted-decision preservation required by the task even when the final answer is short.

Use more structure when:

```text
- the answer affects planning docs;
- the answer may be reviewed by another chat;
- the answer summarizes repo state;
- the answer proposes a change;
- the answer contains a draft, audit, plan or handoff;
- the answer depends on multiple sources;
- the answer has risks, assumptions or incomplete coverage.
```

## 3. Detail Levels

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

The user may request a response level with phrases such as:

```text
level 1
lvl 1
ур 1
level 2
lvl 2
ур 2
level 3
lvl 3
ур 3
```

If the user does not specify a level, choose the smallest level that remains useful and reviewable.

Default guidance:

```text
casual / quick question with no Level 2/3 triggers -> Level 1
non-trivial planning / docs / repo analysis -> Level 2
handoff / external review / broad audit / high-risk answer -> Level 3
```

Level selection changes how explicitly the answer exposes task, scope, sources, risks, verification and next steps. It does not lower the required reasoning quality for lower levels.

### 3A. Automatic Level Escalation

Level 1 is allowed only when the task does not contain Level 2 or Level 3 triggers.

Level 2 triggers are conditions where the task is broad or consequential enough that the answer must expose scope, sources/coverage, assumptions, risks, verification and next step.

Use Level 2 automatically when the task involves any of:

```text
- planning / documentation / code / file update planning;
- multi-file checks;
- synchronization between files, docs or layers;
- workflow / rule / use-case / responsibility changes;
- source-of-truth boundary changes;
- archive / replacement package planning or review;
- diff review before commit;
- repo analysis across multiple files;
- non-trivial audit / check / recheck;
- source usage / cascade / stale-reference work;
- any task where a short answer could hide scope, unchecked sources, risks, assumptions or next steps.
```

Use Level 3 automatically when the answer must survive outside the current chat context or the task is broad/high-risk enough that Level 2 would not preserve enough evidence.

Typical Level 3 triggers:

```text
- handoff to another chat/person;
- broad cross-layer audit;
- high-risk source-of-truth consistency review;
- major migration or cascade review;
- external review package;
- output that must be continued without prior context.
```

Do not make the user ask for Level 2 when Level 2 triggers are already present.

If unsure between Level 1 and Level 2, use Level 2.

If unsure between Level 2 and Level 3, use Level 2 unless the answer must serve as a standalone handoff/audit artifact.

### 3B. Work Quality Is Not Reduced By Level

A Level 1 answer may still require careful reasoning, relevant context recall, a narrow file/source check or web verification when needed for correctness.

Do not use Level 1 as permission to skip necessary reasoning, context checks, source checks, safety checks or accepted-decision preservation.

When the work itself requires many files, many sources, synchronization, audit or planning, Level 2 or Level 3 normally applies because the work must be exposed for review.

## 4. Level 1 — Short Answer

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

Use Level 1 for simple questions, quick decisions, narrow commands or one-step local troubleshooting when the task has no Level 2 or Level 3 triggers.

Template:

```text
## Short Answer

...

## Important Limits

- ...

## Next Step

...
```

Rules:

```text
- Keep it short.
- Mention major uncertainty if it matters.
- Do the checks required by the task even if they are summarized briefly.
- Do not include a full sources block unless the answer depends on specific checked files.
```

## 5. Level 2 — Default Serious Answer

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

Use Level 2 for non-trivial planning, documentation, repo analysis, draft discussion, file/code update planning, synchronization, diff review, source-of-truth reasoning or architectural reasoning.

Level 2 structure must remain reviewable. Extra response blocks such as `Key points first`, `Краткое саммари` and `План файл-обновление` do not replace task/scope, sources/coverage, assumptions/risks, verification or next step.

Default analytical/planning template:

```text
## Key points first, optional when useful

Short points that mirror the main detailed answer.
They do not have a fixed internal field format.
Each point should carry key information plus the conclusion/consequence/practical meaning of that information.
Omit this block when the whole answer would be no longer than the key points.

## 1. Answer / Result

...

## 2. Details / Plan / Review

...

## 3. Verification, Risks And Limits

How to verify:
- ...

Risks / assumptions / limits:
- ...

## 4. Next Step

...

## Краткое саммари

Вывод:
  ...

Следующие действия:
  ...

Цель понял так:
  ...

Учтённый контекст:
  - ...

Границы:
  - ...

## Goal Map Brief / Карта цели

Only when an active long-running workstream has a living Goal Map, or when the user explicitly requests `кц`, `карта цели кратко` or `goal map brief`. Use the current slice expanded and other slices as a compact status table. See §5A.

## План файл-обновление

Only when file/change/update context applies. Use File Update Overview ownership rules.
```

`Key points first` is optional navigation for a longer Level 2 answer. It has no fixed internal field format. It should mirror the main detailed-answer points in compressed form.

Each key point should combine:

```text
- key information from a main-answer point;
- the conclusion, consequence or practical meaning of that information.
```

Do not force key points into fixed labels such as Goal, Sources, Conclusion or Next step. The fixed-format block is `Краткое саммари`, not `Key points first`.

Do not use `Key points first` as a substitute for the detailed answer. If the whole answer would be no longer than the key points, omit `Key points first` and answer directly.

For file/docs/code update planning answers, a key point may mention delivery safety when it is one of the main answer points. The main answer and final `План файл-обновление` still need to expose delivery safety classification when it affects artifact generation.

Do not use `Key points first` by default when the answer is a first draft, a draft update, a strict specialized template, or an already self-evident structured output. In those cases the template/draft structure is the navigation.

For draft updates, use `Отличия от предыдущего драфта` instead of `Key points first` when there is a previous draft version to compare against.

`Краткое саммари` is a contextual human summary of the current answer in the broader discussion context. It appears after the main answer and before `План файл-обновление` when useful.

Use this order for `Краткое саммари`:

```text
Вывод:
Следующие действия:
Цель понял так:
Учтённый контекст:
Границы:
```

Goal/source/context traceability that previously appeared as a long Level 2 opening preamble should normally move into `Краткое саммари` near the end.

`План файл-обновление` is not a general conclusion. It is the final file/change/update overview block for file, documentation, code or archive update planning/review/application contexts.

During planning, `План файл-обновление` is the current rolling nearest-batch plan and should be updated as the plan changes until the artifact/diff is produced. After an archive/script/diff/application exists, `План файл-обновление` summarizes the actual artifact, diff, application state or commit readiness.

Sources and coverage must remain reviewable in Level 2, but they do not have to appear as a long opening section. They may be placed in the main body, verification/limits section, Source Delta or `Краткое саммари`, as long as another chat can see what was checked and what was not checked.

## 5A. Goal Map Brief / `кц` For Active Workstreams

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

`Goal Map Brief` / `Карта цели` is a short in-answer projection of a living Goal Map.

Command aliases:

```text
кц
карта цели кратко
goal map brief
выведи краткую карту цели
```

Placement in Level 2 / Level 3 answers:

```text
main reviewable answer
+
Краткое саммари, when useful
+
Goal Map Brief / Карта цели, when triggered
+
План файл-обновление, when file/change/update context applies
```

Use this block when:

```text
- an active long-running workstream exists;
- a living Goal Map exists or is explicitly named;
- the answer is planning, status, continuation, review or next-step work inside that workstream;
- the user explicitly requests `кц`, `карта цели кратко`, `goal map brief` or equivalent wording.
```

Before producing the block, check the living Goal Map's current snapshot, active slice, roadmap/status rows and next action. If the map is missing, stale or not checked, say so instead of presenting the brief as current truth.

Use this shape:

```text
## Goal Map Brief / Карта цели

Goal
  <current goal from the living Goal Map>

Current slice
  <SL-X — readable name>
  Status: <NOW / NEXT / not started / in progress / done>

Current slice chain
  Why now
  Done
  Now
  Next
  After

Other slices
  | Slice | Status |
```

Rules:

```text
- Expand only the current slice.
- List other slices only as a compact status table.
- Do not use `<details>` / collapsible blocks in this response block.
- Do not copy the full living Goal Map into the answer.
- Do not treat the command as permission to edit the repo map, create an archive, commit or push.
- If the answer changes the workstream state, say whether the living Goal Map needs a follow-up update.
```

Example:

```text
planning/documentation/examples/GOAL-MAP-BRIEF-RESPONSE-EXAMPLE.md
```

## 5B. Critical Review / `крит`

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

`крит` is a response-level modifier command for honest evaluation.

Command aliases:

```text
крит
критически
критически оцени
проверь критически
оцени честно
не соглашайся автоматически
за и против
адвокат дьявола
honest review
critical review
```

Use this command when:

```text
- the user asks for critical evaluation;
- the user asks the assistant not to agree automatically;
- the user asks whether a plan, decision, draft, route or answer is actually good;
- the user combines `крит` with another use-case command.
```

`крит` does not replace the underlying task route. When combined with another command, keep the underlying route and apply critical-review answer mode.

Examples:

```text
крит план
  -> planning route + critical review mode

крит архив
  -> archive/package route or archive plan review + critical review mode;
     do not create a package unless the user also clearly asks for package output

крит кц
  -> Goal Map Brief route + critical review of current workstream direction/status

крит этот слайс
  -> relevant slice/domain/scenario route + critical review mode
```

If the target is obvious from the conversation, review that target. If the target is not obvious, ask what should be reviewed.

Default output shape:

```text
Target
  What is being reviewed.

Verdict
  Honest conclusion first when helpful.

Strong points
  What is good, likely correct or worth preserving.

Weak points / risks
  What can fail, is underspecified, misleading, brittle or too broad.

Hidden assumptions
  What must be true for the plan/claim to work.

Alternatives / adjustments
  Better route, narrower batch, safer wording or trade-off if one exists.

Confidence / checks
  Confidence level, checked/not checked sources and what would change the verdict.
```

Rules:

```text
- Do not disagree just to disagree.
- Do not become hostile or performative.
- Do not invent evidence.
- Do not hide uncertainty.
- Do not override explicit user constraints silently.
- Do not reopen accepted decisions unless there is a clear reason to re-evaluate them.
- Do not treat `крит` as permission to edit files, create archives, commit or push.
- If source checks are needed for a fair verdict, say what was checked and what was not checked.
```

Reusable example:

```text
planning/documentation/examples/CRITICAL-REVIEW-COMMAND-EXAMPLE.md
```


## 6. Level 3 — Review / Handoff Answer

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

Use Level 3 when the output will be passed to another chat/person for review or continuation.

Typical cases:

```text
- audit report;
- documentation update plan review;
- architecture/source-versioning proposal;
- domain/slice/scenario draft review;
- implementation handoff;
- post-edit summary needing verification;
- high-risk repo/doc consistency conclusion;
- broad cross-layer review;
- major migration/cascade review.
```

Template:

```text
## 1. Task / Role / Mode

Task:
...

Role:
...

Mode:
answer / plan / draft / review / apply / handoff

Status:
complete / partial / blocked / needs review

## 2. Scope

In scope:
- ...

Out of scope:
- ...

Explicitly not doing:
- ...

## 3. Sources / Evidence / Coverage

Checked:
- ...

Not checked:
- ...

Valid sources for this task:
- ...

Supporting / non-canonical sources:
- ...

Evidence limits:
- ...

## 4. Result

...

## 5. Findings / Decisions

Finding 1:
- source:
- meaning:
- impact:

Finding 2:
- source:
- meaning:
- impact:

## 6. Assumptions / Questions / Risks

Assumptions:
- ...

Blocking questions:
- ...

Non-blocking questions:
- ...

Risks:
- ...

## 7. Verification

Self-check performed:
- ...

How another chat/person can verify:
- ...

Suggested reviewer focus:
- ...

## 8. Handoff Summary

For another chat:
- task:
- scope:
- key result:
- sources checked:
- sources not checked:
- risks:
- what to review:
- next action:
```

Level 3 is not mandatory for every answer.

Use it when the answer needs to survive outside the original chat context.

## 6A. File Update Overview / `План файл-обновление` For File/Docs/Code Updates

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

For Level 2 or Level 3 answers that plan, create, review or verify non-trivial file, documentation or code changes, end the normal reviewable answer with a File Update Overview / `План файл-обновление` when a structured file-change summary would help review.

Command aliases / triggers:

```text
план файл-обновление
спланируй файл-обновление
спланируй обновление файлов
спланируй архив
план архива
file update plan
archive plan
итог  # legacy shorthand only in file/change/update context
```

These commands request the file/update planning overview. They do not grant edit, archive/package, commit or push permission.

Use:

```text
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
```

The File Update Overview / `План файл-обновление` is a final summary block. It does not replace the main answer, `Краткое саммари` or `Goal Map Brief`.

Placement:

```text
main reviewable answer
+
Краткое саммари, when useful
+
Goal Map Brief / Карта цели, when triggered
+
План файл-обновление, when file/change/update context applies
```

Ownership boundary:

```text
reviewable-agent-output-and-commands-workflow.md
  owns when/where `План файл-обновление` appears and response-level command semantics.

file-update-overview-workflow.md
  owns how to produce the `План файл-обновление` / File Update Overview content.

FILE-UPDATE-OVERVIEW-TEMPLATE.md
  owns the exact reusable Markdown shape.
```

During planning, `План файл-обновление` is the current rolling nearest-batch plan. After artifact/diff/application, it summarizes the actual update state.

Use-case rows may reference File Update Overview as an expected output shape, but they do not own its format.

## 7. Sources And Coverage Rule

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

For non-trivial tasks, explicitly separate:

```text
Checked sources
  sources actually read or inspected.

Not checked sources
  relevant sources that were not inspected, with reason if important.

Valid sources for this task
  source types that can answer the question.

Supporting / non-canonical sources
  useful context that should not override canonical/current sources.

Answer limits
  what the answer cannot prove because of missing checks, stale sources or narrow scope.
```

Examples of valid source types:

```text
current implementation truth
  current branch, code, tests, migrations, generated contracts, runtime screenshots

scenario behavior truth
  scenario text specs, scenario DATA, scenario UI specs, behavior items, clarifications

domain direction
  domain drafts, accepted domain decisions, implementation cuts, domain tests if checking implementation

slice planning
  slice docs, source registers, behavior coverage tables, client sidecars

documentation architecture
  planning docs architecture principles, responsibility map, documentation workflows

VKR / thesis clean wording
  VKR clean docs, thesis workflow docs, evidence maps
```

Do not imply that an answer was repo-grounded if no current repo evidence was checked.

## 8. Section-Level Sources For Drafts

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

For large drafts, the whole-answer source block may not be enough.

Major sections should declare their own sources when a section:

```text
- is edited independently;
- is high-risk;
- depends on several upstream sources;
- is reviewed by another chat;
- will become input for another draft;
- drives tests, implementation or source-version sync.
```

Minimal section sources block:

```text
Sources:
  Format:
  - ...

  Content:
  - ...

  Internal dependencies:
  - ...

  Not checked:
  - ...
```

Use `Format` sources for docs that define how the section should be written.

Use `Content` sources for docs/code/evidence that provide the actual information.

Use `Internal dependencies` for previous or sibling sections of the same draft that this section depends on.

Use `Not checked` to expose relevant sources that were not checked in this pass.

## 9. Expanded Section Basis

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

Use an expanded section basis only when needed.

Template:

```text
Section basis:

Purpose:
...

Format sources:
- ...

Content sources:
- ...

Internal dependencies:
- ...

External dependencies:
- ...

Not checked:
- ...

Assumptions:
- ...

Must re-check if changed:
- ...

Review focus:
- ...
```

Do not add this heavy block to every section by default.

## 10. Response-Level Commands

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

The user may ask the same chat or another chat to perform response-level operations.

Response-level commands affect answer format, checking behavior, active context or reuse of previous context. They do not grant permission to edit files, commit changes, delete files, move files, create PRs or skip necessary evidence checks for claims that require current proof.

### Response Block Commands

These commands request or suppress response blocks. They do not change edit permission, source requirements or repository state.

```text
кп / key points
  Force `Key points first`.

без кп / без key points
  Omit `Key points first`.

отличия драфта / draft diff
  Add `Отличия от предыдущего драфта` for an active draft update.

саммари
  Add `Краткое саммари`.

без саммари
  Omit `Краткое саммари`.

план файл-обновление / спланируй файл-обновление / спланируй обновление файлов / спланируй архив / план архива
  Produce or update the File Update Overview / `План файл-обновление` in planned mode for a file/docs/code/archive update plan. This does not create an archive or edit files by itself.

итог
  Legacy shorthand for `План файл-обновление` only when file/change/update context is active.

без план файл-обновления / без итога
  Omit `План файл-обновление` / legacy `Итог`.

полный конец
  Add `Краткое саммари` and `План файл-обновление` when file/change/update overview is applicable.
```

Rules:

```text
- `Key points first` has no fixed internal format; it mirrors the detailed answer in compressed points with key information plus conclusion/practical meaning.
- `Краткое саммари` is contextual/human summary, not a file-change register.
- `План файл-обновление` is file/change/update-oriented and must stay last when present.
- Do not add `План файл-обновление` for ordinary drafting, casual explanation or strict-template output unless there is a file/change/update context.
- If the user requests `итог` without a file/change/update context, explain that legacy `Итог` / `План файл-обновление` is not applicable and offer `Краткое саммари`.
```

### Obs / Discussion Context Recheck

Canonical command:

```text
обс
```

Aliases:

```text
перепроверь обсуждение
перепроверь предыдущие обсуждения
вспомни договорённости
context recheck
discussion recheck
```

Purpose:

```text
Re-check relevant prior discussion, accepted decisions, rejected options, naming, scope, non-goals, constraints and follow-ups before answering.
```

`обс` changes prior-discussion coverage. It does not change edit permission and does not by itself force Level 2 or Level 3.

It can combine with any response level:

```text
обс + narrow task with no Level 2/3 triggers
  may produce Level 1 after the relevant discussion is checked.

обс + planning / archive / audit / update task
  normally produces Level 2.

обс + standalone handoff / broad audit
  may produce Level 3.
```

The agent should:

```text
- identify relevant prior discussion available in the current context;
- preserve accepted decisions unless the user asks to reopen them;
- distinguish prior discussion from current repo/file evidence;
- avoid reinventing an already accepted command, mode or boundary;
- report important prior context that was unavailable or not checked;
- avoid treating `обс` as permission to edit files or skip current evidence checks.
```

### Recheck

Purpose:

```text
Find mistakes, contradictions, missing sources, scope creep and stale assumptions.
```

The answer should cover:

```text
- possible errors found;
- missed sources or weak evidence;
- scope or source-of-truth problems;
- current/planned/draft confusion;
- hidden assumptions;
- corrections or recommended fixes;
- whether another review is needed.
```

### Clarify

Purpose:

```text
Make the answer more precise without necessarily changing the conclusion.
```

The answer should cover:

```text
- what was too broad or vague;
- refined boundaries;
- stronger source/source-limit statements;
- clearer assumptions;
- sharper risks;
- updated wording.
```

### Expand

Aliases:

```text
expand
расширь
добавь подробнее
добавь примеры
```

Purpose:

```text
Add depth, examples, edge cases or explanation while preserving the current scope unless the user explicitly changes it.
```

The agent should state which dimension is being expanded when useful:

```text
- explanation depth;
- examples;
- edge cases;
- source coverage;
- algorithm steps;
- template content.
```

If expansion would change scope, say so instead of silently broadening the answer or draft.

### Planning Command

Aliases:

```text
планируй
спланируй
давай план
распланируй
plan
```

Purpose:

```text
Produce a concrete action plan now for the implied or named future task.
```

The agent should:

```text
- reconstruct enough context for the task;
- identify files/sources/examples to read;
- define the action sequence;
- define changed files and not-changed files when file work is likely;
- define acceptance criteria;
- define archive/batch boundary when archive generation is the next action.
```

The agent should not:

```text
- answer only that the task needs to be planned later;
- defer planning unless the plan depends on a check/action that must happen first;
- treat planning as permission to edit files or generate an archive.
```

If part of the work is intentionally deferred to a later batch/action, state the boundary and why.

### Keep Previous

Aliases:

```text
keep prev
keep previous
кип прев
оставь прошлое
сохрани предыдущий ответ
```

Purpose:

```text
Apply the user's correction, clarification or additional constraint while preserving the previous answer's structure, scope and useful content.
```

Use this when the user gives a note such as:

```text
keep prev, but add that archive files were not checked
кип прев, но учти что tables/domain should be treated as domain layer
keep previous and make it lvl 2
оставь прошлое, только добавь source coverage section
```

The agent should:

```text
- treat the previous answer as the base version;
- keep the previous structure unless the user asks to change it;
- apply the user's new correction or constraint;
- preserve useful unchanged sections;
- update only the affected parts when possible;
- avoid silently dropping previous caveats, sources, risks or next steps;
- mention if the new instruction conflicts with the previous answer.
```

The agent should not:

```text
- restart from scratch unless necessary;
- replace the previous answer with an unrelated new answer;
- drop sources/coverage just because the user gave a small correction;
- treat `keep prev` as approval to ignore the new correction.
```

### Active Draft / Active Answer Continuation

Aliases:

```text
драфт
давай драфт
покажи драфт
обнови
обнови драфт
актуализируй
continue
продолжи
```

Purpose:

```text
Continue the active draft/answer/plan instead of starting a new one.
```

If there is an active draft in the current conversation, then `драфт`, `давай драфт`, `покажи драфт`, `обнови`, `обнови драфт` and `актуализируй` mean:

```text
- identify the active draft;
- apply latest discussion deltas since the last shown/updated version;
- if the draft is in canvas, update or verify the canvas;
- if no canvas is used, output the latest actual draft version in chat;
- do not start a new draft unless the user says `новый драфт` or names another target;
- do not switch back to an older draft unless the user explicitly names it.
```

If there is no active draft/answer/plan, ask for the target unless the target is obvious from the current message.

For active draft updates, include `Отличия от предыдущего драфта` when the user asks for it or when the changes are non-trivial enough that the user needs to review what changed before reading the updated draft.

### No Changes / No Ch

Aliases:

```text
no ch
no changes
state unchanged
изм нет
изменений нет
ничего не менялось
без изм
б изм
```

Purpose:

```text
Let the user assert that the repository/document state has not changed since the last assistant-checked or assistant-committed state in this chat.
```

Effect:

```text
- do not repeat broad audits only to re-establish already-known context;
- reuse the last checked context from this conversation when the user explicitly says the state is unchanged;
- perform only targeted reads needed for the requested answer or edit;
- for discussion/planning, usually no new repo reads are needed unless the task introduces a new file/scope;
- before GitHub writes, still fetch the exact target file to get current content and SHA;
- before deletion/rename/reference-cleanup claims, still run targeted search if the answer claims references are clean;
- if another chat, local files, archive state or unpushed changes may be involved, ask for confirmation/archive instead of trusting stale remote assumptions.
```

Limits:

```text
- `no ch` / `без изм` reduces unnecessary checks; it does not remove evidence requirements.
- Do not claim current implementation/repo truth without evidence when the task requires proof.
- Do not use `no ch` to skip source checks for new scope, new files or high-risk claims.
- Do not treat `no ch` as permission to edit files.
```

Recommended response behavior:

```text
- acknowledge that broad re-audit is skipped because user asserts no changes;
- state which last checked context is being reused when helpful;
- list only targeted checks performed, if any;
- be explicit when a minimal check is still required before an edit or a cleanup claim.
```

### Use Archive / Archive Source

Aliases:

```text
use archive
archive source
archive src
читать архив
используй архив
репо = архив
repo equals archive
арх
из арх
из архива
```

Purpose:

```text
Let the user assert that an uploaded archive should be used as the repository/document snapshot for read-only checks and analysis.
```

Effect:

```text
- when repository information is needed for a read-only answer, prefer reading/searching the uploaded archive over remote GitHub fetch/search;
- treat the archive as the current repo snapshot only within the scope explicitly stated by the user;
- use the archive for broad text/link/path checks when the user says it matches the repo state;
- if user says `арх` without attaching a new archive, use the latest uploaded archive in the current conversation as the archive source;
- record in the answer that archive snapshot was used as the read source;
- combine with `no ch` / `без изм` when the user asserts both unchanged state and archive/repo equivalence.
```

Limits:

```text
- `use archive` / `арх` does not decide how much to check; it only chooses read source when checks are needed.
- `use archive` does not prove remote branch state if another chat/user may have pushed changes after the archive was created.
- A new chat cannot know an old archive unless it is uploaded again.
- Before GitHub writes, still fetch the exact target file from GitHub to get current content and SHA.
- Before a final claim that remote links/references are clean, either search the whole archive snapshot or run a targeted GitHub search when the claim is about the remote branch.
- Do not use an archive as implementation truth when the task requires current runtime/code/test evidence and the archive may be stale.
- Do not treat `use archive` as permission to edit files.
```

Recommended response behavior:

```text
- acknowledge that archive snapshot will be used for read checks;
- state whether archive is being treated as authoritative or only supporting evidence;
- say which archive/source snapshot was searched when relevant;
- fall back to targeted GitHub checks only for writes, remote-cleanliness claims or uncertainty about archive freshness.
```

### No Changes + Archive Compound Command

Aliases:

```text
без изм, арх
б изм, арх
изм нет, арх
no ch, archive
б из арх
```

Meaning:

```text
Unchanged state + archive read source.
```

Behavior:

```text
- skip broad re-audit;
- reuse previous traversal/context where valid;
- read archive only for needed targeted checks;
- use the latest uploaded archive in the current conversation if no new archive is attached;
- do not use GitHub unless write/SHA or remote-cleanliness claim requires it.
```

### Source Delta For Updates

Use Source Delta when an answer/draft changes because of:

```text
- user correction;
- new source;
- recheck;
- self-correction;
- source coverage change;
- active draft update.
```

Short shape:

```text
Source Delta:
- Previous basis:
- Newly used this pass:
- User-provided sources/constraints:
- Not rechecked:
- Impact:
- Default source model changed? yes/no
```

Rules:

```text
- Default/expected sources belong in templates, source blocks or section definitions.
- Sources used this pass belong in the chat answer/update report.
- Additional sources named by user do not automatically become default sources.
- Promote a source to default only through explicit docs/template/workflow update.
```

### Show Section Separately

Purpose:

```text
Extract one section for focused review or improvement.
```

The extracted section should include:

```text
- purpose;
- content;
- format sources;
- content sources;
- internal dependencies;
- external dependencies, if relevant;
- assumptions;
- must re-check if changed;
- review focus.
```

### Merge Section Back

Purpose:

```text
Return an updated section into the full draft and check cross-section consistency.
```

The answer should check:

```text
- whether the updated section still matches scope;
- whether downstream sections need updates;
- whether questions/risks/verification changed;
- whether source/version metadata needs updates;
- whether the full draft remains consistent.
```

## 11. Interaction With Specialized Formats

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

Specialized formats take priority.

Examples:

```text
Documentation Update Plan
scenario draft template
domain draft template
slice draft template
diagram preflight format
review result format
```

This workflow does not replace those formats.

Instead:

```text
- use the specialized format;
- keep sources/coverage visible;
- do not add `Key points first` when the specialized template already provides clear navigation;
- use `Отличия от предыдущего драфта` for active draft updates instead of forcing key points;
- add section-level sources for major sections when needed;
- add handoff/reviewer notes if another chat should review the output;
- use planning-use-case-map.md for active-context, traversal-depth and read-source decisions.
```

## 12. Do Not

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

```text
- Do not force a heavy format on every casual answer.
- Do not treat Level 1 as permission to skip necessary reasoning, context checks, source checks, safety checks or accepted-decision preservation.
- Do not make the user ask for Level 2 when Level 2 triggers are already present.
- Do not hide unchecked sources.
- Do not claim current implementation truth without checking current repo evidence.
- Do not mix canonical sources with supporting/historical sources without labeling them.
- Do not make assumptions invisible in prose.
- Do not make another chat guess what was checked.
- Do not use section-level source blocks as a substitute for actually checking sources.
- Do not ignore a `keep prev` correction by rewriting the answer from scratch without preserving the useful previous structure/content.
- Do not use `no ch` / `без изм` to skip targeted checks that are required before writes, deletes, renames or current-state claims.
- Do not use `use archive` / `арх` to overclaim remote branch truth when archive freshness is uncertain.
- Do not treat `драфт` / `обнови` as a new draft request when there is an active draft context.
- Do not treat `обс` as permission to edit files, skip current evidence checks or reopen accepted decisions without request.
- Do not use `Key points first` to replace the detailed answer, source/coverage visibility, risks, verification or next step.
- Do not add `План файл-обновление` as a generic conclusion when there is no file/change/update context.
- Do not add `Key points first` to draft updates when `Отличия от предыдущего драфта` is the useful review block.
- Do not silently promote a one-pass additional source into a default template/source requirement.
```

## 13. Success Criteria

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
    - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/documentation/examples/** @ mixed versions/statuses
  Internal dependencies:
    - Core Rule
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-3A
```

This workflow works when:

```text
- a person can see the answer's scope and limits;
- another chat can review the answer without guessing hidden context;
- unchecked sources are visible;
- source-of-truth boundaries are clear;
- assumptions and risks are not hidden;
- important draft sections can be extracted, reviewed and merged back safely;
- small corrections can be applied with `keep prev` without losing useful prior structure or caveats;
- unchanged-state hints can be applied with `no ch` / `без изм` without repeating broad audits or skipping required evidence checks;
- archive snapshot hints can be applied with `use archive` / `арх` without confusing archive evidence with remote/current proof;
- active draft/update commands continue the active work instead of restarting from scratch;
- Source Delta makes newly used sources and not-rechecked sources visible when an answer/draft changes;
- Level 2/3 escalation happens automatically when task breadth requires reviewability;
- `обс` can re-check prior discussion without being confused with edit permission or answer level;
- `Key points first`, `Краткое саммари` and `План файл-обновление` improve navigation without replacing the Level 2/3 reviewable body;
- `Key points first` has no fixed internal format and mirrors the detailed answer points in compressed form;
- `Краткое саммари` starts with вывод and next actions before goal/context/limits;
- file-update planning answers expose delivery safety in the main answer and `План файл-обновление` when it affects artifact generation;
- draft updates use `Отличия от предыдущего драфта` when that is the useful review block;
- File Update Overview / `План файл-обновление` is used as a final summary block when non-trivial file/docs/code updates need file responsibility/change visibility;
- response structure helps verification without adding unnecessary bureaucracy.
```


## Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
  Internal dependencies:
    - Purpose
  Not checked:
    - Goal Map/Tampermonkey and slice source passes outside ROOT-SRC-3A
```

```text
- ROOT-SRC-3A added Doc version and local section-level Sources blocks to this output/archive workflow file.
- ROOT-SRC-3A did not change output semantics or grant edit/commit permission.
```
