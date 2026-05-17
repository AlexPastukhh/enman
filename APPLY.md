# Apply SL-APPL-003 archive

Run from the repository root.

The archive is merge-ready and has repo-relative paths. It does not contain a wrapper folder.

## Apply

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-003-make-current-default-server.zip" -DestinationPath "." -Force
```

If a temporary folder from a previous apply attempt exists, remove it:

```powershell
Remove-Item -Path ".\_incoming_sl_appl_003" -Recurse -Force
```

## Verify source and tests

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

## Regenerate API artifacts

Do not manually edit generated artifacts.

```powershell
npm run generate:openapi
npm run generate:api-types
```

Stage generated artifacts before `check:api`, because `check:api` ends with `git diff --exit-code` against the working tree:

```powershell
git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts
npm run check:api
```

## Expected generated files after local generation

- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`

## Notes

- No GitHub write was performed.
- No domain files are included.
- No planning docs are included.
- No client UI files are included.
