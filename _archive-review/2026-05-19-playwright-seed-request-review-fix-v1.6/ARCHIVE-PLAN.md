# Archive Plan — Playwright seed request review fix v1.6

Purpose: fix remaining Playwright failures caused by `seed-e2e-demo-data` referencing a non-existent `ClientAccountId` column on `dbo.L1RequestReviews`.

Scope:
- `EnergyManagement.Tools/TestDatabase/SeedE2eDemoDataCommand.cs`

Out of scope:
- production UI
- business logic
- SMTP/email
- EF mappings
- migrations
- generated OpenAPI/types
- `TestDatabaseManager` changes
