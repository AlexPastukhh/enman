# APPLY

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-l1commands-full-fix.zip" -DestinationPath "." -Force
```

Then run:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```
