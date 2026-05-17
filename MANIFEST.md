# MANIFEST

Archive: enman-auth-employee-windows-fix-mini.zip

Scope: compile fix for Employee Windows auth implementation.

Files included:
- EnergyManagement.Server/Program.cs
- EnergyManagement.Server/Api/Auth/EmployeeAuthSchemes.cs
- EnergyManagement.Server/L1/Application/Security/L1ClaimsPrincipalFactory.cs

Fixes:
- Use standard Negotiate scheme name through EmployeeAuthSchemes.EmployeeWindows alias.
- Replace `.AddNegotiate(EmployeeAuthSchemes.EmployeeWindows)` with `.AddNegotiate()`.
- Ensure ClaimsPrincipalFactory formats FullName from LastName/FirstName/MiddleName instead of using missing `FullName.Value`.
