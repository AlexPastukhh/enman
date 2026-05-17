# L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages

## Summary

Client-only implementation archive for the shared Agreement Exchange details read sidecar.

This archive implements one shared entity details API/query/model and one shared details widget, with separate Client and Employee details page shells:

- Client route/page shell: `/agreements/:exchangeId`
- Employee route/page shell: `/employee/agreements/:exchangeId`
- Shared read wrapper direction: `GET /api/agreement-exchanges/{exchangeId}`

## Added files

- `energymanagement.client/src/entities/agreement-exchange/api/getAgreementExchangeDetails.ts`
- `energymanagement.client/src/entities/agreement-exchange/api/getAgreementExchangeDetails.test.ts`
- `energymanagement.client/src/entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.ts`
- `energymanagement.client/src/entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.test.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementActiveProposalPanel.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementDocumentRefList.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementExchangeDetailsView.test.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementExchangeRequestSummary.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementExchangeStatusPanel.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/AgreementProposalHistory.tsx`
- `energymanagement.client/src/widgets/agreement-exchange-details/agreementExchangeDetails.css`
- `energymanagement.client/src/widgets/agreement-exchange-details/agreementExchangeDetailsConst.ts`
- `energymanagement.client/src/widgets/agreement-exchange-details/formatAgreementExchangeDetails.ts`
- `energymanagement.client/src/pages/agreements/details/ClientAgreementExchangeDetailsPage.tsx`
- `energymanagement.client/src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx`
- `energymanagement.client/src/pages/agreements/details/clientAgreementExchangeDetailsPage.css`
- `energymanagement.client/src/pages/agreements/details/clientAgreementExchangeDetailsPageConst.ts`
- `energymanagement.client/src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx`
- `energymanagement.client/src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx`
- `energymanagement.client/src/pages/employee/agreements/details/employeeAgreementExchangeDetailsPage.css`
- `energymanagement.client/src/pages/employee/agreements/details/employeeAgreementExchangeDetailsPageConst.ts`

## Replaced files

- `energymanagement.client/src/app/router/router.tsx`
- `energymanagement.client/src/shared/config/clientRoutes.ts`
- `energymanagement.client/src/entities/agreement-exchange/api/agreementExchangeApiTypes.ts`
- `energymanagement.client/src/entities/agreement-exchange/model/agreementExchangeTypes.ts`
- `energymanagement.client/src/entities/agreement-exchange/model/agreementExchangeQueryKeys.ts`
- `energymanagement.client/src/widgets/agreement-exchange-list/formatAgreementExchangeList.ts`

## Deleted files

None.

## Generated artifacts

Unchanged.

The uploaded snapshot does not include generated OpenAPI types for `GET /api/agreement-exchanges/{exchangeId}`. Details DTO types are local first-pass types in `entities/agreement-exchange/api/agreementExchangeApiTypes.ts` and should be replaced with generated aliases after the server endpoint is implemented/generated.

## Tests changed

- Added API wrapper test for `getAgreementExchangeDetails()`.
- Added query hook tests for `useAgreementExchangeDetailsQuery()`.
- Added shared details widget tests.
- Added Client details page shell tests.
- Added Employee details page shell tests.

## Commands run and results

```text
npm --prefix ./energymanagement.client install
→ success; npm reported existing audit vulnerabilities

npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/entities/agreement-exchange/api/getAgreementExchangeDetails.test.ts src/entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.test.tsx src/widgets/agreement-exchange-details/AgreementExchangeDetailsView.test.tsx src/pages/agreements/details/ClientAgreementExchangeDetailsPage.test.tsx src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
→ success: 5 test files passed, 9 tests passed

npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/entities/agreement-exchange/api/listAgreementExchanges.test.ts src/entities/agreement-exchange/model/useAgreementExchangeListQuery.test.tsx src/widgets/agreement-exchange-list/AgreementExchangeList.test.tsx src/pages/agreements/my/ClientAgreementExchangesPage.test.tsx src/pages/employee/agreements/dashboard/EmployeeAgreementExchangesDashboardPage.test.tsx
→ success: 5 test files passed, 10 tests passed

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

- No server/backend changes.
- No Domain.EnergyManagement changes.
- No planning docs changes.
- No generated artifact manual edits.
- No command implementation.
- No counter-proposal / accept / final-refuse UI.
- No file download / document bytes implementation.
- No actor-specific details API wrappers while response shape is shared.
- No `shared/api` business wrapper.
- No unrelated cleanup.
- No GitHub write.

## Risks / handoff notes

- Backend/OpenAPI details endpoint may still be missing or stale. Run OpenAPI generation after server implementation.
- Replace local DTO definitions with generated aliases when `AgreementExchangeDetailsResponseDto` and related DTOs exist in `shared/api/generated/openapi-types.ts`.
- This sidecar only provides optional action slots; command sidecars own buttons/forms/mutations.
