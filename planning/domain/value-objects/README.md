# Domain Value Objects Index

Status: current value object draft folder index  
Scope: reusable/non-trivial value objects and value-integrity concepts

## 1. Purpose

This folder will contain value object drafts.

Create a value object draft when the candidate is:

```text
- reused by multiple aggregates;
- backed by scenario behavior item value-integrity evidence;
- non-trivial enough to need invariants/normalization/equality rules;
- important enough to review separately from one aggregate draft.
```

Do not create a value object file for every primitive wrapper by default.

## 2. Drafting

Use:

```text
planning/domain/value-object-drafting-workflow.md
planning/domain/value-object-draft-template.md
planning/domain/scenario-to-aggregate-map.md
```

## 3. Relationship To Aggregates

Aggregate drafts should reference value object files in their `Value Objects Used` section.

Value object files own actual value shape, invariants, normalization, equality and persistence/serialization notes.
