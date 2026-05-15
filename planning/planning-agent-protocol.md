# Planning Agent Protocol

Status: current collaboration protocol  
Scope: AI/chat agents working with scenarios, behavior items, UI specs, slices, client sidecars, ADRs, responsibility maps and replacement packages

## 1. Purpose

This file centralizes how planning agents should work in this repository.

It complements:

```text
planning/planning-workflow-current.md
planning/planning-doc-responsibility-map.md
planning/replacement-file-generation-guide.md
planning/scenario-specification-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
planning/adr/adr-workflow.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

## 2. Core Rule

Do not continue implementation planning through a question that may change required behavior.

If ambiguity affects scenario behavior, DATA, UI-visible requirements, validation/security, API contract, user-visible outcome or cross-layer responsibility, stop and ask.

## 3. Relevant Questions Rule

Ask questions when the answer can affect the current step.

A question is relevant when it can change required behavior, DATA, UI-visible requirements, validation/security, API contract, file placement, slice boundary, client/server responsibility, client architecture mapping, component placement, extension/change point decisions, extension pressure, ADR capture, test expectations, next action ordering or archive contents.

If a question is not relevant to the current step, record it in the appropriate register if it should not be lost.

## 4. Assumptions With Questions Rule

When asking a question, include:

```text
Question:
...

Assumption / preferred answer:
...

Why it matters for this step:
...
```

## 5. Scenario / UI Question Loop

```text
question about scenario / DATA / UI / validation / visible outcome
-> classify as scenario-level, UI-level, or implementation-only
-> add/update scenario questions register if scenario-level
-> update scenario UI spec if UI-visible requirement changed
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update validation/security addendum if needed
-> update behavior items / UI behavior items if required behavior changed
-> update architecture-decision-notes / adr-candidates if architecture decision changed
-> continue implementation planning
```

## 6. Slice Intake Rule

Before starting a new slice or `.client.md` sidecar:

```text
1. Read current planning entry points.
2. Read planning/adr/architecture-decision-notes.md.
3. Read planning/adr/adr-candidates.md.
4. Read the target scenario text spec.
5. Read the target scenario DATA file.
6. Read relevant validation/security addendum entries.
7. Read relevant per-scenario behavior items.
8. Read relevant scenario UI spec if client-visible behavior is involved.
9. Check planning/diagrams/scenario-questions-register.md.
10. Check planning/slices/slice-implementation-notes-register.md.
11. Check planning/slices/slice-extension-points-register.md.
12. Check relevant shared support docs.
13. If client work is involved, check planning/client/ and planning/slices/client-architecture-principles.md.
14. Promote relevant notes/questions/extension pressure/ADR decisions.
15. Only then create/refine the slice file or `.client.md`.
```

## 7. ADR Capture Rule

When planning or implementation reveals an architectural decision, classify ADR impact:

```text
no ADR relevance
add/update architecture decision note
add/update ADR candidate
propose full ADR promotion
```

Use:

```text
planning/adr/adr-workflow.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

`architecture-decision-notes.md` is the accepted decision registry.

`adr-candidates.md` is the promotion backlog, not the primary guiding source.

Do not create full numbered ADRs unless explicitly requested.

## 8. Change / Extension Point Rule

When making implementation decisions, classify potential seams as:

```text
hard invariant
current behavior change point
future extension point
extension pressure
unnecessary abstraction
```

For each known extension pressure case, decide explicitly:

```text
- create explicit seam now;
- avoid coupling only;
- follow current convention and accept possible future refactor;
- ignore for now because certainty is low;
- revisit when a later slice starts.
```

Record the decision locally and, when it affects future slices, in `planning/slices/slice-extension-points-register.md`.

## 9. Client Sidecar Rule

Do not create `.client.md` files in advance.

Create a client sidecar only when work starts on the concrete client layer of a concrete slice.

Client sidecar owns coverage, UI behavior, questions, architecture mapping, extension/change/pressure sections, component plan, styling, accessibility, API contract use, implementation flow, types and client tests.

## 10. Behavior Items Rule

Behavior items are derived from scenario flow + branches + invariants + outcomes + DATA + validation/security addenda.

UI behavior items are derived from scenario-visible UI expectations + scenario UI specs + validation feedback + action availability + accessibility-relevant requirements.

They must not invent new behavior.

## 11. File Responsibility Rule

Use `planning/planning-doc-responsibility-map.md` to decide where content belongs.

If content is a global workflow/common rule, do not bury it in a local scenario, DATA, behavior item, UI spec, slice or client sidecar file.

## 12. Next Step Protocol

Every planning response, archive summary and implementation prompt should explicitly state the next step.

Use this structure when useful:

```text
Current state:
Relevant questions for this step:
Assumptions used:
Blocking questions:
ADR impact:
Next action:
Files to read:
Files to create/update:
Do not do:
Success criteria:
Stop and ask if:
```

The agent must not move to the next slice or implementation step by itself.

## 13. Archive Rule

When asked to create a replacement package, follow `planning/replacement-file-generation-guide.md`.

Generate complete replacement files, not patches.

Before generating an archive:

```text
1. Check current repository docs when possible.
2. Ask relevant blocking questions for this archive.
3. Include assumptions with questions.
4. Keep archive scope focused.
5. Include MANIFEST.md and APPLY.md.
6. State what is deliberately out of scope.
7. State ADR impact when relevant.
```

## 14. Do Not

```text
- Do not create `.client.md` before concrete client work starts.
- Do not continue planning through scenario-level ambiguity.
- Do not ask irrelevant future questions during a focused step.
- Do not ask a question without giving the current assumption/preferred answer.
- Do not turn every future extension into abstraction now.
- Do not ignore known extension pressure when planning current implementation.
- Do not lose accepted architecture decisions in chat/local docs only.
- Do not use adr-candidates as the sole guiding source.
- Do not create full numbered ADRs unless explicitly requested.
- Do not bury global workflow rules in local files.
- Do not auto-continue to the next slice without a user request.
```
