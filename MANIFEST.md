# L2-AGR-EXCH-SEND-PROPOSAL-001.client — Send Agreement Proposal Version

## Added files

- `energymanagement.client/src/features/agreement-exchange/send-proposal/api/sendAgreementProposal.ts`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/api/sendAgreementProposalApiTypes.ts`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/api/sendAgreementProposal.test.ts`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/model/useSendAgreementProposalMutation.ts`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/model/sendAgreementProposalAvailability.ts`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/model/sendAgreementProposalAvailability.test.ts`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm.tsx`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm.test.tsx`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/ui/sendAgreementProposalForm.css`
- `energymanagement.client/src/features/agreement-exchange/send-proposal/ui/sendAgreementProposalFormConst.ts`

## Replaced files

- `energymanagement.client/src/pages/agreements/details/ClientAgreementExchangeDetailsPage.tsx`
- `energymanagement.client/src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx`
- `energymanagement.client/src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx`
- `energymanagement.client/src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx`

## Deleted files

None.

## Generated artifacts

Unchanged.

The uploaded snapshot does not expose generated OpenAPI types for the send proposal command. This archive uses a local first-pass feature DTO in `sendAgreementProposalApiTypes.ts`. Replace it with generated aliases after `SL-AGR-EXCH-002` is implemented/generated.

## Tests changed

- `sendAgreementProposal.test.ts`
- `sendAgreementProposalAvailability.test.ts`
- `SendAgreementProposalForm.test.tsx`
- updated Client agreement exchange details page test
- updated Employee agreement exchange details page test

## Commands run and results

```text
npm --prefix ./energymanagement.client install
→ success; npm reported existing audit vulnerabilities

npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/features/agreement-exchange/send-proposal/api/sendAgreementProposal.test.ts src/features/agreement-exchange/send-proposal/model/sendAgreementProposalAvailability.test.ts src/features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm.test.tsx src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
→ success: 5 test files passed, 14 tests passed

npm --prefix ./energymanagement.client run build
→ success; Vite printed existing chunk-size warning

npm --prefix ./energymanagement.client run lint
→ failed on existing react-refresh/only-export-components errors outside this slice:
  - src/Tests/ComponentTest/TestClasses/TestSetup.tsx
  - src/app/router/router.tsx
  - src/entities/session/model/SessionProvider.tsx
  - src/shared/errors/pageErrorContext.tsx
```

## Non-goals respected

- no server/backend changes
- no Domain.EnergyManagement changes
- no planning docs changes
- no database/migration changes
- no generated artifact manual edits
- no accept active proposal implementation
- no final refuse implementation
- no start exchange implementation
- no file upload/storage implementation
- no actor-specific client/employee send proposal wrappers
- no shared/api business wrapper
- no GitHub write

## Risks / handoff notes

- Endpoint path is first-pass target: `POST /api/agreement-exchanges/{exchangeId}/proposals`.
- Request payload is local first-pass and includes `document` reference plus optional `comment`.
- When server/OpenAPI is ready, replace local feature DTOs with generated aliases from `shared/api/generated/openapi-types.ts`.
- Command security remains server-side; UI availability is display-only and can be stale.
