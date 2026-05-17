# SL-AGR-EXCH-004 — Accept Active Agreement Proposal

Status: planned slice boundary / full implementation draft pending  
Package: `[AgreementProposalExchange]`  
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

## 2. Preconditions Direction

```text
- exchange exists;
- exchange is active, not finally refused;
- active proposal is from the other side or otherwise acceptable according to scenario/domain rule;
- actor is allowed to accept in current exchange status;
- command does not create a new proposal version.
```

## 3. Out of Scope

```text
- initial exchange creation -> SL-AGR-EXCH-001;
- counter-proposal version creation -> SL-AGR-EXCH-002;
- exchange read model -> SL-AGR-EXCH-003;
- final refusal -> SL-AGR-EXCH-005;
- document bytes/storage adapter -> future document/storage slice.
```

## 4. Notes For Full Draft

Full draft must clarify exact accepted/final status naming from scenario/domain source before implementation.
