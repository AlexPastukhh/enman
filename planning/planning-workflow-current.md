# Current Planning Workflow

Status: current workflow

## 1. Current Point

The current active workflow step is:

```text
client architecture mapping rules for .client.md sidecars have been introduced.
```

Previously completed planning steps:

```text
scenario behavior items migrated into per-scenario files;
planning document responsibility map introduced.
```

## 2. Main Workflow

```text
scenario text specs
+ scenario DATA files
+ validation/security addenda
-> per-scenario behavior items
-> scenario questions register
-> domain draft(s) / L1 domain foundation
-> L1 slice boundary draft
-> parent vertical slice files
-> .client.md sidecar when concrete client work starts
-> implement one slice/client layer at a time
-> update coverage / questions / ADR candidates
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
planning/slices/client-architecture-principles.md
```

## 4. Scenario Question Loop

When planning discovers a question that affects scenario behavior, DATA, validation/security or visible outcome:

```text
question
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update validation/security addendum if needed
-> update behavior items if required behavior changed
-> update scenario questions register
-> continue implementation planning
```

## 5. Relevant Questions Rule

Before a planning/archive/implementation step, ask only questions that can affect that step.

For each question, include the current assumption/preferred answer.

If a possible question is future-only and not relevant to the current step, record it in the appropriate register instead of interrupting the current work.

## 6. Slice / Client Intake Checklist

Before starting any parent slice or `.client.md` sidecar:

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant per-scenario behavior item file.
5. Check planning/diagrams/scenario-questions-register.md.
6. Check planning/slices/slice-implementation-notes-register.md.
7. Check relevant shared support docs under planning/slices/shared/.
8. If client work is involved, check planning/slices/client-architecture-principles.md.
9. Promote relevant notes/questions.
10. If scenario-level ambiguity exists, stop and resolve it first.
```

## 7. Client Architecture Mapping

Use:

```text
planning/slices/client-architecture-principles.md
```

Core rule:

```text
Planning slice and frontend feature are not 1:1.
```

Default mapping:

```text
Read slice    -> pages + entities (+ widgets if reused)
Command slice -> pages + features + entities
Shared concern -> app / shared
```

A `.client.md` sidecar must explain which client architecture part covers which behavior item.

## 8. File Responsibility Gate

Before adding or moving planning content, check:

```text
planning/planning-doc-responsibility-map.md
```

If the content is global workflow/common content, place it in a central workflow/common file.

If the content is local coverage/question/decision/test detail, keep it in the local scenario/slice/client file.

## 9. Behavior Items Position

Primary scenario-specific behavior item source:

```text
planning/diagrams/scenario-behavior-items/
```

Compiled baselines remain for history/cross-checking:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

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

Recommended next steps:

```text
1. Apply client-architecture mapping archive.
2. Use client architecture principles for future .client.md files.
3. Review migrated behavior item files for target scenario.
4. Resolve any blocking scenario questions.
5. Get/prepare concrete UI plan for the next client layer.
6. Create the relevant .client.md sidecar only when concrete client work starts.
7. Implement the client layer and tests.
```

Likely implementation target after planning cleanup:

```text
Request creation Client/UI for already implemented SL-REQ-001 server-side behavior.
```

## 12. Future Cleanup Steps

Track separately:

```text
1. Workflow centralization audit:
   scan planning docs and move global workflow rules from local files into central files.

2. Planning docs responsibility cleanup:
   apply responsibility map where files currently mix local and global concerns.
```

Local coverage tables and local questions are allowed to remain local.

## 13. Next Step Protocol

Every archive summary / implementation prompt should include:

```text
Current state
Relevant questions for this step
Assumptions used
Blocking questions
Next action
Do not do
Success criteria
Stop and ask if
```
