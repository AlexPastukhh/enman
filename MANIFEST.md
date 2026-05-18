# MANIFEST

Archive: `sl-emp-req-002-details-server-client-draft-refactor-v1.zip`  
Review folder: `_archive-review/2026-05-19-sl-emp-req-002-details-server-client-draft-refactor-v1/`

## Purpose

Docs-only paired refactor of:

```text
SL-EMP-REQ-002 — Employee Request Details Read
L2-EMP-DETAILS-001.client — Employee Request Details
```

## Replacement files

```text
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
```

## Original snapshots included

```text
_archive-review/2026-05-19-sl-emp-req-002-details-server-client-draft-refactor-v1/original-files/planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
_archive-review/2026-05-19-sl-emp-req-002-details-server-client-draft-refactor-v1/original-files/planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
```

## Code evidence used read-only

```text
EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs
EnergyManagement.Server/L1/Application/Queries/EmployeeRequestDetailsQuery.cs
EnergyManagement.Server/L1/Application/Queries/EmployeeRequestDetailsHandler.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRequestDetailsIntegrationTests.cs
energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.tsx
energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
energymanagement.client/src/entities/employee-request/api/getEmployeeRequestDetails.ts
energymanagement.client/src/entities/employee-request/api/getEmployeeRequestDetails.test.ts
energymanagement.client/src/entities/employee-request/api/employeeRequestApiTypes.ts
energymanagement.client/src/entities/employee-request/model/useEmployeeRequestDetailsQuery.ts
energymanagement.client/src/entities/employee-request/model/employeeRequestQueryKeys.ts
energymanagement.client/src/entities/employee-request/model/employeeRequestTypes.ts
energymanagement.client/src/entities/employee-request/model/reviewActionAvailability.ts
energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDetailsView.tsx
energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDetailsView.test.tsx
energymanagement.client/src/entities/employee-request/ui/EmployeeReviewActionAvailabilityPanel.tsx
```

## Not included

```text
- no runtime code changes
- no tests changed
- no generated artifacts
- no runtime UI refactor
- no page flow / redirect audit
- no navigation updates
```
