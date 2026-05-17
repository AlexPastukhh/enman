# L2-REVIEW-START-001.client — Start Request Review

## Summary

Client-only implementation archive for `L2-REVIEW-START-001.client`.

Implements one reusable Start Review command feature with two host placements:

- Employee request dashboard/list row under `pages/employee/requests/dashboard`.
- Employee request details action area under `pages/employee/requests/details`.

The endpoint wrapper follows the current API placement rule:

- command wrapper lives in `features/employee-request/start-review/api`;
- `shared/api` remains generic infrastructure only;
- no `shared/api/employeeRequestApi.ts` business wrapper was added.

## Added files

- `energymanagement.client/src/features/employee-request/start-review/api/startRequestReview.ts`
- `energymanagement.client/src/features/employee-request/start-review/api/startRequestReview.test.ts`
- `energymanagement.client/src/features/employee-request/start-review/api/startReviewApiTypes.ts`
- `energymanagement.client/src/features/employee-request/start-review/model/useStartRequestReviewMutation.ts`
- `energymanagement.client/src/features/employee-request/start-review/ui/StartReviewButton.tsx`
- `energymanagement.client/src/features/employee-request/start-review/ui/StartReviewButton.test.tsx`
- `energymanagement.client/src/features/employee-request/start-review/ui/startReviewButton.css`
- `energymanagement.client/src/features/employee-request/start-review/ui/startReviewButtonConst.ts`
- `energymanagement.client/src/pages/employee/requests/dashboard/EmployeeDashboardPage.test.tsx`

## Replaced files

- `energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardList.tsx`
- `energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardRow.tsx`
- `energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardList.test.tsx`
- `energymanagement.client/src/entities/employee-request/ui/employeeRequestDashboard.css`
- `energymanagement.client/src/pages/employee/requests/dashboard/EmployeeDashboardPage.tsx`
- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.tsx`
- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx`

## Deleted files

None.

## Generated artifacts

Unchanged.

This archive consumes the already-present generated OpenAPI operation:

- `EmployeeStartRequestReview`
- `POST /api/employee/requests/{requestId}/review/start`

No manual generated artifact edits were made.

## Tests changed

- Added feature API test for POST/no-body/CSRF header behavior.
- Added StartReviewButton component tests.
- Added dashboard page host-placement test.
- Extended dashboard list test for row action slot.
- Updated details page test to account for the StartReview action slot.

## Commands run and results

```text
npm --prefix ./energymanagement.client install
→ success; npm reported existing audit vulnerabilities

npm --prefix ./energymanagement.client run build
→ success

npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/features/employee-request/start-review/api/startRequestReview.test.ts src/features/employee-request/start-review/ui/StartReviewButton.test.tsx src/entities/employee-request/ui/EmployeeRequestDashboardList.test.tsx src/pages/employee/requests/dashboard/EmployeeDashboardPage.test.tsx src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
→ success: 5 test files passed, 15 tests passed

npm --prefix ./energymanagement.client run test -- --run --reporter=dot
→ timed out in sandbox after startup output; targeted changed tests passed

npm --prefix ./energymanagement.client run lint
→ failed on existing react-refresh/only-export-components errors outside this slice:
  - src/Tests/ComponentTest/TestClasses/TestSetup.tsx
  - src/app/router/router.tsx
  - src/entities/session/model/SessionProvider.tsx
  - src/shared/errors/pageErrorContext.tsx
```

## Non-goals respected

- No server/backend changes.
- No Domain.EnergyManagement changes.
- No planning docs changes.
- No database/migration changes.
- No generated artifact changes.
- No approve/reject implementation.
- No agreement proposal implementation.
- No employee assignment/queue implementation.
- No local CSRF token mechanics.
- No business endpoint wrapper added under `shared/api`.
- No unrelated cleanup.
- No GitHub write.
- No branch/commit/PR.

## Risks / handoff notes

- The implementation assumes the backend/generated contract for `EmployeeStartRequestReview` is already present in the target repo.
- The command returns `204 No Content`, so the client wrapper resolves `Promise<void>`.
- Dashboard placement is intentionally limited to rows where compact row state is sufficient: `status === "InReview"` and `reviewState === "NotStarted"`.
- Details placement uses existing details-derived action availability and renders a disabled StartReviewButton with reason when action is unavailable.
- Full test run timed out in sandbox; targeted tests for changed files passed.
- Lint still fails on pre-existing react-refresh rule violations outside this slice.
