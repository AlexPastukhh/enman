# Current Planning Workflow

Status: current workflow source of truth  
Supersedes: `planning/scenario-to-implementation-workflow-v5-consolidated.md` for current project planning

## 1. Purpose

This workflow describes the current reliable path from corrected scenarios to domain model and implementation planning.

The important correction from older workflow drafts:

```text
Do not stop at broad responsibility-by-layer tables.
Move from scenarios to domain design input:
value objects, domain validation, state transitions, invariants and aggregate boundary pressure.
```

## 2. Current Workflow

```text
0. Planning navigation / indexes
1. Scenario specification principles
2. Corrected scenario text specs
3. Scenario DATA specs
4. Scenario consistency report
5. Server/domain validation addendum
6. Scenario domain design input
7. Domain discovery
8. Aggregate boundary candidates
9. Domain model options
10. UI page responsibility map
11. Scenario-to-slice map
12. Slice cards
13. Ports/adapters map
14. Testing map
15. Changeability/extensibility map
16. ADR candidates
17. Walking skeleton / delivery plan
```

## 3. Current Completed Inputs

```text
planning/scenario-specification-principles.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-diagram-consistency-report.md
```

## 4. Current Added Inputs

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/scenario-domain-design-input-core.md
```

## 5. Active Next Step

Create:

```text
planning/tables/domain-discovery-core.md
```

Inputs:

```text
planning/tables/scenario-domain-design-input-core.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
```

Output should propose:

```text
- confirmed domain concepts;
- value object set;
- entity candidates;
- aggregate root options;
- aggregate boundary variants;
- recommended first implementation cut;
- domain method candidates per aggregate;
- ADR candidates.
```

## 6. Validation Rule

The current validation model is:

```text
client-side validation = UX feedback only;
server-side validation = authoritative use-case boundary validation;
domain/value-object validation = source of truth for data validity and state-transition validity.
```

Use this flow:

```text
form/input payload
-> application use case
-> value object construction / domain method
-> validation result
-> server validation response or successful state change
```

## 7. What Not To Do Now

Do not create:

```text
- database schema;
- repository design;
- controller/endpoint map;
- React component plan;
- CQRS command/query map;
- final aggregate implementation;
- ports/adapters map before slice planning.
```

Do not use stale diagram package summaries as source of truth.

## 8. Separate Track: UI Page Map

UI/page planning is useful, but it should not be mixed into domain discovery.

Create separately after domain design input:

```text
planning/tables/ui-page-responsibility-map-core.md
```

It should cover:

```text
- pages;
- visible DATA per page;
- actions per page;
- status-dependent UI states;
- navigation between pages/scenarios.
```
