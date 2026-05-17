# APPLY — L2-REVIEW-APPROVE-001.client

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-review-approve-001-client-approve-request-review.zip" -DestinationPath "." -Force
```

Then verify:

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

Targeted changed tests:

```powershell
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/features/employee-request/approve-review/api/approveRequestReview.test.ts src/features/employee-request/approve-review/ui/ApproveReviewButton.test.tsx src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
```

If `npm run check:api` reports stale OpenAPI artifacts after backend approve endpoint work, run:

```powershell
npm run generate:api
npm run check:api
```

and include the changed generated artifacts in the backend/API handoff.
