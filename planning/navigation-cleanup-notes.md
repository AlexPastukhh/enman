# Navigation Cleanup Notes

## Purpose

This cleanup removes old downstream references to:

```text
scenario-domain-design-input-core.md
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
```

as current workflow steps.

## Current official workflow

```text
scenario text specs
-> scenario DATA files
-> validation-related file
-> pre-domain-variants-input.md
-> domain model variant 1
-> domain model variant 2
-> compare/refine variants
-> choose domain model direction
-> then plan aggregates/slices/implementation
```

## Files updated by this cleanup

```text
planning/README.md
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/tables/00-planning-tables-index.md
planning/scenario-specification-principles.md
```

## Current active post-scenario bridge

```text
planning/tables/pre-domain-variants-input.md
```
