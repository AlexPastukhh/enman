# Derived Decisions

## Preserved decisions

```text
- Employee-only final refusal first pass.
- Client final refusal is out of scope.
- Success returns 204 No Content.
- Reason is optional and nullable.
- Blank/whitespace-only reason is invalid when provided.
- Max reason length is 2000.
- Final refusal does not create proposal versions.
- Exchange becomes FinallyRefused.
- Related request becomes AgreementExchangeFailed.
- Application orchestrates AgreementProposalExchange + ConnectionRequest.
- No ResponsibleEmployeeId guard first pass.
- Client UI visibility is not authorization.
```

## Code-evidence decisions added

```text
- Current controller action name: FinalRefuse.
- Current route name: EmployeeFinalRefuseAgreementExchange.
- Current app service method: EmployeeFinalRefuseAgreementExchangeAsync.
- Current client feature folder: features/agreement-exchange/final-refuse.
- Current client wrapper sends no body when payload is undefined.
- Current mutation invalidates exchange details/list and request details when requestId exists.
```
