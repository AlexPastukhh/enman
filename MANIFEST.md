# L2-AGR-EXCH-FINAL-REFUSE-001.client — Employee Final Refuse Agreement Exchange

Client-only implementation archive.

## Scope

- Employee-only final-refuse action from Employee agreement exchange details page.
- Feature-owned API wrapper for `POST /api/agreement-exchanges/{exchangeId}/final-refuse`.
- Nullable/optional reason body support.
- Empty reason is allowed; whitespace-only provided reason is rejected by client validation.
- Success response handled as `204 No Content` / `Promise<void>`.
- Details/list query invalidation after success/error.
- Related request details invalidation only when requestId is passed from the details read model.
- No Client final refusal.
- No accept/counter-proposal/start-exchange behavior.
- No shared/api business wrapper.

## Files

```text
energymanagement.client/src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.ts
energymanagement.client/src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.test.ts
energymanagement.client/src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchangeApiTypes.ts
energymanagement.client/src/features/agreement-exchange/final-refuse/model/finalRefuseAgreementExchangeAvailability.ts
energymanagement.client/src/features/agreement-exchange/final-refuse/model/finalRefuseAgreementExchangeAvailability.test.ts
energymanagement.client/src/features/agreement-exchange/final-refuse/model/useFinalRefuseAgreementExchangeMutation.ts
energymanagement.client/src/features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.tsx
energymanagement.client/src/features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.test.tsx
energymanagement.client/src/features/agreement-exchange/final-refuse/ui/finalRefuseAgreementExchangeForm.css
energymanagement.client/src/features/agreement-exchange/final-refuse/ui/finalRefuseAgreementExchangeFormConst.ts
energymanagement.client/src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx
energymanagement.client/src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
```

## Verification run in sandbox

```text
npm --prefix ./energymanagement.client install
npm --prefix ./energymanagement.client run test -- --run --reporter=verbose src/features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.test.ts src/features/agreement-exchange/final-refuse/model/finalRefuseAgreementExchangeAvailability.test.ts src/features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.test.tsx src/pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.test.tsx
npm --prefix ./energymanagement.client run build
```

Targeted tests passed: 4 files, 14 tests.
Build passed; Vite printed an existing chunk-size warning.

Lint was also checked and failed on existing `react-refresh/only-export-components` errors outside this slice:

```text
src/Tests/ComponentTest/TestClasses/TestSetup.tsx
src/app/router/router.tsx
src/entities/session/model/SessionProvider.tsx
src/shared/errors/pageErrorContext.tsx
```

## Generated artifacts

No generated OpenAPI artifacts are included.
After the backend endpoint is added/generated, run the repo generation/check commands and include generated changes if stale:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```
