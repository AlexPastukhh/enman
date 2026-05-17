# SL-EMP-REQ-002 — Employee Request Details Read

Archive: `sl-emp-req-002-employee-request-details-read-v12.zip`

Scope:
- backend/API read slice implementation for `GET /api/employee/requests/{requestId}`;
- Dapper/read projection;
- details response DTOs;
- focused API integration tests;
- test helper update for RequestReview table column naming.

Included files:

```text
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Queries/EmployeeRequestDetailsHandler.cs
EnergyManagement.Server/L1/Application/Queries/EmployeeRequestDetailsQuery.cs
EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs
EnergyManagement.Testing/TestDatabase/TestDatabaseManager.cs
Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRequestDetailsIntegrationTests.cs
Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs
APPLY.md
MANIFEST.md
```

Not included:

```text
planning/**
docs/**
client UI files
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
EF migrations
```

Generated artifacts are not included because they must be produced by the repository generation commands after applying this source archive.
