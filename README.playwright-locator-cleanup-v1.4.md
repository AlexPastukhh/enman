# playwright-locator-cleanup-v1.4

Purpose: safe replacement archive for Playwright locator cleanup with original-file backups.

This archive supersedes v1.3 by adding an archive-review folder that preserves the original versions of every replaced file.

Included changes:
- Update stale Playwright locators to current Russian UI text or robust regex helpers.
- Keep English strings only as regex fallbacks, API enum values, or test data.
- Scope request/status checks through helper functions where practical.
- Use real agreement proposal file input IDs.
- Restore schema-current `SeedE2eDemoDataCommand.cs` instead of dynamic SQL insert workarounds.
- Add `TestDatabaseManager` compatibility ensure for missing `L1ApplicantParties.ClientAccountId` in older local test databases.
- Preserve original snapshots under `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/`.

Not included:
- production UI changes;
- backend business logic changes;
- SMTP/email changes;
- EF mapping changes;
- migrations;
- generated OpenAPI/types.

Apply from repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\playwright-locator-cleanup-v1.4.zip" -DestinationPath "." -Force
```

Validation to run locally:

```powershell
dotnet build .\EnergyManagement.sln
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj

dotnet run --project .\EnergyManagement.Tools -- reset-test-db
dotnet run --project .\EnergyManagement.Tools -- seed-e2e-demo-data

npx playwright test tests/e2e/applicant-party --project=chromium --reporter=list
npx playwright test tests/e2e/applicant-parties --project=chromium --reporter=list
npx playwright test tests/e2e/requests --project=chromium --reporter=list
npx playwright test tests/e2e/employee --project=chromium --reporter=list
npx playwright test tests/e2e/agreement-exchange --project=chromium --reporter=list

npm.cmd run test:e2e
npm.cmd run screenshots:vkr
```

Before commit, exclude run artifacts:

```text
EnergyManagement.Server/App_Data/Documents/agreement-proposals/*.pdf
test-results/
playwright-report/
```

Commit `planning/thesis/assets/screenshots/*.png` only if intentionally adding VKR assets.
