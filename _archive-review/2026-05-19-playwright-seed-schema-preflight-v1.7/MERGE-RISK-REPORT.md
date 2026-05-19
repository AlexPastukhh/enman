# Merge risk report

Risk: low. Single tooling file change.

Reason: remaining Playwright failures are caused by `seed-e2e-demo-data` failing with `Invalid column name 'ClientAccountId'`.

This patch adds preflight DDL for test/demo DB only, before the seed batch runs.
