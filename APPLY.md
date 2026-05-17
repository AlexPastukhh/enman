# Apply

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-accept-001-client.zip" -DestinationPath "." -Force
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
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.test.ts src/features/agreement-exchange/accept-proposal/model/acceptAgreementProposalAvailability.test.ts src/features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton.test.tsx src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
```
