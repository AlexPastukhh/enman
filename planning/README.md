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
UI specs, client component discovery, styling/accessibility conventions,
and server/client change-extension points with extension pressure.
```

Already completed recent planning steps:

```text
scenario behavior items migrated into per-scenario files;
planning document responsibility map introduced;
client architecture mapping rules introduced.
```

Do not create `.client.md` files in advance.

## 3. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md
5. planning/scenario-specification-principles.md
6. planning/scenario-domain-validation-principles.md

7. planning/diagrams/README.md
8. planning/diagrams/scenario-text-specs/README.md
9. planning/diagrams/scenario-data/README.md
10. planning/diagrams/scenario-ui-specs/README.md
11. planning/diagrams/scenario-ui-specs/00-scenario-ui-specs-index.md
12. planning/diagrams/scenario-questions-register.md
13. planning/diagrams/scenario-behavior-items/README.md
14. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md

15. planning/client/README.md
16. planning/client/cross-cutting/README.md

17. planning/tables/README.md
18. planning/tables/pre-domain-variants-input.md
19. planning/tables/scenario-behavior-baseline-account-activation-addendum.md

20. planning/domain-draft-generation-guide.md
21. planning/tables/domain-drafts/README.md
22. planning/tables/domain-drafts/domain-draft-01.md
23. planning/l1-domain-implementation-cut.md
24. planning/l1-domain-testing-rules.md

25. planning/slices/README.md
26. planning/slices/l1-slice-drafting-guide.md
27. planning/slices/implementation-principles.md
28. planning/slices/client-architecture-principles.md
29. planning/slices/client-component-discovery-guide.md
30. planning/slices/change-extension-points-principles.md
31. planning/slices/slice-extension-points-register.md
32. planning/slices/slice-implementation-notes-register.md
33. planning/slices/shared/README.md
34. planning/slices/l1-slice-boundary-draft-01.md

35. planning/replacement-file-generation-guide.md
36. planning/adr/README.md
37. planning/adr/adr-candidates.md
```

## 4. Responsibility Map

Use `planning/planning-doc-responsibility-map.md` to decide where planning content belongs.

## 5. Agent Protocol

Use `planning/planning-agent-protocol.md` for stop-and-ask rules, relevant questions, assumptions-with-questions, next-step protocol, no-auto-continue, archive behavior, and change/extension/pressure classification.

## 6. Behavior Items And UI Specs

Primary scenario-specific behavior item source:

```text
planning/diagrams/scenario-behavior-items/
```

Scenario UI specs live under:

```text
planning/diagrams/scenario-ui-specs/
```

Create concrete UI specs when UI planning starts for a scenario.

## 7. Client Planning

Client-wide conventions:

```text
planning/client/
planning/client/cross-cutting/
```

Client architecture mapping:

```text
planning/slices/client-architecture-principles.md
```

Component discovery:

```text
planning/slices/client-component-discovery-guide.md
```

## 8. Change / Extension Points

Use:

```text
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
```

Known future extension points must be reviewed during current slice planning.

This does not mean every future extension requires abstraction now.

## 9. Scenario Question Loop

```text
question
-> clarify
-> update scenario spec / DATA / UI spec / validation-security addendum if needed
-> update behavior items if needed
-> update scenario questions register
-> continue implementation planning
```

## 10. Replacement File Generation Workflow

When the user asks for files to replace manually, use `planning/replacement-file-generation-guide.md`.

## 11. Current Next Step

After applying this workflow package:

```text
1. use UI specs and client-wide docs for future `.client.md`;
2. check extension points register before parent slice / client sidecar work;
3. review migrated behavior items for the next target scenario;
4. get/prepare UI plan;
5. create concrete `.client.md` only when client work starts.
```
