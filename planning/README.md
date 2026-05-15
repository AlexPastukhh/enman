# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Main Workflow

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
-> update coverage / questions / decisions / ADR candidates
```

## 2. Current Active Planning Focus

```text
prepare slice/client-sidecar workflow, then complete missing Client/UI around already implemented server-side L1 logic
```

Do not create `.client.md` files in advance.

Create a `.client.md` sidecar only when work starts on the concrete client layer of a concrete slice.

## 3. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/scenario-specification-principles.md
5. planning/scenario-domain-validation-principles.md
6. planning/replacement-file-generation-guide.md

7. planning/diagrams/README.md
8. planning/diagrams/scenario-text-specs/README.md
9. planning/diagrams/scenario-data/README.md
10. planning/diagrams/scenario-questions-register.md
11. planning/diagrams/scenario-behavior-items/README.md
12. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md

13. planning/tables/README.md
14. planning/tables/pre-domain-variants-input.md
15. planning/tables/scenario-behavior-baseline-account-activation-addendum.md

16. planning/domain-draft-generation-guide.md
17. planning/tables/domain-drafts/README.md
18. planning/tables/domain-drafts/domain-draft-01.md
19. planning/l1-domain-implementation-cut.md
20. planning/l1-domain-testing-rules.md

21. planning/slices/README.md
22. planning/slices/l1-slice-drafting-guide.md
23. planning/slices/implementation-principles.md
24. planning/slices/slice-implementation-notes-register.md
25. planning/slices/shared/README.md
26. planning/slices/l1-slice-boundary-draft-01.md

27. planning/adr/README.md
28. planning/adr/adr-candidates.md
29. planning/current-state.md
30. planning/domain-model.md
```

## 4. Scenario Question Loop

If a scenario-level question appears during planning:

```text
question
-> clarify
-> update scenario spec / DATA / validation-security addendum if needed
-> update behavior items if needed
-> continue implementation planning
```

## 5. Current Slice Entry Points

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/slice-implementation-notes-register.md
planning/slices/l1-slice-boundary-draft-01.md
```

## 6. Behavior Items Position

The compiled downstream baseline remains:

```text
planning/tables/pre-domain-variants-input.md
```

Future per-scenario behavior item files live under:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior item migration/cleanup is a separate future step.

## 7. Future Cleanup Steps

Do not mix these into unrelated implementation packages:

```text
1. Behavior items migration / cleanup.
2. Workflow centralization audit.
```

Workflow centralization audit means finding files that contain global workflow rules outside central workflow/common docs and moving those rules to central files.

Local coverage tables and local questions are allowed to remain local.

## 8. Replacement File Generation Workflow

When the user asks for files to replace manually, use:

```text
planning/replacement-file-generation-guide.md
```

## 9. Superseded / Not Current

Do not use the old path as current workflow:

```text
scenario-domain-design-input-gate.md
-> scenario-domain-design-input-core.md
-> domain-discovery-core.md
-> aggregate-boundary-candidates-core.md
-> domain-model-options-core.md
```
