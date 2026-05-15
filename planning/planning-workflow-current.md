# Current Planning Workflow

Status: current workflow

## 1. Current Point

The current active workflow step is:

```text
scenario behavior items have been migrated into per-scenario files;
next implementation planning should use those files for slice/client coverage.
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

## 3. Scenario Question Loop

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

## 4. Slice / Client Intake Checklist

Before starting any parent slice or `.client.md` sidecar:

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant per-scenario behavior item file.
5. Check planning/diagrams/scenario-questions-register.md.
6. Check planning/slices/slice-implementation-notes-register.md.
7. Check relevant shared support docs under planning/slices/shared/.
8. Promote relevant notes/questions.
9. If scenario-level ambiguity exists, stop and resolve it first.
```

## 5. Behavior Items Position

Primary scenario-specific behavior item source:

```text
planning/diagrams/scenario-behavior-items/
```

Compiled baselines remain for history/cross-checking:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

## 6. Current Slice Status

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

## 7. Current Next Steps

Recommended next steps:

```text
1. Review migrated behavior item files for target scenario.
2. Resolve any blocking scenario questions.
3. Get/prepare concrete UI plan for the next client layer.
4. Create the relevant .client.md sidecar only when concrete client work starts.
5. Implement the client layer and tests.
```

Likely target:

```text
Request creation Client/UI for already implemented SL-REQ-001 server-side behavior.
```

## 8. Future Cleanup Steps

Track separately:

```text
1. Workflow centralization audit.
2. Planning document responsibility map.
```

Local coverage tables and local questions are allowed to remain local.
