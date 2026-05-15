# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Active Planning Focus

```text
Cross-cutting/helper slice workflow,
CC-CONST-001 as the implementation-ready constants generation/testing slice,
and API/client contract support.
```

Do not create `.client.md` files in advance.

Do not create full numbered ADRs unless explicitly requested.

## 2. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md

5. planning/adr/README.md
6. planning/adr/adr-workflow.md
7. planning/adr/architecture-decision-notes.md
8. planning/adr/adr-candidates.md

9. planning/api/README.md
10. planning/api/api-error-contract.md
11. planning/api/api-error-mapping-boundary.md
12. planning/api/openapi-contract-generation.md
13. planning/api/client-constants-generation.md
14. planning/api/fluentvalidation-error-code-policy-note.md

15. planning/scenario-specification-principles.md
16. planning/scenario-domain-validation-principles.md
17. planning/diagrams/README.md
18. planning/client/README.md
19. planning/client/cross-cutting/README.md

20. planning/slices/README.md
21. planning/slices/cross-cutting/README.md
22. planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
23. planning/slices/l1-slice-drafting-guide.md
24. planning/slices/implementation-principles.md
25. planning/slices/client-architecture-principles.md
26. planning/slices/client-component-discovery-guide.md
27. planning/slices/change-extension-points-principles.md
28. planning/slices/slice-extension-points-register.md
29. planning/replacement-file-generation-guide.md
```

## 3. Cross-Cutting And Helper Slices

Use:

```text
planning/slices/cross-cutting/
```

for technical/support slices with observable behavior, implementation flow and tests.

Use:

```text
planning/slices/shared/
```

for reusable notes/helpers that do not have full slice behavior/test flow.

## 4. Constants Direction

Primary source for constants writer/checker/testing:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

API relationship note:

```text
planning/api/client-constants-generation.md
```

## 5. Current Next Step

```text
1. Apply this archive.
2. Use CC-CONST-001 as implementation plan for EnergyManagement.Tools constants generator/checker/tests.
3. Use generated JSON for ordinary API integration error-code expectations.
4. Use literal integration tests only for critical behavioral codes.
5. Keep class/method details inside flow only when they clarify behavior.
```
