# Apply SL-EMP-REQ-002 archive

From repo root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-emp-req-002-employee-request-details-read-v12.zip" -DestinationPath . -Force
```

Run server checks:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

Because this archive changes API shape, regenerate and verify generated artifacts:

```powershell
npm.cmd run generate:openapi
npm.cmd run generate:api-types

git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm.cmd run check:api
```

Expected generated files after running the workflow:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```
