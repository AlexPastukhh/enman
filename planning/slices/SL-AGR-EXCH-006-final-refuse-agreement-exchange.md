# SL-AGR-EXCH-006 — Final Refuse Agreement Exchange

Status: planned slice boundary / full implementation draft pending  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: backend/API command slice + future client command sidecar  
Primary purpose: Employee finally refuses agreement exchange and request receives exchange-failed result

## 1. Scope Boundary

```text
Employee opens active agreement exchange
  -> Employee chooses final refusal
  -> exchange.FinalRefuseProposal(...) marks exchange FinallyRefused
  -> request.MarkAgreementExchangeFailed(...) marks request result through separate Request domain method
  -> both aggregates are saved by application service orchestration
```

## 2. Domain Direction

```text
AgreementProposalExchange and Request are separate aggregates.

Exchange does:
  FinalRefuseProposal(...)

Request does:
  MarkAgreementExchangeFailed(...)

Application service orchestrates:
  exchange.FinalRefuseProposal(...)
  request.MarkAgreementExchangeFailed(...)
  save
```

The exchange must not mutate Request directly. Request must not hold navigation to Exchange.

## 3. Direct Exchange State

```text
FinalRefusedByEmployeeId?
FinalRefusedAt?
FinalRefusalReason?
```

No separate `AgreementFinalRefusal` class/entity is needed for the current direction.

Final refusal does not create a new proposal version.

## 4. Out of Scope

```text
- initial exchange creation -> SL-AGR-EXCH-001;
- counter-proposal version creation -> SL-AGR-EXCH-002;
- exchange list read -> SL-AGR-EXCH-003;
- exchange details read -> SL-AGR-EXCH-004;
- accept proposal command -> SL-AGR-EXCH-005;
- request review approval -> SL-EMP-REQ-004.
```

## 5. Implementation Checklist

```text
[ ] write full server draft before implementation
[ ] confirm Employee-only final refusal route
[ ] confirm final refusal reason policy
[ ] confirm exchange/request cross-aggregate orchestration
[ ] confirm request status/result after exchange failure
[ ] confirm Error/ProblemDetails mapping
[ ] confirm generated OpenAPI/types workflow
```
