# Merge Risk Report — playwright-locator-cleanup-v1.5

Risk: low-to-medium.

Reasons:

- touches Playwright tests/helpers and test database compatibility utility only;
- TestDatabaseManager dynamic SQL affects test database setup, not production runtime;
- no migrations, EF mapping, generated contracts, SMTP/email, or business logic are changed.

Watch points:

- run `dotnet run --project .\EnergyManagement.Tools -- reset-test-db` and `seed-e2e-demo-data` before E2E;
- if status assertions still fail, inspect request card text rather than global page text;
- avoid committing `test-results/`, `playwright-report/`, and generated agreement proposal PDFs.
