# Critical Review Command Example

Status: current reusable response-command example / demonstration-only  
Scope: demonstrates valid execution of the `крит` / critical review response modifier command

## 1. Purpose

This example demonstrates how `крит` changes answer mode.

Owner files:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/planning-use-case-map.md
```

This example is demonstration-only. It does not own command semantics, routing, source truth, output modes, permission boundaries or living map state.

## 2. User Command Examples

```text
крит
критически оцени
проверь критически
оцени честно
не соглашайся автоматически
за и против
critical review
```

## 3. Meaning

`крит` tells the assistant to treat the current target as a hypothesis and evaluate it honestly.

It may be used alone when the current target is obvious, or combined with another route:

```text
крит план
крит архив
крит этот слайс
крит кц
```

The command does not grant permission to edit files, create archives, commit or push.

## 4. Example Input

User:

```text
крит, оцени этот план: сразу добавить команду `крит`, обновить use-case map, добавить пример, обновить living Goal Map и потом перейти к Tampermonkey.
```

## 5. Example Output

## Critical Review / `крит`

Target:
  План: добавить команду `крит`, route, example, living Goal Map update, then move to Tampermonkey.

Verdict:
  Direction is good, but the batch boundary needs one correction: finish `крит` fully first, then choose between Generic Action Overview and Tampermonkey from the living Goal Map instead of jumping directly to Tampermonkey.

Strong points:
  - The command belongs in `reviewable-agent-output-and-commands-workflow.md`, because it changes answer mode.
  - Root use-case map should route it, but not own behavior.
  - A command-specific example is useful because each command should show expected execution and boundaries.
  - Updating the living Goal Map is appropriate because this is a meaningful workstream status change.

Weak points / risks:
  - Jumping directly to Tampermonkey may skip `SL-5 — Generic Action Overview`, which is still a planned workstream slice.
  - The batch could become too broad if it also changes project-specific Enman command-routing examples.
  - If `крит` is written as “always disagree”, it will produce performative criticism instead of honest review.

Hidden assumptions:
  - The current target is clear enough to review without asking a follow-up.
  - The existing examples index should list command examples, but project-specific examples do not need to be updated for every new command.
  - Living Goal Map state is current enough to update from this batch.

Alternatives / adjustments:
  - Keep this batch limited to command semantics, root route, command example, living Goal Map update and action log.
  - Do not update project-specific Enman route examples in this batch.
  - After commit, choose the next slice explicitly from the living Goal Map.

Confidence / checks:
  - Confidence: medium-high.
  - Check the owner workflow and root use-case map before applying.
  - Check the resulting diff before commit.
  - If the living Goal Map has changed since the last read, refresh the plan before generating the archive.

Boundary:
  - This critical review does not edit files or create an archive by itself.
  - It does not override the user's final choice; it flags the safer route and risks.

## 6. Rules Demonstrated

```text
- The answer reviews the target as a hypothesis.
- It states a verdict without automatic agreement.
- It includes strengths, weaknesses, hidden assumptions, alternatives and checks.
- It does not disagree just to disagree.
- It does not imply edit/archive/commit permission.
```
