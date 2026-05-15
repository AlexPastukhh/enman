# Current Planning Workflow

Status: current workflow

## 1. Current Point

The current active workflow step is:

```text
prepare slice/client-sidecar workflow and then complete missing Client/UI for already implemented server-side L1 logic
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

## 4. Slice Intake Checklist

Before starting any parent slice or `.client.md` sidecar:

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant per-scenario behavior items if they exist.
5. Check planning/diagrams/scenario-questions-register.md.
6. Check planning/slices/slice-implementation-notes-register.md.
7. Check relevant shared support docs under planning/slices/shared/.
8. Promote relevant notes/questions.
9. If scenario-level ambiguity exists, stop and resolve it first.
```

## 5. Parent Slice vs Client Sidecar

Parent slice owns vertical behavior, Scenario Slice Flow, behavior item coverage summary, API contract, application/domain/persistence responsibilities and server/integration tests.

`.client.md` sidecar is created only when concrete client work starts and owns detailed client implementation.

## 6. Behavior Items Position

The compiled baseline remains:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

New workflow introduces per-scenario behavior item files:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior items migration / cleanup is a separate future step.

## 7. Current Slice Status

```text
SL-REQ-001 Submitted vs InReview conflict has been resolved in active implementation/tests.
```

Target statuses:

```text
InReview
Approved
Rejected
```

## 8. Current Next Steps

```text
1. Apply workflow/navigation package.
2. Get or prepare concrete UI plan for the next client layer.
3. Run the slice intake checklist.
4. Create the relevant .client.md sidecar.
5. Implement the client layer and tests.
```

Likely next target:

```text
Request creation Client/UI for already implemented SL-REQ-001 server-side behavior.
```

## 9. Future Cleanup Steps

Track separately:

```text
1. Behavior items migration / cleanup.
2. Workflow centralization audit.
```

## 10. Replacement File Generation Workflow

When the user asks for files to replace manually, use:

```text
planning/replacement-file-generation-guide.md
```
