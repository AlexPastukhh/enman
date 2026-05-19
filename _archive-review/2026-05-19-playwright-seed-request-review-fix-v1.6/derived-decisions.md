# Derived Decisions

1. Do not change SMTP/email; SMTP warning is non-blocking.
2. Do not add dynamic SQL fallback to the seed command.
3. Keep seed schema-current: `L1RequestReviews` should be seeded using columns present in the EF model.
4. Remove `ClientAccountId` only from `L1RequestReviews` seed insert; other tables still require `ClientAccountId`.
