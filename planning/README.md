# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Active Planning Focus

```text
Testing workflow and E2E Playwright workflow,
while keeping CC-CONST-001 as the implementation-ready constants generation/testing cross-cutting slice.
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

9. planning/testing/README.md
10. planning/testing/testing-principles.md
11. planning/testing/e2e-playwright-workflow.md
12. planning/testing/test-object-patterns.md
13. planning/testing/playwright-e2e-cleanup-plan.md

14. planning/api/README.md
15. planning/api/api-error-contract.md
16. planning/api/api-error-mapping-boundary.md
17. planning/api/openapi-contract-generation.md
18. planning/api/client-constants-generation.md
19. planning/api/fluentvalidation-error-code-policy-note.md

20. planning/scenario-specification-principles.md
21. planning/scenario-domain-validation-principles.md
22. planning/diagrams/README.md
23. planning/client/README.md
24. planning/client/cross-cutting/README.md

25. planning/slices/README.md
26. planning/slices/cross-cutting/README.md
27. planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
28. planning/slices/l1-slice-drafting-guide.md
29. planning/slices/implementation-principles.md
30. planning/slices/client-architecture-principles.md
31. planning/slices/client-component-discovery-guide.md
32. planning/slices/change-extension-points-principles.md
33. planning/slices/slice-extension-points-register.md
34. planning/replacement-file-generation-guide.md
```

## 3. Testing Direction

Use:

```text
planning/testing/
```

for cross-slice testing principles, E2E Playwright workflow, Page Object / Component Object rules and Playwright cleanup plan.

Key current decision:

```text
E2E tests prove cross-layer browser-client-server wiring.
Detailed client-visible UI behavior belongs to client/component tests.
```

## 4. Cross-Cutting And Helper Slices

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

## 5. Constants Direction

Primary source for constants writer/checker/testing:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

API relationship note:

```text
planning/api/client-constants-generation.md
```

## 6. Current Next Step

```text
1. Apply this testing/E2E workflow archive.
2. Use planning/testing/e2e-playwright-workflow.md before changing Playwright code.
3. Then implement Playwright cleanup from planning/testing/playwright-e2e-cleanup-plan.md.
4. Keep E2E focused on cross-layer communication, not exhaustive UI behavior.
```
