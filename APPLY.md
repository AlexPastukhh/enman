# APPLY — SL-APPL-002 Account Applicant Parties Read / Templates

This archive is merge-ready: it contains repo-relative paths directly and has no wrapper directory.

## Apply from repo root

PowerShell:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-002-account-applicant-parties-read-v4.zip" -DestinationPath "." -Force
```

If a previous temporary incoming folder exists, remove it:

```powershell
Remove-Item -Path ".\_incoming_sl_appl_002" -Recurse -Force
```

## Verify

Run from repo root:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out .\Shared\openapi.json --check
npm --prefix .\energymanagement.client run generate:api-types
```

## About `npm run check:api`

The project script runs:

```text
npm run check:openapi && npm run generate:api-types && git diff --exit-code Shared/openapi.json energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Since this slice intentionally changes generated artifacts, `git diff --exit-code` can fail on unstaged generated files even when OpenAPI is up to date.

To use that script as a no-extra-drift check, stage the generated files first:

```powershell
git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts
npm run check:api
```

If it still prints a diff after staging, regenerate the artifacts and inspect the new diff.

## No GitHub write

No branch, commit, push or PR is performed by this archive.
