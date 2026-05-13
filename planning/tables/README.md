# Planning Tables Index

Status: current planning tables navigation index

## 1. Current Active Gate

Read first:

```text
planning/tables/scenario-domain-design-input-gate.md
```

Active bridge artifact:

```text
planning/tables/scenario-domain-design-input-core.md
```

This is the required bridge from scenarios/DATA/validation to domain model design.

## 2. Why This Table Exists

Do not skip directly from scenario specs and DATA files to aggregate design.

The workflow needs an intermediate design input that extracts:

```text
- value object candidates;
- server-side/domain validation rules;
- persisted state/status values;
- state-transition invariants;
- candidate domain methods;
- aggregate owner candidates;
- aggregate boundary pressure;
- open domain decisions / ADR candidates.
```

This is what `scenario-domain-design-input-core.md` does.

## 3. Superseded

```text
planning/tables/scenario-responsibility-core.md
planning/tables/scenario-domain-responsibility-core.md
```

These older responsibility-table directions are superseded by:

```text
planning/tables/scenario-domain-design-input-core.md
```

## 4. Current Table Order

```text
1. scenario-domain-design-input-gate.md
2. scenario-domain-design-input-core.md
3. domain-discovery-core.md
4. aggregate-boundary-candidates-core.md
5. domain-model-options-core.md
6. ui-page-responsibility-map-core.md
7. scenario-to-slice-map-core.md
8. slice-cards-core.md
9. ports-adapters-map.md
10. testing-map.md
11. changeability-extensibility-map.md
12. adr-candidates.md
13. walking-skeleton-slice-delivery-plan.md
```

## 5. Current Next Step

Create:

```text
planning/tables/domain-discovery-core.md
```

Input:

```text
planning/tables/scenario-domain-design-input-gate.md
planning/tables/scenario-domain-design-input-core.md
```

## 6. Rule

Do not create database schema, endpoints, handlers or UI components from `scenario-domain-design-input-core.md` directly.

First produce:

```text
planning/tables/domain-discovery-core.md
planning/tables/aggregate-boundary-candidates-core.md
planning/tables/domain-model-options-core.md
```
