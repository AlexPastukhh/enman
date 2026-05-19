# playwright-locator-cleanup-v1.5

Incremental follow-up to v1.4.

## Fixes

1. Makes test database compatibility DDL use dynamic execution for statements referencing optional legacy columns. This prevents SQL Server parse-time `Invalid column name 'ClientAccountId'` failures before compatibility columns are added.
2. Fixes applicant-party E2E to accept current account summary rendering (`Unverified`) as well as future localized rendering (`Данные не проверены`).
3. Fixes My Requests status assertions by scoping to the request card instead of matching the hidden status filter `<option>`.

## Boundaries

Does not change production UI/backend/business/email/schema/generated files.
