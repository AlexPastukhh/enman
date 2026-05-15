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

Current focus:

```text
client architecture mapping rules for .client.md sidecars
```

Already completed recent planning steps:

```text
scenario behavior items migrated into per-scenario files;
planning document responsibility map introduced.
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
24. planning/slices/client-architecture-principles.md
25. planning/slices/slice-implementation-notes-register.md
26. planning/slices/shared/README.md
27. planning/slices/l1-slice-boundary-draft-01.md

28. planning/replacement-file-generation-guide.md
29. planning/adr/README.md
30. planning/adr/adr-candidates.md
```

## 4. Responsibility Map

Use:

```text
planning/planning-doc-responsibility-map.md
```

to decide where planning content belongs.

Core rule:

```text
global workflow/common rules belong in central workflow/common files;
local coverage/questions/decisions/details stay in local files.
```

## 5. Agent Protocol

Use:

```text
planning/planning-agent-protocol.md
```

for:

```text
- stop-and-ask rules;
- relevant questions rule;
- assumptions-with-questions rule;
- next-step protocol;
- no-auto-continue rule;
- archive behavior.
```

## 6. Behavior Items Position

Primary scenario-specific behavior item source:

```text
planning/diagrams/scenario-behavior-items/
```

Compiled baselines remain for history/cross-checking:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

## 7. Slice And Client Sidecar Position

Parent slice file owns vertical behavior and API/server responsibilities.

`.client.md` sidecar owns detailed client implementation when client work starts.

Client architecture mapping rules live in:

```text
planning/slices/client-architecture-principles.md
```

Core rule:

```text
Planning slice and frontend feature are not 1:1.
```

## 8. Scenario Question Loop

If a scenario-level question appears during planning:

```text
question
-> clarify
-> update scenario spec / DATA / validation-security addendum if needed
-> update behavior items if needed
-> continue implementation planning
```

## 9. Replacement File Generation Workflow

When the user asks for files to replace manually, use:

```text
planning/replacement-file-generation-guide.md
```

Generate complete files, package them with repository-relative paths, and include `APPLY.md` / `MANIFEST.md`.

## 10. Current Next Step

After applying the client-architecture mapping package:

```text
1. use client architecture principles for future .client.md files;
2. review migrated behavior items for the next target scenario;
3. get/prepare UI plan;
4. create concrete .client.md only when client work starts.
```
