# Merge Risk Report: playwright-locator-cleanup-v1.4

Review folder: `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/`

## Summary

This archive replaces Playwright E2E files and test database tooling files. It includes original snapshots for every replaced file.

## Main risks

1. Locator drift may remain if current UI text differs locally.
2. Some selectors still intentionally use regex fallback strings; these are not exact stale locators.
3. `SeedE2eDemoDataCommand.cs` is restored to schema-current SQL. `TestDatabaseManager.cs` is responsible for ensuring old local test databases have `L1ApplicantParties.ClientAccountId` before seeding.
4. Playwright must still be run locally because this environment cannot start the .NET/Vite/LocalDB stack.

## Review instructions

1. Compare replacements with originals under `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/`.
2. Run focused Playwright suites first, then full `npm.cmd run test:e2e`.
3. Check `git status --short` before commit and exclude runtime artifacts such as `test-results/`, `playwright-report/`, and generated agreement proposal PDFs.

## Expected remaining English strings

Safe remaining English strings may include:

- API enum values such as `InReview` in `selectOption`.
- Test data such as `Client E2E counter-proposal.`.
- Regex fallback alternatives in test helper locators.

These are not exact stale UI locators.
