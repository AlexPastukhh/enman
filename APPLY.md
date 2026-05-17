# Apply

From repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-emp-req-003-start-request-review-v12-compile-fix.zip" -DestinationPath . -Force
```

Then verify:

```powershell
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```
