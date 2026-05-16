# APPLY — SL-APPL-002 Account Applicant Parties Read / Templates v3

This archive is merge-ready: it contains repo-relative paths directly.
There is no wrapper folder inside the zip.

## Remove old temporary folder if present

If you previously extracted v2 into `_incoming_sl_appl_002`, remove it from repository root:

```powershell
Remove-Item -Path ".\_incoming_sl_appl_002" -Recurse -Force
```

## Apply from repository root

Run PowerShell from the repository root, for example `C:\enman\enman`:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-002-account-applicant-parties-read-v3.zip" -DestinationPath "." -Force
```

This extracts files directly into their target paths, for example:

```text
EnergyManagement.Server\L1\Api\L1Dtos.cs
EnergyManagement.Server\L1\Controllers\L1Controller.cs
Shared\openapi.json
energymanagement.client\src\shared\api\generated\openapi-types.ts
Tests.EnergyManagement\Integration\L1\L1SliceIntegrationTests.cs
```

## Recommended checks after applying

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

npm run check:api
```

Optional explicit generation/check workflow:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out .\Shared\openapi.json
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out .\Shared\openapi.json --check
dotnet run --project .\EnergyManagement.Tools -- generate-client-constants --out .\Shared --check
npm run check:api
```

Expected after v3: the generated OpenAPI/type diffs from v2 should already be present in the applied files. If `npm run check:api` still reports diffs, keep the generated diffs and report the exact output.

## Scope confirmations

- No GitHub write was performed.
- No branch/commit/PR was created.
- No planning docs are included.
- No domain files are included.
- No client UI files are included.
- Generated OpenAPI/type artifacts are included because the API contract changed.
