# MANIFEST — L2-REVIEW-APPROVE-001.client

## Slice

`L2-REVIEW-APPROVE-001.client — Approve Request Review`

Client-only implementation for the Employee Approve Review command action.

## Added files

- `energymanagement.client/src/features/employee-request/approve-review/api/approveRequestReview.ts`
- `energymanagement.client/src/features/employee-request/approve-review/api/approveRequestReview.test.ts`
- `energymanagement.client/src/features/employee-request/approve-review/model/useApproveRequestReviewMutation.ts`
- `energymanagement.client/src/features/employee-request/approve-review/ui/ApproveReviewButton.tsx`
- `energymanagement.client/src/features/employee-request/approve-review/ui/ApproveReviewButton.test.tsx`
- `energymanagement.client/src/features/employee-request/approve-review/ui/approveReviewButton.css`
- `energymanagement.client/src/features/employee-request/approve-review/ui/approveReviewButtonConst.ts`

## Replaced files

- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.tsx`
- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx`

## Deleted files

None.

## Generated artifacts

Unchanged in this archive.

Note: the uploaded project contains the backend approve endpoint in source, but the sandbox cannot run OpenAPI generation because `dotnet` is not installed. If local `npm run check:api` reports stale generated API artifacts, run the repo generation workflow and include:

- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`

## Tests changed

- Added API test for `approveRequestReview()`.
- Added component tests for `ApproveReviewButton`.
- Updated Employee details page tests to cover the approve action placement.

## Commands run and results

```text
npm --prefix ./energymanagement.client install
→ success; npm reported existing audit vulnerabilities

npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/features/employee-request/approve-review/api/approveRequestReview.test.ts src/features/employee-request/approve-review/ui/ApproveReviewButton.test.tsx src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
→ success: 3 test files passed, 12 tests passed

npm --prefix ./energymanagement.client run build
→ success

npm --prefix ./energymanagement.client run lint
→ failed on existing react-refresh/only-export-components errors outside this slice:
   - src/Tests/ComponentTest/TestClasses/TestSetup.tsx
   - src/app/router/router.tsx
   - src/entities/session/model/SessionProvider.tsx
   - src/shared/errors/pageErrorContext.tsx

dotnet --version
→ not available in sandbox, so check:api/OpenAPI generation was not run

zip integrity
→ success
```

## Non-goals respected

- No server/backend changes.
- No Domain.EnergyManagement changes.
- No planning docs changes.
- No database/migration changes.
- No generated artifact manual edits.
- No StartReview implementation changes.
- No RejectReview implementation changes.
- No AgreementProposalExchange implementation.
- No dashboard/list approve entry point.
- No local CSRF mechanics.
- No `shared/api` business endpoint wrapper.
- No unrelated cleanup.
- No GitHub write.

## Notes / risks

- Approve is details-only first pass.
- The command uses `POST /api/employee/requests/{requestId}/review/approve` with no request body and expects `204 No Content`.
- `ApproveReviewButton` supports optional confirmation via `requireConfirmation`, but the details page does not enable confirmation by default.
- The wrapper intentionally returns `Promise<void>` and does not introduce an approve response DTO.
- If generated OpenAPI is stale locally, run repo generation workflow and include generated artifacts in the backend/API handoff.
