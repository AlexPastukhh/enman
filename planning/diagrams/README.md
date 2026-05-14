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

## 3. Important Report

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

Do not use them as semantic source of truth for domain planning or UI planning.

## 5. How To Continue To Domain Drafts

For gradual domain discovery, read:

```text
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/domain-draft-generation-guide.md
planning/tables/domain-drafts/README.md
```

Then create/refine:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

Do not route diagram/domain planning through:

```text
planning/tables/scenario-domain-design-input-core.md
planning/tables/domain-discovery-core.md
planning/tables/domain-variants/
```

Those are not the current workflow.

## 6. How To Continue To UI Planning

For textual UI planning, read:

```text
planning/ui/README.md
planning/ui/ui-planning-workflow.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
```

Then create/update:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```

Do not create final UI design or HTML prototype directly from scenario files.

Create the textual UI plan first.
