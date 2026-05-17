# APPLY

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-agreement-exchange-send-proposal-impl.zip" -DestinationPath "." -Force
```

Then verify:

```powershell
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

Regenerate API artifacts after green server/tests:

```powershell
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd run check:api
```
