# Apply L2 Domain Persistence Cleanup Patch

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-domain-persistence-cleanup.zip" -DestinationPath "." -Force
Remove-Item .\Domain.EnergyManagement\L1\Requests\ReviewDecision.cs -Force
```

Then verify locally:

```powershell
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

Notes:

- This patch does not add EF migrations.
- L1 test database setup creates/ensures the new L1 request review/agreement proposal tables for tests.
- Generated OpenAPI/client artifacts are not included and were not manually edited.
