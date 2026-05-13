# Planning Tables Index

Status: simplified current planning tables navigation

## Current stage

Scenario text specs and DATA files are ready.

Validation-related file is kept separately.

Before generating domain variants, use:

```text
planning/tables/pre-domain-variants-input.md
```

## Current read order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
```

## Why this file exists

`pre-domain-variants-input.md` collects the material needed before generating domain variants:

```text
- invariants;
- write state;
- persisted statuses;
- state-changing actions;
- state-dependent allowed/forbidden actions;
- no-write behavior.
```

It intentionally does not contain a value object catalog.

Value objects should be discovered directly while generating each domain variant from:

```text
scenario specs
DATA files
validation-related file
pre-domain-variants-input.md
```

## Replacement file generation

Manual replacement-file generation is documented in:

```text
planning/replacement-file-generation-guide.md
```

When the user asks for replacement files:

```text
- generate complete files;
- keep repository-relative paths;
- package them in a zip;
- include previous unapplied archive content if the user says it was not applied.
```

## Current next step

```text
Generate domain model variant 1.
```

Then generate more variants until one is selected/refined.

## Avoid for now

Do not add more intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
```

Those names are too technical and fragmented for the current workflow.

Keep the post-scenario bridge as one file:

```text
pre-domain-variants-input.md
```
