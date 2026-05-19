User reported remaining E2E failures after locator cleanup:
- agreement-exchange and employee review fail in `seed-e2e-demo-data` with `Invalid column name 'ClientAccountId'` at `SeedE2eDemoDataCommand.cs:line 159`;
- applicant-party and my-requests tests are now passing;
- screenshots client test passes, employee/agreement screenshot fails due to the same seed error.
