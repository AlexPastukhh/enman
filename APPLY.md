# APPLY — Client Draft + OpenAPI Workflow Rules Docs Sync v2

Run from repository root after downloading the archive:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\client-draft-openapi-rules-docs-sync-v2.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is a docs-only archive. It intentionally does not include backend/client/runtime code, tests or generated artifacts.

Do not run OpenAPI generation for this docs-only archive.

The OpenAPI generated-artifact workflow documented here is for future code/API-contract archives:

```powershell
npm run generate:openapi
npm run generate:api-types
git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts
npm run check:api
```
