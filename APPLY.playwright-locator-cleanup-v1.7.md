# Apply playwright locator cleanup v1.7

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\playwright-locator-cleanup-v1.7.zip" -DestinationPath "." -Force
```

Then verify:

```powershell
dotnet run --project .\EnergyManagement.Tools -- reset-test-db
dotnet run --project .\EnergyManagement.Tools -- seed-e2e-demo-data

npx playwright test tests/e2e/agreement-exchange/agreement-exchange-flow.spec.ts --project=chromium --reporter=list
npx playwright test tests/e2e/employee/employee-request-review.spec.ts --project=chromium --reporter=list

npm.cmd run test:e2e
npm.cmd run screenshots:vkr
```
