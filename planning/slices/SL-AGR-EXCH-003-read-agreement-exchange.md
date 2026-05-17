# SL-AGR-EXCH-003 — Read Agreement Exchange

Status: planned slice boundary / full implementation draft pending  
Package: `[AgreementProposalExchange]`  
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

## 2. Read Data Direction

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

## 3. Out of Scope

```text
- start exchange command -> SL-AGR-EXCH-001;
- send counter-proposal command -> SL-AGR-EXCH-002;
- accept proposal command -> SL-AGR-EXCH-004;
- final refusal command -> SL-AGR-EXCH-005;
- document bytes/download/storage adapter -> future document/storage slice.
```

## 4. Notes For Full Draft

Read endpoint(s) may be actor-specific even if they project the same exchange aggregate.

```text
Client read boundary should not expose employee-private fields.
Employee read boundary should not require client-only ownership assumptions.
```
