# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Main Workflow

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
-> update coverage / questions / decisions / ADR candidates / extension register
```

## 2. Current Active Planning Focus

Current focus:

```text
accepted architecture decision notes are now the guiding decision registry;
ADR candidates are the promotion backlog for possible future full ADRs.
```

Do not create `.client.md` files in advance.

Do not create full numbered ADRs unless explicitly requested.

## 3. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md

5. planning/adr/README.md
6. planning/adr/adr-workflow.md
7. planning/adr/architecture-decision-notes.md
8. planning/adr/adr-candidates.md

9. planning/scenario-specification-principles.md
10. planning/scenario-domain-validation-principles.md

11. planning/diagrams/README.md
12. planning/diagrams/scenario-text-specs/README.md
13. planning/diagrams/scenario-data/README.md
14. planning/diagrams/scenario-ui-specs/README.md
15. planning/diagrams/scenario-ui-specs/00-scenario-ui-specs-index.md
16. planning/diagrams/scenario-questions-register.md
17. planning/diagrams/scenario-behavior-items/README.md
18. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md

19. planning/client/README.md
20. planning/client/cross-cutting/README.md

21. planning/tables/README.md
22. planning/tables/pre-domain-variants-input.md
23. planning/tables/scenario-behavior-baseline-account-activation-addendum.md

24. planning/domain-draft-generation-guide.md
25. planning/tables/domain-drafts/README.md
26. planning/tables/domain-drafts/domain-draft-01.md
27. planning/l1-domain-implementation-cut.md
28. planning/l1-domain-testing-rules.md

29. planning/slices/README.md
30. planning/slices/l1-slice-drafting-guide.md
31. planning/slices/implementation-principles.md
32. planning/slices/client-architecture-principles.md
33. planning/slices/client-component-discovery-guide.md
34. planning/slices/change-extension-points-principles.md
35. planning/slices/slice-extension-points-register.md
36. planning/slices/slice-implementation-notes-register.md
37. planning/slices/shared/README.md
38. planning/slices/l1-slice-boundary-draft-01.md

39. planning/replacement-file-generation-guide.md
```

## 4. ADR / Architecture Decision Documentation

ADR entry point:

```text
planning/adr/README.md
```

Use:

```text
planning/adr/architecture-decision-notes.md
```

as the current guiding accepted decision registry.

Use:

```text
planning/adr/adr-candidates.md
```

as the backlog of possible future full ADRs.

If they conflict, stop and clarify.

## 5. Current Next Step

After applying ADR decision notes audit:

```text
1. use architecture-decision-notes during future planning and implementation;
2. update decision notes when accepted decisions are made;
3. update ADR candidates when a decision may need full ADR later;
4. include ADR impact in future archive summaries / implementation prompts;
5. continue toward target scenario UI plan and concrete `.client.md` only when client work starts.
```
