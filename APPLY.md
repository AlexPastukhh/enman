# Apply Instructions

This is a docs-only replacement package.

From the repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\review-start-client-files-sync-with-api-generation-workflow.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is expected:

```powershell
git add planning
git status
```

No runtime source files, tests or generated artifacts are included in this docs package.

If you are packaging a real implementation that changes backend/API shape, regenerate and include the generated artifacts in that implementation archive/commit:

```powershell
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
```

Then include:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```
