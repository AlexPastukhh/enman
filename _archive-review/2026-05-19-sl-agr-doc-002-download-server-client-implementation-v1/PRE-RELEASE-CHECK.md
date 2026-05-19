# Pre-release check

## Scope check

Included runtime files:

- `EnergyManagement.Server/L1/Controllers/AgreementExchangesController.cs`
- `Tests.EnergyManagement/Integration/L1/AgreementExchanges/AgreementProposalDocumentDownloadIntegrationTests.cs`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementActiveProposalPanel.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementProposalHistory.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementDocumentRefList.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/buildAgreementProposalDocumentDownloadPath.ts`
- `energymanagement.client/src/widgets/agreement-exchange-details/buildAgreementProposalDocumentDownloadPath.test.ts`
- `energymanagement.client/src/widgets/agreement-exchange-details/agreementExchangeDetailsConst.ts`
- `energymanagement.client/src/widgets/agreement-exchange-details/agreementExchangeDetails.css`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementExchangeDetailsView.test.tsx`

Not included:

- `Domain.EnergyManagement/`
- `planning/`
- `Shared/openapi.json`
- `energymanagement.client/src/shared/api/generated/openapi-types.ts`
- migrations

## Security check

The download route is context-bound:

```http
GET /api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download
```

The server does not expose a raw `storageKey` route. It loads agreement exchange details through the existing Client/Employee details query, finds `proposalId` inside the exchange, and only then calls `IDocumentStorage.OpenReadAsync(storageKey)`.

## Manual checks required after applying

```powershell
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj --filter AgreementProposalDocumentDownload
npm.cmd --prefix .\energymanagement.client run test -- --run AgreementExchangeDetailsView buildAgreementProposalDocumentDownloadPath
dotnet build .\EnergyManagement.sln
npm.cmd --prefix .\energymanagement.client run build
```

I could not execute dotnet/npm tests in this sandbox.
