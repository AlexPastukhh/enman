# L2 Domain Persistence Cleanup Patch

This archive updates the current project toward the L2 domain model:

- removes the legacy `ReviewDecision` domain enum from effective code;
- persists request-owned `RequestReview` through `L1RequestReviews`;
- maps `AgreementProposalExchange` and owned `AgreementProposal` versions;
- changes request details read compatibility from legacy review decision columns to `RequestReviewStatus`/`CompletedAt`;
- switches `ClientRequestRepository` read projections to Dapper;
- adds a repository for `AgreementProposalExchange`.

No planning/docs/client/generated/OpenAPI/migration files are included.

## Files included

- `Domain.EnergyManagement/L1/Requests/ConnectionRequest.cs`
- `Domain.EnergyManagement/L1/Requests/ReviewDecision.cs` — no-op placeholder so archive extraction neutralizes the old enum if the file existed
- `EnergyManagement.Server/L1/Application/Abstractions/IAgreementProposalExchangeRepository.cs`
- `EnergyManagement.Server/L1/Application/Abstractions/IClientRequestRepository.cs`
- `EnergyManagement.Server/L1/Application/Queries/L1GetMyRequestDetailsHandler.cs`
- `EnergyManagement.Server/L1/Application/Queries/L1GetMyRequestDetailsQuery.cs`
- `EnergyManagement.Server/L1/Persistence/L1DbContext.cs`
- `EnergyManagement.Server/L1/Persistence/Repositories/AgreementProposalExchangeRepository.cs`
- `EnergyManagement.Server/L1/Persistence/Repositories/ClientRequestRepository.cs`
- `EnergyManagement.Server/Program.cs`
- `EnergyManagement.Testing/TestDatabase/TestDatabaseManager.cs`
- `Tests.EnergyManagement/Integration/L1/L1IntegrationTestBase.cs`

## Intentional deletion

After extraction, delete the old compatibility file path entirely if you want the tree to be clean:

```powershell
Remove-Item .\Domain.EnergyManagement\L1\Requests\ReviewDecision.cs -Force
```

The archive also overwrites that file with a no-op placeholder so the `ReviewDecision` type is removed even before deletion.
