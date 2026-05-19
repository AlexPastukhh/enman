# Playwright seed request review fix v1.6

This incremental archive fixes the remaining E2E seed failure:

`Invalid column name 'ClientAccountId'` in `SeedE2eDemoDataCommand.cs`.

The failing column reference was on `dbo.L1RequestReviews`. EF migrations/model snapshot do not define `ClientAccountId` for that table, so the seed insert now uses only mapped columns.
