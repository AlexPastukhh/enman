# L2 Current Implementation Inventory

Status: current evidence-based inventory / generated API + client code inspected  
Scope: existing L2 slice files vs current code/API artifacts

## 1. Evidence Scope

This inventory is based on inspected repository artifacts in `my-changes`:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
energymanagement.client/src/shared/config/clientRoutes.ts
energymanagement.client/src/app/router/router.tsx
energymanagement.client/src/pages/employee/dashboard/EmployeeDashboardPage.tsx
energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.tsx
energymanagement.client/src/entities/employee-request/api/*
energymanagement.client/src/entities/employee-request/model/*
energymanagement.client/src/entities/employee-request/ui/*
```

Connector search did not locate server controller/source file paths directly, but generated OpenAPI and generated TypeScript types contain the Employee request API paths/operations. Treat server status below as API-contract-present unless controller/source/test inspection later proves otherwise.

## 2. Current L2 Slice Status

| Slice file | Current code status | Evidence | Docs action |
|---|---|---|---|
| `SL-EMP-REQ-001-employee-request-list-read.md` | API contract present; client consumer implemented | `GET /api/employee/requests` exists in OpenAPI/generated types; dashboard client route/page/query/wrapper implemented | no longer say plain `not implemented`; mark as `API contract present / implementation verification pending` |
| `L2-EMP-DASH-001-employee-request-dashboard.client.md` | first-stage client implementation present | route `/employee/requests`, router mapping, page, query hook, entity API wrapper and list UI exist | mark as implemented/current first-stage client |
| `SL-EMP-REQ-002-employee-request-details-read.md` | API contract present; client consumer implemented | `GET /api/employee/requests/{requestId}` exists in OpenAPI/generated types; details route/page/query/wrapper implemented | no longer say plain `not implemented`; mark as `API contract present / implementation verification pending` |
| `L2-EMP-DETAILS-001-employee-request-details.client.md` | first-stage client implementation present | route `/employee/requests/:requestId`, router mapping, page, query hook, entity API wrapper and details UI exist | mark as implemented/current first-stage client |
| `SL-EMP-REQ-003-start-request-review.md` | API contract present | `POST /api/employee/requests/{requestId}/review/start` exists in OpenAPI/generated types | mark as `API contract present / source+tests verification pending` |
| `L2-REVIEW-START-001-start-request-review.client.md` | not implemented | expected feature files under `features/employee-request/start-review/*` were not found; details page does not pass a `renderReviewActions` StartReview feature | keep as not implemented / client gap |

## 3. Server vs Client Slice Naming

Do not create a separate “dashboard server” slice.

Use this split:

```text
SL-EMP-REQ-001
  server/API employee request list/read endpoint for the dashboard.

L2-EMP-DASH-001.client
  client dashboard page/UI consuming the list endpoint.
```

Same pattern:

```text
SL-EMP-REQ-002
  server/API employee request details endpoint.

L2-EMP-DETAILS-001.client
  client details page/UI consuming the details endpoint.

SL-EMP-REQ-003
  server/API StartReview command endpoint.

L2-REVIEW-START-001.client
  client StartReview button/action/mutation consuming the command endpoint.
```

## 4. Important Client Details Drift

`L2-EMP-DETAILS-001.client` previously preferred server-provided action availability.
Current client implementation derives action availability locally from:

```text
EmployeeRequestDetails.status
EmployeeRequestDetails.reviewState
```

This is acceptable as first-stage client behavior if the server details DTO provides enough state. Do not claim a server `reviewActionAvailability` DTO is already implemented unless the API adds it later.

Future server-provided action availability remains an extension point.

## 5. Remaining Implemented-vs-Not-Implemented Answer

Existing L2 slice files that are still not implemented:

```text
L2-REVIEW-START-001-start-request-review.client.md
```

Existing L2 slice files whose docs are stale and should be reconciled with code/API evidence:

```text
SL-EMP-REQ-001-employee-request-list-read.md
SL-EMP-REQ-002-employee-request-details-read.md
SL-EMP-REQ-003-start-request-review.md
L2-EMP-DASH-001-employee-request-dashboard.client.md
L2-EMP-DETAILS-001-employee-request-details.client.md
```

Planned but no full slice file yet:

```text
SL-EMP-REQ-004 — ApproveReview command
SL-EMP-REQ-005 — RejectReview command
SL-AGR-* — AgreementProposalExchange family
SL-DOC-* — AgreementDocumentRef/document-reference family
```

## 6. Verification To Run Before Final “Implemented” Label

For server/API slices:

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

For generated API sync after backend changes:

```powershell
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
```

For client slices:

```powershell
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

If API shape changes in implementation, include generated artifacts in the implementation handoff:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```
