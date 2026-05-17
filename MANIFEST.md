# MANIFEST — SL-EMP-REQ-003 Start Request Review compile fix

Archive: sl-emp-req-003-start-request-review-v12-compile-fix.zip

Scope:
- Fix compile error in `Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs` caused by calling `SqlDataReader.GetDateTimeOffset(string)`.

Changed files:
- Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs

Notes:
- `SqlDataReader.GetDateTimeOffset` accepts an ordinal `int`, not a column name string.
- The fix uses `reader.GetOrdinal("StartedAt")` and `reader.GetOrdinal("CompletedAt")`.
- No docs/planning/client/generated/migrations/domain files changed.
