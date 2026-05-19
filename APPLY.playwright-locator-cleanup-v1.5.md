# APPLY — playwright-locator-cleanup-v1.5

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\playwright-locator-cleanup-v1.5.zip" -DestinationPath "." -Force
```

This is an incremental fix over v1.4. It fixes the remaining failures observed after v1.4:

- seed-e2e-demo-data fails with `Invalid column name 'ClientAccountId'`;
- `create-individual.spec.ts` expects localized verification text while the account summary currently renders raw `Unverified`;
- `my-requests.spec.ts` / `my-requests-filters.spec.ts` match hidden `<option value="InReview">На рассмотрении</option>` instead of a visible request card status.

Validation:

```powershell
dotnet build .\EnergyManagement.sln
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

dotnet run --project .\EnergyManagement.Tools -- reset-test-db
dotnet run --project .\EnergyManagement.Tools -- seed-e2e-demo-data

npx playwright test tests/e2e/agreement-exchange/agreement-exchange-flow.spec.ts --project=chromium --reporter=list
npx playwright test tests/e2e/applicant-party/create-individual.spec.ts --project=chromium --reporter=list
npx playwright test tests/e2e/employee/employee-request-review.spec.ts --project=chromium --reporter=list
npx playwright test tests/e2e/requests/my-requests-filters.spec.ts --project=chromium --reporter=list
npx playwright test tests/e2e/requests/my-requests.spec.ts --project=chromium --reporter=list

npm.cmd run test:e2e
npm.cmd run screenshots:vkr
```
