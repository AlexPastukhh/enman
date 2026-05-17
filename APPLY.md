# Apply L2-REVIEW-REJECT-001.client archive

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-review-reject-001-client-reject-request-review.zip" -DestinationPath "." -Force
```

Then verify:

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

Targeted tests:

```powershell
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/features/employee-request/reject-review/api/rejectRequestReview.test.ts src/features/employee-request/reject-review/ui/RejectReviewForm.test.tsx src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
```
