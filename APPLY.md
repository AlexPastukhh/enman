# Apply archive

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-domain-classes-implementation.zip" -DestinationPath "." -Force
```

Then verify locally:

```powershell
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
```

If the full solution/server is built, also run the relevant tests. This archive does not include persistence mappings/migrations for the new L2 classes.
