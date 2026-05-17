# APPLY

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-auth-employee-windows-fix-mini.zip" -DestinationPath "." -Force
```

Then verify:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```
