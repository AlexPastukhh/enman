# APPLY — Employee auth integration test namespace compile fix

Apply from repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\employee-auth-test-namespace-compile-fix-v18.zip" -DestinationPath . -Force
```

Then run:

```powershell
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```
