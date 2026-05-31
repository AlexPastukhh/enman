# Domain Modeling Principles

Status: current domain modeling principles  
Doc version: v0.1.0  
Scope: aggregate boundaries, child entities, value objects, cross-aggregate relations and persistence boundary notes

## 1. Purpose

This file owns domain modeling rules that are not specific to one aggregate, value object or decision.

## 2. Aggregate Boundary Principles

```text
An aggregate draft describes one aggregate boundary.
The aggregate root owns lifecycle, state changes and invariant protection for its child entities.
Child entities are created and modified through the owning aggregate root.
One aggregate root should not directly create or mutate another aggregate root.
```

## 3. Cross-Aggregate Principles

```text
External aggregates should be referenced by identifier/reference, not owned as child entities.
Cross-aggregate invariants are application coordination unless one aggregate clearly owns the rule.
Application services may coordinate several aggregates but should not hide aggregate-owned invariants.
```

## 4. Value Object Principles

```text
Value objects protect value integrity.
Create separate value object draft files only when the value concept is reusable, non-trivial or source-backed by behavior/value-integrity items.
Value objects should not own aggregate lifecycle rules.
DTO validation and value object invariants must not be confused.
```

## 5. Read/Write Boundary Principles

```text
Read/query convenience does not define write aggregate boundaries.
A read model may join data that should remain separate aggregates for write behavior.
Do not infer ownership only from UI/read shape.
```

## 6. Persistence / EF Boundary Principles

```text
EF navigation convenience is not domain ownership.
Optional navigations can support persistence/read convenience without becoming domain traversal rules.
Primitive ID collections are not enough to define aggregate relationships by themselves.
Persistence shape should be documented when it affects aggregate boundaries, but it should not replace source-backed behavior analysis.
```

## 7. Source Discipline

```text
Start from scenario behavior sources and domain decisions.
Use historical monolithic drafts as source snapshots/cross-checks, not as current target structure.
Record questions when ownership is unclear instead of forcing a class/aggregate boundary.
```
