# SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal

Archive: `sl-agr-exch-005-client-accept-active-agreement-proposal-v27.zip`

## Scope

Implements the client-only accept command for an active employee agreement proposal:

```http
POST /api/agreement-exchanges/{exchangeId}/accept
→ 204 No Content
```

## Files

```text
EnergyManagement.Server/L1/Application/Abstractions/IAgreementExchangeApplicationService.cs
EnergyManagement.Server/L1/Application/Services/AgreementExchangeApplicationService.cs
EnergyManagement.Server/L1/Controllers/AgreementExchangesController.cs
Tests.EnergyManagement/Integration/L1/AgreementExchanges/AgreementProposalAcceptIntegrationTests.cs
APPLY.md
MANIFEST.md
```

## Notes

- Uses existing domain behavior: `AgreementProposalExchange.ClientAcceptActiveProposal(client, now)`.
- Does not add a command status enum.
- Does not manually mutate exchange/proposal state in the application layer.
- Does not add an employee accept path.
- Does not include generated OpenAPI/types; regenerate locally with repo tools.
