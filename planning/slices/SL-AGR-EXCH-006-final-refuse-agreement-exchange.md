# SL-AGR-EXCH-006 — Final Refuse Agreement Exchange

Status: planned slice boundary / full implementation draft pending  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: backend/API command slice + future client command sidecar  
Primary purpose: Employee finally refuses agreement exchange and request receives exchange-failed result

## 1. Numbering / Placement Note

This slice intentionally uses `SL-AGR-EXCH-006`.

Canonical Agreement Exchange numbering:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Old draft file name `SL-AGR-EXCH-005-final-refuse-agreement-exchange.md` is superseded by this file.

## 2. Scope Boundary

```text
Employee opens active agreement exchange
  -> Employee chooses final refusal
  -> exchange.FinalRefuseProposal(...) marks exchange FinallyRefused
  -> request.MarkAgreementExchangeFailed(...) marks request result through separate Request domain method
  -> both aggregates are saved by application service orchestration
```

## 3. Domain Direction

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

## 4. Direct Exchange State

```text
FinalRefusedByEmployeeId?
FinalRefusedAt?
FinalRefusalReason?
```

No separate `AgreementFinalRefusal` class/entity is needed for the current direction.

Final refusal does not create a new proposal version.

## 5. Actor / Ownership Guardrails

```text
Final refusal is Employee-only in current direction.

No ResponsibleEmployeeId authorization guard first pass.

Any active Employee can service/final-refuse an exchange if domain lifecycle allows it.

Proposal authors remain tracked per proposal version.
```

## 6. Out of Scope

```text
- initial exchange creation -> SL-AGR-EXCH-001;
- counter-proposal version creation -> SL-AGR-EXCH-002;
- exchange list read -> SL-AGR-EXCH-003;
- exchange details read -> SL-AGR-EXCH-004;
- accept proposal command -> SL-AGR-EXCH-005;
- request review approval -> SL-EMP-REQ-004.
```

## 7. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-006-Q001` | accepted | Is final refusal Employee-only? | Yes in current direction. | Client final refusal is not part of first pass. |
| `SL-AGR-EXCH-006-Q002` | accepted | Does final refusal create proposal version? | No. | Direct exchange state only. |
| `SL-AGR-EXCH-006-Q003` | accepted | Does Exchange mutate Request directly? | No. | Application service orchestrates both aggregates. |
| `SL-AGR-EXCH-006-Q004` | accepted | Does Request hold navigation to Exchange? | No. | Aggregates remain separate. |
| `SL-AGR-EXCH-006-Q005` | accepted | Does Employee require ResponsibleEmployeeId? | No first pass. | Any active Employee can service exchange. |

## 8. Behavior Coverage

| Behavior item | How slice will cover it | Status |
|---|---|---|
| Employee can final-refuse active exchange | command calls exchange.FinalRefuseProposal | planned |
| Exchange becomes FinallyRefused | exchange state mutation | planned |
| Request receives AgreementExchangeFailed result | application service calls request.MarkAgreementExchangeFailed | planned |
| Final refusal stores employee/time/reason | direct exchange state | planned |
| No proposal version is created | domain guard | planned |
| Exchange and Request remain separate aggregates | application service orchestration | planned |

## 9. Implementation Checklist

```text
[ ] confirm final refusal endpoint and DTO
[ ] add final refusal command returning UnitResult<IReadOnlyList<Error>>
[ ] do not add per-command status enum
[ ] add final refusal handler/application service method
[ ] resolve active Employee from authenticated session
[ ] load AgreementProposalExchange
[ ] load related Request aggregate
[ ] call exchange.FinalRefuseProposal(...)
[ ] call request.MarkAgreementExchangeFailed(...)
[ ] persist both aggregates atomically
[ ] return 204 No Content on success
[ ] map failures through existing Error/ProblemDetails mapping
[ ] add integration tests for auth/lifecycle/cross-aggregate persistence
[ ] regenerate OpenAPI/types
```

## 10. Guardrail Summary

```text
Final refusal is not reject review.

Final refusal is not counter-proposal.

Final refusal does not create a proposal version.

AgreementProposalExchange and Request stay separate aggregates.

Application service orchestrates cross-aggregate state change.

No ResponsibleEmployeeId guard first pass.
```
