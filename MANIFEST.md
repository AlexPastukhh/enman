# L2-EMP-DASH-001.client — Employee Request Dashboard archive

## Summary

Client-only implementation archive for the Employee Request Dashboard read-list sidecar.

Implements:

- `/employee/requests` route helper and router entry;
- `EmployeeDashboardPage` with signed-out, non-Employee, loading, invalid-filter, error, empty and success states;
- Employee request dashboard shared API wrapper for `GET /api/employee/requests`;
- entity API/query/model files for dashboard read data;
- read-only dashboard list/row/review-state UI under `entities/employee-request/ui`;
- status and reviewState filters backed by URL query params;
- component/shared API/entity tests for changed code.

## Added files

- `energymanagement.client/src/shared/api/employeeRequestApi.ts`
- `energymanagement.client/src/shared/api/employeeRequestApi.test.ts`
- `energymanagement.client/src/entities/employee-request/api/listEmployeeDashboardRequests.ts`
- `energymanagement.client/src/entities/employee-request/api/listEmployeeDashboardRequests.test.ts`
- `energymanagement.client/src/entities/employee-request/model/employeeRequestFilters.ts`
- `energymanagement.client/src/entities/employee-request/model/employeeRequestTypes.ts`
- `energymanagement.client/src/entities/employee-request/model/employeeRequestQueryKeys.ts`
- `energymanagement.client/src/entities/employee-request/model/useEmployeeRequestDashboardQuery.ts`
- `energymanagement.client/src/entities/employee-request/ui/employeeRequestDashboardConst.ts`
- `energymanagement.client/src/entities/employee-request/ui/formatEmployeeRequest.ts`
- `energymanagement.client/src/entities/employee-request/ui/EmployeeReviewStateBadge.tsx`
- `energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardEmptyState.tsx`
- `energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardRow.tsx`
- `energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardList.tsx`
- `energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardList.test.tsx`
- `energymanagement.client/src/entities/employee-request/ui/employeeRequestDashboard.css`
- `energymanagement.client/src/pages/employee/dashboard/EmployeeDashboardPage.tsx`
- `energymanagement.client/src/pages/employee/dashboard/EmployeeRequestDashboardFilters.tsx`
- `energymanagement.client/src/pages/employee/dashboard/EmployeeRequestDashboardFilters.test.tsx`
- `energymanagement.client/src/pages/employee/dashboard/employeeDashboardPage.css`
- `energymanagement.client/src/pages/employee/dashboard/model/employeeDashboardUrlFilters.ts`
- `energymanagement.client/src/pages/employee/dashboard/model/employeeDashboardUrlFilters.test.ts`

## Replaced files

- `energymanagement.client/src/shared/config/clientRoutes.ts`
- `energymanagement.client/src/app/router/router.tsx`

## Deleted files

None.

## Generated artifacts

Unchanged:

- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`
- `Shared/constants.json`
- `Shared/errorcodes.json`

No generated artifact was manually edited.

## Tests changed

Added:

- `energymanagement.client/src/shared/api/employeeRequestApi.test.ts`
- `energymanagement.client/src/entities/employee-request/api/listEmployeeDashboardRequests.test.ts`
- `energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardList.test.tsx`
- `energymanagement.client/src/pages/employee/dashboard/model/employeeDashboardUrlFilters.test.ts`
- `energymanagement.client/src/pages/employee/dashboard/EmployeeRequestDashboardFilters.test.tsx`

No E2E test is included because the server endpoint/generated OpenAPI contract is not implemented in this repo snapshot.

## Commands run and results

- `npm install` — success.
- `npm --prefix ./energymanagement.client install` — success; npm reported 8 audit vulnerabilities.
- `npm --prefix ./energymanagement.client run build` — success.
- `npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/shared/api/employeeRequestApi.test.ts src/entities/employee-request/api/listEmployeeDashboardRequests.test.ts src/entities/employee-request/ui/EmployeeRequestDashboardList.test.tsx src/pages/employee/dashboard/model/employeeDashboardUrlFilters.test.ts src/pages/employee/dashboard/EmployeeRequestDashboardFilters.test.tsx` — success: 5 test files passed, 12 tests passed.
- `npm --prefix ./energymanagement.client run test -- --run --reporter=dot` — timed out in sandbox after printing passing dots; targeted changed tests passed.
- `npm --prefix ./energymanagement.client run lint` — failed on existing `react-refresh/only-export-components` errors outside this slice: `TestSetup.tsx`, `router.tsx`, `SessionProvider.tsx`, `pageErrorContext.tsx`.

Not run:

- `npm run check:api` — not run because this sandbox does not have dotnet tooling.
- `npm run test:e2e` — not run because the Employee endpoint is not implemented and E2E requires dotnet/localdb test environment.

## Non-goals respected

- No server/backend changes.
- No `Domain.EnergyManagement` changes.
- No planning docs changes.
- No database/migration changes.
- No generated artifact changes.
- No review command/action implementation.
- No start/approve/reject UI.
- No local CSRF mechanics.
- No GitHub write, branch, commit or PR.

## Risks / handoff notes

- The uploaded client sidecar says runtime implementation waits for the server read endpoint and generated DTOs. This archive implements the client against the documented `SL-EMP-REQ-001` endpoint/DTO direction because generated OpenAPI types are not present yet.
- Once `SL-EMP-REQ-001` backend is implemented and OpenAPI is generated, replace local shared API DTO aliases with generated OpenAPI type aliases.
- The route uses `session.role === "Employee"`. If Employee auth/session role uses another value, update the session guard.
- `employeeRequestDetails` route helper is added for row links, but the actual Employee details page remains future `SL-EMP-REQ-002.client` work.
