# Apply L2-AGR-EXCH-DETAILS-001.client archive

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-details-001-client-shared-agreement-exchange-details-pages.zip" -DestinationPath "." -Force
```

Verify:

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

Targeted tests:

```powershell
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/entities/agreement-exchange/api/getAgreementExchangeDetails.test.ts src/entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.test.tsx src/widgets/agreement-exchange-details/AgreementExchangeDetailsView.test.tsx src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
```

OpenAPI/type generation after server endpoint exists:

```powershell
dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json
npm.cmd --prefix energymanagement.client run generate:api-types
npm run check:api
```
