# MANIFEST — Employee authentication / Windows sign-in implementation

Archive: `enman-auth-employee-windows-impl.zip`
Scope: implementation files for `CC-AUTH-EMP-001 — Employee Authentication / Windows Sign-in / Application Cookie Session`.

## What is included

### Domain / persistence
- `Domain.EnergyManagement/L1/Employees/Employee.cs`
- `EnergyManagement.Server/L1/Persistence/L1DbContext.cs`
- `EnergyManagement.Server/L1/Persistence/Repositories/EmployeeRepository.cs`
- `EnergyManagement.Server/L1/Application/Abstractions/IEmployeeRepository.cs`
- `EnergyManagement.Server/Migrations/20260517113834_ModelEmployeeAsAccountSubtype.cs`
- `EnergyManagement.Server/Migrations/20260517113834_ModelEmployeeAsAccountSubtype.Designer.cs`
- `EnergyManagement.Server/Migrations/L1DbContextModelSnapshot.cs`

### Server auth implementation
- `EnergyManagement.Server/EnergyManagement.Server.csproj`
- `EnergyManagement.Server/Program.cs`
- `EnergyManagement.Server/Api/Auth/EmployeeAuthSchemes.cs`
- `EnergyManagement.Server/L1/Application/Security/L1AuthClaimTypes.cs`
- `EnergyManagement.Server/L1/Application/Security/L1ClaimsPrincipalFactory.cs`
- `EnergyManagement.Server/L1/Application/Commands/L1Commands.cs`
- `EnergyManagement.Server/L1/Application/Commands/L1LoginClientAccountHandler.cs`
- `EnergyManagement.Server/L1/Controllers/L1Controller.cs`
- `EnergyManagement.Server/L1/Controllers/EmployeeAuthController.cs`

### Client support
- `energymanagement.client/src/features/auth/employee-windows-signin/api/signInEmployeeWithWindows.ts`
- `energymanagement.client/src/features/auth/employee-windows-signin/api/signInEmployeeWithWindows.test.ts`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`

### Generated contract
- `Shared/openapi.json`

### Tests/helpers
- `Tests.EnergyManagement/Domain/Employees/EmployeeTests.cs`
- `Tests.EnergyManagement/Integration/WebAppFactory.cs`
- `Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs`
- `Tests.EnergyManagement/Integration/L1/Auth/EmployeeAuthenticationIntegrationTests.cs`

## Behavior included

- Employee remains `Account` subtype through TPH.
- Employee additionally has optional `WindowsLogin` for Windows sign-in mapping.
- Password login is account-role aware: `ClientAccount` -> `Role=Client`, `Employee` -> `Role=Employee`.
- New `GET /api/employee/auth/windows-signin` endpoint maps `EmployeeWindows` external identity to Employee and issues normal app cookie.
- Runtime authorization remains app cookie + app roles.
- New `L1ClaimsPrincipalFactory` centralizes app-cookie claims for password login and Windows sign-in.
- Tests use fake EmployeeWindows scheme; no real AD/Kerberos/NTLM is required.

## Not included / not done

- No real Active Directory setup.
- No approve/reject implementation.
- No employee registration UI.
- No client UI button for Windows sign-in.
- No broad auth API split.
- No change to CSRF behavior.
