# MANIFEST — SL-EMP-REQ-004 Approve Request Review

Archive: `sl-emp-req-004-approve-request-review-v18.zip`

Scope:
- Implement backend/API command slice `SL-EMP-REQ-004 — Approve Request Review`.
- Add API integration tests with DB state assertions.
- Keep command response as `204 No Content` with no DTO/body.

Included files:

```text
Domain.EnergyManagement/L1/Requests/RequestReview.cs
EnergyManagement.Server/L1/Application/Commands/L1Commands.cs
EnergyManagement.Server/L1/Application/Commands/EmployeeApproveRequestReviewHandler.cs
EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs
Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs
Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeApproveRequestReviewIntegrationTests.cs
APPLY.md
MANIFEST.md
```

Not included:
- docs/planning changes
- client UI changes
- migrations
- manually edited OpenAPI/generated artifacts
- approve/reject client sidecar work
- AgreementProposalExchange creation

Generated API artifacts should be produced locally by repo commands after applying this archive.
