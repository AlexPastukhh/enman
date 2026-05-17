# Apply L2-AGR-EXCH-SEND-PROPOSAL-001.client

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-send-proposal-001-client.zip" -DestinationPath "." -Force
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
npm --prefix .\energymanagement.client run test -- --run --reporter=verbose src/features/agreement-exchange/send-proposal/api/sendAgreementProposal.test.ts src/features/agreement-exchange/send-proposal/model/sendAgreementProposalAvailability.test.ts src/features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm.test.tsx src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
```
