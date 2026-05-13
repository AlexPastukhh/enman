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
10. planning/tables/scenario-domain-design-input-core.md
```

## 2. Current Planning Phase

The project is past the initial scenario correction phase.

Current phase:

```text
scenario specs + DATA
-> server/domain validation
-> value object candidates
-> state-transition invariants
-> aggregate boundary inputs
```

Active artifact:

```text
planning/tables/scenario-domain-design-input-core.md
```

Next artifact:

```text
planning/tables/domain-discovery-core.md
```

## 3. Current Source Of Truth

Use:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/tables/scenario-domain-design-input-core.md
```

## 4. Stale / Compatibility Artifacts

Generated scenario package summaries and old draw.io pages may exist for visual reference.

They are not semantic source of truth until regenerated from corrected text specs.

Do not build planning tables from stale package summaries.

## 5. Folder Map

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
  scenario-domain-design-input-core.md
  domain-discovery-core.md                         # next
  ui-page-responsibility-map-core.md               # later, separate from domain discovery
```

## 6. Agent Rules

Planning agents should:

```text
- read current indexes first;
- use corrected text specs and DATA specs as scenario source of truth;
- keep DATA files narrow;
- keep validation in scenario specs/addendum and domain design input;
- use domain/value objects as validation source of truth;
- avoid implementation terms before scenario-to-slice planning;
- create or update files only when explicitly requested.
```
