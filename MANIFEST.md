# MANIFEST

Archive: enman-agreement-exchange-list-impl.zip
Scope: SL-AGR-EXCH-003 agreement exchange list/read page first-pass implementation plus required ClientAccountId domain/persistence change.

Included replacement/addition files:
- Domain.EnergyManagement/Common/Error.cs
- Domain.EnergyManagement/L1/AgreementProposals/AgreementProposalExchange.cs
- EnergyManagement.Server/L1/Api/L1Dtos.cs
- EnergyManagement.Server/L1/Api/Validation/AgreementExchangeListQueryDtoValidator.cs
- EnergyManagement.Server/L1/Api/Validation/L1FieldNames.cs
- EnergyManagement.Server/L1/Application/Abstractions/IAgreementExchangeReadService.cs
- EnergyManagement.Server/L1/Application/Abstractions/IAgreementProposalExchangeRepository.cs
- EnergyManagement.Server/L1/Application/Services/AgreementExchangeReadService.cs
- EnergyManagement.Server/L1/Controllers/AgreementExchangesController.cs
- EnergyManagement.Server/L1/Persistence/L1DbContext.cs
- EnergyManagement.Server/L1/Persistence/Repositories/AgreementProposalExchangeRepository.cs
- EnergyManagement.Server/Migrations/20260518120000_AddAgreementExchangeClientAccountId.cs
- EnergyManagement.Server/Migrations/20260518120000_AddAgreementExchangeClientAccountId.Designer.cs
- EnergyManagement.Server/Migrations/L1DbContextModelSnapshot.cs
- EnergyManagement.Server/Program.cs
- Tests.EnergyManagement/Domain/AgreementProposals/AgreementProposalExchangeTests.cs
- Tests.EnergyManagement/Integration/L1/AgreementExchanges/AgreementExchangeListIntegrationTests.cs
- energymanagement.client/src/app/router/router.tsx
- energymanagement.client/src/entities/agreement-exchange/**
- energymanagement.client/src/pages/agreement-exchanges/**
- energymanagement.client/src/shared/config/clientRoutes.ts
- energymanagement.client/src/shared/ui/layout/Header.tsx
- energymanagement.client/src/shared/ui/layout/headerConst.ts

Not included:
- generated OpenAPI/types artifacts
- binary file upload/document storage
- start exchange endpoint implementation
- proposal counter-proposal commands
- accept/final-refuse commands
