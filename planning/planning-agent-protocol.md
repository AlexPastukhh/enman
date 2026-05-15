# Planning Agent Protocol

Status: current collaboration protocol  
Scope: AI/chat agents working with scenarios, behavior items, slices, client sidecars, responsibility maps and replacement packages

## 1. Purpose

This file centralizes how planning agents should work in this repository.

It complements:

```text
planning/planning-workflow-current.md
planning/planning-doc-responsibility-map.md
planning/replacement-file-generation-guide.md
planning/scenario-specification-principles.md
planning/slices/l1-slice-drafting-guide.md
```

## 2. Core Rule

Do not continue implementation planning through a question that may change required behavior.

If ambiguity affects scenario behavior, DATA, validation/security, API contract, user-visible outcome or cross-layer responsibility, stop and ask.

## 3. Relevant Questions Rule

The agent should not ask questions just because questions are possible.

The agent must ask questions when the answer can affect the current step.

A question is relevant to the current step if the answer can change:

```text
- required behavior;
- DATA;
- validation/security;
- API contract;
- file responsibility / placement;
- slice boundary;
- client/server responsibility;
- test expectations;
- next action ordering;
- archive contents.
```

If a question is not relevant to the current step:

```text
- do not interrupt the current work with it;
- record it in the appropriate register if it should not be lost;
- mention it only as future/non-blocking when useful.
```

## 4. Assumptions With Questions Rule

When the agent asks a question, it should include its current assumption or preferred answer.

Format:

```text
Question:
...

Assumption / preferred answer:
...

Why it matters for this step:
...
```

This keeps planning moving and helps the user answer quickly.

If the user accepts the assumption, use it as the current direction.

## 5. Scenario Question Loop

When a scenario-level question appears during domain drafting, slice drafting, client sidecar planning or implementation planning:

```text
question about scenario / DATA / validation / visible outcome
-> classify as scenario-level or implementation-only
-> add/update scenario questions register if scenario-level
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update validation/security addendum if needed
-> update behavior items if required behavior changed
-> continue implementation planning
```

Scenario-level questions belong in:

```text
planning/diagrams/scenario-questions-register.md
```

Implementation-only questions belong in the parent slice file, `.client.md` sidecar, or:

```text
planning/slices/slice-implementation-notes-register.md
```

## 6. Slice Intake Rule

Before starting a new slice or `.client.md` sidecar:

```text
1. Read current planning entry points.
2. Read the target scenario text spec.
3. Read the target scenario DATA file.
4. Read relevant validation/security addendum entries.
5. Read relevant per-scenario behavior items if they exist.
6. Check planning/diagrams/scenario-questions-register.md.
7. Check planning/slices/slice-implementation-notes-register.md.
8. Check relevant shared support docs under planning/slices/shared/.
9. Promote relevant notes/questions.
10. Only then create/refine the slice file or .client.md.
```

## 7. Client Sidecar Rule

Do not create `.client.md` files in advance.

Create a client sidecar only when work starts on the concrete client layer of a concrete slice.

Parent slice file owns:

```text
- scenario/vertical behavior;
- behavior item coverage summary;
- API contract;
- cross-layer flow;
- application/domain/persistence responsibilities;
- server/integration testing responsibilities.
```

Client sidecar owns:

```text
- Client Behavior Coverage;
- Client Implementation Questions Register;
- Scenario / DATA Coverage;
- routes;
- views;
- components;
- query functions;
- mutation functions;
- hooks;
- form state / DTO mapping;
- cache invalidation;
- client tests.
```

The client sidecar must use the API contract from the parent slice file.

## 8. Behavior Items Rule

Behavior items are derived from:

```text
scenario flow + branches + invariants + outcomes + DATA + validation/security addenda
```

They must not invent new behavior.

Use the same category/card/table style as:

```text
planning/tables/pre-domain-variants-input.md
```

Do not introduce new categories without an explicit decision.

## 9. Implementation Notes Register Rule

Before starting any new slice/client sidecar, check:

```text
planning/slices/slice-implementation-notes-register.md
```

Each relevant note must be promoted, resolved or explicitly marked as not relevant before implementation planning continues.

## 10. File Responsibility Rule

Use:

```text
planning/planning-doc-responsibility-map.md
```

to decide where content belongs.

If content is a global workflow/common rule, do not bury it in a local scenario, DATA, behavior item, slice or client sidecar file.

Local coverage tables, local questions, local decisions and local implementation notes are allowed to stay local.

## 11. Next Step Protocol

Every planning response, archive summary and implementation prompt should explicitly state the next step.

Use this structure when useful:

```text
Current state:
...

Relevant questions for this step:
...

Assumptions used:
...

Blocking questions:
...

Next action:
...

Files to read:
...

Files to create/update:
...

Do not do:
...

Success criteria:
...

Stop and ask if:
...
```

The agent must not move to the next slice or implementation step by itself.

The agent may propose the next step, but execution requires a user request.

## 12. Archive Rule

When asked to create a replacement package, follow:

```text
planning/replacement-file-generation-guide.md
```

Generate complete replacement files, not patches.

Before generating an archive:

```text
1. Check current repository docs when possible.
2. Ask relevant blocking questions for this archive.
3. Include assumptions with questions.
4. Keep archive scope focused.
5. Include MANIFEST.md and APPLY.md.
6. State what is deliberately out of scope.
```

## 13. Workflow Centralization Principle

General workflow rules should live in central workflow/common files.

Local files may keep local coverage tables, local questions, local decisions and local implementation notes.

A future workflow centralization audit should find and move global workflow rules out of local files.

## 14. Do Not

```text
- Do not create .client.md before concrete client work starts.
- Do not continue planning through scenario-level ambiguity.
- Do not ask irrelevant future questions during a focused step.
- Do not ask a question without giving the current assumption/preferred answer.
- Do not invent behavior item categories casually.
- Do not bury global workflow rules in local files.
- Do not auto-continue to the next slice without a user request.
```
