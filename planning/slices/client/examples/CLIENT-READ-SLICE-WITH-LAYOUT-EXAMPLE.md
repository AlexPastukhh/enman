# Example — Client Read Slice With Layout

Status: example only

## Visual Client Implementation Flow Example

```text
[Client Agreement Exchanges Page Shell]
pages/agreements/my/ClientAgreementExchangesPage.tsx

Lives here:
  ClientAgreementExchangesPage

Owns:
  client route shell;
  page title;
  client empty/error/loading branches;
  details href builder for client route.

Uses:
  useAgreementExchangeListQuery()
    from: entities/agreement-exchange/model/useAgreementExchangeListQuery.ts
    needed to: load the shared agreement exchange list read model.

  AgreementExchangeList
    from: widgets/agreement-exchange-list/AgreementExchangeList.tsx
    needed to: render the shared exchange list and row navigation.
    visual: main list block placed below page header; parent controls page spacing and empty-state copy.

Does not own:
  server filtering;
  row internal layout;
  command actions;
  employee route shell.
```

## Styling / CSS Ownership Example

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Page container | page | `pages/agreements/my/clientAgreementExchangesPage.css` | page spacing and title area only |
| List block | widget | `widgets/agreement-exchange-list/agreementExchangeList.css` | list rows/cards internals |
| Status display | entity/widget | relevant entity/widget CSS | status rendering |
| Global tokens | global | `styles/tokens.css` | colors/spacing only |
