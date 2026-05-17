# SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal

This archive implements the server-side start agreement exchange slice.

## Included

- `POST /api/employee/requests/{requestId}/agreement-exchange/start`
- Employee-only + CSRF-protected endpoint
- `StartAgreementExchangeDto` + validator
- `StartAgreementExchangeByEmployeeAsync(...)` application service method
- Duplicate exchange guard by `requestId`
- `AgreementProposalExchange.StartByEmployee(...)` persistence path through repository
- `ClientRequest.ClientAccountId` persistence mapping and migration so loaded requests can provide owner client id to domain
- Integration tests for auth/CSRF/validation/lifecycle/success/duplicate

## Not included

- Generated OpenAPI/types
- Client UI
- Counter-proposal/send/list/details/accept/final-refuse changes
- ResponsibleEmployeeId guard
- Per-command status enum

## Apply

```powershell
cd C:\enman\enman
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-start-agreement-exchange-impl.zip" -DestinationPath "." -Force
```

## Checks

```powershell
dotnet build .\Domain.EnergyManagement\Domain.EnergyManagement.csproj
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

## Migration / generated API

```powershell
dotnet ef database update `
  --project .\EnergyManagement.Server\EnergyManagement.Server.csproj `
  --startup-project .\EnergyManagement.Server\EnergyManagement.Server.csproj `
  --context L1DbContext

dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
dotnet run --project .\EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check
npm.cmd run generate:api
npm.cmd run check:api
```
