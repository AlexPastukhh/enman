# Diagrams And Scenario Specs Index

Status: current navigation index for `planning/diagrams`

## 1. Current Semantic Source Of Truth

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-diagram-consistency-report.md
```

## 2. Subfolders

```text
scenario-text-specs/
  corrected textual scenario specs and validation addendum

scenario-data/
  per-scenario DATA specs

scenario-*.drawio / .svg / .png
  visual artifacts, only authoritative after regenerated from corrected text specs
```

## 3. Important Reports

```text
planning/diagrams/scenario-diagram-consistency-report.md
```

This report defines which scenario files are current, merged, removed or stale.

## 4. Stale Package Summaries

The old package summaries are compatibility notes / stale visual summaries:

```text
planning/diagrams/scenario-core-package.md
planning/diagrams/scenario-extension-package.md
planning/diagrams/scenario-advanced-package.md
```

Do not use them as semantic source of truth for domain planning.

## 5. How To Continue

For domain planning, read:

```text
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/scenario-domain-design-input-core.md
```

For diagram regeneration, read diagram-specific rules separately and regenerate diagrams from corrected text specs.
