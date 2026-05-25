# Reviewable Agent Output Workflow

Status: current response-quality workflow  
Scope: how chats/agents should structure non-trivial answers so a person or another chat can review, verify and continue the work

## 1. Purpose

This file defines how an agent should format important answers.

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

## 2. Core Rule

Use the smallest response format that remains reviewable.

Do not turn every casual answer into a heavy report.

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
casual / quick question -> Level 1
non-trivial planning / docs / repo analysis -> Level 2
handoff / external review / audit / high-risk answer -> Level 3
```

## 4. Level 1 — Short Answer

Use Level 1 for simple questions or quick decisions.

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
- Do not include a full sources block unless the answer depends on specific checked files.
```

## 5. Level 2 — Default Serious Answer

Use Level 2 for non-trivial planning, documentation, repo analysis, draft discussion or architectural reasoning.

Template:

```text
## 1. Short Conclusion

...

## 2. Task And Scope

Task understood as:
...

In scope:
- ...

Out of scope:
- ...

## 3. Sources And Coverage

Checked:
- ...

Not checked:
- ...

Valid sources for this task:
- ...

Supporting / non-canonical sources:
- ...

Answer limits:
- ...

## 4. Answer / Result

...

## 5. Assumptions, Questions And Risks

Assumptions:
- ...

Open questions:
- ...

Risks:
- ...

## 6. Verification

How to verify:
- ...

What another chat/person should check:
- ...

## 7. Next Step

...
```

The `Sources And Coverage` section is the most important part of this level.

It allows another chat to:

```text
- see what the first chat actually checked;
- inspect sources that were not checked;
- challenge invalid source choices;
- verify whether the answer is based on current docs, current code, historical notes or assumptions.
```

## 6. Level 3 — Review / Handoff Answer

Use Level 3 when the output will be passed to another chat/person for review or continuation.

Typical cases:

```text
- audit report;
- documentation update plan review;
- architecture/source-versioning proposal;
- domain/slice/scenario draft review;
- implementation handoff;
- post-edit summary needing verification;
- high-risk repo/doc consistency conclusion.
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

## 7. Sources And Coverage Rule

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

## 10. Recheck / Clarify / Keep Previous / Section Commands

The user may ask the same chat or another chat to perform response-level operations.

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

Recommended mini-format when useful:

```text
Applied change:
- ...

Previous answer preserved:
- structure: yes/no
- scope: yes/no
- important limits: yes/no

Updated answer:
...
```

If the previous answer is too long to fully repeat, the agent may say that it is preserving the previous structure and then provide only the updated sections, unless the user explicitly asks to output the full updated answer.

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
- add section-level sources for major sections when needed;
- add handoff/reviewer notes if another chat should review the output.
```

## 12. Do Not

```text
- Do not force a heavy format on every casual answer.
- Do not hide unchecked sources.
- Do not claim current implementation truth without checking current repo evidence.
- Do not mix canonical sources with supporting/historical sources without labeling them.
- Do not make assumptions invisible in prose.
- Do not make another chat guess what was checked.
- Do not use section-level source blocks as a substitute for actually checking sources.
- Do not ignore a `keep prev` correction by rewriting the answer from scratch without preserving the useful previous structure/content.
```

## 13. Success Criteria

This workflow works when:

```text
- a person can see the answer's scope and limits;
- another chat can review the answer without guessing hidden context;
- unchecked sources are visible;
- source-of-truth boundaries are clear;
- assumptions and risks are not hidden;
- important draft sections can be extracted, reviewed and merged back safely;
- small corrections can be applied with `keep prev` without losing useful prior structure or caveats;
- response structure helps verification without adding unnecessary bureaucracy.
```
