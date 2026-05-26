# Domain Planning Index

Status: current domain-layer entrypoint / aggregate-based target model  
Scope: domain discovery, aggregate drafts, value object drafts, domain notes and domain decisions

## 1. Purpose

This folder owns the current target documentation model for domain planning.

Use it for:

```text
scenario behavior sources -> domain discovery -> scenario-to-aggregate map -> aggregate/value-object drafts
```

This folder does not own scenario specs, slice drafts, API contracts, testing workflows or implementation code.

## 2. Current Model

The target domain model is aggregate-based:

```text
planning/domain/scenario-to-aggregate-map.md
  scenario behavior sources -> aggregate/value-object candidates and cross-aggregate relations.

planning/domain/aggregates/
  one file per aggregate boundary.

planning/domain/value-objects/
  one file per reusable/non-trivial value object or value-integrity concept.

planning/domain/decisions/
  accepted/proposed domain decisions that are not just notes and not owned by one aggregate.

planning/domain/domain-notes-register.md
  loose domain notes that should not be lost but are not yet owned by a specific file.
```

## 3. Transitional Sources

Older domain files remain source material during migration:

```text
planning/domain-draft-generation-guide.md
planning/domain-model.md
planning/domain-design-input-navigation-notes.md
planning/scenario-domain-validation-principles.md
planning/tables/domain-drafts/
planning/tables/pre-domain-variants-input.md
```

Older monolithic domain drafts are historical discovery snapshots, not the target current shape for new domain docs.

## 4. Read Order

For domain discovery:

```text
1. planning/domain/README.md
2. planning/domain/domain-responsibility-map.md
3. planning/domain/domain-modeling-principles.md
4. planning/domain/domain-discovery-workflow.md
5. planning/domain/scenario-to-aggregate-map.md
6. scenario sources and behavior items
```

For aggregate drafting:

```text
1. planning/domain/README.md
2. planning/domain/domain-responsibility-map.md
3. planning/domain/domain-modeling-principles.md
4. planning/domain/scenario-to-aggregate-map.md
5. planning/domain/aggregate-drafting-workflow.md
6. planning/domain/aggregate-draft-template.md
7. relevant scenario/domain sources
```

For value object drafting:

```text
1. planning/domain/value-object-drafting-workflow.md
2. planning/domain/value-object-draft-template.md
3. related aggregate drafts and VI behavior items
```

## 5. Related Layers

```text
planning/diagrams/
  owns scenario text specs, DATA, behavior items, clarifications and scenario questions.

planning/tables/
  owns compiled/historical baselines and pre-domain source snapshots.

planning/slices/
  owns slice drafts and behavior-to-test trace inside slice drafts.

planning/testing/
  owns cross-slice testing principles and reusable testing workflows.
```
