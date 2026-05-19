# Derived Decisions

- Keep SeedE2eDemoDataCommand simple and schema-current; do not reintroduce dynamic conditional insert workarounds.
- Add dynamic SQL only to TestDatabaseManager compatibility DDL/DML where SQL Server parse-time validation can fail for legacy tables.
- For request status assertions, scope to an article/request card to avoid hidden filter options.
- For applicant party verification status in account summary, accept both current raw API value and localized label until production summary formatting is changed.
