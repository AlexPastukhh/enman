# APPLY

From repository root:

```powershell
cd C:\enman\enman
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-approve-reject-windowsauth-combined.zip" -DestinationPath "." -Force
```

Then run:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

Regenerate API artifacts:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd run check:api
```

Client checks:

```powershell
npm.cmd --prefix .\energymanagement.client run build
npm.cmd --prefix .\energymanagement.client run test -- --run
```

If build says a DLL is locked by EnergyManagement.Server, stop the running server process first.
