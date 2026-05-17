# SL-AGR-EXCH-006 — Final Refuse Agreement Exchange

Archive contents are repository-relative and intended to be expanded from the repository root.

## Files

- `EnergyManagement.Server/L1/Api/L1Dtos.cs`
- `EnergyManagement.Server/L1/Api/Validation/L1FieldNames.cs`
- `EnergyManagement.Server/L1/Api/Validation/FinalRefuseAgreementExchangeDtoValidator.cs`
- `EnergyManagement.Server/L1/Application/Abstractions/IAgreementExchangeApplicationService.cs`
- `EnergyManagement.Server/L1/Application/Services/AgreementExchangeApplicationService.cs`
- `EnergyManagement.Server/L1/Controllers/AgreementExchangesController.cs`
- `EnergyManagement.Server/Program.cs`
- `Tests.EnergyManagement/Integration/L1/AgreementExchanges/AgreementExchangeFinalRefusalIntegrationTests.cs`

## Implemented

- Employee-only final refusal endpoint: `POST /api/agreement-exchanges/{exchangeId}/final-refuse`
- CSRF boundary.
- Nullable request body: missing/null body and missing/null reason are allowed.
- Optional `reason` validation: blank/too long values return validation problem.
- Application orchestration over two aggregates:
  - `exchange.FinalRefuseProposal(employee, reason, now)`
  - `request.MarkAgreementExchangeFailed(exchange.Id, now)`
- Single `SaveChangesAsync` after both domain transitions succeed.
- `204 No Content` on success.
- Integration tests with DB state assertions.

## Not included

- OpenAPI/generated TypeScript artifacts.
- Planning/docs updates.
- Client UI.
- Migrations.
