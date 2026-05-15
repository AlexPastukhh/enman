# Apply Instructions

Archive:

```text
enman-api-openapi-contract-artifacts-workflow-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-api-openapi-contract-artifacts-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/api/README.md
planning/api/client-server-contract-principles.md
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
planning/api/fluentvalidation-error-code-policy-note.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/slices/README.md
planning/slices/cross-cutting/README.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

## Delete

```text
nothing
```

## Notes

This package:
- restores/adds the missing `planning/api/` docs referenced by the current planning index;
- adds client/server contract principles;
- adds `CC-API-001` as an OpenAPI structural contract cross-cutting slice;
- updates `CC-CONST-001` to the current cross-cutting slice format with Concern Flow before Implementation Flow;
- keeps OpenAPI and generated constants as separate contract channels;
- documents temporary route constants during OpenAPI migration;
- documents explicit generated artifact commands/checks and no server-startup file writing;
- updates navigation and ADR notes.

It does not:
- change code;
- generate Shared/openapi.json;
- generate TypeScript OpenAPI types;
- create EnergyManagement.Tools;
- migrate client API wrappers;
- create full numbered ADRs.
