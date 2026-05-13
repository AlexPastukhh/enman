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
6. Scenario domain design input gate
7. Scenario domain design input
8. Domain discovery
9. Aggregate boundary candidates
10. Domain model options
11. UI page responsibility map
12. Scenario-to-slice map
13. Slice cards
14. Ports/adapters map
15. Testing map
16. Changeability/extensibility map
17. ADR candidates
18. Walking skeleton / delivery plan
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
planning/tables/scenario-domain-design-input-gate.md
planning/tables/scenario-domain-design-input-core.md
```

## 5. Scenario Domain Design Input Gate

After scenario text specs and scenario DATA specs are ready, create/update:

```text
planning/tables/scenario-domain-design-input-core.md
```

This file is required before:

```text
planning/tables/domain-discovery-core.md
```

The gate exists because scenario specs and DATA files are not yet enough to design aggregates directly.

Scenario specs tell us:

```text
- what actor does;
- what actor sees;
- what data is entered/selected/attached;
- what observable outcome exists.
```

Domain design input extracts:

```text
- value object candidates;
- server-side/domain validation rules;
- persisted state/status values;
- state-transition invariants;
- candidate domain methods;
- aggregate owner candidates;
- aggregate boundary pressure;
- domain decisions / ADR candidates.
```

The most important part of this gate is the state/invariant catalog:

```text
persisted state
-> invariant depending on that state
-> candidate method that changes/checks that state
-> candidate owner/aggregate
```

Example:

```text
Request.status = InReview
-> only InReview request can be Approved/Rejected
-> Request.Approve(...) / Request.Reject(...)
-> Request aggregate candidate
```

Do not skip this gate.

Do not replace it with generic responsibility-by-layer tables.

## 6. Active Next Step

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

## 7. Validation Rule

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

## 8. What Not To Do Now

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

## 9. Separate Track: UI Page Map

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
