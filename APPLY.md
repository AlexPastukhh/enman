# APPLY

From repository root, extract the archive over the working tree:

```powershell
Expand-Archive -Path "C:\path\to\enman-auth-employee-windows-impl.zip" -DestinationPath "." -Force
```

Then verify and regenerate if needed:

```powershell
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
dotnet run --project .\EnergyManagement.Tools -- generate-client-constants --out Shared --check
npm.cmd run check:api
npm.cmd --prefix .\energymanagement.client run build
npm.cmd --prefix .\energymanagement.client run test -- --run
```

If `check:api` reports only intended generated OpenAPI/type changes before commit, stage the generated artifacts and rerun after commit/clean worktree.

Expected old behavior remains absent:

```powershell
rg "/api/auth|/api/ClientRequest|registerIndividual|IndivCreateConnectionRequest" Shared EnergyManagement.Server energymanagement.client/src Tests.EnergyManagement
```

Expected new auth surface:

```powershell
rg "EmployeeWindowsSignIn|employee/auth/windows-signin|EmployeeWindows|WindowsLogin" EnergyManagement.Server Tests.EnergyManagement energymanagement.client/src Shared
```
