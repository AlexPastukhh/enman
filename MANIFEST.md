# MANIFEST

Archive: enman-employee-reject-review-impl.zip
Scope: SL-EMP-REQ-005 — Reject Request Review implementation.

Included files:
- Domain.EnergyManagement/L1/Requests/ConnectionRequest.cs
- EnergyManagement.Server/L1/Api/L1Dtos.cs
- EnergyManagement.Server/L1/Api/Validation/L1FieldNames.cs
- EnergyManagement.Server/L1/Api/Validation/EmployeeRejectRequestReviewDtoValidator.cs
- EnergyManagement.Server/L1/Application/Commands/L1Commands.cs
- EnergyManagement.Server/L1/Application/Commands/EmployeeRejectRequestReviewHandler.cs
- EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs
- EnergyManagement.Server/Program.cs
- Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs
- Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRejectRequestReviewIntegrationTests.cs
- energymanagement.client/src/features/employee-request/reject-review/api/rejectRequestReview.ts
- energymanagement.client/src/features/employee-request/reject-review/api/rejectRequestReview.test.ts
- energymanagement.client/src/features/employee-request/reject-review/model/useRejectRequestReviewMutation.ts

Not included:
- generated OpenAPI/types artifacts. Regenerate them after applying.
- approve review implementation.
- employee auth/windows auth changes.
- dashboard UI wiring.
