# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Active Planning Focus

```text
Client/server contract artifacts:
OpenAPI structural contract + generated semantic constants,
before continuing missing client slices.
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
11. planning/testing/e2e-testing-workflow.md
12. planning/testing/test-object-patterns.md
13. planning/testing/playwright-e2e-cleanup-plan.md

14. planning/api/README.md
15. planning/api/client-server-contract-principles.md
16. planning/api/api-error-contract.md
17. planning/api/api-error-mapping-boundary.md
18. planning/api/openapi-contract-generation.md
19. planning/api/client-constants-generation.md
20. planning/api/fluentvalidation-error-code-policy-note.md

21. planning/diagrams/README.md
22. planning/diagrams/scenario-text-specs/README.md
23. planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
24. planning/diagrams/scenario-behavior-items/README.md
25. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
26. planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md

27. planning/scenario-specification-principles.md
28. planning/scenario-domain-validation-principles.md
29. planning/client/README.md
30. planning/client/cross-cutting/README.md

31. planning/slices/README.md
32. planning/slices/cross-cutting/README.md
33. planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
34. planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
35. planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
36. planning/slices/shared/README.md
37. planning/slices/shared/antiforgery-token-session-context.md
38. planning/slices/l1-slice-drafting-guide.md
39. planning/slices/implementation-principles.md
40. planning/slices/client-architecture-principles.md
41. planning/slices/client-component-discovery-guide.md
42. planning/slices/change-extension-points-principles.md
43. planning/slices/slice-extension-points-register.md
44. planning/replacement-file-generation-guide.md
```

## 3. Client / Server Contract Direction

Primary principles:

```text
planning/api/client-server-contract-principles.md
```

Structural contract:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

Semantic constants:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Core split:

```text
OpenAPI = endpoints / methods / DTOs / response schemas / statuses.
Generated constants JSON = error codes / ProblemDetails extension names / ServerError fields.
```

## 4. CSRF / Antiforgery Direction

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

## 5. Current Next Step

```text
1. Apply this archive.
2. Implement/plan CC-API-001 and CC-CONST-001 before missing client slices.
3. Generate/check OpenAPI and semantic constants artifacts.
4. Then update client API layer and proceed with client slices.
```
