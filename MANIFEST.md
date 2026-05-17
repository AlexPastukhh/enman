# L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal

Client-only implementation archive.

## Scope

- Adds Client accept agreement proposal feature under `features/agreement-exchange/accept-proposal`.
- Wires Accept action into Client agreement exchange details action area only.
- Does not wire Employee accept.
- Uses feature-owned command API wrapper for `POST /api/agreement-exchanges/{exchangeId}/accept`.
- Sends no request body and expects `204 No Content`.
- Invalidates agreement exchange details/list queries after success/error.
- Does not create proposal version, counter-proposal, final refusal, or shared API business wrapper.

## Files

```text
energymanagement.client/src/features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.ts
energymanagement.client/src/features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.test.ts
energymanagement.client/src/features/agreement-exchange/accept-proposal/model/acceptAgreementProposalAvailability.ts
energymanagement.client/src/features/agreement-exchange/accept-proposal/model/acceptAgreementProposalAvailability.test.ts
energymanagement.client/src/features/agreement-exchange/accept-proposal/model/useAcceptAgreementProposalMutation.ts
energymanagement.client/src/features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton.tsx
energymanagement.client/src/features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton.test.tsx
energymanagement.client/src/features/agreement-exchange/accept-proposal/ui/acceptAgreementProposalButton.css
energymanagement.client/src/features/agreement-exchange/accept-proposal/ui/acceptAgreementProposalButtonConst.ts
energymanagement.client/src/pages/agreements/details/ClientAgreementExchangeDetailsPage.tsx
energymanagement.client/src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx
```

## Checks run

```text
npm --prefix ./energymanagement.client install
npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.test.ts src/features/agreement-exchange/accept-proposal/model/acceptAgreementProposalAvailability.test.ts src/features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton.test.tsx src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
npm --prefix ./energymanagement.client run build
```

Results:

```text
targeted tests: 5 files passed, 14 tests passed
build: success; Vite emitted existing chunk-size warning
```

## Notes

Generated OpenAPI artifacts are unchanged in this archive. After server/OpenAPI generation for the accept endpoint, run `npm run check:api` locally and include generated artifacts if needed.
