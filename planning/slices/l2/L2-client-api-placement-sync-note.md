# L2 Client API Placement Sync Note

Status: current / architecture correction note  
Scope: L2 client sidecars drafted before or during API placement correction

## 1. Purpose

Some L2 client drafts were written while the project still described a `shared/api` business-wrapper layer.

The current accepted rule is stricter:

```text
shared/api
  transport/generated infrastructure only.

entities/*/api
  read endpoint wrappers.

features/*/api
  command endpoint wrappers.
```

## 2. Employee Dashboard

```text
entities/employee-request/api/listEmployeeDashboardRequests.ts
  owns GET /api/employee/requests or chosen dashboard read endpoint.

shared/api
  owns fetchJson and generated OpenAPI types only.
```

## 3. Employee Details

```text
entities/employee-request/api/getEmployeeRequestDetails.ts
  owns GET /api/employee/requests/{requestId} read endpoint wrapper.

shared/api
  owns fetchJson and generated OpenAPI types only.
```

## 4. Start Review

```text
features/employee-request/start-review/api/startRequestReview.ts
  owns POST /api/employee/requests/{requestId}/review/start command wrapper.

shared/api
  owns CSRF-aware transport and generated OpenAPI types only.
```
