# L2 Domain Classes Implementation Archive

Merge-ready archive with repo-relative paths. It implements target L2 domain classes from `planning/tables/domain-drafts/domain-draft-02.md` without changing planning docs or server API/client code.

## Included files

Added:

```text
Domain.EnergyManagement/L1/Employees/Employee.cs
Domain.EnergyManagement/L1/Requests/RequestReview.cs
Domain.EnergyManagement/L1/Requests/RequestReviewStatus.cs
Domain.EnergyManagement/L1/AgreementProposals/AgreementExchangeStatus.cs
Domain.EnergyManagement/L1/AgreementProposals/AgreementProposal.cs
Domain.EnergyManagement/L1/AgreementProposals/AgreementProposalAuthor.cs
Domain.EnergyManagement/L1/AgreementProposals/AgreementProposalExchange.cs
Domain.EnergyManagement/L1/AgreementProposals/AgreementProposalSender.cs
Domain.EnergyManagement/L1/AgreementProposals/AgreementProposalState.cs
Domain.EnergyManagement/L1/AgreementProposals/AgreementProposalVersion.cs
Domain.EnergyManagement/L1/AgreementProposals/AgreementDocumentRef.cs
Domain.EnergyManagement/L1/AgreementProposals/ProposalComment.cs
Domain.EnergyManagement/L1/AgreementProposals/FinalRefusalReason.cs
```

Replaced:

```text
Domain.EnergyManagement/Common/Error.cs
Domain.EnergyManagement/L1/Accounts/AccountRole.cs
Domain.EnergyManagement/L1/Requests/ConnectionRequest.cs
Domain.EnergyManagement/L1/Requests/RequestStatus.cs
```

## Compatibility notes

- Existing `EmployeeRef` and `ReviewDecisionRecord` files are not removed.
- Existing `ConnectionRequest.Approve(EmployeeRef)` / `Reject(EmployeeRef, ...)` methods are kept for current L1 behavior compatibility.
- New L2 review API is additive: `StartReview(Employee, ...)`, `ApproveReview(Employee, ...)`, `RejectReview(Employee, ...)`.
- `ConnectionRequest.Review` is marked `[NotMapped]` to avoid accidentally changing current EF persistence mapping in this domain-only package.
- No server DbContext mapping, migrations, API endpoints, OpenAPI artifacts, client code, docs, or tests are included.
