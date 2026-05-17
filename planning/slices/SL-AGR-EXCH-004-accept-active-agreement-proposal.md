# SL-AGR-EXCH-004 — Accept Active Agreement Proposal

Status: planned slice boundary / full implementation draft pending  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: backend/API command slice + future client command sidecar  
Primary purpose: current actor accepts the active agreement proposal

## 1. Scope Boundary

```text
Actor opens active agreement proposal
  -> active proposal is acceptable by this actor
  -> actor accepts proposal
  -> exchange records accepted/finalized state
  -> read state reflects accepted agreement proposal
```

## 2. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-004-Q001` | accepted | Does accept create a new proposal version? | No. Accept finalizes active proposal. | Keeps version semantics clean. |
| `SL-AGR-EXCH-004-Q002` | accepted | How does client ownership work? | Client accept methods check `client.Id == exchange.ClientAccountId`. | Requires exchange `ClientAccountId`. |
| `SL-AGR-EXCH-004-Q003` | accepted | Is there Employee ownership guard? | No first pass. Active Employee can service exchange if lifecycle allows. | No `ResponsibleEmployeeId`. |
| `SL-AGR-EXCH-004-Q004` | accepted | Where are accept lifecycle rules enforced? | In `AgreementProposalExchange`. | Handler stays thin. |
| `SL-AGR-EXCH-004-Q005` | open | Exact accepted/final status naming? | Confirm from scenario/domain source before full implementation draft. | Affects DTO/domain status mapping. |

## 3. Behavior Coverage

| Behavior item | How slice covers it | Status |
|---|---|---|
| Current actor accepts active proposal | command calls actor-specific domain method | planned |
| Client can only accept own exchange | domain checks `ClientAccountId` | planned |
| Active proposal must be acceptable by actor | domain checks active proposal author/status | planned |
| Accept does not create new version | domain finalizes current active version | planned |
| Accepted/finalized state is readable | `SL-AGR-EXCH-003` read reflects status | planned |

## 4. Preconditions Direction

```text
- exchange exists;
- exchange is active, not finally refused;
- active proposal is from the other side or otherwise acceptable according to scenario/domain rule;
- actor is allowed to accept in current exchange status;
- command does not create a new proposal version.
```

Client accept guard:

```text
client.Id == exchange.ClientAccountId
```

Employee accept guard first pass:

```text
employee is active / can perform agreement exchange action
no ResponsibleEmployeeId ownership lock
```

## 5. Out of Scope

```text
- initial exchange creation -> SL-AGR-EXCH-001;
- counter-proposal version creation -> SL-AGR-EXCH-002;
- exchange read model -> SL-AGR-EXCH-003;
- final refusal -> SL-AGR-EXCH-005;
- document bytes/storage adapter -> future document/storage slice.
```

## 6. Notes For Full Draft

Full draft must clarify exact accepted/final status naming from scenario/domain source before implementation.
