# APPLY — Employee TPH Runtime/Test Database Fix

Apply from repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\employee-tph-runtime-fix-v15.zip" -DestinationPath . -Force
```

Recommended checks:

```powershell
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
dotnet run --project .\EnergyManagement.Tools -- generate-client-constants --out Shared --check
npm.cmd run check:api
```

Expected notes:
- This archive does not change API shape, so OpenAPI/types should not change.
- This archive does not include migrations because it only fixes test database provisioning/reset behavior.
