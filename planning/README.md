# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Start Here

Use this read order for current planning work:

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/scenario-specification-principles.md
4. planning/scenario-domain-validation-principles.md
5. planning/diagrams/README.md
6. planning/diagrams/scenario-text-specs/README.md
7. planning/diagrams/scenario-data/README.md
8. planning/diagrams/scenario-diagram-consistency-report.md
9. planning/tables/README.md
10. planning/tables/scenario-domain-design-input-gate.md
11. planning/tables/scenario-domain-design-input-core.md
```

## 2. Current Planning Phase

The project is past the initial scenario correction phase.

Current phase:

```text
scenario specs + DATA
-> server/domain validation
-> scenario domain design input gate
-> value object candidates
-> state-transition invariants
-> aggregate boundary inputs
-> domain discovery
```

Active required bridge artifact:

```text
planning/tables/scenario-domain-design-input-core.md
```

Gate/contract for this artifact:

```text
planning/tables/scenario-domain-design-input-gate.md
```

Next artifact:

```text
planning/tables/domain-discovery-core.md
```

## 3. Scenario Domain Design Input Gate

After scenario text specs and scenario DATA files are ready, create or update:

```text
planning/tables/scenario-domain-design-input-core.md
```

This is a required gate before `domain-discovery-core.md`.

It must collect:

```text
- value object candidates;
- server-side/domain validation rules;
- state/status values;
- state-transition invariants;
- candidate domain methods;
- aggregate owner candidates;
- aggregate boundary pressure;
- unresolved domain decisions / ADR candidates.
```

This artifact exists because scenarios and DATA files describe behavior and data, but domain modeling needs an intermediate design input that extracts:

```text
what values must be valid
what states are persisted
what state transitions are allowed
what methods should guard those transitions
what object/aggregate should own those rules
```

Do not skip from scenarios/DATA directly to aggregate design.

Do not replace this gate with generic layer-responsibility tables.

## 4. Current Source Of Truth

Use:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/tables/scenario-domain-design-input-gate.md
planning/tables/scenario-domain-design-input-core.md
```

## 5. Stale / Compatibility Artifacts

Generated scenario package summaries and old draw.io pages may exist for visual reference.

They are not semantic source of truth until regenerated from corrected text specs.

Do not build planning tables from stale package summaries.

Superseded table directions:

```text
planning/tables/scenario-responsibility-core.md
planning/tables/scenario-domain-responsibility-core.md
```

Active replacement:

```text
planning/tables/scenario-domain-design-input-core.md
```

## 6. Folder Map

```text
planning/
  README.md
  planning-workflow-current.md
  scenario-specification-principles.md
  scenario-domain-validation-principles.md
  scenario-to-implementation-workflow-v5-consolidated.md  # superseded compatibility note

planning/diagrams/
  README.md
  scenario-text-specs/
  scenario-data/
  scenario-diagram-consistency-report.md

planning/tables/
  README.md
  00-planning-tables-index.md
  scenario-domain-design-input-gate.md
  scenario-domain-design-input-core.md
  domain-discovery-core.md                         # next
  ui-page-responsibility-map-core.md               # later, separate from domain discovery
```

## 7. Agent Rules

Planning agents should:

```text
- read current indexes first;
- use corrected text specs and DATA specs as scenario source of truth;
- keep DATA files narrow;
- keep validation in scenario specs/addendum and domain design input;
- use domain/value objects as validation source of truth;
- always pass through scenario-domain-design-input-core.md before domain discovery;
- treat state/status transition invariants as primary aggregate-boundary evidence;
- avoid implementation terms before scenario-to-slice planning;
- create or update files only when explicitly requested.
```
