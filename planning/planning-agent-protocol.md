# Planning Agent Protocol

Status: current collaboration protocol  
Scope: AI/chat agents working with scenarios, behavior items, slices, client sidecars and replacement packages

## 1. Purpose

This file centralizes how planning agents should work in this repository.

It complements:

```text
planning/replacement-file-generation-guide.md
planning/planning-workflow-current.md
planning/scenario-specification-principles.md
planning/slices/l1-slice-drafting-guide.md
```

## 2. Core Rule

Do not continue implementation planning through a question that may change required behavior.

If ambiguity affects scenario behavior, DATA, validation/security, API contract, user-visible outcome or cross-layer responsibility, stop and ask.

## 3. Scenario Question Loop

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

## 4. Slice Intake Rule

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

## 5. Client Sidecar Rule

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

## 6. Behavior Items Rule

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

## 7. Implementation Notes Register Rule

Before starting any new slice/client sidecar, check:

```text
planning/slices/slice-implementation-notes-register.md
```

Each relevant note must be promoted, resolved or explicitly marked as not relevant before implementation planning continues.

## 8. Archive Rule

When asked to create a replacement package, follow:

```text
planning/replacement-file-generation-guide.md
```

Generate complete replacement files, not patches.

## 9. Workflow Centralization Principle

General workflow rules should live in central workflow/common files.

Local files may keep local coverage tables, local questions, local decisions and local implementation notes.

A future workflow centralization audit should find and move global workflow rules out of local files.
