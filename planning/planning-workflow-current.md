# Current Planning Workflow

Status: current workflow

## 1. Current Point

The current active workflow step is:

```text
UI specs, client component discovery, accessibility/styling conventions,
and server/client change-extension points with extension pressure have been introduced.
```

Previously completed planning steps:

```text
scenario behavior items migrated into per-scenario files;
planning document responsibility map introduced;
client architecture mapping rules introduced.
```

## 2. Main Workflow

```text
scenario text specs
+ scenario DATA files
+ validation/security addenda
+ scenario UI specs when client-visible behavior is being planned
-> per-scenario behavior items
-> UI behavior items when client-visible behavior is being planned
-> scenario questions register
-> domain draft(s) / L1 domain foundation
-> L1 slice boundary draft
-> parent vertical slice files
-> .client.md sidecar when concrete client work starts
-> implement one slice/client layer at a time
-> update coverage / questions / ADR candidates / extension register
```

## 3. Central Workflow Files

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/scenario-specification-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
```

## 4. Scenario Question Loop

```text
question
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update scenario UI spec if UI-visible requirement changed
-> update validation/security addendum if needed
-> update behavior items / UI behavior items if required behavior changed
-> update scenario questions register
-> continue implementation planning
```

## 5. Relevant Questions Rule

Before a planning/archive/implementation step, ask only questions that can affect that step.

For each question, include the current assumption/preferred answer.

Future-only questions should be recorded in the appropriate register instead of interrupting the current work.

## 6. Slice / Client Intake Checklist

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant per-scenario behavior item file.
5. Read relevant scenario UI spec if client-visible behavior is involved.
6. Check planning/diagrams/scenario-questions-register.md.
7. Check planning/slices/slice-implementation-notes-register.md.
8. Check planning/slices/slice-extension-points-register.md.
9. Check relevant shared support docs under planning/slices/shared/.
10. If client work is involved, check planning/client/ and planning/slices/client-architecture-principles.md.
11. Promote relevant notes/questions/extension pressure decisions.
12. If scenario-level ambiguity exists, stop and resolve it first.
```

## 7. Client Planning

A `.client.md` sidecar must cover:

```text
UI Behavior Coverage
Client Architecture Mapping
Component / Layout Plan
Styling Change Points
Accessibility / ARIA Contract
Client Extension Points
Client Behavior Change Points
Client Extension Pressure / Anti-Coupling Decisions
```

## 8. Change / Extension Points Gate

Before implementation decisions, classify potential seams as:

```text
hard invariant
current behavior change point
future extension point
extension pressure
unnecessary abstraction
```

Use:

```text
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
```

## 9. File Responsibility Gate

Before adding or moving planning content, check `planning/planning-doc-responsibility-map.md`.

## 10. Current Slice Status

Resolved issue:

```text
SL-REQ-001 Submitted vs InReview conflict has been resolved in active implementation/tests.
```

Target request statuses:

```text
InReview
Approved
Rejected
```

## 11. Current Next Steps

```text
1. Apply this workflow package.
2. Use scenario UI specs/client-wide docs/change-extension docs for future `.client.md` files.
3. Review migrated behavior item files and extension points register for target scenario.
4. Resolve any blocking scenario/UI/extension questions.
5. Get/prepare concrete UI plan for the next client layer.
6. Create the relevant `.client.md` sidecar only when concrete client work starts.
7. Implement the client layer and tests.
```

Likely implementation target after planning cleanup:

```text
Request creation Client/UI for already implemented SL-REQ-001 server-side behavior.
```

## 12. Future Cleanup Steps

```text
1. Workflow centralization audit.
2. Planning docs responsibility cleanup.
3. Concrete scenario UI spec creation for the next target scenario.
```

## 13. Next Step Protocol

Every archive summary / implementation prompt should include current state, relevant questions, assumptions, blocking questions, next action, do-not-do, success criteria and stop-and-ask conditions.
