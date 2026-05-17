# Apply SL-EMP-REQ-003 archive

From repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-emp-req-003-start-request-review-v12.zip" -DestinationPath . -Force
```

Then verify server/tests:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

Because this archive changes API shape, regenerate generated artifacts using repo commands:

```powershell
npm.cmd run generate:openapi
npm.cmd run generate:api-types

git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm.cmd run check:api
```

Expected endpoint:

```http
POST /api/employee/requests/{requestId}/review/start
```

Expected success response:

```http
204 No Content
```
