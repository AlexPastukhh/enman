# Apply L2-REVIEW-START-001.client archive

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-review-start-001-client-start-request-review.zip" -DestinationPath "." -Force
```

## Verify

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
npm --prefix .\energymanagement.client run lint
```

Targeted changed tests:

```powershell
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/features/employee-request/start-review/api/startRequestReview.test.ts src/features/employee-request/start-review/ui/StartReviewButton.test.tsx src/entities/employee-request/ui/EmployeeRequestDashboardList.test.tsx src/pages/employee/requests/dashboard/EmployeeDashboardPage.test.tsx src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
```
