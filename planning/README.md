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

Use migrated per-scenario behavior item files to support the next concrete slice/client planning step.

Do not create `.client.md` files in advance.

## 3. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/scenario-specification-principles.md
5. planning/scenario-domain-validation-principles.md

6. planning/diagrams/README.md
7. planning/diagrams/scenario-text-specs/README.md
8. planning/diagrams/scenario-data/README.md
9. planning/diagrams/scenario-questions-register.md
10. planning/diagrams/scenario-behavior-items/README.md
11. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md

12. planning/tables/README.md
13. planning/tables/pre-domain-variants-input.md
14. planning/tables/scenario-behavior-baseline-account-activation-addendum.md

15. planning/domain-draft-generation-guide.md
16. planning/tables/domain-drafts/README.md
17. planning/tables/domain-drafts/domain-draft-01.md
18. planning/l1-domain-implementation-cut.md
19. planning/l1-domain-testing-rules.md

20. planning/slices/README.md
21. planning/slices/l1-slice-drafting-guide.md
22. planning/slices/implementation-principles.md
23. planning/slices/slice-implementation-notes-register.md
24. planning/slices/shared/README.md
25. planning/slices/l1-slice-boundary-draft-01.md

26. planning/adr/README.md
27. planning/adr/adr-candidates.md
```

## 4. Behavior Items Position

Primary scenario-specific behavior item source:

```text
planning/diagrams/scenario-behavior-items/
```

Compiled baselines remain for history/cross-checking:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

## 5. Slice And Client Sidecar Position

Parent slice file owns vertical behavior and API/server responsibilities.

`.client.md` sidecar owns detailed client implementation when client work starts.

## 6. Scenario Question Loop

If a scenario-level question appears during planning:

```text
question
-> clarify
-> update scenario spec / DATA / validation-security addendum if needed
-> update behavior items if needed
-> continue implementation planning
```

## 7. Replacement File Generation Workflow

When the user asks for files to replace manually, use:

```text
planning/replacement-file-generation-guide.md
```

Generate complete files, package them with repository-relative paths, and include `APPLY.md` / `MANIFEST.md`.
