# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Active Planning Focus

```text
CSRF/antiforgery as cross-cutting security slice,
with security-derived behavior items and unified cross-cutting/helper slice format.
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

20. planning/diagrams/README.md
21. planning/diagrams/scenario-text-specs/README.md
22. planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
23. planning/diagrams/scenario-behavior-items/README.md
24. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
25. planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md

26. planning/scenario-specification-principles.md
27. planning/scenario-domain-validation-principles.md
28. planning/client/README.md
29. planning/client/cross-cutting/README.md

30. planning/slices/README.md
31. planning/slices/cross-cutting/README.md
32. planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
33. planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
34. planning/slices/shared/README.md
35. planning/slices/shared/antiforgery-token-session-context.md
36. planning/slices/l1-slice-drafting-guide.md
37. planning/slices/implementation-principles.md
38. planning/slices/client-architecture-principles.md
39. planning/slices/client-component-discovery-guide.md
40. planning/slices/change-extension-points-principles.md
41. planning/slices/slice-extension-points-register.md
42. planning/replacement-file-generation-guide.md
```

## 3. CSRF / Antiforgery Direction

Security requirements:

```text
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
```

Behavior items:

```text
planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
```

Implementation-ready cross-cutting slice:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## 4. Cross-Cutting And Helper Slices

Use:

```text
planning/slices/cross-cutting/
```

for technical/support slices with observable behavior, concern-derived behavior items, concern flow, implementation flow and tests.

Use:

```text
planning/slices/shared/
```

for reusable notes/helpers that do not have full slice behavior/test flow.

## 5. Current Next Step

```text
1. Apply this archive.
2. Use CC-CSRF-001 as the implementation-ready planning file for antiforgery work.
3. Keep behavior items -> concern flow -> implementation flow traceability.
4. Do not implement CSRF from loose notes only.
```
