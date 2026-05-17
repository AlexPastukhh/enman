# SL-AGR-EXCH-005 — Final Refuse Agreement Exchange

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

## 2. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-005-Q001` | accepted | Is final refusal an Employee action? | Yes, current direction says only Employee can finally refuse. | Client final refusal is not first pass. |
| `SL-AGR-EXCH-005-Q002` | accepted | Does final refusal create proposal version? | No. It updates exchange final refusal state. | Keeps proposal history clean. |
| `SL-AGR-EXCH-005-Q003` | accepted | Does exchange mutate Request directly? | No. Application service orchestrates exchange + request aggregate updates. | Preserves aggregate separation. |
| `SL-AGR-EXCH-005-Q004` | accepted | Is there ResponsibleEmployeeId guard? | No first pass. Any active Employee can service/final-refuse if lifecycle permits. | No assignment model. |
| `SL-AGR-EXCH-005-Q005` | accepted | How is acting Employee recorded? | Direct exchange state: `FinalRefusedByEmployeeId`, `FinalRefusedAt`, optional reason. | Audit without ownership lock. |

## 3. Behavior Coverage

| Behavior item | How slice covers it | Status |
|---|---|---|
| Employee can finally refuse active exchange | command calls `exchange.FinalRefuseProposal(...)` | planned |
| Final refusal marks exchange FinallyRefused | exchange owns final refusal state | planned/current domain direction |
| Request receives agreement exchange failed result | app service calls `request.MarkAgreementExchangeFailed(...)` | planned/current domain direction |
| No proposal version is created | domain updates final refusal state only | planned |
| Exchange does not mutate Request directly | app service orchestrates two aggregates | planned |

## 4. Domain Direction

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

Employee first-pass access:

```text
No ResponsibleEmployeeId guard.
Any active Employee can service/final-refuse if lifecycle allows.
```

## 5. Direct Exchange State

```text
FinalRefusedByEmployeeId?
FinalRefusedAt?
FinalRefusalReason?
```

No separate `AgreementFinalRefusal` class/entity is needed for the current direction.

Final refusal does not create a new proposal version.

## 6. Out of Scope

```text
- initial exchange creation -> SL-AGR-EXCH-001;
- counter-proposal version creation -> SL-AGR-EXCH-002;
- exchange read model -> SL-AGR-EXCH-003;
- accept proposal command -> SL-AGR-EXCH-004;
- request review approval -> SL-EMP-REQ-004.
```
