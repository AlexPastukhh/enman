# Manifest — Review Start Client Files + API Generation Workflow Sync

Status: docs-only replacement package  
Purpose: keep the previous `L2-REVIEW-START-001.client` sync and add the corrected API/generated-artifact workflow guidance from the latest run-log discussion.

## Files

```text
APPLY.md
MANIFEST.md
planning/api/README.md
planning/api/generated-artifact-check-workflow.md
planning/slices/README.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/l2/README.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
planning/slices/l2/SL-EMP-REQ-001-implementation-verification-and-packaging-note.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-scenario-flow-behavior-register.md
```

## Important update

For backend/API shape changes, the explicit generation workflow is:

```powershell
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

`npm run check:api` can still fail after correct generation if the generated files are only modified in the working tree and are not part of the expected state. Its final `git diff --exit-code` intentionally detects generated artifact drift.

Implementation archives/patches that change API shape must include:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Documentation-only archives must not include generated artifacts.

## Not included

```text
- runtime code;
- tests;
- Shared/openapi.json;
- generated TypeScript artifacts.
```
