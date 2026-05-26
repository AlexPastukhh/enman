# Domain Value Object Draft Template

Status: current value object draft template  
Scope: output shape for reusable/non-trivial domain value objects

```text
# Domain Value Object Draft — <Value Object Name>

Status: draft / current / needs review
Scope: <value/integrity concept>

## 1. Purpose

What value integrity this object protects.

## 2. Source Inputs

Scenario sources:
- ...

Behavior items:
- ...

DATA sources:
- ...

Existing domain sources:
- ...

Not checked:
- ...

## 3. Used By

Aggregates:
- ...

Entities / child records:
- ...

Application/API references:
- ...

## 4. Shape / Fields

Fields:
- ...

Optional fields:
- ...

Forbidden fields:
- ...

## 5. Invariants

| Invariant | Source | Failure/error |
|---|---|---|
| ... | ... | ... |

## 6. Creation / Normalization Rules

Creation:
- ...

Normalization:
- ...

Rejected values:
- ...

## 7. Equality Rule

Equality is based on:
- ...

Identity is not:
- ...

## 8. Validation Boundary

Belongs in value object:
- ...

Belongs in DTO/input validation:
- ...

Belongs in aggregate/application:
- ...

## 9. Persistence / Serialization Notes

- ...

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| ... | ... | ... |

## 11. Questions / Decisions

Open:
- ...

Accepted:
- ...

## 12. Source Delta / Change Log

- ...
```
