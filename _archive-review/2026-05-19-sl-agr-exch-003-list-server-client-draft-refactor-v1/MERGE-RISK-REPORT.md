# Merge Risk Report

Archive: `sl-agr-exch-003-list-server-client-draft-refactor-v1`

## Summary

Two paired draft files are replaced.

Runtime implementation, tests, deep UI refactor and redirect/page flow are not changed.

## Server checks

```text
- endpoint remains GET /api/agreement-exchanges;
- success response remains 200 OK;
- Client branch filters by ClientAccountId;
- Employee branch does not use ResponsibleEmployeeId;
- full proposal history remains out of list;
- no AgreementExchangeActor abstraction is introduced;
- Behavior-to-Test Trace was added;
- Implementation Sync Status clearly says implementation was not rechecked.
```

## Client checks

```text
- shared entity wrapper remains entities/agreement-exchange/api/listAgreementExchanges.ts;
- no shared/api/agreementExchangeApi.ts business wrapper;
- no listClientAgreementExchanges / listEmployeeAgreementExchanges first pass;
- Client and Employee page shells remain separate;
- shared query/model/list widget remains;
- Visual Client Implementation Flow uses from / needed to / visual where applicable;
- Behavior-to-Test Trace was added;
- Implementation Sync Status clearly says implementation was not rechecked.
```
