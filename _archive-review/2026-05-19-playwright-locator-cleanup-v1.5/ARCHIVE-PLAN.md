# Archive Plan — playwright-locator-cleanup-v1.5

Purpose: incremental fix after v1.4 based on local Playwright failure logs.

Target issues:

- seed command fails with `Invalid column name 'ClientAccountId'`;
- applicant party summary verification status mismatch;
- My Requests status locator hits hidden filter option.

Approach:

- keep seed command schema-current;
- move SQL Server parse-time compatibility into TestDatabaseManager dynamic DDL/DML only;
- scope request status assertions to visible request cards;
- allow applicant summary to match current raw status or future localized status.
