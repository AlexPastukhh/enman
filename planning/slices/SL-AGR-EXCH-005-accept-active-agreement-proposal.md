# SL-AGR-EXCH-005 — Accept Active Agreement Proposal

Status: planned slice boundary / full implementation draft pending  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: backend/API command slice + future client command sidecar  
Primary purpose: current actor accepts the active agreement proposal

## 1. Numbering / Placement Note

This slice intentionally uses `SL-AGR-EXCH-005`.

Canonical Agreement Exchange numbering:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

Old draft file name `SL-AGR-EXCH-004-accept-active-agreement-proposal.md` is superseded by this file.

## 2. Scope Boundary

```text
Actor opens active agreement proposal
  -> active proposal is acceptable by this actor
  -> actor accepts proposal
  -> exchange records accepted/finalized state
  -> read state reflects accepted agreement proposal
```

## 3. Preconditions Direction

```text
- exchange exists;
- exchange is active, not finally refused;
- active proposal is from the other side or otherwise acceptable according to scenario/domain rule;
- actor is allowed to accept in current exchange status;
- command does not create a new proposal version.
```

## 4. Domain / Actor Guardrails

```text
Client actions:
  client.Id == AgreementProposalExchange.ClientAccountId

Employee actions:
  no ResponsibleEmployeeId guard first pass;
  any active Employee can service the exchange.

Both:
  domain validates whose turn it is, active proposal sender and lifecycle state.
```

## 5. Out of Scope

```text
- initial exchange creation -> SL-AGR-EXCH-001;
- counter-proposal version creation -> SL-AGR-EXCH-002;
- exchange list read -> SL-AGR-EXCH-003;
- exchange details read -> SL-AGR-EXCH-004;
- final refusal -> SL-AGR-EXCH-006;
- document bytes/storage adapter -> future document/storage slice.
```

## 6. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-005-Q001` | planned | Who can accept active proposal? | The actor whose side is allowed by exchange status and active proposal author. | Needs domain confirmation in full draft. |
| `SL-AGR-EXCH-005-Q002` | accepted | Does accept create a new proposal version? | No. | Keeps accept separate from counter-proposal. |
| `SL-AGR-EXCH-005-Q003` | accepted | Does client ownership guard use ClientAccountId? | Yes. | Requires exchange.ClientAccountId. |
| `SL-AGR-EXCH-005-Q004` | accepted | Does Employee require ResponsibleEmployeeId? | No first pass. | Any active Employee can service exchange. |

## 7. Behavior Coverage

| Behavior item | How slice will cover it | Status |
|---|---|---|
| Actor accepts active proposal | command calls accept domain behavior | planned |
| Accept does not create a proposal version | domain command finalizes current active proposal | planned |
| Client cannot accept another client's exchange | ClientAccountId guard | planned |
| Employee access uses active-employee policy | no ResponsibleEmployeeId guard | planned |
| Exchange read state reflects accepted state | future read refresh through SL-AGR-EXCH-003/004 | planned |

## 8. Implementation Checklist

```text
[ ] confirm accept domain method and final status naming
[ ] add accept command DTO if body is needed
[ ] add accept command returning UnitResult<IReadOnlyList<Error>>
[ ] do not add per-command status enum
[ ] add accept command handler/application service method
[ ] resolve current actor from authenticated session
[ ] load AgreementProposalExchange
[ ] enforce ClientAccountId guard for client actor
[ ] enforce active Employee guard for employee actor
[ ] call accept domain method
[ ] persist exchange aggregate
[ ] return 204 No Content on success
[ ] map failures through existing Error/ProblemDetails mapping
[ ] add integration tests for auth/visibility/lifecycle/success
[ ] regenerate OpenAPI/types
```

## 9. Guardrail Summary

```text
Accept is not counter-proposal.

Accept does not create a new proposal version.

Client participant is protected through ClientAccountId.

Employee exchange-level ownership is not introduced first pass.

Server/domain remain authoritative; UI button visibility is not security.
```
