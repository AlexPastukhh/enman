# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Active Planning Focus

```text
Client/server contract artifacts and documentation/status reconciliation:
OpenAPI structural contract + generated semantic constants are first-stage baseline;
next work should avoid stale docs and proceed through draft-driven slice/client planning.
```

Do not create `.client.md` files in advance.

Do not create full numbered ADRs unless explicitly requested.

Do not write directly to GitHub from documentation-only work unless explicitly requested.

## 2. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md

5. planning/documentation/README.md
6. planning/documentation/documentation-update-workflow.md
7. planning/documentation/status-reconciliation-workflow.md
8. planning/documentation/documentation-update-agent-prompt.md

9. planning/adr/README.md
10. planning/adr/adr-workflow.md
11. planning/adr/architecture-decision-notes.md
12. planning/adr/adr-candidates.md

13. planning/testing/README.md
14. planning/testing/testing-principles.md
15. planning/testing/e2e-testing-workflow.md
16. planning/testing/test-object-patterns.md
17. planning/testing/playwright-e2e-cleanup-plan.md

18. planning/api/README.md
19. planning/api/client-server-contract-principles.md
20. planning/api/api-error-contract.md
21. planning/api/api-error-mapping-boundary.md
22. planning/api/openapi-contract-generation.md
23. planning/api/client-constants-generation.md
24. planning/api/fluentvalidation-error-code-policy-note.md

25. planning/diagrams/README.md
26. planning/diagrams/scenario-text-specs/README.md
27. planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
28. planning/diagrams/scenario-behavior-items/README.md
29. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
30. planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
31. planning/diagrams/scenario-clarifications/README.md

32. planning/scenario-specification-principles.md
33. planning/scenario-domain-validation-principles.md
34. planning/client/README.md
35. planning/client/cross-cutting/README.md

36. planning/slices/README.md
37. planning/slices/draft-driven-discovery-principles.md
38. planning/slices/cross-cutting/README.md
39. planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
40. planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
41. planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
42. planning/slices/shared/README.md
43. planning/slices/shared/antiforgery-token-session-context.md
44. planning/slices/l1-slice-drafting-guide.md
45. planning/slices/implementation-principles.md
46. planning/slices/client-architecture-principles.md
47. planning/slices/client-component-discovery-guide.md
48. planning/slices/change-extension-points-principles.md
49. planning/slices/slice-extension-points-register.md
50. planning/replacement-file-generation-guide.md
```

## 3. Documentation Update Direction

Documentation-only updates use:

```text
planning/documentation/
planning/replacement-file-generation-guide.md
```

Rules:

```text
- check current repo state;
- update navigation/responsibility maps;
- create archive packages for manual application;
- do not write to GitHub directly unless explicitly requested.
```

## 4. Draft-Driven Discovery Direction

All slice families use draft-driven discovery:

```text
planning/slices/draft-driven-discovery-principles.md
```

This applies to:

```text
domain drafts
business slice drafts
client sidecar drafts
cross-cutting/helper slice drafts
testing/support drafts
documentation/status reconciliation drafts
```

## 5. Client / Server Contract Direction

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

## 6. CSRF / Antiforgery Direction

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

## 7. Current Next Step

```text
1. Keep docs reconciled with current implementation status.
2. Do not redo already implemented OpenAPI/constants/E2E infrastructure.
3. Use draft-driven discovery for L1 consolidation and client sidecar work.
4. Client work should proceed from confirmed backend-backed slices and generated contract artifacts.
```
