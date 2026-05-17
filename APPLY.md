# Apply

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-final-refuse-001-client.zip" -DestinationPath "." -Force
```

Then verify:

```powershell
npm install
npm --prefix .\energymanagement.client install
npm run check:api
npm --prefix .\energymanagement.client run build
npm --prefix .\energymanagement.client run test -- --run
```

Targeted tests:

```powershell
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.test.ts src/features/agreement-exchange/final-refuse/model/finalRefuseAgreementExchangeAvailability.test.ts src/features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.test.tsx src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
```
