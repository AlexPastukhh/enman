# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Workflow

The current planning workflow is the simplified domain-variant workflow:

```text
scenario text specs
-> scenario DATA files
-> validation-related file
-> pre-domain-variants-input.md
-> generate domain model variant 1
-> generate domain model variant 2
-> compare/refine variants
-> choose domain model direction
-> then plan aggregates/slices/implementation
```

The current next working step is:

```text
Generate domain model variant 1.
```

## 2. Current Read Order

Use this read order:

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
10. planning/tables/pre-domain-variants-input.md
```

## 3. Source Files For Domain Variants

Use exactly these inputs when generating domain model variants:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
```

Optional context:

```text
planning/diagrams/scenario-diagram-consistency-report.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
```

## 4. Current Bridge File

The only active post-scenario bridge file is:

```text
planning/tables/pre-domain-variants-input.md
```

It collects:

```text
- invariants;
- write state persisted by scenarios;
- state/status values;
- state-changing actions;
- state-dependent allowed/forbidden actions;
- no-write behavior;
- method pressure for domain variant generation.
```

It intentionally does not contain a value object catalog. Value objects are discovered while generating each domain variant.

## 5. Superseded / Not Current

Do not use this old path as the current workflow:

```text
scenario-domain-design-input-gate.md
-> scenario-domain-design-input-core.md
-> domain-discovery-core.md
-> aggregate-boundary-candidates-core.md
-> domain-model-options-core.md
```

Do not create these as active intermediate files now:

```text
planning/tables/scenario-domain-design-input-gate.md
planning/tables/scenario-domain-design-input-core.md
planning/tables/domain-discovery-core.md
planning/tables/aggregate-boundary-candidates-core.md
planning/tables/domain-model-options-core.md
planning/tables/scenario-responsibility-core.md
planning/tables/scenario-domain-responsibility-core.md
```

If such files exist historically, treat them as stale/superseded planning notes, not as current source of truth.

## 6. Diagram / Scenario Source Of Truth

Use corrected text specs and DATA specs as semantic source of truth.

Generated diagram package summaries and old `.drawio` pages may exist for visual reference, but they are not semantic source of truth until regenerated from corrected text specs.

## 7. Folder Map

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
  pre-domain-variants-input.md
```

## 8. Agent Rules

Planning agents should:

```text
- read current indexes first;
- use corrected scenario text specs and DATA specs as source of truth;
- keep DATA files narrow;
- keep validation in validation-related files and scenario specs;
- use pre-domain-variants-input.md before generating domain variants;
- generate domain variants one by one;
- avoid implementation terms before scenario-to-slice planning;
- create or update files only when explicitly requested.
```
