# SL-EMP-REQ-003 — Start Request Review Implementation

Archive: `sl-emp-req-003-start-request-review-v12.zip`

Scope:
- implements `POST /api/employee/requests/{requestId}/review/start`;
- starts request-owned review through `ConnectionRequest.StartReview(Employee, startedAt)`;
- returns `204 No Content` on success;
- adds focused API integration tests with DB state assertions.

Changed files:
- `EnergyManagement.Server/L1/Application/Abstractions/IEmployeeRepository.cs`
- `EnergyManagement.Server/L1/Persistence/Repositories/EmployeeRepository.cs`
- `EnergyManagement.Server/L1/Application/Commands/L1Commands.cs`
- `EnergyManagement.Server/L1/Application/Commands/EmployeeStartRequestReviewHandler.cs`
- `EnergyManagement.Server/L1/Persistence/L1DbContext.cs`
- `EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs`
- `EnergyManagement.Server/Program.cs`
- `EnergyManagement.Testing/TestDatabase/TestDatabaseManager.cs`
- `Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs`
- `Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeStartRequestReviewIntegrationTests.cs`

Not included:
- docs/planning changes;
- client UI changes;
- generated OpenAPI / generated TypeScript API types;
- EF migrations.

Generated artifacts must be produced locally with repo commands after applying this archive.
