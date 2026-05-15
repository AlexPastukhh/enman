# Current Planning Workflow

Status: current workflow

## 1. Current Point

The current active workflow step is:

```text
ADR workflow and architecture decision notes have been introduced.
```

Previously completed planning steps:

```text
scenario behavior items migrated into per-scenario files;
planning document responsibility map introduced;
client architecture mapping rules introduced;
UI specs/client conventions/change-extension workflow introduced.
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
-> update coverage / questions / ADR candidates / decision notes / extension register
```

## 3. Central Workflow Files

Read central workflow/common files first:

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/scenario-specification-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/change-extension-points-principles.md
planning/adr/adr-workflow.md
```

## 4. Scenario Question Loop

When planning discovers a question that affects scenario behavior, DATA, UI-visible requirements, validation/security or visible outcome:

```text
question
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update scenario UI spec if UI-visible requirement changed
-> update validation/security addendum if needed
-> update behavior items / UI behavior items if required behavior changed
-> update scenario questions register
-> update ADR candidate/note if decision is architecturally relevant
-> continue implementation planning
```

## 5. Slice / Client Intake Checklist

Before starting any parent slice or `.client.md` sidecar:

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant per-scenario behavior item file.
5. Read relevant scenario UI spec if client-visible behavior is involved.
6. Check planning/diagrams/scenario-questions-register.md.
7. Check planning/slices/slice-implementation-notes-register.md.
8. Check planning/slices/slice-extension-points-register.md.
9. Check planning/adr/adr-candidates.md.
10. Check planning/adr/architecture-decision-notes.md.
11. Check relevant shared support docs under planning/slices/shared/.
12. If client work is involved, check planning/client/ and planning/slices/client-architecture-principles.md.
13. Promote relevant notes/questions/extension pressure/ADR decisions.
14. If scenario-level ambiguity exists, stop and resolve it first.
```

## 6. ADR Gate

When a planning or implementation step makes an architecture decision, classify ADR impact:

```text
no ADR relevance
add/update ADR candidate
add/update accepted architecture decision note
propose full ADR promotion
```

Use:

```text
planning/adr/adr-workflow.md
planning/adr/adr-candidates.md
planning/adr/architecture-decision-notes.md
```

Do not create full numbered ADRs unless explicitly requested.

## 7. Current Next Steps

Recommended next steps:

```text
1. Apply this ADR workflow package.
2. Use ADR candidates and decision notes during future slice/client planning.
3. Review relevant ADR notes before target scenario/client work.
4. Get/prepare concrete UI plan for the next client layer.
5. Create the relevant `.client.md` sidecar only when concrete client work starts.
6. Implement the client layer and tests.
```

Likely implementation target after planning cleanup:

```text
Request creation Client/UI for already implemented SL-REQ-001 server-side behavior.
```

## 8. Next Step Protocol

Every archive summary / implementation prompt should include:

```text
Current state
Relevant questions for this step
Assumptions used
Blocking questions
ADR impact
Next action
Do not do
Success criteria
Stop and ask if
```
