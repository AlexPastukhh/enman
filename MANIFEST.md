# MANIFEST — Employee TPH Runtime/Test Database Fix

Archive: `employee-tph-runtime-fix-v15.zip`

Scope:
- finish runtime/test-database cleanup after Employee was modeled as Account TPH subtype;
- keep Employee identity as `Account.Id == Employee.Id`;
- remove legacy test DB review-column provisioning;
- make test DB reset safe when AgreementProposal tables exist.

Changed files:
- `EnergyManagement.Testing/TestDatabase/TestDatabaseManager.cs`

Changes:
- `ClearAsync` now deletes `L1AgreementProposals` and `L1AgreementProposalExchanges` before request/account tables.
- Removed legacy provisioning of `ReviewDecision`, `ReviewDecidedAt`, `ReviewReviewerId`, `ReviewRejectionReason` columns on `L1ClientRequests`.
- Existing test databases now ensure `L1AgreementProposalExchanges` and `L1AgreementProposals` tables exist when `L1Accounts` already exists.
- Existing test databases still ensure `L1RequestReviews` and Employee TPH full-name columns exist.

Not changed:
- no docs/planning changes;
- no client UI changes;
- no OpenAPI/generated artifacts;
- no API contract changes;
- no StartReview response contract changes;
- no approve/reject implementation;
- no migrations.
