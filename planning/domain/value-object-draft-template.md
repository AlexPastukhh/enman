# Domain Value Object Draft Template

Status: current value object draft template  
Doc version: v0.2.0  
Scope: output shape for reusable/non-trivial domain value objects with local section-level source coverage

````markdown
# Domain Value Object Draft — <Value Object Name>

Status: draft / current / needs review
Doc version: v0.1.0
Scope: <value/integrity concept>

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - <scenario/domain/aggregate/behavior source> @ <Doc version/status>
  Internal dependencies:
    - none
  Not checked:
    - <explicitly unchecked source/evidence>
```

What value integrity this object protects.

## 2. Source Inputs

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - <scenario sources> @ <Doc version/status>
    - <behavior items> @ <Doc version/status>
    - <DATA sources> @ <Doc version/status>
    - <existing domain sources> @ <Doc version/status>
  Internal dependencies:
    - none
  Not checked:
    - <explicitly unchecked source/evidence>
```

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

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
  Content:
    - <aggregate draft using this value object> @ <Doc version/status>
    - <implementation/API/client references if checked> @ <implementation evidence/status>
  Internal dependencies:
    - Source Inputs
  Not checked:
    - <usage evidence not checked>
```

Aggregates:
- ...

Entities / child records:
- ...

Application/API references:
- ...

## 4. Shape / Fields

```text
Sources:
  Format/process:
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - <behavior/domain/implementation source for fields> @ <Doc version/status>
  Internal dependencies:
    - Source Inputs
    - Used By
  Not checked:
    - <field-shape evidence not checked>
```

Fields:
- ...

Optional fields:
- ...

Forbidden fields:
- ...

## 5. Invariants

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - <behavior/domain/implementation source for invariants> @ <Doc version/status>
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - <invariant evidence not checked>
```

| Invariant | Source | Failure/error |
|---|---|---|
| ... | ... | ... |

## 6. Creation / Normalization Rules

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
  Content:
    - <behavior/domain/implementation source for creation or normalization> @ <Doc version/status>
  Internal dependencies:
    - Shape / Fields
    - Invariants
  Not checked:
    - <creation/normalization evidence not checked>
```

Creation:
- ...

Normalization:
- ...

Rejected values:
- ...

## 7. Equality Rule

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
  Content:
    - <domain/implementation source for equality> @ <Doc version/status>
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - <equality evidence not checked>
```

Equality is based on:
- ...

Identity is not:
- ...

## 8. Validation Boundary

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - <aggregate/API/client/domain source for validation boundary> @ <Doc version/status>
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
  Not checked:
    - <validation boundary evidence not checked>
```

Belongs in value object:
- ...

Belongs in DTO/input validation:
- ...

Belongs in aggregate/application:
- ...

## 9. Persistence / Serialization Notes

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
  Content:
    - <aggregate persistence / EF / implementation source if checked> @ <Doc version/status>
  Internal dependencies:
    - Shape / Fields
    - Used By
  Not checked:
    - <persistence/serialization evidence not checked>
```

- ...

## 10. Invalid Examples

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
  Content:
    - <invariant/behavior source for invalid examples> @ <Doc version/status>
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
    - Validation Boundary
  Not checked:
    - <invalid-example evidence not checked>
```

| Invalid value/state | Why invalid | Source |
|---|---|---|
| ... | ... | ... |

## 11. Questions / Decisions

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
  Content:
    - <open decision source or unresolved source conflict> @ <Doc version/status>
  Internal dependencies:
    - Source Inputs
    - Invariants
    - Validation Boundary
  Not checked:
    - <decision evidence not checked>
```

Open:
- ...

Accepted:
- ...

## 12. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - <changed source or reason for this draft change> @ <Doc version/status>
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - <sources intentionally not rechecked in this change>
```

- ...
````
