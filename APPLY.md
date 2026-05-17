# Apply L2-AGR-EXCH-LIST-001.client — Agreement Exchange List Pages

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-list-001-client-shared-agreement-exchange-list-pages.zip" -DestinationPath "." -Force
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
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/entities/agreement-exchange/api/listAgreementExchanges.test.ts src/entities/agreement-exchange/model/useAgreementExchangeListQuery.test.tsx src/widgets/agreement-exchange-list/AgreementExchangeList.test.tsx src/pages/agreements/my/ClientAgreementExchangesPage.test.tsx src/pages/employee/agreements/dashboard/EmployeeAgreementExchangesDashboardPage.test.tsx
```
