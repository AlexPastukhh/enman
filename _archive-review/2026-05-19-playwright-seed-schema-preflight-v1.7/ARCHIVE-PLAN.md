# Archive plan

Patch `SeedE2eDemoDataCommand.cs` only.

Goal: keep E2E seed schema-current, but add a small preflight that ensures legacy/local test DB columns required by seeded rows exist before the seed SQL batch is compiled.

No domain, production server, UI, SMTP, generated files or migrations are changed.
