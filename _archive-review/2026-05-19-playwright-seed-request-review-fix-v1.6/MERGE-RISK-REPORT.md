# Merge Risk Report

Risk: low.

The seed command inserted `ClientAccountId` into `dbo.L1RequestReviews`, but the EF migration/model snapshot for `L1RequestReviews` does not define this column. This caused `Invalid column name 'ClientAccountId'` during E2E seed.

Change:
- remove `ClientAccountId` from the seeded `L1RequestReviews` insert;
- keep the seeded review linked by `RequestId`.

No runtime/business/schema migration changes are included.
