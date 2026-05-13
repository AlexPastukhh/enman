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

It also adds the repository rule for manual replacement-file generation:

```text
planning/replacement-file-generation-guide.md
```

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

## Current replacement-file workflow

When the user asks for files to replace manually:

```text
assistant generates complete files
-> packages repository-relative paths in a zip
-> user replaces files manually
-> user commits
-> assistant checks repository state if requested
```

If the user says a previous archive was not applied, the next archive must include:

```text
- all files from the previous archive;
- plus the newly requested updates.
```

## Files updated by this cleanup

```text
planning/README.md
planning/planning-workflow-current.md
planning/replacement-file-generation-guide.md
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/tables/README.md
planning/tables/00-planning-tables-index.md
planning/scenario-specification-principles.md
```

## Current active post-scenario bridge

```text
planning/tables/pre-domain-variants-input.md
```
