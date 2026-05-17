# L2-AGR-EXCH-START-001.client — Start Agreement Exchange With Initial Employee Proposal

Client-only implementation archive.

## Included files

- `energymanagement.client/src/features/agreement-exchange/start-exchange/api/startAgreementExchange.ts`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/api/startAgreementExchange.test.ts`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/api/startAgreementExchangeApiTypes.ts`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/model/startAgreementExchangeAvailability.ts`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/model/startAgreementExchangeAvailability.test.ts`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/model/useStartAgreementExchangeMutation.ts`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/ui/StartAgreementExchangeForm.tsx`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/ui/StartAgreementExchangeForm.test.tsx`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/ui/startAgreementExchangeForm.css`
- `energymanagement.client/src/features/agreement-exchange/start-exchange/ui/startAgreementExchangeFormConst.ts`
- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.tsx`
- `energymanagement.client/src/pages/employee/requests/details/EmployeeRequestDetailsPage.test.tsx`

## Scope confirmations

- Employee-only first pass.
- Placement is Employee request details action area only.
- Feature-owned command wrapper, no `shared/api` business wrapper.
- Target endpoint direction: `POST /api/agreement-exchanges`.
- Initial proposal DTO is local first pass until generated OpenAPI contract exists.
- Request details and agreement exchange list are invalidated after success/error.
- Navigates to `/employee/agreements/:exchangeId` when response contains `exchangeId`.
- No Client start exchange.
- No send-proposal/counter-proposal inside existing exchange.
- No accept/final-refuse implementation.
