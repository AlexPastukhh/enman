# 2026-05-19-l2-appl-ver-run-001-client-implementation-v1

Client-only implementation archive for `L2-APPL-VER-RUN-001.client`.

Runtime scope:

```text
energymanagement.client/src/features/employee-request/applicant-verification/**
ergymanagement.client/src/entities/employee-request/** selected type/UI/test files
energymanagement.client/src/pages/employee/requests/dashboard/EmployeeDashboardPage.tsx
energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.tsx
```

Out of scope and not included:

```text
Domain.EnergyManagement/
EnergyManagement.Server/
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
planning/
```

Prerequisites expected already present on target branch:

```text
POST /api/employee/requests/{requestId}/applicant-party/verification/run
RunApplicantPartyVerificationResponseDto
EmployeeRequestApplicantVerificationDto
EmployeeRequestListItemDto.applicantVerification
EmployeeRequestDetailsDto.applicantVerification
```
