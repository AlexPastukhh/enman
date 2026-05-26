# Domain Aggregate Draft Template

Status: current aggregate draft template  
Scope: output shape for one aggregate boundary

```text
# Domain Aggregate Draft — <Aggregate Name>

Status: draft / current / needs review
Scope: <aggregate boundary>

## 1. Purpose

What business behavior this aggregate owns.

## 2. Source Inputs

Scenario sources:
- ...

Behavior items:
- ...

DATA sources:
- ...

Clarifications / questions:
- ...

Existing domain sources:
- ...

Not checked:
- ...

## 3. Aggregate Boundary

Aggregate root:
- ...

Child entities:
- ...

Owned value-like child records:
- ...

Not part of this aggregate:
- ...

External aggregate references:
- ...

## 4. Owned State

Root state:
- ...

Child state:
- ...

Derived/read-only state:
- ...

Not stored here:
- ...

## 5. Domain Methods / Commands

### <MethodName>

Purpose:
- ...

Input:
- ...

Preconditions:
- ...

State changes:
- ...

Domain errors:
- ...

Observable result:
- ...

Source behavior:
- ...

## 6. Invariants

| Invariant | Protected by | Source | Failure/error |
|---|---|---|---|
| ... | ... | ... | ... |

## 7. Lifecycle / State Machine

States:
- ...

Allowed transitions:
- ...

Forbidden transitions:
- ...

## 8. Impossible States Prevented

| Impossible state | Prevented by | Source |
|---|---|---|
| ... | ... | ... |

## 9. Value Objects Used

| Value object | File | Purpose in this aggregate |
|---|---|---|
| ... | planning/domain/value-objects/... | ... |

## 10. Cross-Aggregate Relations

References to other aggregates:
- ...

Rules not owned here:
- ...

Application coordination needed:
- ...

## 11. Behavior Coverage

| Source item | Covered by | Status | Notes |
|---|---|---|---|
| CMD-... | method/invariant/state | covered / partial / outside domain / deferred | ... |

## 12. Persistence / EF Notes

Only when needed:
- owned entities;
- navigation policy;
- IDs/references;
- transaction boundary notes.

## 13. Cross-Layer Placement Notes

Application layer:
- ...

API:
- ...

Client:
- ...

Testing:
- ...

## 14. Questions / Decisions

Open:
- ...

Accepted:
- ...

Deferred:
- ...

## 15. Source Delta / Change Log

What changed:
- ...
```
