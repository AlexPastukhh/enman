# MANIFEST — Employee auth integration test namespace compile fix

Archive: employee-auth-test-namespace-compile-fix-v18.zip

Scope: compile fix only.

Changed files:

```text
Tests.EnergyManagement/Integration/L1/Auth/EmployeeAuthenticationIntegrationTests.cs
```

Fix:

```text
Use global::EnergyManagement.Testing.TestDatabase.TestDatabaseManager from inside Tests.EnergyManagement.* namespace.
```

No production/domain/client/docs/generated/migration changes.
