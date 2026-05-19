# Apply — Playwright seed request review fix v1.6

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\playwright-locator-cleanup-v1.6.zip" -DestinationPath "." -Force
```

Validate:

```powershell
dotnet build .\EnergyManagement.sln
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

dotnet run --project .\EnergyManagement.Tools -- reset-test-db
dotnet run --project .\EnergyManagement.Tools -- seed-e2e-demo-data

npx playwright test tests/e2e/agreement-exchange/agreement-exchange-flow.spec.ts --project=chromium --reporter=list
npx playwright test tests/e2e/employee/employee-request-review.spec.ts --project=chromium --reporter=list
npm.cmd run test:e2e
npm.cmd run screenshots:vkr
```
