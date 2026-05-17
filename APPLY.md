# Apply

From repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-agr-exch-004-agreement-exchange-details-read-v23.zip" -DestinationPath . -Force
```

Recommended checks:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

API contract changed. Generate and check API artifacts through repo workflow:

```powershell
npm.cmd run generate:openapi
npm.cmd run generate:api-types

git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm.cmd run check:api
```
