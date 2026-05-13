# Planning Tables Index

Status: current table index

## Active

| Order | File | Status | Purpose |
|---:|---|---|---|
| 1 | `scenario-domain-design-input-gate.md` | current gate | Defines why/when/how `scenario-domain-design-input-core.md` is required. |
| 2 | `scenario-domain-design-input-core.md` | current active artifact | Extract value objects, validations, state invariants, domain methods, aggregate pressure. |
| 3 | `domain-discovery-core.md` | next | Turn design input into domain concepts and model options. |
| 4 | `aggregate-boundary-candidates-core.md` | later | Explore aggregate boundary variants. |
| 5 | `domain-model-options-core.md` | later | Compare domain model options. |
| 6 | `ui-page-responsibility-map-core.md` | later / separate | Map pages, visible data, actions, status states. |
| 7 | `scenario-to-slice-map-core.md` | later | Convert scenarios/domain decisions into implementation slices. |
| 8 | `slice-cards-core.md` | later | Describe each implementation slice. |
| 9 | `ports-adapters-map.md` | later | Identify ports/adapters after slices. |
| 10 | `testing-map.md` | later | Map test strategy. |
| 11 | `changeability-extensibility-map.md` | later | Consolidate EXT/VAR/RISK/ADR. |
| 12 | `adr-candidates.md` | later | Capture decisions. |
| 13 | `walking-skeleton-slice-delivery-plan.md` | later | Delivery sequence. |

## Required Gate

Before creating `domain-discovery-core.md`, ensure:

```text
scenario-domain-design-input-core.md
```

contains:

```text
- value object candidate catalog;
- state / invariant catalog;
- candidate domain methods;
- aggregate boundary pressure map;
- open domain decisions / ADR candidates.
```

## Superseded

| File | Replacement |
|---|---|
| `scenario-responsibility-core.md` | `scenario-domain-design-input-core.md` |
| `scenario-domain-responsibility-core.md` | `scenario-domain-design-input-core.md` |

## Current Next File To Create

```text
planning/tables/domain-discovery-core.md
```
