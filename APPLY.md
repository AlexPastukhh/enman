# APPLY — SL-APPL-002 Account Applicant Parties Read / Templates

This archive is merge-ready: it contains repo-relative paths directly.
There is no wrapper folder inside the zip.

## Recommended apply command from repository root

Run PowerShell from the repository root, for example `C:\enman\enman`:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-002-account-applicant-parties-read-v2.zip" -DestinationPath "." -Force
```

This extracts files directly into their target paths, for example:

```text
EnergyManagement.Server\L1\Api\L1Dtos.cs
EnergyManagement.Server\L1\Controllers\L1Controller.cs
Tests.EnergyManagement\Integration\L1\L1SliceIntegrationTests.cs
```

## If you already extracted into `_incoming_sl_appl_002`

That folder is only a temporary staging folder. From the repository root, either merge it and remove it:

```powershell
Copy-Item -Path ".\_incoming_sl_appl_002\*" -Destination "." -Recurse -Force
Remove-Item -Path ".\_incoming_sl_appl_002" -Recurse -Force
```

Or delete it and use the direct extraction command above:

```powershell
Remove-Item -Path ".\_incoming_sl_appl_002" -Recurse -Force
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-002-account-applicant-parties-read-v2.zip" -DestinationPath "." -Force
```

## Recommended checks after applying

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out .\Shared\openapi.json
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out .\Shared\openapi.json --check
dotnet run --project .\EnergyManagement.Tools -- generate-client-constants --out .\Shared --check
npm run check:api
```

If generation changes OpenAPI/types/constants, keep those generated files as generated artifacts from the repo workflow.

## Scope confirmations

- No GitHub write was performed.
- No branch/commit/PR was created.
- No planning docs are included.
- No domain files are included.
- No client UI files are included.
- No generated artifacts were manually edited.
