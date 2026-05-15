# Apply Instructions

Archive:

```text
enman-api-contract-openapi-constants-a11y-workflow-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-api-contract-openapi-constants-a11y-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/api/README.md
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
planning/api/fluentvalidation-error-code-policy-note.md
```

## Replace

```text
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
planning/client/cross-cutting/README.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/slices/client-component-discovery-guide.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/adr/architecture-decision-notes.md
```

## Notes

This package adds:
- API contract planning area;
- native ProblemDetails + ServerError API error contract;
- OpenAPI structural contract direction;
- generated shared client constants direction;
- deferred FluentValidation ErrorCode inspection note;
- API error mapper/factory target boundary;
- A11Y hardening as component/test contract.

It does not:
- change code;
- create full numbered ADRs;
- create `.client.md`;
- choose exact OpenAPI generation tool;
- implement constants generator;
- migrate FluentValidation ErrorMessage/ErrorCode usage;
- upgrade .NET.
