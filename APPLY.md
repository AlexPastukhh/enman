# Apply

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-start-001-client.zip" -DestinationPath "." -Force
```

# Verify

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

Targeted tests:

```powershell
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/features/agreement-exchange/start-exchange/api/startAgreementExchange.test.ts src/features/agreement-exchange/start-exchange/model/startAgreementExchangeAvailability.test.ts src/features/agreement-exchange/start-exchange/ui/StartAgreementExchangeForm.test.tsx src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx
```

# OpenAPI note

The uploaded snapshot does not expose `POST /api/agreement-exchanges` in generated OpenAPI yet. After `SL-AGR-EXCH-001` backend/OpenAPI is finalized, replace local first-pass DTOs in `startAgreementExchangeApiTypes.ts` with generated aliases and include generated artifacts if they change:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```
