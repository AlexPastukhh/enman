# MANIFEST — L2-AGR-EXCH-LIST-001.client — Shared Agreement Exchange List Pages

## Summary

Client-only implementation for shared Agreement Exchange list pages:

- one shared entity API wrapper/query/model;
- one shared list widget;
- separate Client and Employee page shells;
- shared endpoint direction: `GET /api/agreement-exchanges`;
- no actor-specific client/employee API wrappers while response shape is shared;
- no command buttons/mutations in list first pass.

## Added files

```text
energymanagement.client/src/entities/agreement-exchange/api/agreementExchangeApiTypes.ts
energymanagement.client/src/entities/agreement-exchange/api/listAgreementExchanges.ts
energymanagement.client/src/entities/agreement-exchange/api/listAgreementExchanges.test.ts
energymanagement.client/src/entities/agreement-exchange/model/agreementExchangeQueryKeys.ts
energymanagement.client/src/entities/agreement-exchange/model/agreementExchangeTypes.ts
energymanagement.client/src/entities/agreement-exchange/model/useAgreementExchangeListQuery.ts
energymanagement.client/src/entities/agreement-exchange/model/useAgreementExchangeListQuery.test.tsx
energymanagement.client/src/widgets/agreement-exchange-list/AgreementExchangeList.tsx
energymanagement.client/src/widgets/agreement-exchange-list/AgreementExchangeList.test.tsx
energymanagement.client/src/widgets/agreement-exchange-list/AgreementExchangeListEmptyState.tsx
energymanagement.client/src/widgets/agreement-exchange-list/AgreementExchangeRow.tsx
energymanagement.client/src/widgets/agreement-exchange-list/agreementExchangeList.css
energymanagement.client/src/widgets/agreement-exchange-list/agreementExchangeListConst.ts
energymanagement.client/src/widgets/agreement-exchange-list/formatAgreementExchangeList.ts
energymanagement.client/src/pages/agreements/my/ClientAgreementExchangesPage.tsx
energymanagement.client/src/pages/agreements/my/ClientAgreementExchangesPage.test.tsx
energymanagement.client/src/pages/agreements/my/clientAgreementExchangesPage.css
energymanagement.client/src/pages/agreements/my/clientAgreementExchangesPageConst.ts
energymanagement.client/src/pages/employee/agreements/dashboard/EmployeeAgreementExchangesDashboardPage.tsx
energymanagement.client/src/pages/employee/agreements/dashboard/EmployeeAgreementExchangesDashboardPage.test.tsx
energymanagement.client/src/pages/employee/agreements/dashboard/employeeAgreementExchangesDashboardPage.css
energymanagement.client/src/pages/employee/agreements/dashboard/employeeAgreementExchangesDashboardPageConst.ts
```

## Replaced files

```text
energymanagement.client/src/app/router/router.tsx
energymanagement.client/src/shared/config/clientRoutes.ts
```

## Deleted files

```text
none
```

## Generated artifacts

```text
none changed
```

The uploaded project does not expose generated OpenAPI types for `GET /api/agreement-exchanges`, so this client implementation uses local first-pass DTO types in `entities/agreement-exchange/api/agreementExchangeApiTypes.ts`. After server/OpenAPI generation lands, replace those local DTO definitions with generated aliases.

## Tests changed

```text
energymanagement.client/src/entities/agreement-exchange/api/listAgreementExchanges.test.ts
energymanagement.client/src/entities/agreement-exchange/model/useAgreementExchangeListQuery.test.tsx
energymanagement.client/src/widgets/agreement-exchange-list/AgreementExchangeList.test.tsx
energymanagement.client/src/pages/agreements/my/ClientAgreementExchangesPage.test.tsx
energymanagement.client/src/pages/employee/agreements/dashboard/EmployeeAgreementExchangesDashboardPage.test.tsx
```

## Commands run and results

```text
npm --prefix ./energymanagement.client install
→ success; npm reported existing audit vulnerabilities

targeted changed tests
→ success: 5 test files passed, 9 tests passed

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

```text
- no server/backend changes
- no Domain.EnergyManagement changes
- no planning docs changes
- no database/migration changes
- no generated artifact edits
- no agreement exchange command implementation
- no full proposal history/details implementation
- no request details DTO merge
- no actor-specific client/employee API wrappers
- no business-specific wrapper under shared/api
- no local CSRF mechanics
- no unrelated cleanup
- no GitHub write
```

## Risks / handoff notes

```text
- Backend/OpenAPI contract for GET /api/agreement-exchanges is not present in the uploaded snapshot.
- The endpoint path is implemented according to the current planning decision; update the wrapper if generated OpenAPI uses a different route.
- Local DTO definitions should be replaced by generated OpenAPI aliases once server generation lands.
- Routes added for list pages:
  /agreements
  /employee/agreements
- Detail href helpers are added for future pages:
  /agreements/:exchangeId
  /employee/agreements/:exchangeId
```
