# APPLY

From the repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-agreement-exchange-list-impl.zip" -DestinationPath "." -Force
```

Then run:

```powershell
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

Apply/update local test DB if needed:

```powershell
dotnet ef database update `
  --project .\EnergyManagement.Server\EnergyManagement.Server.csproj `
  --startup-project .\EnergyManagement.Server\EnergyManagement.Server.csproj `
  --context L1DbContext
```

Regenerate API artifacts after server build is green:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd run check:api
npm.cmd --prefix .\energymanagement.client run build
npm.cmd --prefix .\energymanagement.client run test -- --run
```
