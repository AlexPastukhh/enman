# Plan Command Valid Execution Example

Status: current reusable response/command example  
Scope: demonstrates valid execution of `планируй` / `спланируй` / `давай план`

## What This Demonstrates

Owner files:

```text
planning/planning-use-case-map.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

This example demonstrates valid execution only. It does not own routing, command semantics or permission boundaries.

## Example

User says:

```text
спланируй command examples update
```

Valid assistant behavior:

```markdown
## Key points

- The command asks for a concrete plan now, not a statement that planning should happen later.
- The plan must identify owner files, example files, changed/not-changed boundaries and acceptance criteria.
- Planning does not grant permission to edit files or create an archive unless the user asks for that output.

## Plan

Task: update command/example routing so valid execution examples are part of the traversal chain.

Files to read:
- planning/planning-use-case-map.md
- planning/documentation/reviewable-agent-output-and-commands-workflow.md
- planning/documentation/examples/README.md
- planning/documentation/example-coverage-workflow.md

Planned changes:
- add example references to the use-case traversal chain;
- create reusable command examples under planning/documentation/examples/;
- create Enman-specific scenario/domain/slice example under planning/documentation/examples/project-specific/enman/;
- keep examples demonstration-only;
- do not embed long examples into the root use-case map.

Not changing:
- no domain/slice/scenario draft edits;
- no source register changes;
- no direct repo writes without separate approval.

Acceptance criteria:
- use-case map remains the router;
- examples live in separate files;
- relevant routes include examples in the traversal chain;
- permission boundaries remain explicit.

Next action:
- after plan approval, create replacement archive.

## Краткое саммари

Вывод:
  The plan is concrete now and names files, changes, non-changes and acceptance criteria.

Следующие действия:
  Create the archive only if the user says `давай архив`.

Цель понял так:
  Prepare command examples without putting long example text inside the root use-case map.

Учтённый контекст:
  - examples are supporting artifacts;
  - root use-case map owns traversal;
  - workflows own command semantics.

Границы:
  - No files were edited by the planning answer itself.
```

## Invalid Execution

```text
- “We need to plan this later.”
- Creating an archive without the user asking for archive output.
- Editing files because the user said `планируй`.
```
