# MANIFEST

Archive: `sl-emp-req-001-dashboard-server-client-draft-refactor-v1.zip`  
Review folder: `_archive-review/2026-05-19-sl-emp-req-001-dashboard-server-client-draft-refactor-v1/`

## Purpose

Docs-only paired refactor of:

```text
SL-EMP-REQ-001 — Employee Request List Read
L2-EMP-DASH-001.client — Employee Request Dashboard
```

## Replacement files

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
```

## Original snapshots included

```text
_archive-review/2026-05-19-sl-emp-req-001-dashboard-server-client-draft-refactor-v1/original-files/planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
_archive-review/2026-05-19-sl-emp-req-001-dashboard-server-client-draft-refactor-v1/original-files/planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
```

## Code evidence used read-only

```text
EnergyManagement.Server/L1/Controllers/EmployeeRequestsController.cs
EnergyManagement.Server/L1/Application/Queries/EmployeeRequestListQuery.cs
EnergyManagement.Server/L1/Application/Queries/EmployeeRequestListHandler.cs
EnergyManagement.Server/L1/Api/Validation/EmployeeRequestListQueryDtoValidator.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
Tests.EnergyManagement/Integration/L1/EmployeeRequests/EmployeeRequestListIntegrationTests.cs
energymanagement.client/src/pages/employee/requests/dashboard/**
energymanagement.client/src/entities/employee-request/api/listEmployeeDashboardRequests.ts
energymanagement.client/src/entities/employee-request/api/employeeRequestApiTypes.ts
energymanagement.client/src/entities/employee-request/model/**
energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboard*.tsx
energymanagement.client/src/entities/employee-request/ui/EmployeeReviewStateBadge.tsx
energymanagement.client/src/entities/employee-request/**/*.test.ts*
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
