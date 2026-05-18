# Example — Client Command Form Slice With CSS

Status: example only

## Visual Client Implementation Flow Example

```text
[Employee Agreement Details Page Shell]
pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage.tsx

Lives here:
  EmployeeAgreementExchangeDetailsPage

Owns:
  route param parsing;
  employee page title and back link;
  loading/error/not-found branches;
  composition of details widget and Employee action slot.

Uses:
  useAgreementExchangeDetailsQuery(exchangeId)
    from: entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.ts
    needed to: load details data for this page.

  AgreementExchangeDetailsView
    from: widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
    needed to: render shared read-only exchange details and action slot.
    visual: main details content block in the page content area; owns its internal section layout.

  FinalRefuseAgreementExchangeForm
    from: features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm.tsx
    needed to: provide the Employee final-refuse command in the action slot.
    visual: compact negative-decision form placed in the details action area.

Does not own:
  internal layout of AgreementExchangeDetailsView;
  internal field styling of FinalRefuseAgreementExchangeForm;
  final-refuse endpoint wrapper;
  server authorization/lifecycle rules.
```

## Styling / CSS Ownership Example

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Page layout | page | `pages/employee/agreements/details/*.css` | page spacing and action slot placement |
| Details widget | widget | `widgets/agreement-exchange-details/*.css` | details sections and proposal history layout |
| Command form | feature | `features/agreement-exchange/final-refuse/ui/*.css` | reason field, buttons, validation and command feedback |
| Tokens/base | global | `styles/*.css` | no business-specific styling |
