# Planning Tables Index

Status: current planning tables navigation index

## 1. Current Active Table

Use:

```text
planning/tables/scenario-domain-design-input-core.md
```

This is the current bridge from scenarios/DATA/validation to domain model design.

## 2. Superseded

```text
planning/tables/scenario-responsibility-core.md
planning/tables/scenario-domain-responsibility-core.md
```

These older responsibility-table directions are superseded by `scenario-domain-design-input-core.md`.

## 3. Current Table Order

```text
1. scenario-domain-design-input-core.md
2. domain-discovery-core.md
3. aggregate-boundary-candidates-core.md
4. domain-model-options-core.md
5. ui-page-responsibility-map-core.md
6. scenario-to-slice-map-core.md
7. slice-cards-core.md
8. ports-adapters-map.md
9. testing-map.md
10. changeability-extensibility-map.md
11. adr-candidates.md
12. walking-skeleton-slice-delivery-plan.md
```

## 4. Current Next Step

Create:

```text
planning/tables/domain-discovery-core.md
```

Input:

```text
planning/tables/scenario-domain-design-input-core.md
```

## 5. Rule

Do not create database schema, endpoints, handlers or UI components from `scenario-domain-design-input-core.md` directly.

First produce domain discovery and aggregate boundary options.
