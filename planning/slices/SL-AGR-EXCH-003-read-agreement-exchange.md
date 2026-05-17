# SL-AGR-EXCH-003 — Read Agreement Exchange

Status: planned slice boundary / full implementation draft pending  
Package: `[L2] Agreement Proposal Exchange`  
Slice type: backend/API read slice + future client read sidecars  
Primary purpose: read current agreement exchange state, active proposal and proposal versions

## 1. Scope Boundary

This slice owns read model shape for active/historical AgreementProposalExchange state.

```text
Client/Employee opens agreement exchange surface
  -> system returns exchange status
  -> system returns active proposal version
  -> system returns proposal version history/metadata needed for UI
  -> system returns current actor action availability
```

## 2. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-AGR-EXCH-003-Q001` | accepted | Can read model be shared? | Use shared read shape where practical. | Avoids duplicate projections. |
| `SL-AGR-EXCH-003-Q002` | accepted | Is access shared? | No. Read service uses actor-specific filters. | Prevents client data exposure. |
| `SL-AGR-EXCH-003-Q003` | accepted | How does client access filter work? | Client can read only if `exchange.ClientAccountId == currentClientAccountId`. | Requires `ClientAccountId`. |
| `SL-AGR-EXCH-003-Q004` | accepted | How does Employee access filter work first pass? | Any active Employee can read/service review-relevant agreement exchanges. | Assignment visibility is future. |
| `SL-AGR-EXCH-003-Q005` | accepted | Are transition invariants enforced in reads? | No. Reads expose state/action availability; commands/domain enforce transitions. | Keeps read side simple. |

## 3. Behavior Coverage

| Behavior item | How slice covers it | Status |
|---|---|---|
| Client sees own agreement exchange | query filters by `ClientAccountId` | planned |
| Wrong client cannot read exchange | query excludes non-owned exchange | planned |
| Employee can read first-pass exchange work | active Employee read policy | planned |
| Read returns active proposal | projection includes active version/document/comment/author | planned |
| Read returns current actor action availability | projection derives actor-relative actions | planned |
| Read does not mutate exchange | read endpoint only | planned |

## 4. Read Data Direction

```text
- request id / exchange id;
- exchange status;
- active proposal version number;
- active proposal author side;
- agreement document reference metadata;
- optional proposal comment;
- proposal version list/history if scenario requires it;
- actor-relative action availability.
```

## 5. Read Access Direction

Client read:

```text
return exchange only if exchange.ClientAccountId == current client account id
```

Employee read:

```text
first pass: any active Employee can read/service agreement exchanges relevant to employee work
future: department/region/assignment filters
```

## 6. Out of Scope

```text
- start exchange command -> SL-AGR-EXCH-001;
- send counter-proposal command -> SL-AGR-EXCH-002;
- accept proposal command -> SL-AGR-EXCH-004;
- final refusal command -> SL-AGR-EXCH-005;
- document bytes/download/storage adapter -> future document/storage slice.
```

## 7. Notes For Full Draft

Read endpoint(s) may be actor-specific even if they project the same exchange aggregate.

```text
Client read boundary should not expose employee-private fields.
Employee read boundary should not require client-only ownership assumptions.
```
