# Diagrams And Scenario Specs Index

Status: current navigation index for `planning/diagrams`

## 1. Current Semantic Source Of Truth

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-diagram-consistency-report.md
```

## 2. Subfolders

```text
scenario-text-specs/
  corrected textual scenario specs and validation/security addenda

scenario-data/
  per-scenario DATA specs

scenario-behavior-items/
  per-scenario behavior item files and lightweight behavior item index

scenario-*.drawio / .svg / .png
  visual artifacts, only authoritative after regenerated from corrected text specs
```

## 3. Scenario Questions

Central scenario-stage questions register:

```text
planning/diagrams/scenario-questions-register.md
```

Use it when questions affect scenario behavior, DATA, validation/security or visible outcome.

## 4. Scenario Behavior Items

Behavior item folder:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior items are derived from scenario text specs, DATA specs, validation/security addenda and existing compiled baselines.

## 5. Important Report

```text
planning/diagrams/scenario-diagram-consistency-report.md
```

## 6. Stale Package Summaries

The old package summaries are compatibility notes / stale visual summaries:

```text
planning/diagrams/scenario-core-package.md
planning/diagrams/scenario-extension-package.md
planning/diagrams/scenario-advanced-package.md
```

Do not use them as semantic source of truth for domain planning, slice planning or client planning.

## 7. How To Continue

For current planning, read:

```text
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/slices/README.md
```

Then continue to domain drafts, slice boundary drafts, parent vertical slice files or `.client.md` sidecars when concrete client work starts.
