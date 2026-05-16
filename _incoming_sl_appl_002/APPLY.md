# APPLY — SL-APPL-002 Account Applicant Parties Read / Templates

## How to apply

1. Extract this archive into a temporary folder.
2. Copy the archive contents into the repository root, preserving paths.
3. Allow replacement of existing files.
4. Review the changed files.
5. Run build/tests/generation checks from repository root.

Example from repository root:

```bash
# Linux/macOS example after extracting archive to /tmp/sl-appl-002
cp -R /tmp/sl-appl-002/* .
```

PowerShell example:

```powershell
Copy-Item -Path "C:\temp\sl-appl-002\*" -Destination "." -Recurse -Force
```

## Recommended checks after applying

```bash
dotnet build EnergyManagement.Server/EnergyManagement.Server.csproj
dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj

dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
npm.cmd run check:api
```

If generation changes OpenAPI/types/constants, keep those generated files as generated artifacts from the repo workflow.

## Scope confirmations

- No GitHub write was performed.
- No branch/commit/PR was created.
- No planning docs are included.
- No domain files are included.
- No client UI files are included.
- No generated artifacts were manually edited.
