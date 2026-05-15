# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## Current Active Planning Focus

```text
API contract / OpenAPI / client constants generation workflow
and accessibility as client component/test contract.
```

Do not create `.client.md` files in advance.

Do not create full numbered ADRs unless explicitly requested.

## Current Read Order

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
21. planning/slices/l1-slice-drafting-guide.md
22. planning/slices/implementation-principles.md
23. planning/slices/client-architecture-principles.md
24. planning/slices/client-component-discovery-guide.md
25. planning/slices/change-extension-points-principles.md
26. planning/slices/slice-extension-points-register.md
27. planning/replacement-file-generation-guide.md
```

## API Contract Docs

Use:

```text
planning/api/
```

for native ProblemDetails error contract, ServerError / ServerValidationError, OpenAPI structural contract, generated shared constants JSON, client-facing vs internal error distinction and FluentValidation error-code migration note.

## Current Next Step

```text
1. use planning/api docs during API/client slice work;
2. use hardened CL-A11Y-001 during client component/test planning;
3. inspect FluentValidation helpers before changing ErrorMessage/ErrorCode mapping;
4. plan API error mapper / ProblemDetails factory as a separate API boundary step;
5. keep .NET upgrade deferred as infra task.
```
