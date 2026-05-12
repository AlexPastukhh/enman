# SC-18 — Archive / Audit

## Status

Deferred / low priority / unresolved.

## Decision

Do not focus on Archive / Audit as a detailed scenario now.

## Why

The user-facing behavior is unclear.

Open questions remain:

```text
who archives;
when archive is allowed;
whether client sees archived requests;
whether audit/history is user-visible;
whether this is a user-facing scenario or retention/audit policy.
```

## Possible Future Directions

### User-facing employee archive scenario

Employee opens final request  
-> archive action available?  
-> archive allowed?  
-> request moved out of active queue  
-> archive status visible  
-> audit/history remains visible

### Policy / cross-cutting concern

Request reaches terminal state  
-> retention/archive policy applies  
-> audit/history is retained

## Triggered By

If this becomes a background/policy process later, it may use triggered-by:

```text
Triggered by:
retention/archive policy applies after terminal request state.
```

Current decision: do not model now.

## Consistency Report Note

Mark SC-18 as deferred / unresolved / low priority.
