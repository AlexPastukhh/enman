# APPLY — SL-EMP-REQ-004 Approve Request Review

Apply from repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-emp-req-004-approve-request-review-v18.zip" -DestinationPath . -Force
```

Verify server/domain/tests:

```powershell
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

Because this adds an API endpoint, regenerate and check API artifacts through repo commands:

```powershell
npm.cmd run generate:openapi
npm.cmd run generate:api-types

git add .\Shared\openapi.json .\energymanagement.client\src\shared\api\generated\openapi-types.ts

npm.cmd run check:api
```

Expected endpoint:

```http
POST /api/employee/requests/{requestId}/review/approve
204 No Content
```
