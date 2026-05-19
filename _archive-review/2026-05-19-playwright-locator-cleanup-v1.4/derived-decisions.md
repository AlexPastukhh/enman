# Derived Decisions

1. Supersede `playwright-locator-cleanup-v1.3.zip` with `playwright-locator-cleanup-v1.4.zip` because v1.4 includes original-file backups.
2. Preserve every replaced file under `_archive-review/2026-05-19-playwright-locator-cleanup-v1.4/original-files/`.
3. Keep the cleanup scoped to Playwright E2E and test database tooling.
4. Do not include production UI/backend business/SMTP/generated/migration changes.
5. Require local focused Playwright and full E2E validation after applying.
