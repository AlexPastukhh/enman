# MANIFEST

Archive: enman-approve-reject-windowsauth-combined.zip

Scope:
- Merge SL-EMP-REQ-004 Approve Request Review implementation
- Merge SL-EMP-REQ-005 Reject Request Review implementation
- Keep combined command/controller/test helper files
- Add Windows Auth TestServer fix

Important merged files:
- EnergyManagement.Server/L1/Application/Commands/L1Commands.cs
  - Start + Approve + Reject command/result/status blocks
- EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs
  - Start + Approve + Reject endpoints
  - Reject DTO validator dependency
- Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs
  - Start + Approve + Reject helper methods
- Tests.EnergyManagement/Integration/WebAppFactory.cs
  - Base integration test host replaces only EmployeeWindows/Negotiate scheme provider
  - Cookie auth remains real

Included from approve slice:
- Domain.EnergyManagement/L1/Requests/RequestReview.cs
- EnergyManagement.Server/L1/Application/Commands/EmployeeApproveRequestReviewHandler.cs
- Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeApproveRequestReviewIntegrationTests.cs

Included from reject slice:
- Domain.EnergyManagement/L1/Requests/ConnectionRequest.cs
- EnergyManagement.Server/L1/Api/L1Dtos.cs
- EnergyManagement.Server/L1/Api/Validation/L1FieldNames.cs
- EnergyManagement.Server/L1/Api/Validation/EmployeeRejectRequestReviewDtoValidator.cs
- EnergyManagement.Server/L1/Application/Commands/EmployeeRejectRequestReviewHandler.cs
- Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRejectRequestReviewIntegrationTests.cs
- client reject-review feature files

Included from Windows Auth fix:
- EnergyManagement.Server/Api/Auth/EmployeeAuthSchemes.cs
- EnergyManagement.Server/L1/Application/Security/L1ClaimsPrincipalFactory.cs
- Tests.EnergyManagement/Integration/WebAppFactory.cs

Generated artifacts are not included:
- Shared/openapi.json
- energymanagement.client/src/shared/api/generated/openapi-types.ts

Regenerate them locally after apply.
